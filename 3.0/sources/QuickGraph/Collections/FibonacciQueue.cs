using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Diagnostics.Contracts;

namespace QuickGraph.Collections
{
    public sealed class FibonacciQueue<TVertex, TDistance> :
        IPriorityQueue<TVertex>
    {
        public FibonacciQueue(
            IEnumerable<TVertex> values,
            Func<TVertex, TDistance> distances
            )
            : this(values, distances, Comparer<TDistance>.Default.Compare)
        { }

        public FibonacciQueue(
            IEnumerable<TVertex> values,
            Func<TVertex, TDistance> distances,
            Comparison<TDistance> distanceComparison
            )
		{
            Contract.Requires(values != null);
            Contract.Requires(distances != null);
            Contract.Requires(distanceComparison != null);

            this.distances = distances;
            this.mDistances = new Dictionary<TVertex, FibonacciHeapCell<TDistance, TVertex>>();
            foreach(var x in values)
                this.mDistances.Add(x, new FibonacciHeapCell<TDistance, TVertex> { Priority = this.distances(x), Value = x, Removed = true });
            this.heap = new FibonacciHeap<TDistance, TVertex>(HeapDirection.Increasing, distanceComparison);            
		}
        private readonly FibonacciHeap<TDistance, TVertex> heap;
        private readonly Dictionary<TVertex, FibonacciHeapCell<TDistance, TVertex>> mDistances;        
        private readonly Func<TVertex, TDistance> distances;
        #region IQueue<TVertex> Members

        public int Count
        {
            [Pure]
            get { return this.heap.Count; }
        }

        [Pure]
        public bool Contains(TVertex value)
        {
            Contract.Requires(value != null);

            FibonacciHeapCell<TDistance, TVertex> result;
            return 
                this.mDistances.TryGetValue(value, out result) && 
                !result.Removed;
        }

        public void Update(TVertex v)
        {
            this.heap.ChangeKey(this.mDistances[v], this.distances(v));
        }

        public void Enqueue(TVertex value)
        {
            Contract.Requires(value != null);
            Contract.Requires(this.mDistances.ContainsKey(value));

            this.mDistances[value] = heap.Enqueue(this.distances(value), value);
        }

        public TVertex Dequeue()
        {
            var result = heap.Top;
            Contract.Assert(result != null);
            heap.Dequeue();            
            return result.Value;
        }

        public TVertex Peek()
        {
            Contract.Assert(this.heap.Top != null);

            return this.heap.Top.Value;
        }

        [Pure]
        public TVertex[] ToArray()
        {
            TVertex[] result = new TVertex[this.heap.Count];
            int i = 0;
            foreach (var entry in this.heap)
                result[i++] = entry.Value;
            return result;
        }
        #endregion
    }
}
