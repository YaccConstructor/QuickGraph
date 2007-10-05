using System;
using System.Collections.Generic;

namespace QuickGraph
{
    public interface IUndirectedGraph<TVertex,TEdge> :
        IVertexAndEdgeSet<TVertex,TEdge>,
        IGraph<TVertex,TEdge>
        where TEdge : IEdge<TVertex>
    {
        IEnumerable<TEdge> AdjacentEdges(TVertex v);
        int AdjacentDegree(TVertex v);
        bool IsAdjacentEdgesEmpty(TVertex v);
        TEdge AdjacentEdge(TVertex v, int index);

        bool ContainsEdge(TVertex source, TVertex target);
    }
}
