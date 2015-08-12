using System;
using System.Diagnostics.Contracts;

namespace QuickGraph.Algorithms
{
    [ContractClass(typeof(Contracts.IComputationContract))]
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
