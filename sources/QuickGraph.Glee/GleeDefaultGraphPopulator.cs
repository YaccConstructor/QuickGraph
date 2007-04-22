using System;
using System.Collections.Generic;
using Microsoft.Glee.Drawing;

namespace QuickGraph.Glee
{
    public class GleeDefaultGraphPopulator<Vertex, Edge>
        : GleeGraphPopulator<Vertex, Edge>
        where Edge : IEdge<Vertex>
    {
        public GleeDefaultGraphPopulator(IVertexAndEdgeListGraph<Vertex, Edge> visitedGraph)
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
            return (Node)this.GleeGraph.AddNode(id);
        }

        protected virtual string GetVertexId(Vertex v)
        {
            return this.vertexIds.Count.ToString();
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
