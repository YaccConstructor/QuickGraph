using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuickGraph
{
    public class EquateGraphs
    {
        public static bool Equate<V, E>(IVertexAndEdgeListGraph<V, E> g, IVertexAndEdgeListGraph<V, E> h,
            IEqualityComparer<V> vertexEquality, IEqualityComparer<E> edgeEquality)
            where E : IEdge<V>
        {
            if (ReferenceEquals(g, null))
                return ReferenceEquals(h, null);
            if (ReferenceEquals(h, null))
                return ReferenceEquals(g, null);

            if (ReferenceEquals(g, h))
                return true;

            if (g.VertexCount != h.VertexCount)
                return false;

            if (g.EdgeCount != h.EdgeCount)
                return false;

            foreach (var v in g.Vertices)
            {
                if (!h.Vertices.Any(u => vertexEquality.Equals(u, v)))
                    return false;
            }

            foreach (var e in g.Edges)
            {
                if (!h.Edges.Any(f => edgeEquality.Equals(e, f)))
                    return false;
            }

            return true;
        }

        public static bool Equate<V, E>(IVertexAndEdgeListGraph<V, E> g, IVertexAndEdgeListGraph<V, E> h)
            where E : IEdge<V>
        {
            return Equate(g, h, EqualityComparer<V>.Default, EqualityComparer<E>.Default);
        }
    }
}
