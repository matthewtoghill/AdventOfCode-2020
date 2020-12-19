using System;
using System.Collections.Generic;
using System.IO;

namespace Day03
{
    class Program
    {
        private static string[] input = File.ReadAllLines(@"..\..\..\data\day03.txt");
        static void Main(string[] args)
        {
            Part1();
            Part2();
            Console.ReadLine();
        }

        private static void Part1()
        {
            int treesHit = CountTreesHit(3, 1, input);
            Console.WriteLine($"\nPart 1: Trees Hit: {treesHit}\n");
        }

        private static void Part2()
        {
            long result = 1;
            List<int> allTreesHit = new List<int>
            {
                CountTreesHit(1, 1, input),
                CountTreesHit(3, 1, input),
                CountTreesHit(5, 1, input),
                CountTreesHit(7, 1, input),
                CountTreesHit(1, 2, input)
            };

            // Calculate the result by multiplying each treesHit number together.
            allTreesHit.ForEach(n => result *= n);

            Console.WriteLine($"\nPart 2: {result}");
        }

        private static int CountTreesHit(int stepsRight, int stepsDown, string[] map)
        {
            int treesHit = 0;
            int x = 0, y = 0;
            int mapWidth = map[0].Length;

            do
            {
                x = (x + stepsRight) % mapWidth;
                y += stepsDown;
                if (input[y][x] == '#') treesHit++;
            } while (y < map.Length - 1);

            Console.WriteLine($"With steps: Right {stepsRight}, Down {stepsDown} = {treesHit} Trees Hit");
            return treesHit;
        }
    }
}
