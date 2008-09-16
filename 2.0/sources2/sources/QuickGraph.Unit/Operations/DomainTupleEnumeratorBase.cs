using System;
using System.Collections;
using System.Collections.Generic;

namespace QuickGraph.Operations
{
	internal abstract class DomainTupleEnumeratorBase : 
        IEnumerator<ITuple>,
        IEnumerator
	{
		private IList<IDomain> domains;
        public DomainTupleEnumeratorBase(IList<IDomain> domains)
        {
            if (domains == null)
                throw new ArgumentNullException("domains");
            this.domains = domains;
            foreach(IDomain domain in domains)
            {
                if (domain.Count == 0)
                    throw new ArgumentException("A domain is empty");
            }
        }
		
		public IList<IDomain> Domains
		{
			get
			{
				return this.domains;
			}
		}
		
		public abstract void Reset();
		public abstract bool MoveNext();
		public abstract ITuple Current {get;}
		
		Object IEnumerator.Current
		{
			get
			{
				return this.Current;
			}
		}

        public virtual void Dispose()
        {
            this.domains = null;
        }
    }
}
