using System;
using System.Collections.Generic;

namespace QuickGraph
{
    public interface IMutableVertexListGraph<Vertex, Edge> : 
        IMutableIncidenceGraph<Vertex, Edge>,
        IVertexListGraph<Vertex,Edge>
        where Edge : IEdge<Vertex>
    {
        event VertexEventHandler<Vertex> VertexAdded;
        void AddVertex(Vertex v);
        void AddVertexRange(IEnumerable<Vertex> vertices);

        event VertexEventHandler<Vertex> VertexRemoved;
        bool RemoveVertex(Vertex v);
        int RemoveVertexIf(IVertexPredicate<Vertex> pred);
    }
}
