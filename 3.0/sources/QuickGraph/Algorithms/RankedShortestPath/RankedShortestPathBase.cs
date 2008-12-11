using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuickGraph.Algorithms.Services;

namespace QuickGraph.Algorithms.RankedShortestPath
{
    abstract class RankingShortestPathBase<TVertex, TEdge, TGraph>
        : RootedAlgorithmBase<TVertex, TGraph>
        where TEdge : IEdge<TVertex>
        where TGraph : IGraph<TVertex, TEdge>
    {
        public int K { get; set; }

        protected RankingShortestPathBase(IAlgorithmComponent host, TGraph visitedGraph)
            : base(host, visitedGraph)
        { }
    }
}
