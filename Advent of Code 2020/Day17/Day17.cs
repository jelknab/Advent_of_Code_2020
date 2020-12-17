using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Advent_of_Code_2020.Day17
{
    public class Day17 : IDay
    {
        public static CubeGrid3 ParseCubeGrid(string file)
        {
            return new ResourceReader<CubeGrid3> (file)
                .ReadFully(input =>
                {
                    var cubeGrid = new CubeGrid3();
                    
                    var lines = Regex.Matches(input, ".*\r?\n?", RegexOptions.Multiline)
                        .Select(match => match.Value)
                        .ToArray();

                    for (var y = 0; y < lines.Length; y++)
                    for (var x = 0; x < lines[y].Length; x++)
                        if (lines[y][x] == '#')
                            cubeGrid.SetActive((x, y, 0));

                    return cubeGrid;
                });
        }
        
        public void SolveProblem1()
        {
            var cubeGrid = ParseCubeGrid("Advent_of_Code_2020.Day17.input.txt");

            cubeGrid.RunSimulation(6);

            Console.WriteLine(cubeGrid.GridItems.Count);
        }

        public void SolveProblem2()
        {
            var cubeGrid = ParseCubeGrid("Advent_of_Code_2020.Day17.input.txt");
            
            var cubeGrid4 = new CubeGrid4(cubeGrid);
            cubeGrid4.RunSimulation(6);
            
            Console.WriteLine(cubeGrid4.GridItems.Count);
        }
    }

    public interface ICubeGrid
    {
        void RunSimulation(int cycles);
    }

    public class CubeGrid3 : ICubeGrid
    {
        public HashSet<(int x, int y, int z)> GridItems = new HashSet<(int x, int y, int z)>();

        public IEnumerable<(int x, int y, int z, bool active)> EnumerateNeighbors((int x, int y, int z) org)
        {
            for (var x = org.x - 1; x <= org.x + 1; x++)
            for (var y = org.y - 1; y <= org.y + 1; y++)
            for (var z = org.z - 1; z <= org.z + 1; z++)
                if (x != org.x || y != org.y || z != org.z)
                    yield return (x, y, z, GridItems.Contains((x, y, z)));
        }

        public void SetActive((int x, int y, int z) coordinate)
        {
            GridItems.Add(coordinate);
        }
        
        public void SetInActive((int x, int y, int z) coordinate)
        {
            GridItems.Remove(coordinate);
        }

        public void RunSimulation(int cycles)
        {
            for (var cycle = 0; cycle < cycles; cycle++)
            {
                var activations = new HashSet<(int x, int y, int z)>();
                var deactivations = new HashSet<(int x, int y, int z)>();

                foreach (var coordinate in GridItems)
                {
                    var activeNeighborsCount = EnumerateNeighbors(coordinate)
                        .Count(neighbour => neighbour.active);

                    if (activeNeighborsCount != 2 && activeNeighborsCount != 3)
                        deactivations.Add(coordinate);

                    var inactiveNeighborsToActivate = EnumerateNeighbors(coordinate)
                        .Where(neighbour => !neighbour.active)
                        .Where(inactive => EnumerateNeighbors((inactive.x, inactive.y, inactive.z))
                                .Count(neighbour => neighbour.active) == 3
                        );
                    
                    foreach (var (x, y, z, _) in inactiveNeighborsToActivate)
                    {
                        activations.Add((x, y, z));
                    }
                }

                foreach (var cube in activations) SetActive(cube);

                foreach (var cube in deactivations)  SetInActive(cube);
            }
        }
    }
    
    public class CubeGrid4 : ICubeGrid
    {
        public CubeGrid4(CubeGrid3 cubeGrid3)
        {
            foreach (var grid3Item in cubeGrid3.GridItems)
            {
                GridItems.Add((grid3Item.x, grid3Item.y, grid3Item.z, 0));
            }
        }
        
        public HashSet<(int x, int y, int z, int w)> GridItems = new HashSet<(int x, int y, int z, int w)>();

        public IEnumerable<(int x, int y, int z, int w, bool active)> EnumerateNeighbors((int x, int y, int z, int w) org)
        {
            for (var x = org.x - 1; x <= org.x + 1; x++)
            for (var y = org.y - 1; y <= org.y + 1; y++)
            for (var z = org.z - 1; z <= org.z + 1; z++)
            for (var w = org.w - 1; w <= org.w + 1; w++)
                if (x != org.x || y != org.y || z != org.z || w != org.w)
                    yield return (x, y, z, w, GridItems.Contains((x, y, z, w)));
        }

        public void SetActive((int x, int y, int z, int w) coordinate)
        {
            GridItems.Add(coordinate);
        }
        
        public void SetInActive((int x, int y, int z, int w) coordinate)
        {
            GridItems.Remove(coordinate);
        }

        public void RunSimulation(int cycles)
        {
            for (var cycle = 0; cycle < cycles; cycle++)
            {
                var activations = new HashSet<(int x, int y, int z, int w)>();
                var deactivations = new HashSet<(int x, int y, int z, int w)>();

                foreach (var coordinate in GridItems)
                {
                    var activeNeighborsCount = EnumerateNeighbors(coordinate)
                        .Count(neighbour => neighbour.active);

                    if (activeNeighborsCount != 2 && activeNeighborsCount != 3)
                        deactivations.Add(coordinate);

                    var inactiveNeighborsToActivate = EnumerateNeighbors(coordinate)
                        .Where(neighbour => !neighbour.active)
                        .Where(inactive => EnumerateNeighbors((inactive.x, inactive.y, inactive.z, inactive.w))
                                .Count(neighbour => neighbour.active) == 3
                        );
                    
                    foreach (var (x, y, z, w, _) in inactiveNeighborsToActivate)
                    {
                        activations.Add((x, y, z, w));
                    }
                }

                foreach (var cube in activations) SetActive(cube);

                foreach (var cube in deactivations)  SetInActive(cube);
            }
        }
    }
}