using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Diagnostics.Contracts;

namespace QuickGraph
{
    /// <summary>
    /// Debug only assertions and assumptions
    /// </summary>
    public static class GraphContracts
    {
        [Conditional("DEBUG")]
        public static void AssumeInVertexSet<TVertex>(
            IVertexSet<TVertex> g, 
            TVertex v,
            string parameterName)
        {
            CodeContract.Requires(g != null);
            AssumeNotNull(v, parameterName);
            if (!g.ContainsVertex(v))
                throw new VertexNotFoundException(parameterName);
        }

        [Conditional("DEBUG")]
        private static void AssumeNotNull<TVertex>(TVertex v, string parameterName)
        {
            if (v == null)
                throw new ArgumentNullException(parameterName);
            CodeContract.EndContractBlock();
        }

        [Conditional("DEBUG")]
        public static void AssumeNotInVertexSet<TVertex>(
            IVertexSet<TVertex> g,
            TVertex v,
            string parameterName)
        {
            CodeContract.Requires(g != null);

            AssumeNotNull(v, parameterName);
            if (g.ContainsVertex(v))
                throw new ArgumentException("vertex already in set", parameterName);
        }

        [Conditional("DEBUG")]
        public static void AssumeInVertexSet<TVertex, TEdge>(
            IVertexAndEdgeSet<TVertex, TEdge> g,
            TEdge e,
            string parameterName)
            where TEdge : IEdge<TVertex>
        {
            CodeContract.Requires(g != null);
            if (e == null)
                throw new ArgumentNullException(parameterName);
            CodeContract.EndContractBlock();
            AssumeInVertexSet<TVertex>(g, e.Source, parameterName + ".Source");
            AssumeInVertexSet<TVertex>(g, e.Target, parameterName + ".Target");
        }

        [Conditional("DEBUG")]
        public static void AssumeInEdgeSet<TVertex, TEdge>(
            IVertexAndEdgeSet<TVertex, TEdge> g,
            TEdge e,
            string parameterName)
            where TEdge : IEdge<TVertex>
        {
            AssumeInVertexSet(g, e, parameterName);
            if (!g.ContainsEdge(e))
                throw new EdgeNotFoundException(parameterName);
        }

    }
}
