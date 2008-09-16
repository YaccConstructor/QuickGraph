using System;
using QuickGraph.Algorithms.Services;

namespace QuickGraph.Algorithms
{
    public interface IAlgorithm<TGraph> :
        IComputation
    {
        TGraph VisitedGraph { get;}
    }
}
