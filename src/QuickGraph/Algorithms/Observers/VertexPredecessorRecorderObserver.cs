using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace QuickGraph.Algorithms.Observers
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TVertex">type of a vertex</typeparam>
    /// <typeparam name="TEdge">type of an edge</typeparam>
    /// <reference-ref
    ///     idref="boost"
    ///     />
#if !SILVERLIGHT
    [Serializable]
#endif
    public sealed class VertexPredecessorRecorderObserver<TVertex, TEdge> :
        IObserver<ITreeBuilderAlgorithm<TVertex, TEdge>>
        where TEdge : IEdge<TVertex>
    {
        private readonly Dictionary<TVertex, TEdge> vertexPredecessors;

        public VertexPredecessorRecorderObserver()
            :this(new Dictionary<TVertex,TEdge>())
        {}

        public VertexPredecessorRecorderObserver(
            Dictionary<TVertex, TEdge> vertexPredecessors)
        {
            Contract.Requires(vertexPredecessors != null);

            this.vertexPredecessors = vertexPredecessors;
        }

        public IDictionary<TVertex, TEdge> VertexPredecessors
        {
            get { return this.vertexPredecessors; }
        }

        public IDisposable Attach(ITreeBuilderAlgorithm<TVertex, TEdge> algorithm)
        {
            algorithm.TreeEdge += new EdgeAction<TVertex, TEdge>(TreeEdge);
            return new DisposableAction(
                () => algorithm.TreeEdge -= new EdgeAction<TVertex, TEdge>(TreeEdge)
                );
        }

        void TreeEdge(TEdge e)
        {
            this.vertexPredecessors[e.Target] = e;
        }

        public bool TryGetPath(TVertex vertex, out IEnumerable<TEdge> path)
        {
            return EdgeExtensions.TryGetPath(this.VertexPredecessors, vertex, out path);
        }
    }
}
