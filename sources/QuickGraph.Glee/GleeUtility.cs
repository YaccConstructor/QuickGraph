using System;
using System.Collections.Generic;
using System.Text;

namespace QuickGraph.Glee
{
    public static class GleeGraphUtility
    {
        public static GleeGraphPopulator<Vertex, Edge> Create<Vertex, Edge>(
            IVertexAndEdgeSet<Vertex, Edge> visitedGraph,
            IFormatProvider formatProvider,
            string format)
            where Edge : IEdge<Vertex>
        {
            return new GleeToStringGraphPopulator<Vertex, Edge>(visitedGraph, formatProvider, format);
        }

        public static GleeGraphPopulator<Vertex, Edge> Create<Vertex, Edge>(
            IVertexAndEdgeSet<Vertex, Edge> visitedGraph)
            where Edge : IEdge<Vertex>
        {
            return new GleeDefaultGraphPopulator<Vertex, Edge>(visitedGraph);
        }

        public static GleeGraphPopulator<Vertex, Edge> CreateIdentifiable<Vertex, Edge>(
            IVertexAndEdgeSet<Vertex, Edge> visitedGraph)
            where Vertex : IIdentifiable
            where Edge : IEdge<Vertex>
        {
            return new GleeIndentifiableGraphPopulator<Vertex, Edge>(visitedGraph);
        }
    }
}
