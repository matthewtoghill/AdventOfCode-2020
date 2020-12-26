using AoCHelpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day13
{
    class Program
    {
        private static readonly string[] input = File.ReadAllLines(@"..\..\..\data\day13.txt");
        static void Main(string[] args)
        {
            Part1();
            Part2();
            Console.ReadLine();
        }

        private static void Part1()
        {
            int startMins = int.Parse(input[0]);
            string[] busesList = input[1].StripOut(",x").Split(',');
            Dictionary<int, int> soonestBusTimes = new Dictionary<int, int>();

            foreach (var bus in busesList)
            {
                int busNum = int.Parse(bus), soonestTime = 0;

                while (soonestTime < startMins)
                {
                    soonestTime += busNum;
                }

                soonestBusTimes.Add(busNum, soonestTime);
            }

            var soonestBus = soonestBusTimes.OrderBy(kvp => kvp.Value).First();
            Console.WriteLine($"Part 1: Bus: {soonestBus.Key} at {soonestBus.Value} \nAnswer: {(soonestBus.Value - startMins) * soonestBus.Key}");
        }

        private static void Part2()
        {
            // Tests
            Solve("7,13");
            Solve("7,13,x,x,59,x,31,19");
            Solve("17,x,13,19");
            Solve("67,7,59,61");
            Solve("67,x,7,59,61");
            Solve("67,7,x,59,61");
            Solve("1789,37,47,1889");

            // Part 2
            Console.WriteLine($"\nPart 2:");
            Solve(input[1]);
        }

        private static void Solve(string rules)
        {
            // Create list of buses, change the x values to 0's
            List<int> buses = rules.Split(',').Select(n => int.TryParse(n, out int v) ? v : 0).ToList();

            long t = 0, increment = buses[0];
            int offset = 1;

            // Check each bus in turn
            while (offset < buses.Count)
            {
                // Check for offset incrementing values
                if (buses[offset] == 0) { offset++; continue; }

                // Increase the timestamp by the increment
                t += increment;

                // If the timestamp + offset value is a multiple of the bus time
                // Set the new increment value by multiplying and move onto the next bus
                if ((t + offset) % buses[offset] == 0)
                {
                    increment *= buses[offset];
                    offset++;
                }
            }

            Console.WriteLine($"\n{rules}");
            Console.WriteLine($"Answer: {t}");
        }
    }
}
