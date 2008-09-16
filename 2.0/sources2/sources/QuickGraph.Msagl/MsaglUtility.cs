using System;
using System.Collections.Generic;
using System.Text;

namespace QuickGraph.Msagl
{
    public static class MsaglGraphUtility
    {
        public static MsaglGraphPopulator<TVertex, TEdge> Create<TVertex, TEdge>(
            IVertexAndEdgeSet<TVertex, TEdge> visitedGraph,
            IFormatProvider formatProvider,
            string format)
            where TEdge : IEdge<TVertex>
        {
            return new MsaglToStringGraphPopulator<TVertex, TEdge>(visitedGraph, formatProvider, format);
        }

        public static MsaglGraphPopulator<TVertex, TEdge> Create<TVertex, TEdge>(
            IVertexAndEdgeSet<TVertex, TEdge> visitedGraph)
            where TEdge : IEdge<TVertex>
        {
            return new MsaglDefaultGraphPopulator<TVertex, TEdge>(visitedGraph);
        }

        public static MsaglGraphPopulator<TVertex, TEdge> CreateIdentifiable<TVertex, TEdge>(
            IVertexAndEdgeSet<TVertex, TEdge> visitedGraph)
            where TVertex : IIdentifiable
            where TEdge : IEdge<TVertex>
        {
            return new MsaglIndentifiableGraphPopulator<TVertex, TEdge>(visitedGraph);
        }
    }
}
