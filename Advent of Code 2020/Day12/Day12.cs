using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Advent_of_Code_2020.Day12
{
    public class Day12 : IDay
    {
        private static readonly Regex NavigationInstructionRegex = new Regex("^(?<instruction>\\w)(?<value>\\d+$)"); 
        
        public static InstructionValue[] ParseNavigationInstructions(string file)
        {
            return new ResourceReader<InstructionValue>(file)
                .LineReader(line =>
                {
                    var match = NavigationInstructionRegex.Match(line);
                    
                    return new InstructionValue
                    {
                        Instruction = match.Groups["instruction"].Value[0],
                        Value = int.Parse(match.Groups["value"].Value)
                    };
                })
                .ToArray();
        }

        public static void SailNavigationInstructions(Movable ship, InstructionValue[] instructions)
        {
            foreach (var instruction in instructions)
            {
                switch (instruction.Instruction)
                {
                    case 'F':
                        ship.MoveForward(instruction.Value);
                        break;
                    case 'L':
                        ship.RotateLeft(instruction.Value);
                        break;
                    case 'R':
                        ship.RotateRight(instruction.Value);
                        break;
                    default:
                        ship.MoveInDirection(instruction.Instruction, instruction.Value);
                        break;
                }
            }
        }

        public static void CorrectNavigationInstructions(Movable waypoint, Movable ship, InstructionValue[] instructions)
        {
            foreach (var instruction in instructions)
            {
                switch (instruction.Instruction)
                {
                    case 'F':
                        ship.MoveInDirection('N', waypoint.Y * instruction.Value);
                        ship.MoveInDirection('E', waypoint.X * instruction.Value);
                        break;
                    case 'L':
                        waypoint.RotateAroundCenterLeft(instruction.Value);
                        break;
                    case 'R':
                        waypoint.RotateAroundCenterRight(instruction.Value);
                        break;
                    default:
                        waypoint.MoveInDirection(instruction.Instruction, instruction.Value);
                        break;
                }
            }
        }
        
        public void SolveProblem1()
        {
            var input = ParseNavigationInstructions("Advent_of_Code_2020.Day12.input.txt");

            var ship = new Movable();
            
            SailNavigationInstructions(ship, input);

            Console.WriteLine(ship.Manhattan());
        }

        public void SolveProblem2()
        {
            var input = ParseNavigationInstructions("Advent_of_Code_2020.Day12.input.txt");

            var ship = new Movable();
            
            CorrectNavigationInstructions(waypoint: new Movable() {X = 10, Y = 1}, ship, input);

            Console.WriteLine(ship.Manhattan());
        }
    }

    public class InstructionValue
    {
        public char Instruction { get; set; }
        public int Value { get; set; }
    }

    public class Movable
    {
        private static readonly Dictionary<char, (int x, int y)> Movements = new Dictionary<char, (int x, int y)>()
        {
            {'N', (0, 1)},
            {'S', (0, -1)},
            {'E', (1, 0)},
            {'W', (-1, 0)}
        };
        private static readonly Dictionary<int, char> DirectionToMovement = new Dictionary<int, char>()
        {
            {0, 'N'},
            {90, 'E'},
            {180, 'S'},
            {270, 'W'}
        };

        public int Dir { get; set; } = 90;

        public int X { get; set; }
        public int Y { get; set; }

        public void MoveInDirection(char direction, int steps)
        {
            X += Movements[direction].x * steps;
            Y += Movements[direction].y * steps;
        }

        public void MoveForward(int steps)
        {
            MoveInDirection(DirectionToMovement[Dir], steps);
        }

        public void RotateLeft(int amount)
        {
            Dir -= amount;
            if (Dir < 0)
            {
                Dir += 360;
            }
        }
        
        public void RotateRight(int amount)
        {
            Dir = (Dir + amount) % 360;
        }

        public int Manhattan()
        {
            return Math.Abs(X) + Math.Abs(Y);
        }

        public void RotateAroundCenterRight(int instructionValue)
        {
            var degToRad = -instructionValue * (Math.PI / 180);

            var s = Math.Sin(degToRad);
            var c = Math.Cos(degToRad);

            var xNew = Math.Round(X * c - Y * s);
            var yNew = Math.Round(X * s + Y * c);

            X = (int) xNew;
            Y = (int) yNew;
        }
        
        public void RotateAroundCenterLeft(int instructionValue)
        {
            var degToRad = instructionValue * (Math.PI / 180);

            var s = Math.Sin(degToRad);
            var c = Math.Cos(degToRad);

            var xNew = Math.Round(X * c - Y * s);
            var yNew = Math.Round(X * s + Y * c);

            X = (int) xNew;
            Y = (int) yNew;
        }
    }
}