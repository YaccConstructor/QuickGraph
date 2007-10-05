namespace QuickGraph.Collections
{
    using System;
    using System.Collections.Generic;

    [Serializable]
    public sealed class DistanceComparer<TVertex, TDistance> : 
        IComparer<TVertex>
        where TDistance : IComparable
    {
        IDictionary<TVertex,TDistance> distances;

        public DistanceComparer(IDictionary<TVertex, TDistance> distances)
        {
            this.distances = distances;
        }

        public int Compare(TVertex v, TVertex y)
        {
            return distances[v].CompareTo(distances[y]);
        }

        public bool Equals(TVertex v, TVertex w)
        {
            return v.Equals(w);
        }

        public int GetHashCode(TVertex v)
        {
            return this.distances[v].GetHashCode();
        }
    }
}
