using System;

namespace QuickGraph.Predicates
{
    [Serializable]
    public sealed class AnyEdgePredicate<Vertex, Edge>
        where Edge : IEdge<Vertex>
    {
        public bool Test(Edge edge)
        {
            return true;
        }
    }
}
