using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using QuickGraph.Algorithms.ShortestPath;

namespace QuickGraph.Algorithms.Observers
{
    /// <summary>
    /// A distance recorder for directed tree builder algorithms
    /// </summary>
    /// <typeparam name="Vertex"></typeparam>
    /// <typeparam name="Edge"></typeparam>
    [Serializable]
    public sealed class VertexDistanceRecorderObserver<TVertex, TEdge>
        : IObserver<ITreeBuilderAlgorithm<TVertex, TEdge>>
        where TEdge : IEdge<TVertex>
    {
        private readonly IDistanceRelaxer distanceRelaxer;
        private readonly Func<TEdge, double> edgeWeights;
        private readonly IDictionary<TVertex, double> distances;

        public VertexDistanceRecorderObserver(Func<TEdge, double> edgeWeights)
            : this(edgeWeights, EdgeDistanceRelaxer.Instance, new Dictionary<TVertex, double>())
        { }

        public VertexDistanceRecorderObserver(
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

        public void Attach(ITreeBuilderAlgorithm<TVertex, TEdge> algorithm)
        {
            Contract.Requires(algorithm != null);

            algorithm.TreeEdge += new EdgeEventHandler<TVertex, TEdge>(this.TreeEdge);
        }

        public void Detach(ITreeBuilderAlgorithm<TVertex, TEdge> algorithm)
        {
            Contract.Requires(algorithm != null);

            algorithm.TreeEdge -= new EdgeEventHandler<TVertex, TEdge>(this.TreeEdge);
        }

        private void TreeEdge(Object sender, EdgeEventArgs<TVertex, TEdge> args)
        {
            var edge = args.Edge;
            var source = edge.Source;
            var target = edge.Target;

            double sourceDistance;
            if (!this.distances.TryGetValue(source, out sourceDistance))
                this.distances[source] = sourceDistance = this.distanceRelaxer.InitialDistance;
            this.distances[target] = this.DistanceRelaxer.Combine(sourceDistance, this.edgeWeights(edge));
        }
    }
}
