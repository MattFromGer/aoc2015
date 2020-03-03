using System;
using System.Collections.Generic;
using System.Linq;
using ClassLib.util;

namespace ClassLib
{
    public class Day24 : AocDay
    {
        public double GetQuantumEntanglementWithThreeGroups()
        {
            var packages = ParseInput(Input).ToList();

            var qe = GetQuantumEntanglement(packages.ToArray(), 3);

            return qe;
        }

        public double GetQuantumEntanglementWithFourGroups()
        {
            var packages = ParseInput(Input).ToList();

            var qe = GetQuantumEntanglement(packages.ToArray(), 4);

            return qe;
        }

        private double GetQuantumEntanglement(int[] packages, int noOfGroups)
        {
            var binSize = packages.Sum() / noOfGroups;

            var permutations = GetSubsetsThatSumTo(binSize, packages.Select(x => (double) x).ToArray());

            var permLowestCount = permutations
                .Where(x => x.Count() == permutations.Min(x => x.Count()));

            var minQe = permLowestCount.Min(x => x.Aggregate((x, y) => x * y));

            return minQe;
        }

        private static IList<double[]> GetSubsetsThatSumTo(double binSize, double[] numbers)
        {
            var result = new List<double[]>();
            bool[] wheel = new bool[numbers.Length];
            double? sum = 0;

            do
            {
                sum = IncrementWheel(0, sum, numbers, wheel);
                //Use subtraction comparison due to double type imprecision
                if (sum.HasValue && Math.Abs(sum.Value - binSize) < 0.000001F)
                {
                    result.Add(numbers.Where((n, idx) => wheel[idx]).ToArray());
                }
            } while (sum != null);

            return result;
        }

        private static double? IncrementWheel(int pos, double? sum, double[] numbers, bool[] wheel)
        {
            if (pos == numbers.Length || !sum.HasValue)
            {
                return null;
            }

            wheel[pos] = !wheel[pos];
            if (!wheel[pos])
            {
                sum -= numbers[pos];
                sum = IncrementWheel(pos + 1, sum, numbers, wheel);
            }
            else
            {
                sum += numbers[pos];
            }

            return sum;
        }

        private IEnumerable<int> ParseInput(string[] input)
        {
            return input.Select(s => Convert.ToInt32(s));
        }
    }
}