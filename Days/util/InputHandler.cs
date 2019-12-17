using System;
using System.Collections.Generic;
using System.IO;

namespace ClassLib.util
{
    public static class InputHandler
    {
        public static string[] ReadFile(string fileName)
        {
            var allLines = new string[1000]; //only allocate memory here
            var fileLocation = "../../../../Days/input/";
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