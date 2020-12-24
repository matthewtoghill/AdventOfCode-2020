using System;

namespace AoCHelpers
{
    public static class NumericExtensions
    {
        public static bool IsBetween<T>(this T value, T min, T max) where T : IComparable<T>
        {
            return min.CompareTo(value) <= 0 && value.CompareTo(max) <= 0;
        }

        public static int ManhattanDistance(this (int x, int y) startPos, (int x, int y) endPos)
        {
            return Math.Abs(startPos.x - endPos.x) + Math.Abs(startPos.y - endPos.y);
        }
    }
}
