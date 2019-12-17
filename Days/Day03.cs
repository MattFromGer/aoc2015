using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using ClassLib.util;

namespace ClassLib
{
    public class Day03
    {
        private class Santa
        {
            private int _x = 0;
            private int _y = 0;

            public Point CurrentPosition()
            {
                return new Point(_x, _y);
            }

            private Point MoveUp()
            {
                _y++;
                return CurrentPosition();
            }

            private Point MoveRight()
            {
                _x++;
                return CurrentPosition();
            }

            private Point MoveDown()
            {
                _y--;
                return CurrentPosition();
            }

            private Point MoveLeft()
            {
                _x--;
                return CurrentPosition();
            }

            public Point Move(char d) => d switch
            {
                '^' => MoveUp(),
                '>' => MoveRight(),
                'v' => MoveDown(),
                '<' => MoveLeft(),
            };
        }

        public int getNumberOfHouses()
        {
            string[] input = InputHandler.readFile("Day03.txt");
            var path = input[0];
            var housesVisited = new List<Point>();
            var santa = new Santa();

            // Init
            housesVisited.Add(santa.CurrentPosition());

            foreach (char direction in path)
            {
                var currentPosition = santa.Move(direction);
                housesVisited.Add(currentPosition);
            }

            return housesVisited.Distinct().Count();
        }

        public int getNumberOfHousesWithRoboSanta()
        {
            string[] input = InputHandler.readFile("Day03.txt");
            var path = input[0];
            var housesVisited = new List<Point>();
            var santa = new Santa();
            var santaRobo = new Santa();

            // Init
            housesVisited.Add(santa.CurrentPosition());

            for (int i = 0; i < path.Length; i++)
            {
                if (IsOdd(i))
                {
                    var currentPosition = santaRobo.Move(path[i]);
                    housesVisited.Add(currentPosition);
                }
                else
                {
                    var currentPosition = santa.Move(path[i]);
                    housesVisited.Add(currentPosition);
                }
            }

            return housesVisited.Distinct().Count();
        }

        private static bool IsOdd(int value)
        {
            return value % 2 != 0;
        }
    }
}