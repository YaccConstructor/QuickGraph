using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using QuickGraph.Algorithms.ShortestPath;

namespace QuickGraph.Algorithms.Observers
{
    /// <summary>
    /// A distance recorder for undirected tree builder algorithms
    /// </summary>
    /// <typeparam name="TVertex">type of a vertex</typeparam>
    /// <typeparam name="TEdge">type of an edge</typeparam>
#if !SILVERLIGHT
    [Serializable]
#endif
    public sealed class UndirectedVertexDistanceRecorderObserver<TVertex, TEdge> 
        : IObserver<IUndirectedTreeBuilderAlgorithm<TVertex, TEdge>>
        where TEdge : IEdge<TVertex>
    {
        private readonly IDistanceRelaxer distanceRelaxer;
        private readonly Func<TEdge, double> edgeWeights;
        private readonly IDictionary<TVertex, double> distances;

        public UndirectedVertexDistanceRecorderObserver(Func<TEdge, double> edgeWeights)
            : this(edgeWeights, DistanceRelaxers.EdgeShortestDistance, new Dictionary<TVertex, double>())
        {}

        public UndirectedVertexDistanceRecorderObserver(
            Func<TEdge, double> edgeWeights,
            IDistanceRelaxer distanceRelaxer,
            IDictionary<TVertex, double> distances)
        {
            Contract.Requires(edgeWeights != null);
            Contract.Requires(distanceRelaxer != null);
            Contract.Requires(distances != null);

            this.edgeWeights = edgeWeights;
            this.distanceRelaxer = distanceRelaxer;
            this.distances = distances;
        }

        public IDistanceRelaxer DistanceRelaxer
        {
            get { return this.distanceRelaxer; }
        }

        public Func<TEdge, double> EdgeWeights
        {
            get { return this.edgeWeights; }
        }

        public IDictionary<TVertex, double> Distances
        {
            get { return this.distances; }
        }

        public IDisposable Attach(IUndirectedTreeBuilderAlgorithm<TVertex, TEdge> algorithm)
        {
            algorithm.TreeEdge += new UndirectedEdgeAction<TVertex, TEdge>(this.TreeEdge);
            return new DisposableAction(
                () => algorithm.TreeEdge -= new UndirectedEdgeAction<TVertex, TEdge>(this.TreeEdge)
                );
        }

        private void TreeEdge(Object sender, UndirectedEdgeEventArgs<TVertex,TEdge> args)
        {
            double sourceDistance;
            if (!this.distances.TryGetValue(args.Source, out sourceDistance))
                this.distances[args.Source] = sourceDistance = this.distanceRelaxer.InitialDistance;
            this.distances[args.Target] = this.DistanceRelaxer.Combine(sourceDistance, this.edgeWeights(args.Edge));
        }
    }
}
