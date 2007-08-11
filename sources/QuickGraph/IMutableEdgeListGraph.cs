using System;
using System.Collections.Generic;

namespace QuickGraph
{
    public interface IMutableEdgeListGraph<Vertex, Edge> : 
        IMutableGraph<Vertex, Edge>,
        IEdgeListGraph<Vertex,Edge>
        where Edge : IEdge<Vertex>
    {
        bool AddEdge(Edge edge);
        event EdgeEventHandler<Vertex, Edge> EdgeAdded;

        bool RemoveEdge(Edge edge);
        event EdgeEventHandler<Vertex, Edge> EdgeRemoved;

        int RemoveEdgeIf(EdgePredicate<Vertex,Edge> predicate);
    }
}
