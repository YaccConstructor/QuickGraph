using System;
using System.Collections.Generic;

namespace QuickGraph
{
    /// <summary>
    /// A directed graph datastructure where out-edges can be traversed,
    /// i.e. a vertex set + implicit graph.
    /// </summary>
    /// <typeparam name="TVertex">The type of the vertex.</typeparam>
    /// <typeparam name="TEdge">The type of the edge.</typeparam>
    public interface IVertexListGraph<TVertex, TEdge> 
        : IIncidenceGraph<TVertex, TEdge>
        , IVertexSet<TVertex>
        where TEdge : IEdge<TVertex>
    {
    }
}
