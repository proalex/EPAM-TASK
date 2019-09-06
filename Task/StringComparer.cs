using System;
using System.Collections.Generic;

namespace Task
{
    /// <summary>
    ///     Sorts strings by the second last character.
    /// </summary>
    public class StringComparer : IComparer<string>
    {
        /// <inheritdoc />
        public int Compare(string x, string y)
        {
            if (x == null)
            {
                throw new ArgumentNullException(nameof(x));
            }

            if (y == null)
            {
                throw new ArgumentNullException(nameof(y));
            }

            if (x.Length < 2)
            {
                throw new ArgumentException($"{nameof(x)} length is less than 2.", nameof(x));
            }

            if (y.Length < 2)
            {
                throw new ArgumentException($"{nameof(y)} length is less than 2.", nameof(y));
            }

            char xChar = x[x.Length - 2];
            char yChar = y[y.Length - 2];

            if (xChar < yChar)
            {
                return -1;
            }
            else if (xChar == yChar)
            {
                return 0;
            }

            return 1;
        }
    }
}
