using System;
using System.Collections.Generic;
using System.Text;
using QuickGraph.Graphviz.Dot;

namespace QuickGraph.Graphviz
{
    public interface IDotEngine
    {
        string Run(
            GraphvizImageType imageType,
            string dot,
            string outputFileName);
    }
}
