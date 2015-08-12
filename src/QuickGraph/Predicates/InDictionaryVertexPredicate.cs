using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace QuickGraph.Predicates
{
#if !SILVERLIGHT
    [Serializable]
#endif
    public sealed class InDictionaryVertexPredicate<TVertex, TValue>
    {
        private readonly IDictionary<TVertex, TValue> dictionary;

        public InDictionaryVertexPredicate(
            IDictionary<TVertex,TValue> dictionary)
        {
            Contract.Requires(dictionary != null);
            this.dictionary = dictionary;
        }

        [Pure]
        public bool Test(TVertex v)
        {
            Contract.Requires(v != null);

            return this.dictionary.ContainsKey(v);
        }
    }
}
