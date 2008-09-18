using System;

namespace QuickGraph
{
    public delegate TEdge EdgeFactory<TVertex, TEdge>(TVertex source, TVertex target)
        where TEdge : IEdge<TVertex>;
}
