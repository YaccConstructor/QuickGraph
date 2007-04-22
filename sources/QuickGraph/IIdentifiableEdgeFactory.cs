using System;
using System.Collections.Generic;
using System.Text;

namespace QuickGraph
{
    public interface IIdentifiableEdgeFactory<Vertex,Edge>
        where Edge: IIdentifiable, IEdge<Vertex>
    {
        Edge CreateEdge(string id, Vertex source, Vertex target);
    }
}
