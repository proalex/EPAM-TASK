using System;
using System.Collections.Generic;
using System.Linq;

namespace Task
{
    /// <summary>
    ///     Used to find the elements that exists in the first list, but doesn't exist in the second.
    ///     Based on linq queries.
    /// </summary>
    public static class Linq
    {
        /// <summary>
        ///     Find the elements that exists in the first list, but doesn't exist in the second.
        ///     Based on linq queries.
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
        public static List<int> InANotInB(List<int> first, List<int> second)
        {
            if (first == null)
            {
                throw new ArgumentNullException(nameof(first));
            }

            if (second == null)
            {
                throw new ArgumentNullException(nameof(second));
            }

            List<int?> nullableList = new List<int?>(second.Count);

            foreach (var i in second)
                nullableList.Add(i);

            // There's no performance gain for little lists since parallel operations have some cost to start.
            if (first.Count > 500000 || second.Count > 500000)
            {
                var result =
                    from firstElement in first.AsParallel()
                    join secondElement in nullableList.AsParallel()
                        on firstElement equals secondElement.Value into joinResult
                    from joinElement in joinResult.DefaultIfEmpty()
                    where joinElement == null
                    select firstElement;

                return new List<int>(result);
            }
            else
            {
                var result =
                    from firstElement in first
                    join secondElement in nullableList
                        on firstElement equals secondElement.Value into joinResult
                    from joinElement in joinResult.DefaultIfEmpty()
                    where joinElement == null
                    select firstElement;

                return new List<int>(result);
            }
        }
    }
}
