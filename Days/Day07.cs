using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using ClassLib.util;

namespace ClassLib
{
    public class Day07 : AocDay
    {
        private static readonly Regex Regex =
            new Regex(@"([a-z]*|\d*)?\s?(NOT|AND|OR|[R|L]SHIFT)?(\s\d*)?\s?([a-z]*)(\d*)?\s->\s([a-z]*)");

        private enum Operation
        {
            None,
            Simple,
            And,
            Or,
            Not,
            Rshift,
            Lshift
        }

        #region Wire
        private class Wire
        {
            private string InstructionString { get; }
            public string InputWireCode1 { get; }
            public string InputWireCode2 { get; }
            public int? InputSignal { get; set; }
            public Operation Operation { get; }
            public int ShiftArgument { get; }
            public string OutputWireCode { get; }
            public int? OutputSignal { get; set; }

            private static readonly Dictionary<string, Operation> OperationMapping =
                new Dictionary<string, Operation>()
                {
                    {"NOT", Operation.Not},
                    {"OR", Operation.Or},
                    {"AND", Operation.And},
                    {"LSHIFT", Operation.Lshift},
                    {"RSHIFT", Operation.Rshift},
                };
            
            public Wire()
            {
            }

            public Wire(string instr)
            {
                InstructionString = instr;
                var match = Regex.Match(instr);
                InputWireCode1 = match.Groups[1].Value;
                Operation = GetOperation(match);

                var hasShiftArgument = int.TryParse(match.Groups[3].Value, out var shiftArgument);
                ShiftArgument = hasShiftArgument ? shiftArgument : 0;

                InputWireCode2 = match.Groups[4].Value;

                var hasSignal = int.TryParse(match.Groups[5]?.Value, out var inputSignal);
                if (hasSignal)
                {
                    InputSignal = inputSignal;
                }

                OutputWireCode = match.Groups[6].Value;
            }

            private Operation GetOperation(Match match)
            {
                var operationStr = match.Groups[2].Value;
                if (string.IsNullOrEmpty(operationStr))
                {
                    if (!string.IsNullOrEmpty(match.Groups[5].Value))
                    {
                        return Operation.None;
                    }

                    if (!string.IsNullOrEmpty(match.Groups[1].Value))
                    {
                        return Operation.Simple;
                    }
                }

                if (OperationMapping.TryGetValue(operationStr, out var operation))
                {
                    return operation;
                }

                throw new NotSupportedException($"Operation {operationStr} is not supported.");
            }

            public override string ToString()
            {
                return InstructionString;
            }
        }
        #endregion

        #region CircuitBoard
        private class CircuitBoard
        {
            public IList<Wire> Wires { get; } = new List<Wire>();

            public int ProcessSignal(Wire input)
            {
                if (input.InputSignal.HasValue)
                {
                    input.OutputSignal = input.InputSignal.Value;
                    return input.InputSignal.Value;
                }

                if (input.OutputSignal.HasValue)
                {
                    return input.OutputSignal.Value;
                }

                var inputWire1 = GetWireByCode(input.InputWireCode1);
                var inputWire2 = GetWireByCode(input.InputWireCode2);
                var output = input.Operation switch
                {
                    Operation.None => input.InputSignal.Value,
                    Operation.Simple => ProcessSignal(inputWire1),
                    Operation.And => ProcessSignal(inputWire1) & ProcessSignal(inputWire2),
                    Operation.Or => ProcessSignal(inputWire1) | ProcessSignal(inputWire2),
                    Operation.Not => ~ ProcessSignal(inputWire2),
                    Operation.Lshift => ProcessSignal(inputWire1) << input.ShiftArgument,
                    Operation.Rshift => ProcessSignal(inputWire1) >> input.ShiftArgument
                };

                input.OutputSignal = output;
                
                return output;
            }

            private Wire? GetWireByCode(string wireCode)
            {
                if (int.TryParse(wireCode, out int signal))
                {
                    return new Wire()
                    {
                        InputSignal = signal
                    };
                }

                return wireCode != null ? Wires.FirstOrDefault(w => w.OutputWireCode == wireCode) : null;
            }
        }
        #endregion

        public int GetSignalOfWireA()
        {
            var circuitBoard = new CircuitBoard();
            foreach (var instr in Input)
            {
                var wire = new Wire(instr);
                circuitBoard.Wires.Add(wire);
            }

            var startingWire = circuitBoard.Wires.FirstOrDefault((w => w.OutputWireCode == "a"));
            var signalOutput = circuitBoard.ProcessSignal(startingWire);

            return signalOutput;
        }

        public int GetSignalWithNewValueOfWireA(int newSignalValue)
        {
            var circuitBoard = new CircuitBoard();
            foreach (var instr in Input)
            {
                var wire = new Wire(instr);
                circuitBoard.Wires.Add(wire);
            }

            var wireToOverride = circuitBoard.Wires.FirstOrDefault(w => w.OutputWireCode == "b");
            wireToOverride.InputSignal = newSignalValue;
            
            var startingWire = circuitBoard.Wires.FirstOrDefault((w => w.OutputWireCode == "a"));
            var signalOutput = circuitBoard.ProcessSignal(startingWire);

            return signalOutput;
        }
    }
}