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
        : RankedShortestPathAlgorithmBase<TVertex, TEdge, IUndirectedGraph<TVertex, TEdge>>
        where TEdge: IEdge<TVertex>
    {
        private readonly Func<TEdge, double> edgeWeights;
        private TVertex goalVertex;
        private bool goalVertexSet = false;

        public HoffmanPavletRankedShortestPathAlgorithm(
            IUndirectedGraph<TVertex, TEdge> visitedGraph,
            Func<TEdge, double> edgeWeights)
            : this(null, visitedGraph, edgeWeights, ShortestDistanceRelaxer.Instance)
        { }

        public HoffmanPavletRankedShortestPathAlgorithm(
            IAlgorithmComponent host,
            IUndirectedGraph<TVertex, TEdge> visitedGraph,
            Func<TEdge, double> edgeWeights,
            IDistanceRelaxer distanceRelaxer)
            :base(host, visitedGraph, distanceRelaxer)
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
            var cancelManager = this.Services.CancelManager;
            TVertex root;
            if (!this.TryGetRootVertex(out root))
                throw new RootVertexNotSpecifiedException();
            TVertex goal;
            if (!this.TryGetGoalVertex(out goal))
                throw new RootVertexNotSpecifiedException();

            // start by building the minimum tree starting from the goal vertex.
            var successorsObserver = new UndirectedVertexPredecessorRecorderObserver<TVertex, TEdge>();
            var distancesObserser = new UndirectedVertexDistanceRecorderObserver<TVertex, TEdge>(this.edgeWeights);
            var shortestpath = new UndirectedDijkstraShortestPathAlgorithm<TVertex, TEdge>(
                this, this.VisitedGraph, this.edgeWeights, this.DistanceRelaxer);
            using (ObserverScope.Create(shortestpath, successorsObserver))
            using (ObserverScope.Create(shortestpath, distancesObserser))
                shortestpath.Compute(goal);
            if (cancelManager.IsCancelling) return;

            var successors = successorsObserver.VertexPredecessors;
            var distances = distancesObserser.Distances;

            // first shortest path
            var path = new List<TEdge>();
            AppendShortestPath(
                path,
                successors,
                root);
            this.AddComputedShortestPath(path);

            var queue = new FibonacciQueue<DeviationPath, double>(dp => dp.Weight);

            // create deviation paths
            this.CreateDeviationPaths(
                queue,
                root,
                successors,
                distances,
                path,
                0);

            while (queue.Count > 0 && this.ComputedShortestPathCount < this.ShortestPathCount)
            {
                var deviation = queue.Dequeue();
                // turn into path

                // add to list
            }
        }

        private void CreateDeviationPaths(
            IQueue<DeviationPath> queue, 
            TVertex root,
            IDictionary<TVertex, TEdge> successors, 
            IDictionary<TVertex, double> distances, 
            List<TEdge> path,
            int startEdge)
        {
            Contract.Requires(queue != null);
            Contract.Requires(root != null);
            Contract.Requires(successors != null);
            Contract.Requires(distances != null);
            Contract.Requires(path != null);
            Contract.Requires(EdgeExtensions.IsAdjacent<TVertex, TEdge>(path[0], root));
            Contract.Requires(0 <= startEdge && startEdge < path.Count);

            int iedge = 0;
            TVertex previousVertex = root;
            double previousWeight = 0;
            for (; iedge < startEdge; ++iedge)
            {
                var edge = path[iedge];
                if (iedge >= startEdge)
                {
                    // find best candidate amound adjacent edges
                    TEdge deviationEdge = edge;
                    double deviationWeight = double.MaxValue;
                    foreach (var aedge in this.VisitedGraph.AdjacentEdges(previousVertex))
                    {
                        var atarget = EdgeExtensions.GetOtherVertex<TVertex, TEdge>(aedge, previousVertex);
                        var aweight = this.edgeWeights(aedge) + distances[atarget];// + path weight
                        if (aweight < deviationWeight)
                        {
                            deviationEdge = aedge;
                            deviationWeight = aweight;
                        }
                    }

                    // found deviation
                    if (!deviationEdge.Equals(edge))
                    {
                        var weight = previousWeight + deviationWeight;
                        var deviation = new DeviationPath(path, iedge, deviationEdge, deviationWeight);
                        queue.Enqueue(deviation);
                    }
                }

                // update counter
                previousVertex = EdgeExtensions.GetOtherVertex<TVertex, TEdge>(edge, previousVertex);
                previousWeight += this.edgeWeights(edge);
            }
        }

        private void AppendShortestPath(
            List<TEdge> path, 
            IDictionary<TVertex, TEdge> successors, 
            TVertex startVertex)
        {
            Contract.Requires(path != null);
            Contract.Requires(successors != null);
            Contract.Requires(startVertex != null);
            Contract.Ensures(
                path[path.Count - 1].Source.Equals(this.goalVertex)
                || path[path.Count - 1].Target.Equals(this.goalVertex)
                );

            var current = startVertex;
            TEdge edge;
            while(successors.TryGetValue(current, out edge))
            {
                path.Add(edge);
                current = edge.Source.Equals(current) ? edge.Source : edge.Target;
            }
        }

        class DeviationPath
        {
            public readonly List<TEdge> ParentPath;
            public readonly int DeviationIndex;
            public readonly TEdge DeviationEdge;
            public readonly double Weight;
            public DeviationPath(
                List<TEdge> parentPath, 
                int deviationIndex,
                TEdge deviationEdge,
                double weight)
            {
                Contract.Requires(parentPath != null);
                Contract.Requires(0 <= deviationIndex && deviationIndex < parentPath.Count);
                Contract.Requires(deviationEdge != null);
                Contract.Requires(weight >= 0);

                this.ParentPath = parentPath;
                this.DeviationIndex = deviationIndex;
                this.DeviationEdge = deviationEdge;
                this.Weight = weight;
            }
        }
    }
}
