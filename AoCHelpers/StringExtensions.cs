using System;
using System.Text;

namespace AoCHelpers
{
    public static class StringExtensions
    {
        public static string Left(this string input, int length)
        {
            return input != null && input.Length > length ? input.Substring(0, length) : input;
        }

        public static string Right(this string input, int length)
        {
            return input != null && input.Length > length ? input.Substring(input.Length - length) : input;
        }

        public static string StripOut(this string input, char character)
        {
            return input.Replace(character.ToString(), "");
        }

        public static string StripOut(this string input, params char[] chars)
        {
            foreach (char c in chars)
            {
                input = input.Replace(c.ToString(), "");
            }
            return input;
        }

        public static string StripOut(this string input, string subString)
        {
            return input.Replace(subString, "");
        }

        public static string SortString(string input)
        {
            char[] chars = input.ToCharArray();
            Array.Sort(chars);
            return new string(chars);
        }

        public static void PrintAllLines(this string[] lines, bool includeLineNums = false)
        {
            if (includeLineNums)
            {
                int count = lines.Length;
                string formatString = " {0," + count.ToString().Length + "}: {1}";
                for (int i = 0; i < count; i++)
                    Console.WriteLine(formatString, i, lines[i]);
            }
            else
            {
                foreach (var line in lines)
                    Console.WriteLine(line);
            }
        }

        public static string ReplaceAtIndex(this string text, int index, char c)
        {
            StringBuilder sb = new StringBuilder(text);
            sb[index] = c;
            return sb.ToString();
        }
    }
}
