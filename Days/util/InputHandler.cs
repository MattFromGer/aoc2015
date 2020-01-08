using System;
using System.Collections.Generic;
using System.IO;

namespace ClassLib.util
{
    public static class InputHandler
    {
        public static string[] ReadFile(string fileName)
        {
            var allLines = new List<string>();
            var fileLocation = "../../../../Days/input/";
            using (StreamReader sr = File.OpenText(fileLocation + fileName))
            {
                int x = 0;
                while (!sr.EndOfStream)
                {
                    allLines.Add(sr.ReadLine());
                    x += 1;
                }
            }

            return allLines.ToArray();
        }
    }
}