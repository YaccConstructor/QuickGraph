using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;
using QuickGraph.Algorithms.Services;
using QuickGraph.Algorithms.Observers;
using QuickGraph.Algorithms.ShortestPath;
using QuickGraph.Collections;

namespace QuickGraph.Algorithms.RankedShortestPath
{
    /// <summary>
    /// Hoffman and Pavlet K-shortest path algorithm.
    /// </summary>
    /// <remarks>
    /// Reference:
    /// Hoffman, W. and Pavley, R. 1959. A Method for the Solution of the Nth Best Path Problem. 
    /// J. ACM 6, 4 (Oct. 1959), 506-514. DOI= http://doi.acm.org/10.1145/320998.321004
    /// </remarks>
    /// <typeparam name="TVertex"></typeparam>
    /// <typeparam name="TEdge"></typeparam>
    public sealed class HoffmanPavletRankedShortestPathAlgorithm<TVertex, TEdge>
        : RootedAlgorithmBase<TVertex, IUndirectedGraph<TVertex, TEdge>>
        where TEdge: IEdge<TVertex>
    {
        private readonly Func<TEdge, double> edgeWeights;
        private TVertex goalVertex;
        private bool goalVertexSet = false;

        public HoffmanPavletRankedShortestPathAlgorithm(
            IUndirectedGraph<TVertex, TEdge> visitedGraph,
            Func<TEdge, double> edgeWeights)
            : this(null, visitedGraph, edgeWeights)
        { }

        public HoffmanPavletRankedShortestPathAlgorithm(
            IAlgorithmComponent host,
            IUndirectedGraph<TVertex, TEdge> visitedGraph,
            Func<TEdge, double> edgeWeights)
            :base(host, visitedGraph)
        {
            Contract.Requires(edgeWeights != null);

            this.edgeWeights = edgeWeights;
        }

        public void SetGoalVertex(TVertex goalVertex)
        {
            Contract.Requires(goalVertex != null);
            Contract.Requires(this.VisitedGraph.ContainsVertex(goalVertex));
            this.goalVertex = goalVertex;
            this.goalVertexSet = true;
        }

        public bool TryGetGoalVertex(out TVertex goalVertex)
        {
            if (this.goalVertexSet)
            {
                goalVertex = this.goalVertex;
                return true;
            }
            else
            {
                goalVertex = default(TVertex);
                return false;
            }
        }

        public void Compute(TVertex rootVertex, TVertex goalVertex)
        {
            Contract.Requires(rootVertex != null);
            Contract.Requires(goalVertex != null);
            Contract.Requires(this.VisitedGraph.ContainsVertex(rootVertex));
            Contract.Requires(this.VisitedGraph.ContainsVertex(goalVertex));

            this.SetRootVertex(rootVertex);
            this.SetGoalVertex(goalVertex);
            this.Compute();
        }

        protected override void InternalCompute()
        {
            TVertex root;
            if (!this.TryGetRootVertex(out root))
                throw new RootVertexNotSpecifiedException();
            TVertex goal;
            if (!this.TryGetGoalVertex(out goal))
                throw new RootVertexNotSpecifiedException();

            // start by building the minimum tree starting from the goal vertex.
            var successorsObserver = new UndirectedVertexPredecessorRecorderObserver<TVertex, TEdge>();
            var distancesObserser = new UndirectedVertexDistanceRecorderObserver<TVertex, TEdge>();
            var shortestpath = new UndirectedDijkstraShortestPathAlgorithm<TVertex, TEdge>(this.VisitedGraph, this.edgeWeights);
            using (ObserverScope.Create(shortestpath, successorsObserver))
            using (ObserverScope.Create(shortestpath, distancesObserser))
                shortestpath.Compute(goal);

            var successors = successorsObserver.VertexPredecessors;
            var distances = distancesObserser.Distances;

            // first shortest path
            //var path = GetShortestPath(successors, root);

            //var queue = new FibonacciQueue<DeviationPath, double>();
        }

        //class DeviationPath
        //{
        //    public readonly TVertex BranchingVertex;
        //    public readonly TVertex NewVertex;
        //    public readonly TEdge DeviationEdge;
        //    public readonly double Weight;
        //}
    }
}
