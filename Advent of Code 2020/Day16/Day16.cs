using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;

namespace Advent_of_Code_2020.Day16
{
    public class Day16 : IDay
    {
        public static (TicketMachine, Ticket, Ticket[]) ParseNotes(string file)
        {
            return new ResourceReader<(TicketMachine, Ticket, Ticket[])>(file)
                .ReadFully(input =>
                {
                    var ruleMatches = Regex.Matches(input,
                        "(?<rule>.+): (?<minA>\\d+)-(?<maxA>\\d+) or (?<minB>\\d+)-(?<maxB>\\d+)");

                    var ticketMachine = new TicketMachine
                    {
                        Rules = new Dictionary<string, (int min, int max)[]>(
                            ruleMatches.Select(match =>
                            {
                                return new KeyValuePair<string, (int min, int max)[]>(
                                    match.Groups["rule"].Value,
                                    new (int min, int max)[]
                                    {
                                        (int.Parse(match.Groups["minA"].Value), int.Parse(match.Groups["maxA"].Value)),
                                        (int.Parse(match.Groups["minB"].Value), int.Parse(match.Groups["maxB"].Value))
                                    }
                                );
                            })
                        )
                    };

                    var myTicketMatch = Regex.Match(input, "your ticket:\r?\n((?<field>\\d+),?)+\r?\n",
                        RegexOptions.Multiline);

                    var myTicket = new Ticket()
                    {
                        Fields = myTicketMatch.Groups["field"].Captures.Select(c => int.Parse(c.Value)).ToArray()
                    };

                    var ticketsMatch = Regex.Match(input, "nearby tickets:\r?\n(?<ticket>(\\d+,?)+\r?\n?)+");
                    var tickets = ticketsMatch.Groups["ticket"].Captures
                        .Select(capture => new Ticket
                        {
                            Fields = capture.Value.Split(',').Select(int.Parse).ToArray()
                        })
                        .ToArray();


                    return (ticketMachine, myTicket, tickets);
                });
        }

        public void SolveProblem1()
        {
            var (ticketMachine, myTicket, tickets) = ParseNotes("Advent_of_Code_2020.Day16.input.txt");

            var errors = ticketMachine.FindFindErroringFields(tickets);
            Console.WriteLine(errors.Sum());
        }

        public void SolveProblem2()
        {
            var (ticketMachine, myTicket, tickets) = ParseNotes("Advent_of_Code_2020.Day16.input.txt");
            
            var fields = ticketMachine.IdentifyFields(tickets);

            var departureFieldsMultiplied = fields
                .Where(field => field.StartsWith("departure"))
                .Select(field => myTicket.Fields[Array.IndexOf(fields, field)])
                .Aggregate(1L, (l, r) => l * r);

            Console.WriteLine(departureFieldsMultiplied);
        }
    }

    public class TicketMachine
    {
        public Dictionary<string, (int min, int max)[]> Rules { get; set; }

        public int[] FindFindErroringFields(Ticket[] tickets)
        {
            return tickets
                .Select(ticket => ticket.Fields).SelectMany(fields => fields)
                .Where(field => !Rules.Values.SelectMany(tuples => tuples)
                    .Any(rule => field >= rule.min && field <= rule.max)
                )
                .ToArray();
        }
        
        

        private int[] TestRulesAgainstTickets((int min, int max)[] rules, Ticket[] tickets, List<int> solved)
        {
            var ruleSuccessCount = new Dictionary<int, int>();
            
            foreach (var ticket in tickets)
            {
                for (int i = 0; i < ticket.Fields.Length; i++)
                {
                    if (solved.Contains(i)) continue;
                    var field = ticket.Fields[i];

                    if (rules.Any(rule => field >= rule.min && field <= rule.max))
                    {
                        if (!ruleSuccessCount.TryAdd(i, 1))
                        {
                            ruleSuccessCount[i]++;
                        }
                    }
                }
            }

            return ruleSuccessCount
                .Where(pair => pair.Value == tickets.Length)
                .Select(pair => pair.Key)
                .ToArray();
        }

        public string[] IdentifyFields(Ticket[] tickets)
        {
            var filteredTickets = tickets
                .Where(ticket => !FindFindErroringFields(new[] {ticket}).Any())
                .ToArray();

            var result = new string[filteredTickets[0].Fields.Length];
            var solved = new List<int>();
            var solvedRules = new List<string>();


            do
            {
                foreach (var rule in Rules.Keys)
                {
                    if (solvedRules.Contains(rule)) continue;
                    
                    var test = TestRulesAgainstTickets(Rules[rule], filteredTickets, solved);
                    if (test.Length == 1)
                    {
                        result[test[0]] = rule;
                        solved.Add(test[0]);
                        solvedRules.Add(rule);
                    }
                }
            } while (solved.Count < result.Length);
            

            return result;
        } 
    }

    public class Ticket
    {
        public int[] Fields { get; set; }
    }
}