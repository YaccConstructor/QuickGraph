using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Diagnostics.Contracts;

namespace QuickGraph.Collections
{
    public sealed class FibonacciQueue<TVertex, TDistance> :
        IQueue<TVertex>
    {
        public FibonacciQueue(
            IDictionary<TVertex, TDistance> distances
            )
            : this(distances, Comparer<TDistance>.Default.Compare)
        { }

        public FibonacciQueue(
            IDictionary<TVertex, TDistance> distances,
            Comparison<TDistance> distanceComparison
            )
		{
            Contract.Requires(distances != null);
            Contract.Requires(distanceComparison != null);

            this.distances = distances;
            this.mDistances = this.distances.Select(x => new FibonacciHeapCell<TDistance, TVertex> { Priority = x.Value, Value = x.Key, Removed = true }).ToDictionary(x => x.Value);
            this.heap = new FibonacciHeap<TDistance, TVertex>(HeapDirection.Increasing, distanceComparison);            
		}
        private readonly FibonacciHeap<TDistance, TVertex> heap;
        private readonly Dictionary<TVertex, FibonacciHeapCell<TDistance, TVertex>> mDistances;        
        private readonly IDictionary<TVertex, TDistance> distances;
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
            this.heap.ChangeKey(this.mDistances[v], this.distances[v]);
        }

        public void Enqueue(TVertex value)
        {
            Contract.Requires(value != null);
            Contract.Requires(this.mDistances.ContainsKey(value));

            this.mDistances[value] = heap.Enqueue(this.distances[value], value);
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
            return this.heap.Select(x => x.Value).ToArray();
        }
        #endregion
    }
}
