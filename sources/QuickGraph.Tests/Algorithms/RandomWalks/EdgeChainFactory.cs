using System;
using System.Collections.Generic;
using QuickGraph.Unit;

namespace QuickGraph.Algorithms.RandomWalks
{
    public class EdgeChainFactory
    {
        private IVertexAndEdgeListGraph<string, Edge<string>> g = new BidirectionalGraphFactory().RegularLattice10x10();

        [Factory]
        public KeyValuePair<
            IVertexAndEdgeListGraph<string,Edge<string>>,
            IEdgeChain<string,Edge<String>>>
            RoundRobin()
        {
            return new KeyValuePair<IVertexAndEdgeListGraph<string,Edge<string>>,IEdgeChain<string,Edge<String>>>(
                this.g,
                new RoundRobinEdgeChain<string, Edge<string>>()
                );
        }

        [Factory]
        public KeyValuePair<
            IVertexAndEdgeListGraph<string, Edge<string>>,
            IEdgeChain<string, Edge<String>>>
            NormalizedMarkov()
        {
                return new KeyValuePair<IVertexAndEdgeListGraph<string,Edge<string>>,IEdgeChain<string,Edge<String>>>(
                this.g,
                new NormalizedMarkovEdgeChain<string, Edge<string>>()
                );
        }

        [Factory]
        public KeyValuePair<
            IVertexAndEdgeListGraph<string, Edge<string>>,
            IEdgeChain<string, Edge<String>>>
            WeightedMarkov()
        {
            Dictionary<Edge<String>, double> weights = new Dictionary<Edge<String>, double>();
            int i=1;
            foreach (Edge<string> e in g.Edges)
                weights.Add(e,1 + i / g.EdgeCount * 3.0);
            return new KeyValuePair<IVertexAndEdgeListGraph<string,Edge<string>>,IEdgeChain<string,Edge<String>>>(
                this.g,
                new WeightedMarkovEdgeChain<string, Edge<string>>(weights)
                );
        }

        [Factory]
        public KeyValuePair<
            IVertexAndEdgeListGraph<string, Edge<string>>,
            IEdgeChain<string, Edge<String>>>
            VanishingWeightedMarkov()
        {
            Dictionary<Edge<String>, double> weights = new Dictionary<Edge<String>, double>();
            int i = 1;
            foreach (Edge<string> e in g.Edges)
                weights.Add(e, 1 + i / g.EdgeCount * 3.0);
            return new KeyValuePair<IVertexAndEdgeListGraph<string,Edge<string>>,IEdgeChain<string,Edge<String>>>(
                this.g,
                new VanishingWeightedMarkovEdgeChain<string, Edge<string>>(weights)
                );
        }
    }
}
