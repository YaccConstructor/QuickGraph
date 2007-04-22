using System;
using System.Collections.Generic;
using System.Text;

namespace QuickGraph
{
    public interface IView<Vertex,Edge> 
        where Edge : IEdge<Vertex>
    {
        IHierarchy<Vertex, Edge> Hierarchy { get;}
        void Expand(Vertex vertex);
        void Collapse(Vertex vertex);
    }
}
