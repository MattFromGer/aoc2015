using System;
using System.Collections.Generic;
using System.Linq;
using ClassLib.util;

namespace ClassLib
{
    public class Day17 : AocDay
    {
        public int GetNumberOfCombinations(int liters)
        {
            var buckets = ParseInput(Input);

            var combinations = GenerateCombinations(buckets)
                .Where(x => x.Sum() == liters);

            return combinations.Count();
        }

        public int GetMinNumberOfContainers(int liters)
        {
            var buckets = ParseInput(Input);

            var allCombinations = GenerateCombinations(buckets)
                .Where(x => x.Sum() == liters);
            var minCount = allCombinations.Min(x => x.Count);
            var minCombinations = allCombinations.Where(x => x.Count == minCount);

            return minCombinations.Count();
        }

        private IEnumerable<List<int>> GenerateCombinations(IList<int> buckets)
        {
            return Enumerable
                .Range(1, (1 << buckets.Count) - 1)
                .Select(index => buckets
                    .Where((item, idx) => ((1 << idx) & index) != 0)
                    .ToList());
        }

        private IList<int> ParseInput(string[] input)
        {
            return input.Select(s => Convert.ToInt32(s)).ToList();
        }
    }
}