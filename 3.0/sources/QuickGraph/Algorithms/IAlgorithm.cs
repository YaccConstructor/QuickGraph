using System;
using QuickGraph.Algorithms.Services;
using QuickGraph.Algorithms.Contracts;
using System.Diagnostics.Contracts;

namespace QuickGraph.Algorithms
{
#if CONTRACTS_FULL
   [ContractClass(typeof(IAlgorithmContract<>))]
#endif
    public interface IAlgorithm<TGraph> :
        IComputation
    {
        TGraph VisitedGraph { get;}
    }
}
