using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace QuickGraph.Collections
{
#if !SILVERLIGHT
    [Serializable]
#endif
    public sealed class VertexEdgeDictionary<TVertex,TEdge>
        : Dictionary<TVertex, IEdgeList<TVertex, TEdge>>
        , IVertexEdgeDictionary<TVertex, TEdge>
#if !SILVERLIGHT
        , ICloneable
        , ISerializable
#endif
        where TEdge : IEdge<TVertex>
    {
        public VertexEdgeDictionary() { }
        public VertexEdgeDictionary(int capacity)
            : base(capacity)
        { }

#if !SILVERLIGHT
        public VertexEdgeDictionary(
            SerializationInfo info, StreamingContext context) 
            : base(info, context) { }
#endif

        public VertexEdgeDictionary<TVertex, TEdge> Clone()
        {
            var clone = new VertexEdgeDictionary<TVertex, TEdge>(this.Count);
            foreach (var kv in this)
                clone.Add(kv.Key, kv.Value.Clone());
            return clone;
        }

        IVertexEdgeDictionary<TVertex, TEdge> IVertexEdgeDictionary<TVertex,TEdge>.Clone()
        {
            return this.Clone();
        }

#if !SILVERLIGHT
        object ICloneable.Clone()
        {
            return this.Clone();
        }
#endif
    }
}
