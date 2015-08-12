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
    /// Best first frontier search
    /// </summary>
    /// <remarks>
    /// Algorithm from Frontier Search, Korkf, Zhand, Thayer, Hohwald.
    /// </remarks>
    /// <typeparam name="TVertex">type of the vertices</typeparam>
    /// <typeparam name="TEdge">type of the edges</typeparam>
    public sealed class BestFirstFrontierSearchAlgorithm<TVertex, TEdge>
        : RootedSearchAlgorithmBase<TVertex, IBidirectionalIncidenceGraph<TVertex, TEdge>>
        , ITreeBuilderAlgorithm<TVertex, TEdge>
        where TEdge : IEdge<TVertex>
    {
        private readonly Func<TEdge, double> edgeWeights;
        private readonly IDistanceRelaxer distanceRelaxer;

        public BestFirstFrontierSearchAlgorithm(
            IAlgorithmComponent host,
            IBidirectionalIncidenceGraph<TVertex, TEdge> visitedGraph,
            Func<TEdge, double> edgeWeights,
            IDistanceRelaxer distanceRelaxer)
            : base(host, visitedGraph)
        {
            Contract.Requires(edgeWeights != null);
            Contract.Requires(distanceRelaxer != null);

            this.edgeWeights = edgeWeights;
            this.distanceRelaxer = distanceRelaxer;
        }

        protected override void InternalCompute()
        {
            TVertex root;
            if (!this.TryGetRootVertex(out root))
                throw new InvalidOperationException("root vertex not set");
            TVertex goal;
            if (!this.TryGetGoalVertex(out goal))
                throw new InvalidOperationException("goal vertex not set");

            // little shortcut
            if (root.Equals(goal))
            {
                this.OnGoalReached();
                return; // found it
            }

            var cancelManager = this.Services.CancelManager;
            var open = new BinaryHeap<double, TVertex>(this.distanceRelaxer.Compare);
            var operators = new Dictionary<TEdge, GraphColor>();
            var g = this.VisitedGraph;

            // (1) Place the initial node on Open, with all its operators marked unused.
            open.Add(0, root);
            foreach (var edge in g.OutEdges(root))
                operators.Add(edge, GraphColor.White);

            while (open.Count > 0)
            {
                if (cancelManager.IsCancelling) return;

                // (3) Else, choose an Open node n of lowest cost for expansion
                var entry = open.RemoveMinimum();
                var cost = entry.Key;
                var n = entry.Value;

                // (4) if node n is a goal node, terminate with success
                if (n.Equals(goal))
                {
                    this.OnGoalReached();
                    return;
                }

                // (5) else, expand node n, 
                // genarting all successors n' reachable via unused legal operators
                // compute their cost and delete node n
                foreach (var edge in g.OutEdges(n))
                {
                    if (EdgeExtensions.IsSelfEdge<TVertex, TEdge>(edge)) 
                        continue; // skip self-edges

                    GraphColor edgeColor;
                    bool hasColor = operators.TryGetValue(edge, out edgeColor);
                    if (!hasColor || edgeColor == GraphColor.White)
                    {
                        var weight = this.edgeWeights(edge);
                        var ncost = this.distanceRelaxer.Combine(cost, weight);

                        // (7) foreach neighboring node of n' mark the operator from n to n' as used
                        // (8) for each node n', if there is no copy of n' in open addit
                        // else save on open on the copy of n' with lowest cose. Mark as used all operators
                        // mak as used in any of the copies
                        operators[edge] = GraphColor.Gray;
                        if (open.MinimumUpdate(ncost, edge.Target))
                            this.OnTreeEdge(edge);
                    }
                    else if (hasColor)
                    {
                        Contract.Assume(edgeColor == GraphColor.Gray);
                        // edge already seen, remove it
                        operators.Remove(edge);
                    }
                }

#if DEBUG
                this.operatorMaxCount = Math.Max(this.operatorMaxCount, operators.Count);
#endif

                // (6) in a directed graph, generate each predecessor node n via an unused operator
                // and create dummy nodes for each with costs of infinity
                foreach (var edge in g.InEdges(n))
                {
                    GraphColor edgeColor;
                    if (operators.TryGetValue(edge, out edgeColor) &&
                        edgeColor == GraphColor.Gray)
                    {
                        // delete node n
                        operators.Remove(edge);
                    }
                }
            }
        }

#if DEBUG
        int operatorMaxCount = -1;
        public int OperatorMaxCount
        {
            get { return this.operatorMaxCount; }
        }
#endif

        #region ITreeBuilderAlgorithm<TVertex,TEdge> Members
        public event EdgeAction<TVertex, TEdge> TreeEdge;
        private void OnTreeEdge(TEdge edge)
        {
            var eh = this.TreeEdge;
            if (eh != null)
                eh(edge);
        }
        #endregion
    }
}
