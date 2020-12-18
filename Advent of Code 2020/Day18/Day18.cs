using System;
using System.Collections.Generic;
using System.Linq;

namespace Advent_of_Code_2020.Day18
{
    public class Day18 : IDay
    {
        public static EquationPiece[] ParseEquations(string file)
        {
            return new ResourceReader<EquationPiece>(file)
                .LineReader(ParseEquation)
                .ToArray();
        }
        
        private static int FindParenthesisStop(string line, int from)
        {
            var parenthesisStack = new Stack<int>(new []{from});

            var i = from + 1;
            for (; parenthesisStack.Any(); ++i)
            {
                if (line[i] == '(')
                    parenthesisStack.Push(i);

                if (line[i] == ')')
                    parenthesisStack.Pop();
            }

            return i;
        }

        public static EquationPiece ParseEquation(string line)
        {
            line = line.Replace(" ", "");
            
            var equation  = new ParenthesesEquationPiece();

            for (var i = 0; i < line.Length; i++)
            {
                switch (line[i])
                {
                    case '(':
                        var length = FindParenthesisStop(line, i) - i;
                        var subEquation = ParseEquation(line.Substring(i + 1, length - 2));
                        
                        i += length;
                        subEquation.OperatorBehind = (i < line.Length ? line[i] : '0');
                        
                        equation.SubPieces.Add(subEquation);
                        break;
                    case char n when n >= '0' && n <= '9':
                        var numberPiece = new NumberEquationPiece()
                        {
                            Number = n - '0',
                            OperatorBehind = (i < line.Length-1 ? line[++i] : '0')
                        };
                        equation.SubPieces.Add(numberPiece);
                        break;
                }
            }

            return equation;
        }

        public void SolveProblem1()
        {
            var equations = ParseEquations("Advent_of_Code_2020.Day18.input.txt");
            Console.WriteLine(equations.Sum(e => e.SolveSelf()));
        }

        public void SolveProblem2()
        {
            var equations = ParseEquations("Advent_of_Code_2020.Day18.input.txt");
            Console.WriteLine(equations.Sum(e => e.SolveSelfPrecedence()));
        }
    }

    public class Equation
    {
    }

    public abstract class EquationPiece
    {
        public char OperatorBehind { get; set; }

        public abstract long SolveSelf();

        public abstract long SolveSelfPrecedence();
    }

    public class NumberEquationPiece : EquationPiece
    {
        public long Number { get; set; }

        public override long SolveSelf()
        {
            return Number;
        }

        public override long SolveSelfPrecedence()
        {
            return Number;
        }
    }

    public class ParenthesesEquationPiece : EquationPiece
    {
        public List<EquationPiece> SubPieces = new List<EquationPiece>();

        public override long SolveSelf()
        {
            while (SubPieces.Count() > 1)
            {
                SubPieces[0] = SubPieces[0].OperatorBehind switch
                {
                    '+' => new NumberEquationPiece
                    {
                        Number = SubPieces[0].SolveSelf() + SubPieces[1].SolveSelf(),
                        OperatorBehind = SubPieces[1].OperatorBehind
                    },
                    '*' => new NumberEquationPiece
                    {
                        Number = SubPieces[0].SolveSelf() * SubPieces[1].SolveSelf(),
                        OperatorBehind = SubPieces[1].OperatorBehind
                    },
                    _ => SubPieces[0]
                };

                SubPieces.RemoveAt(1);
            }

            return SubPieces[0].SolveSelf();
        }

        public override long SolveSelfPrecedence()
        {
            while (SubPieces.Count > 1)
            {
                var piece = SubPieces.FirstOrDefault(p => p.OperatorBehind == '+')
                            ?? SubPieces[0];

                var pieceIndex = SubPieces.IndexOf(piece);
                
                SubPieces[pieceIndex] = SubPieces[pieceIndex].OperatorBehind switch
                {
                    '+' => new NumberEquationPiece
                    {
                        Number = SubPieces[pieceIndex].SolveSelfPrecedence() + 
                                 SubPieces[pieceIndex + 1].SolveSelfPrecedence(),
                        OperatorBehind = SubPieces[pieceIndex + 1].OperatorBehind
                    },
                    '*' => new NumberEquationPiece
                    {
                        Number = SubPieces[pieceIndex].SolveSelfPrecedence() * 
                                 SubPieces[pieceIndex + 1].SolveSelfPrecedence(),
                        OperatorBehind = SubPieces[pieceIndex + 1].OperatorBehind
                    },
                    _ => SubPieces[pieceIndex]
                };
                
                SubPieces.RemoveAt(pieceIndex + 1);
            }

            return SubPieces[0].SolveSelf();
        }
    }
}