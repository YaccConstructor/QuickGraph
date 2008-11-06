using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Diagnostics.Contracts;

namespace QuickGraph.Collections
{
    internal sealed class FibonacciQueue<TVertex, TDistance> :
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
			if (distances == null)
				throw new ArgumentNullException("distances");
            if (distanceComparison == null)
                throw new ArgumentNullException("distanceComparison");

            mDistances = distances.Select(x => new FibonacciHeapCell<TDistance, TVertex> { Priority = x.Value, Value = x.Key, Removed = true  }).ToDictionary(x => x.Value);
            mHeap = new FibonacciHeap<TDistance, TVertex>(HeapDirection.Increasing, distanceComparison);            
            this.distances = distances;
		}
        private FibonacciHeap<TDistance, TVertex> mHeap;
        private Dictionary<TVertex, FibonacciHeapCell<TDistance, TVertex>> mDistances;        
        private IDictionary<TVertex, TDistance> distances;
        #region IQueue<TVertex> Members

        public int Count
        {
            get { return mHeap.Count; }
        }

        public bool Contains(TVertex value)
        {
            FibonacciHeapCell<TDistance, TVertex> result;
            return (mDistances.TryGetValue(value, out result) && result.Removed == false);
        }

        public void Update(TVertex v)
        {
            mHeap.ChangeKey(mDistances[v], distances[v]);
        }

        public void Enqueue(TVertex value)
        {
            CodeContract.Requires(value != null);
            CodeContract.Requires(this.mDistances.ContainsKey(value));
            mDistances[value] = mHeap.Enqueue(this.mDistances[value].Priority, value);
        }

        public TVertex Dequeue()
        {
            var result = mHeap.Top;
            mHeap.Dequeue();            
            return result.Value;
        }

        public TVertex Peek()
        {
            return mHeap.Top.Value;
        }

        public TVertex[] ToArray()
        {
            return this.mHeap.Select(x => x.Value).ToArray();
        }

        #endregion
    }
}
