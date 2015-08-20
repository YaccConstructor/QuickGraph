using System;

namespace QuickGraph.Petri
{
    public interface IPetriGraph<Token> : IMutableBidirectionalGraph<IPetriVertex, IArc<Token>>
    {}
}
