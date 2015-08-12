using System;
using QuickGraph.Algorithms.Services;

namespace QuickGraph.Algorithms.MaximumFlow
{
    public sealed class MultiSourceSinkGraphAugmentorAlgorithm<TVertex, TEdge> 
        : GraphAugmentorAlgorithmBase<TVertex, TEdge, IMutableBidirectionalGraph<TVertex, TEdge>>
        where TEdge : IEdge<TVertex>
    {
        public MultiSourceSinkGraphAugmentorAlgorithm(
            IMutableBidirectionalGraph<TVertex, TEdge> visitedGraph,
            VertexFactory<TVertex> vertexFactory,
            EdgeFactory<TVertex,TEdge> edgeFactory)
            :this(null, visitedGraph, vertexFactory, edgeFactory)
        {}

        public MultiSourceSinkGraphAugmentorAlgorithm(
            IAlgorithmComponent host,
            IMutableBidirectionalGraph<TVertex, TEdge> visitedGraph,
            VertexFactory<TVertex> vertexFactory,
            EdgeFactory<TVertex,TEdge> edgeFactory)
            :base(host, visitedGraph,vertexFactory,edgeFactory)
        {}

        protected override void AugmentGraph()
        {
            var cancelManager = this.Services.CancelManager;
            foreach (var v in this.VisitedGraph.Vertices)
            {
                if (cancelManager.IsCancelling) break;

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
