using System;
using System.Collections.Generic;
using System.Text;

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
            this.gedge = gedge;
        }
    }

    public delegate void MsaglEdgeEventHandler<Vertex, Edge>(
        object sender,
        MsaglEdgeEventArgs<Vertex, Edge> e)
        where Edge : IEdge<Vertex>;
}
