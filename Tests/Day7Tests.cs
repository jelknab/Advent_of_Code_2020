using System.Linq;
using Advent_of_Code_2020.Day7;
using Xunit;

namespace Tests
{
    public class Day7Tests
    {
        [Fact]
        public void TestInputParser()
        {
            var bags = Day7
                .ParseBagInput("Advent_of_Code_2020.Day7.SampleInput.txt")
                .ToList();

            var testChildBagAmount = bags
                .First(bag => bag.Color == "light red").ChildBags
                .First(relation => relation.ChildBag.Color == "muted yellow")
                .Amount;

            Assert.Equal(2, testChildBagAmount);
        }

        [Fact]
        public void TestCountContainingBags()
        {
            var bags = Day7
                .ParseBagInput("Advent_of_Code_2020.Day7.SampleInput.txt")
                .ToList();
            
            Assert.Equal(4, Day7.CountParentBags(bags, "shiny gold"));
        }

        [Fact]
        public void TestCountChildBagAmount()
        {
            var bags = Day7
                .ParseBagInput("Advent_of_Code_2020.Day7.SampleInput2.txt")
                .ToList();
            
            Assert.Equal(126, Day7.CountAmountOfChildBags(bags, "shiny gold"));
        }
    }
}