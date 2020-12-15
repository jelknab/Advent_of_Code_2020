using System.Collections.Generic;
using System.Linq;
using Advent_of_Code_2020.Day15;
using Xunit;

namespace Tests
{
    public class Day15Tests
    {
        [Fact]
        public void TestMemoryGame()
        {
            Assert.Equal(436, Day15.PlayMemoryGame(new List<long>{0, 3, 6}, 2020).Last());
            Assert.Equal(1, Day15.PlayMemoryGame(new List<long>{1, 3, 2}, 2020).Last());
            Assert.Equal(10, Day15.PlayMemoryGame(new List<long>{2, 1, 3}, 2020).Last());
            Assert.Equal(27, Day15.PlayMemoryGame(new List<long>{1, 2, 3}, 2020).Last());
            Assert.Equal(78, Day15.PlayMemoryGame(new List<long>{2, 3, 1}, 2020).Last());
            Assert.Equal(438, Day15.PlayMemoryGame(new List<long>{3, 2, 1}, 2020).Last());
            Assert.Equal(1836, Day15.PlayMemoryGame(new List<long>{3, 1, 2}, 2020).Last());
        }
    }
}