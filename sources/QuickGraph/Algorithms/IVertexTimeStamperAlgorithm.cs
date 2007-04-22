using System;

namespace QuickGraph.Algorithms
{
    public interface IVertexTimeStamperAlgorithm<Vertex,Edge>
        where Edge : IEdge<Vertex>
    {
        event VertexEventHandler<Vertex> DiscoverVertex;
        event VertexEventHandler<Vertex> FinishVertex;
    }
}
