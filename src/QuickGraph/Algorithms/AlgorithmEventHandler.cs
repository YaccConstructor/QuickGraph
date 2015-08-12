using System;
using System.Collections.Generic;
using System.Text;

namespace QuickGraph.Algorithms
{
    public delegate void AlgorithmEventHandler<TGraph>(
        IAlgorithm<TGraph> sender,
        EventArgs e);
}
