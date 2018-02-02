using System;
using System.Diagnostics;
using System.Diagnostics.Contracts;

namespace QuickGraph
{
    /// <summary>
    /// The default <see cref="ITermEdge&lt;TVertex&gt;"/> implementation.
    /// </summary>
    /// <typeparam name="TVertex">The type of the vertex.</typeparam>
#if !SILVERLIGHT
    [Serializable]
#endif
    [DebuggerDisplay("{Source}->{Target}")]
    public class TermEdge<TVertex> 
        : ITermEdge<TVertex>
    {
        private readonly TVertex source;
        private readonly TVertex target;
        private readonly int sourceTerminal;
        private readonly int targetTerminal;

        /// <summary>
        /// Constructs new <see cref="TermEdge&lt;TVertex&gt;"/> using source/target vertex and source/target terminals.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="target">The target.</param>
        /// <param name="sourceTerminal">The source terminal.</param>
        /// <param name="targetTerminal">The target terminal.</param>
        public TermEdge(TVertex source, TVertex target, int sourceTerminal, int targetTerminal)
        {
            Contract.Requires(source != null);
            Contract.Requires(target != null);
            Contract.Requires(sourceTerminal >= 0);
            Contract.Requires(targetTerminal >= 0);
            Contract.Ensures(this.Source.Equals(source));
            Contract.Ensures(this.Target.Equals(target));
            Contract.Ensures(this.SourceTerminal.Equals(sourceTerminal));
            Contract.Ensures(this.TargetTerminal.Equals(targetTerminal));

            this.source = source;
            this.target = target;
            this.sourceTerminal = sourceTerminal;
            this.targetTerminal = targetTerminal;
        }

        /// <summary>
        /// Constructs new <see cref="TermEdge&lt;TVertex&gt;"/> using source/target vertex and zero terminals.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="target">The target.</param>
        public TermEdge(TVertex source, TVertex target)
            : this(source, target, 0, 0)
        {
        }

        /// <summary>
        /// Gets the source vertex
        /// </summary>
        /// <value></value>
        public TVertex Source
        {
            get { return this.source; }
        }

        /// <summary>
        /// Gets the target vertex
        /// </summary>
        /// <value></value>
        public TVertex Target
        {
            get { return this.target; }
        }

        /// <summary>
        /// Gets the source terminal
        /// </summary>
        public int SourceTerminal
        {
            get { return this.sourceTerminal; }
        }

        /// <summary>
        /// Gets the target terminal
        /// </summary>
        public int TargetTerminal
        {
            get { return this.targetTerminal; }
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </returns>
        public override string ToString()
        {
            return string.Format("{0} ({1}) -> {2} ({3})", this.Source, this.SourceTerminal, this.Target, this.TargetTerminal);
        }
    }
}
