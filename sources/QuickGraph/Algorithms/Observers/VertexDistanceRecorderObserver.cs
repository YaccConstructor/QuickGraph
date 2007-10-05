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
    public sealed class VertexDistanceRecorderObserver<TVertex, TEdge> :
        IObserver<TVertex,TEdge,IDistanceRecorderAlgorithm<TVertex, TEdge>>
        where TEdge : IEdge<TVertex>
    {
        private IDictionary<TVertex, int> distances;

        public VertexDistanceRecorderObserver()
            :this(new Dictionary<TVertex,int>())
        {}

        public VertexDistanceRecorderObserver(IDictionary<TVertex, int> distances)
        {
            if (distances == null)
                throw new ArgumentNullException("distances");
            this.distances = distances;
        }

        public IDictionary<TVertex, int> Distances
        {
            get { return this.distances; }
        }

        public void Attach(IDistanceRecorderAlgorithm<TVertex, TEdge> algorithm)
        {
            algorithm.InitializeVertex += new VertexEventHandler<TVertex>(this.InitializeVertex);
            algorithm.DiscoverVertex += new VertexEventHandler<TVertex>(this.DiscoverVertex);
            algorithm.TreeEdge += new EdgeEventHandler<TVertex, TEdge>(this.TreeEdge);
        }

        public void Detach(IDistanceRecorderAlgorithm<TVertex, TEdge> algorithm)
        {
            algorithm.InitializeVertex -= new VertexEventHandler<TVertex>(this.InitializeVertex);
            algorithm.DiscoverVertex -= new VertexEventHandler<TVertex>(this.DiscoverVertex);
            algorithm.TreeEdge -= new EdgeEventHandler<TVertex, TEdge>(this.TreeEdge);
        }

        private void InitializeVertex(Object sender, VertexEventArgs<TVertex> args)
        {
            this.distances[args.Vertex] = int.MaxValue;
        }

        private void DiscoverVertex(Object sender, VertexEventArgs<TVertex> args)
        {
            this.distances[args.Vertex] = 0;
        }

        private void TreeEdge(Object sender, EdgeEventArgs<TVertex,TEdge> args)
        {
            this.distances[args.Edge.Target] = 
                this.distances[args.Edge.Source] + 1;
        }
    }
}
