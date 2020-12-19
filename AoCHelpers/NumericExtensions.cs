using System;

namespace AoCHelpers
{
    public static class NumericExtensions
    {
        public static bool IsBetween<T>(this T value, T min, T max) where T : IComparable<T>
        {
            return min.CompareTo(value) <= 0 && value.CompareTo(max) <= 0;
        }
    }
}
