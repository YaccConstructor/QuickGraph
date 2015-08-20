using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using QuickGraph.Contracts;
using QuickGraph.Collections;

namespace QuickGraph
{
#if !SILVERLIGHT
    [Serializable]
#endif
    [DebuggerDisplay("VertexCount = {VertexCount}, EdgeCount = {EdgeCount}")]
    public class TermBidirectionalGraph<TVertex, TEdge>
        : BidirectionalGraph<TVertex, TEdge>
        , ITermBidirectionalGraph<TVertex, TEdge>
        , IMutableTermBidirectionalGraph<TVertex, TEdge>
#if !SILVERLIGHT
, ICloneable
#endif
 where TEdge : ITermEdge<TVertex>
    {
        public TermBidirectionalGraph()
            : base(true, -1)
        { }

        public int OutTerminalCount(TVertex v)
        {
            throw new NotImplementedException();
        }

        public bool IsOutEdgesEmptyAt(TVertex v, int terminal)
        {
            throw new NotImplementedException();
        }

        public int OutDegreeAt(TVertex v, int terminal)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEdge> OutEdgesAt(TVertex v, int terminal)
        {
            throw new NotImplementedException();
        }

        public bool TryGetOutEdgesAt(TVertex v, int terminal, out IEnumerable<TEdge> edges)
        {
            throw new NotImplementedException();
        }

        public int InTerminalCount(TVertex v)
        {
            throw new NotImplementedException();
        }

        public bool IsInEdgesEmptyAt(TVertex v, int terminal)
        {
            throw new NotImplementedException();
        }

        public int InDegreeAt(TVertex v, int terminal)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEdge> InEdgesAt(TVertex v, int terminal)
        {
            throw new NotImplementedException();
        }

        public bool TryGetInEdgesAt(TVertex v, int terminal, out IEnumerable<TEdge> edges)
        {
            throw new NotImplementedException();
        }
    }
}
