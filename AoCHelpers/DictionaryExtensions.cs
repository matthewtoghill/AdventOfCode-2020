using System.Collections.Generic;

namespace AoCHelpers
{
    public static class DictionaryExtensions
    {
        public static void IncrementAt<T>(this Dictionary<T, int> dictionary, T index)
        {
            dictionary.TryGetValue(index, out int count);
            dictionary[index] = ++count;
        }

        public static void IncrementAt<T>(this Dictionary<T, long> dictionary, T index)
        {
            dictionary.TryGetValue(index, out long count);
            dictionary[index] = ++count;
        }

        public static void IncrementAt<T>(this Dictionary<T, int> dictionary, T index, int incrementAmount)
        {
            dictionary.TryGetValue(index, out int count);
            dictionary[index] = count + incrementAmount;
        }

        public static void IncrementAt<T>(this Dictionary<T, long> dictionary, T index, long incrementAmount)
        {
            dictionary.TryGetValue(index, out long count);
            dictionary[index] = count + incrementAmount;
        }
    }
}
