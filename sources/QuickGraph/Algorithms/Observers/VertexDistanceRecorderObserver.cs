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
    public sealed class VertexDistanceRecorderObserver<Vertex, Edge> :
        IObserver<Vertex,Edge,IDistanceRecorderAlgorithm<Vertex, Edge>>
        where Edge : IEdge<Vertex>
    {
        private IDictionary<Vertex, int> distances;

        public VertexDistanceRecorderObserver()
            :this(new Dictionary<Vertex,int>())
        {}

        public VertexDistanceRecorderObserver(IDictionary<Vertex, int> distances)
        {
            if (distances == null)
                throw new ArgumentNullException("distances");
            this.distances = distances;
        }

        public IDictionary<Vertex, int> Distances
        {
            get { return this.distances; }
        }

        public void Attach(IDistanceRecorderAlgorithm<Vertex, Edge> algorithm)
        {
            algorithm.InitializeVertex += new VertexEventHandler<Vertex>(this.InitializeVertex);
            algorithm.DiscoverVertex += new VertexEventHandler<Vertex>(this.DiscoverVertex);
            algorithm.TreeEdge += new EdgeEventHandler<Vertex, Edge>(this.TreeEdge);
        }

        public void Detach(IDistanceRecorderAlgorithm<Vertex, Edge> algorithm)
        {
            algorithm.InitializeVertex -= new VertexEventHandler<Vertex>(this.InitializeVertex);
            algorithm.DiscoverVertex -= new VertexEventHandler<Vertex>(this.DiscoverVertex);
            algorithm.TreeEdge -= new EdgeEventHandler<Vertex, Edge>(this.TreeEdge);
        }

        private void InitializeVertex(Object sender, VertexEventArgs<Vertex> args)
        {
            this.distances[args.Vertex] = int.MaxValue;
        }

        private void DiscoverVertex(Object sender, VertexEventArgs<Vertex> args)
        {
            this.distances[args.Vertex] = 0;
        }

        private void TreeEdge(Object sender, EdgeEventArgs<Vertex,Edge> args)
        {
            this.distances[args.Edge.Target] = 
                this.distances[args.Edge.Source] + 1;
        }
    }
}
