using System;
using System.Collections.Generic;
using QuickGraph.Collections;

namespace QuickGraph.Algorithms.Condensation
{
    [Serializable]
    public sealed class CondensatedEdge<V, E, G> : Edge<G>
        where E : IEdge<V>
        where G : IMutableVertexAndEdgeListGraph<V, E>, new()
    {
        private List<E> edges = new List<E>();
        public CondensatedEdge(G source, G target)
            :base(source,target)
        { }

        public IList<E> Edges
        {
            get { return this.edges; }
        }
    }
}
