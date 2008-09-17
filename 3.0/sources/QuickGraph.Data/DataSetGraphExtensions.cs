using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace QuickGraph.Data
{
    public static class DataSetGraphExtensions
    {
        public static DataSetGraph ToGraph(this DataSet ds)
        {
            if (ds == null)
                throw new ArgumentNullException("ds");
            var g = new DataSetGraph(ds);
            var populator = new DataSetGraphPopulatorAlgorithm(g, ds);
            populator.Compute();

            return g;
        }
    }
}
