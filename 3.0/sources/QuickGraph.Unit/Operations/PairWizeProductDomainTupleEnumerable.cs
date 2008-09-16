using System;
using System.Collections.Generic;

namespace QuickGraph.Operations
{
    internal sealed class PairWizeProductDomainTupleEnumerable : IEnumerable<ITuple>
    {
        private IList<IDomain> domains;
        public PairWizeProductDomainTupleEnumerable(IList<IDomain> domains)
        {
            if (domains==null)
                throw new ArgumentNullException("domains");
            this.domains = domains;
        }

        public IList<IDomain> Domains
        {
            get
            {
                return this.domains;
            }
        }

        public IEnumerator<ITuple> GetEnumerator()
        {
            return new PairWizeProductDomainTupleEnumerator(this.Domains);
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private sealed class PairWizeProductDomainTupleEnumerator : DomainTupleEnumeratorBase
        {
            public PairWizeProductDomainTupleEnumerator(IList<IDomain> domains)
		    :base(domains)
            {
                this.Reset();
            }

            public override void Reset()
            {
                throw new NotImplementedException();
            }

            public override bool MoveNext()
            {
                throw new NotImplementedException();
            }

            public override ITuple Current
            {
                get
                {
                    throw new NotImplementedException();
                }
            }
        }
    }
}
