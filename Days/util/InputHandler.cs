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
            const string fileLocation = "../../../../Days/input/";
            using (StreamReader sr = File.OpenText(fileLocation + fileName))
            {
                while (!sr.EndOfStream)
                {
                    allLines.Add(sr.ReadLine());
                }
            }

            return allLines.ToArray();
        }
    }
}