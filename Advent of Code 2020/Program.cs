using System;

namespace Advent_of_Code_2020
{
    class Program
    {
        static void Main(string[] args)
        {
            var dayType = Type.GetType($"Advent_of_Code_2020.Day{DateTime.Today.Day}.Day{DateTime.Today.Day}")
                    ?? throw new Exception("Please stick to naming scheme format."); 
            
            var day = (IDay) Activator.CreateInstance(dayType)
                      ?? throw new Exception("Could not make instance of today's challenge.");

            try
            {
                Console.WriteLine("Solution problem 1:");
                day.SolveProblem1();
            }
            catch (NotImplementedException e)
            {
                Console.WriteLine("Day 1 not solved yet");
            }

            Console.WriteLine();
            
            try
            {
                Console.WriteLine("Solution problem 2:");
                day.SolveProblem2();
            }
            catch (NotImplementedException e)
            {
                Console.WriteLine("Day 2 not solved yet");
            }
        }
    }
}