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

        [Fact]
        public void Playground()
        {
            // var adapters = Day10.ParseAdapters("Advent_of_Code_2020.Day10.SampleInput.txt").ToArray();
            var adapters = new[] {0, 1, 4, 5, 6, 7, 10, 11, 12, 15, 16, 19, 22};
            var options = new int[adapters.Length];

            var paths = new int[adapters.Count()][];
            
            for (var i = 0; i < adapters.Length - 1; i++)
            {
                options[i] = adapters
                    .Where((adapter, index) => index > i && adapter - adapters[i] <= 3)
                    .Count() - 1;

                paths[i] = adapters
                    .Where((adapter, index) => index > i && adapter - adapters[i] <= 3)
                    .Select(adapter => Array.IndexOf(adapters, adapter))
                    .ToArray();
            }

            options = options.Where(option => option > 0).ToArray();
            
            Assert.Equal(8, options.Sum());
        }
    }
}