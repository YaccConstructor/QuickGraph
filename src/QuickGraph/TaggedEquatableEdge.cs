using System;
using System.Diagnostics.Contracts;
using System.Diagnostics;

namespace QuickGraph
{
    /// <summary>
    /// An equatable, tagged, edge
    /// </summary>
    /// <typeparam name="TVertex">type of the vertices</typeparam>
    /// <typeparam name="TTag"></typeparam>
#if !SILVERLIGHT
    [Serializable]
#endif
    [DebuggerDisplay("{Source}->{Target}:{Tag}")]
    public class TaggedEquatableEdge<TVertex, TTag>
        : EquatableEdge<TVertex>
        , ITagged<TTag>
    {
        private TTag tag;

        public TaggedEquatableEdge(TVertex source, TVertex target, TTag tag)
            : base(source, target)
        {
            Contract.Ensures(Object.Equals(this.Tag, tag));

            this.tag = tag;
        }

        public event EventHandler TagChanged;

        protected virtual void OnTagChanged(EventArgs e)
        {
            var eh = this.TagChanged;
            if (eh != null)
                eh(this, e);
        }

        public TTag Tag
        {
            get { return this.tag; }
            set
            {
                if (!object.Equals(this.tag, value))
                {
                    this.tag = value;
                    this.OnTagChanged(EventArgs.Empty);
                }
            }
        }
    }
}
