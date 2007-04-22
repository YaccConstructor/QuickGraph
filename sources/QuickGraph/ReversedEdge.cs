using System;

namespace QuickGraph
{
    public sealed class ReversedEdge<Vertex,Edge> : IEdge<Vertex>
        where Edge : IEdge<Vertex>
    {
        private readonly Edge originalEdge;
        public ReversedEdge(Edge originalEdge)
        {
            if (originalEdge == null)
                throw new ArgumentNullException("originalEdge");
            this.originalEdge = originalEdge;
        }

        public Edge OriginalEdge
        {
            get { return this.originalEdge; }
        }

        public Vertex Source
        {
            get { return this.OriginalEdge.Target; }
        }

        public Vertex Target
        {
            get { return this.OriginalEdge.Target; }
        }
        
        public override bool  Equals(object obj)
        {
            ReversedEdge<Vertex, Edge> edge = obj as ReversedEdge<Vertex, Edge>;
            if (obj == null)
                return false;

            return this.OriginalEdge.Equals(edge.OriginalEdge);
        }

        public override int GetHashCode()
        {
            return this.OriginalEdge.GetHashCode();
        }

        public override string ToString()
        {
            return String.Format("R()", this.OriginalEdge);
        }
    }
}
