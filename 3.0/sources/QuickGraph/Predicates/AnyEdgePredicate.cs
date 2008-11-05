using System;
using System.Diagnostics.Contracts;

namespace QuickGraph.Predicates
{
    [Serializable]
    public sealed class AnyEdgePredicate<TVertex, TEdge>
        where TEdge : IEdge<TVertex>
    {
        [Pure]
        public bool Test(TEdge edge)
        {
            return true;
        }
    }
}
