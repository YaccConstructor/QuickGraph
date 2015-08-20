using System;
using System.Collections.Generic;

namespace QuickGraph.Predicates
{
    public sealed class ResidualEdgePredicate<TVertex,TEdge>
        where TEdge : IEdge<TVertex>
    {
		private IDictionary<TEdge,double> residualCapacities;

        public ResidualEdgePredicate(
            IDictionary<TEdge,double> residualCapacities)
		{
			if (residualCapacities == null)
				throw new ArgumentNullException("residualCapacities");
			this.residualCapacities = residualCapacities;
		}

		public IDictionary<TEdge,double> ResidualCapacities
		{
			get
			{
				return this.residualCapacities;
			}
		}

		public bool Test(TEdge e)
		{
			if (e == null)
				throw new ArgumentNullException("e");

			return 0 < this.residualCapacities[e];
		}
    }
}
