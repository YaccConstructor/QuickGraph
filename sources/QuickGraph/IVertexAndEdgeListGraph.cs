using System;

namespace QuickGraph
{
    public interface IVertexAndEdgeListGraph<TVertex,TEdge> :
        IVertexListGraph<TVertex,TEdge>,
        IEdgeListGraph<TVertex,TEdge>,
        IVertexAndEdgeSet<TVertex,TEdge>
        where TEdge : IEdge<TVertex>
    {}
}
