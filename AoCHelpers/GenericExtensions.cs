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

        public static int IndexOf<T>(this T[] array, T value)
        {
            return Array.IndexOf(array, value);
        }

        public static int FindIndex<T>(this T[] array, Predicate<T> match)
        {
            return Array.FindIndex(array, match);
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

        public static IEnumerable<IEnumerable<T>> GetPermutations<T>(this IEnumerable<T> list)
        {
            if (list.Count() > 1)
                return list.SelectMany(
                     item => GetPermutations(list.Where(i => !i.Equals(item))),
                     (item, permutation) => new[] { item }.Concat(permutation));

            return new[] { list };
        }

        public static IEnumerable<IEnumerable<T>> GetPermutations<T>(this IEnumerable<T> list, int length)
        {
            if (length == 1) return list.Select(t => new T[] { t });

            return list.GetPermutations(length - 1)
                .SelectMany(t => list.Where(e => !t.Contains(e)),
                    (t1, t2) => t1.Concat(new T[] { t2 }));
        }

        public static IEnumerable<IEnumerable<T>> GetPowerSet<T>(this List<T> list)
        {
            return from m in Enumerable.Range(0, 1 << list.Count)
                   select
                        from i in Enumerable.Range(0, list.Count)
                        where (m & (1 << i)) != 0
                        select list[i];
        }
    }
}
