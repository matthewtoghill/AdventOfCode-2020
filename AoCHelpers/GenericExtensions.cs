using System;
using System.Collections.Generic;
using System.Linq;

namespace AoCHelpers
{
    public static class GenericExtensions
    {
        public static void Swap<T>(ref T firstVal, ref T secondVal)
        {
            T temp = firstVal;
            firstVal = secondVal;
            secondVal = temp;
        }

        public static IEnumerable<IEnumerable<T>> Split<T>(this T[] array, int size)
        {
            for (int i = 0; i < (float)array.Length / size; i++)
            {
                yield return array.Skip(i * size).Take(size);
            }
        }

        public static IEnumerable<IEnumerable<T>> Split<T>(this IEnumerable<T> list, int parts)
        {
            int i = 0;
            var splits = from item in list
                         group item by i++ % parts into part
                         select part.AsEnumerable();
            return splits;
        }

        public static IEnumerable<IEnumerable<T>> Split<T>(this T[] array, Func<T, bool> predicate)
        {
            int skip = 0, pos = 0;
            List<int> splitPositions = new List<int>();

            // Count the number of elements that match the predicate
            foreach (var item in array)
            {
                if (predicate(item)) splitPositions.Add(pos);
                pos++;
            }

            splitPositions.Add(array.Length);

            for (int i = 0; i < splitPositions.Count; i++)
            {
                int take = splitPositions[i] - skip;
                yield return array.Skip(skip).Take(take);
                skip = splitPositions[i] + 1;
            }
        }
    }
}
