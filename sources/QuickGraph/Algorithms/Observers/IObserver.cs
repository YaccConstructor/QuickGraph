using System;
namespace QuickGraph.Algorithms.Observers
{
    /// <summary>
    /// An algorithm observer
    /// </summary>
    /// <typeparam name="Vertex"></typeparam>
    /// <typeparam name="Edge"></typeparam>
    /// <typeparam name="Algorithm"></typeparam>
    /// <reference-ref
    ///     id="gof02designpatterns"
    ///     />
    public interface IObserver<TVertex,TEdge,TAlgorithm>
        where TEdge : IEdge<TVertex>
    {
        void Attach(TAlgorithm algorithm);
        void Detach(TAlgorithm algorithm);
    }
}
