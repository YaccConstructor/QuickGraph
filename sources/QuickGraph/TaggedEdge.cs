using System;

namespace QuickGraph
{
    public class TaggedEdge<Vertex,Marker> : Edge<Vertex>
    {
        private Marker tag;

        public TaggedEdge(Vertex source, Vertex target, Marker tag)
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

        public Marker Tag
        {
            get { return this.tag; }
            set 
            {
                if (!Comparison<Marker>.Equals(this.tag, value))
                {
                    this.tag = value;
                    this.OnTagChanged(EventArgs.Empty);
                }
            }
        }
    }
}
