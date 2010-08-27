using System;
using System.Collections;
using System.Collections.Generic;

namespace QuickGraph.Unit
{
    public static class CollectionAssert
    {
        public static void DoesNotContainKey(IDictionary dictionary, object key)
        {
            Assert.IsFalse(dictionary.Contains(key),
                "Collection contains {0}", key);
        }

        public static void DoesNotContainKey<K, V>(IDictionary<K, V> dictionary, K value)
        {
            Assert.IsFalse(dictionary.ContainsKey(value),
                "Collection contains {0}", value);
        }

        public static void ContainsKey(IDictionary dictionary, object key)
        {
            Assert.IsTrue(dictionary.Contains(key),
                "Collection does not contain {0}", key);
        }

        public static void ContainsKey<K, V>(IDictionary<K, V> dictionary, K value)
        {
            Assert.IsTrue(dictionary.ContainsKey(value),
                "Collection does not contain {0}", value);
        }

        public static void DoesNotContain(IEnumerable collection, object value)
        {
            foreach(object item in collection)
            {
                Assert.AreNotEqual(item, value,"Collection contains {0}", value);
            }
        }

        public static void DoesNotContain<T>(ICollection<T> collection, T value)
        {
            Assert.IsFalse(collection.Contains(value),
                "Collection contains {0}", value);
        }

        public static void Contains(ICollection collection, object value)
        {
            foreach(object item in collection)
            {
                if (item.Equals(value))
                    return;
            }
            Assert.Fail("Collection does not contains {0}", value);
        }

        public static void Contains<T>(ICollection<T> collection, T value)
        {
            Assert.IsTrue(collection.Contains(value),
                "Collection does not contain {0}",value);
        }

        public static void AreEqual(ICollection left, ICollection right)
        {
            AreCountEqual(left, right);
            AreElementEqual(left, right);
        }

        public static void AreEqual<T>(ICollection<T> left, ICollection<T> right)
        {
            AreCountEqual(left, right);
            AreElementEqual(left, right);
        }

        public static void AreCountEqual(ICollection left, ICollection right)
        {
            Assert.AreEqual(left.Count, right.Count,
                "Count is not equal");
        }

        public static void AreCountEqual<T>(ICollection<T> left, ICollection<T> right)
        {
            Assert.AreEqual(left.Count, right.Count,
                "Count is not equal");
        }

        public static void IsCountEqual(int count, ICollection collection)
        {
            Assert.AreEqual(count, collection.Count,
                "collection.Count ({0}) is not equal to {1}", count, collection.Count);
        }

        public static void IsCountEqual<T>(int count, ICollection<T> collection)
        {
            Assert.AreEqual(count, collection.Count,
                "collection.Count ({0}) is not equal to {1}", count, collection.Count);
        }

        public static void AreElementEqual(IEnumerable left, IEnumerable right)
        {
            IEnumerator leftEnumerator = left.GetEnumerator();
            IEnumerator rightEnumerator = right.GetEnumerator();
            try
            {
                int i = 0;
                bool moveNext;
                do
                {
                    moveNext = leftEnumerator.MoveNext();
                    Assert.AreEqual(moveNext, rightEnumerator.MoveNext(),
                        "Collection have not the same size");
                    if (moveNext)
                    {
                        Assert.AreEqual(leftEnumerator.Current, rightEnumerator.Current,
                            "Element {0} is not equal", i);
                    }
                    i++;
                } while (moveNext);
            }
            finally
            {
                IDisposable disposable = leftEnumerator as IDisposable;
                if (disposable != null)
                    disposable.Dispose();
                disposable = rightEnumerator as IDisposable;
                if (disposable != null)
                    disposable.Dispose();
            }
        }

        public static void AreElementEqual<T>(IEnumerable<T> left, IEnumerable<T> right)
        {
            using (IEnumerator<T> leftEnumerator = left.GetEnumerator())
            using (IEnumerator<T> rightEnumerator = right.GetEnumerator())
            {
                int i=0;
                bool moveNext;
                do
                {
                    moveNext = leftEnumerator.MoveNext();
                    Assert.AreEqual(moveNext, rightEnumerator.MoveNext(),
                        "Collection have not the same size");
                    if (moveNext)
                    {
                        Assert.AreEqual<T>(leftEnumerator.Current, rightEnumerator.Current,
                            "Element {0} is not equal", i);
                    }
                    i++;
                } while (moveNext);
            }
        }
    }
}
