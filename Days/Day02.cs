using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ClassLib.util;

namespace ClassLib
{
    public class Day02 : AocDay
    {
        public int getAreaOfWrappingPaper()
        {
            var areaWrappingPaper = 0;

            Parallel.ForEach(Input, line =>
            {
                var dimensions = line.Split("x").Select(int.Parse).ToArray();
                Array.Sort(dimensions);

                var area = new int[3];
                area[0] = 2 * dimensions[0] * dimensions[1];
                area[1] = 2 * dimensions[1] * dimensions[2];
                area[2] = 2 * dimensions[0] * dimensions[2];

                var areaTotal = area.Sum() + area.Min() / 2;

                Interlocked.Add(ref areaWrappingPaper, areaTotal);
            });

            return areaWrappingPaper;
        }
        
        public int getRibbonLength()
        {
            var areaWrappingPaper = 0;

            Parallel.ForEach(Input, line =>
            {
                var dimensions = line.Split("x").Select(int.Parse).ToArray();
                Array.Sort(dimensions);

                var ribbonPart1 = 2 * dimensions[0] + 2 * dimensions[1];
                var ribbonPart2 = dimensions[0] * dimensions[1] * dimensions[2];

                Interlocked.Add(ref areaWrappingPaper, ribbonPart1 + ribbonPart2);
            });

            return areaWrappingPaper;
        }
    }
}