using System;
using System.Collections.Generic;

namespace QuickGraph
{
    public interface IHyperEdge<Vertex>
    {
        int EndPointCount { get;}
        IEnumerable<Vertex> EndPoints { get;}
    }
}
