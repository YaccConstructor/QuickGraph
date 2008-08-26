using System;

namespace QuickGraph.Algorithms
{
    public interface IAlgorithm<TGraph> :
        IComputation
    {
        TGraph VisitedGraph { get;}
    }
}
