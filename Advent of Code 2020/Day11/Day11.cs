using System;
using System.Linq;
using System.Text;

namespace Advent_of_Code_2020.Day11
{
    public class Day11 : IDay
    {
        public static string[] ParseSeatGrid(string file)
        {
            return new ResourceReader<string>(file).LineReader(line => line).ToArray();
        }

        private static int CountAdjacentOccupiedSeats(string[] seats, int seatX, int seatY)
        {
            var count = 0;

            for (var y = seatY - 1; y <= seatY + 1; y++)
            {
                if (y < 0 || y >= seats.Length) continue;
                
                for (var x = seatX - 1; x <= seatX + 1; x++)
                {
                    if (x < 0 || x >= seats[y].Length) continue;
                    if (x == seatX && y == seatY) continue;

                    if (seats[y][x] == '#') count++;
                }
            }

            return count;
        }

        public static string[] SimulateSeatsTillUnchanged(string[] seats)
        {
            var newSeatPlan = new StringBuilder[seats.Length];
            for (var i = 0; i < seats.Length; i++)
                newSeatPlan[i] = new StringBuilder(seats[i]);

            bool change;

            do
            {
                change = false;
                
                for (var y = 0; y < seats.Length; y++)
                {
                    for (var x = 0; x < seats.Length; x++)
                    {
                        switch (seats[y][x])
                        {
                            case '.':
                                continue;
                            case 'L':
                                if (CountAdjacentOccupiedSeats(seats, x, y) == 0)
                                {
                                    newSeatPlan[y][x] = '#';
                                    change = true;
                                }
                                break;
                            case '#':
                                if (CountAdjacentOccupiedSeats(seats, x, y) >= 4)
                                {
                                    newSeatPlan[y][x] = 'L';
                                    change = true;
                                }
                                break;
                        }
                    }
                }
                
                seats = newSeatPlan.Select(builder => builder.ToString()).ToArray();
            } while (change);

            return seats;
        }
        
        public void SolveProblem1()
        {
            var input = ParseSeatGrid("Advent_of_Code_2020.Day11.input.txt");
            var grid = SimulateSeatsTillUnchanged(input);

            Console.WriteLine(grid.Sum(row => row.Count(c => c == '#')));
        }

        public void SolveProblem2()
        {
            throw new System.NotImplementedException();
        }
    }
}