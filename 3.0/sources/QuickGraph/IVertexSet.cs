using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics.Contracts;

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
