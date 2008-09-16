using System;
using System.Collections.Generic;
using System.Text;

namespace QuickGraph
{
    public static class TraversalExtensions
    {
        public static TVertex GetFirstVertexOrDefault<TVertex,TEdge>(
            this IVertexListGraph<TVertex, TEdge> g)
            where TEdge : IEdge<TVertex>
        {
            foreach (var v in g.Vertices)
                return v;
            return default(TVertex);
        }

        public static TVertex GetFirstVertexOrDefault<TVertex, TEdge>(
            this IUndirectedGraph<TVertex, TEdge> g)
            where TEdge : IEdge<TVertex>
        {
            foreach (var v in g.Vertices)
                return v;
            return default(TVertex);
        }
    }
}
