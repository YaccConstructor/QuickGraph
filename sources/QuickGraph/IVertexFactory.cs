using System;

namespace QuickGraph
{
    public interface IVertexFactory<Vertex>
    {
        Vertex CreateVertex();
    }
}
