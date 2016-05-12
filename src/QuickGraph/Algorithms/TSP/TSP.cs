using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using QuickGraph.Algorithms.Services;
using QuickGraph.Algorithms.ShortestPath;
using QuickGraph.Collections;

namespace QuickGraph.Algorithms.TSP
{
    public class TSP<TVertex, TEdge, TGraph> : ShortestPathAlgorithmBase<TVertex, TEdge
        , TGraph>
        , ITreeBuilderAlgorithm<TVertex, TEdge>
        where TGraph : BidirectionalGraph<TVertex, TEdge>
        where TEdge : EquatableEdge<TVertex>
    {
        private TasksManager<TVertex, TEdge> taskManager = new TasksManager<TVertex, TEdge>();

        private BidirectionalGraph<TVertex, TEdge> resultPath;
        private double bestCost;

        public TSP(TGraph visitedGraph, Dictionary<EquatableEdge<TVertex>, double> weights)
            :base(null, visitedGraph, edge => weights[edge], DistanceRelaxers.ShortestDistance)
        {
            BidirectionalGraph<TVertex, TEdge> path = new BidirectionalGraph<TVertex, TEdge>();
            path.AddVertexRange(visitedGraph.Vertices);
            taskManager.addTask(new Task<TVertex, TEdge>(visitedGraph, weights, path, 0));
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
            while(taskManager.hasTasks())
            {
                if (State == ComputationState.Aborted)
                {
                    return;
                }
                Task<TVertex, TEdge> task = taskManager.getTask();
                if (task.isResultReady())
                {
                    bestCost = task.minCost;
                    resultPath = task.path;
                    return;
                }
                else
                {
                    if (task.canSplit())
                    {
                        Task<TVertex, TEdge> task1;
                        Task<TVertex, TEdge> task2;
                        task.split(out task1, out task2);
                        taskManager.addTask(task1);
                        taskManager.addTask(task2);
                    }
                }
            }

            return;
            
        }

    }
}
