using System;
using System.Collections.Generic;
namespace QuickGraph.Algorithms.RandomWalks
{
#if !SILVERLIGHT
    [Serializable]
#endif
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

        public override bool TryGetSuccessor(IImplicitGraph<TVertex,TEdge> g, TVertex u, out TEdge successor)
        {
            if (!g.IsOutEdgesEmpty(u))
            {
                // get outweight
                double outWeight = GetOutWeight(g, u);
                // get succesor
                TEdge s;
                if (this.TryGetSuccessor(g, u, this.Rand.NextDouble() * outWeight, out s))
                {
                    // update probabilities
                    this.Weights[s] *= this.Factor;

                    // normalize
                    foreach (TEdge e in g.OutEdges(u))
                    {
                        checked
                        {
                            this.Weights[e] /= outWeight;
                        }
                    }

                    successor = s;
                    return true;
                }
            }

            successor = default(TEdge);
            return false;
		}

        public override bool TryGetSuccessor(IEnumerable<TEdge> edges, TVertex u, out TEdge successor)
        {
            // get outweight
            double outWeight = this.GetWeights(edges);
            // get succesor
            TEdge s;
            if (this.TryGetSuccessor(edges, this.Rand.NextDouble() * outWeight, out s))
            {
                // update probabilities
                this.Weights[s] *= this.Factor;

                // normalize
                foreach (var e in edges)
                {
                    checked
                    {
                        this.Weights[e] /= outWeight;
                    }
                }


                successor = s;
                return true;
            }

            successor = default(TEdge);
            return false;
        }
    }
}
