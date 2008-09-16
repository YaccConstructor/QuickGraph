using System;
using System.Collections.Generic;
using Microsoft.Msagl.Drawing;

namespace QuickGraph.Msagl
{
    public sealed class MsaglIndentifiableGraphPopulator<TVertex,TEdge>
        : MsaglGraphPopulator<TVertex, TEdge>
        where TEdge : IEdge<TVertex>
        where TVertex : IIdentifiable
    {
        public MsaglIndentifiableGraphPopulator(IVertexAndEdgeSet<TVertex, TEdge> visitedGraph)
            : base(visitedGraph)
        { }

        protected override Node AddNode(TVertex v)
        {
            return (Node)this.MsaglGraph.AddNode(v.ID);
        }

        protected override Microsoft.Msagl.Drawing.Edge AddEdge(TEdge e)
        {
            return (Microsoft.Msagl.Drawing.Edge)this.MsaglGraph.AddEdge(
                e.Source.ID,
                e.Target.ID);
        }
    }
}
