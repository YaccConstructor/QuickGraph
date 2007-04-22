using System;

namespace QuickGraph
{
    [Serializable]
    public class NamedEdge<Vertex> : Edge<Vertex>
    {
        private string name;
        public NamedEdge(Vertex source, Vertex target, string name)
            :base(source,target)
        {
            if (name == null)
                throw new ArgumentNullException("name");
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
