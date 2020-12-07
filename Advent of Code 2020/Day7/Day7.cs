using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

namespace Advent_of_Code_2020.Day7
{
    public class Day7 : IDay
    {
        private static readonly Regex BagRegex = new Regex("^(?<color>.+?)\\sbags\\scontain\\s(?:\\s*(?<amount>\\d+)\\s(?<child>.+?)\\sbags?[,|\\.])*$");
        
        public static IEnumerable<ColorBag> ParseBagInput(string file)
        {
            var bags = new Dictionary<string, ColorBag>();

            new ResourceReader<ColorBag>(file)
                .LineReader(line =>
                {
                    var match = BagRegex.Match(line);

                    var color = match.Groups["color"].Value;

                    if (!bags.ContainsKey(color))
                        bags.Add(color, new ColorBag() {Color = color});

                    for (var i = 0; i < match.Groups["child"].Captures.Count; i++)
                    {
                        var childColor = match.Groups["child"].Captures[i].Value;
                        var childAmount = match.Groups["amount"].Captures[i].Value;

                        if (!bags.ContainsKey(childColor))
                            bags.Add(childColor, new ColorBag() {Color = childColor});

                        var relation = new BagRelation()
                        {
                            Amount = int.Parse(childAmount),
                            Bag = bags[color],
                            ChildBag = bags[childColor]
                        };

                        bags[color].ChildBags.Add(relation);
                        bags[childColor].ParentBags.Add(relation);
                    }

                    return null;
                });
            return bags.Values;
        }

        public static int CountParentBags(List<ColorBag> bags, string color, HashSet<ColorBag> knownBags = null)
        {
            knownBags ??= new HashSet<ColorBag>();

            var colorBag = bags.First(bag => bag.Color == color);
            foreach (var bag in colorBag.ParentBags.Select(relation => relation.Bag))
            {
                knownBags.Add(bag);
                CountParentBags(bags, bag.Color, knownBags);
            }

            return knownBags.Count;
        }

        public static int CountAmountOfChildBags(List<ColorBag> bags, string color)
        {
            var count = 0;
            
            var colorBag = bags.First(bag => bag.Color == color);
            foreach (var relation in colorBag.ChildBags)
            {
                count += relation.Amount;
                count += CountAmountOfChildBags(bags, relation.ChildBag.Color) * relation.Amount;
            }

            return count;
        }

        public void SolveProblem1()
        {
            var bags = ParseBagInput("Advent_of_Code_2020.Day7.input.txt").ToList();
            Console.WriteLine(CountParentBags(bags, "shiny gold"));
        }

        public void SolveProblem2()
        {
            var bags = ParseBagInput("Advent_of_Code_2020.Day7.input.txt").ToList();
            Console.WriteLine(CountAmountOfChildBags(bags, "shiny gold"));
        }
    }

    public class ColorBag
    {
        public string Color { get; set; }
        public List<BagRelation> ChildBags { get; } = new List<BagRelation>();
        public List<BagRelation> ParentBags { get; } = new List<BagRelation>();
    }

    public class BagRelation
    {
        public ColorBag Bag { get; set; }
        public ColorBag ChildBag { get; set; }
        public int Amount { get; set; }
    }
}