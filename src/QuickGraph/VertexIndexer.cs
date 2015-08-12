using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace QuickGraph
{
    [Pure]
    public delegate int VertexIndexer<TVertex>(TVertex v);
}
