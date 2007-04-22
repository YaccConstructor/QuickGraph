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
    public sealed class VertexTimeStamperObserver<Vertex, Edge> :
        IObserver<Vertex, Edge, IVertexTimeStamperAlgorithm<Vertex, Edge>>
        where Edge : IEdge<Vertex>
    {
        private IDictionary<Vertex, int> discoverTimes;
        private IDictionary<Vertex, int> finishTimes;
        private int currentTime = 0;

        public VertexTimeStamperObserver()
            :this(new Dictionary<Vertex,int>(), new Dictionary<Vertex,int>())
        {}

        public VertexTimeStamperObserver(
            IDictionary<Vertex, int> discoverTimes,
            IDictionary<Vertex, int> finishTimes)
        {
            if (discoverTimes == null)
                throw new ArgumentNullException("discoverTimes");
            if (finishTimes == null)
                throw new ArgumentNullException("finishTimes");
            this.discoverTimes = discoverTimes;
            this.finishTimes = finishTimes;
        }

        public IDictionary<Vertex, int> DiscoverTimes
        {
            get { return this.discoverTimes; }
        }

        public IDictionary<Vertex, int> FinishTimes
        {
            get { return this.finishTimes; }
        }

        public void Attach(IVertexTimeStamperAlgorithm<Vertex, Edge> algorithm)
        {
            algorithm.DiscoverVertex+=new VertexEventHandler<Vertex>(DiscoverVertex);
            algorithm.FinishVertex+=new VertexEventHandler<Vertex>(FinishVertex);
        }

        public void Detach(IVertexTimeStamperAlgorithm<Vertex, Edge> algorithm)
        {
            algorithm.DiscoverVertex -= new VertexEventHandler<Vertex>(DiscoverVertex);
            algorithm.FinishVertex -= new VertexEventHandler<Vertex>(FinishVertex);
        }

        void DiscoverVertex(Object sender, VertexEventArgs<Vertex> e)
        {
            this.discoverTimes[e.Vertex] = this.currentTime++;
        }

        void FinishVertex(Object sender, VertexEventArgs<Vertex> e)
        {
            this.finishTimes[e.Vertex] = this.currentTime++;
        }
    }
}
