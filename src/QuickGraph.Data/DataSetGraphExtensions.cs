using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Diagnostics.Contracts;
using QuickGraph.Graphviz;

namespace QuickGraph.Data
{
    public static class DataSetGraphExtensions
    {
        public static DataSetGraph ToGraph(
#if !NET20
            this
#endif
            DataSet ds)
        {
            Contract.Requires(ds != null);

            var g = new DataSetGraph(ds);
            var populator = new DataSetGraphPopulatorAlgorithm(g, ds);
            populator.Compute();

            return g;
        }

        public static string ToGraphviz(
#if !NET20
this
#endif
            DataSetGraph visitedGraph)
        {
            Contract.Requires(visitedGraph != null);

            var algorithm = new DataSetGraphvizAlgorithm(visitedGraph);
            return algorithm.Generate();
        }
    }
}
