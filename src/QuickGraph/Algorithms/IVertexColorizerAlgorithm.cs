using System;
using System.Collections.Generic;

namespace QuickGraph.Algorithms
{
    public interface IVertexColorizerAlgorithm<TVertex,TEdge>
        where TEdge : IEdge<TVertex>
    {
        GraphColor GetVertexColor(TVertex v);
    }
}
