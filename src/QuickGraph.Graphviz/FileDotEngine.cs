using System;
using System.Collections.Generic;
using System.Text;
using QuickGraph.Graphviz.Dot;
using System.IO;
using System.Diagnostics.Contracts;

namespace QuickGraph.Graphviz
{
    /// <summary>
    /// Default dot engine implementation, writes dot code to disk
    /// </summary>
    public sealed class FileDotEngine : IDotEngine
    {
        public string Run(GraphvizImageType imageType, string dot, string outputFileName)
        {
            string output = outputFileName;
            if (!output.EndsWith(".dot", StringComparison.InvariantCultureIgnoreCase))
                output = output + ".dot";

            File.WriteAllText(output, dot);
            return output;
        }
    }
}
