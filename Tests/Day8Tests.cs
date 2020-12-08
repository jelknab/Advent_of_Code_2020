using Advent_of_Code_2020.Day8;
using Xunit;

namespace Tests
{
    public class Day8Tests
    {
        [Fact]
        public void TestLoopStopper()
        {
            var console = Day8.ParseGameConsoleCode("Advent_of_Code_2020.Day8.SampleInput.txt");
            Day8.DoesConsoleLoop(console);
            
            Assert.Equal(5, console.Accumulator);
        }

        [Fact]
        public void TestLoopFixer()
        {
            var console = Day8.ParseGameConsoleCode("Advent_of_Code_2020.Day8.SampleInput.txt");
            Day8.FindLoopingFix(console);
            
            Assert.Equal(8, console.Accumulator);
        }
    }
}