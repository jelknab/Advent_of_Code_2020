using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Advent_of_Code_2020.Day14
{
    public class Day14 : IDay
    {
        public static DockingComputer ParseMemory(string file, int version)
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

                    switch (version)
                    {
                        case 1:
                            computer.StoreDataVersion1(
                                long.Parse(memMatch.Groups["address"].Value),
                                long.Parse(memMatch.Groups["value"].Value)
                            );
                            break;
                        case 2:
                            computer.StoreDataVersion2(
                                long.Parse(memMatch.Groups["address"].Value),
                                long.Parse(memMatch.Groups["value"].Value)
                                );
                            break;
                    }
                    

                    return null;
                });

            return computer;
        }

        public void SolveProblem1()
        {
            var computer = ParseMemory("Advent_of_Code_2020.Day14.input.txt", version:1);

            Console.WriteLine(computer.Memory.Values.Sum());
        }

        public void SolveProblem2()
        {
            var computer = ParseMemory("Advent_of_Code_2020.Day14.input.txt", version:2);

            Console.WriteLine(computer.Memory.Values.Sum());
        }
    }

    public class DockingComputer
    {
        public Dictionary<long, long> Memory { get; set; } = new Dictionary<long, long>();

        public string Mask { get; set; }

        public void StoreDataVersion1(long address, long value)
        {
            var maskedValue = value;
            for (var i = 0; i < Mask.Length; i++)
            {
                switch (Mask[i])
                {
                    case 'X': break;
                    case '1':
                        maskedValue |= 1L << 35-i; 
                        break;
                    case '0':
                        maskedValue &= ~(1L << 35-i); 
                        break;
                }
            }
            
            Memory[address] = maskedValue;
        }
        
        private static IEnumerable<int> AllIndexesOf(string str, string searchString)
        {
            int minIndex = str.IndexOf(searchString);
            while (minIndex != -1)
            {
                yield return minIndex;
                minIndex = str.IndexOf(searchString, minIndex + searchString.Length);
            }
        }

        public void StoreDataVersion2(long address, long value)
        {
            var maskedAddress = address;
            
            for (var i = 0; i < Mask.Length; i++)
            {
                if (Mask[i] == '1') maskedAddress |= 1L << 35 - i;
            }
            
            
            var floatIndices = AllIndexesOf(Mask, "X").ToArray();


            for (long floater = 0; floater < Math.Pow(2, floatIndices.Length); floater++)
            {
                for (var i = 0; i < floatIndices.Length; i++)
                {
                    var floaterBit = (floater & (1L << i)) > 0;
                    var index = floatIndices[i];
                    maskedAddress ^= (-(floaterBit ? 1 : 0) ^ maskedAddress) & (1L << 35-index);
                }
                
                Memory[maskedAddress] = value;
            }
        }
    }
}