using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace QuickGraph.Algorithms.RandomWalks
{
#if !SILVERLIGHT
    [Serializable]
#endif
    public abstract class WeightedMarkovEdgeChainBase<TVertex, TEdge> :
        MarkovEdgeChainBase<TVertex, TEdge>
        where TEdge : IEdge<TVertex>
    {
        private IDictionary<TEdge, double> weights;
        public WeightedMarkovEdgeChainBase(IDictionary<TEdge, double> weights)
        {
            Contract.Requires(weights != null);

            this.weights = weights;
        }

        public IDictionary<TEdge, double> Weights
        {
            get { return this.weights; }
            set { this.weights = value; }
        }

        protected double GetOutWeight(IImplicitGraph<TVertex, TEdge> g, TVertex u)
        {
            var edges = g.OutEdges(u);
            return GetWeights(edges);
        }

        protected double GetWeights(IEnumerable<TEdge> edges)
        {
            double outWeight = 0;
            foreach (var e in edges)
            {
                outWeight += this.weights[e];
            }
            return outWeight;
        }

        protected bool TryGetSuccessor(IImplicitGraph<TVertex, TEdge> g, TVertex u, double position, out TEdge successor)
        {
            Contract.Requires(g != null);
            Contract.Requires(u != null);

            var edges = g.OutEdges(u);
            return TryGetSuccessor(edges, position, out successor);
        }

        protected bool TryGetSuccessor(IEnumerable<TEdge> edges, double position, out TEdge successor)
        {
            Contract.Requires(edges != null);

            double pos = 0;
            double nextPos = 0;
            foreach (var e in edges)
            {
                nextPos = pos + this.weights[e];
                if (position >= pos && position <= nextPos)
                {
                    successor = e;
                    return true;
                }
                pos = nextPos;
            }

            successor = default(TEdge);
            return false;
        }
    }
}
