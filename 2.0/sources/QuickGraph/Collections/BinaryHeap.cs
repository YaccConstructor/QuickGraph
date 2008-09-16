using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Collections;

namespace QuickGraph.Collections
{
    /// <summary>
    /// Binary heap
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <remarks>
    /// Indexing rules:
    /// 
    /// parent index: index ¡ 1)/2
    /// left child: 2 * index + 1
    /// right child: 2 * index + 2
    /// 
    /// Reference:
    /// http://dotnetslackers.com/Community/files/folders/data-structures-and-algorithms/entry28722.aspx
    /// </remarks>
    [DebuggerDisplay("Count = {Count}")]
    public class BinaryHeap<TPriority, TValue> :
        IEnumerable<KeyValuePair<TPriority, TValue>>
    {
        readonly Comparison<TPriority> priorityComparsion;
        KeyValuePair<TPriority, TValue>[] items;
        int count;
        int version;

        const int DefaultCapacity = 16;

        public BinaryHeap()
            : this(DefaultCapacity, Comparer<TPriority>.Default.Compare)
        { }

        public BinaryHeap(Comparison<TPriority> priorityComparison)
            : this(DefaultCapacity, priorityComparison)
        { }

        public BinaryHeap(int capacity, Comparison<TPriority> priorityComparison)
        {
            if (capacity < 0)
                throw new ArgumentOutOfRangeException("capacity");
            if (priorityComparison == null)
                throw new ArgumentNullException("priorityComparison");

            this.items = new KeyValuePair<TPriority, TValue>[capacity];
            this.priorityComparsion = priorityComparison;
        }

        public Comparison<TPriority> PriorityComparison
        {
            get { return this.priorityComparsion; }
        }

        public int Capacity
        {
            get { return this.items.Length; }
        }

        public int Count
        {
            get { return this.count; }
        }

        public void Add(TPriority priority, TValue value)
        {
            GraphContracts.Assert(count <= this.items.Length);

            this.version++;
            this.ResizeArray();
            this.items[this.count++] = new KeyValuePair<TPriority, TValue>(priority, value);
            this.MinHeapifyDown(this.count - 1);
        }

        private void MinHeapifyDown(int start)
        {
            int i = start;
            int j = (i - 1) / 2;
            while (i > 0 &&
                this.Less(i, j))
            {
                this.Swap(i, j);
                i = j;
                j = (i - 1) / 2;
            }
        }

        private void ResizeArray()
        {
            if (this.count == this.items.Length)
            {
                var newItems = new KeyValuePair<TPriority, TValue>[this.count * 2 + 1];
                Array.Copy(this.items, newItems, this.count);
                this.items = newItems;
            }
        }

        public KeyValuePair<TPriority, TValue> Minimum()
        {
            if (this.count == 0)
                throw new InvalidOperationException();
            return this.items[0];
        }

        public KeyValuePair<TPriority, TValue> RemoveMinimum()
        {
            // shortcut for heap with 1 element.
            if (this.count == 1)
            {
                this.version++;
                return this.items[--this.count];
            }
            return this.RemoveAt(0);
        }

        public KeyValuePair<TPriority, TValue> RemoveAt(int index)
        {
            if (this.count == 0)
                throw new InvalidOperationException("heap is empty");
            if (index < 0 | index >= this.count | index + this.count < this.count)
                throw new ArgumentOutOfRangeException("index");

            this.version++;
            // shortcut for heap with 1 element.
            if (this.count == 1)
                return this.items[--this.count];

            if (index < this.count - 1)
            {
                this.Swap(index, this.count - 1);
                this.MinHeapifyUp(index);
            }

            return this.items[--this.count];
        }

        private void MinHeapifyUp(int index)
        {
            var left = 2 * index + 1;
            var right = 2 * index + 2;
            while (
                    (left < this.count - 1 && !this.Less(index, left)) ||
                    (right < this.count - 1 && !this.Less(index, right))
                   )
            {
                if (right >= this.count - 1 ||
                    this.Less(left, right))
                {
                    this.Swap(left, index);
                    index = left;
                }
                else
                {
                    this.Swap(right, index);
                    index = right;
                }
                left = 2 * index + 1;
                right = 2 * index + 2;
            }
        }

        public int IndexOf(TValue value)
        {
            for (int i = 0; i < this.count; i++)
            {
                if (object.Equals(value, this.items[i].Value))
                    return i;
            }
            return -1;
        }

        public void Update(TPriority priority, TValue value)
        {
            // find index
            var index = this.IndexOf(value);
            if (index < 0)
                throw new InvalidOperationException("value was not found in the heap");

            // remove and add
            this.RemoveAt(index);
            this.Add(priority, value);
        }

        private bool Less(int i, int j)
        {
            GraphContracts.Assert(i >= 0 & i < this.count &
                         j >= 0 & j < this.count &
                         i != j, String.Format("i: {0}, j: {1}", i, j));

            return this.priorityComparsion(this.items[i].Key, this.items[j].Key) <= 0;
        }

        private void Swap(int i, int j)
        {
            GraphContracts.Assert(i >= 0 & i < this.count &
                         j >= 0 & j < this.count &
                         i != j);

            var kv = this.items[i];
            this.items[i] = this.items[j];
            this.items[j] = kv;
        }

        [Conditional("DEBUG")]
        public void ObjectInvariant()
        {
            GraphContracts.Assert(this.items != null);
            GraphContracts.Assert(
                this.count > -1 &
                this.count <= this.items.Length);
            for (int index = 0; index < this.count; ++index)
            {
                var left = 2 * index + 1;
                GraphContracts.Assert(left >= count || this.Less(index, left));
                var right = 2 * index + 2;
                GraphContracts.Assert(right >= count || this.Less(index, right));
            }
        }

        #region IEnumerable<KeyValuePair<TKey,TValue>> Members
        public IEnumerator<KeyValuePair<TPriority, TValue>> GetEnumerator()
        {
            return new Enumerator(this);
        }

        struct Enumerator :
            IEnumerator<KeyValuePair<TPriority, TValue>>
        {
            BinaryHeap<TPriority, TValue> owner;
            KeyValuePair<TPriority, TValue>[] items;
            readonly int count;
            readonly int version;
            int index;

            public Enumerator(BinaryHeap<TPriority, TValue> owner)
            {
                this.owner = owner;
                this.items = owner.items;
                this.count = owner.count;
                this.version = owner.version;
                this.index = -1;
            }

            public KeyValuePair<TPriority, TValue> Current
            {
                get
                {
                    if (this.version != this.owner.version)
                        throw new InvalidOperationException();
                    if (this.index < 0 | this.index == this.count)
                        throw new InvalidOperationException();
                    GraphContracts.Assert(this.index <= this.count);
                    return this.items[this.index];
                }
            }

            void IDisposable.Dispose()
            {
                this.owner = null;
                this.items = null;
            }

            object IEnumerator.Current
            {
                get { return this.Current; }
            }

            public bool MoveNext()
            {
                if (this.version != this.owner.version)
                    throw new InvalidOperationException();
                return ++this.index < this.count;
            }

            public void Reset()
            {
                if (this.version != this.owner.version)
                    throw new InvalidOperationException();
                this.index = -1;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
        #endregion
    }
}
