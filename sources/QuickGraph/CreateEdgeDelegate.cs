using System;
namespace QuickGraph
{
    [Serializable]
    public delegate Edge CreateEdgeDelegate<Vertex, Edge>(
        IVertexListGraph<Vertex, Edge> g,
        Vertex source,
        Vertex target)
        where Edge : IEdge<Vertex>;
}
