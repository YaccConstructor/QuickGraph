using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace QuickGraph.Collections
{
    [Serializable]
    public sealed class PriorityQueue<TVertex, TDistance> : 
        IQueue<TVertex>
    {
        private readonly IDictionary<TVertex, TDistance> distances;
        private readonly BinaryHeap<TDistance, TVertex> heap;

        public PriorityQueue(
            IDictionary<TVertex, TDistance> distances
            )
            : this(distances, Comparer<TDistance>.Default.Compare)
        { }

		public PriorityQueue(
            IDictionary<TVertex, TDistance> distances,
            Comparison<TDistance> distanceComparison
            )
		{
			if (distances == null)
				throw new ArgumentNullException("distances");
            if (distanceComparison == null)
                throw new ArgumentNullException("distanceComparison");

			this.distances = distances;
            this.heap = new BinaryHeap<TDistance, TVertex>(distanceComparison);
		}

		public void Update(TVertex v)
		{
            this.heap.Update(this.distances[v], v);
        }

        public int Count
        {
            get { return this.heap.Count; }
        }

        public bool Contains(TVertex value)
        {
            return this.heap.IndexOf(value) > -1;
        }

        public void Enqueue(TVertex value)
        {
            GraphContracts.AssumeNotNull(value, "value");
            GraphContracts.Assume(this.distances.ContainsKey(value), "this.distances.ContainsKey(value)");
            this.heap.Add(this.distances[value], value);
        }

        public TVertex Dequeue()
        {
            return this.heap.RemoveMinimum().Value;
        }

        public TVertex Peek()
        {
            return this.heap.Minimum().Value;
        }
    }
}
