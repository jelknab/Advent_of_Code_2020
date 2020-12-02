using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Advent_of_Code_2020.Day2;
using Xunit;

namespace Tests
{
    public class Day2Tests
    {
        [Theory]
        [ClassData(typeof(PolicyTestData))]
        private void TestPasswordPolicyCheck(PasswordPolicy pp, bool correctForPolicy1, bool correctForPolicy2)
        {
            Assert.Equal(correctForPolicy1, pp.FulfillsPolicy1());
            Assert.Equal(correctForPolicy2, pp.FulfillsPolicy2());
        }

        [Fact]
        private void TestRegexPattern()
        {
            var match = Day2.PolicyPattern.Match("2-8 t: pncmjxlvckfbtrjh");
            Assert.Equal("2", match.Groups["num1"].Value);
            Assert.Equal("8", match.Groups["num2"].Value);
            Assert.Equal("t", match.Groups["char"].Value);
            Assert.Equal("pncmjxlvckfbtrjh", match.Groups["pass"].Value);
        }
    }

    public class PolicyTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new PasswordPolicy()
                {
                    FirstNumber = 1,
                    SecondNumber = 3,
                    Character = 'a',
                    Password = "abcde"
                },
                true,
                true
            };
            yield return new object[]
            {
                new PasswordPolicy()
                {
                    FirstNumber = 1,
                    SecondNumber = 3,
                    Character = 'b',
                    Password = "cdefg"
                },
                false,
                false
            };
            yield return new object[]
            {
                new PasswordPolicy()
                {
                    FirstNumber = 2,
                    SecondNumber = 9,
                    Character = 'c',
                    Password = "ccccccccc"
                },
                true,
                false
            };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}