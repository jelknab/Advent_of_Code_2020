using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace Advent_of_Code_2020.Day19
{
    public class Day19 : IDay
    {
        public static Rule ParseRuleCollectionPiped(string input)
        {
            var collections = input.Split('|');

            return new RuleCollectionPiped()
            {
                Groups = collections.Select(ParseRuleCollection).ToArray()
            };
        }
        
        private static Rule ParseRuleCollection(string input)
        {
            var references = Regex
                .Matches(input, "\\d+")
                .Select(match => int.Parse(match.Value));

            return new RuleCollection()
            {
                SubRules = references
                    .Select(reference => new ReferenceRule() {References = reference})
                    .ToArray()
            };
        }
        
        private static Rule ParseTextRule(string input)
        {
            var match = Regex.Match(input, "\"(.)\"").Groups[1].Value;

            return new TextRule() { Match = match[0] };
        }

        public static (Dictionary<int, Rule> rules, string[] inputs) ParseInput(string file)
        {
            return new ResourceReader<(Dictionary<int, Rule> rules, string[] inputs)>(file)
                .ReadFully(input =>
                {
                    var lines = input.Split(new[] {"\n", "\r\n"}, StringSplitOptions.RemoveEmptyEntries);

                    var rulesWithIndex = lines
                        .Where(line => Regex.IsMatch(line, "^\\d+:"))
                        .Select(ruleStr =>
                        {
                            var index = int.Parse(Regex.Match(ruleStr, "^\\d+").Value);
                            ruleStr = Regex.Replace(ruleStr, "^\\d+:\\s", "");
                            
                            if (ruleStr.Contains('|')) return (index, ParseRuleCollectionPiped(ruleStr));
                            if (ruleStr.Contains('\"')) return (index, ParseTextRule(ruleStr));
                            return (index, ParseRuleCollection(ruleStr));
                        })
                        .Select(pair => new KeyValuePair<int, Rule>(pair.index, pair.Item2))
                        .ToArray();

                    var rules = new Dictionary<int, Rule>(rulesWithIndex);

                    var inputs = lines
                        .Where(line => !Regex.IsMatch(line, "^\\d+:"))
                        .ToArray();

                    return (rules, inputs);
                });
        }
        
        public void SolveProblem1()
        {
            var (rules, inputs) = ParseInput("Advent_of_Code_2020.Day19.input.txt");
            Console.WriteLine(inputs.Count(input => rules[0].Matches(rules, input)));
        }

        public void SolveProblem2()
        {
            var (rules, inputs) = ParseInput("Advent_of_Code_2020.Day19.input.txt");

            rules[8] = ParseRuleCollectionPiped("42 | 42 8");
            rules[11] = ParseRuleCollectionPiped("42 31 | 42 11 31");
            
            Console.WriteLine(inputs.Count(input => rules[0].Matches(rules, input)));
        }
    }

    public abstract class Rule
    {
        public bool Matches(Dictionary<int, Rule> rules, string input)
        {
            var (matches, index) = Matches(rules, input, 0);
            
            return matches && index == input.Length;
        }
        
        protected internal abstract (bool matches, int newIndex) Matches(Dictionary<int, Rule> rules, string input,
            int index);
    }

    public class RuleCollection : Rule
    {
        public ReferenceRule[] SubRules { get; set; }
        
        protected internal override (bool matches, int newIndex) Matches(Dictionary<int, Rule> rules, string input,
            int index)
        {
            foreach (var subRule in SubRules)
            {
                var (matches, newIndex) = subRule.Matches(rules, input, index);

                if (!matches)
                    return (false, index);

                index = newIndex;
            }

            return (true, index);
        }
    }


    public class RuleCollectionPiped : Rule
    {
        public Rule[] Groups { get; set; }
        
        protected internal override (bool matches, int newIndex) Matches(Dictionary<int, Rule> rules, string input,
            int index)
        {
            foreach (var group in Groups)
            {
                var match = group.Matches(rules, input, index);

                if (match.matches)
                    return match;
            }

            return (false, index);
        }
    }
    
    public class TextRule : Rule
    {
        public char Match { get; set; }
        
        protected internal override (bool matches, int newIndex) Matches(Dictionary<int, Rule> rules, string input,
            int index)
        {
            if (index == input.Length) return (false, 0);
            
            return input[index] == Match ? (true, index + 1) : (false, index);
        }
    }
    
    public class ReferenceRule : Rule
    {
        public int References { get; set; }
        
        protected internal override (bool matches, int newIndex) Matches(Dictionary<int, Rule> rules, string input,
            int index)
        {
            return rules[References].Matches(rules, input, index);
        }
    }
}