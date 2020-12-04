using System.Linq;
using Advent_of_Code_2020.Day4;
using Xunit;

namespace Tests
{
    public class Day4Tests
    {
        [Fact]
        public void TestFirstProblem()
        {
            var passports = Day4.ParsePassports("Advent_of_Code_2020.Day4.SampleInput.txt")
                .ToList();
            
            Assert.Equal("gry", passports.First().Information["ecl"]);
            Assert.Equal("183cm", passports.First().Information["hgt"]);
            
            Assert.Equal("#cfa07d", passports.Last().Information["hcl"]);
            Assert.Equal("59in", passports.Last().Information["hgt"]);
            
            Assert.Empty(Day4.FindMissingPassportFields(passports.First()));
            Assert.Equal(new [] {"hgt"}, Day4.FindMissingPassportFields(passports[1]));
            
            Assert.Equal(2, Day4.FilterPassportsByExpectedFields(passports).Count());
        }

        [Fact]
        public void TestSecondProblem()
        {
            Assert.True(Day4.ExpectedPassportFields["byr"].Invoke("2002"));
            Assert.False(Day4.ExpectedPassportFields["byr"].Invoke("2003"));
            
            Assert.True(Day4.ExpectedPassportFields["hgt"].Invoke("60in"));
            Assert.True(Day4.ExpectedPassportFields["hgt"].Invoke("190cm"));
            Assert.False(Day4.ExpectedPassportFields["hgt"].Invoke("190in"));
            Assert.False(Day4.ExpectedPassportFields["hgt"].Invoke("190"));
            
            
            Assert.True(Day4.ExpectedPassportFields["hcl"].Invoke("#123abc"));
            Assert.False(Day4.ExpectedPassportFields["hcl"].Invoke("#123abz"));
            Assert.False(Day4.ExpectedPassportFields["hcl"].Invoke("123abc"));

            Assert.True(Day4.ExpectedPassportFields["ecl"].Invoke("brn"));
            Assert.False(Day4.ExpectedPassportFields["ecl"].Invoke("wat"));

            Assert.True(Day4.ExpectedPassportFields["pid"].Invoke("000000001"));
            Assert.False(Day4.ExpectedPassportFields["pid"].Invoke("0123456789"));
        }
    }
}