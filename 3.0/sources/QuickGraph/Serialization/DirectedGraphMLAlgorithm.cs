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
        readonly VertexIdentity<TVertex> vertexIds;
        DirectedGraph directedGraph;

        public DirectedGraphMLAlgorithm(
            IVertexAndEdgeListGraph<TVertex, TEdge> visitedGraph, 
            VertexIdentity<TVertex> vertexIds)
            :base(visitedGraph)
        {
            Contract.Requires(vertexIds != null);
            this.vertexIds = vertexIds;
        }

        public DirectedGraph DirectedGraph
        {
            get { return this.directedGraph; }
        }

        protected override void InternalCompute()
        {
            this.directedGraph = new DirectedGraph();
            var nodes = new List<DirectedGraphNode>(this.VisitedGraph.VertexCount);
            foreach (var vertex in this.VisitedGraph.Vertices)
            {
                var node = new DirectedGraphNode { Id = this.vertexIds(vertex) };
                this.OnFormatNode(vertex, node);
                nodes.Add(node);
            }
            this.directedGraph.Nodes = nodes.ToArray();

            var links = new List<DirectedGraphLink>(this.VisitedGraph.EdgeCount);
            foreach (var edge in this.VisitedGraph.Edges)
            {
                var link = new DirectedGraphLink
                {
                    Source = this.vertexIds(edge.Source),
                    Target = this.vertexIds(edge.Target)
                };
                this.OnFormatEdge(edge, link);
                links.Add(link);
            }
            this.directedGraph.Links = links.ToArray();
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
