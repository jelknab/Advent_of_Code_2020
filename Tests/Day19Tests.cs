using System.Linq;
using Advent_of_Code_2020.Day19;
using Xunit;

namespace Tests
{
    public class Day19Tests
    {
        [Fact]
        public void TestMatcher()
        {
            var (rules, inputs) = Day19.ParseInput("Advent_of_Code_2020.Day19.SampleInput.txt");
            
            Assert.Equal(2, inputs.Count(input => rules[0].Matches(rules, input)));
        }

        [Fact]
        public void TestLoopingMatcher()
        {
            var (rules, inputs) = Day19.ParseInput("Advent_of_Code_2020.Day19.SampleInput2.txt");
            
            rules[8] = Day19.ParseRuleCollectionPiped("42 | 42 8");
            rules[11] = Day19.ParseRuleCollectionPiped("42 31 | 42 11 31");

            Assert.Equal(12, inputs.Count(input => rules[0].Matches(rules, input)));
        }
    }
}