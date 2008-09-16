using System;
using System.Collections.Generic;

namespace QuickGraph.Operations
{
    internal sealed class CartesianProductDomainTupleEnumerable : IEnumerable<ITuple>
    {
        private IList<IDomain> domains;
        public CartesianProductDomainTupleEnumerable(IList<IDomain> domains)
        {
            this.domains = domains;
        }

        public IEnumerator<ITuple> GetEnumerator()
        {
            return new CartesianProductDomainTupleEnumerator(this.domains);
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

    }
}
