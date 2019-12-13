using System.Diagnostics;
using Xunit;
using ClassLib;

namespace Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Day01()
        {
            var day01 = new Day01();
        }

        [Fact]
        public void Day02()
        {
            var day02 = new Day02();
            var area = day02.getAreaOfWrappingPaper();
            Trace.WriteLine("Day02: Part 1: "  + area);
            var ribbonLength = day02.getRibbonLength();
            Trace.WriteLine("Day02: Part 2: " + ribbonLength);
        }

        [Fact]
        public void Day03()
        {
            var day3 = new Day03();
        }
    }
}