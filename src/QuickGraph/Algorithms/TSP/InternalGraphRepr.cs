using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickGraph.Algorithms.TSP
{
    class InternalGraphRepr<TVertex, TEdge>
        where TEdge : EquatableEdge<TVertex>
    {

        Dictionary<TEdge, double> weights;
        BidirectionalGraph<TVertex, TEdge> graph;
        double minimumCost;
        TEdge edgeForSplit;

        public InternalGraphRepr(BidirectionalGraph<TVertex, TEdge> graph, Dictionary<TEdge, double> weights)
        {
            this.graph = new BidirectionalGraph<TVertex, TEdge>(graph);
            this.weights = new Dictionary<TEdge, double>(weights);
            minimumCost = 0;
        }

        public InternalGraphRepr<TVertex, TEdge> simplify()
        {
            double sum = 0;

            foreach (var v in graph.Vertices)
            {
                IEnumerable<TEdge> outEdges;
                if (graph.TryGetOutEdges(v, out outEdges))
                {
                    double min = outEdges.Min(edge => weights[edge]);
                    outEdges.ToList().ForEach(edge => weights[edge] -= min);
                    sum += min;
                }
                else
                {
                    sum = Double.MaxValue;
                }
            }

            foreach (var v in graph.Vertices)
            {
                IEnumerable<TEdge> inEdges;
                if (graph.TryGetInEdges(v, out inEdges))
                {
                    double min = inEdges.Min(edge => weights[edge]);
                    inEdges.ToList().ForEach(Edge => weights[Edge] -= min);
                    sum += min;
                }
                else
                {
                    sum = Double.MaxValue;
                }
            }

            minimumCost = sum;
            return this;
        }

        public void chooseEdgeForSplit()
        {
            List<TEdge> zeros = new List<TEdge>();

            foreach (var v in graph.Vertices)
            {
                IEnumerable<TEdge> outEdges;
                if (graph.TryGetOutEdges(v, out outEdges))
                {
                    zeros.AddRange(outEdges.Where(edge => weights[edge] == 0));
                }
            }

            TEdge edgeForSplit = null;
            double max = Double.MinValue;
            foreach (var edge in zeros)
            {
                var v1 = edge.Source;
                var v2 = edge.Target;
                IEnumerable<TEdge> row;
                IEnumerable<TEdge> column;

                var maxCandidate = 0.0;
                

                if (graph.TryGetOutEdges(v1, out row) && graph.TryGetInEdges(v2, out column))
                {
                    maxCandidate = row.Where(e => !e.Target.Equals(v2)).Min(e => weights[e]) 
                        + column.Where(e => !e.Source.Equals(v1)).Min(e => weights[e]);
                    
                    if (maxCandidate > max)
                    {
                        max = maxCandidate;
                        edgeForSplit = edge;
                    }
                }
            }
            this.edgeForSplit = edgeForSplit;
        }

        public void buildSplit()
        {
            TEdge edgeForSplit = this.edgeForSplit;
            var v1 = edgeForSplit.Source;
            var v2 = edgeForSplit.Target;

            var graph_take = this.graph.Clone();
            graph_take.ClearOutEdges(v1);
            graph_take.ClearInEdges(v2);
            var weights_take = new Dictionary<TEdge, double>(weights);

            var graph_drop = this.graph.Clone();
            var weights_drop = new Dictionary<TEdge, double>(weights);
            weights_drop[edgeForSplit] = double.MaxValue;
        }

    }
}
