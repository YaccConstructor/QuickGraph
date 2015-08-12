using System;

namespace QuickGraph
{
    /// <summary>
    /// A directed graph where vertices and edges can be enumerated efficiently.
    /// </summary>
    /// <typeparam name="TVertex">type of the vertices</typeparam>
    /// <typeparam name="TEdge">type of the edges</typeparam>
    public interface IVertexAndEdgeListGraph<TVertex,TEdge> 
        : IVertexListGraph<TVertex,TEdge>
        , IEdgeListGraph<TVertex,TEdge>
        where TEdge : IEdge<TVertex>
    {}
}
