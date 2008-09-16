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
    public sealed class VertexTimeStamperObserver<TVertex, TEdge> :
        IObserver<IVertexTimeStamperAlgorithm<TVertex, TEdge>>
        where TEdge : IEdge<TVertex>
    {
        private IDictionary<TVertex, int> discoverTimes;
        private IDictionary<TVertex, int> finishTimes;
        private int currentTime = 0;

        public VertexTimeStamperObserver()
            :this(new Dictionary<TVertex,int>(), new Dictionary<TVertex,int>())
        {}

        public VertexTimeStamperObserver(
            IDictionary<TVertex, int> discoverTimes,
            IDictionary<TVertex, int> finishTimes)
        {
            if (discoverTimes == null)
                throw new ArgumentNullException("discoverTimes");
            if (finishTimes == null)
                throw new ArgumentNullException("finishTimes");
            this.discoverTimes = discoverTimes;
            this.finishTimes = finishTimes;
        }

        public IDictionary<TVertex, int> DiscoverTimes
        {
            get { return this.discoverTimes; }
        }

        public IDictionary<TVertex, int> FinishTimes
        {
            get { return this.finishTimes; }
        }

        public void Attach(IVertexTimeStamperAlgorithm<TVertex, TEdge> algorithm)
        {
            algorithm.DiscoverVertex+=new VertexEventHandler<TVertex>(DiscoverVertex);
            algorithm.FinishVertex+=new VertexEventHandler<TVertex>(FinishVertex);
        }

        public void Detach(IVertexTimeStamperAlgorithm<TVertex, TEdge> algorithm)
        {
            algorithm.DiscoverVertex -= new VertexEventHandler<TVertex>(DiscoverVertex);
            algorithm.FinishVertex -= new VertexEventHandler<TVertex>(FinishVertex);
        }

        void DiscoverVertex(Object sender, VertexEventArgs<TVertex> e)
        {
            this.discoverTimes[e.Vertex] = this.currentTime++;
        }

        void FinishVertex(Object sender, VertexEventArgs<TVertex> e)
        {
            this.finishTimes[e.Vertex] = this.currentTime++;
        }
    }
}
