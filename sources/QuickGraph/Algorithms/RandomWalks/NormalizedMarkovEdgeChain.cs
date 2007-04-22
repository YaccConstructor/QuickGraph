using System;
using System.Collections.Generic;

namespace QuickGraph.Algorithms.RandomWalks
{
    [Serializable]
    public sealed class NormalizedMarkovEdgeChain<Vertex, Edge> :
        MarkovEdgeChainBase<Vertex, Edge>
        where Edge : IEdge<Vertex>
    {
        public override Edge Successor(IImplicitGraph<Vertex,Edge> g, Vertex u)
        {
            int outDegree = g.OutDegree(u);
            if (outDegree == 0)
                return default(Edge);

            int index = this.Rand.Next(0, outDegree);
            return g.OutEdge(u, index);
        }
    }
}
