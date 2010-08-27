namespace QuickGraph.Graphviz.Dot
{
    using System;

    public class GraphvizLayer
    {
        private string name;

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
            this.name = name;
        }

        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }
    }
}

