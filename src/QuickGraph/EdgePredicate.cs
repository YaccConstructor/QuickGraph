using System;
using System.Diagnostics.Contracts;

namespace QuickGraph
{
    [Pure]
    public delegate bool EdgePredicate<TVertex, TEdge>(TEdge e)
        where TEdge : IEdge<TVertex>;
}
