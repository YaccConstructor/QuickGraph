using System;

namespace QuickGraph.Predicates
{
    [Serializable]
    public sealed class AnyVertexPredicate<Vertex> : IVertexPredicate<Vertex>
    {
        public bool Test(Vertex vertex)
        {
            return true;
        }
    }
}
