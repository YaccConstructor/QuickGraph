using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace QuickGraph
{
    internal static class GraphContracts
    {
        [Conditional("DEBUG")]
        public static void Assert(bool value)
        {
            if (!value)
                throw new InvalidOperationException();
        }

        [Conditional("DEBUG")]
        public static void Assert(bool value, string message)
        {
            if (!value)
                throw new InvalidOperationException(message);
        }

        [Conditional("DEBUG")]
        public static void AssumeNotNull<T>(T v, string parameterName)
        {
            if (object.Equals(v, null))
                throw new ArgumentNullException(parameterName);
        }

        [Conditional("DEBUG")]
        public static void Assume(bool value, string message)
        {
            if (!value)
                throw new ArgumentException(message);
        }

        [Conditional("DEBUG")]
        public static void AssumeInVertexSet<TVertex>(
            IVertexSet<TVertex> g, 
            TVertex v,
            string parameterName)
        {
            AssumeNotNull(g, "g");
            AssumeNotNull(v, parameterName);
            if (!g.ContainsVertex(v))
                throw new VertexNotFoundException(parameterName);
        }

        [Conditional("DEBUG")]
        public static void AssumeNotInVertexSet<TVertex>(
            IVertexSet<TVertex> g,
            TVertex v,
            string parameterName)
        {
            AssumeNotNull(g, "g");
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
            AssumeNotNull(g, "g");
            AssumeNotNull(e, parameterName);
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
