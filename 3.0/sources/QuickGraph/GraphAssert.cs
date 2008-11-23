using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;
using QuickGraph.Contracts;

namespace QuickGraph
{
    public static class GraphAssert
    {
        public static void VertexCountEqual<TVertex>(
            this IVertexSet<TVertex> left,
            IVertexSet<TVertex> right)
        {
            CodeContract.Requires(left != null);
            CodeContract.Requires(right != null);

            CodeContract.Assert(left.VertexCount == right.VertexCount);
        }

        public static void EdgeCountEqual<TVertex, TEdge>(
            this IVertexAndEdgeSet<TVertex, TEdge> left,
            IVertexAndEdgeSet<TVertex, TEdge> right)
            where TEdge : IEdge<TVertex>
        {
            CodeContract.Requires(left != null);
            CodeContract.Requires(right != null);

            CodeContract.Assert(left.EdgeCount == right.EdgeCount);
        }
    }
}
