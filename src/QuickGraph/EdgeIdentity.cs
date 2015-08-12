using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuickGraph
{
    public delegate string EdgeIdentity<TVertex, TEdge>(TEdge edge)
        where TEdge : IEdge<TVertex>;
}
