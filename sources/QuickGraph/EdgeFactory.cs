using System;

namespace QuickGraph
{
    [Serializable]
    public sealed class EdgeFactory<Vertex> : IEdgeFactory<Vertex, Edge<Vertex>>
    {
        public Edge<Vertex> CreateEdge(Vertex source, Vertex target)
        {
            return new Edge<Vertex>(source, target);
        }
    }
}
