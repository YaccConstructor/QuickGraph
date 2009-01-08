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
    public sealed class VertexRecorderObserver<TVertex, TEdge> :
        IObserver<IVertexTimeStamperAlgorithm<TVertex, TEdge>>
        where TEdge : IEdge<TVertex>
    {
        private readonly IList<TVertex> vertices;
        public VertexRecorderObserver()
            : this(new List<TVertex>())
        { }

        public VertexRecorderObserver(IList<TVertex> vertices)
        {
            if (vertices == null)
                throw new ArgumentNullException("edges");
            this.vertices = vertices;
        }

        public IEnumerable<TVertex> Vertices
        {
            get
            {
                return this.vertices;
            }
        }

        public void Attach(IVertexTimeStamperAlgorithm<TVertex, TEdge> algorithm)
        {
            Contract.Requires(algorithm != null);

            algorithm.DiscoverVertex += new VertexEventHandler<TVertex>(algorithm_DiscoverVertex);
        }

        public void Detach(IVertexTimeStamperAlgorithm<TVertex, TEdge> algorithm)
        {
            Contract.Requires(algorithm != null);
            algorithm.DiscoverVertex -= new VertexEventHandler<TVertex>(algorithm_DiscoverVertex);
        }

        void algorithm_DiscoverVertex(object sender, TVertex v)
        {
            this.vertices.Add(v);
        }
    }
}
