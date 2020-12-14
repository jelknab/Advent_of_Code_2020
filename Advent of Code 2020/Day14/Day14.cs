using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Advent_of_Code_2020.Day14
{
    public class Day14 : IDay
    {
        public static DockingComputer ParseMemory(string file)
        {
            var computer = new DockingComputer();

            new ResourceReader<object>(file)
                .LineReader(line =>
                {
                    if (line.StartsWith("mask"))
                    {
                        var maskMatch = Regex.Match(line, "^mask\\s=\\s(?<mask>.*)$");
                        computer.Mask = maskMatch.Groups["mask"].Value;
                        return null;
                    }

                    var memMatch = Regex.Match(line, "^mem\\[(?<address>\\d+)\\]\\s=\\s(?<value>\\d+)$");

                    computer.WriteMemory(
                        long.Parse(memMatch.Groups["address"].Value),
                        long.Parse(memMatch.Groups["value"].Value)
                    );

                    return null;
                });

            return computer;
        }

        public void SolveProblem1()
        {
            var computer = ParseMemory("Advent_of_Code_2020.Day14.input.txt");

            Console.WriteLine(computer.Memory.Values.Sum());
        }

        public void SolveProblem2()
        {
            throw new System.NotImplementedException();
        }
    }

    public class DockingComputer
    {
        public Dictionary<long, long> Memory { get; set; } = new Dictionary<long, long>();

        public string Mask { get; set; }

        public void WriteMemory(long address, long value)
        {
            var maskedValue = value;
            for (var i = 0; i < Mask.Length; i++)
            {
                switch (Mask[i])
                {
                    case 'X': break;
                    case '1':
                        maskedValue ^= (-1 ^ maskedValue) & (1 << 35-i);
                        break;
                    case '0':
                        maskedValue ^= (0 ^ maskedValue) & (1 << 35-i);
                        break;
                }
            }
            
            Memory[address] = maskedValue;
        }
    }
}