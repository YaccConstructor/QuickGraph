using System;
using System.Collections.Generic;

namespace ModelDriven.Graph.Collections
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// Algorithms in Java 3rd edition. Chapter 9.6
    /// </remarks>
    /// <typeparam name="T"></typeparam>
    public class PriorityQueue<T> : IEnumerable<T>
    {
        private T[] items;
        private int[] pq;
        private int[] qp;
        private int count;
        private IComparer<T> comparer;

        public PriorityQueue(T[] items, IComparer<T> comparer)
        {
            this.items = items;
            this.count = 0;
            this.comparer = comparer;
            this.pq = new int[this.items.Length + 1];
            this.qp = new int[this.items.Length + 1];
        }

        #region Heap methods
        private bool Less(int left, int right)
        {
            return this.comparer.Compare(
                this.items[pq[left]], 
                this.items[pq[right]]
                ) < 0;
        }

        private void Exchange(int i, int j)
        {
            int t = pq[i];
            pq[i] = pq[j];
            pq[j] = t;
            qp[pq[i]] = i;
            qp[pq[j]] = j;
        }

        private void Swim(int k)
        {
            while (k > 1 && Less(k / 2, k))
            {
                Exchange(k, k / 2);
                k = k / 2;
            }
        }

        private void Sink(int k, int N)
        {
            while (2 * k <= N)
            {
                int j = 2 * k;
                if (j < N && Less(j, j + 1))
                    j++;
                if (!Less(k, j))
                    break;
                Exchange(k, j);
                k = j;
            }
        }
        #endregion

        public int Count
        {
            get { return this.count; }
        }

        public int Capacity
        {
            get { return this.items.Length - 1; }
        }

        public void Add(int v)
        {
            pq[++this.count] = v;
            qp[v] = this.Count;
            Swim(this.Count);
        }

        public void Update(int k)
        {
            Swim(qp[k]);
            Sink(qp[k], this.Count);
        }

        public int Pop()
        {
            Exchange(1, this.Count);
            Sink(1, this.Count - 1);
            return pq[this.count--];
        }

        public IEnumerator<T> GetEnumerator()
        {
            return (IEnumerator<T>)this.items.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
