using System;

namespace QuickGraph
{
    public struct ReversedEdge<Vertex,Edge> : IEdge<Vertex>, IEquatable<ReversedEdge<Vertex, Edge>>
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
            if (!(obj is ReversedEdge<Vertex, Edge>))
                return false;

            return Equals((ReversedEdge<Vertex, Edge>)obj);
        }

        public override int GetHashCode()
        {
            return this.OriginalEdge.GetHashCode();
        }

        public override string ToString()
        {
            return String.Format("R()", this.OriginalEdge);
        }

        public bool Equals(ReversedEdge<Vertex, Edge> other)
        {
            return this.OriginalEdge.Equals(other.OriginalEdge);
        }
    }
}
