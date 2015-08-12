namespace QuickGraph.Graphviz.Dot
{
    using System;
    using System.Reflection;
    using System.Collections.ObjectModel;

    public sealed class GraphvizRecordCellCollection : Collection<GraphvizRecordCell>
    {
        public GraphvizRecordCellCollection()
        {}

        public GraphvizRecordCellCollection(GraphvizRecordCell[] items)
            :base(items)
        {}

        public GraphvizRecordCellCollection(GraphvizRecordCellCollection items)
            :base(items)
        {}
    }
}

