using System;
using System.Collections.Generic;
using System.Text;

namespace QuickGraph
{
    public interface IIdentifiableVertexFactory<TVertex>
        where TVertex : IIdentifiable
    {
        TVertex CreateVertex(string id);
    }
}
