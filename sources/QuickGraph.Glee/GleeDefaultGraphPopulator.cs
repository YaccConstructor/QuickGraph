using System;
using System.Collections.Generic;
using Microsoft.Glee.Drawing;

namespace QuickGraph.Glee
{
    public class GleeDefaultGraphPopulator<Vertex, Edge>
        : GleeGraphPopulator<Vertex, Edge>
        where Edge : IEdge<Vertex>
    {
        public GleeDefaultGraphPopulator(IVertexAndEdgeSet<Vertex, Edge> visitedGraph)
            : base(visitedGraph)
        { }

        private Dictionary<Vertex, string> vertexIds;
        protected override void OnStarted(EventArgs e)
        {
            base.OnStarted(e);
            this.vertexIds = new Dictionary<Vertex, string>(this.VisitedGraph.VertexCount);
        }

        protected override void OnFinished(EventArgs e)
        {
            this.vertexIds = null;
            base.OnFinished(e);
        }

        protected override Node AddNode(Vertex v)
        {
            string id = this.GetVertexId(v);
            this.vertexIds.Add(v, id);
            Node node = (Node)this.GleeGraph.AddNode(id);
            node.Attr.Shape = Shape.Box;
            node.Attr.Label = this.GetVertexLabel(id, v);
            return node;
        }

        protected virtual string GetVertexId(Vertex v)
        {
            return this.vertexIds.Count.ToString();
        }

        protected virtual string GetVertexLabel(string id, Vertex v)
        {
            return String.Format("{0}: {1}", id, v.ToString());
        }

        protected override Microsoft.Glee.Drawing.Edge AddEdge(Edge e)
        {
            return (Microsoft.Glee.Drawing.Edge)this.GleeGraph.AddEdge(
                this.vertexIds[e.Source],
                this.vertexIds[e.Target]
                );
        }
    }
}
