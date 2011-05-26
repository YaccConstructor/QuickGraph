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

        public static void AssertInvariants<TPriority, TValue>(this BinaryHeap<TPriority, TValue> target)
        {
            Assert.IsTrue(target.Capacity >= 0);
            Assert.IsTrue(target.Count >= 0);
            Assert.IsTrue(target.Count <= target.Capacity);
            Assert.IsTrue(target.IsConsistent());
        }

        public static BinaryHeap<int, int> ExampleHeap01()
        {
            var heap = BinaryHeapFactory.Create(20);
            heap.Add(1, 0);
            heap.Add(2, 1);
            heap.Add(1, 2);
            heap.Add(2, 3);
            heap.Add(2, 4);
            heap.Add(1, 5);
            heap.Add(1, 6);
            heap.Add(2, 7);
            heap.Add(2, 8);
            heap.Add(2, 9);
            heap.Add(2, 10);
            heap.Add(1, 11);
            heap.Add(1, 12);
            heap.Add(1, 13);
            heap.Add(1, 14);
            var str = "True: 1 0, 2 1, 1 2, 2 3, 2 4, 1 5, 1 6, 2 7, 2 8, 2 9, 2 10, 1 11, 1 12, 1 13, 1 14, null, null, null, null, null, ";
            Assert.AreEqual(str, heap.ToString2());
            Assert.AreEqual(15, heap.Count);
            heap.AssertInvariants();
            return heap;
        }

        public static BinaryHeap<int, int> ExampleHeapFromTopologicalSortOfDCT8()
        {
            var heap = BinaryHeapFactory.Create(20);
            heap.Add(0, 255);
            heap.Add(0, 256);
            heap.Add(0, 257);
            heap.Add(0, 258);
            heap.Add(0, 259);
            heap.Add(0, 260);
            heap.Add(0, 261);
            heap.Add(0, 262);
            heap.Add(2, 263);
            heap.Add(2, 264);
            heap.Add(2, 265);
            heap.Add(2, 266);
            heap.Add(2, 267);
            heap.Add(2, 268);
            heap.Add(2, 269);
            heap.Add(2, 270);
            heap.Add(1, 271);
            heap.Add(1, 272);
            heap.Add(1, 273);
            heap.Add(1, 274);
            heap.Add(1, 275);
            heap.Add(1, 276);
            heap.Add(1, 277);
            heap.Add(1, 278);
            heap.Add(2, 279);
            heap.Add(2, 280);
            heap.Add(1, 281);
            heap.Add(1, 282);
            heap.Add(1, 283);
            heap.Add(1, 284);
            heap.Add(2, 285);
            heap.Add(2, 286);
            heap.Add(2, 287);
            heap.Add(2, 288);
            heap.Add(1, 289);
            heap.Add(1, 290);
            heap.Add(1, 291);
            heap.Add(1, 292);
            heap.Add(1, 293);
            heap.Add(1, 294);
            heap.Add(1, 295);
            heap.Add(1, 296);
            heap.Add(1, 297);
            heap.Add(1, 298);
            heap.Add(1, 299);
            heap.Add(2, 300);
            heap.Add(2, 301);
            heap.Add(2, 302);
            heap.Add(2, 303);
            heap.Add(1, 304);
            heap.Add(1, 305);
            heap.Add(1, 306);
            heap.Add(1, 307);
            heap.Add(2, 308);
            heap.Add(2, 309);
            heap.Add(2, 310);
            heap.Add(1, 311);
            heap.Add(2, 312);
            heap.Add(2, 313);
            heap.Add(2, 314);
            heap.Add(1, 315);
            heap.Add(1, 316);
            heap.Add(1, 317);
            heap.Add(1, 318);
            heap.Add(2, 319);
            heap.Add(2, 320);
            heap.Add(2, 321);
            heap.Add(2, 322);
            heap.Add(2, 323);
            heap.Add(2, 324);
            heap.Add(1, 325);
            heap.Add(2, 326);
            heap.Add(2, 327);
            heap.Add(2, 328);
            heap.Add(2, 329);
            heap.Add(1, 330);
            heap.Add(1, 331);
            heap.Add(1, 332);
            heap.Add(1, 333);
            heap.Add(0, 334);
            heap.Add(0, 335);
            heap.Add(0, 336);
            heap.Add(0, 337);
            heap.Add(0, 338);
            heap.AssertInvariants();
            return heap;
        }
    }

    [TestClass, PexClass]
    public partial class BinaryHeapTest
    {
        [TestMethod]
        public void UpdateTest()
        {
            var heap = BinaryHeapFactory.ExampleHeap01();
            heap.Update(1, 4);
            Assert.AreEqual(15, heap.Count);
            Console.WriteLine(heap.ToStringTree());
            heap.AssertInvariants();
        }

        [TestMethod]
        public void UpdateTestUsingDCT8()
        {
            var heap = BinaryHeapFactory.ExampleHeapFromTopologicalSortOfDCT8();
            heap.Update(1, 320);
            Console.WriteLine(heap.ToStringTree());
            heap.AssertInvariants();
        }

        [TestMethod]
        public void RemoveAtTest()
        {
            var heap = BinaryHeapFactory.ExampleHeap01();
            heap.RemoveAt(4);
            Assert.AreEqual(14, heap.Count);
            Console.WriteLine(heap.ToStringTree());
            heap.AssertInvariants();
        }

        [TestMethod]
        public void RemoveAtTestUsingDCT8()
        {
            var heap = BinaryHeapFactory.ExampleHeapFromTopologicalSortOfDCT8();
            heap.RemoveAt(66);
            Console.WriteLine(heap.ToStringTree());
            heap.AssertInvariants();
        }

        [TestMethod]
        public void RemoveMinimumTest()
        {
            var heap = BinaryHeapFactory.ExampleHeap01();
            heap.RemoveMinimum();
            Assert.AreEqual(14, heap.Count);
            Console.WriteLine(heap.ToStringTree());
            heap.AssertInvariants();
        }

        [TestMethod]
        public void RemoveMinimumTestUsingDCT8()
        {
            var heap = BinaryHeapFactory.ExampleHeapFromTopologicalSortOfDCT8();
            heap.RemoveMinimum();
            Console.WriteLine(heap.ToStringTree());
            heap.AssertInvariants();
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
