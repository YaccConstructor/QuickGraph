using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuickGraph.Algorithms.Services;
using System.Diagnostics.Contracts;
using QuickGraph.Collections;

namespace QuickGraph.Algorithms.ShortestPath
{
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

        public bool TryGetShortestPath(
            TVertex source,
            TVertex target,
            out IEnumerable<TEdge> path)
        {
            CodeContract.Requires(source != null);
            CodeContract.Requires(target != null);

            var edges = new EdgeList<TVertex, TEdge>();
            var todo = new Stack<VertexPair<TVertex>>();
            todo.Push(new VertexPair<TVertex>(source, target));
            while (todo.Count > 0)
            {
                var current = todo.Pop();
                TVertex intermediate;
                if (this.predecessors.TryGetValue(current, out intermediate))
                {
                    todo.Push(new VertexPair<TVertex>(source, intermediate));
                    todo.Push(new VertexPair<TVertex>(intermediate, target));
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
            var paths = new Dictionary<VertexPair<TVertex>, double>();

            // build matrix of edges
            foreach (var vi in vertices)
            {
                foreach (var vj in vertices)
                {
                    if (cancelManager.IsCancelling) return;

                    // is there an edge from i-> j?
                    TEdge edge;
                    if (this.VisitedGraph.TryGetEdge(vi, vj, out edge))
                    {
                        var ij = new VertexPair<TVertex>(vi, vj);
                        paths[ij] = this.weights(edge);
                    }
                }
            }

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
                        if (paths.TryGetValue(ik, out pathik) &&
                            paths.TryGetValue(kj, out pathkj))
                        {
                            double combined = this.distanceRelaxer.Combine(pathik, pathkj);
                            double pathij;
                            if (paths.TryGetValue(ij, out pathij))
                            {
                                if (this.distanceRelaxer.Compare(combined, pathij))
                                {
                                    paths[ij] = pathij = combined;
                                    this.predecessors[ij] = vk;
                                }
                            }
                            else
                            {
                                paths[ij] = combined;
                                this.predecessors[ij] = vk;
                            }
                        }
                    }
                }
            }
        }
    }
}
