using System;
using System.Collections.Generic;
using System.Linq;

namespace Task
{
    /// <summary>
    ///     Sorts strings by the second last character.
    /// </summary>
    public static class StringSorter
    {
        /// <summary>
        ///     Sorts strings by the second last character using quick sort algorithm.
        /// </summary>
        /// <param name="strings"> List of strings. </param>
        /// <returns> Sorted list. </returns>
        /// <exception cref="ArgumentNullException"> Throws when strings is null. </exception>
        /// <exception cref="ArgumentException"> Throws when string in list is less than 2 characters. </exception>
        public static List<string> QSort(List<string> strings)
        {
            if (strings == null)
            {
                throw new ArgumentNullException(nameof(strings));
            }

            var array = strings.ToArray();

            QuickSort.Sort(ref array, 0, array.Length - 1, new StringComparer());
            return new List<string>(array);
        }

        /// <summary>
        ///     Sorts strings by the second last character using List.Sort method.
        /// </summary>
        /// <param name="strings"> List of strings. </param>
        /// <returns> Sorted list. </returns>
        /// <exception cref="ArgumentNullException"> Throws when strings is null. </exception>
        /// <exception cref="InvalidOperationException"> Throws when string in list is less than 2 characters. </exception>
        public static List<string> ListSort(List<string> strings)
        {
            if (strings == null)
            {
                throw new ArgumentNullException(nameof(strings));
            }

            strings.Sort(new StringComparer());
            return strings;
        }

        /// <summary>
        ///     Sorts strings by the second last character using Linq.
        /// </summary>
        /// <param name="strings"> List of strings. </param>
        /// <returns> Sorted list. </returns>
        /// <exception cref="ArgumentNullException"> Throws when strings is null. </exception>
        /// <exception cref="IndexOutOfRangeException"> Throws when string in list is less than 2 characters. </exception>
        public static List<string> LinqSort(List<string> strings)
        {
            if (strings == null)
            {
                throw new ArgumentNullException(nameof(strings));
            }

            return new List<string>(strings.OrderBy(n => n[n.Length - 2]));
        }
    }
}
