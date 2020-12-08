using System;
using System.Collections.Generic;
using System.IO;

namespace Day08
{
    class Program
    {
        private static string[] input = File.ReadAllLines(@"..\..\..\data\day08.txt");
        static void Main(string[] args)
        {
            Part1();
            Part2();
            Console.ReadLine();
        }

        private static void Part1()
        {
            List<Instruction> instructionSet = CreateInstructionSet();
            RunSimulation(instructionSet, out int accumulator);
            Console.WriteLine($"Part 1: Ended with acc: {accumulator}\n");
        }

        private static void Part2()
        {
            // Create the original unaltered instruction set
            List<Instruction> originalInstructionSet = CreateInstructionSet();
            int index = 0, accumulator = 0;

            // Iterate through the instruction set, attempt to create an altered instruction set and simulate it
            while (index < originalInstructionSet.Count - 1)
            {
                index++;

                // Get the altered instruction set
                List<Instruction> instructions = CreateAlteredInstructionSet(originalInstructionSet, index, out bool isAltered);

                // Use out isAltered to check if the method returned an altered list or not
                if (isAltered)
                {
                    // Only run simulations on altered instruction sets
                    if (RunSimulation(instructions, out accumulator) == false)
                    {
                        // exit the while loop if RunSimulation does not hit an infinite loop and returns false
                        break;
                    }
                }
            }

            Console.WriteLine($"\nPart 2: {accumulator} after changing index {index}");
        }

        private static List<Instruction> CreateInstructionSet()
        {
            List<Instruction> instructions = new List<Instruction>();

            // Convert string[] to List<Instruction>
            foreach (var line in input)
                instructions.Add(new Instruction(line));

            return instructions;
        }

        private static List<Instruction> CreateAlteredInstructionSet(List<Instruction> originalInstructions, int indexToChange, out bool isAltered)
        {
            isAltered = false;

            // Create a new deep copied instruction list from the original list by using the Instruction copy constructor 
            List<Instruction> instructions = new List<Instruction>(originalInstructions.Count);
            originalInstructions.ForEach((item) => { instructions.Add(new Instruction(item)); });

            // Swap nop <-> jmp commands at the specified index
            if (instructions[indexToChange].Command == "nop")
            {
                instructions[indexToChange].Command = "jmp";
                isAltered = true;
            }
            else if (instructions[indexToChange].Command == "jmp")
            {
                instructions[indexToChange].Command = "nop";
                isAltered = true;
            }

            return instructions;
        }

        private static bool RunSimulation(List<Instruction> instructionSet, out int accumulator)
        {
            bool hitLoop = false;
            int index = 0;
            accumulator = 0;

            // Keep looping as long as the index falls within the instruction set
            while (index < instructionSet.Count && index >= 0)
            {
                Instruction thisInstruction = instructionSet[index];

                // If the current instruction has been run before then exit the loop as an infinite loop has been found
                if (thisInstruction.TimesRun > 0)
                {
                    Console.WriteLine($"Loop hit at {index} acc: {accumulator}");
                    hitLoop = true;
                    break;
                }

                thisInstruction.TimesRun++;

                switch (thisInstruction.Command)
                {
                    case "acc":
                        accumulator += thisInstruction.Param;
                        index++;
                        break;
                    case "jmp":
                        index += thisInstruction.Param;
                        break;
                    case "nop":
                        index++;
                        break;
                    default:
                        break;
                };
            }

            return hitLoop;
        }
    }
}
