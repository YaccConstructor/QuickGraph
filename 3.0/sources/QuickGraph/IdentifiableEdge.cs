using System;
using System.Diagnostics;
using System.Diagnostics.Contracts;

namespace QuickGraph
{
    /// <summary>
    /// An identifiable edge.
    /// </summary>
    /// <typeparam name="TVertex"></typeparam>
    [Serializable]
    [DebuggerDisplay("{ID}:{Source}->{Target}")]
    public class IdentifiableEdge<TVertex> 
        : Edge<TVertex>
        , IIdentifiable
    {
        private readonly string id;

        /// <summary>
        /// Initializes a new instance of the <see cref="IdentifiableEdge&lt;TVertex&gt;"/> class.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="target">The target.</param>
        /// <param name="id">The id.</param>
        public IdentifiableEdge(TVertex source, TVertex target, string id)
            : base(source, target)
        {
            Contract.Requires(id != null);
            this.id = id;
        }

        /// <summary>
        /// Gets a string that uniquely indentifies the object.
        /// </summary>
        /// <value>The identity.</value>
        public string ID
        {
            [Pure]
            get { return this.id; }
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </returns>
        public override string ToString()
        {
            return this.id + ':' + base.ToString();
        }
    }
}
