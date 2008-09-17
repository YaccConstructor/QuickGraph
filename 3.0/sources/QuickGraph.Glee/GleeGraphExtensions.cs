using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Glee.GraphViewerGdi;
using System.Windows.Forms;

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

        public static GleeGraphPopulator<TVertex, TEdge> CreateIdentifiableGleePopulator<TVertex, TEdge>(
            this IVertexAndEdgeSet<TVertex, TEdge> visitedGraph)
            where TVertex : IIdentifiable
            where TEdge : IEdge<TVertex>
        {
            return new GleeIndentifiableGraphPopulator<TVertex, TEdge>(visitedGraph);
        }

        public static Microsoft.Glee.Drawing.Graph ToGleeGraph<TVertex, TEdge>(
            this IVertexAndEdgeSet<TVertex, TEdge> visitedGraph,
            GleeVertexNodeEventHandler<TVertex> nodeAdded,
            GleeEdgeEventHandler<TVertex, TEdge> edgeAdded
            )
            where TEdge : IEdge<TVertex>
        {
            if (visitedGraph == null)
                throw new ArgumentNullException("visitedGraph");

            var populator = visitedGraph.CreateGleePopulator();
            try
            {
                if (nodeAdded != null)
                    populator.NodeAdded += nodeAdded;
                if (edgeAdded != null)
                    populator.EdgeAdded += edgeAdded;

                populator.Compute();
                return populator.GleeGraph;
            }
            finally
            {
                if (nodeAdded != null)
                    populator.NodeAdded -= nodeAdded;
                if (edgeAdded != null)
                    populator.EdgeAdded -= edgeAdded;
            }
        }

        public static Microsoft.Glee.Drawing.Graph ToIdentifiableGleeGraph<TVertex, TEdge>(
            this IVertexAndEdgeSet<TVertex, TEdge> visitedGraph,
            GleeVertexNodeEventHandler<TVertex> nodeAdded,
            GleeEdgeEventHandler<TVertex, TEdge> edgeAdded
            )
            where TVertex : IIdentifiable
            where TEdge : IEdge<TVertex>
        {
            if (visitedGraph == null)
                throw new ArgumentNullException("visitedGraph");

            var populator = visitedGraph.CreateIdentifiableGleePopulator();
            try
            {
                if (nodeAdded != null)
                    populator.NodeAdded += nodeAdded;
                if (edgeAdded != null)
                    populator.EdgeAdded += edgeAdded;

                populator.Compute();
                return populator.GleeGraph;
            }
            finally
            {
                if (nodeAdded != null)
                    populator.NodeAdded -= nodeAdded;
                if (edgeAdded != null)
                    populator.EdgeAdded -= edgeAdded;
            }
        }

        public static void ShowDialog(Microsoft.Glee.Drawing.Graph graph)
        {
            if (graph == null)
                throw new ArgumentNullException("graph");

            var viewerForm = new Form();
            viewerForm.SuspendLayout();
            var viewer = new GViewer();
            viewer.Dock = DockStyle.Fill;
            viewerForm.Controls.Add(viewer);
            viewerForm.ResumeLayout();

            viewer.Graph = graph;
            viewerForm.ShowDialog();
        }
    }
}
