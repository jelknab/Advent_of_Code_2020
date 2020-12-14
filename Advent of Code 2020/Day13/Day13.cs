using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;

namespace Advent_of_Code_2020.Day13
{
    public class Day13 : IDay
    {
        public static (int timestamp, int[] ids) ParseBusses(string file)
        {
            return new ResourceReader<(int, int[])>(file)
                .ReadFully(input =>
                {
                    var match = Regex.Match(input, "^(?<timestamp>\\d+)\r?\n((?<id>\\d+|x),?)+$");

                    if (!match.Success) throw new Exception("Could not parse input");

                    var timestamp = int.Parse(match.Groups["timestamp"].Value);
                    var ids = new List<int>();

                    foreach (Capture idCapture in match.Groups["id"].Captures)
                    {
                        if (idCapture.Value == "x")
                        {
                            ids.Add(0);
                            continue;
                        }

                        ids.Add(int.Parse(idCapture.Value));
                    }

                    return (timestamp, ids.ToArray());
                });
        }

        public static int EarliestDeparture(int timestamp, int[] ids)
        {
            return ids
                .Where(id => id != 0)
                .Select(id => (NextDeparture(timestamp, id), id))
                .Min().id;
        }

        public static int NextDeparture(int timestamp, int id)
        {
            return (int) (Math.Ceiling(timestamp / (double) id) * id);
        }

        private static long CalculateFirstMatch(Bus b1, Bus b2)
        {
            for (long i = b1.BusID - b1.Offset;; i += b1.BusID)
            {
                if ((i + b2.Offset) % b2.BusID == 0)
                {
                    return i;
                }
            }
        }

        private static Bus CombineTwoBussesIntoOne(Bus b1, Bus b2)
        {
            var firstMatch = CalculateFirstMatch(b1, b2);
            return new Bus
            {
                BusID = b1.BusID * b2.BusID,
                Offset = firstMatch * -1
            };
        }

        public void SolveProblem1()
        {
            var (timestamp, ids) = ParseBusses("Advent_of_Code_2020.Day13.input.txt");

            var earliestDeparture = EarliestDeparture(timestamp, ids);
            var waitTime = NextDeparture(timestamp, earliestDeparture) - timestamp;

            Console.WriteLine(earliestDeparture * waitTime);
        }

        public static long FindMatchingTimestamp(int[] ids)
        {
            var busses = ids
                .Where(id => id != 0)
                .Select(id => new Bus
                {
                    BusID = id,
                    Offset = Array.IndexOf(ids, id)
                }).ToList();

            var currentBus = busses[0];

            for (int bi = 1; bi < busses.Count; bi++)
            {
                currentBus = CombineTwoBussesIntoOne(currentBus, busses[bi]);
            }

            return currentBus.Offset * -1;
        }

        public void SolveProblem2()
        {
            var (timestamp, ids) = ParseBusses("Advent_of_Code_2020.Day13.input.txt");

            Console.WriteLine(FindMatchingTimestamp(ids));
        }
    }

    public class Bus
    {
        public long BusID { get; set; }
        public long Offset { get; set; }
    }
}