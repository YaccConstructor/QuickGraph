using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace QuickGraph.Data
{
    public class DataSetGraph :
        BidirectionalGraph<DataTable, DataRelationEdge>
    {
        public DataSet DataSet { get; private set; }

        public DataSetGraph(DataSet dataSet)
        {
            if (dataSet == null)
                throw new ArgumentNullException("dataSet");
            this.DataSet = dataSet;
        }
    }
}
