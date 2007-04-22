using System;

namespace QuickGraph.Algorithms.RandomWalks
{
    [Serializable]
    public abstract class MarkovEdgeChainBase<Vertex, Edge> : IMarkovEdgeChain<Vertex, Edge>
        where Edge : IEdge<Vertex>
    {
        private Random rand = new Random();

        public Random Rand
        {
            get
            {
                return this.rand;
            }
            set
            {
                this.rand = value;
            }
        }

        public abstract Edge Successor(IImplicitGraph<Vertex, Edge> g, Vertex u);
    }
}
