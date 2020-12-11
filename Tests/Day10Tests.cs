using System;
using System.Linq;
using Advent_of_Code_2020.Day10;
using Xunit;

namespace Tests
{
    public class Day10Tests
    {
        [Fact]
        public void TestDifferenceCounting()
        {
            var adapters = Day10.ParseAdapters("Advent_of_Code_2020.Day10.SampleInput.txt");
            var differences = Day10.GetDifferences(adapters);
            
            Assert.Equal(22, Day10.CountSpecificDifferences(differences, 1));
            Assert.Equal(10, Day10.CountSpecificDifferences(differences, 3));
        }

        [Fact]
        public void TestArrangements()
        {
            var adapters = Day10.ParseAdapters("Advent_of_Code_2020.Day10.SampleInput.txt");
            Assert.Equal(19208, Day10.CountAllArrangements(adapters));
        }
    }
}