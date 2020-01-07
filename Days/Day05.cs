using System.Collections.Generic;
using System.Linq;
using ClassLib.util;

namespace ClassLib
{
    public class Day05 : AocDay
    {
        private static readonly IList<char> VOWELS = new List<char>()
        {
            'a', 'e', 'i', 'o', 'u'
        };

        private static readonly IList<string> DISALLOWED_STRINGS = new List<string>()
        {
            "ab", "cd", "pq", "xy"
        };

        #region Part1

        public int GetNumberOfNiceStrings()
        {
            var niceStrings = Input
                .Where(ContainsAtLeastThreeVowels)
                .Where(DoesNotContainDisallowedStrings)
                .Where(HasCharTwiceInRow);

            return niceStrings.Count();
        }

        private static bool ContainsAtLeastThreeVowels(string stringToCheck)
        {
            return stringToCheck.Count(c => VOWELS.Contains(c)) >= 3;
        }

        private static bool DoesNotContainDisallowedStrings(string stringToCheck)
        {
            return !DISALLOWED_STRINGS.Any(x => stringToCheck.Contains(x));
        }

        private static bool HasCharTwiceInRow(string stringToCheck)
        {
            var lastChar = ' ';
            foreach (var c in stringToCheck)
            {
                if (c == lastChar)
                {
                    return true;
                }

                lastChar = c;
            }

            return false;
        }

        private static bool ContainsAtLeastTwoPairs(string stringToCheck)
        {
            return GetPairs(stringToCheck).Any();
        }

        private static IList<string> GetPairs(string stringToCheck)
        {
            var pairs = new List<string>();
            for (var i = 0; i < stringToCheck.Length - 1; i++)
            {
                var c = stringToCheck[i];
                var c1 = stringToCheck[i + 1];
                if (c == c1)
                {
                    pairs.Add((c.ToString() + c));
                }
            }

            return pairs;
        }

        #endregion

        #region Part2

        public int GetNumberOfNiceStringsPart2()
        {
            //var test = new string[] {"aaaa"};
            var niceStrings = Input
                .Where(HasAtLeastTwoSamePairs)
                .Where(HasSpecificRepeatingPattern);

            return niceStrings.Count();
        }

        private static bool HasAtLeastTwoSamePairs(string stringToCheck)
        {
            return Enumerable.Range(0, stringToCheck.Length - 1).Any(i => stringToCheck.IndexOf(stringToCheck.Substring(i, 2), i+2) >= 0); 
        }

        private static bool HasSpecificRepeatingPattern(string stringToCheck)
        {
            for (var i = 0; i < stringToCheck.Length - 2; i++)
            {
                var c = stringToCheck[i];
                var c2 = stringToCheck[i + 2];
                if (c == c2)
                {
                    return true;
                }
            }

            return false;
        }

        #endregion
    }
}