using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace QuickGraph
{
    internal static class GraphContracts
    {
        [Conditional("DEBUG")]
        public static void ValidateVertex<Vertex>(
            IVertexSet<Vertex> g, 
            Vertex v)
        {

            if (object.Equals(v, null))
                throw new ArgumentNullException("source");
            if (!g.ContainsVertex(v))
                throw new VertexNotFoundException("source");
        }
    }
}
