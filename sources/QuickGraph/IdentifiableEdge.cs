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

    public sealed class IdentifiableEdgeFactory<TVertex> : IIdentifiableEdgeFactory<TVertex,IdentifiableEdge<TVertex>>
    {
        public IdentifiableEdge<TVertex> CreateEdge(string id, TVertex source, TVertex target)
        {
            return new IdentifiableEdge<TVertex>(id, source, target);
        }
    }
}
