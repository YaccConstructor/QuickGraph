using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace QuickGraph.Collections
{
    [Serializable]
    public sealed class VertexEdgeDictionary<TVertex,TEdge>
        : Dictionary<TVertex, EdgeList<TVertex, TEdge>>
        , ICloneable
        , ISerializable
        where TEdge : IEdge<TVertex>
    {
        public VertexEdgeDictionary() { }
        public VertexEdgeDictionary(int capacity)
            : base(capacity)
        { }

        public VertexEdgeDictionary(
            SerializationInfo info, StreamingContext context) 
            : base(info, context) { }

        public VertexEdgeDictionary<TVertex, TEdge> Clone()
        {
            var clone = new VertexEdgeDictionary<TVertex, TEdge>(this.Count);
            foreach (var kv in this)
                clone.Add(kv.Key, kv.Value.Clone());
            return clone;
        }

        object ICloneable.Clone()
        {
            return this.Clone();
        }
    }
}
