using System;

namespace QuickGraph.Algorithms.RandomWalks
{
    [Serializable]
    public abstract class MarkovEdgeChainBase<TVertex, TEdge> : 
        IMarkovEdgeChain<TVertex, TEdge>
        where TEdge : IEdge<TVertex>
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

        public abstract TEdge Successor(IImplicitGraph<TVertex, TEdge> g, TVertex u);
    }
}
