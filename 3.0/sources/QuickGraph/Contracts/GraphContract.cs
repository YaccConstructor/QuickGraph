using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Diagnostics.Contracts;

namespace QuickGraph.Contracts
{
    /// <summary>
    /// Debug only assertions and assumptions
    /// </summary>
    public static class GraphContract
    {
        [Pure, Conditional("CONTRACTS_PRECONDITIONS"), Conditional("CONTRACTS_FULL")]
        public static void RequiresInVertexSet<TVertex>(
            IVertexSet<TVertex> g, 
            TVertex v)
        {
            CodeContract.Requires(g != null);
            CodeContract.Requires(v != null);
            // todo make requires
            CodeContract.Requires(g.ContainsVertex(v));
        }

        [Pure, Conditional("CONTRACTS_PRECONDITIONS"), Conditional("CONTRACTS_FULL")]
        public static void RequiresNotInVertexSet<TVertex>(
            IVertexSet<TVertex> g,
            TVertex v)
        {
            CodeContract.Requires(g != null);
            CodeContract.Requires(v != null);
            // todo make requires
            CodeContract.Requires(!g.ContainsVertex(v));
        }

        [Pure, Conditional("CONTRACTS_PRECONDITIONS"), Conditional("CONTRACTS_FULL")]
        public static void RequiresInVertexSet<TVertex, TEdge>(
            IVertexAndEdgeSet<TVertex, TEdge> g,
            TEdge e)
            where TEdge : IEdge<TVertex>
        {
            CodeContract.Requires(g != null);
            CodeContract.Requires(e != null);

            RequiresInVertexSet<TVertex>(g, e.Source);
            RequiresInVertexSet<TVertex>(g, e.Target);
        }

        [Pure, Conditional("CONTRACTS_PRECONDITIONS"), Conditional("CONTRACTS_FULL")]
        public static void RequiresInEdgeSet<TVertex, TEdge>(
            IVertexAndEdgeSet<TVertex, TEdge> g,
            TEdge e)
            where TEdge : IEdge<TVertex>
        {
            CodeContract.Requires(g != null);
            CodeContract.Requires(e != null);
            RequiresInVertexSet(g, e);
            CodeContract.Requires(g.ContainsEdge(e));
        }

    }
}
