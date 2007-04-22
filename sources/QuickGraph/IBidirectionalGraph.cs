using System;
using System.Collections.Generic;

namespace QuickGraph
{
    public interface IBidirectionalGraph<Vertex,Edge> : 
        IVertexAndEdgeListGraph<Vertex,Edge>
        where Edge : IEdge<Vertex>
    {
        bool IsInEdgesEmpty(Vertex v);
        int InDegree(Vertex v);

        IEnumerable<Edge> InEdges(Vertex v);
        Edge InEdge(Vertex v, int index);

        int Degree(Vertex v);
    }
}
