using System;
using System.Collections.Generic;
using System.Text;

namespace QuickGraph.Graphviz.Dot
{
    public sealed class GraphvizPoint
    {
        readonly int x;
        readonly int y;

        public int X { get { return this.x; } }
        public int Y { get { return this.y; } }

        public GraphvizPoint(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
