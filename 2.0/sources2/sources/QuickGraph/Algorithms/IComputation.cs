using System;
using System.Collections.Generic;
using System.Text;

namespace QuickGraph.Algorithms
{
    public interface IComputation
    {
        object SyncRoot { get; }
        ComputationState State { get; }

        void Compute();
        void Abort();

        event EventHandler StateChanged;
        event EventHandler Started;
        event EventHandler Finished;
        event EventHandler Aborted;
    }
}
