using System;
using System.Linq;
using ClassLib.util;

namespace ClassLib
{
    public class Day18 : AocDay
    {
        public int GetNumberOfLightsSwitchedOn(int numOfSteps)
        {
            var grid = new GridOfLights(100, Input);
            grid.Animate(numOfSteps);

            return grid.GetNumberOfLights();
        }

        public int GetNumberOfLightsSwitchedOnConway(int numOfSteps)
        {
            var grid = new GridOfLights(100, Input, true);
            grid.Animate(numOfSteps);

            return grid.GetNumberOfLights();
        }

        private class GridOfLights
        {
            private readonly int _size;
            private readonly bool _isConway;
            private readonly int[,] _nextGrid;
            private int[,] _grid;

            public GridOfLights(int size, string[] input, bool isConway = false)
            {
                _size = size;
                _isConway = isConway;
                _grid = new int[size, size];
                _nextGrid = new int[size, size];

                ConstructGrid(input);

                if (isConway)
                {
                    ConstructConway();
                }
            }

            public int GetNumberOfLights()
            {
                return _grid.Cast<int>().Sum();
            }

            private void ConstructGrid(string[] input)
            {
                for (var i = 0; i < _size; i++)
                {
                    for (var j = 0; j < _size; j++)
                    {
                        _grid[i, j] = input[i][j] switch
                        {
                            '#' => 1,
                            '.' => 0,
                            _ => throw new ArgumentOutOfRangeException()
                        };
                    }
                }
            }

            public void Animate(int numberOfSteps)
            {
                for (var n = 0; n < numberOfSteps; n++)
                {
                    for (int i = 0; i < _size; i++)
                    {
                        for (int j = 0; j < _size; j++)
                        {
                            var neighborsSum = GetNeighborSum(i, j);

                            if (_isConway && IsCorner(i, j))
                            {
                                _nextGrid[i, j] = 1;
                                continue;
                            }

                            _nextGrid[i, j] = ToggleLights(i, j, neighborsSum);
                        }
                    }

                    _grid = _nextGrid.Clone() as int[,];
                }
            }

            private int ToggleLights(int i, int j, int neighborsSum)
            {
                return _grid[i, j] switch
                {
                    1 => neighborsSum switch
                    {
                        2 => 1,
                        3 => 1,
                        _ => 0
                    },
                    0 => neighborsSum switch
                    {
                        3 => 1,
                        _ => 0
                    },
                    _ => throw new ArgumentOutOfRangeException()
                };
            }

            private void ConstructConway()
            {
                _grid[0, _size - 1] = 1;
                _grid[0, _size - 1] = 1;
                _grid[_size - 1, 0] = 1;
                _grid[_size - 1, 0] = 1;
            }

            private bool IsCorner(int i, int j)
            {
                return (i == 0 && j == 0) ||
                       (i == _size - 1 && j == 0) ||
                       (i == 0 && j == _size - 1) ||
                       (i == _size - 1 && j == _size - 1);
            }

            private int GetNeighborSum(int i, int j)
            {
                var neighbors = new int[8];
                neighbors[0] = GetNeighbor(i - 1, j - 1);
                neighbors[1] = GetNeighbor(i - 1, j);
                neighbors[2] = GetNeighbor(i - 1, j + 1);
                neighbors[3] = GetNeighbor(i, j + 1);
                neighbors[4] = GetNeighbor(i + 1, j + 1);
                neighbors[5] = GetNeighbor(i + 1, j);
                neighbors[6] = GetNeighbor(i + 1, j - 1);
                neighbors[7] = GetNeighbor(i, j - 1);

                return neighbors.Sum();
            }

            private int GetNeighbor(int x, int y)
            {
                return x >= 0 &&
                       y >= 0 &&
                       x < _size &&
                       y < _size
                    ? _grid[x, y]
                    : 0;
            }
        }
    }
}