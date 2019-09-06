using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Task
{
    /// <summary>
    ///     Used to find the elements that exists in the first list, but doesn't exist in the second
    ///     with qick sort and merge algorithms.
    /// </summary>
    public static class QSortMerge
    {
        /// <summary>
        ///     Find the elements that exists in the first list, but doesn't exist in the second
        ///     with qick sort and merge algorithms.
        /// </summary>
        /// <typeparam name="T"> The type of elements. </typeparam>
        /// <param name="first"> The first list. </param>
        /// <param name="second"> The second list. </param>
        /// <returns>
        ///     The elements that exists in first list, but doesn't exists in second.
        ///     The order can be different from original first list.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     Throws when first or second list, or comparer is null.
        /// </exception>
        /// <remarks> The algorithm works better for unsorted lists with unique elements. </remarks>
        public static List<T> InANotInB<T>(List<T> first, List<T> second)
        {
            return InANotInB<T>(first, second, Comparer<T>.Default);
        }

        /// <summary>
        ///    Find the elements that exists in the first list, but doesn't exist in the second
        ///    with qick sort and merge algorithms.
        /// </summary>
        /// <typeparam name="T"> The type of elements. </typeparam>
        /// <param name="first"> The first list. </param>
        /// <param name="second"> The second list. </param>
        /// <param name="comparer"> The comparer used for sorting algorithm. </param>
        /// <returns>
        ///     The elements that exists in first list, but doesn't exists in second.
        ///     The order can be different from original first list.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     Throws when first or second list, or comparer is null.
        /// </exception>
        /// <remarks> The algorithm works better for unsorted lists with unique elements. </remarks>
        public static List<T> InANotInB<T>(List<T> first, List<T> second, IComparer<T> comparer)
        {
            if (first == null)
            {
                throw new ArgumentNullException(nameof(first));
            }

            if (second == null)
            {
                throw new ArgumentNullException(nameof(second));
            }

            if (comparer == null)
            {
                throw new ArgumentNullException(nameof(comparer));
            }

            var leftArray = first.ToArray();
            var rightArray = second.ToArray();

            // There's no performance gain for little arrays since parallel operations have some cost to start.
            if (leftArray.Length >= 50000 && rightArray.Length >= 50000)
            {
                Parallel.Invoke(
                    () => QuickSort.Sort(ref leftArray, 0, leftArray.Length - 1, comparer),
                    () => QuickSort.Sort(ref rightArray, 0, rightArray.Length - 1, comparer));
            }
            else
            {
                QuickSort.Sort(ref leftArray, 0, leftArray.Length - 1, comparer);
                QuickSort.Sort(ref rightArray, 0, rightArray.Length - 1, comparer);
            }

            return Merge(leftArray, rightArray, comparer);
        }

        

        /// <summary>
        ///     Finds the elements that exists in the first array but doesn't exist in the second.
        /// </summary>
        /// <typeparam name="T"> Type of elements. </typeparam>
        /// <param name="first"> The first sorted array. </param>
        /// <param name="second"> The second sorted array </param>
        /// <param name="comparer"> The comparer used for the merge algorithm. </param>
        /// <returns> Elements in the first array that desn't exists in the second array. </returns>
        private static List<T> Merge<T>(T[] first, T[] second, IComparer<T> comparer)
        {
            int counter = 0;
            var result = new List<T>();

            foreach (var left in first)
            {
                var right = default(T);

                while (counter < second.Length)
                {
                    right = second[counter];

                    if (comparer.Compare(right, left) >= 0)
                    {
                        break;
                    }

                    counter++;
                }

                if (comparer.Compare(right, left) != 0)
                {
                    result.Add(left);
                }
            }

            return result;
        }
    }
}
