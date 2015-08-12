using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using QuickGraph.Graphviz;
using QuickGraph.Graphviz.Dot;
using System.Diagnostics.Contracts;

namespace QuickGraph.Data
{
    /// <summary>
    /// An algorithm that renders a DataSet graph to the Graphviz DOT format.
    /// </summary>
    public class DataSetGraphvizAlgorithm
        : GraphvizAlgorithm<DataTable, DataRelationEdge>
    {
        public DataSetGraphvizAlgorithm(DataSetGraph visitedGraph)
            : base(visitedGraph)
        {
            this.InitializeFormat();
        }

        public DataSetGraphvizAlgorithm(
            DataSetGraph visitedGraph,
            string path,
            GraphvizImageType imageType
            )
            : base(visitedGraph, path, imageType)
        {
            this.InitializeFormat();
        }

        private void InitializeFormat()
        {
            this.FormatVertex += new FormatVertexEventHandler<DataTable>(FormatTable);
            this.FormatEdge += new FormatEdgeAction<DataTable, DataRelationEdge>(FormatRelationEdge);

            this.CommonVertexFormat.Style = GraphvizVertexStyle.Solid;
            this.CommonVertexFormat.Shape = GraphvizVertexShape.Record;
        }


        protected virtual void FormatTable(object sender, FormatVertexEventArgs<DataTable> e)
        {
            Contract.Requires(sender != null);
            Contract.Requires(e != null);

            var v = e.Vertex;
            var format = e.VertexFormatter;
            format.Shape = GraphvizVertexShape.Record;

            // creating a record with a title and a list of columns.
            var title = new GraphvizRecordCell() {
                 Text = v.TableName
            };
            var sb = new StringBuilder();
            bool first = true;
            foreach (DataColumn column in v.Columns)
            {
                if (first) first = false;
                else
                    sb.AppendLine();             
                sb.AppendFormat("+ {0} : {1}", column.ColumnName, column.DataType.Name);
                if (column.Unique) sb.Append(" unique");
            }
            var columns = new GraphvizRecordCell()
            {
                Text = sb.ToString().TrimEnd()
            };

            format.Record.Cells.Add(title);
            format.Record.Cells.Add(columns);
        }

        protected virtual void FormatRelationEdge(object sender, FormatEdgeEventArgs<DataTable, DataRelationEdge> args)
        {
            Contract.Requires(sender != null); 
            Contract.Requires(args != null);

            var e = args.Edge;
            var format = args.EdgeFormatter;

            format.Label.Value = e.Relation.RelationName;
        }
    }
}
