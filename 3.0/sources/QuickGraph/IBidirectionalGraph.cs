using System;
using System.Collections.Generic;

namespace QuickGraph
{
    public interface IBidirectionalGraph<TVertex,TEdge> : 
        IVertexAndEdgeListGraph<TVertex,TEdge>
        where TEdge : IEdge<TVertex>
    {
        bool IsInEdgesEmpty(TVertex v);
        int InDegree(TVertex v);

        IEnumerable<TEdge> InEdges(TVertex v);
        TEdge InEdge(TVertex v, int index);

        int Degree(TVertex v);
    }
}
