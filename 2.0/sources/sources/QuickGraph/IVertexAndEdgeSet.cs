using System;
using System.Collections.Generic;
using System.Text;

namespace QuickGraph
{
    public interface IVertexAndEdgeSet<TVertex,TEdge> :
        IVertexSet<TVertex>,
        IEdgeListGraph<TVertex,TEdge>
        where TEdge : IEdge<TVertex>
    {
    }
}
