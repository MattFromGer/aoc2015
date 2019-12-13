using System;
using System.IO;

namespace ClassLib.util
{
    public class InputHandler
    {
        public static string[] readFile(string fileName)
        {
            var allLines = new string[1000]; //only allocate memory here
            var fileLocation = "../../../../ClassLib/";
            using (StreamReader sr = File.OpenText(fileLocation + fileName))
            {
                int x = 0;
                while (!sr.EndOfStream)
                {
                    allLines[x] = sr.ReadLine();
                    x += 1;
                }
            }

            return allLines;
        }
    }
}