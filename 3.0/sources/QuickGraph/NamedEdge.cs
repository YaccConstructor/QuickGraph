using System;
using System.Diagnostics.Contracts;

namespace QuickGraph
{
    [Serializable]
    public class NamedEdge<TVertex> : Edge<TVertex>
    {
        private readonly string name;
        public NamedEdge(TVertex source, TVertex target, string name)
            :base(source,target)
        {
            Contract.Requires(name != null);

            this.name = name;
        }

        public string Name
        {
            get { return this.name; }
        }
            
        public override string ToString()
        {
            return this.Name;
        }
    }
}
