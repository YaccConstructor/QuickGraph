using System;
using System.Collections.Generic;
using System.Text;

namespace QuickGraph
{
    public interface IIdentifiableVertexFactory<Vertex>
        where Vertex : IIdentifiable
    {
        Vertex CreateVertex(string id);
    }
}
