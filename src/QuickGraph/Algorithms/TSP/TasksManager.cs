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
        private BinaryHeap<TaskPriority, Task<TVertex, TEdge>> _tasksQueue;


        public TasksManager()
        {
            _tasksQueue = new BinaryHeap<TaskPriority, Task<TVertex, TEdge>>();
        }

        public void AddTask(Task<TVertex, TEdge> task)
        {
            if (task.MinCost < Double.PositiveInfinity)
            {
                _tasksQueue.Add(task.Priority, task);
            }
        }

        public Task<TVertex, TEdge> GetTask()
        {
            return _tasksQueue.RemoveMinimum().Value;
        }

        public bool HasTasks()
        {
            return _tasksQueue.Count > 0 ;
        }
    }

}
