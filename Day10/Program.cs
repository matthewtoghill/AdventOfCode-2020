using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day10
{
    class Program
    {
        private static string[] input = File.ReadAllLines(@"..\..\..\data\day10.txt");
        static void Main(string[] args)
        {
            Part1();
            Part2();
            Console.ReadLine();
        }

        private static void Part1()
        {
            List<int> adapters = input.Select(int.Parse).ToList();
            adapters = adapters.Concat(new List<int> { 0, adapters.Max() + 3 }).ToList();
            adapters.Sort();

            int diffOf1 = 0, diffOf3 = 0;

            for (int i = 0; i < adapters.Count - 1; i++)
            {
                int diff = adapters[i + 1] - adapters[i];

                if (diff == 1)
                {
                    diffOf1++;
                }
                else if (diff == 3)
                {
                    diffOf3++;
                }
            }

            Console.WriteLine($"\nPart 1: {diffOf1} * {diffOf3} = {diffOf1 * diffOf3}  Max: {adapters.Last()}");
        }

        private static void Part2()
        {
            List<int> adapters = input.Select(int.Parse).ToList();
            adapters = adapters.Concat(new List<int> { 0, adapters.Max() + 3 }).ToList();
            adapters.Sort();

            Dictionary<int, long> adapterRouteCounts = new Dictionary<int, long> { { 0, 1 } };

            // Iterate through the list of adapters
            foreach (var adapter in adapters)
            {
                // For each adapter, check if the list of adapters contains adapters with ratings +1, +2, or +3 
                // Increment the routes dictionary with the number of routes stored in the routes dict for the current adapter
                if (adapters.Contains(adapter + 1)) adapterRouteCounts.IncrementAt(adapter + 1, adapterRouteCounts[adapter]);
                if (adapters.Contains(adapter + 2)) adapterRouteCounts.IncrementAt(adapter + 2, adapterRouteCounts[adapter]);
                if (adapters.Contains(adapter + 3)) adapterRouteCounts.IncrementAt(adapter + 3, adapterRouteCounts[adapter]);
            }

            // Return the number of possible routes to reach the last adapter
            Console.WriteLine($"\nPart 2: {adapterRouteCounts[adapters.Max()]}");
        }
    }

    public static class DictionaryExtensions
    {
        public static void IncrementAt<T>(this Dictionary<T, int> dictionary, T index)
        {
            dictionary.TryGetValue(index, out int count);
            dictionary[index] = ++count;
        }

        public static void IncrementAt<T>(this Dictionary<T, int> dictionary, T index, int incrementAmount)
        {
            dictionary.TryGetValue(index, out int count);
            dictionary[index] = count + incrementAmount;
        }

        public static void IncrementAt<T>(this Dictionary<T, long> dictionary, T index, long incrementAmount)
        {
            dictionary.TryGetValue(index, out long count);
            dictionary[index] = count + incrementAmount;
        }
    }
}
