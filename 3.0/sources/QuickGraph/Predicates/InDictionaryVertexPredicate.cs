using System;
using System.Collections.Generic;

namespace QuickGraph.Predicates
{
    [Serializable]
    public sealed class InDictionaryVertexPredicate<TVertex, TValue>
    {
        private readonly IDictionary<TVertex, TValue> dictionary;

        public InDictionaryVertexPredicate(
            IDictionary<TVertex,TValue> dictionary)
        {
            GraphContracts.AssumeNotNull(dictionary, "dictionary");
            this.dictionary = dictionary;
        }

        public bool Test(TVertex v)
        {
            return this.dictionary.ContainsKey(v);
        }
    }
}
