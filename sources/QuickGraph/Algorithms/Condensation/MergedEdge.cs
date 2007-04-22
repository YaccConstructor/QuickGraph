using System;
using System.Collections.Generic;
using QuickGraph.Collections;

namespace QuickGraph.Algorithms.Condensation
{
    [Serializable]
    public sealed class MergedEdge<V, E> : Edge<V>
        where E : IEdge<V>
    {
        private List<E> edges = new List<E>();

        public MergedEdge(V source, V target)
            :base(source,target)
        { }

        public IList<E> Edges
        {
            get { return this.edges; }
        }

        public static MergedEdge<V, E> Merge(
            MergedEdge<V, E> inEdge,
            MergedEdge<V, E> outEdge
            )
        {
            MergedEdge<V, E> newEdge = new MergedEdge<V, E>(
                inEdge.Source, outEdge.Target);
            newEdge.edges = new List<E>(inEdge.Edges.Count + outEdge.Edges.Count);
            newEdge.edges.AddRange(inEdge.Edges);
            newEdge.edges.AddRange(outEdge.edges);

            return newEdge;
        }
    }
}
