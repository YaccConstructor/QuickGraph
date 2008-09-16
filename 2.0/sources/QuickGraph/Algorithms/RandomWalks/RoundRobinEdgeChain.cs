using System;
using System.Collections.Generic;

namespace QuickGraph.Algorithms.RandomWalks
{
    [Serializable]
    public sealed class RoundRobinEdgeChain<TVertex, TEdge> : 
        IEdgeChain<TVertex, TEdge>
        where TEdge : IEdge<TVertex>
    {
        private Dictionary<TVertex, int> outEdgeIndices = new Dictionary<TVertex, int>();

        public TEdge Successor(IImplicitGraph<TVertex, TEdge> g, TVertex u)
        {
            int outDegree = g.OutDegree(u);
            if (outDegree == 0)
                return default(TEdge);

            int index;
            if (!outEdgeIndices.TryGetValue(u, out index))
            {
                index = 0;
                outEdgeIndices.Add(u, index);
            }
            TEdge e = g.OutEdge(u, index);
            this.outEdgeIndices[u] = (++index) % outDegree;

            return e;
        }
    }
}
