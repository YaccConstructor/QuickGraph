using System;
using System.Diagnostics.Contracts;

namespace QuickGraph.Predicates
{
    [Serializable]
    public sealed class AnyVertexPredicate<TVertex>
    {
        [Pure]
        public bool Test(TVertex vertex)
        {
            return true;
        }
    }
}
