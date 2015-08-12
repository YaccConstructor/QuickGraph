using System;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Runtime.InteropServices;

namespace QuickGraph
{
    /// <summary>
    /// A reversed edge
    /// </summary>
    /// <typeparam name="TVertex">type of the vertices</typeparam>
    /// <typeparam name="TEdge">type of the edges</typeparam>
#if !SILVERLIGHT
    [Serializable]
#endif
    [StructLayout(LayoutKind.Auto)]
    [DebuggerDisplay("{Source}<-{Target}")]
    public struct SReversedEdge<TVertex, TEdge> 
        : IEdge<TVertex>
        , IEquatable<SReversedEdge<TVertex, TEdge>>
        where TEdge : IEdge<TVertex>
    {
        private readonly TEdge originalEdge;
        public SReversedEdge(TEdge originalEdge)
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
        public override bool Equals(object obj)
        {
            if (!(obj is SReversedEdge<TVertex, TEdge>))
                return false;

            return Equals((SReversedEdge<TVertex, TEdge>)obj);
        }

        [Pure]
        public override int GetHashCode()
        {
            return this.OriginalEdge.GetHashCode() ^ 16777619;
        }

        [Pure]
        public override string ToString()
        {
            return String.Format("R({0})", this.OriginalEdge);
        }

        [Pure]
        public bool Equals(SReversedEdge<TVertex, TEdge> other)
        {
            return this.OriginalEdge.Equals(other.OriginalEdge);
        }
    }
}
