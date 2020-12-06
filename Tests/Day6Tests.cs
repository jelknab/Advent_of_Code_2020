using System.Linq;
using Advent_of_Code_2020.Day6;
using Xunit;

namespace Tests
{
    public class Day6Tests
    {
        [Fact]
        public void TestUniqueAnswer()
        {
            var groupAnswers = Day6
                .ParseInputPerGroup("Advent_of_Code_2020.Day6.SampleInput.txt")
                .ToList();
            
            Assert.Equal(new [] {'a', 'b', 'c'}, Day6.FindUniqueAnswers(groupAnswers[0]));
            Assert.Equal(new [] {'a', 'b', 'c'}, Day6.FindUniqueAnswers(groupAnswers[1]));
            Assert.Equal(new [] {'a', 'b', 'c'}, Day6.FindUniqueAnswers(groupAnswers[2]));
            Assert.Equal(new [] {'a'}, Day6.FindUniqueAnswers(groupAnswers[3]));
            Assert.Equal(new [] {'b'}, Day6.FindUniqueAnswers(groupAnswers[4]));
        }

        [Fact]
        public void TestMatchingAnswer()
        {
            var groupAnswers = Day6
                .ParseInputPerGroup("Advent_of_Code_2020.Day6.SampleInput.txt")
                .ToList();
            
            Assert.Equal(new [] {'a', 'b', 'c'}, Day6.FindMatchingAnswers(groupAnswers[0]));
            Assert.Empty(Day6.FindMatchingAnswers(groupAnswers[1]));
            Assert.Equal(new [] {'a'}, Day6.FindMatchingAnswers(groupAnswers[2]));
            Assert.Equal(new [] {'a'}, Day6.FindMatchingAnswers(groupAnswers[3]));
            Assert.Equal(new [] {'b'}, Day6.FindMatchingAnswers(groupAnswers[4]));
        }
    }
}