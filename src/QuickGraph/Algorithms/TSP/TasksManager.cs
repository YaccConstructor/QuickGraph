using QuickGraph.Algorithms.TSP;
using QuickGraph.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuickGraph.Algorithms.TSP
{
    class TasksManager<TVertex, TEdge>
        where TEdge : EquatableEdge<TVertex>
    {
        BinaryHeap<double, Task<TVertex, TEdge>> tasksQueue;


        public TasksManager()
        {
            tasksQueue = new BinaryHeap<double, Task<TVertex, TEdge>>();
        }

        public void addTask(Task<TVertex, TEdge> task)
        {
            if (task.minCost < Double.PositiveInfinity)
            {
                tasksQueue.Add(task.minCost, task);
            }
        }

        public Task<TVertex, TEdge> getTask()
        {
            return tasksQueue.RemoveMinimum().Value;
        }

        public bool hasTasks()
        {
            return tasksQueue.Any();
        }
    }
}
