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
        public static void AssumeInVertexSet<Vertex>(
            IVertexSet<Vertex> g, 
            Vertex v,
            string parameterName)
        {
            AssumeNotNull(g, "g");
            AssumeNotNull(v, parameterName);
            if (!g.ContainsVertex(v))
                throw new VertexNotFoundException(parameterName);
        }

        [Conditional("DEBUG")]
        public static void AssumeNotInVertexSet<Vertex>(
            IVertexSet<Vertex> g,
            Vertex v,
            string parameterName)
        {
            AssumeNotNull(g, "g");
            AssumeNotNull(v, parameterName);
            if (g.ContainsVertex(v))
                throw new ArgumentException("vertex already in set", parameterName);
        }

        [Conditional("DEBUG")]
        public static void AssumeInVertexSet<Vertex, Edge>(
            IVertexAndEdgeSet<Vertex, Edge> g,
            Edge e,
            string parameterName)
            where Edge : IEdge<Vertex>
        {
            AssumeNotNull(g, "g");
            AssumeNotNull(e, parameterName);
            AssumeInVertexSet<Vertex>(g, e.Source, parameterName + ".Source");
            AssumeInVertexSet<Vertex>(g, e.Target, parameterName + ".Target");
        }

        [Conditional("DEBUG")]
        public static void AssumeInEdgeSet<Vertex, Edge>(
            IVertexAndEdgeSet<Vertex, Edge> g,
            Edge e,
            string parameterName)
            where Edge : IEdge<Vertex>
        {
            AssumeInVertexSet(g, e, parameterName);
            if (!g.ContainsEdge(e))
                throw new EdgeNotFoundException(parameterName);
        }

    }
}
