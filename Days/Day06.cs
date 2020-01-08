using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using ClassLib.util;

namespace ClassLib
{
    public class Day06 : AocDay
    {
        private static readonly Regex REGEX = new Regex(@"([a-z ]*)\s(\d*,\d*)\sthrough\s(\d*,\d*)");

        private enum Operation
        {
            TurnOn,
            TurnOff,
            Toggle
        }

        private static readonly Dictionary<string, Operation> OPERATION_MAPPING =
            new Dictionary<string, Operation>()
            {
                {"turn on", Operation.TurnOn},
                {"turn off", Operation.TurnOff},
                {"toggle", Operation.Toggle}
            };

        private class GridOfLights
        {
            private int[,] _grid = new int[1000, 1000];
            
            public void ExecuteBoolean(Operation op, Point firstPoint, Point secondPoint)
            {
                for (int i = firstPoint.X; i <= secondPoint.X; i++)
                {
                    for (int j = firstPoint.Y; j <= secondPoint.Y; j++)
                    {
                        _grid[i, j] = op switch
                        {
                            Operation.TurnOn => 1,
                            Operation.TurnOff => 0,
                            Operation.Toggle => _grid[i, j] == 0 ? 1 : 0
                        };
                    }
                }
            }

            public void ExecuteIncremental(Operation op, Point firstPoint, Point secondPoint)
            {
                for (int i = firstPoint.X; i <= secondPoint.X; i++)
                {
                    for (int j = firstPoint.Y; j <= secondPoint.Y; j++)
                    {
                        _grid[i, j] = op switch
                        {
                            Operation.TurnOn => _grid[i, j] + 1,
                            Operation.TurnOff => _grid[i, j] > 0 ? _grid[i, j] - 1 : 0,
                            Operation.Toggle => _grid[i, j] + 2
                        };
                    }
                }
            }

            public int GetNumberOfActivePoints()
            {
                return _grid.Cast<int>().Count(p => p == 1);
            }

            public int GetTotalBrightness()
            {
                return _grid.Cast<int>().Sum();
            }
        }

        public int GetNumberOfLitLights()
        {
            var grid = new GridOfLights();

            foreach (string instr in Input)
            {
                var match = REGEX.Match(instr);
                var op = GetOperation(match.Groups[1].Value);
                var firstPoint = match.Groups[2].Value.ToPoint();
                var secondPoint = match.Groups[3].Value.ToPoint();

                grid.ExecuteBoolean(op, firstPoint, secondPoint);
            }

            return grid.GetNumberOfActivePoints();
        }

        public int GetTotalBrightness()
        {
            var grid = new GridOfLights();

            foreach (string instr in Input)
            {
                var match = REGEX.Match(instr);
                var op = GetOperation(match.Groups[1].Value);
                var firstPoint = match.Groups[2].Value.ToPoint();
                var secondPoint = match.Groups[3].Value.ToPoint();

                grid.ExecuteIncremental(op, firstPoint, secondPoint);
            }

            return grid.GetTotalBrightness();
        }

        private Operation GetOperation(string operationStr)
        {
            if (OPERATION_MAPPING.TryGetValue(operationStr, out var operation))
            {
                return operation;
            }

            throw new NotSupportedException($"Operation {operationStr} is not supported.");
        }
    }

    public static class StringExtensions
    {
        public static Point ToPoint(this string commaSeparatedString)
        {
            var split = commaSeparatedString.Split(",");
            var x = int.Parse(split[0]);
            var y = int.Parse(split[1]);

            return new Point(x, y);
        }
    }
}