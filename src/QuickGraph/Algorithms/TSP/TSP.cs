using System.Collections.Generic;
using QuickGraph.Algorithms.ShortestPath;
using System;

namespace QuickGraph.Algorithms.TSP
{
    public class TSP<TVertex, TEdge, TGraph> : ShortestPathAlgorithmBase<TVertex, TEdge, TGraph>
            , ITreeBuilderAlgorithm<TVertex, TEdge>
        where TGraph : BidirectionalGraph<TVertex, TEdge>
        where TEdge : EquatableEdge<TVertex>
    {
        private TasksManager<TVertex, TEdge> taskManager = new TasksManager<TVertex, TEdge>();

        public BidirectionalGraph<TVertex, TEdge> ResultPath;
        public double BestCost = Double.PositiveInfinity;

        public TSP(TGraph visitedGraph, Func<TEdge, double> weights)
            : base(null, visitedGraph, weights, DistanceRelaxers.ShortestDistance)
        {
            BidirectionalGraph<TVertex, TEdge> path = new BidirectionalGraph<TVertex, TEdge>();
            path.AddVertexRange(visitedGraph.Vertices);
            taskManager.AddTask(new Task<TVertex, TEdge>(visitedGraph, buildWeightsDict(visitedGraph, weights), path, 0));
        }

        private Dictionary<EquatableEdge<TVertex>, double> buildWeightsDict(TGraph visitedGraph, Func<TEdge, double> weights)
        {
            var dict = new Dictionary<EquatableEdge<TVertex>, double>();
            foreach (var edge in visitedGraph.Edges) {
                dict[edge] = weights(edge);
            }
            return dict;
        }
        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void Clean()
        {
            base.Clean();
        }

        protected override void InternalCompute()
        {
            while(taskManager.HasTasks())
            {
                if (State == ComputationState.Aborted)
                {
                    return;
                }
                Task<TVertex, TEdge> task = taskManager.GetTask();
                if (task.IsResultReady())
                {
                    BestCost = task.MinCost;
                    ResultPath = task.Path;
                    return;
                }
                else
                {
                    Task<TVertex, TEdge> task1;
                    Task<TVertex, TEdge> task2;
                    if (task.Split(out task1, out task2))
                    {
                        taskManager.AddTask(task1);
                        taskManager.AddTask(task2);
                    }
                }
            }
            
        }

    }
}
