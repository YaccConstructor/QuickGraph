using System;
using System.Collections.Generic;

namespace QuickGraph
{
    public delegate bool EdgePredicate<TVertex, TEdge>(TEdge e)
        where TEdge : IEdge<TVertex>;

    public delegate bool VertexPredicate<TVertex>(TVertex v);

    public interface IMutableVertexListGraph<TVertex, TEdge> : 
        IMutableIncidenceGraph<TVertex, TEdge>,
        IVertexListGraph<TVertex,TEdge>
        where TEdge : IEdge<TVertex>
    {
        event VertexEventHandler<TVertex> VertexAdded;
        void AddVertex(TVertex v);
        void AddVertexRange(IEnumerable<TVertex> vertices);

        event VertexEventHandler<TVertex> VertexRemoved;
        bool RemoveVertex(TVertex v);
        int RemoveVertexIf(VertexPredicate<TVertex> pred);
    }
}
