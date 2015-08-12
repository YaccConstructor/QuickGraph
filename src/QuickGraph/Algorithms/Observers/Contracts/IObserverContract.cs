using System;
using System.Diagnostics.Contracts;

namespace QuickGraph.Algorithms.Observers.Contracts
{
    [ContractClassFor(typeof(IObserver<>))]
    abstract class IObserverContract<TAlgorithm>
        : IObserver<TAlgorithm>
    {
        IDisposable IObserver<TAlgorithm>.Attach(TAlgorithm algorithm)
        {
            Contract.Requires(algorithm != null);
            Contract.Ensures(Contract.Result<IDisposable>() != null);

            return default(IDisposable);
        }
    }
}
