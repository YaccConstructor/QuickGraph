using System;
using System.Collections.Generic;

using QuickGraph.Algorithms.ShortestPath;
using QuickGraph.Algorithms.Observers;

namespace QuickGraph.Algorithms
{
    [Serializable]
    public sealed class CentralityApproximationAlgorithm<Vertex, Edge> :
        AlgorithmBase<IVertexListGraph<Vertex,Edge>>
        where Edge : IEdge<Vertex>
    {
        private Random rand = new Random();
        private DijkstraShortestPathAlgorithm<Vertex, Edge> dijkstra;
        private VertexPredecessorRecorderObserver<Vertex, Edge> predecessorRecorder;
        private int maxIterationCount = 50;
        private IDictionary<Vertex, double> centralities = new Dictionary<Vertex, double>();

        public CentralityApproximationAlgorithm(
            IVertexListGraph<Vertex, Edge> visitedGraph,
            IDictionary<Edge, double> distances
            )
            :base(visitedGraph)
        {
            if (distances==null)
                throw new ArgumentNullException("distances");
            this.dijkstra = new DijkstraShortestPathAlgorithm<Vertex, Edge>(
                this.VisitedGraph,
                distances,
                new ShortestDistanceRelaxer()
                );
            this.predecessorRecorder = new VertexPredecessorRecorderObserver<Vertex, Edge>();
            this.predecessorRecorder.Attach(this.dijkstra);
        }

        public IDictionary<Edge, double> Distances
        {
            get { return this.dijkstra.Weights; }
        }

        public Random Rand
        {
            get { return this.rand; }
            set { this.rand = value; }
        }

        public int MaxIterationCount
        {
            get { return this.maxIterationCount; }
            set { this.maxIterationCount = value; }
        }

        private void Initialize()
        {
            this.centralities.Clear();
            foreach (Vertex v in this.VisitedGraph.Vertices)
                this.centralities.Add(v, 0);
        }

        protected override void InternalCompute()
        {
            if (this.VisitedGraph.VertexCount == 0)
                return;

            // compute temporary values
            int n = this.VisitedGraph.VertexCount;
            for(int i = 0;i<this.MaxIterationCount;++i)
            {
                Vertex v = RandomGraphFactory.GetVertex<Vertex, Edge>(this.VisitedGraph, this.Rand);
                this.dijkstra.Compute(v);

                foreach (Vertex u in this.VisitedGraph.Vertices)
                    this.centralities[u] += n * this.dijkstra.Distances[u] / (this.MaxIterationCount * (n - 1));
            }

            // update
            foreach (Vertex v in this.VisitedGraph.Vertices)
                this.centralities[v] = 1.0/this.centralities[v];
        }
    }
}
