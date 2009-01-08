using System;

namespace QuickGraph.Algorithms
{
    public interface IDistanceRecorderAlgorithm<TVertex,TEdge>
        where TEdge : IEdge<TVertex>
    {
        event VertexAction<TVertex> InitializeVertex;
        event VertexAction<TVertex> DiscoverVertex;
    }
}
