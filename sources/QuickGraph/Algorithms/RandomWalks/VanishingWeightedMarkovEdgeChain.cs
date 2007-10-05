using System;
using System.Collections.Generic;
namespace QuickGraph.Algorithms.RandomWalks
{
    [Serializable]
    public sealed class VanishingWeightedMarkovEdgeChain<TVertex, TEdge> :
        WeightedMarkovEdgeChainBase<TVertex,TEdge>
        where TEdge : IEdge<TVertex>
    {
		private double factor;

		public VanishingWeightedMarkovEdgeChain(IDictionary<TEdge,double> weights)
            :this(weights,0.2)
        {}

		public VanishingWeightedMarkovEdgeChain(IDictionary<TEdge,double> weights, double factor)
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

        public override TEdge Successor(IImplicitGraph<TVertex,TEdge> g, TVertex u)
        {
            if (g.IsOutEdgesEmpty(u))
                return default(TEdge);
            // get outweight
            double outWeight = GetOutWeight(g, u);
            // get succesor
            TEdge s = Successor(g,u,this.Rand.NextDouble() * outWeight);

			// update probabilities
			this.Weights[s]*=this.Factor;

            // normalize
            foreach(TEdge e in g.OutEdges(u))
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
