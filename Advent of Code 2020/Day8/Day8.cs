using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace Advent_of_Code_2020.Day8
{
    public class Day8 : IDay
    {
        public static GameConsole ParseGameConsoleCode(string file)
        {
            var instructions = new ResourceReader<IInstruction>(file)
                .LineReader(line =>
                {
                    var match = Regex.Match(line, "^(?<instruction>\\w+)\\s(?<value>[\\+|\\-]\\d+)$");
                    var instruction = match.Groups["instruction"].Value;
                    var value = int.Parse(match.Groups["value"].Value, NumberStyles.AllowLeadingSign);

                    return instruction switch
                    {
                        "acc" => new AccInstruction {Value = value},
                        "jmp" => new JmpInstruction {RelativeJumps = value},
                        "nop" => new NopInstruction {Value = value},
                        _ => throw new Exception($"No such instruction: {instruction}")
                    };
                });
            
            return new GameConsole
            {
                Instructions = instructions.ToList()
            };
        }

        public static bool DoesConsoleLoop(GameConsole console)
        {
            var visitedInstructions = new HashSet<int>();

            while (!visitedInstructions.Contains(console.InstructionPointer))
            {
                visitedInstructions.Add(console.InstructionPointer);
                
                if (!console.Execute()) return false;
            }

            return true;
        }

        public static void FindLoopingFix(GameConsole console)
        {
            for (var i = 0; i < console.Instructions.Count; i++)
            {
                var instruction = console.Instructions[i];

                switch (instruction)
                {
                    case JmpInstruction jmp:
                        console.Instructions[i] = new NopInstruction();
                        break;
                    case NopInstruction nop:
                        console.Instructions[i] = new JmpInstruction {RelativeJumps = nop.Value};
                        break;
                    default:
                        continue;
                }
                
                console.Reset();
                if (!DoesConsoleLoop(console)) return;

                console.Instructions[i] = instruction;
            }

            throw new Exception("Cannot fix looping issue");
        }
        
        public void SolveProblem1()
        {
            var console = ParseGameConsoleCode("Advent_of_Code_2020.Day8.input.txt");
            DoesConsoleLoop(console);

            Console.WriteLine(console.Accumulator);
        }

        public void SolveProblem2()
        {
            var console = ParseGameConsoleCode("Advent_of_Code_2020.Day8.input.txt");
            FindLoopingFix(console);

            Console.WriteLine(console.Accumulator);
        }
    }

    public class GameConsole
    {
        public int Accumulator { get; set; } = 0;
        
        public int InstructionPointer { get; set; } = 0;
        
        public List<IInstruction> Instructions { get; set; }

        public void Reset()
        {
            Accumulator = 0;
            InstructionPointer = 0;
        }

        public bool Execute()
        {
            if (InstructionPointer >= Instructions.Count)
                return false;
            
            Instructions[InstructionPointer].Execute(this);
            return true;
        }
    }

    public interface IInstruction
    {
        public abstract void Execute(GameConsole console);
    }

    public class AccInstruction : IInstruction
    {
        public int Value { get; set; }
        
        public void Execute(GameConsole console)
        {
            console.Accumulator += Value;
            console.InstructionPointer++;
        }
    }
    
    public class JmpInstruction : IInstruction
    {
        public int RelativeJumps { get; set; }
        
        public void Execute(GameConsole console)
        {
            console.InstructionPointer += RelativeJumps;
        }
    }
    
    public class NopInstruction : IInstruction
    {
        public int Value { get; set; }
        
        public void Execute(GameConsole console)
        {
            console.InstructionPointer++;
        }
    }
}