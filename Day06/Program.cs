using AoCHelpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day06
{
    class Program
    {
        private static string[] input = File.ReadAllText(@"..\..\..\data\day06.txt").Split(new string[] { "\r\n\r\n", "\n\n" }, StringSplitOptions.RemoveEmptyEntries);
        static void Main(string[] args)
        {
            BothParts();
            Console.ReadLine();
        }

        private static void BothParts()
        {
            List<Dictionary<char, int>> allYesAnswers = new List<Dictionary<char, int>>();
            int totalGroupYesAnswers = 0;
            int totalGroupAllYes = 0;

            foreach (var group in input)
            {
                Dictionary<char, int> groupYesAnswers = new Dictionary<char, int>();
                string[] lines = group.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var line in lines)
                {
                    foreach (var c in line)
                    {
                        groupYesAnswers.IncrementAt(c);
                    }
                }

                totalGroupYesAnswers += groupYesAnswers.Count;
                totalGroupAllYes += groupYesAnswers.Count(y => y.Value == lines.Length);

                allYesAnswers.Add(groupYesAnswers);
            }

            Console.WriteLine($"Part 1: {totalGroupYesAnswers}");
            Console.WriteLine($"\nPart 2: {totalGroupAllYes}");
        }
    }
}
