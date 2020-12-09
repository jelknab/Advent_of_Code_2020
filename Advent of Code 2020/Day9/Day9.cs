using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Advent_of_Code_2020.Day9
{
    public class Day9 : IDay
    {
        public static long[] XmasParser(string file)
        => new ResourceReader<long>(file).LineReader(long.Parse).ToArray();

        private static (int a, int b) FindPairThatSumsInput(long[] input, int preample, int index)
        {
            for (var y = index - preample; y < index; y++)
                for (var x = y + 1; x < index; x++)
                    if (input[y] + input[x] == input[index]) return (y, x);

            throw new InvalidDataException("No pair that sums could be made");
        }

        public static long FindWrongNumber(long[] input, int preample)
        {
            for (var i = preample; i < input.Length; i++)
            {
                try
                {
                    FindPairThatSumsInput(input, preample, i);
                }
                catch (InvalidDataException e)
                {
                    return input[i];
                }
            }
            
            throw new Exception("All values seem correct.");
        }

        public static List<long> FindSetOfNumbersThatSumInput(long[] numbers, long input)
        {
            var inputIndex = Array.IndexOf(numbers, input);

            for (var i = 0; i < inputIndex; i++)
            {
                var sum = numbers[i];

                for (var j = i; j < inputIndex; sum += numbers[++j])
                {
                    if (sum > input) break;
                    if (sum == input)
                        return numbers.Where((val, index) => index >= i && index <= j).ToList();
                }
            }

            throw new Exception("not found");
        } 
        
        public void SolveProblem1()
        {
            var input = XmasParser("Advent_of_Code_2020.Day9.input.txt");

            Console.WriteLine(FindWrongNumber(input, 25));
        }

        public void SolveProblem2()
        {
            var input = XmasParser("Advent_of_Code_2020.Day9.input.txt");

            var sumNumbers = FindSetOfNumbersThatSumInput(input, FindWrongNumber(input, 25));
            Console.WriteLine(sumNumbers.Min() + sumNumbers.Max());
        }
    }
}