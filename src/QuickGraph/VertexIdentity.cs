using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace QuickGraph
{
    [Pure]
    public delegate string VertexIdentity<TVertex>(TVertex v);
}
