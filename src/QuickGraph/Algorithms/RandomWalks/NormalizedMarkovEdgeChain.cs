using System;
using System.Collections.Generic;
using System.Linq;

namespace QuickGraph.Algorithms.RandomWalks
{
#if !SILVERLIGHT
    [Serializable]
#endif
    public sealed class NormalizedMarkovEdgeChain<TVertex, TEdge> :
        MarkovEdgeChainBase<TVertex, TEdge>
        where TEdge : IEdge<TVertex>
    {
        public override bool TryGetSuccessor(IImplicitGraph<TVertex,TEdge> g, TVertex u, out TEdge successor)
        {
            int outDegree = g.OutDegree(u);
            if (outDegree > 0)
            {
                int index = this.Rand.Next(0, outDegree);
                successor = g.OutEdge(u, index);
                return true;
            }

            successor = default(TEdge);
            return false;
        }

        public override bool TryGetSuccessor(IEnumerable<TEdge> edges, TVertex u, out TEdge successor)
        {
            var edgeCount = Enumerable.Count(edges);

            if (edgeCount > 0)
            {
                int index = this.Rand.Next(0, edgeCount);
                successor = Enumerable.ElementAt(edges, index);
                return true;
            }

            successor = default(TEdge);
            return false;
        }
    }
}
