using System;
using System.Collections.Generic;
using System.Linq;
using Advent_of_Code_2020;
using Advent_of_Code_2020.Day1;
using Xunit;

namespace Tests
{
    public class Day1Tests
    {
        [Fact]
        private void TestFindValuesThatSum2020()
        {
            var input = new List<int> {1721, 979, 366, 299, 675, 1456};

            var twoSumsOutput = Day1.FindValuesThatSum2020(input, 2);

            Assert.Equal(new[] {1721, 299}, twoSumsOutput);
            Assert.Equal(514579, Day1.GetProduct(twoSumsOutput));

            var threeSumsOutput = Day1.FindValuesThatSum2020(input, 3);

            Assert.Equal(new[] {979, 366, 675}, threeSumsOutput);
            Assert.Equal(241861950, Day1.GetProduct(threeSumsOutput));
        }
    }
}