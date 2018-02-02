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
        private Dictionary<EquatableEdge<TVertex>, double> _weight;
        private BidirectionalGraph<TVertex, TEdge> _graph;

        public double MinCost;
        public BidirectionalGraph<TVertex, TEdge> Path;
        public String TaskName;
        public TaskPriority Priority;

        public Task(BidirectionalGraph<TVertex, TEdge> graph, Dictionary<EquatableEdge<TVertex>, double> weights, BidirectionalGraph<TVertex, TEdge> path, double cost) : this(graph, weights, path, cost, "Init")
        { }

        public Task(BidirectionalGraph<TVertex, TEdge> graph, Dictionary<EquatableEdge<TVertex>, double> weights, BidirectionalGraph<TVertex, TEdge> path, double cost, String taskName)
        {
            TaskName = taskName;
            _graph = new BidirectionalGraph<TVertex, TEdge>(graph);
            _weight = new Dictionary<EquatableEdge<TVertex>, double>(weights);
            Path = path;
            MinCost = cost;
            init();
            Priority = new TaskPriority(MinCost, path.EdgeCount);
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
            if (_graph.EdgeCount == 1)
            {
                Path.AddEdge(_graph.Edges.First());
                if (Path.IsDirectedAcyclicGraph())
                {
                    Path.RemoveEdge(_graph.Edges.First());
                    return false;
                }
                return true;
            }
            return false;
        }

        private void removeCycles()
        {
            List<TEdge> edgesToRemove = new List<TEdge>();
            foreach (var edge in _graph.Edges)
            {
                Path.AddEdge(edge);
                if (!Path.IsDirectedAcyclicGraph())
                {
                    edgesToRemove.Add(edge);
                    _weight.Remove(edge);
                }
                Path.RemoveEdge(edge);
            }
            edgesToRemove.ForEach(edge => _graph.RemoveEdge(edge));
        }

        private void reduce()
        {
            double sum = 0;

            if (_graph.IsEdgesEmpty)
            {
                MinCost = Double.PositiveInfinity;
                return;
            }

            foreach (var v in _graph.Vertices)
            {
                IEnumerable<TEdge> outEdges;
                if (_graph.TryGetOutEdges(v, out outEdges))
                {
                    if (outEdges.Any())
                    {
                        double min = outEdges.Min(edge => _weight[edge]);

                        foreach (var edge in outEdges)
                        {
                            _weight[edge] -= min;
                        }

                        sum += min;
                    }
                }
            }

            foreach (var v in _graph.Vertices)
            {
                IEnumerable<TEdge> inEdges;
                if (_graph.TryGetInEdges(v, out inEdges))
                {
                    if (inEdges.Any())
                    {
                        double min = inEdges.Min(edge => _weight[edge]);

                        foreach (var edge in inEdges)
                        {
                            _weight[edge] -= min;
                        }

                        sum += min;
                    }
                }
            }

            MinCost += sum;
        }

        private TEdge chooseEdgeForSplit()
        {
            List<TEdge> zeros = new List<TEdge>();

            foreach (var v in _graph.Vertices)
            {
                IEnumerable<TEdge> outEdges;
                if (_graph.TryGetOutEdges(v, out outEdges))
                {
                    zeros.AddRange(outEdges.Where(edge => _weight[edge] == 0));
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


                if (_graph.TryGetOutEdges(v1, out row) && _graph.TryGetInEdges(v2, out column))
                {
                    maxCandidate = row.Where(e => !e.Target.Equals(v2)).DefaultIfEmpty(null).
                        Min(e => e == null ? Double.PositiveInfinity : _weight[e])
                        + column.Where(e => !e.Source.Equals(v1)).DefaultIfEmpty(null).Min(e => e == null ? Double.PositiveInfinity : _weight[e]);

                    if (maxCandidate > max)
                    {
                        max = maxCandidate;
                        edgeForSplit = edge;
                    }
                }
            }
            return edgeForSplit;
        }
        private bool canSplit()
        {
            return MinCost < Double.PositiveInfinity;
        }
        public bool Split(out Task<TVertex, TEdge> taskTake, out Task<TVertex, TEdge> taskDrop)
        {
            if (!canSplit())
            {
                taskTake = null;
                taskDrop = null;
                return false;
            }

            TEdge edgeForSplit = this.chooseEdgeForSplit();
            var v1 = edgeForSplit.Source;
            var v2 = edgeForSplit.Target;

            var graphTake = this._graph.Clone();
            var weightsTake = new Dictionary<EquatableEdge<TVertex>, double>(_weight);
            var reverseEdge = new EquatableEdge<TVertex>(edgeForSplit.Target, edgeForSplit.Source);
            weightsTake.Remove(reverseEdge);
            graphTake.RemoveEdgeIf(edge => edge.Equals(reverseEdge));

            foreach (var outEdge in graphTake.OutEdges(v1))
            {
                weightsTake.Remove(outEdge);
            }

            foreach (var inEdge in graphTake.InEdges(v2))
            {
                weightsTake.Remove(inEdge);
            }

            graphTake.ClearOutEdges(v1);
            graphTake.ClearInEdges(v2);
            var pathTake = new BidirectionalGraph<TVertex, TEdge>(Path);
            pathTake.AddEdge(edgeForSplit);
            taskTake = new Task<TVertex, TEdge>(graphTake, weightsTake, pathTake, MinCost);
            taskTake.TaskName = "Take" + edgeForSplit.ToString();

            var graphDrop = this._graph.Clone();
            var weightsDrop = new Dictionary<EquatableEdge<TVertex>, double>(_weight);
            weightsDrop.Remove(edgeForSplit);
            graphDrop.RemoveEdge(edgeForSplit);
            taskDrop = new Task<TVertex, TEdge>(graphDrop, weightsDrop, new BidirectionalGraph<TVertex, TEdge>(Path), MinCost);
            taskDrop.TaskName = "Drop" + edgeForSplit.ToString();

            return true;
        }

        public bool IsResultReady()
        {
            return Path.EdgeCount == _graph.VertexCount;
        }

    }

    public class TaskPriority : IComparable<TaskPriority>
    {
        double _cost;
        int _pathSize;

        public TaskPriority(double cost, int pathSize)
        {
            _cost = cost;
    
            _pathSize = pathSize;
        }

        public int CompareTo(TaskPriority other)
        {
            var costCompare = _cost.CompareTo(other._cost);
            if (costCompare == 0)
            {
                return -_pathSize.CompareTo(other._pathSize);
            }
            return costCompare;
        }
    }
}
