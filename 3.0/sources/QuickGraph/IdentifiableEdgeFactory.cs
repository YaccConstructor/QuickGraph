using System;
using System.Collections.Generic;
using System.Text;

namespace QuickGraph
{
    public delegate TEdge IdentifiableEdgeFactory<TVertex, TEdge>(TVertex source, TVertex target, string id)
        where TEdge: IIdentifiable, IEdge<TVertex>;
}
