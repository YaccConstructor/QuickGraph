namespace QuickGraph.Graphviz.Dot
{
    using System;
    using System.IO;
    using System.Reflection;
    using System.Collections.ObjectModel;
    using System.Diagnostics.Contracts;

    public class GraphvizLayerCollection : Collection<GraphvizLayer>
    {
        private string m_Separators = ":";

        public GraphvizLayerCollection()
        {}

        public GraphvizLayerCollection(GraphvizLayer[] items)
            :base(items)
        {}

        public GraphvizLayerCollection(GraphvizLayerCollection items)
            :base(items)
        {}

        public string ToDot()
        {
            if (base.Count == 0)
            {
                return null;
            }
            using (StringWriter writer = new StringWriter())
            {
                writer.Write("layers=\"");
                bool flag = false;
                foreach (GraphvizLayer layer in this)
                {
                    if (flag)
                    {
                        writer.Write(this.Separators);
                    }
                    else
                    {
                        flag = true;
                    }
                    writer.Write(layer.Name);
                }
                writer.WriteLine("\";");
                writer.WriteLine("layersep=\"{0}\"", this.Separators);
                return writer.ToString();
            }
        }

        public string Separators
        {
            get
            {
                return this.m_Separators;
            }
            set
            {
                Contract.Requires(!String.IsNullOrEmpty(value));

                this.m_Separators = value;
            }
        }
    }
}

