using System.Collections.Generic;
using System.Linq;
using ClassLib.util;

namespace ClassLib
{
    public class Day08 : AocDay
    {
        private static readonly Dictionary<string, int> Literals = new Dictionary<string, int>()
        {
            {"\\\"", 2},
            {"\\\\", 2},
            {"\\x", 4}
        };

        private static string[] TestData = new string[]
        {
            "\"\"",
            "\"abc\"",
            "\"aaa\\\"aaa\"",
            "\"\\x27\""
        };

        public int GetNumberOfCharactersOfCode()
        {
            return Input.Sum(CountWithoutEscapeCharacters);
        }

        public int GetNumberOfCharactersDoubleEscaped()
        {
            int totalNumberOfChars = 0;
            foreach (var lineStr in Input)
            {
                var escapedStr = BuildNewDoubleEscapedString(lineStr);
                totalNumberOfChars += CountWithoutEscapeCharacters(escapedStr);
            }

            return totalNumberOfChars;
        }

        private string BuildNewDoubleEscapedString(string lineStr)
        {
            var newLineStr = "";
            foreach (var c in lineStr)
            {
                newLineStr += c switch
                {
                    '\\' => "\\\\",
                    '\"' => "\\\"",
                    _ => c
                };
            }

            return newLineStr;
        }

        private int CountWithoutEscapeCharacters(string input)
        {
            int totalNumberOfChars = 0;
            for (var i = 0; i < input.Length; i++)
            {
                var c = input[i];
                if (c == '\\')
                {
                    var str = string.Concat(input[i], input[i + 1]);
                    var noOfChar = Literals[str];
                    i += noOfChar - 1;
                }

                totalNumberOfChars++;
            }

            return input.Length - totalNumberOfChars + 2; // quotation marks
        }
    }
}