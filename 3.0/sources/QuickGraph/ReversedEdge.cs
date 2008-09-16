using System;

namespace QuickGraph
{
    public struct ReversedEdge<TVertex,TEdge> : 
        IEdge<TVertex>, 
        IEquatable<ReversedEdge<TVertex, TEdge>>
        where TEdge : IEdge<TVertex>
    {
        private readonly TEdge originalEdge;
        public ReversedEdge(TEdge originalEdge)
        {
            GraphContracts.AssumeNotNull(originalEdge, "originalEdge");
            this.originalEdge = originalEdge;
        }

        public TEdge OriginalEdge
        {
            get { return this.originalEdge; }
        }

        public TVertex Source
        {
            get { return this.OriginalEdge.Target; }
        }

        public TVertex Target
        {
            get { return this.OriginalEdge.Source; }
        }
        
        public override bool  Equals(object obj)
        {
            if (!(obj is ReversedEdge<TVertex, TEdge>))
                return false;

            return Equals((ReversedEdge<TVertex, TEdge>)obj);
        }

        public override int GetHashCode()
        {
            return this.OriginalEdge.GetHashCode();
        }

        public override string ToString()
        {
            return String.Format("R({0})", this.OriginalEdge);
        }

        public bool Equals(ReversedEdge<TVertex, TEdge> other)
        {
            return this.OriginalEdge.Equals(other.OriginalEdge);
        }
    }
}
