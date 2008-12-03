using System;
using System.Collections.Generic;
using QuickGraph.Algorithms.Search;
using QuickGraph.Algorithms.Observers;
using QuickGraph.Collections;
using QuickGraph.Algorithms.Services;
using System.Diagnostics.Contracts;

namespace QuickGraph.Algorithms.ShortestPath
{
    [Serializable]
    public abstract class ShortestPathAlgorithmBase<TVertex, TEdge, TGraph> :
        RootedAlgorithmBase<TVertex,TGraph>
        where TEdge : IEdge<TVertex>
    {
        private readonly IDictionary<TVertex, GraphColor> vertexColors;
        private readonly Dictionary<TVertex, double> distances;
        private readonly Func<TEdge, double> weights;
        private readonly IDistanceRelaxer distanceRelaxer;

        protected ShortestPathAlgorithmBase(
            IAlgorithmComponent host,
            TGraph visitedGraph,
            Func<TEdge, double> weights
            )
            :this(host, visitedGraph, weights, ShortestDistanceRelaxer.Instance)
        {}

        protected ShortestPathAlgorithmBase(
            IAlgorithmComponent host,
            TGraph visitedGraph,
            Func<TEdge, double> weights,
            IDistanceRelaxer distanceRelaxer
            )
            :base(host, visitedGraph)
        {
            Contract.Requires(weights != null);
            Contract.Requires(distanceRelaxer != null);

            this.vertexColors = new Dictionary<TVertex, GraphColor>();
            this.distances = new Dictionary<TVertex, double>();
            this.weights = weights;
            this.distanceRelaxer = distanceRelaxer;
        }

        public IDictionary<TVertex, GraphColor> VertexColors
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

        public IDictionary<TVertex, double> Distances
        {
            get { return this.distances; }
        }

        public Func<TEdge, double> Weights
        {
            get { return this.weights; }
        }

        public IDistanceRelaxer DistanceRelaxer
        {
            get { return this.distanceRelaxer; }
        }

        protected bool Relax(TEdge e)
        {
            double du = this.distances[e.Source];
            double dv = this.distances[e.Target];
            double we = this.Weights(e);

            var relaxer = this.DistanceRelaxer;
            var duwe = relaxer.Combine(du, we);
            if (relaxer.Compare(duwe, dv))
            {
                this.distances[e.Target] = duwe;
                return true;
            }
            else
                return false;
        }
    }
}
