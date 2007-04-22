using System;
using System.Collections.Generic;

namespace QuickGraph
{
    public interface IImplicitGraph<Vertex,Edge> : IGraph<Vertex,Edge>
        where Edge : IEdge<Vertex>
    {
        bool IsOutEdgesEmpty(Vertex v);
        int OutDegree(Vertex v);
        IEnumerable<Edge> OutEdges(Vertex v);
        Edge OutEdge(Vertex v, int index);
    }
}
