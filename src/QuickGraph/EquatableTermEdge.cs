using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace QuickGraph
{
    /// <summary>
    /// An equatable term edge implementation
    /// </summary>
    /// <typeparam name="TVertex">type of the vertices</typeparam>
#if !SILVERLIGHT
    [Serializable]
#endif
    [DebuggerDisplay("{Source}->{Target}")]
    public class EquatableTermEdge<TVertex> 
        : TermEdge<TVertex>
        , IEquatable<EquatableTermEdge<TVertex>>
    {
        public EquatableTermEdge(TVertex source, TVertex target, int sourceTerminal, int targetTerminal)
            : base(source, target, sourceTerminal, targetTerminal)
        { }

        public EquatableTermEdge(TVertex source, TVertex target)
            : base(source, target)
        { }

        public bool Equals(EquatableTermEdge<TVertex> other)
        {
            return
                (object)other != null &&
                this.Source.Equals(other.Source) &&
                this.Target.Equals(other.Target) &&
                this.SourceTerminal.Equals(other.SourceTerminal) &&
                this.TargetTerminal.Equals(other.TargetTerminal);
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as EquatableTermEdge<TVertex>);
        }

        public override int GetHashCode()
        {
            return
                HashCodeHelper.Combine(this.Source.GetHashCode(), this.Target.GetHashCode(),
                                       this.SourceTerminal.GetHashCode(), this.TargetTerminal.GetHashCode());
        }
    }
}
