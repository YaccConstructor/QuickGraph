using System;

namespace QuickGraph.Algorithms.MaximumFlow
{
    public sealed class MultiSourceSinkGraphAugmentorAlgorithm<Vertex, Edge> :
        GraphAugmentorAlgorithmBase<Vertex, Edge, IMutableBidirectionalGraph<Vertex, Edge>>
        where Edge : IEdge<Vertex>
    {
        public MultiSourceSinkGraphAugmentorAlgorithm(
            IMutableBidirectionalGraph<Vertex, Edge> visitedGraph
            )
            : this(visitedGraph,
                FactoryCompiler.GetVertexFactory<Vertex>(),
                FactoryCompiler.GetEdgeFactory<Vertex, Edge>()
                )
        { }

        public MultiSourceSinkGraphAugmentorAlgorithm(
            IMutableBidirectionalGraph<Vertex, Edge> visitedGraph,
            IVertexFactory<Vertex> vertexFactory,
            IEdgeFactory<Vertex,Edge> edgeFactory
            )
            :base(visitedGraph,vertexFactory,edgeFactory)
        {}

        protected override void AugmentGraph()
        {
            foreach (Vertex v in this.VisitedGraph.Vertices)
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
