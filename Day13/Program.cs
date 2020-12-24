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
                int busNum = int.Parse(bus);
                int soonestTime = 0;

                while (soonestTime < startMins)
                {
                    soonestTime += busNum;
                }

                soonestBusTimes.Add(busNum, soonestTime);
            }


            foreach (var item in soonestBusTimes.Keys)
                Console.WriteLine($"Bus: {item}, Soonest Time: {soonestBusTimes[item]}");

            var soonestBus = soonestBusTimes.OrderBy(kvp => kvp.Value).First();
            Console.WriteLine($"Part 1: Bus: {soonestBus.Key} at {soonestBus.Value}, result = {(soonestBus.Value - startMins) * soonestBus.Key}");
        }

        private static void Part2()
        {
            string[] rules = input[1].Split(',');
            rules.PrintAllLines(true);
        }
    }
}
