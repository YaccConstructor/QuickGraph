using System;
using System.Collections.Generic;

namespace QuickGraph.Algorithms
{
    public class TransitiveClosureAlgorithm<TVertex, TEdge> : AlgorithmBase<BidirectionalGraph<TVertex, TEdge>> where TEdge : IEdge<TVertex>
    {
        public TransitiveClosureAlgorithm(
            BidirectionalGraph<TVertex, TEdge> visitedGraph,
            Func<TVertex, TVertex, TEdge> createEdge
            )
            : base(visitedGraph)
        {
            TransitiveClosure = new BidirectionalGraph<TVertex, TEdge>();
            _createEdge = createEdge;
        }

        public BidirectionalGraph<TVertex, TEdge> TransitiveClosure { get; private set; } //R# will say you do not need this. AppVeyor wants it.

        private readonly Func<TVertex, TVertex, TEdge> _createEdge;

        protected override void InternalCompute()
        {
            // Clone the visited graph
            TransitiveClosure.AddVerticesAndEdgeRange(VisitedGraph.Edges);

            var algo = new TransitiveAlgorithmHelper<TVertex, TEdge>(TransitiveClosure);
            algo.InternalCompute((g, u, v, e) =>
            {
                if (e == null) g.AddEdge(_createEdge(u, v));
            });
        }
    }
}
