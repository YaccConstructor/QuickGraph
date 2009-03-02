using System;
using System.Diagnostics.Contracts;

namespace QuickGraph.Algorithms.Observers
{
    /// <summary>
    /// An algorithm observer
    /// </summary>
    /// <typeparam name="Algorithm"></typeparam>
    /// <reference-ref
    ///     id="gof02designpatterns"
    ///     />
    [ContractClass(typeof(Contracts.IObserverContract<>))]
    public interface IObserver<TAlgorithm>
    {
        /// <summary>
        /// Attaches to the algorithm events
        /// and returns a disposable object that can be used
        /// to detach from the events
        /// </summary>
        /// <param name="algorithm"></param>
        /// <returns></returns>
        IDisposable Attach(TAlgorithm algorithm);
    }
}
