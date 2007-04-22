using System;
using System.Collections;
using System.Collections.Generic;

namespace QuickGraph.Operations
{
    internal sealed class CartesianProductDomainTupleEnumerator : DomainTupleEnumeratorBase
	{
		private ITuple tuple = null;
		private int[] indices = null;

        public CartesianProductDomainTupleEnumerator(IList<IDomain> domains)
		:base(domains)
		{
			this.indices=new int[this.Domains.Count];			
			this.Reset();
		}
		
		public override void Reset()
		{
            for (int i = 0; i < this.indices.Length; ++i)
            {
                if (i==this.indices.Length-1)
                    this.indices[i] = -1;
                else
                    this.indices[i] = 0;
            }
            this.tuple=null;
		}
		
		public override bool MoveNext()
		{
            for (int i = this.indices.Length - 1; i >= 0; i--)
            {
                if (this.indices[i] < this.Domains[i].Count - 1)
                {
                    // updating index
                    this.indices[i]++;
                    // reseting the other to zero
                    for(int j = i+1 ; j < this.indices.Length;++j)
                        this.indices[j] = 0;

                    // getting the tuple
                    tuple = new Tuple();
                    for (int k = 0; k < this.indices.Length; ++k)
                    {
                        object item = this.Domains[k][this.indices[k]];
                        tuple.Add(item);
                    }

                    return true;
                }
            }


            return false;
        }

        public override ITuple Current
        {
            get
            {
                return this.tuple;
            }
        }
    }
}
