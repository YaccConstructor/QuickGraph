using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Diagnostics.Contracts;
using System.Linq;
using QuickGraph.Algorithms;
using System.Diagnostics;

namespace QuickGraph.Collections
{
    [DebuggerDisplay("Count = {Count}")]
    public sealed class FibonacciQueue<TVertex, TDistance> :
        IPriorityQueue<TVertex>
    {
        public FibonacciQueue(Func<TVertex, TDistance> distances)
            : this(0, null, distances, Comparer<TDistance>.Default.Compare)
        { }

        public FibonacciQueue(
            int valueCount,
            IEnumerable<TVertex> values,
            Func<TVertex, TDistance> distances
            )
            : this(valueCount, values, distances, Comparer<TDistance>.Default.Compare)
        { }

        public FibonacciQueue(
            int valueCount,
            IEnumerable<TVertex> values,
            Func<TVertex, TDistance> distances,
            Func<TDistance, TDistance, int> distanceComparison
            )
        {
            Contract.Requires(valueCount >= 0);
            Contract.Requires(valueCount == 0 || (values != null && valueCount == Enumerable.Count(values)));
            Contract.Requires(distances != null);
            Contract.Requires(distanceComparison != null);

            this.distances = distances;
            this.cells = new Dictionary<TVertex, FibonacciHeapCell<TDistance, TVertex>>(valueCount);
            if (valueCount > 0)
                foreach (var x in values)
                    this.cells.Add(
                        x,
                        new FibonacciHeapCell<TDistance, TVertex>
                        {
                            Priority = this.distances(x),
                            Value = x,
                            Removed = true
                        }
                    );
            this.heap = new FibonacciHeap<TDistance, TVertex>(HeapDirection.Increasing, distanceComparison);
        }

        public FibonacciQueue(
            Dictionary<TVertex, TDistance> values,
            Func<TDistance, TDistance, int> distanceComparison
            )
        {
            Contract.Requires(values != null);
            Contract.Requires(distanceComparison != null);

            this.distances = AlgorithmExtensions.GetIndexer(values);
            this.cells = new Dictionary<TVertex, FibonacciHeapCell<TDistance, TVertex>>(values.Count);
            foreach (var kv in values)
                this.cells.Add(
                    kv.Key,
                    new FibonacciHeapCell<TDistance, TVertex>
                    {
                        Priority = kv.Value,
                        Value = kv.Key,
                        Removed = true
                    }
                );
            this.heap = new FibonacciHeap<TDistance, TVertex>(HeapDirection.Increasing, distanceComparison);
        }

        public FibonacciQueue(
            Dictionary<TVertex, TDistance> values)
            : this(values, Comparer<TDistance>.Default.Compare)
        { }

        private readonly FibonacciHeap<TDistance, TVertex> heap;
        private readonly Dictionary<TVertex, FibonacciHeapCell<TDistance, TVertex>> cells;        
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
            FibonacciHeapCell<TDistance, TVertex> result;
            return 
                this.cells.TryGetValue(value, out result) && 
                !result.Removed;
        }

        public void Update(TVertex v)
        {
            this.heap.ChangeKey(this.cells[v], this.distances(v));
        }

        public void Enqueue(TVertex value)
        {
            this.cells[value] = this.heap.Enqueue(this.distances(value), value);
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
