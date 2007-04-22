using System;

namespace QuickGraph.Algorithms.MaximumFlow
{
    public sealed class AllVerticesGraphAugmentorAlgorithm<Vertex,Edge> :
        GraphAugmentorAlgorithmBase<Vertex,Edge,IMutableVertexAndEdgeListGraph<Vertex,Edge>>
        where Edge : IEdge<Vertex>
    {
        public AllVerticesGraphAugmentorAlgorithm(
            IMutableVertexAndEdgeListGraph<Vertex, Edge> visitedGraph
            )
            : this(visitedGraph,
                FactoryCompiler.GetVertexFactory<Vertex>(),
                FactoryCompiler.GetEdgeFactory<Vertex, Edge>()
                )
        { }

        public AllVerticesGraphAugmentorAlgorithm(
            IMutableVertexAndEdgeListGraph<Vertex, Edge> visitedGraph,
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

                this.AddAugmentedEdge(this.SuperSource, v);
                this.AddAugmentedEdge(v, this.SuperSink);
            }
        }
    }
}
