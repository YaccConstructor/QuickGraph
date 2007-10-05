using System;

namespace QuickGraph.Algorithms.Observers
{
    [Serializable]
    public sealed class ObserverGuardian<TVertex,TEdge,TAlgorithm,TObserver> :
        IDisposable
        where TEdge : IEdge<TVertex>
        where TObserver : IObserver<TVertex,TEdge,TAlgorithm>
    {
        private TAlgorithm algorithm;
        private TObserver observer;

        public ObserverGuardian(TAlgorithm algorithm, TObserver observer)
        {
            this.algorithm = algorithm;
            this.observer = observer;

            this.observer.Attach(this.algorithm);
        }

        public TAlgorithm Algorithm
        {
            get { return this.algorithm; }
        }

        public TObserver Observer
        {
            get { return this.observer; }
        }

        public void Dispose()
        {
            if (this.observer != null && this.algorithm != null)
            {
                this.observer.Detach(this.algorithm);
                this.algorithm = default(TAlgorithm);
                this.observer = default(TObserver);
            }
        }
    }
}
