using System;

namespace QuickGraph.Algorithms.Observers
{
    [Serializable]
    public sealed class ObserverGuardian<Vertex,Edge,A,O> :
        IDisposable
        where Edge : IEdge<Vertex>
        where O : IObserver<Vertex,Edge,A>
    {
        private A algorithm;
        private O observer;

        public ObserverGuardian(A algorithm, O observer)
        {
            this.algorithm = algorithm;
            this.observer = observer;

            this.observer.Attach(this.algorithm);
        }

        public A Algorithm
        {
            get { return this.algorithm; }
        }

        public O Observer
        {
            get { return this.observer; }
        }

        public void Dispose()
        {
            if (this.observer != null && this.algorithm != null)
            {
                this.observer.Detach(this.algorithm);
                this.algorithm = default(A);
                this.observer = default(O);
            }
        }
    }
}
