using System;
using System.Collections.Generic;

namespace QuickGraph.Predicates
{
    [Serializable]
    public sealed class InDictionaryVertexPredicate<Vertex, Value>
    {
        private IDictionary<Vertex, Value> dictionary;

        public InDictionaryVertexPredicate(
            IDictionary<Vertex,Value> dictionary)
        {
            this.dictionary = dictionary;
        }

        public bool Test(Vertex v)
        {
            if (v == null)
                throw new ArgumentNullException("v");
            return this.dictionary.ContainsKey(v);
        }
    }
}
