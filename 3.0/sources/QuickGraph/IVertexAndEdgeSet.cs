using System;
using System.Collections.Generic;
using System.Text;

namespace QuickGraph
{
    /// <summary>
    /// A vertex and edge set.
    /// </summary>
    /// <typeparam name="TVertex">The type of the vertex.</typeparam>
    /// <typeparam name="TEdge">The type of the edge.</typeparam>
    public interface IVertexAndEdgeSet<TVertex,TEdge> :
        IVertexSet<TVertex>,
        IEdgeListGraph<TVertex,TEdge>
        where TEdge : IEdge<TVertex>
    {
    }
}
