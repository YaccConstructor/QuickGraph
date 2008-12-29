using System;
using System.Collections.Generic;

namespace QuickGraph
{
    public interface IMutableVertexListGraph<TVertex, TEdge> : 
        IMutableIncidenceGraph<TVertex, TEdge>,
        IMutableVertexSet<TVertex>
        where TEdge : IEdge<TVertex>
    {
    }
}
