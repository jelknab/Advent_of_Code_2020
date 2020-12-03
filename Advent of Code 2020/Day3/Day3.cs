using System;
using System.Collections.Generic;
using System.Linq;

namespace Advent_of_Code_2020.Day3
{
    public class Day3 : IDay
    {
        public static readonly (int stepX, int stepY)[] slopes = {
            (1, 1),
            (3, 1),
            (5, 1),
            (7, 1),
            (1, 2)
        };
        
        public static int CountTreesOnSlope(List<string> grid, int stepX, int stepY)
        {
            var counter = 0;
            var step = 0;
            
            for (var y = 0; y < grid.Count; y += stepY, step++)
            {
                var x = stepX * step % grid[y].Length;
                
                if (grid[y][x] == '#') counter++;
            }

            return counter;
        }

        public static int[] GetTreesPerSlope(List<string> grid)
        {
            return slopes
                .Select(slope => CountTreesOnSlope(grid, slope.stepX, slope.stepY))
                .ToArray();
        }

        public static long GetProduct(IEnumerable<int> treesPerSlope)
        {
            return treesPerSlope.Aggregate(1L, (a, b) => a * b);
        }

        public static List<string> ParseTreeGrid(string resourceFile)
        {
            return new ResourceReader<string>(resourceFile)
                .LineReader(line => line)
                .ToList();
        }
        
        public void SolveProblem1()
        {
            var treeGrid = ParseTreeGrid("Advent_of_Code_2020.Day3.input.txt");
            Console.WriteLine(CountTreesOnSlope(treeGrid, 3, 1));
        }

        public void SolveProblem2()
        {
            var treeGrid = ParseTreeGrid("Advent_of_Code_2020.Day3.input.txt");
            var treesPerSlope = GetTreesPerSlope(treeGrid);
            Console.WriteLine(GetProduct(treesPerSlope));
        }
    }
}