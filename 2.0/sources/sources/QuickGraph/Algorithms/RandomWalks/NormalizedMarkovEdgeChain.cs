using System;
using System.Collections.Generic;

namespace QuickGraph.Algorithms.RandomWalks
{
    [Serializable]
    public sealed class NormalizedMarkovEdgeChain<TVertex, TEdge> :
        MarkovEdgeChainBase<TVertex, TEdge>
        where TEdge : IEdge<TVertex>
    {
        public override TEdge Successor(IImplicitGraph<TVertex,TEdge> g, TVertex u)
        {
            int outDegree = g.OutDegree(u);
            if (outDegree == 0)
                return default(TEdge);

            int index = this.Rand.Next(0, outDegree);
            return g.OutEdge(u, index);
        }
    }
}
