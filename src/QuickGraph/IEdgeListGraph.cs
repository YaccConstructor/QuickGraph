using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using QuickGraph.Contracts;

namespace QuickGraph
{
    /// <summary>
    /// A graph whose edges can be enumerated
    /// </summary>
    /// <typeparam name="TVertex">type of the vertices</typeparam>
    /// <typeparam name="TEdge">type of the edges</typeparam>
    [ContractClass(typeof(IEdgeListGraphContract<,>))]
    public interface IEdgeListGraph<TVertex, TEdge> 
        : IGraph<TVertex, TEdge>
        , IEdgeSet<TVertex, TEdge>
        , IVertexSet<TVertex>
        where TEdge : IEdge<TVertex>
    {}
}
