using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Advent_of_Code_2020.Day2
{
    public class Day2 : IDay
    {
        public static readonly Regex PolicyPattern = new Regex("(?<min>\\d+)-(?<max>\\d+)\\s(?<char>.):\\s(?<pass>.*)");

        private static List<PasswordPolicy> ReadPasswordPolicies()
        {
            var assembly = Assembly.GetExecutingAssembly();
            const string resourceName = "Advent_of_Code_2020.Day2.input.txt";

            using var stream = assembly.GetManifestResourceStream(resourceName) ?? throw new Exception("Input not read");
            using var reader = new StreamReader(stream);
            
            var values = new List<PasswordPolicy>();
            
            string line;
            while ((line = reader.ReadLine()) != null)
            {

                var match = PolicyPattern.Match(line);
                values.Add(new PasswordPolicy()
                {
                    FirstNumber = int.Parse(match.Groups["min"].Value),
                    SecondNumber = int.Parse(match.Groups["max"].Value),
                    Character = char.Parse(match.Groups["char"].Value),
                    Password = match.Groups["pass"].Value
                });
            }

            return values;
        }
        
        public void SolveProblem1()
        {
            var policies = ReadPasswordPolicies();
            var count = policies
                .Select(pp => pp.FulfillsPolicy1())
                .Count(result => result);
            Console.WriteLine(count);
        }

        public void SolveProblem2()
        {
            var policies = ReadPasswordPolicies();
            var count = policies
                .Select(pp => pp.FulfillsPolicy2())
                .Count(result => result);
            Console.WriteLine(count);
        }
    }

    public class PasswordPolicy
    {
        public int FirstNumber { get; set; }
        public int SecondNumber { get; set; }
        public char Character { get; set; }
        
        public string Password { get; set; }

        public bool FulfillsPolicy1()
        {
            var requiredCharCount = Password
                .ToCharArray()
                .Count(c => c == Character);

            return requiredCharCount <= SecondNumber && requiredCharCount >= FirstNumber;
        }
        
        public bool FulfillsPolicy2()
        {
            var passwordAsChars = Password.ToCharArray();

            return passwordAsChars[FirstNumber - 1] == Character ^ passwordAsChars[SecondNumber - 1] == Character;
        }
    }
}