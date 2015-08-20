using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace QuickGraph
{
    [Serializable]
    public class ParallelEdgeEventArgs<TVertex, TEdge, TLocal>
        : EdgeEventArgs<TVertex, TEdge>
        where TEdge : IEdge<TVertex>
    {
        readonly ParallelState<TLocal> local;
        public ParallelEdgeEventArgs(TEdge edge, ParallelState<TLocal> local)
            :base(edge)
        {
            this.local = local;
        }

        public ParallelState<TLocal> Local
        {
            get { return this.local; }
        }
    }

    public delegate void ParallelEdgeEventHandler<TVertex, TEdge, TLocal>(
        object sender,
        ParallelEdgeEventArgs<TVertex, TEdge, TLocal> args)
        where TEdge : IEdge<TVertex>;
}
