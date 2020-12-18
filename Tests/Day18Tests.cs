using System.Collections.Generic;
using Advent_of_Code_2020.Day18;
using Xunit;

namespace Tests
{
    public class Day18Tests
    {
        [Fact]
        public void TestParserAndSolver()
        {
            Assert.Equal(71, Day18.ParseEquation("1 + 2 * 3 + 4 * 5 + 6").SolveSelf());
            Assert.Equal(51, Day18.ParseEquation("1 + (2 * 3) + (4 * (5 + 6))").SolveSelf());
        }

        [Fact]
        public void TestPrecedence()
        {
            Assert.Equal(231, Day18.ParseEquation("1 + 2 * 3 + 4 * 5 + 6").SolveSelfPrecedence());
            Assert.Equal(51, Day18.ParseEquation("1 + (2 * 3) + (4 * (5 + 6))").SolveSelfPrecedence());
            Assert.Equal(46, Day18.ParseEquation("2 * 3 + (4 * 5)").SolveSelfPrecedence());
            Assert.Equal(1445, Day18.ParseEquation("5 + (8 * 3 + 9 + 3 * 4 * 3)").SolveSelfPrecedence());
            Assert.Equal(669060, Day18.ParseEquation("5 * 9 * (7 * 3 * 3 + 9 * 3 + (8 + 6 * 4))").SolveSelfPrecedence());
            Assert.Equal(23340, Day18.ParseEquation("((2 + 4 * 9) * (6 + 9 * 8 + 6) + 6) + 2 + 4 * 2").SolveSelfPrecedence());
        }
    }
}