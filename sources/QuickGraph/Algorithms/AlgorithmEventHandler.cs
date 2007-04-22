using System;
using System.Collections.Generic;
using System.Text;

namespace QuickGraph.Algorithms
{
    public delegate void AlgorithmEventHandler<Graph>(
        IAlgorithm<Graph> sender,
        EventArgs e);
}
