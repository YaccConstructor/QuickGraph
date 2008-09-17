using System;
using System.Collections.Generic;
using System.Text;

namespace QuickGraph.Glee
{
    public static class GleeGraphExtensions
    {
        public static GleeGraphPopulator<TVertex, TEdge> CreateGleePopulator<TVertex, TEdge>(
            this IVertexAndEdgeSet<TVertex, TEdge> visitedGraph,
            IFormatProvider formatProvider,
            string format)
            where TEdge : IEdge<TVertex>
        {
            return new GleeToStringGraphPopulator<TVertex, TEdge>(visitedGraph, formatProvider, format);
        }

        public static GleeGraphPopulator<TVertex, TEdge> CreateGleePopulator<TVertex, TEdge>(
            this IVertexAndEdgeSet<TVertex, TEdge> visitedGraph)
            where TEdge : IEdge<TVertex>
        {
            return new GleeDefaultGraphPopulator<TVertex, TEdge>(visitedGraph);
        }

        public static GleeGraphPopulator<TVertex, TEdge> CreateIdentifiable<TVertex, TEdge>(
            this IVertexAndEdgeSet<TVertex, TEdge> visitedGraph)
            where TVertex : IIdentifiable
            where TEdge : IEdge<TVertex>
        {
            return new GleeIndentifiableGraphPopulator<TVertex, TEdge>(visitedGraph);
        }
    }
}
