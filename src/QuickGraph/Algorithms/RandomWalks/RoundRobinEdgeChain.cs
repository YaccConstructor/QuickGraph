using System;
using System.Collections.Generic;
using System.Linq;

namespace QuickGraph.Algorithms.RandomWalks
{
#if !SILVERLIGHT
    [Serializable]
#endif
    public sealed class RoundRobinEdgeChain<TVertex, TEdge> 
        : IEdgeChain<TVertex, TEdge>
        where TEdge : IEdge<TVertex>
    {
        private Dictionary<TVertex, int> outEdgeIndices = new Dictionary<TVertex, int>();

        public bool TryGetSuccessor(IImplicitGraph<TVertex, TEdge> g, TVertex u, out TEdge successor)
        {
            int outDegree = g.OutDegree(u);
            if (outDegree > 0)
            {
                int index;
                if (!outEdgeIndices.TryGetValue(u, out index))
                {
                    index = 0;
                    outEdgeIndices.Add(u, index);
                }
                TEdge e = g.OutEdge(u, index);
                this.outEdgeIndices[u] = (++index) % outDegree;

                successor = e;
                return true;
            }

            successor = default(TEdge);
            return false;
        }

        public bool TryGetSuccessor(IEnumerable<TEdge> edges, TVertex u, out TEdge successor)
        {
            var edgeCount = Enumerable.Count(edges);

            if (edgeCount > 0)
            {
                int index;
                if (!outEdgeIndices.TryGetValue(u, out index))
                {
                    index = 0;
                    outEdgeIndices.Add(u, index);
                }
                var e = Enumerable.ElementAt(edges, index);
                this.outEdgeIndices[u] = (++index) % edgeCount;
                successor = e;
            }
            successor = default(TEdge);
            return false;
        }
    }
}
