using System;
using System.Collections.Generic;

namespace QuickGraph.Algorithms.RandomWalks
{
    [Serializable]
    public abstract class WeightedMarkovEdgeChainBase<Vertex, Edge> :
        MarkovEdgeChainBase<Vertex, Edge>
        where Edge : IEdge<Vertex>
    {
        private IDictionary<Edge, double> weights;
        public WeightedMarkovEdgeChainBase(IDictionary<Edge, double> weights)
        {
            if (weights == null)
                throw new ArgumentNullException("weights");
            this.weights = weights;
        }

        public IDictionary<Edge, double> Weights
        {
            get { return this.weights; }
            set { this.weights = value; }
        }

        protected double GetOutWeight(IImplicitGraph<Vertex, Edge> g, Vertex u)
        {
            double outWeight = 0;
            foreach (Edge e in g.OutEdges(u))
            {
                outWeight += this.weights[e];
            }
            return outWeight;
        }

        protected Edge Successor(IImplicitGraph<Vertex, Edge> g, Vertex u, double position)
        {
            double pos = 0;
            double nextPos = 0;
            foreach (Edge e in g.OutEdges(u))
            {
                nextPos = pos + this.weights[e];
                if (position >= pos && position <= nextPos)
                    return e;
                pos = nextPos;
            }
            return default(Edge);
        }
    }
}
