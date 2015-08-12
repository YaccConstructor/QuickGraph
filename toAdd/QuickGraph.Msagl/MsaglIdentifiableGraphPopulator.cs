using System;
using System.Collections.Generic;
using Microsoft.Msagl.Drawing;
using System.Diagnostics.Contracts;

namespace QuickGraph.Msagl
{
    public sealed class MsaglIndentifiableGraphPopulator<TVertex,TEdge>
        : MsaglGraphPopulator<TVertex, TEdge>
        where TEdge : IEdge<TVertex>
    {
        private readonly VertexIdentity<TVertex> vertexIdentities;

        public MsaglIndentifiableGraphPopulator(IEdgeListGraph<TVertex, TEdge> visitedGraph, VertexIdentity<TVertex> vertexIdentities)
            : base(visitedGraph)
        {
            Contract.Requires(vertexIdentities != null);

            this.vertexIdentities = vertexIdentities;
        }

        protected override Node AddNode(TVertex v)
        {
            return (Node)this.MsaglGraph.AddNode(this.vertexIdentities(v));
        }

        protected override Microsoft.Msagl.Drawing.Edge AddEdge(TEdge e)
        {
            var edge = (Microsoft.Msagl.Drawing.Edge)this.MsaglGraph.AddEdge(
                this.vertexIdentities(e.Source),
                this.vertexIdentities(e.Target)
                );
            return edge;
        }
    }
}
