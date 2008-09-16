using System;
using System.Collections.Generic;

namespace QuickGraph.Algorithms.RandomWalks
{
    [Serializable]
    public abstract class WeightedMarkovEdgeChainBase<TVertex, TEdge> :
        MarkovEdgeChainBase<TVertex, TEdge>
        where TEdge : IEdge<TVertex>
    {
        private IDictionary<TEdge, double> weights;
        public WeightedMarkovEdgeChainBase(IDictionary<TEdge, double> weights)
        {
            if (weights == null)
                throw new ArgumentNullException("weights");
            this.weights = weights;
        }

        public IDictionary<TEdge, double> Weights
        {
            get { return this.weights; }
            set { this.weights = value; }
        }

        protected double GetOutWeight(IImplicitGraph<TVertex, TEdge> g, TVertex u)
        {
            double outWeight = 0;
            foreach (var e in g.OutEdges(u))
            {
                outWeight += this.weights[e];
            }
            return outWeight;
        }

        protected TEdge Successor(IImplicitGraph<TVertex, TEdge> g, TVertex u, double position)
        {
            double pos = 0;
            double nextPos = 0;
            foreach (var e in g.OutEdges(u))
            {
                nextPos = pos + this.weights[e];
                if (position >= pos && position <= nextPos)
                    return e;
                pos = nextPos;
            }
            return default(TEdge);
        }
    }
}
