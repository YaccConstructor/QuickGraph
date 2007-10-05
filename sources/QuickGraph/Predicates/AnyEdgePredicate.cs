using System;

namespace QuickGraph.Predicates
{
    [Serializable]
    public sealed class AnyEdgePredicate<TVertex, TEdge>
        where TEdge : IEdge<TVertex>
    {
        public bool Test(TEdge edge)
        {
            return true;
        }
    }
}
