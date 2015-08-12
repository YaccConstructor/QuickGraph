using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics.Contracts;

namespace QuickGraph.Msagl
{
    public sealed class MsaglEdgeEventArgs<TVertex, TEdge> : EdgeEventArgs<TVertex, TEdge>
        where TEdge : IEdge<TVertex>
    {
        private readonly Microsoft.Msagl.Drawing.Edge gedge;
        public Microsoft.Msagl.Drawing.Edge GEdge
        {
            get { return this.gedge; }
        }

        public MsaglEdgeEventArgs(TEdge edge, Microsoft.Msagl.Drawing.Edge gedge)
            :base(edge)
        {
            Contract.Requires(gedge != null);

            this.gedge = gedge;
        }
    }

    public delegate void MsaglEdgeAction<Vertex, Edge>(
        object sender,
        MsaglEdgeEventArgs<Vertex, Edge> e)
        where Edge : IEdge<Vertex>;
}
