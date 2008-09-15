using System;

namespace QuickGraph
{
    public interface IMutableIncidenceGraph<TVertex,TEdge> :
        IMutableGraph<TVertex,TEdge>,
        IIncidenceGraph<TVertex,TEdge>
        where TEdge : IEdge<TVertex>
    {
        int RemoveOutEdgeIf(
            TVertex v,
            EdgePredicate<TVertex, TEdge> predicate);
        void ClearOutEdges(TVertex v);

        void TrimEdgeExcess();
    }
}
