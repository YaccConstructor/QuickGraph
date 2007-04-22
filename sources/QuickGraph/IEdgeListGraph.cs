using System;
using System.Collections.Generic;

namespace QuickGraph
{
    public interface IEdgeListGraph<Vertex, Edge> : IGraph<Vertex, Edge>
        where Edge : IEdge<Vertex>
    {
        bool IsEdgesEmpty { get;}
        int EdgeCount { get;}
        IEnumerable<Edge> Edges { get;}
        bool ContainsEdge(Edge edge);
    }
}
