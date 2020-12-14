using System.Linq;
using Advent_of_Code_2020.Day14;
using Xunit;

namespace Tests
{
    public class Day14Tests
    {
        [Fact]
        public void TestParser()
        {
            var computer = Day14.ParseMemory("Advent_of_Code_2020.Day14.SampleInput.txt");
            
            Assert.Equal(101, computer.Memory[7]);
            Assert.Equal(64, computer.Memory[8]);
            Assert.Equal(165, computer.Memory.Values.Sum());
        }
    }
}