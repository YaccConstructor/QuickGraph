using System;

namespace QuickGraph
{
    /// <summary>
    /// An edge factory
    /// </summary>
    public delegate TEdge EdgeFactory<TVertex, TEdge>(TVertex source, TVertex target)
        where TEdge : IEdge<TVertex>;
}
