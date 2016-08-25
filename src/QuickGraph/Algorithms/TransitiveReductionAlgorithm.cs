/*
 * Code by Yoad Snapir <yoadsn@gmail.com> with a bit of refactoring from YC team
 * Taken from https://github.com/yoadsn/ArrowDiagramGenerator because PR was not opened
 * 
 * */

namespace QuickGraph.Algorithms
{
    using System.Collections.Generic;

    public class TransitiveReductionAlgorithm<TVertex, TEdge> : AlgorithmBase<BidirectionalGraph<TVertex, TEdge>> where TEdge : IEdge<TVertex>
    {
        public TransitiveReductionAlgorithm(
            BidirectionalGraph<TVertex, TEdge> visitedGraph
            )
            : base(visitedGraph)
        {
            TransitiveReduction = new BidirectionalGraph<TVertex, TEdge>();
        }

        public BidirectionalGraph<TVertex, TEdge> TransitiveReduction { get; private set; } //R# will say you do not need this. AppVeyor wants it.

        protected override void InternalCompute()
        {
            // Clone the visited graph
            TransitiveReduction.AddVerticesAndEdgeRange(VisitedGraph.Edges);

            var algo = new TransitiveAlgorithmHelper<TVertex, TEdge>(TransitiveReduction);
            algo.InternalCompute((g, u, v, e) =>
            {
                if (e != null) g.RemoveEdge(e);
            });
        }
    }
}