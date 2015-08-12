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
    public sealed class VertexTimeStamperObserver<TVertex, TEdge> :
        IObserver<IVertexTimeStamperAlgorithm<TVertex, TEdge>>
        where TEdge : IEdge<TVertex>
    {
        private readonly Dictionary<TVertex, int> discoverTimes;
        private readonly Dictionary<TVertex, int> _finishTimes;
        private int currentTime = 0;


        public VertexTimeStamperObserver()
            :this(new Dictionary<TVertex,int>(), new Dictionary<TVertex,int>())
        {}

        public VertexTimeStamperObserver(Dictionary<TVertex, int> discoverTimes)
        {
            Contract.Requires(discoverTimes != null);

            this.discoverTimes = discoverTimes;
            this._finishTimes = null;
        }

        public VertexTimeStamperObserver(
            Dictionary<TVertex, int> discoverTimes,
            Dictionary<TVertex, int> finishTimes)
        {
            Contract.Requires(discoverTimes != null);
            Contract.Requires(finishTimes != null);

            this.discoverTimes = discoverTimes;
            this._finishTimes = finishTimes;
        }

        public IDictionary<TVertex, int> DiscoverTimes
        {
            get { return this.discoverTimes; }
        }

        public IDictionary<TVertex, int> FinishTimes
        {
            get { return this._finishTimes; }
        }

        public IDisposable Attach(IVertexTimeStamperAlgorithm<TVertex, TEdge> algorithm)
        {
            algorithm.DiscoverVertex+=new VertexAction<TVertex>(DiscoverVertex);
            if (this._finishTimes != null)
                algorithm.FinishVertex+=new VertexAction<TVertex>(FinishVertex);

            return new DisposableAction(
                () =>
                {
                    algorithm.DiscoverVertex -= new VertexAction<TVertex>(DiscoverVertex);
                    if (this._finishTimes != null)
                        algorithm.FinishVertex -= new VertexAction<TVertex>(FinishVertex);
                });
        }

        void DiscoverVertex(TVertex v)
        {
            this.discoverTimes[v] = this.currentTime++;
        }

        void FinishVertex(TVertex v)
        {
            this._finishTimes[v] = this.currentTime++;
        }
    }
}
