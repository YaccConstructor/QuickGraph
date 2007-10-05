using System;

namespace QuickGraph.Algorithms.Observers
{
    [Serializable]
    public static class ObserverUtility
    {
        public static ObserverGuardian<TVertex, TEdge, TAlgorithm, TObserver>
            Guard<TVertex, TEdge, TAlgorithm, TObserver>(TAlgorithm algorithm, TObserver observer)
            where TEdge : IEdge<TVertex>
            where TObserver : IObserver<TVertex, TEdge, TAlgorithm>
        {
            return new ObserverGuardian<TVertex, TEdge, TAlgorithm, TObserver>(
                algorithm, observer);
        }
    }
}
