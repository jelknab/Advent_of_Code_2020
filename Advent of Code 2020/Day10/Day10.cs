using System;
using System.Collections.Generic;
using System.Linq;

namespace Advent_of_Code_2020.Day10
{
    public class Day10 : IDay
    {
        public static List<int> ParseAdapters(string file)
        {
            var adapters = new ResourceReader<int>(file)
                .LineReader(int.Parse)
                .OrderBy(a => a)
                .ToList();
            
            adapters.Add(adapters.Max() + 3);
            adapters.Insert(0, 0);

            return adapters;
        }

        public static int[] GetDifferences(List<int> adapters)
        {
            var differences = new int[adapters.Count - 1];
            
            for (var i = 0; i < adapters.Count - 1; i++)
                differences[i] = adapters[i + 1] - adapters[i];

            return differences;
        }

        public static int CountSpecificDifferences(IEnumerable<int> differences, int countingDifference)
        => differences.Count(difference => difference == countingDifference);

        public static long CountAllArrangements(List<int> adapters)
        {
            var paths = new int[adapters.Count()][];
            for (var i = 0; i < adapters.Count; i++)
            {
                paths[i] = adapters
                    .Where((adapter, index) => index > i && adapter - adapters[i] <= 3)
                    .Select(adapter => adapters.IndexOf(adapter))
                    .ToArray();
            }

            return CountAllArrangements(paths, new Dictionary<int, long>(), 0) + 1;
        }

        private static long CountAllArrangements(IReadOnlyList<int[]> paths, IDictionary<int, long> mem, int start)
        {
            if (mem.TryGetValue(start, out var count))
                return count;

            count = paths[start].Length - 1;
            
            foreach (var i in paths[start])
            {
                if (i == paths.Count() - 1) continue;
                count += CountAllArrangements(paths, mem, i);
            }

            mem[start] = count;

            return count;
        }

        public void SolveProblem1()
        {
            var adapters = ParseAdapters("Advent_of_Code_2020.Day10.input.txt");
            var differences = GetDifferences(adapters);

            var answer = CountSpecificDifferences(differences, 1) * CountSpecificDifferences(differences, 3);
            Console.WriteLine(answer);
        }

        public void SolveProblem2()
        {
            var adapters = ParseAdapters("Advent_of_Code_2020.Day10.input.txt");
            Console.WriteLine(CountAllArrangements(adapters));
        }
    }
}