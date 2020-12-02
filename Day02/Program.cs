using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Day02
{
    class Program
    {
        private static string[] input = File.ReadAllLines(@"..\..\..\data\day02.txt");
        static void Main(string[] args)
        {
            Part1();
            Console.ReadLine();
        }

        private static void Part1()
        {
            int validCount = 0;
            string charRule;
            string pword;

            // Iterate through each line of the input
            foreach (var line in input)
            {
                // Parse the line to retrieve each part
                int.TryParse(line.Substring(0, line.IndexOf('-')), out int lowRule);
                int.TryParse(line.Substring(line.IndexOf('-') + 1, line.IndexOf(':') - 4), out int highRule);
                charRule = line.Substring(line.IndexOf(':') - 1, 1);
                pword = line.Substring(line.IndexOf(':') + 2);

                // Count the number of times the character appears in the string
                int charCount = 0;
                for (int i = 0; i < pword.Length; i++)
                {
                    if (pword[i].ToString() == charRule) charCount++; 
                }

                // Count the password as valid if the character appears within the required low to high rule range
                if (lowRule <= charCount && charCount <= highRule) validCount++;
            }

            Console.WriteLine($"Part 1, Valid Passwords: {validCount}");
        }
    }
}
