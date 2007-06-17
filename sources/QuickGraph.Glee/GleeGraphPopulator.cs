using System;
using System.Collections.Generic;
using QuickGraph.Algorithms;
using Microsoft.Glee.Drawing;

namespace QuickGraph.Glee
{
    public abstract class GleeGraphPopulator<Vertex,Edge> :
        AlgorithmBase<IVertexAndEdgeSet<Vertex, Edge>>
        where Edge : IEdge<Vertex>
    {
        protected GleeGraphPopulator(IVertexAndEdgeSet<Vertex, Edge> visitedGraph)
            : base(visitedGraph)
        { }

        private Microsoft.Glee.Drawing.Graph gleeGraph;
        public Microsoft.Glee.Drawing.Graph GleeGraph
        {
            get { return this.gleeGraph; }
        }

        #region Events
        public event GleeVertexNodeEventHandler<Vertex> NodeAdded;
        protected virtual void OnNodeAdded(GleeVertexEventArgs<Vertex> e)
        {
            GleeVertexNodeEventHandler<Vertex> eh = this.NodeAdded;
            if (eh != null)
                eh(this, e);
        }

        public event GleeEdgeEventHandler<Vertex, Edge> EdgeAdded;
        protected virtual void OnEdgeAdded(GleeEdgeEventArgs<Vertex, Edge> e)
        {
            GleeEdgeEventHandler<Vertex, Edge> eh = this.EdgeAdded;
            if (eh != null)
                eh(this, e);
        }
        #endregion

        protected override void InternalCompute()
        {
            this.gleeGraph = new Microsoft.Glee.Drawing.Graph("");

            foreach (Vertex v in this.VisitedGraph.Vertices)
            {
                Node node = this.AddNode(v);
                node.UserData = v;
                this.OnNodeAdded(new GleeVertexEventArgs<Vertex>(v, node));
            }

            foreach (Edge e in this.VisitedGraph.Edges)
            {
                Microsoft.Glee.Drawing.Edge edge = this.AddEdge(e);
                edge.UserData = e;
                this.OnEdgeAdded(new GleeEdgeEventArgs<Vertex,Edge>(e, edge));
            }
        }

        protected abstract Node AddNode(Vertex v);

        protected abstract Microsoft.Glee.Drawing.Edge AddEdge(Edge e);
    }
}
