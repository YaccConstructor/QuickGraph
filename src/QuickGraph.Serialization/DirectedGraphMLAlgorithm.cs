using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;
using System.Xml;
using QuickGraph.Algorithms;
using QuickGraph.Serialization.DirectedGraphML;

namespace QuickGraph.Serialization
{
    public sealed class DirectedGraphMLAlgorithm<TVertex, TEdge>
        : AlgorithmBase<IVertexAndEdgeListGraph<TVertex,TEdge>>
        where TEdge : IEdge<TVertex>
    {
        readonly VertexIdentity<TVertex> vertexIdentities;
        readonly EdgeIdentity<TVertex, TEdge> edgeIdentities;
        DirectedGraph directedGraph;

        public DirectedGraphMLAlgorithm(
            IVertexAndEdgeListGraph<TVertex, TEdge> visitedGraph,
            VertexIdentity<TVertex> vertexIdentities,
            EdgeIdentity<TVertex, TEdge> edgeIdentities)
            :base(visitedGraph)
        {
            Contract.Requires(vertexIdentities != null);
            this.vertexIdentities = vertexIdentities;
            this.edgeIdentities = edgeIdentities;
        }

        public DirectedGraph DirectedGraph
        {
            get { return this.directedGraph; }
        }

        protected override void InternalCompute()
        {
            var cancelManager = this.Services.CancelManager;
            this.directedGraph = new DirectedGraph();

            var nodes = new List<DirectedGraphNode>(this.VisitedGraph.VertexCount);
            foreach (var vertex in this.VisitedGraph.Vertices)
            {
                if (cancelManager.IsCancelling) return;

                var node = new DirectedGraphNode { Id = this.vertexIdentities(vertex) };
                this.OnFormatNode(vertex, node);
                nodes.Add(node);
            }
            this.directedGraph.Nodes = nodes.ToArray();

            var links = new List<DirectedGraphLink>(this.VisitedGraph.EdgeCount);
            foreach (var edge in this.VisitedGraph.Edges)
            {
                if (cancelManager.IsCancelling) return;

                var link = new DirectedGraphLink
                {
                    Label = this.edgeIdentities(edge),
                    Source = this.vertexIdentities(edge.Source),
                    Target = this.vertexIdentities(edge.Target)
                };
                this.OnFormatEdge(edge, link);
                links.Add(link);
            }
            this.directedGraph.Links = links.ToArray();

            this.OnFormatGraph();
        }

        /// <summary>
        /// Raised when the graph is about to be returned
        /// </summary>
        public event Action<IVertexAndEdgeListGraph<TVertex, TEdge>, DirectedGraph> FormatGraph;

        private void OnFormatGraph()
        {
            var eh = this.FormatGraph;
            if (eh != null)
                eh(this.VisitedGraph, this.DirectedGraph);
        }

        /// <summary>
        /// Raised when a new link is added to the graph
        /// </summary>
        public event Action<TEdge, DirectedGraphLink> FormatEdge;

        private void OnFormatEdge(TEdge edge, DirectedGraphLink link)
        {
            Contract.Requires(edge != null);
            Contract.Requires(link != null);

            var eh = this.FormatEdge;
            if (eh != null)
                eh(edge, link);
        }

        /// <summary>
        /// Raised when a new node is added to the graph
        /// </summary>
        public event Action<TVertex, DirectedGraphNode> FormatNode;

        private void OnFormatNode(TVertex vertex, DirectedGraphNode node)
        {
            Contract.Requires(node != null);
            var eh = this.FormatNode;
            if (eh != null)
                eh(vertex, node);
        }
    }
}
