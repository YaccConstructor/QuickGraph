using System;
using System.Diagnostics.Contracts;

namespace QuickGraph
{
    /// <summary>
    /// The handler for events with <see cref="EdgeEdgeEventArgs&lt;TVertex, TEdge&gt;"/>
    /// </summary>
    public delegate void EdgeEdgeAction<TVertex, TEdge>(TEdge edge, TEdge targetEdge)
        where TEdge : IEdge<TVertex>;
}
