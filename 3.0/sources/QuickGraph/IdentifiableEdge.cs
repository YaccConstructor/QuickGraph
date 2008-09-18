using System;
using System.Diagnostics;

namespace QuickGraph
{
    [Serializable]
    [DebuggerDisplay("{ID}:{Source}->{Target}")]
    public class IdentifiableEdge<TVertex> : 
        Edge<TVertex>, 
        IIdentifiable
    {
        private readonly string id;

        public IdentifiableEdge(string id, TVertex source, TVertex target)
            : base(source, target)
        {
            this.id = id;
        }

        public string ID
        {
            get { return this.id; }
        }

        public override string ToString()
        {
            return this.id;
        }
    }
}
