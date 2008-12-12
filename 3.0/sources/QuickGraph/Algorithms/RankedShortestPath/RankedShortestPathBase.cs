using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuickGraph.Algorithms.Services;
using QuickGraph.Algorithms.ShortestPath;
using System.Diagnostics.Contracts;

namespace QuickGraph.Algorithms.RankedShortestPath
{
    abstract class RankingShortestPathBase<TVertex, TEdge, TGraph>
        : RootedAlgorithmBase<TVertex, TGraph>
        where TEdge : IEdge<TVertex>
        where TGraph : IGraph<TVertex, TEdge>
    {
        readonly IDistanceRelaxer relaxer;

        public int K { get; set; }

        public IDistanceRelaxer Relaxer
        {
            get { return this.relaxer; }
        }

        protected RankingShortestPathBase(
            IAlgorithmComponent host, 
            TGraph visitedGraph,
            IDistanceRelaxer relaxer)
            : base(host, visitedGraph)
        {
            Contract.Requires(relaxer != null);
            this.relaxer = relaxer;
        }
    }
}
