using System;
using System.Diagnostics.Contracts;

namespace QuickGraph
{
#if !SILVERLIGHT
	[Serializable]
#endif
    public class TaggedEdge<TVertex,TTag> 
        : Edge<TVertex>
        , ITagged<TTag>
    {
        private TTag tag;

        public TaggedEdge(TVertex source, TVertex target, TTag tag)
            :base(source,target)
        {
            Contract.Ensures(Object.Equals(this.Tag,tag));

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
