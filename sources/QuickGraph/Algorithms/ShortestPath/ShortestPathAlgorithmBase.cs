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
        private IDictionary<Vertex, GraphColor> vertexColors;
        private IDictionary<Vertex, double> distances;
        private IDictionary<Edge, double> weights;

        public ShortestPathAlgorithmBase(
            Graph visitedGraph,
            IDictionary<Edge, double> weights
            )
            :base(visitedGraph)
        {
            if (weights == null)
                throw new ArgumentNullException("weights");

            this.vertexColors = new Dictionary<Vertex, GraphColor>();
            this.distances = new Dictionary<Vertex, double>();
            this.weights = weights;
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

        protected static bool Compare(double a, double b)
        {
            return a < b;
        }

        protected static double Combine(double d, double w)
        {
            return d + w;
        }
    }
}
