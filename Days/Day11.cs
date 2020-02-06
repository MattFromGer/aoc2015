using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClassLib.util;

namespace ClassLib
{
    public class Day11 : AocDay
    {
        private readonly IList<char> _forbiddenChars = new List<char>()
        {
            'i', 'o', 'l'
        };

        public string FindNewPassword()
        {
            var currentPw = Input[0];
            while (!IsMeetingConstrains(currentPw))
            {
                currentPw = GetNextPassword(currentPw);
            }

            return currentPw;
        }

        private string GetNextPassword(string previousPw)
        {
            var newPw = new StringBuilder(previousPw);

            for (int i = newPw.Length - 1; i >= 0; i--)
            {
                var c = newPw[i];
                if (c == 'z')
                {
                    newPw[i] = 'a';
                    continue;
                }

                newPw[i]++;
                if (_forbiddenChars.Contains(c))
                {
                    newPw[i]++;
                }

                break;
            }

            return newPw.ToString();
        }

        private bool IsMeetingConstrains(string pw)
        {
            if (pw.Any(x => _forbiddenChars.Contains(x)))
            {
                return false;
            }

            if (!ContainAtLeastTwoPairs(pw))
            {
                return false;
            }

            if (!Contains3CharsInStreet(pw))
            {
                return false;
            }

            return true;
        }

        private bool ContainAtLeastTwoPairs(string pw)
        {
            var pairCount = 0;
            for (var i = 0; i < pw.Length - 1; i++)
            {
                var c = pw[i];
                var c1 = pw[i + 1];

                if (c == c1)
                {
                    pairCount++;
                    if (pairCount > 1)
                    {
                        return true;
                    }

                    i++;
                }
            }

            return false;
        }

        private bool Contains3CharsInStreet(string pw)
        {
            for (int i = 0; i < pw.Length - 2; i++)
            {
                var c = pw[i];
                var c1 = pw[i + 1];
                var c2 = pw[i + 2];

                if (c == c1 && c == c1 + 1 && c2 == c2 + 2)
                {
                    return true;
                }
            }

            return false;
        }
    }
}