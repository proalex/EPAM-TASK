using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Task
{
    /// <summary>
    ///     Used to find the elements that exists in the first list, but doesn't exist in the second.
    ///     Based on hash table.
    /// </summary>
    public static class HashTable
    {
        /// <summary>
        ///     Find the elements that exists in the first list, but doesn't exist in the second.
        ///     Based on hash table.
        /// </summary>
        /// <typeparam name="T"> The type of elements. </typeparam>
        /// <param name="first"> The first list. </param>
        /// <param name="second"> The second list. </param>
        /// <returns>
        ///     The elements that exists in first list, but doesn't exists in second.
        ///     The order can be different from original first list.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     Throws when first or second list is null.
        /// </exception>
        public static List<T> InANotInB<T>(List<T> first, List<T> second)
        {
            if (first == null)
            {
                throw new ArgumentNullException(nameof(first));
            }

            if (second == null)
            {
                throw new ArgumentNullException(nameof(second));
            }

            List<T> result = new List<T>();
            Dictionary<T, T> hashTable = new Dictionary<T, T>(second.Count);

            foreach (var element in second)
            {
                hashTable.Add(element, element);
            }

            foreach (var element in first)
            {
                if (!hashTable.ContainsKey(element))
                {
                    result.Add(element);
                }
            }

            return result;
        }

        /// <summary>
        ///     Find the elements that exists in the first list, but doesn't exist in the second.
        ///     Based on hash table.
        /// </summary>
        /// <typeparam name="T"> The type of elements. </typeparam>
        /// <param name="first"> The first list. </param>
        /// <param name="second"> The second list. </param>
        /// <returns>
        ///     The elements that exists in first list, but doesn't exists in second.
        ///     The order can be different from original first list.
        /// </returns>
        /// <exception cref="ArgumentNullException"> Throws when first or second list is null. </exception>
        /// <exception cref="ArgumentException"> Throws when hash of some element already exists in hash table. </exception>
        public static List<T> InANotInBParallel<T>(List<T> first, List<T> second)
        {
            if (first == null)
            {
                throw new ArgumentNullException(nameof(first));
            }

            if (second == null)
            {
                throw new ArgumentNullException(nameof(second));
            }

            List<T> result = new List<T>();

            ThreadPool.GetMaxThreads(out int workerThreads, out int completionPortThreads);

            ConcurrentDictionary<T, T> hashTable = 
                new ConcurrentDictionary<T, T>(workerThreads, second.Count);

            Parallel.ForEach(second, (n) =>
            {
                if (!hashTable.TryAdd(n, n))
                {
                    throw new ArgumentException($"Trying to add {n} element which hash already exists in hastTable.", nameof(second));
                }
            });

            foreach (var element in first)
            {
                if (!hashTable.ContainsKey(element))
                {
                    result.Add(element);
                }
            }

            return result;
        }
    }
}
