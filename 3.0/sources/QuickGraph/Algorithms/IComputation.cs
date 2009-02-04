using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics.Contracts;
using QuickGraph.Algorithms.Contracts;

namespace QuickGraph.Algorithms
{
#if CONTRACTS_FULL
    [ContractClass(typeof(IComputationContract))]
#endif
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
