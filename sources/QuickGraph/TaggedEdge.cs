using System;

namespace QuickGraph
{
	[Serializable]
    public class TaggedEdge<TVertex,TTag> : Edge<TVertex>
    {
        private TTag tag;

        public TaggedEdge(TVertex source, TVertex target, TTag tag)
            :base(source,target)
        {
            this.tag = tag;
        }

        public event EventHandler TagChanged;

        protected virtual void OnTagChanged(EventArgs e)
        {
            if (this.TagChanged != null)
                this.TagChanged(this, e);
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
