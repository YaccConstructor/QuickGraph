using System;
using System.Collections.Generic;
using QuickGraph.Contracts;
using System.Diagnostics.Contracts;

namespace QuickGraph
{
//#if CONTRACTS_FULL
//    [ContractClass(typeof(IMutableVertexAndEdgeListGraphContract<,>))]
//#endif
    public interface IMutableVertexAndEdgeListGraph<TVertex,TEdge>
        : IMutableVertexListGraph<TVertex,TEdge>
        , IMutableEdgeListGraph<TVertex,TEdge>
        , IMutableVertexAndEdgeSet<TVertex,TEdge>
        , IVertexAndEdgeListGraph<TVertex,TEdge>
        where TEdge : IEdge<TVertex>
    {
    }
}
