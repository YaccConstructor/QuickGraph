using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics.Contracts;
using QuickGraph.Contracts;

namespace QuickGraph
{
#if CONTRACTS_FULL
    [ContractClass(typeof(IVertexSetContract<>))]
#endif
    public interface IVertexSet<TVertex>
    {
        bool IsVerticesEmpty { get;}
        int VertexCount { get;}
        IEnumerable<TVertex> Vertices { get;}
        bool ContainsVertex(TVertex vertex);
    }
}
