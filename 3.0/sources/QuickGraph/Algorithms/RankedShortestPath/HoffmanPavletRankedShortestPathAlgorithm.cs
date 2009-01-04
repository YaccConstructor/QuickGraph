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
        : RankedShortestPathAlgorithmBase<TVertex, TEdge, IBidirectionalGraph<TVertex, TEdge>>
        where TEdge: IEdge<TVertex>
    {
        private readonly Func<TEdge, double> edgeWeights;
        private TVertex goalVertex;
        private bool goalVertexSet = false;

        public HoffmanPavletRankedShortestPathAlgorithm(
            IBidirectionalGraph<TVertex, TEdge> visitedGraph,
            Func<TEdge, double> edgeWeights)
            : this(null, visitedGraph, edgeWeights, ShortestDistanceRelaxer.Instance)
        { }

        public HoffmanPavletRankedShortestPathAlgorithm(
            IAlgorithmComponent host,
            IBidirectionalGraph<TVertex, TEdge> visitedGraph,
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
            IDictionary<TVertex, TEdge> successors;
            IDictionary<TVertex, double> distances;
            this.ComputeMinimumTree(goal, out successors, out distances);
            if (cancelManager.IsCancelling) return;

            var queue = new FibonacciQueue<DeviationPath, double>(dp => dp.Weight);

            // first shortest path
            this.EnqueueFirstShortestPath(queue, successors, distances, root);

            while (queue.Count > 0 && this.ComputedShortestPathCount < this.ShortestPathCount)
            {
                if (cancelManager.IsCancelling) return;

                var deviation = queue.Dequeue();

                // turn into path
                var path = new List<TEdge>();
                for (int i = 0; i < deviation.DeviationIndex; ++i)
                    path.Add(deviation.ParentPath[i]);
                path.Add(deviation.DeviationEdge);
                int startEdge = path.Count;
                this.AppendShortestPath(path, successors, deviation.DeviationEdge.Target);

                Contract.Assert(deviation.Weight == Enumerable.Sum(path, e => edgeWeights(e)));
                Contract.Assert(path.Count > 0);

                // add to list if loopless
                if (!EdgeExtensions.HasCycles<TVertex, TEdge>(path))
                    this.AddComputedShortestPath(path);

                // append new deviation paths
                this.EnqueueDeviationPaths(
                    queue,
                    root,
                    successors,
                    distances,
                    path.ToArray(),
                    startEdge
                    );
            }
        }

        private void EnqueueFirstShortestPath(
            IQueue<DeviationPath> queue,
            IDictionary<TVertex, TEdge> successors, 
            IDictionary<TVertex, double> distances, 
            TVertex root)
        {
            Contract.Requires(queue != null);
            Contract.Requires(queue.Count == 0);
            Contract.Requires(successors != null);
            Contract.Requires(distances != null);
            Contract.Requires(root != null);

            var path = new List<TEdge>();
            AppendShortestPath(
                path,
                successors,
                root);
            if (path.Count == 0)
                return; // unreachable vertices

            if (!EdgeExtensions.HasCycles<TVertex, TEdge>(path))
                this.AddComputedShortestPath(path);

            // create deviation paths
            this.EnqueueDeviationPaths(
                queue,
                root,
                successors,
                distances,
                path.ToArray(),
                0);
        }

        private void ComputeMinimumTree(
            TVertex goal, 
            out IDictionary<TVertex, TEdge> successors, 
            out IDictionary<TVertex, double> distances)
        {
            var reversedGraph = new ReversedBidirectionalGraph<TVertex, TEdge>(this.VisitedGraph);
            var successorsObserver = new VertexPredecessorRecorderObserver<TVertex, ReversedEdge<TVertex, TEdge>>();
            Func<ReversedEdge<TVertex, TEdge>, double> reversedEdgeWeight = e => this.edgeWeights(e.OriginalEdge);
            var distancesObserser = new VertexDistanceRecorderObserver<TVertex, ReversedEdge<TVertex, TEdge>>(reversedEdgeWeight);
            var shortestpath = new DijkstraShortestPathAlgorithm<TVertex, ReversedEdge<TVertex, TEdge>>(
                this, reversedGraph, reversedEdgeWeight, this.DistanceRelaxer);
            using (ObserverScope.Create(shortestpath, successorsObserver))
            using (ObserverScope.Create(shortestpath, distancesObserser))
                shortestpath.Compute(goal);

            successors = new Dictionary<TVertex, TEdge>();
            foreach (var kv in successorsObserver.VertexPredecessors)
                successors.Add(kv.Key, kv.Value.OriginalEdge);
            distances = distancesObserser.Distances;
        }

        private void EnqueueDeviationPaths(
            IQueue<DeviationPath> queue, 
            TVertex root,
            IDictionary<TVertex, TEdge> successors, 
            IDictionary<TVertex, double> distances, 
            TEdge[] path,
            int startEdge
            )
        {
            Contract.Requires(queue != null);
            Contract.Requires(root != null);
            Contract.Requires(successors != null);
            Contract.Requires(distances != null);
            Contract.Requires(path != null);
            Contract.Requires(EdgeExtensions.IsAdjacent<TVertex, TEdge>(path[0], root));
            Contract.Requires(0 <= startEdge && startEdge < path.Length);

            int iedge = 0;
            TVertex previousVertex = root;
            double previousWeight = 0;
            for (; iedge < path.Length; ++iedge)
            {
                var edge = path[iedge];
                if (iedge >= startEdge)
                    this.EnqueueDeviationPaths(
                        queue, 
                        distances, 
                        path, 
                        iedge, 
                        previousVertex, 
                        previousWeight);

                // update counter
                previousVertex = edge.Target;
                previousWeight += this.edgeWeights(edge);
            }
        }

        private void EnqueueDeviationPaths(
            IQueue<DeviationPath> queue, 
            IDictionary<TVertex, double> distances, 
            TEdge[] path, 
            int iedge, 
            TVertex previousVertex, 
            double previousWeight)
        {
            Contract.Requires(queue != null);
            Contract.Requires(distances != null);
            Contract.Requires(path != null);

            var edge = path[iedge];
            foreach (var deviationEdge in this.VisitedGraph.OutEdges(previousVertex))
            {
                if (deviationEdge.Equals(edge) ||
                    EdgeExtensions.IsSelfEdge<TVertex, TEdge>(deviationEdge)) continue;

                var atarget = deviationEdge.Target;
                double adistance;
                if (distances.TryGetValue(atarget, out adistance))
                {
                    var deviationWeight =
                        previousWeight +
                        this.edgeWeights(deviationEdge) +
                        adistance;

                    var deviation = new DeviationPath(
                        path, iedge,
                        deviationEdge,
                        deviationWeight
                        );
                    queue.Enqueue(deviation);
                }
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
            Contract.Ensures(path[path.Count - 1].Target.Equals(this.goalVertex));

            var current = startVertex;
            TEdge edge;
            while(successors.TryGetValue(current, out edge))
            {
                path.Add(edge);
                current = edge.Target;
            }
        }

        struct DeviationPath
        {
            public readonly TEdge[] ParentPath;
            public readonly int DeviationIndex;
            public readonly TEdge DeviationEdge;
            public readonly double Weight;
            public DeviationPath(
                TEdge[] parentPath, 
                int deviationIndex,
                TEdge deviationEdge,
                double weight)
            {
                Contract.Requires(parentPath != null);
                Contract.Requires(0 <= deviationIndex && deviationIndex < parentPath.Length);
                Contract.Requires(deviationEdge != null);
                Contract.Requires(weight >= 0);

                this.ParentPath = parentPath;
                this.DeviationIndex = deviationIndex;
                this.DeviationEdge = deviationEdge;
                this.Weight = weight;
            }

            public override string ToString()
            {
                return String.Format("{0} at {1}", this.Weight, this.DeviationEdge);
            }
        }
    }
}
