using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Advent_of_Code_2020.Day6
{
    public class Day6 : IDay
    {
        public static char[] FindUniqueAnswers(string answers)
        {
            return answers.Replace("\n", "")
                .ToCharArray()
                .Distinct()
                .ToArray();
        }

        public static char[] FindMatchingAnswers(string answers)
        {
            return answers
                .Split('\n')
                .Select(answer => answer.ToCharArray())
                .Aggregate((a, b) => a.Intersect(b).ToArray());
        }
        
        public static IEnumerable<string> ParseInputPerGroup(string file)
        {
            return new ResourceReader<string>(file).ParagraphReader(paragraph => paragraph);
        }

        public void SolveProblem1()
        {
            var groupAnswers = ParseInputPerGroup("Advent_of_Code_2020.Day6.input.txt");
            var sumOfUniqueAnswerCount = groupAnswers
                .Select(FindUniqueAnswers)
                .Select(answers => answers.Length)
                .Sum();

            Console.WriteLine(sumOfUniqueAnswerCount);
        }

        public void SolveProblem2()
        {
            var groupAnswers = ParseInputPerGroup("Advent_of_Code_2020.Day6.input.txt");
            var sumOfMatchingAnswerCount = groupAnswers
                .Select(FindMatchingAnswers)
                .Select(answers => answers.Length)
                .Sum();

            Console.WriteLine(sumOfMatchingAnswerCount);
        }
    }
}