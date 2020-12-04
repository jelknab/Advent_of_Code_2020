using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Advent_of_Code_2020.Day4
{
    public class Day4 : IDay
    {
        public static readonly Dictionary<string, Func<string, bool>> ExpectedPassportFields = new Dictionary<string, Func<string, bool>>
        {
            {
                "byr",
                value =>
                {
                    if (!Regex.IsMatch(value, "^\\d{4}$")) return false;
                    
                    var date = int.Parse(value);
                    return date >= 1920 && date <= 2002;
                }
            },
            {
                "iyr",
                value =>
                {
                    if (!Regex.IsMatch(value, "^\\d{4}$")) return false;
                    
                    var date = int.Parse(value);
                    return date >= 2010 && date <= 2020;
                }
            },
            {
                "eyr",
                value =>
                {
                    if (!Regex.IsMatch(value, "^\\d{4}$")) return false;
                    
                    var date = int.Parse(value);
                    return date >= 2020 && date <= 2030;
                }
            },
            {
                "hgt",
                value =>
                {
                    var heightRegex = new Regex("^(?<height>\\d+)(?<unit>cm|in)$");
                    var regexMatch = heightRegex.Match(value);
                    
                    if (!regexMatch.Success) return false;

                    var height = int.Parse(regexMatch.Groups["height"].Value);

                    return regexMatch.Groups["unit"].Value switch
                    {
                        "cm" => height >= 150 && height <= 193,
                        "in" => height >= 59 && height <= 76,
                        _ => false
                    };
                }
            },
            {
                "hcl",
                value => Regex.IsMatch(value, "^#[0-f]{6}$")
            },
            {
                "ecl",
                value => Regex.IsMatch(value, "^amb|blu|brn|gry|grn|hzl|oth$")
            },
            {
                "pid",
                value => Regex.IsMatch(value, "^[0-9]{9}$")
            },
            {
                "cid",
                value => true
            }
        }; 
        
        private static readonly Regex KeyValuePattern = new Regex(
            "(?<key>\\S*):(?<value>\\S*)([\\s\\n]?)",
            RegexOptions.Multiline
        );

        public static IEnumerable<Passport> FilterPassportsByValidFields(IEnumerable<Passport> passports)
        {
            return passports
                .Where(passport => passport.Information.All(field => ExpectedPassportFields[field.Key].Invoke(field.Value)));
        }

        public static IEnumerable<Passport> FilterPassportsByExpectedFields(IEnumerable<Passport> passports)
        {
            return passports
                .Where(passport => !FindMissingPassportFields(passport).Any());
        }

        public static string[] FindMissingPassportFields(Passport passport)
        {
            return ExpectedPassportFields
                .Where(expectedField => !expectedField.Key.Equals("cid"))
                .Where(expectedField => !passport.Information.ContainsKey(expectedField.Key))
                .Select(expectedField => expectedField.Key)
                .ToArray();
        }

        public static IEnumerable<Passport> ParsePassports(string resourceName)
        {
            return new ResourceReader<List<Passport>>(resourceName)
                .ReadFully(input =>
                {
                    var passportParagraphs = input.Split(
                        new[] { "\r\n\r\n", "\r\r", "\n\n" },
                        StringSplitOptions.RemoveEmptyEntries
                    );

                    return passportParagraphs
                        .Select(paragraph => new Passport
                        {
                            Information = new Dictionary<string, string>(ParsePassportFields(paragraph))
                        })
                        .ToList();
                });
        }

        private static IEnumerable<KeyValuePair<string, string>> ParsePassportFields(string passportData)
        {
            return KeyValuePattern.Matches(passportData)
                .Select(match => new KeyValuePair<string, string>(
                    match.Groups["key"].Value,
                    match.Groups["value"].Value)
                );
        }

        public void SolveProblem1()
        {
            var passports = ParsePassports("Advent_of_Code_2020.Day4.input.txt");
            Console.WriteLine(FilterPassportsByExpectedFields(passports).Count());
        }

        public void SolveProblem2()
        {
            var passports = ParsePassports("Advent_of_Code_2020.Day4.input.txt");
            var passportsWithFields = FilterPassportsByExpectedFields(passports);
            var passportsWithFieldsAndValid = FilterPassportsByValidFields(passportsWithFields);
            Console.WriteLine(passportsWithFieldsAndValid.Count());
        }
    }

    public class Passport
    {
        public Dictionary<string, string> Information { get; set; }
    }
}