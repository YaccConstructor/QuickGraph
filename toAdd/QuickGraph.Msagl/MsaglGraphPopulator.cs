using System;
using System.Collections.Generic;
using QuickGraph.Algorithms;
using Microsoft.Msagl.Drawing;

namespace QuickGraph.Msagl
{
    public abstract class MsaglGraphPopulator<TVertex,TEdge> :
        AlgorithmBase<IEdgeListGraph<TVertex, TEdge>>
        where TEdge : IEdge<TVertex>
    {
        protected MsaglGraphPopulator(IEdgeListGraph<TVertex, TEdge> visitedGraph)
            : base(visitedGraph)
        { }

        private Microsoft.Msagl.Drawing.Graph msaglGraph;
        public Microsoft.Msagl.Drawing.Graph MsaglGraph
        {
            get { return this.msaglGraph; }
        }

        #region Events
        public event MsaglVertexNodeEventHandler<TVertex> NodeAdded;
        protected virtual void OnNodeAdded(MsaglVertexEventArgs<TVertex> e)
        {
            MsaglVertexNodeEventHandler<TVertex> eh = this.NodeAdded;
            if (eh != null)
                eh(this, e);
        }

        public event MsaglEdgeAction<TVertex, TEdge> EdgeAdded;
        protected virtual void OnEdgeAdded(MsaglEdgeEventArgs<TVertex,TEdge> e)
        {
            var eh = this.EdgeAdded;
            if (eh != null)
                eh(this, e);
        }
        #endregion

        protected override void InternalCompute()
        {
            this.msaglGraph = new Microsoft.Msagl.Drawing.Graph("");

            foreach (TVertex v in this.VisitedGraph.Vertices)
            {
                Node node = this.AddNode(v);
                node.UserData = v;
                this.OnNodeAdded(new MsaglVertexEventArgs<TVertex>(v, node));
            }

            foreach (TEdge e in this.VisitedGraph.Edges)
            {
                Microsoft.Msagl.Drawing.Edge edge = this.AddEdge(e);
                edge.UserData = e;
                this.OnEdgeAdded(new MsaglEdgeEventArgs<TVertex, TEdge>(e, edge));
            }
        }

        protected abstract Node AddNode(TVertex v);

        protected abstract Microsoft.Msagl.Drawing.Edge AddEdge(TEdge e);
    }
}
