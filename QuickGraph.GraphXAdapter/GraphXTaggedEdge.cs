using System;
using GraphX.PCL.Common.Models;

namespace QuickGraph.GraphXAdapter
{
    public class GraphXTaggedEdge<TVertex, TTag> : GraphXEdge<TVertex>, ITagged<TTag>
    {
        public GraphXTaggedEdge(TVertex source, TVertex target, TTag tag) : base(source, target)
        {
            Tag = tag;
        }

        public TTag Tag { get; set; }
        public event EventHandler TagChanged;
    }
}
