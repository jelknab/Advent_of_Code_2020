using System.Linq;
using Advent_of_Code_2020.Day10;
using Advent_of_Code_2020.Day11;
using Xunit;

namespace Tests
{
    public class Day11Tests
    {
        [Fact]
        public void TestSeatChaos()
        {
            var input = Day11.ParseSeatGrid("Advent_of_Code_2020.Day11.SampleInput.txt");
            var grid = Day11.SimulateSeatsTillUnchanged(input);
            
            Assert.Equal(37, grid.Sum(row => row.Count(c => c == '#')));
        }
    }
}