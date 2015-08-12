using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace QuickGraph
{
    [Serializable]
    public class ParallelVertexEventArgs<TVertex, TLocal>
        : VertexEventArgs<TVertex>
    {
        readonly ParallelState<TLocal> local;
        public ParallelVertexEventArgs(TVertex vertex, ParallelState<TLocal> local)
            : base(vertex)
        {
            this.local = local; 
        }

        public ParallelState<TLocal> Local
        {
            get { return this.local; }
        }
    }

    public delegate void ParallelVertexEventHandler<TVertex, TLocal>(
        object sender,
        ParallelVertexEventArgs<TVertex, TLocal> args)
    ;
}
