using System;
using System.Diagnostics.Contracts;

namespace QuickGraph
{
    /// <summary>
    /// The handler for events involving two edges
    /// </summary>
    public delegate void EdgeEdgeAction<TVertex, TEdge>(TEdge edge, TEdge targetEdge)
        where TEdge : IEdge<TVertex>;
}
