using System;
using System.Diagnostics.Contracts;

namespace QuickGraph.Algorithms
{
#if CONTRACTS_FULL
    [ContractClass(typeof(Contracts.IComputationContract))]
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
