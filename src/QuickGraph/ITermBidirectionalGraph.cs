using System.Collections.Generic;

namespace QuickGraph
{
    public interface ITermBidirectionalGraph<TVertex, TEdge>
        : IBidirectionalGraph<TVertex, TEdge>
        where TEdge : ITermEdge<TVertex>
    {
        int OutTerminalCount(TVertex v);

        bool IsOutEdgesEmptyAt(TVertex v, int terminal);

        int OutDegreeAt(TVertex v, int terminal);

        IEnumerable<TEdge> OutEdgesAt(TVertex v, int terminal);

        bool TryGetOutEdgesAt(TVertex v, int terminal, out IEnumerable<TEdge> edges);

        int InTerminalCount(TVertex v);

        bool IsInEdgesEmptyAt(TVertex v, int terminal);

        int InDegreeAt(TVertex v, int terminal);

        IEnumerable<TEdge> InEdgesAt(TVertex v, int terminal);

        bool TryGetInEdgesAt(TVertex v, int terminal, out IEnumerable<TEdge> edges);
    }

    public interface IMutableTermBidirectionalGraph<TVertex, TEdge>
        : ITermBidirectionalGraph<TVertex, TEdge>
        , IMutableBidirectionalGraph<TVertex, TEdge>
        where TEdge : ITermEdge<TVertex>
    {
    }
}