using Advent_of_Code_2020.Day5;
using Xunit;

namespace Tests
{
    public class Day5Tests
    {
        [Theory]
        [InlineData("BFFFBBFRRR", 70, 7, 567)]
        [InlineData("FFFBBBFRRR", 14, 7, 119)]
        [InlineData("BBFFBBFRLL", 102, 4, 820)]
        private void TestParser(string seatString, int row, int column, int id)
        {
            Assert.Equal(row, Day5.GetRowFromSeatString(seatString));
            Assert.Equal(column, Day5.GetColumnFromSeatString(seatString));
            Assert.Equal(id, Day5.GetSeatId(row, column));
        }

        [Fact]
        private void TestSeatReverser()
        {
            var reverse = Day5.ReverseSeatId(567);
            
            Assert.Equal(70, reverse.row);
            Assert.Equal(7, reverse.col);
        }
    }
}