using System;
using GraphX.PCL.Common.Models;

namespace QuickGraph.GraphXAdapter
{
    public class GraphXVertex : VertexBase
    {
        public GraphXVertex(string text)
        {
            Text = text;
        }

        public string Text { get; set; }

        public override string ToString()
        {
            return Text;
        }

        public GraphXVertex() : this(string.Empty)
        {
        }
    }
}
