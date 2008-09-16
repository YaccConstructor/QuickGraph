using System;
using System.Collections.Generic;
namespace QuickGraph.Predicates
{
    [Serializable]
    public sealed class ReversedResidualEdgePredicate<TVertex,TEdge>
        where TEdge : IEdge<TVertex>
    {
        private IDictionary<TEdge,double> residualCapacities;
        private IDictionary<TEdge,TEdge> reversedEdges;

        public ReversedResidualEdgePredicate(
            IDictionary<TEdge, double> residualCapacities,
            IDictionary<TEdge, TEdge> reversedEdges)
        {
            if (residualCapacities == null)
                throw new ArgumentNullException("residualCapacities");
            if (reversedEdges == null)
                throw new ArgumentNullException("reversedEdges");
            this.residualCapacities = residualCapacities;
            this.reversedEdges = reversedEdges;
        }

        /// <summary>
        /// Residual capacities map
        /// </summary>
        public IDictionary<TEdge,double> ResidualCapacities
        {
            get
            {
                return this.residualCapacities;
            }
        }

        /// <summary>
        /// Reversed edges map
        /// </summary>
        public IDictionary<TEdge,TEdge> ReversedEdges
        {
            get
            {
                return this.reversedEdges;
            }
        }

        public bool Test(TEdge e)
        {
            if (e == null)
                throw new ArgumentNullException("e");

            return 0 < this.residualCapacities[reversedEdges[e]];
        }
    }
}
