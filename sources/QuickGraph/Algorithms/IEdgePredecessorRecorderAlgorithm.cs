using System;

namespace QuickGraph.Algorithms
{
    public interface IEdgePredecessorRecorderAlgorithm<Vertex, Edge>
        where Edge : IEdge<Vertex>
    {
        event EdgeEdgeEventHandler<Vertex, Edge> DiscoverTreeEdge;
        event EdgeEventHandler<Vertex,Edge> FinishEdge;
    }
}
