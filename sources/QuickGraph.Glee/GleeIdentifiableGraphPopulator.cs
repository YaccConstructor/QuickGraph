using System;
using System.Collections.Generic;
using Microsoft.Glee.Drawing;

namespace QuickGraph.Glee
{
    public sealed class GleeIndentifiableGraphPopulator<TVertex,TEdge>
        : GleeGraphPopulator<TVertex, TEdge>
        where TEdge : IEdge<TVertex>
        where TVertex : IIdentifiable
    {
        public GleeIndentifiableGraphPopulator(IVertexAndEdgeSet<TVertex, TEdge> visitedGraph)
            : base(visitedGraph)
        { }

        protected override Node AddNode(TVertex v)
        {
            return (Node)this.GleeGraph.AddNode(v.ID);
        }

        protected override Microsoft.Glee.Drawing.Edge AddEdge(TEdge e)
        {
            return (Microsoft.Glee.Drawing.Edge)this.GleeGraph.AddEdge(
                e.Source.ID,
                e.Target.ID);
        }
    }
}
