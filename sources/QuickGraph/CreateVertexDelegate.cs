using System;
namespace QuickGraph
{
    [Serializable]
    public delegate Vertex CreateVertexDelegate<Vertex, Edge>(
        IVertexListGraph<Vertex,Edge> g) 
    where Edge : IEdge<Vertex>;
}
