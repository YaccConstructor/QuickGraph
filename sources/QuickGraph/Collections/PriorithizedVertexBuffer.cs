using System;
using System.Collections.Generic;

namespace QuickGraph.Collections
{
    [Serializable]
    public sealed class PriorithizedVertexBuffer<Vertex, Distance> : VertexBuffer<Vertex>
        where Distance : IComparable
    {
        private IDictionary<Vertex, Distance> distances;
        private DistanceComparer<Vertex,Distance> comparer;

		public PriorithizedVertexBuffer(
            IDictionary<Vertex, Distance> distances)
		{
			if ( distances == null)
				throw new ArgumentNullException("Distance map is null");
			this.distances = distances;
			this.comparer = new DistanceComparer<Vertex,Distance>(this.distances);
		}

		public void Update(Vertex v)
		{
            Sort(this.comparer);
        }

		public new void Push(Vertex v)
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
