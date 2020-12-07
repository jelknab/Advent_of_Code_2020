using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Advent_of_Code_2020.Day7
{
    public class Day7 : IDay
    {
        private static readonly Regex BagRegex = new Regex("^(?<color>.+?)\\sbags\\scontain\\s(?:\\s*(?<amount>\\d+)\\s(?<child>.+?)\\sbags?[,|\\.])*$");
        
        public static IEnumerable<ColoredBag> ParseBagInput(string file)
        {
            var bags = new Dictionary<string, ColoredBag>();

            new ResourceReader<ColoredBag>(file)
                .LineReader(line =>
                {
                    var match = BagRegex.Match(line);

                    var color = match.Groups["color"].Value;

                    if (!bags.ContainsKey(color))
                        bags.Add(color, new ColoredBag() {Color = color});

                    for (var i = 0; i < match.Groups["child"].Captures.Count; i++)
                    {
                        var childColor = match.Groups["child"].Captures[i].Value;
                        var childAmount = match.Groups["amount"].Captures[i].Value;

                        if (!bags.ContainsKey(childColor))
                            bags.Add(childColor, new ColoredBag() {Color = childColor});

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

        private static int CountParentBags(ColoredBag coloredBag, ISet<ColoredBag> knownBags)
        {
            foreach (var bag in coloredBag.ParentBags.Select(relation => relation.Bag))
            {
                knownBags.Add(bag);
                CountParentBags(bag, knownBags);
            }

            return knownBags.Count;
        }

        public static int CountParentBags(IEnumerable<ColoredBag> bags, string color)
        {
            return CountParentBags(bags.First(bag => bag.Color == color), new HashSet<ColoredBag>());
        }

        private static int CountAmountOfChildBags(ColoredBag bag)
        {
            var count = 0;
            
            foreach (var relation in bag.ChildBags)
            {
                count += relation.Amount;
                count += CountAmountOfChildBags(relation.ChildBag) * relation.Amount;
            }

            return count;
        }
        

        public static int CountAmountOfChildBags(IEnumerable<ColoredBag> bags, string color)
        {
            return CountAmountOfChildBags(bags.First(bag => bag.Color == color));
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

    public class ColoredBag
    {
        public string Color { get; set; }
        public List<BagRelation> ChildBags { get; } = new List<BagRelation>();
        public List<BagRelation> ParentBags { get; } = new List<BagRelation>();
    }

    public class BagRelation
    {
        public ColoredBag Bag { get; set; }
        public ColoredBag ChildBag { get; set; }
        public int Amount { get; set; }
    }
}