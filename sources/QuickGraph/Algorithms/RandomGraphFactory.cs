using System;
using System.Collections.Generic;

namespace QuickGraph.Algorithms
{
    [Serializable]
    public static class RandomGraphFactory
    {
        public static Vertex GetVertex<Vertex,Edge>(IVertexListGraph<Vertex,Edge> g, Random rnd)
            where Edge : IEdge<Vertex>
        {
            if (g == null)
                throw new ArgumentNullException("g");
            if (rnd == null)
                throw new ArgumentNullException("random generator");
            if (g.VertexCount == 0)
                throw new ArgumentException("g is empty");
            return GetVertex<Vertex,Edge>(g.Vertices, g.VertexCount, rnd);
        }

        public static Vertex GetVertex<Vertex,Edge>(IEnumerable<Vertex> vertices, int count, Random rnd)
            where Edge : IEdge<Vertex>
        {
            if (vertices == null)
                throw new ArgumentNullException("vertices");
            if (rnd == null)
                throw new ArgumentNullException("random generator");
            if (count == 0)
                throw new ArgumentException("vertices is empty");

            int i = rnd.Next(count);
            foreach (Vertex v in vertices)
            {
                if (i == 0)
                    return v;
                else
                    --i;
            }

            // failed
            throw new InvalidOperationException("Could not find vertex");
        }

        public static Edge GetEdge<Vertex,Edge>(IEdgeListGraph<Vertex,Edge> g, Random rnd)
            where Edge : IEdge<Vertex>
        {
            if (g == null)
                throw new ArgumentNullException("g");
            if (rnd == null)
                throw new ArgumentNullException("random generator");
            if (g.EdgeCount == 0)
                throw new ArgumentException("g is empty");

            int i = rnd.Next(g.EdgeCount);
            foreach (Edge e in g.Edges)
            {
                if (i == 0)
                    return e;
                else
                    --i;
            }

            // failed
            throw new InvalidOperationException("Could not find edge");
        }

        public static Edge GetEdge<Vertex,Edge>(IEnumerable<Edge> edges, int count, Random rnd)
            where Edge : IEdge<Vertex>
        {
            if (edges == null)
                throw new ArgumentNullException("edges");
            if (rnd == null)
                throw new ArgumentNullException("random generator");
            if (count == 0)
                throw new ArgumentException("edges is empty");

            int i = rnd.Next(count);
            foreach (Edge e in edges)
            {
                if (i == 0)
                    return e;
                else
                    --i;
            }

            // failed
            throw new InvalidOperationException("Could not find edge");
        }

        public static void Create<Vertex, Edge>(
            IMutableVertexAndEdgeListGraph<Vertex, Edge> g,
            Random rnd,
            int vertexCount,
            int edgeCount,
            bool selfEdges
            ) where Edge : IEdge<Vertex>
        {
            Create<Vertex, Edge>(
                g,
                FactoryCompiler.GetVertexFactory<Vertex>(),
                FactoryCompiler.GetEdgeFactory<Vertex, Edge>(),
                rnd,
                vertexCount,
                edgeCount,
                selfEdges
                );
        }

        public static void Create<Vertex, Edge>(
            IMutableVertexAndEdgeListGraph<Vertex, Edge> g,
            IVertexFactory<Vertex> vertexFactory,
            IEdgeFactory<Vertex,Edge> edgeFactory,
            Random rnd,
            int vertexCount,
            int edgeCount,
            bool selfEdges
            ) where Edge : IEdge<Vertex>
        {
            if (g == null)
                throw new ArgumentNullException("g");
            if (vertexFactory == null)
                throw new ArgumentNullException("vertexFactory");
            if (edgeFactory == null)
                throw new ArgumentNullException("edgeFactory");
            if (rnd == null)
                throw new ArgumentNullException("random generator");


            for (int i = 0; i < vertexCount; ++i)
                g.AddVertex( vertexFactory.CreateVertex() );


            Vertex a;
            Vertex b;
            int j = 0;
            while (j < edgeCount)
            {
                a = GetVertex(g, rnd);
                do
                {
                    b = GetVertex(g, rnd);
                }
                while (selfEdges == false && a.Equals(b));

                if (g.AddEdge( edgeFactory.CreateEdge(a,b)))
                      ++j;
            }
        }
    }
}
