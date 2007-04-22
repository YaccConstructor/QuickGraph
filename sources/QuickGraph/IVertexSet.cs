using System;
using System.Collections.Generic;
using System.Text;

namespace QuickGraph
{
    public interface IVertexSet<Vertex, Edge>
        where Edge : IEdge<Vertex>
    {
        bool IsVerticesEmpty { get;}
        int VertexCount { get;}
        IEnumerable<Vertex> Vertices { get;}
        bool ContainsVertex(Vertex vertex);
    }
}
