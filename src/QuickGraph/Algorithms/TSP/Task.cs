using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickGraph.Algorithms.TSP
{
    class Task<TVertex, TEdge>
        where TEdge : EquatableEdge<TVertex>
    {
        Dictionary<EquatableEdge<TVertex>, double> weights;
        BidirectionalGraph<TVertex, TEdge> graph;
        public double minCost;
        public BidirectionalGraph<TVertex, TEdge> path;

        public Task(BidirectionalGraph<TVertex, TEdge> graph, Dictionary<EquatableEdge<TVertex>, double> weights, BidirectionalGraph<TVertex, TEdge> path, double cost)
        {
            this.graph = new BidirectionalGraph<TVertex, TEdge>(graph);
            this.weights = new Dictionary<EquatableEdge<TVertex>, double>(weights);
            this.path = path;
            minCost = cost;
            init();
        }

        private void init()
        {
            if (!check())
            {
                removeCycles();
                reduce();
            }
        }

        private bool check()
        {
            if (graph.EdgeCount == 1)
            {
                path.AddEdge(graph.Edges.First());
                if (path.IsDirectedAcyclicGraph())
                {
                    path.RemoveEdge(graph.Edges.First());
                    return false;
                }
                return true;
            }
            return false;
        }

        public void removeCycles()
        {
            List<TEdge> edgesToRemove = new List<TEdge>();
            foreach (var edge in graph.Edges)
            {
                path.AddEdge(edge);
                if (!path.IsDirectedAcyclicGraph())
                {
                    edgesToRemove.Add(edge);
                    weights.Remove(edge);
                }
                path.RemoveEdge(edge);
            }
            edgesToRemove.ForEach(edge => graph.RemoveEdge(edge));
        }

        public void reduce()
        {
            double sum = 0;

            if (graph.Edges.Count() == 0)
            {
                minCost = Double.PositiveInfinity;
                return;
            }

            foreach (var v in graph.Vertices)
            {
                IEnumerable<TEdge> outEdges;
                if (graph.TryGetOutEdges(v, out outEdges))
                {
                    if (outEdges.Count() > 0)
                    {
                        double min = outEdges.Min(edge => weights[edge]);
                        outEdges.ToList().ForEach(edge => weights[edge] -= min);
                        sum += min;
                    }
                }
            }

            foreach (var v in graph.Vertices)
            {
                IEnumerable<TEdge> inEdges;
                if (graph.TryGetInEdges(v, out inEdges))
                {
                    if (inEdges.Count() > 0)
                    {
                        double min = inEdges.Min(edge => weights[edge]);
                        inEdges.ToList().ForEach(Edge => weights[Edge] -= min);
                        sum += min;
                    }
                }
            }

            minCost += sum;
        }

        public TEdge chooseEdgeForSplit()
        {
            List<TEdge> zeros = new List<TEdge>();

            foreach (var v in graph.Vertices)
            {
                IEnumerable<TEdge> outEdges;
                if (graph.TryGetOutEdges(v, out outEdges))
                {
                    zeros.AddRange(outEdges.Where(edge => weights[edge] == 0));
                }
            }

            TEdge edgeForSplit = null;
            double max = Double.NegativeInfinity;
            foreach (var edge in zeros)
            {
                var v1 = edge.Source;
                var v2 = edge.Target;
                IEnumerable<TEdge> row;
                IEnumerable<TEdge> column;

                var maxCandidate = 0.0;


                if (graph.TryGetOutEdges(v1, out row) && graph.TryGetInEdges(v2, out column))
                {
                    maxCandidate = row.Where(e => !e.Target.Equals(v2)).DefaultIfEmpty(null).
                        Min(e => e == null ? Double.PositiveInfinity : weights[e])
                        + column.Where(e => !e.Source.Equals(v1)).DefaultIfEmpty(null).Min(e => e == null ? Double.PositiveInfinity : weights[e]);

                    if (maxCandidate > max)
                    {
                        max = maxCandidate;
                        edgeForSplit = edge;
                    }
                }
            }
            return edgeForSplit;
        }
        public bool canSplit()
        {
            return minCost < Double.PositiveInfinity;
        }
        public void split(out Task<TVertex, TEdge> taskTake, out Task<TVertex, TEdge> taskDrop)
        {
            TEdge edgeForSplit = this.chooseEdgeForSplit();
            var v1 = edgeForSplit.Source;
            var v2 = edgeForSplit.Target;

            var graph_take = this.graph.Clone();
            var weights_take = new Dictionary<EquatableEdge<TVertex>, double>(weights);
            var reverseEdge = new EquatableEdge<TVertex>(edgeForSplit.Target, edgeForSplit.Source);
            weights_take.Remove(reverseEdge);
            graph_take.RemoveEdgeIf(edge => edge.Equals(reverseEdge));
            graph_take.OutEdges(v1).ToList().ForEach(edge => weights_take.Remove(edge));
            graph_take.InEdges(v2).ToList().ForEach(edge => weights_take.Remove(edge));
            graph_take.ClearOutEdges(v1);
            graph_take.ClearInEdges(v2);
            var path_take = new BidirectionalGraph<TVertex, TEdge>(path);
            path_take.AddEdge(edgeForSplit);
            taskTake = new Task<TVertex, TEdge>(graph_take, weights_take, path_take, minCost);

            var graph_drop = this.graph.Clone();
            var weights_drop = new Dictionary<EquatableEdge<TVertex>, double>(weights);
            weights_drop.Remove(edgeForSplit);
            graph_drop.RemoveEdge(edgeForSplit);
            taskDrop = new Task<TVertex, TEdge>(graph_drop, weights_drop, new BidirectionalGraph<TVertex, TEdge>(path), minCost);
        }

        public bool isResultReady()
        {
            return path.EdgeCount == graph.VertexCount;
        }
    }
}
