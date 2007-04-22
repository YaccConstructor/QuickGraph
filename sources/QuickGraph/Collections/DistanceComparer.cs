namespace QuickGraph.Collections
{
    using System;
    using System.Collections.Generic;

    [Serializable]
    public sealed class DistanceComparer<Vertex, Distance> : 
        IComparer<Vertex>
        where Distance : IComparable
    {
        IDictionary<Vertex,Distance> distances;

        public DistanceComparer(IDictionary<Vertex, Distance> distances)
        {
            this.distances = distances;
        }

        public int Compare(Vertex v, Vertex y)
        {
            return distances[v].CompareTo(distances[y]);
        }

        public bool Equals(Vertex v, Vertex w)
        {
            return v.Equals(w);
        }

        public int GetHashCode(Vertex v)
        {
            return this.distances[v].GetHashCode();
        }
    }
}
