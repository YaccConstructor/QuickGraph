using System;

namespace QuickGraph
{
    public class IdentifiableEdge<TVertex> : Edge<TVertex>, IIdentifiable
    {
        private string id;

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
