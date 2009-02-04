using System;
using System.Diagnostics.Contracts;
using QuickGraph.Algorithms.Observers.Contracts;

namespace QuickGraph.Algorithms.Observers
{
    /// <summary>
    /// An algorithm observer
    /// </summary>
    /// <typeparam name="Algorithm"></typeparam>
    /// <reference-ref
    ///     id="gof02designpatterns"
    ///     />
#if CONTRACTS_FULL
    [ContractClass(typeof(IObserverContract<>))]
#endif
    public interface IObserver<TAlgorithm>
    {
        void Attach(TAlgorithm algorithm);
        void Detach(TAlgorithm algorithm);
    }
}
