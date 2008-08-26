using System;

namespace QuickGraph.Algorithms.MaximumFlow
{
    public sealed class AllVerticesGraphAugmentorAlgorithm<TVertex,TEdge> :
        GraphAugmentorAlgorithmBase<TVertex,TEdge,IMutableVertexAndEdgeListGraph<TVertex,TEdge>>
        where TEdge : IEdge<TVertex>
    {
        public AllVerticesGraphAugmentorAlgorithm(
            IMutableVertexAndEdgeListGraph<TVertex, TEdge> visitedGraph
            )
            : this(visitedGraph,
                FactoryCompiler.GetVertexFactory<TVertex>(),
                FactoryCompiler.GetEdgeFactory<TVertex, TEdge>()
                )
        { }

        public AllVerticesGraphAugmentorAlgorithm(
            IMutableVertexAndEdgeListGraph<TVertex, TEdge> visitedGraph,
            IVertexFactory<TVertex> vertexFactory,
            IEdgeFactory<TVertex,TEdge> edgeFactory
            )
            :base(visitedGraph,vertexFactory,edgeFactory)
        {}

        protected override void AugmentGraph()
        {
            foreach (var v in this.VisitedGraph.Vertices)
            {
                if (this.IsAborting)
                    return;

                this.AddAugmentedEdge(this.SuperSource, v);
                this.AddAugmentedEdge(v, this.SuperSink);
            }
        }
    }
}
