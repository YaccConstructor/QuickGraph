using System;
using System.Collections.Generic;
using System.Text;

namespace QuickGraph
{
    public interface IVertexAndEdgeSet<Vertex,Edge> :
        IVertexSet<Vertex>,
        IEdgeListGraph<Vertex,Edge>
        where Edge : IEdge<Vertex>
    {
    }
}
