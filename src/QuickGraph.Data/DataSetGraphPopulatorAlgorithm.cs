using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using QuickGraph.Algorithms;
using System.Diagnostics.Contracts;

namespace QuickGraph.Data
{
    public sealed class DataSetGraphPopulatorAlgorithm :
        AlgorithmBase<IMutableVertexAndEdgeListGraph<DataTable, DataRelationEdge>>
    {
        private readonly DataSet dataSet;

        public DataSetGraphPopulatorAlgorithm(
            IMutableVertexAndEdgeListGraph<DataTable, DataRelationEdge> visitedGraph,
            DataSet dataSet
            )
            : base(visitedGraph)
        {
            Contract.Requires(dataSet != null);

            this.dataSet = dataSet;
        }

        public DataSet DataSet
        {
            get { return this.dataSet; }
        }

        protected override void InternalCompute()
        {
            foreach (DataTable table in this.DataSet.Tables)
                this.VisitedGraph.AddVertex(table);

            foreach (DataRelation relation in this.DataSet.Relations)
                this.VisitedGraph.AddEdge(new DataRelationEdge(relation));
        }
    }
}
