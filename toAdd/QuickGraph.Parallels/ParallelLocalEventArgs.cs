using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuickGraph
{
    public class ParallelLocalEventArgs<TLocal>
        : EventArgs
    {
        readonly TLocal local;
        public ParallelLocalEventArgs(TLocal local)
        {
            this.local = local;
        }

        public TLocal Local
        {
            get { return this.local; }
        }
    }

    public delegate void ParallelLocalEventHandler<TLocal>(
        object sender,
        ParallelLocalEventArgs<TLocal> local
        );
}
