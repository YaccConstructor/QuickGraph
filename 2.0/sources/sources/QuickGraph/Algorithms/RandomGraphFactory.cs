using System;
using System.Collections.Generic;

namespace QuickGraph.Algorithms
{
    [Serializable]
    public static class RandomGraphFactory
    {
        public static TVertex GetVertex<TVertex,TEdge>(IVertexListGraph<TVertex,TEdge> g, Random rnd)
            where TEdge : IEdge<TVertex>
        {
            if (g == null)
                throw new ArgumentNullException("g");
            if (rnd == null)
                throw new ArgumentNullException("random generator");
            if (g.VertexCount == 0)
                throw new ArgumentException("g is empty");
            return GetVertex<TVertex,TEdge>(g.Vertices, g.VertexCount, rnd);
        }

        public static TVertex GetVertex<TVertex,TEdge>(IEnumerable<TVertex> vertices, int count, Random rnd)
            where TEdge : IEdge<TVertex>
        {
            if (vertices == null)
                throw new ArgumentNullException("vertices");
            if (rnd == null)
                throw new ArgumentNullException("random generator");
            if (count == 0)
                throw new ArgumentException("vertices is empty");

            int i = rnd.Next(count);
            foreach (var v in vertices)
            {
                if (i == 0)
                    return v;
                else
                    --i;
            }

            // failed
            throw new InvalidOperationException("Could not find vertex");
        }

        public static TEdge GetEdge<TVertex, TEdge>(IEdgeSet<TVertex, TEdge> g, Random rnd)
            where TEdge : IEdge<TVertex>
        {
            if (g == null)
                throw new ArgumentNullException("g");
            if (rnd == null)
                throw new ArgumentNullException("random generator");
            if (g.EdgeCount == 0)
                throw new ArgumentException("g is empty");

            int i = rnd.Next(g.EdgeCount);
            foreach (var e in g.Edges)
            {
                if (i == 0)
                    return e;
                else
                    --i;
            }

            // failed
            throw new InvalidOperationException("Could not find edge");
        }

        public static TEdge GetEdge<TVertex,TEdge>(IEnumerable<TEdge> edges, int count, Random rnd)
            where TEdge : IEdge<TVertex>
        {
            if (edges == null)
                throw new ArgumentNullException("edges");
            if (rnd == null)
                throw new ArgumentNullException("random generator");
            if (count == 0)
                throw new ArgumentException("edges is empty");

            int i = rnd.Next(count);
            foreach (var e in edges)
            {
                if (i == 0)
                    return e;
                else
                    --i;
            }

            // failed
            throw new InvalidOperationException("Could not find edge");
        }

        public static void Create<TVertex, TEdge>(
            IMutableVertexAndEdgeListGraph<TVertex, TEdge> g,
            Random rnd,
            int vertexCount,
            int edgeCount,
            bool selfEdges
            ) where TEdge : IEdge<TVertex>
        {
            Create<TVertex, TEdge>(
                g,
                FactoryCompiler.GetVertexFactory<TVertex>(),
                FactoryCompiler.GetEdgeFactory<TVertex, TEdge>(),
                rnd,
                vertexCount,
                edgeCount,
                selfEdges
                );
        }

        public static void Create<TVertex, TEdge>(
            IMutableVertexAndEdgeListGraph<TVertex, TEdge> g,
            IVertexFactory<TVertex> vertexFactory,
            IEdgeFactory<TVertex,TEdge> edgeFactory,
            Random rnd,
            int vertexCount,
            int edgeCount,
            bool selfEdges
            ) where TEdge : IEdge<TVertex>
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


            TVertex a;
            TVertex b;
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
