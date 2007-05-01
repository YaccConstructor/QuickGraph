namespace QuickGraph.Graphviz.Dot
{
    using System;

    public class GraphvizLayer
    {
        private string m_Name;

        public GraphvizLayer(string name)
        {
            if ((name == null) || (name.Length == 0))
            {
                throw new ArgumentNullException("name");
            }
            if (name.Length == 0)
            {
                throw new ArgumentException("name is empty");
            }
            this.m_Name = name;
        }

        public string Name
        {
            get
            {
                return this.m_Name;
            }
            set
            {
                this.m_Name = value;
            }
        }
    }
}

