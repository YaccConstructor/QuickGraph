using System;
using System.Collections.Generic;
using System.Text;

namespace QuickGraph.Glee
{
    public sealed class GleeEdgeEventArgs<TVertex, TEdge> : EdgeEventArgs<TVertex, TEdge>
        where TEdge : IEdge<TVertex>
    {
        private readonly Microsoft.Glee.Drawing.Edge gedge;
        public Microsoft.Glee.Drawing.Edge GEdge
        {
            get { return this.gedge; }
        }

        public GleeEdgeEventArgs(TEdge edge, Microsoft.Glee.Drawing.Edge gedge)
            :base(edge)
        {
            this.gedge = gedge;
        }
    }

    public delegate void GleeEdgeEventHandler<Vertex,Edge>(
        object sender,
        GleeEdgeEventArgs<Vertex, Edge> e)
        where Edge : IEdge<Vertex>;
}
