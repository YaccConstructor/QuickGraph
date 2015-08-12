using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace QuickGraph
{
    public class ParallelEventArgs<TLocal>
        : EventArgs
    {
        readonly ParallelState<TLocal> local;
        public ParallelEventArgs(ParallelState<TLocal> local)
        {
            this.local = local;
        }

        public ParallelState<TLocal> Local
        {
            get { return this.local; }
        }
    }

    public delegate void ParallelEventHandler<TLocal>(
        object sender, 
        ParallelEventArgs<TLocal> local);
}
