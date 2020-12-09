using System.Linq;
using Advent_of_Code_2020.Day9;
using Xunit;

namespace Tests
{
    public class Day9Tests
    {
        [Fact]
        public void TestWrongNumberFinder()
        {
            var input = Day9.XmasParser("Advent_of_Code_2020.Day9.SampleInput.txt");
            
            Assert.Equal(127, Day9.FindWrongNumber(input, 5));
        }

        [Fact]
        public void TestFindSetOfNumbersThatSum()
        {
            var input = Day9.XmasParser("Advent_of_Code_2020.Day9.SampleInput.txt");
            var answer = Day9.FindSetOfNumbersThatSumInput(input, 127);
            
            Assert.Equal(15, answer.Min());
            Assert.Equal(47, answer.Max());
        }
    }
}