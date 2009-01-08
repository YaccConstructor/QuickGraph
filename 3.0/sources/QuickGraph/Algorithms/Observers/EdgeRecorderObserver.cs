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
    public sealed class EdgeRecorderObserver<TVertex, TEdge> :
        IObserver<ITreeBuilderAlgorithm<TVertex, TEdge>>
        where TEdge : IEdge<TVertex>
    {
        private readonly IList<TEdge> edges;

        public EdgeRecorderObserver()
            :this(new List<TEdge>())
        {}

        public EdgeRecorderObserver(IList<TEdge> edges)
        {
            Contract.Requires(edges != null);

            this.edges = edges;
        }

        public IList<TEdge> Edges
        {
            get
            {
                return this.edges;
            }
        }

        public void Attach(ITreeBuilderAlgorithm<TVertex, TEdge> algorithm)
        {
            Contract.Requires(algorithm != null);

            algorithm.TreeEdge +=new EdgeEventHandler<TVertex,TEdge>(RecordEdge);
        }

        public void Detach(ITreeBuilderAlgorithm<TVertex, TEdge> algorithm)
        {
            Contract.Requires(algorithm != null);

            algorithm.TreeEdge -= new EdgeEventHandler<TVertex, TEdge>(RecordEdge);
        }

        private void RecordEdge(Object sender, TEdge args)
        {
            Contract.Requires(args != null);

            this.Edges.Add(args);
        }
    }
}
