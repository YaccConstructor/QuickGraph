using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Diagnostics.Contracts;

namespace QuickGraph.Data
{
    public class DataSetGraph :
        BidirectionalGraph<DataTable, DataRelationEdge>
    {
        readonly DataSet dataSet;
        public DataSet DataSet
        {
            get { return this.dataSet; }
        }

        internal DataSetGraph(DataSet dataSet)
        {
            Contract.Requires(dataSet != null);

            this.dataSet = dataSet;
        }
    }
}
