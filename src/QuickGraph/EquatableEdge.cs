using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace QuickGraph
{
    /// <summary>
    /// An equatable edge implementation
    /// </summary>
    /// <typeparam name="TVertex">type of the vertices</typeparam>
#if !SILVERLIGHT
    [Serializable]
#endif
    [DebuggerDisplay("{Source}->{Target}")]
    public class EquatableEdge<TVertex> 
        : Edge<TVertex>
        , IEquatable<EquatableEdge<TVertex>>
    {
        public EquatableEdge(TVertex source, TVertex target)
            : base(source, target)
        { }

        public bool Equals(EquatableEdge<TVertex> other)
        {
            return
                (object)other != null &&
                this.Source.Equals(other.Source) &&
                this.Target.Equals(other.Target);
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as EquatableEdge<TVertex>);
        }

        public override int GetHashCode()
        {
            return
                HashCodeHelper.Combine(this.Source.GetHashCode(), this.Target.GetHashCode());
        }
    }
}
