using Advent_of_Code_2020.Day17;
using Xunit;

namespace Tests
{
    public class Day17Tests
    {
        [Fact]
        public void TestParser()
        {
            var cubeGrid = Day17.ParseCubeGrid("Advent_of_Code_2020.Day17.SampleInput.txt");
            
            Assert.Contains((1, 0, 0), cubeGrid.GridItems);
            Assert.Contains((0, 2, 0), cubeGrid.GridItems);
        }

        [Fact]
        public void TestSimulation()
        {
            var cubeGrid = Day17.ParseCubeGrid("Advent_of_Code_2020.Day17.SampleInput.txt");

            cubeGrid.RunSimulation(6);
            Assert.Equal(112, cubeGrid.GridItems.Count);
        }

        [Fact]
        public void Test4DSimulation()
        {
            var cubeGrid = Day17.ParseCubeGrid("Advent_of_Code_2020.Day17.SampleInput.txt");

            var cubeGrid4 = new CubeGrid4(cubeGrid);
            cubeGrid4.RunSimulation(6);
            Assert.Equal(848, cubeGrid4.GridItems.Count);
        }
    }
}