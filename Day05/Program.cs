using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day05
{
    class Program
    {
        private static string[] input = File.ReadAllLines(@"..\..\..\data\day05.txt");
        static void Main(string[] args)
        {
            BothParts();
            Console.ReadLine();
        }

        private static void BothParts()
        {
            List<PlaneSeat> planeSeats = new List<PlaneSeat>();

            // Itereate through each line of the input creating PlaneSeat objects and storing in list
            foreach (var line in input)
            {
                PlaneSeat seat = new PlaneSeat(line);
                planeSeats.Add(seat);
            }

            Console.WriteLine($"\nPart 1: Max ID: {planeSeats.Max(s => s.ID)}");

            // Create list of IDs
            List<int> seatIDs = planeSeats.Select(s => s.ID).ToList();

            // Get a list of any missing IDs
            List<int> missingValues = FindMissing(seatIDs).ToList();
            string missingIDs = string.Join(",", missingValues);

            // Print list of missing IDs
            Console.WriteLine($"\nPart 2: Missing IDs: {missingIDs}");
        }

        public static IEnumerable<int> FindMissing(IEnumerable<int> values)
        {
            int min = values.Min();
            int count = values.Max() - min;
            HashSet<int> myRange = new HashSet<int>(Enumerable.Range(min, count));
            myRange.ExceptWith(values);
            return myRange;
        }

    }
}
