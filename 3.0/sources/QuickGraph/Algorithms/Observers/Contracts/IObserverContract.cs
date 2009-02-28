using System;
using System.Diagnostics.Contracts;

namespace QuickGraph.Algorithms.Observers.Contracts
{
    [ContractClassFor(typeof(IObserver<>))]
    sealed class IObserverContract<TAlgorithm>
        : IObserver<TAlgorithm>
    {
        void IObserver<TAlgorithm>.Attach(TAlgorithm algorithm)
        {
            Contract.Requires(algorithm != null);
        }

        void IObserver<TAlgorithm>.Detach(TAlgorithm algorithm)
        {
            Contract.Requires(algorithm != null);
        }
    }
}
