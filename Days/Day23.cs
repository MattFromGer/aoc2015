using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using ClassLib.util;

namespace ClassLib
{
    public class Day23 : AocDay
    {
        private readonly Regex _inputRegex = new Regex(@"(\w{3})\s(?:(.\d+)|(a|b))(?:,\s(.\d+))?");

        public uint GetValueOfRegisterB(uint initValueRegisterA = 0)
        {
            var instructions = ParseInput(Input).ToArray();

            var simplePc = new SimpleComputer(initValueRegisterA);

            for (int i = 0; i < instructions.Length; i++)
            {
                var instr = instructions[i];
                switch (instr.Type)
                {
                    case InstructionType.Half:
                        simplePc.HalfRegister(instr.TargetRegister.Value);
                        break;
                    case InstructionType.Triple:
                        simplePc.TripleRegister(instr.TargetRegister.Value);
                        break;
                    case InstructionType.Increment:
                        simplePc.IncrementRegister(instr.TargetRegister.Value);
                        break;
                    case InstructionType.Jump:
                        i += instr.JumpOffset - 1;
                        continue;
                    case InstructionType.JumpEven:
                        if (simplePc.IsEven(instr.TargetRegister.Value))
                        {
                            i += instr.JumpOffset - 1;
                        }
                        continue;
                    case InstructionType.JumpOne:
                        if (simplePc.IsOne(instr.TargetRegister.Value))
                        {
                            i += instr.JumpOffset - 1;
                        }
                        continue;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            return simplePc.GetRegisterB;
        }

        private IEnumerable<Instructions> ParseInput(string[] input)
        {
            return input
                .Select(s => _inputRegex.Match(s))
                .Select(match =>
                    new Instructions(
                        match.Groups[1].Value,
                        match.Groups[3].Value,
                        !string.IsNullOrEmpty(match.Groups[2].Value)
                            ? match.Groups[2].Value
                            : match.Groups[4].Value));
        }

        private class SimpleComputer
        {
            public bool IsEven(char r) => _registers[r] % 2 == 0;
            public bool IsOne(char r) => _registers[r] == 1;
            public uint GetRegisterB => _registers['b'];

            private readonly IDictionary<char, uint> _registers;

            public SimpleComputer(uint initValueRegisterA = 0)
            {
                _registers = new Dictionary<char, uint>()
                {
                    {'a', initValueRegisterA},
                    {'b', 0}
                };
            }

            public void IncrementRegister(char r)
            {
                _registers[r]++;
            }

            public void HalfRegister(char r)
            {
                _registers[r] = _registers[r] / 2;
            }

            public void TripleRegister(char r)
            {
                _registers[r] = _registers[r] * 3;
            }
        }

        private class Instructions
        {
            public InstructionType Type { get; }
            public char? TargetRegister { get; }
            public int JumpOffset { get; }

            private static readonly IDictionary<string, InstructionType> MappingInstructions =
                new Dictionary<string, InstructionType>()
                {
                    {"hlf", InstructionType.Half},
                    {"tpl", InstructionType.Triple},
                    {"inc", InstructionType.Increment},
                    {"jmp", InstructionType.Jump},
                    {"jie", InstructionType.JumpEven},
                    {"jio", InstructionType.JumpOne},
                };

            public Instructions(string type, string register = "", string offset = "")
            {
                Type = MappingInstructions[type];
                if (!string.IsNullOrEmpty(register))
                {
                    TargetRegister = register.ToCharArray().First();
                }

                if (!string.IsNullOrEmpty(offset))
                {
                    JumpOffset = int.Parse(offset);
                }
            }

            public override string ToString()
            {
                return string.Format(
                    "{0}, {1}, {2}",
                    MappingInstructions.First(x => x.Value == Type).Key,
                    TargetRegister,
                    JumpOffset
                );
            }
        }

        private enum InstructionType
        {
            Half,
            Triple,
            Increment,
            Jump,
            JumpEven,
            JumpOne
        }
    }
}