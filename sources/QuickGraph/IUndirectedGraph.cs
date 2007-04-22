using System;
using System.Collections.Generic;

namespace QuickGraph
{
    public interface IUndirectedGraph<Vertex,Edge> :
        IVertexAndEdgeSet<Vertex,Edge>,
        IGraph<Vertex,Edge>
        where Edge : IEdge<Vertex>
    {
        IEnumerable<Edge> AdjacentEdges(Vertex v);
        int AdjacentDegree(Vertex v);
        bool IsAdjacentEdgesEmpty(Vertex v);
        Edge AdjacentEdge(Vertex v, int index);

        bool ContainsEdge(Vertex source, Vertex target);
    }
}
