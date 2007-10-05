using System;

namespace QuickGraph.Predicates
{
    [Serializable]
    public sealed class AnyVertexPredicate<TVertex>
    {
        public bool Test(TVertex vertex)
        {
            return true;
        }
    }
}
