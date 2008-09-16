using System;

namespace QuickGraph.Algorithms
{
    [Serializable]
    public enum ComputationState
    {
        NotRunning,
        Running,
        PendingAbortion,
        Finished,
        Aborted
    }
}
