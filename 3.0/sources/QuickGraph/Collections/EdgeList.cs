using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuickGraph.Collections
{
    [Serializable]
    public sealed class EdgeList<TVertex, TEdge>
        : List<TEdge>
        , ICloneable
        where TEdge : IEdge<TVertex>
    {
        public EdgeList() 
        { }

        public EdgeList(int capacity)
            : base(capacity)
        { }

        private EdgeList(EdgeList<TVertex, TEdge> list)
            : base(list)
        {}

        public EdgeList<TVertex, TEdge> Clone()
        {
            return new EdgeList<TVertex, TEdge>(this);
        }

        object ICloneable.Clone()
        {
            return this.Clone();
        }
    }
}
