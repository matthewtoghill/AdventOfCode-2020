using System;
using System.Collections.Generic;

namespace Day15
{
    class Program
    {
        private static readonly List<int> input = new List<int> { 5, 1, 9, 18, 13, 8, 0 };
        static void Main(string[] args)
        {
            int part1 = PlayGame(input, 2020);
            Console.WriteLine($"Part 1: After 2020 turns, last number spoken {part1}");

            int part2 = PlayGame(input, 30000000);
            Console.WriteLine($"Part 2: After 30000000 turns, last number spoken {part2}");

            Console.ReadLine();
        }

        private static int PlayGame(List<int> startingNums, int turnsToComplete)
        {
            int numbersSpoken = 0;
            int lastNumberSpoken = 0;
            Dictionary<int, (int ageStart, int ageEnd)> spokenNumbers = new Dictionary<int, (int ageStart, int ageEnd)>();

            // Record the starting numbers
            foreach (var num in startingNums)
            {
                numbersSpoken++;
                spokenNumbers[num] = (numbersSpoken, numbersSpoken);
                lastNumberSpoken = num;
            }

            while (numbersSpoken < turnsToComplete)
            {
                numbersSpoken++;
                // Check if the number has been spoken before
                if (spokenNumbers.ContainsKey(lastNumberSpoken))
                {
                    // Get the number of turns since the number was last spoken
                    int sinceLastSpoken = spokenNumbers[lastNumberSpoken].ageEnd - spokenNumbers[lastNumberSpoken].ageStart;

                    // Set the ageStart and ageEnd values for the new number in spokenNumbers dictionary
                    int ageStart = spokenNumbers.ContainsKey(sinceLastSpoken) ? spokenNumbers[sinceLastSpoken].ageEnd : numbersSpoken;
                    spokenNumbers[sinceLastSpoken] = (ageStart, numbersSpoken);

                    // Set the last spoken number
                    lastNumberSpoken = sinceLastSpoken;
                }
                else
                {
                    lastNumberSpoken = 0;
                }
            }
            return lastNumberSpoken;
        }
    }
}
