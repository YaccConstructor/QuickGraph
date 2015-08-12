using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Using;
using System;
using System.Collections.Generic;
using Microsoft.Pex.Framework.Validation;
using Microsoft.Pex.Framework.Wizard;
using System.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace QuickGraph.Collections
{
    public static class BinaryHeapFactory
    {
        [PexFactoryMethod(typeof(BinaryHeap<int, int>))]
        public static BinaryHeap<int, int> Create(int capacity)
        {
            var heap = new BinaryHeap<int, int>(capacity, (i, j) => i.CompareTo(j));
            return heap;
        }
    }

    /// <summary>
    /// This class contains parameterized unit tests for BinaryHeap`2
    /// </summary>
    [TestClass]
    [PexClass(typeof(BinaryHeap<,>))]
    [PexGenericArguments(typeof(int), typeof(int))]
    [PexAllowedContractRequiresFailureAtTypeUnderTestSurface]
    public partial class BinaryHeapTPriorityTValueTest
    {
        /// <summary>
        /// Checks heap invariant
        /// </summary>
        private static void AssertInvariant<TPriority, TValue>(
            BinaryHeap<TPriority, TValue> target
            )
        {
            Assert.IsTrue(target.Capacity >= 0);
            Assert.IsTrue(target.Count >= 0);
            Assert.IsTrue(target.Count <= target.Capacity);
        }

        [TestMethod]
        public void Constructor()
        {
            var target = new BinaryHeap<int, int>();
            AssertInvariant<int, int>(target);
        }

        [PexMethod]
        [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentNullException))]
        [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentOutOfRangeException))]
        public void Constructor<TPriority, TValue>(int capacity)
        {
            var target = new BinaryHeap<TPriority, TValue>(capacity, Comparer<TPriority>.Default.Compare);
            Assert.AreEqual(target.Capacity, capacity);
            AssertInvariant<TPriority, TValue>(target);
        }

        [PexMethod]
        [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
        public void Operations<TPriority, TValue>(
            [PexAssumeUnderTest]BinaryHeap<TPriority, TValue> target,
            [PexAssumeNotNull]KeyValuePair<bool, TPriority>[] values)
        {
            foreach (var value in values)
            {
                if (value.Key)
                    target.Add(value.Value, default(TValue));
                else
                {
                    var min = target.RemoveMinimum();
                }
                AssertInvariant<TPriority, TValue>(target);
            }
        }

        [PexMethod(MaxRuns = 20)]
        public void Insert<TPriority, TValue>(
            [PexAssumeUnderTest]BinaryHeap<TPriority, TValue> target,
            [PexAssumeNotNull] KeyValuePair<TPriority, TValue>[] kvs)
        {
            var count = target.Count;
            foreach (var kv in kvs)
            {
                target.Add(kv.Key, kv.Value);
                AssertInvariant<TPriority, TValue>(target);
            }
            Assert.IsTrue(count + kvs.Length == target.Count);
        }

        [PexMethod(MaxRuns = 20)]
        public void InsertAndIndexOf<TPriority, TValue>(
            [PexAssumeUnderTest]BinaryHeap<TPriority, TValue> target,
            [PexAssumeNotNull] KeyValuePair<TPriority, TValue>[] kvs)
        {
            foreach (var kv in kvs)
                target.Add(kv.Key, kv.Value);
            foreach (var kv in kvs)
                Assert.IsTrue(target.IndexOf(kv.Value) > -1, "target.IndexOf(kv.Value) > -1");
        }

        [PexMethod(MaxRuns = 20)]
        [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
        [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentOutOfRangeException))]
        public void InsertAndRemoveAt<TPriority, TValue>(
            [PexAssumeUnderTest]BinaryHeap<TPriority, TValue> target,
            [PexAssumeNotNull] KeyValuePair<TPriority, TValue>[] kvs,
            int removeAtIndex)
        {
            foreach (var kv in kvs)
                target.Add(kv.Key, kv.Value);
            var count = target.Count;
            var removed = target.RemoveAt(removeAtIndex);
            Assert.AreEqual(count - 1, target.Count);
            AssertInvariant<TPriority, TValue>(target);
        }

        [PexMethod(MaxRuns = 20)]
        public void InsertAndEnumerate<TPriority, TValue>(
            [PexAssumeUnderTest]BinaryHeap<TPriority, TValue> target,
            [PexAssumeNotNull] KeyValuePair<TPriority, TValue>[] kvs)
        {
            var dic = new Dictionary<TPriority, TValue>();
            foreach (var kv in kvs)
            {
                target.Add(kv.Key, kv.Value);
                dic[kv.Key] = kv.Value;
            }
            PexAssert.TrueForAll(target, kv => dic.ContainsKey(kv.Key));
        }

        [PexMethod(MaxRuns = 100)]
        [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
        public void InsertAndRemoveMinimum<TPriority, TValue>(
            [PexAssumeUnderTest]BinaryHeap<TPriority, TValue> target,
            [PexAssumeNotNull] KeyValuePair<TPriority, TValue>[] kvs)
        {
            var count = target.Count;
            foreach (var kv in kvs)
                target.Add(kv.Key, kv.Value);

            TPriority minimum = default(TPriority);
            for (int i = 0; i < kvs.Length; ++i)
            {
                if (i == 0)
                    minimum = target.RemoveMinimum().Key;
                else
                {
                    var m = target.RemoveMinimum().Key;
                    Assert.IsTrue(target.PriorityComparison(minimum, m) <= 0);
                    minimum = m;
                }
                AssertInvariant(target);
            }

            Assert.AreEqual(0, target.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void RemoveMinimumOnEmpty()
        {
            new BinaryHeap<int, int>().RemoveMinimum();
        }

        [PexMethod(MaxRuns = 40)]
        [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
        public void InsertAndMinimum<TPriority, TValue>(
            [PexAssumeUnderTest]BinaryHeap<TPriority, TValue> target,
            [PexAssumeNotNull] KeyValuePair<TPriority, TValue>[] kvs)
        {
            PexAssume.IsTrue(kvs.Length > 0);

            var count = target.Count;
            TPriority minimum = default(TPriority);
            for (int i = 0; i < kvs.Length; ++i)
            {
                var kv = kvs[i];
                if (i == 0)
                    minimum = kv.Key;
                else
                    minimum = target.PriorityComparison(kv.Key, minimum) < 0 ? kv.Key : minimum;
                target.Add(kv.Key, kv.Value);
                // check minimum
                var kvMin = target.Minimum();
                Assert.AreEqual(minimum, kvMin.Key);
            }
            AssertInvariant<TPriority, TValue>(target);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void MinimumOnEmpty()
        {
            new BinaryHeap<int, int>().Minimum();
        }
    }

    [TestClass]
    [PexClass(typeof(BinaryHeap<,>))]
    [PexGenericArguments(typeof(int), typeof(int))]
    public partial class BinaryHeapTPriorityTValueEnumeratorTest
    {
        [PexMethod(MaxRuns = 20)]
        public void InsertManyAndEnumerateUntyped<TPriority, TValue>(
            [PexAssumeUnderTest]BinaryHeap<TPriority, TValue> target,
            [PexAssumeNotNull] KeyValuePair<TPriority, TValue>[] kvs)
        {
            foreach (var kv in kvs)
                target.Add(kv.Key, kv.Value);
            foreach (KeyValuePair<TPriority, TValue> kv in (IEnumerable)target) ;
        }

        [PexMethod(MaxRuns = 20)]
        public void InsertManyAndDoubleForEach<TPriority, TValue>(
            [PexAssumeUnderTest]BinaryHeap<TPriority, TValue> target,
            [PexAssumeNotNull] KeyValuePair<TPriority, TValue>[] kvs)
        {
            foreach (var kv in kvs)
                target.Add(kv.Key, kv.Value);
            PexEnumerablePatterns.DoubleForEach(target);
        }

        [PexMethod(MaxRuns = 20)]
        public void InsertManyAndMoveNextAndReset<TPriority, TValue>(
            [PexAssumeUnderTest]BinaryHeap<TPriority, TValue> target,
            [PexAssumeNotNull] KeyValuePair<TPriority, TValue>[] kvs)
        {
            foreach (var kv in kvs)
                target.Add(kv.Key, kv.Value);
            PexEnumerablePatterns.MoveNextAndReset(target);
        }

        [PexMethod(MaxRuns = 20)]
        public void InsertAndMoveNextAndModify<TPriority, TValue>(
            [PexAssumeUnderTest]BinaryHeap<TPriority, TValue> target,
            KeyValuePair<TPriority, TValue> kv)
        {
            target.Add(kv.Key, kv.Value);
            PexAssert.Throws<InvalidOperationException>(delegate
            {
                var enumerator = target.GetEnumerator();
                target.Add(kv.Key, kv.Value);
                enumerator.MoveNext();
            });
        }

        [PexMethod(MaxRuns = 20)]
        public void InsertAndResetAndModify<TPriority, TValue>(
            [PexAssumeUnderTest]BinaryHeap<TPriority, TValue> target,
            KeyValuePair<TPriority, TValue> kv)
        {
            target.Add(kv.Key, kv.Value);
            PexAssert.Throws<InvalidOperationException>(delegate
            {
                var enumerator = target.GetEnumerator();
                target.Add(kv.Key, kv.Value);
                enumerator.Reset();
            });
        }

        [PexMethod(MaxRuns = 20)]
        public void InsertAndCurrentAndModify<TPriority, TValue>(
            [PexAssumeUnderTest]BinaryHeap<TPriority, TValue> target,
            KeyValuePair<TPriority, TValue> kv)
        {
            target.Add(kv.Key, kv.Value);
            PexAssert.Throws<InvalidOperationException>(delegate
            {
                var enumerator = target.GetEnumerator();
                target.Add(kv.Key, kv.Value);
                var current = enumerator.Current;
            });
        }

        [PexMethod(MaxRuns = 20)]
        public void CurrentAfterMoveNextFinished<TPriority, TValue>(
            [PexAssumeUnderTest]BinaryHeap<TPriority, TValue> target,
            KeyValuePair<TPriority, TValue> kv)
        {
            target.Add(kv.Key, kv.Value);
            PexAssert.Throws<InvalidOperationException>(delegate
            {
                var enumerator = target.GetEnumerator();
                while (enumerator.MoveNext()) ;
                var current = enumerator.Current;
            });
        }

        [PexMethod(MaxRuns = 20)]
        public void CurrentBeforeMoveNext<TPriority, TValue>(
            [PexAssumeUnderTest]BinaryHeap<TPriority, TValue> target,
            KeyValuePair<TPriority, TValue> kv)
        {
            target.Add(kv.Key, kv.Value);
            PexAssert.Throws<InvalidOperationException>(delegate
            {
                var enumerator = target.GetEnumerator();
                var current = enumerator.Current;
            });
        }
    }
}
