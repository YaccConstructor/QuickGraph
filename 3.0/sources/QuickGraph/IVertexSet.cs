using System;
using System.Collections.Generic;
using System.Text;

namespace QuickGraph
{
    public interface IVertexSet<TVertex>
    {
        bool IsVerticesEmpty { get;}
        int VertexCount { get;}
        IEnumerable<TVertex> Vertices { get;}
        bool ContainsVertex(TVertex vertex);
    }
}
