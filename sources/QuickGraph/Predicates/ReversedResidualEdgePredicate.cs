using System;
using System.Collections.Generic;
namespace QuickGraph.Predicates
{
    [Serializable]
    public sealed class ReversedResidualEdgePredicate<Vertex,Edge> :
        IEdgePredicate<Vertex,Edge>
        where Edge : IEdge<Vertex>
    {
        private IDictionary<Edge,double> residualCapacities;
        private IDictionary<Edge,Edge> reversedEdges;

        public ReversedResidualEdgePredicate(
            IDictionary<Edge, double> residualCapacities,
            IDictionary<Edge, Edge> reversedEdges)
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
        public IDictionary<Edge,double> ResidualCapacities
        {
            get
            {
                return this.residualCapacities;
            }
        }

        /// <summary>
        /// Reversed edges map
        /// </summary>
        public IDictionary<Edge,Edge> ReversedEdges
        {
            get
            {
                return this.reversedEdges;
            }
        }

        public bool Test(Edge e)
        {
            if (e == null)
                throw new ArgumentNullException("e");

            return 0 < this.residualCapacities[reversedEdges[e]];
        }
    }
}
