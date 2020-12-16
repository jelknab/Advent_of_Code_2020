using System.Linq;
using Advent_of_Code_2020.Day16;
using Xunit;

namespace Tests
{
    public class Day16Tests
    {
        [Fact]
        public void TestParser()
        {
            var (ticketMachine, myTicket, tickets) = Day16.ParseNotes("Advent_of_Code_2020.Day16.SampleInput.txt");

            Assert.Equal(1, ticketMachine.Rules["class"][0].min);
            Assert.Equal(3, ticketMachine.Rules["class"][0].max);
            
            Assert.Equal(new [] {7, 1, 14}, myTicket.Fields);
            Assert.Equal(new [] {7, 3, 47}, tickets[0].Fields);
            Assert.Equal(new [] {40, 4, 50}, tickets[1].Fields);
        }

        [Fact]
        public void TestErrorRate()
        {
            var (ticketMachine, myTicket, tickets) = Day16.ParseNotes("Advent_of_Code_2020.Day16.SampleInput.txt");

            var errors = ticketMachine.FindFindErroringFields(tickets);
            Assert.Equal(71, errors.Sum());
        }

        [Fact]
        public void TestFieldIdentifier()
        {
            var (ticketMachine, myTicket, tickets) = Day16.ParseNotes("Advent_of_Code_2020.Day16.SampleInput2.txt");

            var fields = ticketMachine.IdentifyFields(tickets);
            
            Assert.Equal("row", fields[0]);
            Assert.Equal("class", fields[1]);
            Assert.Equal("seat", fields[2]);
        }
    }
}