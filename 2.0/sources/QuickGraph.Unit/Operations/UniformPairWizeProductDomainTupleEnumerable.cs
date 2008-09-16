using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Collections.Generic;

namespace QuickGraph.Operations
{
    public sealed class UniformPairWizeProductDomainTupleEnumerable : 
        IEnumerable<ITuple>
    {
        private IList<IDomain> domains;
        public UniformPairWizeProductDomainTupleEnumerable(IList<IDomain> domains)
        {
            if (domains == null)
                throw new ArgumentNullException("domains");
            this.domains = domains;
            int count = -1;
            for (int i = 0; i < domains.Count; ++i)
            {
                if (domains[i].Count == 0)
                    throw new ArgumentException("domain count empty", i.ToString());
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
            return new UniformPairWizeProductDomainTupleEnumerator(this.Domains);
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private sealed class UniformPairWizeProductDomainTupleEnumerator : 
            DomainTupleEnumeratorBase
        {
            private int m = -1;
            private int domainCount=-1;
            private List<BipartiteGraph> bipartiteGraphs = null;
            private int bipartiteGraphIndex = -1;
            private int leftIndex = 0;
            private int rightIndex = -1;
            private Tuple tuple = null;

            public UniformPairWizeProductDomainTupleEnumerator(IList<IDomain> domains)
		    :base(domains)
            {
                this.domainCount = this.Domains[0].Count;
                this.Reset();
            }

            public override void Reset()
            {
                // get number of bipartite graphs
                m = (int)Math.Ceiling(Math.Log(this.Domains.Count, 2));

                // create bipartite graphs
                this.bipartiteGraphs = new List<BipartiteGraph>(m);
                for (int i = 0; i < m; ++i)
                {
                    // create bipartie graph
                    BipartiteGraph bg = new BipartiteGraph(this.Domains.Count);
                    this.bipartiteGraphs.Add(bg);

                    // do some swapping
                    if (i>0)
                        bg.Swap(i-1, bg.Left.Count+i-1);
                }


                this.bipartiteGraphIndex = -1;
                this.leftIndex = 0;
                this.rightIndex = 0;
                this.tuple = null;
            }

            public override bool MoveNext()
            {
                do
                {
                    if (this.leftIndex == this.rightIndex && this.bipartiteGraphIndex < this.bipartiteGraphs.Count)
                    {
                        this.bipartiteGraphIndex++;
                        this.CreateTuple();
                        this.bipartiteGraphIndex = this.bipartiteGraphs.Count;
                        return true;
                    }
                    else
                    {
                        this.bipartiteGraphIndex++;
                        if (this.bipartiteGraphIndex < this.bipartiteGraphs.Count)
                        {
                            this.CreateTuple();
                            return true;
                        }
                    }

                    // increasing index
                    this.rightIndex++;
                    if (this.rightIndex >= this.domainCount)
                    {
                        this.leftIndex++;
                        this.rightIndex = 0;
                    }
                    this.bipartiteGraphIndex = -1;
                } while (this.leftIndex < this.domainCount && this.rightIndex < this.domainCount);

                return false;
            }

            private void CreateTuple()
            {
                // get bipartite graph
                BipartiteGraph bg = (BipartiteGraph)this.bipartiteGraphs[this.bipartiteGraphIndex];

                this.tuple = new Tuple();
                for (int i = 0; i < this.Domains.Count; ++i)
                {
                    if (bg.Left.ContainsKey(i))
                        this.tuple.Add(this.Domains[i][leftIndex]);
                    else
                        this.tuple.Add(this.Domains[i][rightIndex]);
                }
            }

            public override ITuple Current
            {
                get
                {
                    return this.tuple;
                }
            }

            private sealed class BipartiteGraph
            {
                private SortedList<int, int> left = new SortedList<int, int>();
                private SortedList<int, int> right = new SortedList<int, int>();

                public BipartiteGraph(int count)
                {
                    int middle = count / 2 + count%2;
                    int i = 0;
                    for (i = 0; i < middle; ++i)
                    {
                        left.Add(i, i);
                    }
                    for (; i < count; ++i)
                        right.Add(i, i);
                }

                public void Swap(int i, int j)
                {
                    left.Remove(i);
                    right.Remove(j);
                    left.Add(j, j);
                    right.Add(i, i);
                }

                public SortedList<int, int> Left
                {
                    get
                    {
                        return this.left;
                    }
                }
                public SortedList<int, int> Right
                {
                    get
                    {
                        return this.right;
                    }
                }

                public override string ToString()
                {
                    StringWriter sw = new StringWriter();
                    sw.Write("[");
                    foreach (object o in this.Left.Keys)
                        sw.Write("{0} ",o);
                    sw.Write("] [");
                    foreach (object o in this.Right.Keys)
                        sw.Write("{0} ", o);
                    sw.Write("]"); 
                    return sw.ToString();
                }
            }
        }
    }
}
