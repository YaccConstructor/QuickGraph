using System;
using System.Diagnostics.Contracts;
using QuickGraph.Contracts;

namespace QuickGraph
{
    /// <summary>
    /// A mutable graph instance
    /// </summary>
    /// <typeparam name="TVertex"></typeparam>
    /// <typeparam name="TEdge"></typeparam>
#if CONTRACTS_FULL
    [ContractClass(typeof(IMutableGraphContract<,>))]
#endif
    public interface IMutableGraph<TVertex,TEdge> 
        : IGraph<TVertex,TEdge>
        where TEdge : IEdge<TVertex>
    {
        void Clear();
    }
}
