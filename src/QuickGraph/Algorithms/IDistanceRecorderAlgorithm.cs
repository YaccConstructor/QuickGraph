using System;

namespace QuickGraph.Algorithms
{
    /// <summary>
    /// An algorithm that exposes events to compute a distance map between vertices
    /// </summary>
    /// <typeparam name="TVertex">type of the vertices</typeparam>
    /// <typeparam name="TEdge">type of the edges</typeparam>
    public interface IDistanceRecorderAlgorithm<TVertex,TEdge>
        where TEdge : IEdge<TVertex>
    {
        event VertexAction<TVertex> InitializeVertex;
        event VertexAction<TVertex> DiscoverVertex;
    }
}
