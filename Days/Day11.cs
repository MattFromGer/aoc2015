using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClassLib.util;

namespace ClassLib
{
    public class Day11 : AocDay
    {
        private StringBuilder _pw;
        private string _previousPw;
        private readonly string _lastString = "zzzzzzzz";

        private readonly IList<char> _forbiddenChars = new List<char>()
        {
            'i', 'o', 'l'
        };
        
        public string FindNewPassword(string previousPw = null)
        {
            if (previousPw == null)
            {
                previousPw = Input[0];
            }
            
            _previousPw = previousPw;
            _pw = new StringBuilder(previousPw); 
            while (!IsMeetingConstrains())
            {
                SetNextPassword();
            }

            return _pw.ToString();
        }

        private void SetNextPassword()
        {
            for (int i = _pw.Length - 1; i >= 0; i--)
            {
                var c = _pw[i];
                if (c == 'z')
                {
                    _pw[i] = 'a';
                    continue;
                }
        
                _pw[i]++;
                break;
            }
        }

        private bool IsMeetingConstrains()
        {
            if(_pw.ToString() == "zzzzzzzz")
            {
                throw new OverflowException("No new password found");
            }

            if (_pw.ToString() == _previousPw)
            {
                return false;
            }

            for (int i = 0; i < _pw.Length; i++)
            {
                if (_forbiddenChars.Contains(_pw[i])) return false;
            }
            
            if (!ContainAtLeastTwoPairs())
            {
                return false;
            }

            if (!Contains3CharsInStreet())
            {
                return false;
            }

            return true;
        }

        private bool ContainAtLeastTwoPairs()
        {
            var pairCount = 0;
            for (var i = 0; i < _pw.Length - 1; i++)
            {
                var c = _pw[i];
                var c1 = _pw[i + 1];

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

        private bool Contains3CharsInStreet()
        {
            for (int i = 0; i < _pw.Length - 2; i++)
            {
                var c = _pw[i];
                var c1 = _pw[i + 1];
                var c2 = _pw[i + 2];

                if (c1 == c + 1 && c2 == c + 2)
                {
                    return true;
                }
            }

            return false;
        }
    }
}