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
        where TEdge : IEdge<TVertex>
        where TGraph : IVertexAndEdgeListGraph<TVertex, TEdge>
    {

        public TSP(TGraph visitedGraph, Func<TEdge, double> weights)
            :base(null, visitedGraph, weights, DistanceRelaxers.ShortestDistance)
        {}

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
            throw new NotImplementedException();
        }
    }
}
