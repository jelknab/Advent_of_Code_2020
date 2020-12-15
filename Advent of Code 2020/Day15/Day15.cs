using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Advent_of_Code_2020.Day15
{
    public class Day15 : IDay
    {
        public static List<long> ParseInput(string input)
        {
            return input.Split(',').Select(long.Parse).ToList();
        }

        private static int FindPreviousIndexOfNumber(List<long> input, int searchStart)
        {
            for (var a = searchStart-1; a >= 0; a--)
            {
                if (input[a] == input[searchStart])
                {
                    return a;
                }
            }

            throw new Exception("Could not find previous index");
        }
        
        private static void CreateOrIncrease(Dictionary<long, long> dict, long key)
        {
            dict[key] = dict.ContainsKey(key) ? dict[key] + 1 : 1;
        }

        public static List<long> PlayMemoryGame(List<long> input, int cutoff)
        {
            var countCache = new Dictionary<long, long>();
            var lastIndexCache = new Dictionary<long, FixedSizedQueue<int>>();

            for (var i = 0; i < input.Count; i++)
            {
                countCache[input[i]] = input.Count(n => n == input[i]);

                if (!lastIndexCache.TryGetValue(input[i], out _))
                {
                    lastIndexCache[input[i]] = new FixedSizedQueue<int> {Limit = 2};
                }
                
                lastIndexCache[input[i]].Enqueue(i);
            }

            for (var i = input.Count; i < cutoff; i++)
            {
                var lastSpoken = input[i - 1];

                if (!countCache.TryGetValue(lastSpoken, out var timesSpoken))
                {
                    countCache[lastSpoken] = 1;
                    timesSpoken = 1;
                }
                
                if (timesSpoken == 1)
                {
                    input.Add(0);
                    CreateOrIncrease(countCache, 0L);
                }

                if (timesSpoken > 1)
                {
                    var diff = (i - 1) - lastIndexCache[lastSpoken].q.First();
                    input.Add(diff);
                    CreateOrIncrease(countCache, diff);
                }
                
                if (!lastIndexCache.TryGetValue(input[i], out _))
                {
                    lastIndexCache[input[i]] = new FixedSizedQueue<int> {Limit = 2};
                }
                lastIndexCache[input[i]].Enqueue(i);
            }

            return input;
        }
        
        public void SolveProblem1()
        {
            Console.WriteLine(PlayMemoryGame(new List<long>{1,20,11,6,12,0}, 2020).Last());
        }

        public void SolveProblem2()
        {
            Console.WriteLine(PlayMemoryGame(new List<long>{1,20,11,6,12,0}, 30000000).Last());
        }
    }
    
    public class FixedSizedQueue<T>
    {
        public ConcurrentQueue<T> q = new ConcurrentQueue<T>();
        private object lockObject = new object();

        public int Limit { get; set; }
        public void Enqueue(T obj)
        {
            q.Enqueue(obj);
            lock (lockObject)
            {
                while (q.Count > Limit && q.TryDequeue(out _))
                {
                }
            }
        }
    }
}