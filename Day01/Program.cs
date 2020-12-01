using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Day01
{
    class Program
    {
        private static string[] input = File.ReadAllLines(@"..\..\..\data\day01.txt");
        private static int requiredValue = 2020;
        static void Main(string[] args)
        {
            //Part1();
            BothParts();
            Console.ReadLine();
        }

        private static void Part1()
        {
            // Parse input to List of integers
            List<int> data = new List<int>();
            foreach (var item in input)
            {
                int.TryParse(item, out int num);
                data.Add(num);
            }

            bool keepGoing = true;

            int x = 0, y = 0;

            while (keepGoing)
            {
                x = data.First();
                for (int i = 1; i < data.Count; i++)
                {
                    if (data[i] + x == requiredValue)
                    {
                        y = data[i];
                        keepGoing = false;
                    }
                }
                data.RemoveAt(0);
            }

            Console.WriteLine("Part 1:");
            Console.WriteLine($"{x} + {y} = {x + y}");
            Console.WriteLine($"{x} * {y} = {x * y}");
        }

        private static void BothParts()
        {
            // Parse input to List of integers
            List<int> data = new List<int>();
            foreach (var item in input)
            {
                int.TryParse(item, out int num);
                data.Add(num);
            }

            // Sort High to Low
            data.Sort();
            data.Reverse();

            int a = 0, b = 0, c = 0;

            for (int i = 0; i < data.Count; i++)
            {
                a = data[i];

                for (int j = 1; j < data.Count; j++)
                {
                    b = data[j];

                    if (a + b == requiredValue)
                    {
                        Console.WriteLine("Part 1:");
                        Console.WriteLine($"{a} + {b} = {a + b}");
                        Console.WriteLine($"{a} * {b} = {a * b}");
                    }

                    for (int k = 2; k < data.Count; k++)
                    {
                        c = data[k];

                        if (a + b + c == requiredValue)
                        {
                            Console.WriteLine("\nPart 2:");
                            Console.WriteLine($"{a} + {b} + {c} = {a + b + c}");
                            Console.WriteLine($"{a} * {b} * {c} = {a * b * c}");
                            return;
                        }
                    }
                }
            }


        }
    }
}
