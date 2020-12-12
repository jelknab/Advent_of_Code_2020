using System.IO;
using System.Linq;
using Advent_of_Code_2020.Day12;
using Xunit;

namespace Tests
{
    public class Day12Tests
    {
        [Fact]
        public void TestParser()
        {
            var input = Day12.ParseNavigationInstructions("Advent_of_Code_2020.Day12.SampleInput.txt");

            Assert.Equal('F', input[0].Instruction);
            Assert.Equal(10, input[0].Value);
            
            Assert.Equal('F', input.Last().Instruction);
            Assert.Equal(11, input.Last().Value);
        }

        [Fact]
        public void TestShipInstructions()
        {
            var input = Day12.ParseNavigationInstructions("Advent_of_Code_2020.Day12.SampleInput.txt");

            var ship = new Movable();
            
            Day12.SailNavigationInstructions(ship, input);
            
            Assert.Equal(25, ship.Manhattan());
        }

        [Fact]
        public void TestShipAndWaypoint()
        {
            var input = Day12.ParseNavigationInstructions("Advent_of_Code_2020.Day12.SampleInput.txt");

            var ship = new Movable();
            var waypoint = new Movable {X = 10, Y = 1};
            
            Day12.CorrectNavigationInstructions(waypoint, ship, input);
            
            Assert.Equal(286, ship.Manhattan());
        }

        [Fact]
        public void TestShipFunctions()
        {
            var ship = new Movable();
            ship.MoveForward(1);
            
            Assert.Equal(1, ship.X);
            
            ship.RotateLeft(90);
            ship.MoveForward(1);
            
            Assert.Equal(1, ship.Y);
        }

        [Fact]
        public void TestRotations()
        {
            var ship = new Movable{X = 2, Y = 0};
            
            ship.RotateAroundCenterLeft(90);
            Assert.Equal(0, ship.X);
            Assert.Equal(2, ship.Y);
            
            ship.RotateAroundCenterLeft(90);
            Assert.Equal(-2, ship.X);
            Assert.Equal(0, ship.Y);
            
            ship.RotateAroundCenterRight(90);
            Assert.Equal(0, ship.X);
            Assert.Equal(2, ship.Y);
        }
    }
}