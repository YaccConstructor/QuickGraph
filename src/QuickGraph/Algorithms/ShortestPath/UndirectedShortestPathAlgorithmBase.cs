using System;
using System.Collections.Generic;
using QuickGraph.Algorithms.Search;
using QuickGraph.Algorithms.Observers;
using QuickGraph.Collections;
using QuickGraph.Algorithms.Services;
using System.Diagnostics.Contracts;

namespace QuickGraph.Algorithms.ShortestPath
{
#if !SILVERLIGHT
    [Serializable]
#endif
    public abstract class UndirectedShortestPathAlgorithmBase<TVertex, TEdge>
        : RootedAlgorithmBase<TVertex, IUndirectedGraph<TVertex,TEdge>>
        , IUndirectedTreeBuilderAlgorithm<TVertex, TEdge>
        where TEdge : IEdge<TVertex>
    {
        private readonly Func<TEdge, double> weights;
        private readonly IDistanceRelaxer distanceRelaxer;
        private Dictionary<TVertex, GraphColor> vertexColors;
        private Dictionary<TVertex, double> distances;

        protected UndirectedShortestPathAlgorithmBase(
            IAlgorithmComponent host,
            IUndirectedGraph<TVertex,TEdge> visitedGraph,
            Func<TEdge, double> weights
            )
            :this(host, visitedGraph, weights, DistanceRelaxers.ShortestDistance)
        {}

        protected UndirectedShortestPathAlgorithmBase(
            IAlgorithmComponent host,
            IUndirectedGraph<TVertex, TEdge> visitedGraph,
            Func<TEdge, double> weights,
            IDistanceRelaxer distanceRelaxer
            )
            :base(host, visitedGraph)
        {
            Contract.Requires(weights != null);
            Contract.Requires(distanceRelaxer != null);

            this.weights = weights;
            this.distanceRelaxer = distanceRelaxer;
        }

        public Dictionary<TVertex, GraphColor> VertexColors
        {
            get
            {
                return this.vertexColors;
            }
        }

        public GraphColor GetVertexColor(TVertex vertex)
        {
            return this.vertexColors[vertex];
        }

        public bool TryGetDistance(TVertex vertex, out double distance)
        {
            Contract.Requires(vertex != null);
            return this.distances.TryGetValue(vertex, out distance);
        }

        public Dictionary<TVertex, double> Distances
        {
            get { return this.distances; }
        }

        protected Func<TVertex, double> DistancesIndexGetter()
        {
            return AlgorithmExtensions.GetIndexer<TVertex, double>(this.distances);
        }

        public Func<TEdge, double> Weights
        {
            get { return this.weights; }
        }

        public IDistanceRelaxer DistanceRelaxer
        {
            get { return this.distanceRelaxer; }
        }

        protected override void Initialize()
        {
            base.Initialize();
            this.vertexColors = new Dictionary<TVertex, GraphColor>(this.VisitedGraph.VertexCount);
            this.distances = new Dictionary<TVertex, double>(this.VisitedGraph.VertexCount);
        }

        /// <summary>
        /// Invoked when the distance label for the target vertex is decreased. 
        /// The edge that participated in the last relaxation for vertex v is 
        /// an edge in the shortest paths tree.
        /// </summary>
        public event UndirectedEdgeAction<TVertex, TEdge> TreeEdge;

        /// <summary>
        /// Raises the <see cref="TreeEdge"/> event.
        /// </summary>
        /// <param name="e">edge that raised the event</param>
        /// <param name="reversed"></param>
        protected virtual void OnTreeEdge(TEdge e, bool reversed)
        {
            var eh = this.TreeEdge;
            if (eh != null)
                eh(this, new UndirectedEdgeEventArgs<TVertex, TEdge>(e, reversed));
        }

        protected bool Relax(TEdge e, TVertex source, TVertex target)
        {
            Contract.Requires(e != null);
            Contract.Requires(source != null);
            Contract.Requires(target != null);
            Contract.Requires(
                (e.Source.Equals(source) && e.Target.Equals(target))
                || (e.Source.Equals(target) && e.Target.Equals(source))
                );

            double du = this.distances[source];
            double dv = this.distances[target];
            double we = this.Weights(e);

            var relaxer = this.DistanceRelaxer;
            var duwe = relaxer.Combine(du, we);
            if (relaxer.Compare(duwe, dv) < 0)
            {
                this.distances[target] = duwe;
                return true;
            }
            else
                return false;
        }
    }
}
