using AoCHelpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Day16
{
    public class FieldRule
    {
        public string RuleName { get; }
        public int LowerRangeStart { get; }
        public int LowerRangeEnd { get; }
        public int UpperRangeStart { get; }
        public int UpperRangeEnd { get; }
        public List<int> PossibleColumns { get; set; }
        public bool ColumnFound { get; set; }

        public FieldRule(string ruleName, int lowerRangeStart, int lowerRangeEnd, int upperRangeStart, int upperRangeEnd, int columnCount)
        {
            RuleName = ruleName;
            LowerRangeStart = lowerRangeStart;
            LowerRangeEnd = lowerRangeEnd;
            UpperRangeStart = upperRangeStart;
            UpperRangeEnd = upperRangeEnd;
            ColumnFound = false;
            PossibleColumns = Enumerable.Range(1, columnCount).ToList();
        }

        public void CheckNumMeetsRule(int num, int col)
        {
            if (!(num.IsBetween(LowerRangeStart, LowerRangeEnd) || num.IsBetween(UpperRangeStart, UpperRangeEnd)))
            {
                PossibleColumns.Remove(col);
            }
        }

        public void PrintPossibleColumns()
        {
            Console.WriteLine($"\n{RuleName}:");
            string output = "";
            foreach (var val in PossibleColumns)
            {
                output += val + ",";
            }
            output = output.Left(output.Length - 1);
            Console.WriteLine(output);
        }
    }
}
