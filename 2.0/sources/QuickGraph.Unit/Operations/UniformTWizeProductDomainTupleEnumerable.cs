using System;
using System.Collections.Generic;

namespace QuickGraph.Operations
{
    public sealed class UniformTWizeProductDomainTupleEnumerable : 
        IEnumerable<ITuple>
    {
        private IList<IDomain> domains;
        private int tupleSize;
        public UniformTWizeProductDomainTupleEnumerable(IList<IDomain> domains, int tupleSize)
        {
            if (domains == null)
                throw new ArgumentNullException("domains");
            if (tupleSize <= 0)
                throw new ArgumentOutOfRangeException("tupleSize is negative or zero");

            this.domains = domains;
            this.tupleSize = tupleSize;

            int count = -1;
            for (int i = 0; i < domains.Count; ++i)
            {
                if (i == 0)
                    count = domains[i].Count;
                else
                {
                    if (count != domains[i].Count)
                        throw new ArgumentException("Domains have not uniform size");
                }
            }
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
            return new UniformTWizeProductDomainTupleEnumerator(this.Domains, this.tupleSize);
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        internal sealed class UniformTWizeProductDomainTupleEnumerator :
            DomainTupleEnumeratorBase
        {
            private int tupleSize;
            public UniformTWizeProductDomainTupleEnumerator(IList<IDomain> domains, int tupleSize)
                :base(domains)
            {
                this.tupleSize = tupleSize;
                this.CreateColoring();
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
                get { throw new NotImplementedException(); }
            }

            private void CreateColoring()
            {
            }
        }
    }
}
