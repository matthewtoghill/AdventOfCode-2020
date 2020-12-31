using AoCHelpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day16
{
    class Program
    {
        private static readonly string[] input = File.ReadAllText(@"..\..\..\data\day16.txt").Split(new string[] { "\r\n\r\n", "\n\n" }, StringSplitOptions.RemoveEmptyEntries);
        static void Main(string[] args)
        {
            BothParts();
            Console.ReadLine();
        }

        private static void BothParts()
        {
            List<FieldRule> rules = new List<FieldRule>();
            List<int> invalidVals = new List<int>();
            List<List<int>> validTickets = new List<List<int>>();
            List<int> ourTicket = new List<int>();

            int groupCounter = 0;
            foreach (var group in input)
            {
                switch (groupCounter)
                {
                    case 0:
                        foreach (var line in group.Split('\n'))
                        {
                            // Parse list of rules
                            // rule name[:] first range [or] second range
                            // could split on ':' and 'or'
                            // create a list of the rules, each rule is recorded twice and has a range start and range end value
                            string ruleName = line.LeftOf(':');
                            string firstRange = line.GetBetween(": ", " or");
                            string secondRange = line.RightOfLast(" ");

                            rules.Add(new FieldRule(ruleName, int.Parse(firstRange.LeftOf('-')), int.Parse(firstRange.RightOf('-')),
                                                    int.Parse(secondRange.LeftOf('-')), int.Parse(secondRange.RightOf('-')), 20));
                        }
                        break;

                    case 1:
                        ourTicket = group.Split('\n')[1].Split(',').Select(int.Parse).ToList();
                        break;

                    case 2:
                        // Check all lines after the 'nearby tickets:' line
                        // on each line, split by ',' to get the separate values
                        // check if each number meets a rule or not 
                        // if the rule is not met, add the number to a list of invalid fields
                        foreach (var line in group.Split('\n').Skip(1))
                        {
                            List<int> ticket = line.Split(',').Select(int.Parse).ToList();
                            bool isValidTicket = true;
                            foreach (var num in ticket)
                            {
                                if (!IsAnyRuleMet(num, ref rules))
                                {
                                    invalidVals.Add(num);
                                    isValidTicket = false;
                                }
                            }

                            if (isValidTicket) validTickets.Add(ticket);
                        }
                        break;

                    default:
                        break;
                }
                groupCounter++;
            }

            // Check each field value in each valid ticket
            // Pass each value into the CheckNumMeetsRule method of the rule
            foreach (var ticket in validTickets)
            {
                int col = 0;
                foreach (var field in ticket)
                {
                    col++;
                    foreach (var rule in rules)
                    {
                        rule.CheckNumMeetsRule(field, col);
                    }
                }
            }

            // Iterate through the rules
            // find the first rule where the column found property is false and the possible columns list only has 1 value
            // Then remove that value from all other rules
            // Repeat until all rules have only a single possible column value
            while (rules.Count(r => r.ColumnFound == true) < rules.Count)
            {
                int countWithMultiple = rules.Count(r => r.PossibleColumns.Count > 1);

                FieldRule nextToMatch = rules.Find(r => r.ColumnFound == false && r.PossibleColumns.Count == 1);

                foreach (var rule in rules)
                {
                    if (rule.RuleName != nextToMatch.RuleName) rule.PossibleColumns.Remove(nextToMatch.PossibleColumns[0]);
                }

                nextToMatch.ColumnFound = true;
            }

            // Part 1: Sum the list of invalid fields
            int sumOfInvalid = invalidVals.Sum(v => v);
            Console.WriteLine($"Part 1: {sumOfInvalid}");

            // Part 2: Multiply the values together from our ticket where the values are in the 
            //         fields with names starting "departure"
            List<int> departureFieldColumns = rules.Where(r => r.RuleName.Contains("departure")).Select(r => r.PossibleColumns[0]).ToList();
            long departureValProduct = 1;
            foreach (var col in departureFieldColumns)
            {
                departureValProduct *= ourTicket[col - 1];
            }

            Console.WriteLine($"\nPart 2: {departureValProduct}");
        }

        private static bool IsAnyRuleMet(int num, ref List<FieldRule> rules)
        {
            foreach (var rule in rules)
            {
                if (num.IsBetween(rule.LowerRangeStart, rule.LowerRangeEnd) || num.IsBetween(rule.UpperRangeStart, rule.UpperRangeEnd)) return true;
            }
            return false;
        }
    }
}
