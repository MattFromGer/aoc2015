using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using ClassLib.util;

namespace ClassLib
{
    public class Day16 : AocDay
    {
        private static readonly Regex InputRegex = new Regex(@"Sue (\d*): (\w*): (\d*), (\w*): (\d*), (\w*): (\d*)");
        private static readonly IDictionary<string, int> Clues = new Dictionary<string, int>()
        {
            {"children", 3},
            {"cats", 7},
            {"samoyeds", 2},
            {"pomeranians", 3},
            {"akitas", 0},
            {"vizslas", 0},
            {"goldfish", 5},
            {"trees", 3},
            {"cars", 2},
            {"perfumes", 1}
        };

        public int GetNumberOfAunt()
        {
            var aunts = ParseInput(Input);
            var aunt = aunts.First(x =>
                x.Clues.All(c => Clues.Contains(c)));

            return aunt.Number;
        }

        public int GetNumberOfRealAunt()
        {
            var aunts = ParseInput(Input);
            var aunt = aunts.First(a =>
                a.Clues.All(clue => clue.Key switch
                {
                    "cats" => clue.Value > Clues["cats"],
                    "trees" => clue.Value > Clues["trees"],
                    "pomeranians" => clue.Value < Clues["pomeranians"],
                    "goldfish" => clue.Value < Clues["goldfish"],
                    _ => clue.Value == Clues[clue.Key ?? throw new ArgumentOutOfRangeException()]
                })
            );

            return aunt.Number;
        }

        private IEnumerable<Aunt> ParseInput(string[] input)
        {
            return input
                .Select(instr => InputRegex.Match(instr))
                .Select(match =>
                    new Aunt(Convert.ToInt32(match.Groups[1].Value),
                        new Dictionary<string, int>()
                        {
                            {Convert.ToString(match.Groups[2].Value), Convert.ToInt32(match.Groups[3].Value)},
                            {Convert.ToString(match.Groups[4].Value), Convert.ToInt32(match.Groups[5].Value)},
                            {Convert.ToString(match.Groups[6].Value), Convert.ToInt32(match.Groups[7].Value)}
                        })
                );
        }

        private class Aunt
        {
            public int Number { get; }
            public IDictionary<string, int> Clues { get; }

            public Aunt(int number, IDictionary<string, int> clues)
            {
                Number = number;
                Clues = clues;
            }
        }
    }
}