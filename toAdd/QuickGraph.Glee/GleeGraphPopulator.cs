using System;
using System.Collections.Generic;
using QuickGraph.Algorithms;
using Microsoft.Glee.Drawing;

namespace QuickGraph.Glee
{
    public abstract class GleeGraphPopulator<TVertex,TEdge> :
        AlgorithmBase<IEdgeListGraph<TVertex, TEdge>>
        where TEdge : IEdge<TVertex>
    {
        protected GleeGraphPopulator(IEdgeListGraph<TVertex, TEdge> visitedGraph)
            : base(visitedGraph)
        { }

        private Microsoft.Glee.Drawing.Graph gleeGraph;
        public Microsoft.Glee.Drawing.Graph GleeGraph
        {
            get { return this.gleeGraph; }
        }

        #region Events
        public event GleeVertexNodeEventHandler<TVertex> NodeAdded;
        protected virtual void OnNodeAdded(GleeVertexEventArgs<TVertex> e)
        {
            GleeVertexNodeEventHandler<TVertex> eh = this.NodeAdded;
            if (eh != null)
                eh(this, e);
        }

        public event GleeEdgeEventHandler<TVertex, TEdge> EdgeAdded;
        protected virtual void OnEdgeAdded(GleeEdgeEventArgs<TVertex, TEdge> e)
        {
            var eh = this.EdgeAdded;
            if (eh != null)
                eh(this, e);
        }
        #endregion

        protected override void InternalCompute()
        {
            this.gleeGraph = new Microsoft.Glee.Drawing.Graph("");

            foreach (var v in this.VisitedGraph.Vertices)
            {
                Node node = this.AddNode(v);
                node.UserData = v;
                this.OnNodeAdded(new GleeVertexEventArgs<TVertex>(v, node));
            }

            foreach (var e in this.VisitedGraph.Edges)
            {
                Microsoft.Glee.Drawing.Edge edge = this.AddEdge(e);
                edge.UserData = e;
                this.OnEdgeAdded(new GleeEdgeEventArgs<TVertex,TEdge>(e, edge));
            }
        }

        protected abstract Node AddNode(TVertex v);

        protected abstract Microsoft.Glee.Drawing.Edge AddEdge(TEdge e);
    }
}
