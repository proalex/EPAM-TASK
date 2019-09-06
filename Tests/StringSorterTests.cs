using System;
using System.Collections.Generic;
using NUnit.Framework;
using Task;

namespace Tests
{
    [TestFixture]
    public class StringSorterTests
    {
        [Test]
        public void QSort_SomeStrings_SortedStrings()
        {
            List<string> strings = new List<string>()
            {
                "asdasdcb",
                "qweqweab",
                "dfsffdda"
            };

            List<string> expected = new List<string>()
            {
                "qweqweab",
                "asdasdcb",
                "dfsffdda"
            };

            CollectionAssert.AreEqual(expected, StringSorter.QSort(strings));
        }

        [Test]
        public void ListSort_SomeStrings_SortedStrings()
        {
            List<string> strings = new List<string>()
            {
                "asdasdcb",
                "qweqweab",
                "dfsffdda"
            };

            List<string> expected = new List<string>()
            {
                "qweqweab",
                "asdasdcb",
                "dfsffdda"
            };

            CollectionAssert.AreEqual(expected, StringSorter.ListSort(strings));
        }

        [Test]
        public void LinqSort_SomeStrings_SortedStrings()
        {
            List<string> strings = new List<string>()
            {
                "asdasdcb",
                "qweqweab",
                "dfsffdda"
            };

            List<string> expected = new List<string>()
            {
                "qweqweab",
                "asdasdcb",
                "dfsffdda"
            };

            CollectionAssert.AreEqual(expected, StringSorter.LinqSort(strings));
        }

        [Test]
        public void LinqSort_NullList_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => StringSorter.LinqSort(null));
        }

        [Test]
        public void ListSort_NullList_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => StringSorter.ListSort(null));
        }

        [Test]
        public void QSort_NullList_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => StringSorter.QSort(null));
        }

        [Test]
        public void LinqSort_ListWithInvalidString_IndexOutOfRangeException()
        {
            List<string> expected = new List<string>()
            {
                "qweqweab",
                "a",
                "dfsffdda"
            };

            Assert.Throws<IndexOutOfRangeException>(() => StringSorter.LinqSort(expected));
        }

        [Test]
        public void ListSort_ListWithInvalidString_IndexOutOfRangeException()
        {
            List<string> expected = new List<string>()
            {
                "qweqweab",
                "a",
                "dfsffdda"
            };

            Assert.Throws<InvalidOperationException>(() => StringSorter.ListSort(expected));
        }

        [Test]
        public void QSort_ListWithInvalidString_IndexOutOfRangeException()
        {
            List<string> expected = new List<string>()
            {
                "qweqweab",
                "a",
                "dfsffdda"
            };

            Assert.Throws<ArgumentException>(() => StringSorter.QSort(expected));
        }
    }
}
