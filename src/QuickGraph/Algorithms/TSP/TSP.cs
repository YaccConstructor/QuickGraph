using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using QuickGraph.Algorithms.Services;
using QuickGraph.Algorithms.ShortestPath;

namespace QuickGraph.Algorithms.TSP
{
    public class TSP<TVertex, TEdge, TGraph> : ShortestPathAlgorithmBase<TVertex, TEdge
        , TGraph>
        , ITreeBuilderAlgorithm<TVertex, TEdge>
        where TGraph : BidirectionalGraph<TVertex, TEdge>
        where TEdge : EquatableEdge<TVertex>
    {
        private InternalGraphRepr<TVertex, TEdge> graph;
        private readonly Dictionary<TEdge, double> weights;

        public TSP(TGraph visitedGraph, Dictionary<TEdge, double> weights)
            :base(null, visitedGraph, edge => weights[edge], DistanceRelaxers.ShortestDistance)
        {
            this.weights = weights;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void Clean()
        {
            base.Clean();
        }

        protected override void InternalCompute()
        {
            graph = new InternalGraphRepr<TVertex, TEdge>(VisitedGraph, weights);
            graph.simplify();
            graph.chooseEdgeForSplit();
            graph.buildSplit();
        }

    }
}
