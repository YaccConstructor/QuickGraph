using System;
using System.Collections.Generic;

namespace QuickGraph.Algorithms
{
    public interface IVertexColorizerAlgorithm<TVertex,TEdge>
        where TEdge : IEdge<TVertex>
    {
        IDictionary<TVertex, GraphColor> VertexColors { get;}
    }
}
