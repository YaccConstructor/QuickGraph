using System;
using System.Collections.Generic;
using System.Text;

namespace QuickGraph
{
    public interface IIdentifiableEdgeFactory<TVertex,TEdge>
        where TEdge: IIdentifiable, IEdge<TVertex>
    {
        TEdge CreateEdge(string id, TVertex source, TVertex target);
    }
}
