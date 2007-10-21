using System;

namespace QuickGraph.Algorithms.Observers
{
    [Serializable]
    public static class ObserverScope
    {
        public static IDisposable
            Create<TAlgorithm>(TAlgorithm algorithm, IObserver<TAlgorithm> observer)
        {
            return new ObserverGuardian<TAlgorithm>(algorithm, observer);
        }

        [Serializable]
        internal sealed class ObserverGuardian<TAlgorithm> :
            IDisposable 
        {
            private TAlgorithm algorithm;
            private IObserver<TAlgorithm> observer;

            public ObserverGuardian(TAlgorithm algorithm, IObserver<TAlgorithm> observer) {
                if (algorithm == null)
                    throw new ArgumentNullException("algorithm");
                if (observer == null)
                    throw new ArgumentNullException("observer");

                this.algorithm = algorithm;
                this.observer = observer;

                this.observer.Attach(this.algorithm);
            }

            public void Dispose() {
                if (this.observer != null && this.algorithm != null) 
                {
                    this.observer.Detach(this.algorithm);
                    this.algorithm = default(TAlgorithm);
                    this.observer = null;
                }
            }
        }

    }
}
