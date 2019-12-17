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
            Assert.Equal(1588178, area);
            
            var ribbonLength = day02.getRibbonLength();
            Assert.Equal(3783758, ribbonLength);
        }

        [Fact]
        public void Day03()
        {
            var day03 = new Day03();
            var numberOfHouses = day03.getNumberOfHouses();
            Assert.Equal(2081, numberOfHouses);

            var numberOfHousesWithRobo = day03.getNumberOfHousesWithRoboSanta();
            Assert.Equal(2341, numberOfHousesWithRobo);
        }
        
        [Fact]
        public void Day04()
        {
            var day04 = new Day04();
        }
    }
}