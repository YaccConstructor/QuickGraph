using System;

namespace QuickGraph
{
    /// <summary>
    /// A directed graph where vertices and edges can be enumerated efficiently
    /// </summary>
    /// <typeparam name="TVertex"></typeparam>
    /// <typeparam name="TEdge"></typeparam>
    public interface IVertexAndEdgeListGraph<TVertex,TEdge> :
        IVertexListGraph<TVertex,TEdge>,
        IEdgeListGraph<TVertex,TEdge>,
        IVertexAndEdgeSet<TVertex,TEdge>
        where TEdge : IEdge<TVertex>
    {}
}
