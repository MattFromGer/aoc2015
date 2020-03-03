using System;
using System.Text.RegularExpressions;
using ClassLib.util;

namespace ClassLib
{
    public class Day25 : AocDay
    {
        private const int Multiplier = 252533;
        private const int Divisor = 33554393;
        private const long FirstCode = 20151125;
        
        public long GetCodeForWeatherMachine()
        {
            var row = Convert.ToInt32(Regex.Match(Input[0], @"row (\d+)").Groups[1].Value);
            var column = Convert.ToInt32(Regex.Match(Input[0], @"column (\d+)").Groups[1].Value);

            var diagonal = row + column - 1;
            var noOfCodes = (diagonal * diagonal - diagonal) / 2 + column;

            var code = FirstCode;
            for (int i = 1; i < noOfCodes; i++)
            {
                code = CalculateCode(code);
            }
            
            return code;
        }

        private static long CalculateCode(long previousCode)
        {
            return previousCode * Multiplier % Divisor;
        }
    }
}