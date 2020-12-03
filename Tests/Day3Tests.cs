using System.Linq;
using Advent_of_Code_2020.Day3;
using Xunit;

namespace Tests
{
    public class Day3Tests
    {
        [Fact]
        private void TestTreeCollisions()
        {
            var treeGrid = Day3.ParseTreeGrid("Advent_of_Code_2020.Day3.SampleInput.txt");
            
            Assert.Equal("..##.......", treeGrid[0]);
            Assert.Equal(".#..#...#.#", treeGrid.Last());
            
            Assert.Equal(7, Day3.CountTreesOnSlope(treeGrid, 3, 1));
            Assert.Equal(new[] {2, 7, 3, 4, 2}, Day3.GetTreesPerSlope(treeGrid));
            Assert.Equal(336, Day3.GetProduct(Day3.GetTreesPerSlope(treeGrid)));
        }
    }
}