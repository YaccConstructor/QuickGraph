using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using QuickGraph.Contracts;

namespace QuickGraph
{
    [ContractClass(typeof(IEdgeListGraphContract<,>))]
    public interface IEdgeListGraph<TVertex, TEdge> : 
        IGraph<TVertex, TEdge>,
        IEdgeSet<TVertex, TEdge>,
        IVertexSet<TVertex>
        where TEdge : IEdge<TVertex>
    {}
}
