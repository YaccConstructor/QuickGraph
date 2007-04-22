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
    public interface IObserver<Vertex,Edge,Algorithm>
        where Edge : IEdge<Vertex>
    {
        void Attach(Algorithm algorithm);
        void Detach(Algorithm algorithm);
    }
}
