using System;
using System.Collections.Generic;

namespace QuickGraph.Algorithms.RandomWalks
{
    [Serializable]
    public sealed class WeightedMarkovEdgeChain<TVertex, TEdge> :
        WeightedMarkovEdgeChainBase<TVertex, TEdge>
        where TEdge : IEdge<TVertex>
    {
        public WeightedMarkovEdgeChain(IDictionary<TEdge,double> weights)
            :base(weights)
        {}

        public override TEdge Successor(IImplicitGraph<TVertex, TEdge> g, TVertex u)
        {
            if (g == null)
                throw new ArgumentNullException("g");
            if (u == null)
                throw new ArgumentNullException("u");

            // get number of out-edges
            int n = g.OutDegree(u);
            if (n == 0)
                return default(TEdge);
            // compute out-edge su
            double outWeight = GetOutWeight(g, u);
            // scale and get next edge
            double r = this.Rand.NextDouble() * outWeight;
            return Successor(g, u, r);
        }
    }
}
