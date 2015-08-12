using System;
using System.Collections.Generic;

namespace QuickGraph.Algorithms.RandomWalks
{
#if !SILVERLIGHT
    [Serializable]
#endif
    public sealed class WeightedMarkovEdgeChain<TVertex, TEdge> :
        WeightedMarkovEdgeChainBase<TVertex, TEdge>
        where TEdge : IEdge<TVertex>
    {
        public WeightedMarkovEdgeChain(IDictionary<TEdge,double> weights)
            :base(weights)
        {}

        public override bool TryGetSuccessor(IImplicitGraph<TVertex, TEdge> g, TVertex u, out TEdge successor)
        {
            // get number of out-edges
            int n = g.OutDegree(u);
            if (n > 0)
            {
                // compute out-edge su
                double outWeight = GetOutWeight(g, u);
                // scale and get next edge
                double r = this.Rand.NextDouble() * outWeight;
                return TryGetSuccessor(g, u, r, out successor);
            }

            successor = default(TEdge);
            return false;
        }

        public override bool TryGetSuccessor(IEnumerable<TEdge> edges, TVertex u, out TEdge sucessor)
        {
            // compute out-edge su
            double outWeight = GetWeights(edges);
            // scale and get next edge
            double r = this.Rand.NextDouble() * outWeight;
            return TryGetSuccessor(edges, r, out sucessor);
        }

    }
}
