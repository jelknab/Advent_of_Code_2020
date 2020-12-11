using System.Linq;
using Advent_of_Code_2020.Day10;
using Advent_of_Code_2020.Day11;
using Xunit;

namespace Tests
{
    public class Day11Tests
    {
        [Fact]
        public void TestSeatChaosAdjacent()
        {
            var input = Day11.ParseSeatGrid("Advent_of_Code_2020.Day11.SampleInput.txt");
            var grid = Day11.SimulateSeatsAdjacent(input);
            
            Assert.Equal(37, grid.Sum(row => row.Count(c => c == '#')));
        }
        
        [Fact]
        public void TestSeatChaosVisible()
        {
            var input = Day11.ParseSeatGrid("Advent_of_Code_2020.Day11.SampleInput.txt");
            var grid = Day11.SimulateSeatsVisible(input);
            
            Assert.Equal(26, grid.Sum(row => row.Count(c => c == '#')));
        }
    }
}