using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using QuickGraph.Contracts;

namespace QuickGraph
{
    [ContractClass(typeof(IUndirectedGraphContract<,>))]
    public interface IUndirectedGraph<TVertex,TEdge> 
        : IImplicitUndirectedGraph<TVertex, TEdge>
        , IEdgeListGraph<TVertex,TEdge>
        , IGraph<TVertex,TEdge>
        where TEdge : IEdge<TVertex>
    {
    }
}
