using System;

namespace QuickGraph
{
    [Serializable]
    public class EdgeEventArgs<TVertex, TEdge> : EventArgs
        where TEdge : IEdge<TVertex>
    {
        private TEdge edge;
        public EdgeEventArgs(TEdge edge)
        {
            if (edge == null)
                throw new ArgumentNullException("edge");
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
