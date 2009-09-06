using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuickGraph.Collections;
using QuickGraph.Algorithms.Services;
using System.Diagnostics.Contracts;
using QuickGraph.Algorithms.ShortestPath;

namespace QuickGraph.Algorithms.Search
{
    /// <summary>
    /// NOT FINISHED
    /// </summary>
    /// <remarks>
    /// Algorithm from Frontier Search, Korkf, Zhand, Thayer, Hohwald.
    /// </remarks>
    /// <typeparam name="TVertex">type of the vertices</typeparam>
    /// <typeparam name="TEdge">type of the edges</typeparam>
    internal sealed class BestFirstFrontierSearchAlgorithm<TVertex, TEdge>
        : RootedAlgorithmBase<TVertex, IBidirectionalImplicitGraph<TVertex, TEdge>>
        where TEdge : IEdge<TVertex>
    {
        private readonly Func<TEdge, double> edgeWeights;
        private readonly IDistanceRelaxer distanceRelaxer;
        private TVertex _targetVertex;
        private bool hasTargetVertex;

        public BestFirstFrontierSearchAlgorithm(
            IAlgorithmComponent host,
            IBidirectionalImplicitGraph<TVertex, TEdge> visitedGraph,
            Func<TEdge, double> edgeWeights,
            IDistanceRelaxer distanceRelaxer)
            : base(host, visitedGraph)
        {
            Contract.Requires(edgeWeights != null);
            Contract.Requires(distanceRelaxer != null);

            this.edgeWeights = edgeWeights;
            this.distanceRelaxer = distanceRelaxer;
        }

        public bool TryGetTargetVertex(out TVertex targetVertex)
        {
            targetVertex = this._targetVertex;
            return this.hasTargetVertex;
        }

        public TVertex TargetVertex
        {
            set
            {
                this._targetVertex = value;
                this.hasTargetVertex = this._targetVertex != null;
            }
        }

        protected override void InternalCompute()
        {
            TVertex root;
            if (!this.TryGetRootVertex(out root))
                throw new RootVertexNotSpecifiedException();
            TVertex target;
            if (!this.TryGetTargetVertex(out target))
                throw new InvalidOperationException("target vertex not specified");

            var cancelManager = this.Services.CancelManager;
            var open = new BinaryHeap<double, TVertex>();
            var operators = new Dictionary<TEdge, GraphColor>();
            var g = this.VisitedGraph;

            // (1) Place the initial node on Open, with all its operators marked unused.
            open.Add(this.distanceRelaxer.InitialDistance, root);
            foreach (var edge in g.OutEdges(root))
                operators.Add(edge, GraphColor.White);

            while (open.Count > 0)
            {
                // (3) Else, choose an Open node n of lowest cost for expansion
                var entry = open.RemoveMinimum();
                var cost = entry.Key;
                var n = entry.Value;
                // (2) if there are no nodes on open with finite cost, terminate with failure
                if (double.IsPositiveInfinity(cost))
                    break; // not found

                // (4) if node n is a goal node, terminate with success
                if (n.Equals(target))
                    break;

                // (5) else, expand node n, genearting all successors n' reachable via unused legal operators
                //, compute their cost and delete node n
                var neighbors = new List<KeyValuePair<double, TEdge>>();
                foreach (var edge in g.OutEdges(n))
                {
                    if (operators[edge] == GraphColor.White)
                    {
                        var weight = this.edgeWeights(edge);
                        var ncost = this.distanceRelaxer.Combine(cost, weight);
                        neighbors.Add(new KeyValuePair<double, TEdge>(ncost, edge));
                    }
                }
                // delete node n
                foreach (var edge in g.OutEdges(n))
                    operators.Remove(edge);
                foreach (var edge in g.InEdges(n))
                    operators.Remove(edge);

                // (6) in a directed graph, generate each predecessor node n via an unused operator
                // and create dummy nodes for each with costs of infinity
                foreach (var edge in g.InEdges(n))
                    if (operators[edge] == GraphColor.White)
                        open.Add(double.PositiveInfinity, edge.Source);

                // (7) foreach neighboring node of n' mark the operator from n' to n' as used
                // 
            }
        }
    }
}
