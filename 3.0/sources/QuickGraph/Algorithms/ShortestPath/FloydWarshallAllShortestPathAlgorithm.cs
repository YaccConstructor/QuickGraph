using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuickGraph.Algorithms.Services;
using System.Diagnostics.Contracts;
using QuickGraph.Collections;

namespace QuickGraph.Algorithms.ShortestPath
{
    /// <summary>
    /// Floyd-Warshall all shortest path algorith,
    /// </summary>
    /// <typeparam name="TVertex"></typeparam>
    /// <typeparam name="TEdge"></typeparam>
    public class FloydWarshallAllShortestPathAlgorithm<TVertex, TEdge> 
        : AlgorithmBase<IVertexAndEdgeListGraph<TVertex, TEdge>>
        where TEdge : IEdge<TVertex>
    {
        private readonly Func<TEdge, double> weights;
        private readonly IDistanceRelaxer distanceRelaxer;
        private readonly Dictionary<VertexPair<TVertex>, TVertex> predecessors;

        public FloydWarshallAllShortestPathAlgorithm(
            IAlgorithmComponent host,
            IVertexAndEdgeListGraph<TVertex, TEdge> visitedGraph,
            Func<TEdge, double> weights,
            IDistanceRelaxer distanceRelaxer
            )
            : base(host, visitedGraph)
        {
            CodeContract.Requires(weights != null);
            CodeContract.Requires(distanceRelaxer != null);

            this.weights = weights;
            this.distanceRelaxer = distanceRelaxer;
            this.predecessors = new Dictionary<VertexPair<TVertex>, TVertex>();
        }

        public FloydWarshallAllShortestPathAlgorithm(
            IVertexAndEdgeListGraph<TVertex, TEdge> visitedGraph,
            Func<TEdge, double> weights,
            IDistanceRelaxer distanceRelaxer)
            : base(visitedGraph)
        {
            CodeContract.Requires(weights != null);
            CodeContract.Requires(distanceRelaxer != null);

            this.weights =weights;
            this.distanceRelaxer = distanceRelaxer;
            this.predecessors = new Dictionary<VertexPair<TVertex>, TVertex>();
        }

        public FloydWarshallAllShortestPathAlgorithm(
            IVertexAndEdgeListGraph<TVertex, TEdge> visitedGraph,
            Func<TEdge, double> weights)
            : this(visitedGraph, weights, ShortestDistanceRelaxer.Instance)
        {
        }

        public bool TryGetShortestPath(
            TVertex source,
            TVertex target,
            out IEnumerable<TEdge> path)
        {
            CodeContract.Requires(source != null);
            CodeContract.Requires(target != null);

            if (source.Equals(target))
            {
                path = new TEdge[0];
                return true;
            }

            var edges = new EdgeList<TVertex, TEdge>();
            var todo = new Stack<VertexPair<TVertex>>();
            todo.Push(new VertexPair<TVertex>(source, target));
            while (todo.Count > 0)
            {
                var current = todo.Pop();
                CodeContract.Assert(!current.Source.Equals(current.Target));
                TVertex intermediate;
                if (this.predecessors.TryGetValue(current, out intermediate))
                {
                    todo.Push(new VertexPair<TVertex>(intermediate, target));
                    todo.Push(new VertexPair<TVertex>(source, intermediate));
                }
                else
                {
                    TEdge edge;
                    if (!this.VisitedGraph.TryGetEdge(current.Source, current.Target, out edge))
                    {
                        // no path found
                        path = null;
                        return false;
                    }
                    edges.Add(edge);
                }
            }

            CodeContract.Assert(todo.Count == 0);
            CodeContract.Assert(edges.Count > 0);
            path = edges.ToArray();
            return true;
        }

        protected override void InternalCompute()
        {
            var cancelManager = this.Services.CancelManager;
            // matrix i,j -> path
            this.predecessors.Clear();
            var vertices = this.VisitedGraph.Vertices;
            var edges = this.VisitedGraph.Edges;
            var costs = new Dictionary<VertexPair<TVertex>, double>();

            // prepare the matrix with initial costs
            // walk each edge and add entry in cost dictionary
            foreach (var edge in edges)
            {
                var ij = VertexPair<TVertex>.FromEdge<TEdge>(edge);
                var cost = this.weights(edge);
                double value;
                if (!costs.TryGetValue(ij, out value))
                    costs[ij] = value = this.weights(edge);
                else if (cost < value)
                    costs[ij] = cost;
            }
            if (cancelManager.IsCancelling) return;

            // walk each vertices and make sure cost is 0
            foreach (var v in vertices)
                costs[new VertexPair<TVertex>(v, v)] = 0;
            if (cancelManager.IsCancelling) return;

            // iterate k, i, j
            foreach (var vk in vertices)
            {
                if (cancelManager.IsCancelling) return;
                foreach (var vi in vertices)
                {
                    var ik = new VertexPair<TVertex>(vi, vk);
                    foreach (var vj in vertices)
                    {
                        var ij = new VertexPair<TVertex>(vi, vj);
                        var kj = new VertexPair<TVertex>(vk, vj);

                        double pathik;
                        double pathkj;
                        if (costs.TryGetValue(ik, out pathik) &&
                            costs.TryGetValue(kj, out pathkj))
                        {
                            double combined = this.distanceRelaxer.Combine(pathik, pathkj);
                            double pathij;
                            if (costs.TryGetValue(ij, out pathij))
                            {
                                if (this.distanceRelaxer.Compare(combined, pathij))
                                {
                                    costs[ij] = pathij = combined;
                                    this.predecessors[ij] = vk;
                                }
                            }
                            else
                            {
                                costs[ij] = combined;
                                this.predecessors[ij] = vk;
                            }
                        }
                    }
                }
            }

            // check negative cycles
            foreach (var vi in vertices)
            {
                var ij = new VertexPair<TVertex>(vi, vi);
                double value;
                if (costs.TryGetValue(ij, out value) &&
                    value < 0)
                    throw new NegativeCycleGraphException();
            }
        }
    }
}
