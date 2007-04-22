using System;
using System.Collections.Generic;
namespace QuickGraph.Algorithms.RandomWalks
{
    [Serializable]
    public sealed class VanishingWeightedMarkovEdgeChain<Vertex, Edge> :
        WeightedMarkovEdgeChainBase<Vertex,Edge>
        where Edge : IEdge<Vertex>
    {
		private double factor;

		public VanishingWeightedMarkovEdgeChain(IDictionary<Edge,double> weights)
            :this(weights,0.2)
        {}

		public VanishingWeightedMarkovEdgeChain(IDictionary<Edge,double> weights, double factor)
			:base(weights)
		{
			this.factor = factor;
		}

		public double Factor
		{
			get
			{
				return this.factor;
			}
            set 
            {
                this.factor = value;
            }
		}

        public override Edge Successor(IImplicitGraph<Vertex,Edge> g, Vertex u)
        {
            if (g.IsOutEdgesEmpty(u))
                return default(Edge);
            // get outweight
            double outWeight = GetOutWeight(g, u);
            // get succesor
            Edge s = Successor(g,u,this.Rand.NextDouble() * outWeight);

			// update probabilities
			this.Weights[s]*=this.Factor;

            // normalize
            foreach(Edge e in g.OutEdges(u))
            {
                checked
                {
                    this.Weights[e]/=outWeight;
                }
            }

			return s;
		}
	}
}
