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
            var area = day02.GetAreaOfWrappingPaper();
            Assert.Equal(1588178, area);
            
            var ribbonLength = day02.GetRibbonLength();
            Assert.Equal(3783758, ribbonLength);
        }

        [Fact]
        public void Day03()
        {
            var day03 = new Day03();
            var numberOfHouses = day03.GetNumberOfHouses();
            Assert.Equal(2081, numberOfHouses);

            var numberOfHousesWithRobo = day03.GetNumberOfHousesWithRoboSanta();
            Assert.Equal(2341, numberOfHousesWithRobo);
        }
        
        [Fact]
        public void Day04()
        {
            var day04 = new Day04();
            var numberFiveZeros = day04.GetLowestPositiveNumber("00000");
            Assert.Equal(117946, numberFiveZeros);

            var numberSixZeros = day04.GetLowestPositiveNumber("000000");
            Assert.Equal(3938038, numberSixZeros);
        }

        [Fact]
        public void Day05()
        {
            var day05 = new Day05();
            var numberOfNiceStrings = day05.GetNumberOfNiceStrings();
            Assert.Equal(238, numberOfNiceStrings);
            
            var numberOfNiceStringsPart2 = day05.GetNumberOfNiceStringsPart2();
            Assert.Equal(69, numberOfNiceStringsPart2);
        }
    }
}