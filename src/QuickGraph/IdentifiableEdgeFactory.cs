using System;
using System.Collections.Generic;
using System.Text;

namespace QuickGraph
{
    /// <summary>
    /// A factory of identifiable edges.
    /// </summary>
    public delegate TEdge IdentifiableEdgeFactory<TVertex, TEdge>(TVertex source, TVertex target, string id)
        where TEdge: IEdge<TVertex>;
}
