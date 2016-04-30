using System;

namespace QuickGraph.GraphXAdapter
{
    public class GraphXTaggedVertex<TTag> : GraphXVertex
    {
        public GraphXTaggedVertex(string text, TTag tag) : base(text)
        {
            Tag = tag;
        }

        public TTag Tag { get; set; }
        public event EventHandler TagChanged;
    }
}
