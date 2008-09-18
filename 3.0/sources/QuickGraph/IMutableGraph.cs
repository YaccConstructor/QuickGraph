using System;

namespace QuickGraph
{
    /// <summary>
    /// A mutable graph instance
    /// </summary>
    /// <typeparam name="TVertex"></typeparam>
    /// <typeparam name="TEdge"></typeparam>
    public interface IMutableGraph<TVertex,TEdge> 
        : IGraph<TVertex,TEdge>
        where TEdge : IEdge<TVertex>
    {
        void Clear();
    }
}
