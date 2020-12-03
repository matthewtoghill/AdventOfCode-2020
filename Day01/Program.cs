using System;
using System.Collections.Generic;
using System.IO;

namespace Day01
{
    class Program
    {
        private static string[] input = File.ReadAllLines(@"..\..\..\data\day01.txt");
        private static int requiredValue = 2020;
        static void Main(string[] args)
        {
            BothParts();
            Console.ReadLine();
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

            // Sort Low to High
            data.Sort();

            for (int i = 0; i < data.Count; i++)
            {
                int a = data[i];

                for (int j = 1; j < data.Count; j++)
                {
                    int b = data[j];

                    if (a + b == requiredValue)
                    {
                        Console.WriteLine("Part 1:");
                        Console.WriteLine($"{a} + {b} = {a + b}");
                        Console.WriteLine($"{a} * {b} = {a * b}");
                    }

                    for (int k = 2; k < data.Count; k++)
                    {
                        int c = data[k];

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
