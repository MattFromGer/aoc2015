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
            var lvl = day01.GetFloorLevel();
            Assert.Equal(138, lvl);

            var basementPos = day01.GetFirstPositionInBasement();
            Assert.Equal(1771, basementPos);
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
        
        [Fact]
        public void Day06()
        {
            var day06 = new Day06();
            var numberOfLitLights = day06.GetNumberOfLitLights();
            Assert.Equal(400410, numberOfLitLights);

            var totalBrightness = day06.GetTotalBrightness();
            Assert.Equal(15343601, totalBrightness);
        }
        
        [Fact]
        public void Day07()
        {
            var day07 = new Day07();
            var signalOfWireA = day07.GetSignalOfWireA();
            Assert.Equal(3176, signalOfWireA);

            var signalWithNewValueOfWireA = day07.GetSignalWithNewValueOfWireA(signalOfWireA);
            Assert.Equal(14710, signalWithNewValueOfWireA);
        }

        [Fact]
        public void Day08()
        {
            var day08 = new Day08();
            var totalNumberOfEscapedCharacters = day08.GetNumberOfCharactersOfCode();
            Assert.Equal(1371, totalNumberOfEscapedCharacters);
            
            var totalNumberOfDoubleEscapedCharacters = day08.GetNumberOfCharactersDoubleEscaped();
            Assert.Equal(2117, totalNumberOfDoubleEscapedCharacters);
        }
        
        [Fact]
        public void Day09()
        {
            var day09 = new Day09();
            var distanceShortest = day09.GetShortestPath();
            Assert.Equal(251, distanceShortest);

            var distanceLongest = day09.GetLongestPath();
            Assert.Equal(898, distanceLongest);
        }
        
        [Fact]
        public void Day10()
        {
            var day10 = new Day10();
            var length = day10.GetLengthOfStringAfter40Times();
             Assert.Equal(252594, length);
            
            var lengthPart2 = day10.GetLengthOfStringAfter50Times();
            Assert.Equal(3579328, lengthPart2);

        }
        
        [Fact]
        public void Day11()
        {
            var day11 = new Day11();
            var newPw = day11.FindNewPassword();
            Assert.Equal("cqjxxyzz", newPw);

            var newPw2 = day11.FindNewPassword(newPw);
            Assert.Equal("cqkaabcc", newPw2);
        }

        [Fact]
        public void Day12()
        {
            var day12 = new Day12();
            var sum = day12.GetTotalSumOfNumbers();
            Assert.Equal(119433, sum);

            var sumWithoutRed = day12.GetTotalSumOfNumbersWithoutRed();
            Assert.Equal(68466, sumWithoutRed);
        }

        [Fact]
        public void Day13()
        {
            var day13 = new Day13();
            var maxHappiness = day13.GetMaxHappiness();
            Assert.Equal(664, maxHappiness);

            var maxHappinessWithNewPerson = day13.GetMaxHappinessWithAddedPerson();
            Assert.Equal(640, maxHappinessWithNewPerson);
        }

        [Fact]
        public void Day14()
        {
            var day14 = new Day14();
            var distanceOfWinningReindeer = day14.GetDistanceOfWinnerAfter(2503);
            Assert.Equal(2655, distanceOfWinningReindeer);

            var pointsOfWinningReindeer = day14.GetPointsOfWinner(2503);
            Assert.Equal(1059, pointsOfWinningReindeer);
        }
    }
}