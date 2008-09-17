using System;
using System.Collections.Generic;
using System.Text;

namespace QuickGraph.Msagl
{
    public static class MsaglGraphExtensions
    {
        public static MsaglGraphPopulator<TVertex, TEdge> CreateMsaglPopulator<TVertex, TEdge>(
            this IVertexAndEdgeSet<TVertex, TEdge> visitedGraph,
            IFormatProvider formatProvider,
            string format)
            where TEdge : IEdge<TVertex>
        {
            return new MsaglToStringGraphPopulator<TVertex, TEdge>(visitedGraph, formatProvider, format);
        }

        public static MsaglGraphPopulator<TVertex, TEdge> CreateMsaglPopulator<TVertex, TEdge>(
            this IVertexAndEdgeSet<TVertex, TEdge> visitedGraph)
            where TEdge : IEdge<TVertex>
        {
            return new MsaglDefaultGraphPopulator<TVertex, TEdge>(visitedGraph);
        }

        public static MsaglGraphPopulator<TVertex, TEdge> CreateIdentifiableMsaglPopulator<TVertex, TEdge>(
            this IVertexAndEdgeSet<TVertex, TEdge> visitedGraph)
            where TVertex : IIdentifiable
            where TEdge : IEdge<TVertex>
        {
            return new MsaglIndentifiableGraphPopulator<TVertex, TEdge>(visitedGraph);
        }

        public static Microsoft.Msagl.Drawing.Graph ToMsaglGraph<TVertex, TEdge>(
            this IVertexAndEdgeSet<TVertex, TEdge> visitedGraph,
            MsaglVertexNodeEventHandler<TVertex> nodeAdded,
            MsaglEdgeEventHandler<TVertex, TEdge> edgeAdded
            )
            where TEdge : IEdge<TVertex>
        {
            if (visitedGraph == null)
                throw new ArgumentNullException("visitedGraph");

            var populator = visitedGraph.CreateMsaglPopulator();
            try
            {
                if (nodeAdded != null)
                    populator.NodeAdded += nodeAdded;
                if (edgeAdded != null)
                    populator.EdgeAdded += edgeAdded;

                populator.Compute();
                return populator.MsaglGraph;
            }
            finally
            {
                if (nodeAdded != null)
                    populator.NodeAdded -= nodeAdded;
                if (edgeAdded != null)
                    populator.EdgeAdded -= edgeAdded;
            }
        }

        public static Microsoft.Msagl.Drawing.Graph ToIdentifiableMsaglGraph<TVertex, TEdge>(
            this IVertexAndEdgeSet<TVertex, TEdge> visitedGraph,
            MsaglVertexNodeEventHandler<TVertex> nodeAdded,
            MsaglEdgeEventHandler<TVertex, TEdge> edgeAdded
            )
            where TVertex : IIdentifiable
            where TEdge : IEdge<TVertex>
        {
            if (visitedGraph == null)
                throw new ArgumentNullException("visitedGraph");

            var populator = visitedGraph.CreateIdentifiableMsaglPopulator();
            try
            {
                if (nodeAdded != null)
                    populator.NodeAdded += nodeAdded;
                if (edgeAdded != null)
                    populator.EdgeAdded += edgeAdded;

                populator.Compute();
                return populator.MsaglGraph;
            }
            finally
            {
                if (nodeAdded != null)
                    populator.NodeAdded -= nodeAdded;
                if (edgeAdded != null)
                    populator.EdgeAdded -= edgeAdded;
            }
        }
    }
}
