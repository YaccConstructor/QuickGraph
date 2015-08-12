using System;
using System.Diagnostics.Contracts;

namespace QuickGraph
{
    [Pure]
    public delegate bool VertexPredicate<TVertex>(TVertex v);
}
