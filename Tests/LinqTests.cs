using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using Task;

namespace Tests
{
    [TestFixture]
    public class LinqTests
    {
        private readonly Random _random = new Random();

        [TestCase(1000000, 1000000, 1000000 / 2)]
        [TestCase(1000000, 1000000, 0)]
        [TestCase(1000000, 1000000, 1000000)]
        [TestCase(1000000, 1000000)]
        [TestCase(0, 0)]
        [TestCase(1, 1)]
        public void InANotInB_RandomLists_CorrectResult(int firstSize, int secondSize, int? maxDistance = null)
        {
            var firstArray = new int[firstSize];
            var secondArray = new int[secondSize];
            int offset = _random.Next();

            int secondOffset = maxDistance != null ?
                _random.Next(offset - maxDistance.Value, offset + maxDistance.Value + 1) :
                _random.Next();

            // Generating input lists.
            Parallel.Invoke(
                () => Parallel.For(0, firstSize, (i) => firstArray[i] = i + offset),
                () => Parallel.For(0, secondSize, (i) => secondArray[i] = i + secondOffset));

            // Shuffling them using modern version of Fisher–Yates algorithm.
            for (int i = firstArray.Length - 1; i >= 0; i--)
            {
                int j = _random.Next(i + 1);
                var temp = firstArray[i];

                firstArray[i] = firstArray[j];
                firstArray[j] = temp;
            }

            for (int i = secondArray.Length - 1; i >= 0; i--)
            {
                int j = _random.Next(i + 1);
                var temp = secondArray[i];

                secondArray[i] = secondArray[j];
                secondArray[j] = temp;
            }

            // OrderBy is used to reduce CollectionAssert complexity from O(n^2) to O(n).
            // Otherwise, CollectionAssert.AreEquivalent would be used which is very slow.
            var result = Linq.InANotInB(new List<int>(firstArray), new List<int>(secondArray)).OrderBy(n => n);
            var expected = firstArray.AsParallel().Except(secondArray.AsParallel()).OrderBy(n => n);

            CollectionAssert.AreEqual(expected, result);
        }

        [Test]
        public void InANotInB_FirstListNull_ArgumentNullException()
        {
            List<int> second = new List<int>(0);

            Assert.Throws<ArgumentNullException>(() => Linq.InANotInB(null, second));
        }

        [Test]
        public void InANotInB_SecondListNull_ArgumentNullException()
        {
            List<int> first = new List<int>(0);

            Assert.Throws<ArgumentNullException>(() => Linq.InANotInB(first, null));
        }
    }
}
