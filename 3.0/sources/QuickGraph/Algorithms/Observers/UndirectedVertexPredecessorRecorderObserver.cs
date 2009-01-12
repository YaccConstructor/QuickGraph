using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace QuickGraph.Algorithms.Observers
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="Vertex"></typeparam>
    /// <typeparam name="Edge"></typeparam>
    /// <reference-ref
    ///     idref="boost"
    ///     />
    [Serializable]
    public sealed class UndirectedVertexPredecessorRecorderObserver<TVertex, TEdge> :
        IObserver<IUndirectedTreeBuilderAlgorithm<TVertex, TEdge>>
        where TEdge : IEdge<TVertex>
    {
        private readonly IDictionary<TVertex, TEdge> vertexPredecessors;

        public UndirectedVertexPredecessorRecorderObserver()
            :this(new Dictionary<TVertex,TEdge>())
        {}

        public UndirectedVertexPredecessorRecorderObserver(
            IDictionary<TVertex, TEdge> vertexPredecessors)
        {
            Contract.Requires(vertexPredecessors != null);

            this.vertexPredecessors = vertexPredecessors;
        }

        public IDictionary<TVertex, TEdge> VertexPredecessors
        {
            get { return this.vertexPredecessors; }
        }

        public void Attach(IUndirectedTreeBuilderAlgorithm<TVertex, TEdge> algorithm)
        {
            Contract.Requires(algorithm != null);
            algorithm.TreeEdge += new UndirectedEdgeAction<TVertex, TEdge>(TreeEdge);
        }

        public void Detach(IUndirectedTreeBuilderAlgorithm<TVertex, TEdge> algorithm)
        {
            Contract.Requires(algorithm != null);
            algorithm.TreeEdge -= new UndirectedEdgeAction<TVertex, TEdge>(TreeEdge);
        }

        void TreeEdge(Object sender, UndirectedEdgeEventArgs<TVertex,TEdge> e)
        {
            this.vertexPredecessors[e.Target] = e.Edge;
        }

        public bool TryGetPath(TVertex vertex, out IEnumerable<TEdge> path)
        {
            return EdgeExtensions.TryGetPath(this.VertexPredecessors, vertex, out path);
        }
    }
}
