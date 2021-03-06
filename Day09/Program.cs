﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day09
{
    class Program
    {
        private static string[] input = File.ReadAllLines(@"..\..\..\data\day09.txt");
        private static List<long> allValues = new List<long>();
        static void Main(string[] args)
        {
            long x = Part1(25);
            Part2(x);
            Console.ReadLine();
        }

        private static long Part1(int preambleSize)
        {
            long result = 0;
            allValues = input.Select(long.Parse).ToList();

            for (int i = preambleSize; i < allValues.Count; i++)
            {
                long thisNum = allValues[i];
                bool combinationFound = false;

                for (int j = i - preambleSize; j < i - 1; j++)
                {
                    for (int k = i - (preambleSize - 1); k < i; k++)
                    {
                        if (allValues[j] + allValues[k] == thisNum)
                        {
                            combinationFound = true;
                            //Console.WriteLine($"{thisNum} = {allValues[j]} + {allValues[k]}");
                            break;
                        }
                    }

                    if (combinationFound) break;
                }

                if (!combinationFound)
                {
                    result = thisNum;
                    Console.WriteLine($"Part 1: No combination found for {thisNum}");
                    break;
                }
            }
            return result;
        }

        private static void Part2(long numX)
        {
            int numIndex = allValues.IndexOf(numX);
            long minInRange, maxInRange, minMaxSum;
            bool groupFound = false;

            for (int i = 0; i < numIndex; i++)
            {
                long sum = allValues[i];
                minInRange = sum;
                maxInRange = sum;

                for (int j = i + 1; j < numIndex - 1; j++)
                {
                    sum += allValues[j];

                    if (sum > numX) break;

                    minInRange = Math.Min(minInRange, allValues[j]);
                    maxInRange = Math.Max(maxInRange, allValues[j]);

                    if (sum == numX)
                    {
                        groupFound = true;
                        minMaxSum = minInRange + maxInRange;
                        Console.WriteLine($"\n{sum} made from contiguous index range {i} to {j}");
                        Console.WriteLine($"\nPart 2: {minInRange} + {maxInRange} = {minMaxSum}");
                        break;
                    }
                }

                if (groupFound) break;
            }
        }
    }
}
