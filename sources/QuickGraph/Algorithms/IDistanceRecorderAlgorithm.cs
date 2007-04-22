using System;

namespace QuickGraph.Algorithms
{
    public interface IDistanceRecorderAlgorithm<Vertex,Edge>
        where Edge : IEdge<Vertex>
    {
        event VertexEventHandler<Vertex> InitializeVertex;
        event VertexEventHandler<Vertex> DiscoverVertex;
        event EdgeEventHandler<Vertex, Edge> TreeEdge;
    }
}
