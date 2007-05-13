using System;
using System.Collections.Generic;
using QuickGraph.Algorithms.Search;
using QuickGraph.Algorithms.Observers;
using QuickGraph.Collections;

namespace QuickGraph.Algorithms.ShortestPath
{
    [Serializable]
    public abstract class ShortestPathAlgorithmBase<Vertex, Edge, Graph> :
        RootedAlgorithmBase<Vertex,Graph>
        where Edge : IEdge<Vertex>
    {
        private readonly IDictionary<Vertex, GraphColor> vertexColors;
        private readonly IDictionary<Vertex, double> distances;
        private readonly IDictionary<Edge, double> weights;
        private readonly IDistanceRelaxer distanceRelaxer;

        protected ShortestPathAlgorithmBase(
            Graph visitedGraph,
            IDictionary<Edge, double> weights
            )
            :this(visitedGraph, weights, new ShortestDistanceRelaxer())
        {}

        protected ShortestPathAlgorithmBase(
            Graph visitedGraph,
            IDictionary<Edge, double> weights,
            IDistanceRelaxer distanceRelaxer
            )
            :base(visitedGraph)
        {
            if (weights == null)
                throw new ArgumentNullException("weights");
            if (distanceRelaxer == null)
                throw new ArgumentNullException("distanceRelaxer");

            this.vertexColors = new Dictionary<Vertex, GraphColor>();
            this.distances = new Dictionary<Vertex, double>();
            this.weights = weights;
            this.distanceRelaxer = distanceRelaxer;
        }

        public static Dictionary<Edge, double> UnaryWeightsFromEdgeList(
            IEdgeListGraph<Vertex, Edge> graph)
        {
            if (graph == null)
                throw new ArgumentNullException("graph");
            Dictionary<Edge, double> weights = new Dictionary<Edge, double>();
            foreach (Edge e in graph.Edges)
                weights.Add(e, 1);
            return weights;
        }

        public static Dictionary<Edge, double> UnaryWeightsFromVertexList(
            IVertexListGraph<Vertex, Edge> graph)
        {
            if (graph == null)
                throw new ArgumentNullException("graph");
            Dictionary<Edge, double> weights = new Dictionary<Edge, double>();
            foreach (Vertex v in graph.Vertices)
                foreach (Edge e in graph.OutEdges(v))
                    weights.Add(e, 1);
            return weights;
        }

        public IDictionary<Vertex, GraphColor> VertexColors
        {
            get
            {
                return this.vertexColors;
            }
        }

        public IDictionary<Vertex, double> Distances
        {
            get
            {
                return this.distances;
            }
        }

        public IDictionary<Edge, double> Weights
        {
            get { return this.weights; }
        }

        public IDistanceRelaxer DistanceRelaxer
        {
            get { return this.distanceRelaxer; }
        }

        protected bool Compare(double a, double b)
        {
            return this.distanceRelaxer.Compare(a, b);
        }

        protected double Combine(double distance, double weight)
        {
            return this.distanceRelaxer.Combine(distance, weight);
        }
    }
}
