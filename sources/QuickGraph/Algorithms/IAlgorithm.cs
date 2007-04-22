using System;

namespace QuickGraph.Algorithms
{
    public interface IAlgorithm<Graph>
    {
        Graph VisitedGraph { get;}

        object SyncRoot { get;}
        ComputationState State { get;}

        void Compute();
        void Abort();

        event EventHandler StateChanged;
        event EventHandler Started;
        event EventHandler Finished;
        event EventHandler Aborted;
    }
}
