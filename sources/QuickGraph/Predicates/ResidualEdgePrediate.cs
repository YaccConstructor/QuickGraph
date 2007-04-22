using System;
using System.Collections.Generic;

namespace QuickGraph.Predicates
{
    public sealed class ResidualEdgePredicate<Vertex,Edge> :
        IEdgePredicate<Vertex,Edge>
        where Edge : IEdge<Vertex>
    {
		private IDictionary<Edge,double> residualCapacities;

        public ResidualEdgePredicate(
            IDictionary<Edge,double> residualCapacities)
		{
			if (residualCapacities == null)
				throw new ArgumentNullException("residualCapacities");
			this.residualCapacities = residualCapacities;
		}

		public IDictionary<Edge,double> ResidualCapacities
		{
			get
			{
				return this.residualCapacities;
			}
		}

		public bool Test(Edge e)
		{
			if (e == null)
				throw new ArgumentNullException("e");

			return 0 < this.residualCapacities[e];
		}
    }
}
