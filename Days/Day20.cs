using System;
using ClassLib.util;

namespace ClassLib
{
    public class Day20 : AocDay
    {
        public long GetLowestHouse()
        {
            var minNumOfPresents = Convert.ToInt32(Input[0]);
            //return GetLowestHouseAlg1(minNumOfPresents);
            return GetLowestHouseAlg2(minNumOfPresents);
        }

        public long GetLowestHouseMax50()
        {
            var minNumOfPresents = Convert.ToInt32(Input[0]);
            
            var sigmaCache = GenerateSigmaCache(minNumOfPresents, 11, 50);

            return GetNoOfIterations(minNumOfPresents, sigmaCache);
        }

        private int GetLowestHouseAlg2(int targetAmount)
        {
            for (int i = 1; i < targetAmount / 10; i++)
            {
                var sum = 0;

                for (int j = 1; j < Math.Sqrt(i); j++)
                {
                    if (i % j == 0)
                    {
                        sum += (j + i/j) * 10;
                    }
                }

                if (sum >= targetAmount)
                {
                    return i;
                }
            }

            throw new Exception("No number found.");
        }
        
        private int GetLowestHouseAlg1(int targetAmount)
        {
            var sigmaCache = GenerateSigmaCache(targetAmount, 10);

            return GetNoOfIterations(targetAmount, sigmaCache);
        }

        private static int GetNoOfIterations(int targetAmount, int[] sigmaCache)
        {
            int value = 0;
            var iterations = 1;
            while (value < targetAmount)
            {
                value = sigmaCache[iterations];
                iterations++;
            }

            return iterations - 1;
        }

        //Use sieve-like method to compute sum of divisors
        private static int[] GenerateSigmaCache(int targetAmount, int noOfPresents, int? maxHousesPerElf = null)
        {
            int top = targetAmount / noOfPresents;
            int[] sigmaCache = new int[top + 1];
            for (int i = 1; i <= top; i++)
            {
                var topForElf = (maxHousesPerElf - 1) * i ?? top;
                topForElf = topForElf > top ? top : topForElf;
                for (int j = i; j <= topForElf; j += i)
                {
                    sigmaCache[j] += i * noOfPresents;
                }
            }

            return sigmaCache;
        }
    }
}