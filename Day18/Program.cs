using AoCHelpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day18
{
    class Program
    {
        private static readonly string[] input = File.ReadAllLines(@"..\..\..\data\day18.txt");
        static void Main(string[] args)
        {
            Part1();
            Part2();
            Console.ReadLine();
        }

        private static void Part1()
        {
            long totalSum = 0;
            foreach (var line in input)
            {
                totalSum += ResolveExpression(line);
            }
            Console.WriteLine($"Part 1: {totalSum}");
        }

        private static void Part2()
        {
            long totalSum = 0;
            foreach (var line in input)
            {
                totalSum += ResolveExpression(line, '+');
            }
            Console.WriteLine($"\nPart 2: {totalSum}");
        }

        private static long ResolveExpression(string expression, char prioritize = ' ')
        {
            string current = expression;

            // Resolve each parenthesis working from the inside out if they are nested
            while (current.Count(c => c == '(') > 0)
            {
                int x = current.LastIndexOf('(');   // Find the last Open Bracket
                int y = current.IndexOf(')', x);    // Find the next Closed Bracket

                // Resolve the calculation within the brackets, then replace within the string to remove the brackets
                string fullInner = current.Substring(x, y - x + 1);
                string inner = current.Substring(x + 1, y - x - 1);

                if (prioritize != ' ')
                {
                    inner = ResolvePriority(inner, prioritize);
                }

                long innerResult = Calculate(inner);
                current = current.Replace(fullInner, innerResult.ToString());
            }

            // Resolve remaining expression
            if (prioritize != ' ')
            {
                current = ResolvePriority(current, prioritize);
            }

            long result = Calculate(current);
            return result;
        }

        private static string ResolvePriority(string expression, char prioritize)
        {
            while (expression.Contains(prioritize))
            {
                string[] terms = expression.Split(' ');
                // find the first time the terms list contains the priority item
                int z = terms.IndexOf(prioritize.ToString());
                string priorityInner = $"{terms[z - 1]} {terms[z]} {terms[z + 1]}";
                long priortyResult = Calculate(priorityInner);

                // rebuild the expression, replacing the priority calc with the result
                string newExpression = "";

                for (int i = 0; i < terms.Length; i++)
                {
                    if (i == z - 1 || i == z + 1) continue;
                    string nextPart = i == z ? priortyResult.ToString() : terms[i];
                    newExpression += nextPart + " ";
                }
                expression = newExpression.Substring(0, newExpression.Length - 1);
            }

            return expression;
        }

        private static long Calculate(string expression)
        {
            if (expression.Length == 0) return 0;
            string[] terms = expression.Split(' ');
            long result = long.Parse(terms[0]);
            Queue<string> opQueue = new Queue<string>();

            // Iterate through expression array, create opQueue and handle operations
            for (int i = 1; i < terms.Length; i++)
            {
                switch (terms[i])
                {
                    case "+":
                    case "*":
                        opQueue.Enqueue(terms[i]);
                        break;
                    default:
                        if (int.TryParse(terms[i], out int val))
                        {
                            string op = opQueue.Dequeue();
                            if (op == "+")
                                result += val;
                            else
                                result *= val;
                        }
                        break;
                }
            }

            return result;
        }
    }
}
