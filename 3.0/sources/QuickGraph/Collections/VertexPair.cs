using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics.Contracts;
using System.Diagnostics;

namespace QuickGraph.Collections
{
    /// <summary>
    /// An equatable pair of vertices
    /// </summary>
    /// <typeparam name="TVertex"></typeparam>
    [Serializable]
    [DebuggerDisplay("{source} -> {target}")]
    public struct VertexPair<TVertex>
        : IEquatable<VertexPair<TVertex>>
        , IEdge<TVertex>
    {
        readonly TVertex source;
        readonly TVertex target;

        public static VertexPair<TVertex> FromEdge<TEdge>(TEdge edge)
            where TEdge : IEdge<TVertex>
        {
            Contract.Requires(edge != null);

            return new VertexPair<TVertex>(edge.Source, edge.Target);
        }

        public VertexPair(TVertex source, TVertex target)
        {
            Contract.Requires(source != null);
            Contract.Requires(target != null);

            this.source = source;
            this.target = target;
        }

        public override int GetHashCode()
        {
            return this.source.GetHashCode() ^
                this.target.GetHashCode();
        }

        public bool Equals(VertexPair<TVertex> other)
        {
            return
                other.source.Equals(this.source) &&
                other.target.Equals(this.target);
        }

        public override bool Equals(object obj)
        {
            return
                obj is VertexPair<TVertex> &&
                base.Equals((VertexPair<TVertex>)obj);
        }

        public override string ToString()
        {
            return this.source.ToString() + " -> " + this.target.ToString();
        }

        [Pure]
        public TVertex Source
        {
            get { return this.source; }
        }

        [Pure]
        public TVertex Target
        {
            get { return this.target; }
        }
    }
}
