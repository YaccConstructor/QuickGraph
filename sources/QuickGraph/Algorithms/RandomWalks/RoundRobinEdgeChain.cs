using System;
using System.Collections.Generic;

namespace QuickGraph.Algorithms.RandomWalks
{
    [Serializable]
    public sealed class RoundRobinEdgeChain<Vertex, Edge> : IEdgeChain<Vertex, Edge>
        where Edge : IEdge<Vertex>
    {
        private Dictionary<Vertex, int> outEdgeIndices = new Dictionary<Vertex, int>();

        public Edge Successor(IImplicitGraph<Vertex, Edge> g, Vertex u)
        {
            int outDegree = g.OutDegree(u);
            if (outDegree == 0)
                return default(Edge);

            int index;
            if (!outEdgeIndices.TryGetValue(u, out index))
            {
                index = 0;
                outEdgeIndices.Add(u, index);
            }
            Edge e = g.OutEdge(u, index);
            this.outEdgeIndices[u] = (++index) % outDegree;

            return e;
        }
    }
}
