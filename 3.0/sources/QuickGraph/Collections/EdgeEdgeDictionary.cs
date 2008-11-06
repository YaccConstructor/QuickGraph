using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace QuickGraph.Collections
{
    [Serializable]
    public class EdgeEdgeDictionary<TVertex, TEdge>
        : Dictionary<TEdge, TEdge>
        , ICloneable
        , ISerializable
        where TEdge : IEdge<TVertex>
    {
        public EdgeEdgeDictionary()
        { }

        public EdgeEdgeDictionary(int capacity)
            : base(capacity)
        { }

        protected EdgeEdgeDictionary(
            SerializationInfo info, StreamingContext context) 
            : base(info, context) { }

        public EdgeEdgeDictionary<TVertex, TEdge> Clone()
        {
            var clone = new EdgeEdgeDictionary<TVertex, TEdge>(this.Count);
            foreach (var kv in this)
                clone.Add(kv.Key, kv.Value);
            return clone;
        }

        object ICloneable.Clone()
        {
            return this.Clone();
        }
    }
}
