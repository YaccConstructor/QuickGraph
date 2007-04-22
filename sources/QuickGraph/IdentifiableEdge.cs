using System;

namespace QuickGraph
{
    public class IdentifiableEdge<Vertex> : Edge<Vertex>, IIdentifiable
    {
        private string id;

        public IdentifiableEdge(string id, Vertex source, Vertex target)
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

    public sealed class IdentifiableEdgeFactory<Vertex> : IIdentifiableEdgeFactory<Vertex,IdentifiableEdge<Vertex>>
    {
        public IdentifiableEdge<Vertex> CreateEdge(string id, Vertex source, Vertex target)
        {
            return new IdentifiableEdge<Vertex>(id, source, target);
        }
    }
}
