using System;
using System.Collections.Generic;
using System.Linq;

namespace Advent_of_Code_2020.Day5
{
    public class Day5 : IDay
    {
        public static byte GetRowFromSeatString(string seatString)
        {
            byte row = 0;
            for (var index = 0; index < 7; index++)
            {
                if (seatString[index] == 'B')
                    row = (byte) (row | (1 << (6 - index)));
            }

            return row;
        }

        public static byte GetColumnFromSeatString(string seatString)
        {
            byte col = 0;
            for (var index = 0; index < 3; index++)
            {
                if (seatString[index + 7] == 'R')
                    col = (byte) (col | (1 << (2 - index)));
            }

            return col;
        }

        public static int GetSeatId(int row, int col)
        {
            return row * 8 + col;
        }

        public static (int row, int col) ReverseSeatId(int id)
        {
            var row = (int) Math.Floor(id / 8d);
            var col = id - row * 8;
            
            return (row, col);
        }

        public static int FindIdOfOpenSeat(List<string> passes)
        {
            var seatGrid = new bool[128,8];
            
            passes
                .ForEach(pass =>
                {
                    var row = GetRowFromSeatString(pass);
                    var col = GetColumnFromSeatString(pass);
                    
                    seatGrid[row, col] = true;
                });

            for (var row = 0; row < 128; row++)
            {
                for (var col = 0; col < 8; col++)
                {
                    if (seatGrid[row, col]) continue;
                    
                    var id = GetSeatId(row, col);
                    var next = ReverseSeatId(id + 1);
                    var prev = ReverseSeatId(id - 1);

                    if (seatGrid[next.row, next.col] && seatGrid[prev.row, prev.col])
                        return id;
                }
            }

            return 0;
        }
        
        private static List<string> ReadBoardingPassesToList()
        {
            return new ResourceReader<string>("Advent_of_Code_2020.Day5.input.txt")
                .LineReader(line => line)
                .ToList();
        }

        public void SolveProblem1()
        {
            var highestId = ReadBoardingPassesToList()
                .Select(pass =>
                {
                    var row = GetRowFromSeatString(pass);
                    var col = GetColumnFromSeatString(pass);

                    return GetSeatId(row, col);
                })
                .Max();

            Console.WriteLine(highestId);
        }

        public void SolveProblem2()
        {
            Console.WriteLine(FindIdOfOpenSeat(ReadBoardingPassesToList()));
        }
    }
}