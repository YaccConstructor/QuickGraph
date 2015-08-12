using System;
using System.Collections.Generic;

namespace QuickGraph
{
    public interface IHyperEdge<TVertex>
    {
        int EndPointCount { get;}
        IEnumerable<TVertex> EndPoints { get;}
    }
}
