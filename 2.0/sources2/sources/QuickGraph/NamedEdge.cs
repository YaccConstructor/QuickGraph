using System;

namespace QuickGraph
{
    [Serializable]
    public class NamedEdge<TVertex> : Edge<TVertex>
    {
        private readonly string name;
        public NamedEdge(TVertex source, TVertex target, string name)
            :base(source,target)
        {
            GraphContracts.AssumeNotNull(name, "name");
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
