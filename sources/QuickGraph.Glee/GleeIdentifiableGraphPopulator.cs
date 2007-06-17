using System;
using System.Collections.Generic;
using Microsoft.Glee.Drawing;

namespace QuickGraph.Glee
{
    public sealed class GleeIndentifiableGraphPopulator<Vertex,Edge>
        : GleeGraphPopulator<Vertex, Edge>
        where Edge : IEdge<Vertex>
        where Vertex : IIdentifiable
    {
        public GleeIndentifiableGraphPopulator(IVertexAndEdgeSet<Vertex, Edge> visitedGraph)
            : base(visitedGraph)
        { }

        protected override Node AddNode(Vertex v)
        {
            return (Node)this.GleeGraph.AddNode(v.ID);
        }

        protected override Microsoft.Glee.Drawing.Edge AddEdge(Edge e)
        {
            return (Microsoft.Glee.Drawing.Edge)this.GleeGraph.AddEdge(
                e.Source.ID,
                e.Target.ID);
        }
    }
}
