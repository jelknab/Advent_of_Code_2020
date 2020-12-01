using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Advent_of_Code_2020.Day1
{
    public class Day1 : Day
    {
        private static IEnumerable<int[]> SumIndicesEnumerable(int amountOfValues, int amountOfSums)
        {
            var summingIndices = new int[amountOfSums];
            
            for (var i = 0; i < Math.Pow(amountOfValues, amountOfSums); i++)
            {
                var continueFlag = false;
                for (var a = 0; a < amountOfSums; a++)
                {
                    summingIndices[a] = (int) (Math.Floor(i / Math.Pow(amountOfValues, a)) % amountOfValues);

                    if (a == 0 || summingIndices[a] < summingIndices[a - 1]) continue;
                    
                    continueFlag = true;
                    break;
                }
                if (continueFlag) continue;

                yield return summingIndices;
            }
        }
        
        public static int[] FindValuesThatSum2020(List<int> expenseRecords, int amountOfSums)
        {
            foreach (var sumIndices in SumIndicesEnumerable(expenseRecords.Count, amountOfSums))
            {
                var summingValues = sumIndices.Select(sumIndex => expenseRecords[sumIndex]);
                if (summingValues.Sum() == 2020)
                    return sumIndices
                        .OrderBy(index => index)
                        .Select(index => expenseRecords[index])
                        .ToArray();
            }

            throw new ArgumentException("No values add up to 2020");
        }

        public static int GetProduct(IEnumerable<int> values)
        {
            return values.Aggregate(1, (a, b) => a * b);
        }

        private static List<int> ReadInputToIntList()
        {
            var assembly = Assembly.GetExecutingAssembly();
            const string resourceName = "Advent_of_Code_2020.Day1.input.txt";

            using var stream = assembly.GetManifestResourceStream(resourceName) ?? throw new Exception("Input not read");
            using var reader = new StreamReader(stream);
            
            var values = new List<int>();
            
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                values.Add(int.Parse(line));
            }

            return values;
        }

        public void SolveProblem1()
        {
            var values = FindValuesThatSum2020(ReadInputToIntList(), 2);
            var productOfValues = GetProduct(values);
            Console.WriteLine(productOfValues);
        }

        public void SolveProblem2()
        {
            var values = FindValuesThatSum2020(ReadInputToIntList(), 3);
            var productOfValues = GetProduct(values);
            Console.WriteLine(productOfValues);
        }
    }
}