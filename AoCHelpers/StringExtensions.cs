using System;

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
    }
}
