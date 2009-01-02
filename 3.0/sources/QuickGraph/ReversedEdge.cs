using System;
using System.Diagnostics;
using System.Diagnostics.Contracts;

namespace QuickGraph
{
    [DebuggerDisplay("{Source}<-{Target}")]
    public class ReversedEdge<TVertex, TEdge> : 
        IEdge<TVertex>, 
        IEquatable<ReversedEdge<TVertex, TEdge>>
        where TEdge : IEdge<TVertex>
    {
        private readonly TEdge originalEdge;
        public ReversedEdge(TEdge originalEdge)
        {
            Contract.Requires(originalEdge != null);

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
        
        [Pure]
        public override bool  Equals(object obj)
        {
            if (!(obj is ReversedEdge<TVertex, TEdge>))
                return false;

            return Equals((ReversedEdge<TVertex, TEdge>)obj);
        }

        [Pure]
        public override int GetHashCode()
        {
            return this.OriginalEdge.GetHashCode();
        }

        [Pure]
        public override string ToString()
        {
            return String.Format("R({0})", this.OriginalEdge);
        }

        [Pure]
        public bool Equals(ReversedEdge<TVertex, TEdge> other)
        {
            return this.OriginalEdge.Equals(other.OriginalEdge);
        }
    }
}
