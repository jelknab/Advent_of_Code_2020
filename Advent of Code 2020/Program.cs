using System;

namespace Advent_of_Code_2020
{
    class Program
    {
        static void Main(string[] args)
        {
            IDay day = new Day3.Day3();
            try
            {
                day.SolveProblem1();
            }
            catch (NotImplementedException e)
            {
                Console.WriteLine("Day 1 not solved yet");
            }
            
            try
            {
                day.SolveProblem2();
            }
            catch (NotImplementedException e)
            {
                Console.WriteLine("Day 2 not solved yet");
            }
        }
    }
}