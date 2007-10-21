using System;
namespace QuickGraph.Algorithms.Observers
{
    /// <summary>
    /// An algorithm observer
    /// </summary>
    /// <typeparam name="Algorithm"></typeparam>
    /// <reference-ref
    ///     id="gof02designpatterns"
    ///     />
    public interface IObserver<TAlgorithm>
    {
        void Attach(TAlgorithm algorithm);
        void Detach(TAlgorithm algorithm);
    }
}
