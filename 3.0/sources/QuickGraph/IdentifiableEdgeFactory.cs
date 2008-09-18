using System;
using System.Collections.Generic;
using System.Text;

namespace QuickGraph
{
    public delegate TEdge IdentifiableEdgeFactory<TVertex, TEdge>(string id, TVertex source, TVertex target)
        where TEdge: IIdentifiable, IEdge<TVertex>;
}
