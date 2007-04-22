using System;
using System.Collections.Generic;

namespace QuickGraph.Algorithms.RandomWalks
{
    [Serializable]
    public sealed class WeightedMarkovEdgeChain<Vertex, Edge> :
        WeightedMarkovEdgeChainBase<Vertex, Edge>
        where Edge : IEdge<Vertex>
    {
        public WeightedMarkovEdgeChain(IDictionary<Edge,double> weights)
            :base(weights)
        {}

        public override Edge Successor(IImplicitGraph<Vertex, Edge> g, Vertex u)
        {
            if (g == null)
                throw new ArgumentNullException("g");
            if (u == null)
                throw new ArgumentNullException("u");

            // get number of out-edges
            int n = g.OutDegree(u);
            if (n == 0)
                return default(Edge);
            // compute out-edge su
            double outWeight = GetOutWeight(g, u);
            // scale and get next edge
            double r = this.Rand.NextDouble() * outWeight;
            return Successor(g, u, r);
        }
    }
}
