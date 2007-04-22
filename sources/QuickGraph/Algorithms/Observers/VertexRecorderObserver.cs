using System;
using System.Collections.Generic;

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
    public sealed class VertexRecorderObserver<Vertex, Edge> :
        IObserver<Vertex, Edge, IVertexTimeStamperAlgorithm<Vertex, Edge>>
        where Edge : IEdge<Vertex>
    {
        private IList<Vertex> vertices;
        public VertexRecorderObserver()
            : this(new List<Vertex>())
        { }

        public VertexRecorderObserver(IList<Vertex> vertices)
        {
            if (vertices == null)
                throw new ArgumentNullException("edges");
            this.vertices = vertices;
        }

        public IList<Vertex> Vertices
        {
            get
            {
                return this.vertices;
            }
        }

        public void Attach(IVertexTimeStamperAlgorithm<Vertex, Edge> algorithm)
        {
            algorithm.DiscoverVertex += new VertexEventHandler<Vertex>(algorithm_DiscoverVertex);
        }

        public void Detach(IVertexTimeStamperAlgorithm<Vertex, Edge> algorithm)
        {
            algorithm.DiscoverVertex -= new VertexEventHandler<Vertex>(algorithm_DiscoverVertex);
        }

        void algorithm_DiscoverVertex(object sender, VertexEventArgs<Vertex> e)
        {
            this.Vertices.Add(e.Vertex);
        }
    }
}
