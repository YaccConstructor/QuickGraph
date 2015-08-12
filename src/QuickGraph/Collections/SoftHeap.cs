using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace QuickGraph.Collections
{
    public sealed class SoftHeap<TKey, TValue>
        : IEnumerable<KeyValuePair<TKey, TValue>>
    {
        sealed class Cell
        {
            public readonly TKey Key;
            public readonly TValue Value;
            public Cell Next = null;

            public Cell(TKey key, TValue value)
            {
                this.Key = key;
                this.Value = value;
            }
        }

        sealed class Node
        {
            public TKey CKey;
            public int Rank;
            public Node Next;
            public Node Child;
            public Cell IL;
            public Cell ILTail;

            public Node(Cell cell)
            {
                this.Rank = 0;
                this.CKey = cell.Key;
                this.IL = cell;
                this.ILTail = cell;
            }

            public Node(TKey cKey, int rank, Node next, Node child, Cell il, Cell iltail)
            {
                this.CKey = cKey;
                this.Rank = rank;
                this.Next = next;
                this.Child = child;
                this.IL = il;
                this.ILTail = iltail;
            }
        }

        sealed class Head
        {
            public Node Queue;
            public Head Next;
            public Head Prev;
            public Head SuffixMin;
            public int Rank;
        }

        readonly Func<TKey, TKey, int> comparison;
        readonly TKey keyMaxValue;
        readonly double errorRate;
        readonly Head header;
        readonly Head tail;
        int count;
        int r;

        public SoftHeap(double maximumErrorRate, TKey keyMaxValue)
            : this(maximumErrorRate, keyMaxValue, Comparer<TKey>.Default.Compare)
        { }

        public SoftHeap(double maximumErrorRate, TKey keyMaxValue, Func<TKey, TKey, int> comparison)
        {
            Contract.Requires(comparison != null);
            Contract.Requires(0 < maximumErrorRate && maximumErrorRate <= 0.5);

            this.comparison = comparison;
            this.keyMaxValue = keyMaxValue;
            this.header = new Head();
            this.tail = new Head();
            this.tail.Rank = int.MaxValue;
            this.header.Next = tail;
            this.tail.Prev = header;
            this.errorRate = maximumErrorRate;
            this.r = 2 + 2 * (int)Math.Ceiling(Math.Log(1.0 / this.errorRate, 2.0));
            this.count = 0;
        }

        public int MinRank
        {
            get { return this.r; }
        }

        public Func<TKey, TKey, int> Comparison
        {
            get { return this.comparison; }
        }

        public double ErrorRate
        {
            get { return this.errorRate; }
        }

        public int Count
        {
            get { return this.count; }
        }

        public TKey KeyMaxValue
        {
            get { return this.keyMaxValue; }
        }

        public void Add(TKey key, TValue value)
        {
            Contract.Requires(this.Comparison(key, this.KeyMaxValue) < 0);

            var l = new Cell(key, value);
            var q = new Node(l);

            this.Meld(q);
            this.count++;
        }

        private void Meld(Node q)
        {
            Contract.Requires(q != null);

            Head toHead = header.Next;
            while (q.Rank > toHead.Rank)
            {
                Contract.Assert(toHead.Next != null);
                toHead = toHead.Next;
            }
            Head prevHead = toHead.Prev;
            while (q.Rank == toHead.Rank)
            {
                Node top, bottom;
                if (this.comparison(toHead.Queue.CKey, q.CKey) > 0)
                {
                    top = q;
                    bottom = toHead.Queue;
                }
                else
                {
                    top = toHead.Queue;
                    bottom = q;
                }
                q = new Node(top.CKey, top.Rank + 1, bottom, top, top.IL, top.ILTail);
                toHead = toHead.Next;
            }

            Head h;
            if (prevHead == toHead.Prev)
                h = new Head();
            else
                h = prevHead.Next;
            h.Queue = q;
            h.Rank = q.Rank;
            h.Prev = prevHead;
            h.Next = toHead;
            prevHead.Next = h;
            toHead.Prev = h;

            FixMinLinst(h);
        }

        private void FixMinLinst(Head h)
        {
            Contract.Requires(h != null);

            Head tmpmin;
            if (h.Next == tail)
                tmpmin = h;
            else
                tmpmin = h.Next.SuffixMin;

            while (h != header)
            {
                if (this.comparison(tmpmin.Queue.CKey, h.Queue.CKey) > 0)
                    tmpmin = h;

                h.SuffixMin = tmpmin;
                h = h.Prev;
            }
        }

        private Node Shift(Node v)
        {
            Contract.Requires(v != null);

            v.IL = null;
            v.ILTail = null;
            if (v.Next == null && v.Child == null)
            {
                v.CKey = this.keyMaxValue;
                return v;
            }

            v.Next = Shift(v.Next);
            // restore heap ordering that might be broken by shifting
            if (this.comparison(v.Next.CKey, v.Child.CKey) > 0)
            {
                var tmp = v.Child;
                v.Child = v.Next;
                v.Next = tmp;
            }

            v.IL = v.Next.IL;
            v.ILTail = v.Next.ILTail;
            v.CKey = v.Next.CKey;

            // softening the heap
            if (v.Rank > this.r &&
                (v.Rank % 2 == 1 || v.Child.Rank < v.Rank - 1))
            {
                v.Next = Shift(v.Next);
                // restore heap ordering that might be broken by shifting
                if (this.comparison(v.Next.CKey, v.Child.CKey) > 0)
                {
                    var tmp = v.Child;
                    v.Child = v.Next;
                    v.Next = tmp;
                }

                if (this.comparison(v.Next.CKey, this.keyMaxValue) != 0 && v.Next.IL != null)
                {
                    v.Next.ILTail.Next = v.IL;
                    v.IL = v.Next.IL;
                    if (v.ILTail == null)
                        v.ILTail = v.Next.ILTail;
                    v.CKey = v.Next.CKey;
                }
            }  // end second shift

            if (this.comparison(v.Child.CKey, this.keyMaxValue) == 0)
            {
                if (this.comparison(v.Next.CKey, this.keyMaxValue) == 0)
                {
                    v.Child = null;
                    v.Next = null;
                }
                else
                {
                    v.Child = v.Next.Child;
                    v.Next = v.Next.Next;
                }
            }

            return v;
        } // Shift

        public KeyValuePair<TKey, TValue> DeleteMin()
        {
            if (this.count == 0)
                throw new InvalidOperationException();

            var h = this.header.Next.SuffixMin;
            while (h.Queue.IL == null)
            {
                var tmp = h.Queue;
                int childCount = 0;
                while (tmp.Next != null)
                {
                    tmp = tmp.Next;
                    childCount++;
                }

                if (childCount < h.Rank / 2)
                {
                    h.Prev.Next = h.Next;
                    h.Next.Prev = h.Prev;
                    FixMinLinst(h.Prev);
                    tmp = h.Queue;
                    while (tmp.Next != null)
                    {
                        Meld(tmp.Child);
                        tmp = tmp.Next;
                    }
                }
                else
                {
                    h.Queue = Shift(h.Queue);
                    if (this.comparison(h.Queue.CKey, this.keyMaxValue) == 0)
                    {
                        h.Prev.Next = h.Next;
                        h.Next.Prev = h.Prev;
                        h = h.Prev;
                    }
                }

                h = header.Next.SuffixMin;
            } // end of outer while loop

            var min = h.Queue.IL.Key;
            var value = h.Queue.IL.Value;
            h.Queue.IL = h.Queue.IL.Next;
            if (h.Queue.IL == null)
                h.Queue.ILTail = null;

            this.count--;
            return new KeyValuePair<TKey, TValue>(min, value);
        }

        [ContractInvariantMethod]
        void Invariant()
        {
            Contract.Invariant(this.count > -1);
            Contract.Invariant(this.header != null);
            Contract.Invariant(this.tail != null);
        }

        #region IEnumerable<KeyValuePair<TKey,TValue>> Members
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return new Enumerator(this);
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        struct Enumerator
            : IEnumerator<KeyValuePair<TKey, TValue>>
        {
            readonly SoftHeap<TKey, TValue> owner;
            KeyValuePair<TKey, TValue> current;

            public Enumerator(SoftHeap<TKey, TValue> owner)
            {
                Contract.Requires(owner != null);
                this.owner = owner;
                this.current = new KeyValuePair<TKey, TValue>();
            }

            public bool MoveNext()
            {
                // TODO
                return false;
            }

            public KeyValuePair<TKey, TValue> Current
            {
                get { return this.current; }
            }

            public void Dispose()
            { }

            object System.Collections.IEnumerator.Current
            {
                get { return this.Current; }
            }

            public void Reset()
            {
                throw new NotSupportedException();
            }
        }
        #endregion
    }
}
