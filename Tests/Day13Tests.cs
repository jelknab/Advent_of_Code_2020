using System.Linq;
using Advent_of_Code_2020.Day12;
using Advent_of_Code_2020.Day13;
using Xunit;

namespace Tests
{
    public class Day13Tests
    {
        [Fact]
        public void TestParser()
        {
            var input = Day13.ParseBusses("Advent_of_Code_2020.Day13.SampleInput.txt");

            Assert.Equal(939, input.timestamp);
            Assert.Equal(new [] {7, 13, 0, 0, 59, 0, 31, 19}, input.ids);
        }

        [Fact]
        public void TestFindEarliestDeparture()
        {
            var (timestamp, ids) = Day13.ParseBusses("Advent_of_Code_2020.Day13.SampleInput.txt");

            var earliestDeparture = Day13.EarliestDeparture(timestamp, ids);
            var waitTime = Day13.NextDeparture(timestamp, earliestDeparture) - timestamp;
            
            Assert.Equal(59, earliestDeparture);
            Assert.Equal(5, waitTime);
            Assert.Equal(295, earliestDeparture * waitTime);
        }
        
        [Fact]
        public void TestPartTwo()
        {
            var (timestamp, ids) = Day13.ParseBusses("Advent_of_Code_2020.Day13.SampleInput.txt");

            Assert.Equal(1068781, Day13.FindMatchingTimestamp(ids));
            
            Assert.Equal(3417, Day13.FindMatchingTimestamp(new [] {17, 0, 13, 19}));
            Assert.Equal(754018, Day13.FindMatchingTimestamp(new [] {67, 7, 59, 61}));
            Assert.Equal(779210, Day13.FindMatchingTimestamp(new [] {67, 0, 7, 59, 61}));
        }
    }
}