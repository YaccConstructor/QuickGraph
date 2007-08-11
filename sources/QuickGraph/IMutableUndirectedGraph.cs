using System;
using System.Collections.Generic;

namespace QuickGraph
{
    public interface IMutableUndirectedGraph<Vertex,Edge> :
        IMutableEdgeListGraph<Vertex,Edge>,
        IUndirectedGraph<Vertex,Edge>
        where Edge : IEdge<Vertex>
    {
        int RemoveAdjacentEdgeIf(Vertex vertex, EdgePredicate<Vertex, Edge> predicate);
        void ClearAdjacentEdges(Vertex vertex);

    }
}
