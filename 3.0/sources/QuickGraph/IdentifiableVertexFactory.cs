using System;
using System.Collections.Generic;
using System.Text;

namespace QuickGraph
{
    public delegate TVertex IdentifiableVertexFactory<TVertex>(string id)
        where TVertex : IIdentifiable;
}
