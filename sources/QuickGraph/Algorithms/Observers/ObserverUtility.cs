using System;

namespace QuickGraph.Algorithms.Observers
{
    [Serializable]
    public static class ObserverUtility
    {
        public static ObserverGuardian<Vertex, Edge, Algorithm, Observer>
            Guard<Vertex, Edge, Algorithm, Observer>(Algorithm algorithm, Observer observer)
            where Edge : IEdge<Vertex>
            where Observer : IObserver<Vertex, Edge, Algorithm>
        {
            return new ObserverGuardian<Vertex, Edge, Algorithm, Observer>(
                algorithm, observer);
        }
    }
}
