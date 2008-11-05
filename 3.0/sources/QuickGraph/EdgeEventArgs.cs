using System;
using System.Diagnostics.Contracts;

namespace QuickGraph
{
    [Serializable]
    public class EdgeEventArgs<TVertex, TEdge> 
        : EventArgs
        where TEdge : IEdge<TVertex>
    {
        private readonly TEdge edge;
        public EdgeEventArgs(TEdge edge)
        {
            CodeContract.Requires(edge != null);

            this.edge = edge;
        }

        public TEdge Edge
        {
            get { return this.edge; }
        }
    }

    public delegate void EdgeEventHandler<TVertex,TEdge>(
        Object sender,
        EdgeEventArgs<TVertex,TEdge> e)
        where TEdge : IEdge<TVertex>;
}
