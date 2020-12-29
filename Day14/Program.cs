using AoCHelpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day14
{
    class Program
    {
        private static readonly string[] input = File.ReadAllLines(@"..\..\..\data\day14.txt");
        static void Main(string[] args)
        {
            Part1();
            Part2();
            Console.ReadLine();
        }

        private static void Part1()
        {
            string mask = "";
            Dictionary<int, long> memVals = new Dictionary<int, long>();
            foreach (var line in input)
            {
                if (line.Left(4) == "mask")
                {
                    mask = line.Right(36);
                    continue;
                }

                if (line.Left(3) == "mem")
                {
                    int memLoc = int.Parse(line.GetBetween("[", "]"));  // Parse the memory location
                    long num = long.Parse(line.RightOf("= "));          // Parse the value to be stored
                    long newNum = ApplyBitMask(num, mask);              // Apply the mask to the value before storing it
                    memVals[memLoc] = newNum;                           // Update the memory values dictionary
                }
            }

            Console.WriteLine($"Part 1: {memVals.Sum(v => v.Value)}");
        }

        private static void Part2()
        {
            string mask = "";
            Dictionary<long, long> memVals = new Dictionary<long, long>();
            foreach (var line in input)
            {
                if (line.Left(4) == "mask")
                {
                    mask = line.Right(36);
                    continue;
                }

                if (line.Left(3) == "mem")
                {
                    int memLoc = int.Parse(line.GetBetween("[", "]"));  // Parse the initial memory location
                    long num = long.Parse(line.RightOf("= "));          // Parse the value to be stored at memory locations
                    List<long> locs = GetMemoryLocations(memLoc, mask); // Get the list of memory locations
                    locs.ForEach(l => memVals[l] = num);                // Update the memory values using the memory locations
                }
            }

            Console.WriteLine($"\nPart 2: {memVals.Sum(v => v.Value)}");
        }

        private static long ApplyBitMask(long num, string mask)
        {
            // Convert num to 36 character binary string
            string bin = Convert.ToString(num, 2).PadLeft(36, '0');

            // Apply the mask to the binary string
            for (int i = 0; i < mask.Length; i++)
                if (mask[i] != 'X') bin = bin.ReplaceAtIndex(i, mask[i]);

            // Convert the updated binary string back to a number and return 
            return Convert.ToInt64(bin, 2);
        }

        private static List<long> GetMemoryLocations(int memLoc, string mask)
        {
            List<long> memLocations = new List<long>();

            // Convert the memLoc to 36 character binary string
            string bin = Convert.ToString(memLoc, 2).PadLeft(36, '0');

            // Apply the mask to the binary string
            for (int i = 0; i < mask.Length; i++)
                if (mask[i] != '0') bin = bin.ReplaceAtIndex(i, mask[i]);

            // Repalce the X values with 0's, this represents the first permutation
            bin = bin.Replace('X', '0');

            // Get the list of all floating X val permutations
            List<string> perms = GetPermutations(bin, mask);

            // Create the numerical list of memory locations by converting the binary strings
            perms.ForEach(p => memLocations.Add(Convert.ToInt64(p, 2)));

            return memLocations;
        }

        private static List<string> GetPermutations(string bin, string mask, int pos = 0)
        {
            List<string> results = new List<string>();

            // Iterate through characters in mask
            for (int i = pos; i < mask.Length; i++)
            {
                // If the char is 'X' replace it with a 1 and recursively call method again with incremented position
                if (mask[i] == 'X')
                {
                    string newBin = bin.ReplaceAtIndex(i, '1');
                    results.Add(newBin);

                    // Union the recursive method calls to exclude any duplicates
                    results = results.Union(GetPermutations(newBin, mask, i + 1)).ToList();
                }
            }

            results.Add(bin);
            return results;
        }
    }
}
