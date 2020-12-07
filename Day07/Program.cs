using System;
using System.Collections.Generic;
using System.IO;

namespace Day07
{
    class Program
    {
        private static string[] input = File.ReadAllLines(@"..\..\..\data\day07.txt");
        private static List<BagRule> allBagRules = new List<BagRule>();
        private static string ourBagColour = "shiny gold";

        static void Main(string[] args)
        {
            CreateBagRules();
            Part1();
            Part2();
            Console.ReadLine();
        }

        private static void Part1()
        {
            // Create list of outermost bags that could contain at least 1 shiny gold bag
            List<string> bagsContainingOurBagColour = new List<string>();

            foreach (var rule in allBagRules)
            {
                if (RuleContainsBag(rule, ourBagColour)) bagsContainingOurBagColour.Add(rule.Name);
            }

            Console.WriteLine($"Part 1: {bagsContainingOurBagColour.Count - 1}");
        }

        private static void Part2()
        {
            // Count the number of bags inside our bag rule
            BagRule ourBagRule = allBagRules.Find(r => r.Name == ourBagColour);
            long totalBagsInRule = BagsWithinRule(ourBagRule, 1);
            Console.WriteLine($"\nPart 2: {totalBagsInRule}");
        }

        private static long BagsWithinRule(BagRule rule, int quantity)
        {
            long result = rule.Quantity;
            foreach (var bag in rule.BagContents)
            {
                result += quantity * bag.Quantity;
                BagRule thisRule = allBagRules.Find(r => r.Name == bag.Name);
                result += quantity * BagsWithinRule(thisRule, bag.Quantity);
            }
            return result;
        }

        private static bool RuleContainsBag(BagRule rule, string bagName)
        {
            bool result = false;

            if (rule.Name == bagName) return true;

            foreach (var bag in rule.BagContents)
            {
                BagRule thisRule = allBagRules.Find(r => r.Name == bag.Name);
                result = RuleContainsBag(thisRule, bagName);
                if (result) return true;
            }

            return result;
        }

        private static void CreateBagRules()
        {
            allBagRules = new List<BagRule>();

            foreach (var line in input)
            {
                // Remove words bag and bags and Replace 'contains' with ":"
                string newLine = line.Replace(".", "").Replace("bags", "bag").Replace("bag", "").Replace("contain", ":");

                //Console.WriteLine($"\n{newLine}");

                // Split on ':'
                string[] thisLine = newLine.Split(':');
                string thisColour = thisLine[0].Trim();

                // Split on ','
                string[] contains = thisLine[1].Split(',');

                BagRule newBag = new BagRule();
                newBag.Name = thisColour;
                //Console.WriteLine($"{newBag.Name}");

                foreach (var bag in contains)
                {
                    string thisContents = bag.Trim();

                    if (thisContents != "no other")
                    {
                        string quantity = thisContents.Substring(0, thisContents.IndexOf(' ')).Trim();
                        string name = thisContents.Substring(thisContents.IndexOf(' ')).Trim();
                        newBag.BagContents.Add(new BagRule(name, quantity));
                        //Console.WriteLine($"{quantity} - {name}");
                    }
                }

                allBagRules.Add(newBag);
            }
        }
    }
}
