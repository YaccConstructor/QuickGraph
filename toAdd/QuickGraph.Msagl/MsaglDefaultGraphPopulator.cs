using System;
using System.Collections.Generic;
using Microsoft.Msagl.Drawing;

namespace QuickGraph.Msagl
{
    public class MsaglDefaultGraphPopulator<TVertex, TEdge>
        : MsaglGraphPopulator<TVertex, TEdge>
        where TEdge : IEdge<TVertex>
    {
        public MsaglDefaultGraphPopulator(IEdgeListGraph<TVertex, TEdge> visitedGraph)
            : base(visitedGraph)
        { }

        private Dictionary<TVertex, string> vertexIds;
        protected override void OnStarted(EventArgs e)
        {
            base.OnStarted(e);
            this.vertexIds = new Dictionary<TVertex, string>(this.VisitedGraph.VertexCount);
        }

        protected override void OnFinished(EventArgs e)
        {
            this.vertexIds = null;
            base.OnFinished(e);
        }

        protected override Node AddNode(TVertex v)
        {
            string id = this.GetVertexId(v);
            this.vertexIds.Add(v, id);
            Node node = (Node)this.MsaglGraph.AddNode(id);
            node.Attr.Shape = Shape.Box;
            node.Attr.Label = this.GetVertexLabel(id, v);
            return node;
        }

        protected virtual string GetVertexId(TVertex v)
        {
            return this.vertexIds.Count.ToString();
        }

        protected virtual string GetVertexLabel(string id, TVertex v)
        {
            return String.Format("{0}: {1}", id, v.ToString());
        }

        protected override Microsoft.Msagl.Drawing.Edge AddEdge(TEdge e)
        {
            var edge = (Microsoft.Msagl.Drawing.Edge)this.MsaglGraph.AddEdge(
                this.vertexIds[e.Source],
                this.vertexIds[e.Target]
                );
            edge.Attr.Label = this.GetEdgeLabel(e);
            return edge;
        }

        protected virtual string GetEdgeLabel(TEdge e)
        {
            return e.ToString();
        }
    }
}
