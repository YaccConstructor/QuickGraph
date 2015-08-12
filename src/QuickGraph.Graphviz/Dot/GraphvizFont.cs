using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics.Contracts;

namespace QuickGraph.Graphviz.Dot
{
    public sealed class GraphvizFont
    {
        readonly string name;
        readonly float sizeInPoints;

        public string Name { get { return this.name; } }
        public float SizeInPoints { get { return this.sizeInPoints; } }

        public GraphvizFont(string name, float sizeInPoints)
        {
            Contract.Requires(!String.IsNullOrEmpty(name));
            Contract.Requires(sizeInPoints > 0);

            this.name = name;
            this.sizeInPoints = sizeInPoints;
        }
    }
}
