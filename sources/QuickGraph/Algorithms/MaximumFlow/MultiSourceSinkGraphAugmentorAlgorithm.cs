using System;

namespace QuickGraph.Algorithms.MaximumFlow
{
    public sealed class MultiSourceSinkGraphAugmentorAlgorithm<TVertex, TEdge> :
        GraphAugmentorAlgorithmBase<TVertex, TEdge, IMutableBidirectionalGraph<TVertex, TEdge>>
        where TEdge : IEdge<TVertex>
    {
        public MultiSourceSinkGraphAugmentorAlgorithm(
            IMutableBidirectionalGraph<TVertex, TEdge> visitedGraph
            )
            : this(visitedGraph,
                FactoryCompiler.GetVertexFactory<TVertex>(),
                FactoryCompiler.GetEdgeFactory<TVertex, TEdge>()
                )
        { }

        public MultiSourceSinkGraphAugmentorAlgorithm(
            IMutableBidirectionalGraph<TVertex, TEdge> visitedGraph,
            IVertexFactory<TVertex> vertexFactory,
            IEdgeFactory<TVertex,TEdge> edgeFactory
            )
            :base(visitedGraph,vertexFactory,edgeFactory)
        {}

        protected override void AugmentGraph()
        {
            foreach (TVertex v in this.VisitedGraph.Vertices)
            {
                if (this.IsAborting)
                    return;

                // is source
                if (this.VisitedGraph.IsInEdgesEmpty(v))
                    this.AddAugmentedEdge(this.SuperSource, v);

                // is sink
                if (this.VisitedGraph.IsOutEdgesEmpty(v))
                    this.AddAugmentedEdge(v,this.SuperSink);
            }
        }
    }
}
