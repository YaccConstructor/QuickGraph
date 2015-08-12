using System;
using System.Collections.Generic;
using System.Text;
#if !SILVERLIGHT
using System.Runtime.Serialization;
#endif

namespace QuickGraph.Collections
{
#if !SILVERLIGHT
    [Serializable]
#endif
    public class EdgeEdgeDictionary<TVertex, TEdge>
        : Dictionary<TEdge, TEdge>
#if !SILVERLIGHT
        , ICloneable
        , ISerializable
#endif
        where TEdge : IEdge<TVertex>
    {
        public EdgeEdgeDictionary()
        { }

        public EdgeEdgeDictionary(int capacity)
            : base(capacity)
        { }

#if !SILVERLIGHT
        protected EdgeEdgeDictionary(
            SerializationInfo info, StreamingContext context) 
            : base(info, context) { }
#endif

        public EdgeEdgeDictionary<TVertex, TEdge> Clone()
        {
            var clone = new EdgeEdgeDictionary<TVertex, TEdge>(this.Count);
            foreach (var kv in this)
                clone.Add(kv.Key, kv.Value);
            return clone;
        }

#if !SILVERLIGHT
        object ICloneable.Clone()
        {
            return this.Clone();
        }
#endif
    }
}
