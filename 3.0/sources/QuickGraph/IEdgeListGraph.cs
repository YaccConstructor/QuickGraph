using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace QuickGraph
{
    [ContractClass(typeof(IEdgeListGraph<,>))]
    public interface IEdgeListGraph<TVertex, TEdge> : 
        IGraph<TVertex, TEdge>,
        IEdgeSet<TVertex, TEdge>,
        IVertexSet<TVertex>
        where TEdge : IEdge<TVertex>
    {}
}
