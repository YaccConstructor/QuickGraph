using System;

namespace QuickGraph.Algorithms
{
    public interface IDistanceRecorderAlgorithm<TVertex,TEdge>
        : ITreeBuilderAlgorithm<TVertex, TEdge>
        where TEdge : IEdge<TVertex>
    {
        event VertexEventHandler<TVertex> InitializeVertex;
        event VertexEventHandler<TVertex> DiscoverVertex;
    }
}
