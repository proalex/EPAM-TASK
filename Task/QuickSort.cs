using System.Collections.Generic;

namespace Task
{
    /// <summary>
    ///     Represents quick sort algorithm.
    /// </summary>
    public static class QuickSort
    {
        /// <summary>
        ///     Sorts the array with quick sort algorithm.
        /// </summary>
        /// <typeparam name="T"> The type of elements. </typeparam>
        /// <param name="array"> An array to sort. </param>
        /// <param name="lowIndex"> Start index. </param>
        /// <param name="highIndex"> End index. </param>
        /// <param name="comparer"> The comparer used for sorting algorithm. </param>
        public static void Sort<T>(ref T[] array, int lowIndex, int highIndex, IComparer<T> comparer)
        {
            if (lowIndex < highIndex)
            {
                int pivot = Partition(ref array, lowIndex, highIndex, comparer);

                Sort(ref array, lowIndex, pivot, comparer);
                Sort(ref array, pivot + 1, highIndex, comparer);
            }
        }

        /// <summary>
        ///     The partition operation for quick sort algorithm.
        /// </summary>
        /// <typeparam name="T"> The type of elements. </typeparam>
        /// <param name="array"> An array to sort. </param>
        /// <param name="lowIndex"> Start index. </param>
        /// <param name="highIndex"> End index. </param>
        /// <param name="comparer"> The comparer used for the partition operation. </param>
        /// <returns> The index of first new pivot. </returns>
        private static int Partition<T>(ref T[] array, int lowIndex, int highIndex, IComparer<T> comparer)
        {
            var pivot = array[lowIndex];

            lowIndex--;
            highIndex++;

            while (true)
            {
                do
                {
                    lowIndex++;
                } while (lowIndex < array.Length && comparer.Compare(array[lowIndex], pivot) < 0);

                do
                {
                    highIndex--;
                } while (highIndex >= 0 && comparer.Compare(array[highIndex], pivot) > 0);

                if (lowIndex >= highIndex)
                {
                    return highIndex;
                }

                Swap(ref array[lowIndex], ref array[highIndex]);
            }
        }

        /// <summary>
        ///     Swaps two elements.
        /// </summary>
        /// <typeparam name="T"> The type of elements. </typeparam>
        /// <param name="first"> The first element. </param>
        /// <param name="second"> The second element. </param>
        private static void Swap<T>(ref T first, ref T second)
        {
            var temp = first;

            first = second;
            second = temp;
        }
    }
}
