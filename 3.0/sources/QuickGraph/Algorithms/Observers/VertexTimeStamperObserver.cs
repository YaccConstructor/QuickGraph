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
    public sealed class VertexTimeStamperObserver<TVertex, TEdge> :
        IObserver<IVertexTimeStamperAlgorithm<TVertex, TEdge>>
        where TEdge : IEdge<TVertex>
    {
        private readonly IDictionary<TVertex, int> discoverTimes;
        private readonly IDictionary<TVertex, int> finishTimes;
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
            Contract.Requires(algorithm != null);
            algorithm.DiscoverVertex+=new VertexAction<TVertex>(DiscoverVertex);
            algorithm.FinishVertex+=new VertexAction<TVertex>(FinishVertex);
        }

        public void Detach(IVertexTimeStamperAlgorithm<TVertex, TEdge> algorithm)
        {
            Contract.Requires(algorithm != null);

            algorithm.DiscoverVertex -= new VertexAction<TVertex>(DiscoverVertex);
            algorithm.FinishVertex -= new VertexAction<TVertex>(FinishVertex);
        }

        void DiscoverVertex(object sender, TVertex v)
        {
            this.discoverTimes[v] = this.currentTime++;
        }

        void FinishVertex(object sender, TVertex v)
        {
            this.finishTimes[v] = this.currentTime++;
        }
    }
}
