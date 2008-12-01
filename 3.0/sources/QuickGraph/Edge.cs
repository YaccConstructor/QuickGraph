using System;
using System.Diagnostics;
using System.Diagnostics.Contracts;

namespace QuickGraph
{
    [Serializable]
    [DebuggerDisplay("{Source}->{Target}")]
    public class Edge<TVertex> 
        : IEdge<TVertex>
    {
        private readonly TVertex source;
        private readonly TVertex target;

        public Edge(TVertex source, TVertex target)
        {
            Contract.Requires(source != null);
            Contract.Requires(target != null);

            this.source = source;
            this.target = target;
        }

        public static Type VertexType
        {
            [Pure]
            get { return typeof(TVertex); }
        }

        public TVertex Source
        {
            [Pure]
            get { return this.source; }
        }

        public TVertex Target
        {
            [Pure]
            get { return this.target; }
        }

        public override string ToString()
        {
            return String.Format("{0}->{1}", this.Source, this.Target);
        }
    }
}
