using System;
using System.Collections.Generic;

namespace QuickGraph.Predicates
{
    [Serializable]
    public sealed class InDictionaryVertexPredicate<TVertex, TValue>
    {
        private IDictionary<TVertex, TValue> dictionary;

        public InDictionaryVertexPredicate(
            IDictionary<TVertex,TValue> dictionary)
        {
            this.dictionary = dictionary;
        }

        public bool Test(TVertex v)
        {
            if (v == null)
                throw new ArgumentNullException("v");
            return this.dictionary.ContainsKey(v);
        }
    }
}
