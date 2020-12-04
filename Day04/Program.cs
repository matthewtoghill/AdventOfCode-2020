using System;
using System.Collections.Generic;
using System.IO;

namespace Day04
{
    class Program
    {
        private static string[] input = File.ReadAllText(@"..\..\..\data\day04.txt").Split(new string[] { "\r\n\r\n" }, StringSplitOptions.RemoveEmptyEntries);
        static void Main(string[] args)
        {
            BothParts();
            Console.ReadLine();
        }

        private static void BothParts()
        {
            List<Passport> passports = new List<Passport>();
            int allRequiredFieldsCount = 0, validCount = 0;

            foreach (var line in input)
            {
                Passport newPassport = new Passport();

                // Replace spaces with new lines then split on new lines to a new string array
                string[] passportData = line.Replace(' ', '\n').Split('\n');

                // Iterate through array of passport data
                foreach (var item in passportData)
                {
                    // Split on : to get key and value pair
                    string[] keyValuePair = item.Split(':');
                    string key = keyValuePair[0];
                    string val = keyValuePair[1];

                    // Try parsing the key and value to the passport object
                    newPassport.TryParse(key, val);
                }

                // Add the passport to the list
                passports.Add(newPassport);

                // Check if the passport meets the criteria for each part
                if (newPassport.HasRequiredFields()) allRequiredFieldsCount++;
                if (newPassport.IsValid()) validCount++;
            }

            Console.WriteLine($"Part 1: {allRequiredFieldsCount} valid passports");

            Console.WriteLine($"\nPart 2: {validCount} valid passports");
        }
    }
}
