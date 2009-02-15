using System;
using QuickGraph.Algorithms.Services;
using System.Diagnostics.Contracts;

namespace QuickGraph.Algorithms
{
#if CONTRACTS_FULL
   [ContractClass(typeof(Contracts.IAlgorithmContract<>))]
#endif
    public interface IAlgorithm<TGraph> :
        IComputation
    {
        TGraph VisitedGraph { get;}
    }
}
