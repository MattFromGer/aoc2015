using System.Text;
using ClassLib.util;

namespace ClassLib
{
    public class Day10 : AocDay
    {
        public int GetLengthOfStringAfter40Times()
        {
            var lookAndSayString = Input[0];
            for (int i = 0; i < 40; i++)
            {
                lookAndSayString = ProcessLookAndSay(lookAndSayString);
            }
        
            return lookAndSayString.Length;
        }
        
        public int GetLengthOfStringAfter50Times()
        {
            var lookAndSayString = Input[0];
            for (int i = 0; i < 50; i++)
            {
                lookAndSayString = ProcessLookAndSay(lookAndSayString);
            }
        
            return lookAndSayString.Length;
        }

        private string ProcessLookAndSay(string input)
        {
            // Using StringBuilder instead of string type is crucial for this process 
            var output = new StringBuilder();
            var noOfChar = 1;

            for (var i = 0; i < input.Length; i++)
            {
                var c = input[i];
                if (i < input.Length - 1 && c == input[i + 1])
                {
                    noOfChar++;
                }
                else
                {
                    output.Append(noOfChar.ToString() + c);
                    noOfChar = 1;
                }
            }

            return output.ToString();
        }
    }
}