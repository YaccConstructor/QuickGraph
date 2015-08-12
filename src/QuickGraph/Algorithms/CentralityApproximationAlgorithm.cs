using System;
using System.Collections.Generic;

using QuickGraph.Algorithms.ShortestPath;
using QuickGraph.Algorithms.Observers;
using System.Diagnostics.Contracts;

namespace QuickGraph.Algorithms
{
#if !SILVERLIGHT
    [Serializable]
#endif
    public sealed class CentralityApproximationAlgorithm<TVertex, TEdge> :
        AlgorithmBase<IVertexListGraph<TVertex,TEdge>>
        where TEdge : IEdge<TVertex>
    {
        private Random rand = new Random();
        private DijkstraShortestPathAlgorithm<TVertex, TEdge> dijkstra;
        private VertexPredecessorRecorderObserver<TVertex, TEdge> predecessorRecorder;
        private int maxIterationCount = 50;
        private IDictionary<TVertex, double> centralities = new Dictionary<TVertex, double>();

        public CentralityApproximationAlgorithm(
            IVertexListGraph<TVertex, TEdge> visitedGraph,
            Func<TEdge, double> distances
            )
            :base(visitedGraph)
        {
            Contract.Requires(distances != null);

            this.dijkstra = new DijkstraShortestPathAlgorithm<TVertex, TEdge>(
                this.VisitedGraph,
                distances,
                DistanceRelaxers.ShortestDistance
                );
            this.predecessorRecorder = new VertexPredecessorRecorderObserver<TVertex, TEdge>();
            this.predecessorRecorder.Attach(this.dijkstra);
        }

        public Func<TEdge, double> Distances
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

        protected override void Initialize()
        {
            base.Initialize();
            this.centralities.Clear();
            foreach (var v in this.VisitedGraph.Vertices)
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
                TVertex v = RandomGraphFactory.GetVertex<TVertex, TEdge>(this.VisitedGraph, this.Rand);
                this.dijkstra.Compute(v);

                foreach (var u in this.VisitedGraph.Vertices)
                {
                    double d;
                    if (this.dijkstra.TryGetDistance(u, out d))
                        this.centralities[u] += n * d / (this.MaxIterationCount * (n - 1));
                }
            }

            // update
            foreach (var v in this.centralities.Keys)
                this.centralities[v] = 1.0/this.centralities[v];
        }
    }
}
