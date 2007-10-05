using System;
using System.Collections.Generic;

namespace QuickGraph.Collections
{
    [Serializable]
    public sealed class PriorithizedVertexBuffer<TVertex, TDistance> : VertexBuffer<TVertex>
        where TDistance : IComparable
    {
        private IDictionary<TVertex, TDistance> distances;
        private DistanceComparer<TVertex,TDistance> comparer;

		public PriorithizedVertexBuffer(
            IDictionary<TVertex, TDistance> distances)
		{
			if ( distances == null)
				throw new ArgumentNullException("Distance map is null");
			this.distances = distances;
			this.comparer = new DistanceComparer<TVertex,TDistance>(this.distances);
		}

		public void Update(TVertex v)
		{
            Sort(this.comparer);
        }

		public new void Push(TVertex v)
		{
			// add to queue
			base.Push(v);
			// sort queue
			Sort(this.comparer);
		}

        public new void Sort()
        {
            this.Sort(this.comparer);
        }
    }
}
