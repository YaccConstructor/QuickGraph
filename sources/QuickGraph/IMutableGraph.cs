using System;

namespace QuickGraph
{
    public interface IMutableGraph<TVertex,TEdge> : IGraph<TVertex,TEdge>
        where TEdge : IEdge<TVertex>
    {
        void Clear();
    }
}
