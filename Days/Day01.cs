using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using ClassLib.util;

namespace ClassLib
{
    public class Day01 : AocDay
    {
        public int GetFloorLevel()
        {
            return GenerateSequence(Input[0]).Sum();
        }

        public int GetFirstPositionInBasement()
        {
            var instr = GenerateSequence(Input[0]).ToList();

            var sum = 0;
            for (int i = 0; i < instr.Count(); i++)
            {
                sum += instr[i];
                if (sum == -1)
                {
                    return i + 1;
                }
            }
            
            throw new AggregateException();
        }

        private IEnumerable<int> GenerateSequence(string str)
        {
            return str.Select(c => c switch
            {
                '(' => 1,
                ')' => -1,
                _ => throw new ArgumentOutOfRangeException()
            });
        }
    }
}