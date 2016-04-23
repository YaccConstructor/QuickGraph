using System;
using System.Drawing;
using System.Windows.Forms;
using Common;
using Mono.Addins;
using QuickGraph;

// This plugin demonstrates how to use plugin system.
// Add your plugin reference to MainForm project.
// Read more at Mono.Addins wiki page on GitHub.
// https://github.com/mono/mono-addins/wiki/Architecture-Overview


[assembly: Addin]
[assembly: AddinDependency("GraphTasks", "1.0")]

namespace PluginSample
{
    [Extension]
    public class PluginSample : IAlgorithm
    {
        private readonly CheckBox _countSymbolsCheckBox = new CheckBox
        {
            Text = "Count symbols",
            Location = new Point(12, 20)
        };

        public PluginSample()
        {
            Options.Controls.Add(_countSymbolsCheckBox);
        }

        public string Name => "Sample Plugin";
        public string Author => "Eugene Auduchinok";

        public string Description =>
            "This plugin demonstrates how to use plugin system.\n" +
            "Algorithms removes one vertex on each step until there's no vertices.\n";

        public Panel Options { get; } = new Panel();
        public Panel Output { get; } = new Panel();

        public void Run(string dotSource)
        {
            var vertexFun = DotParserAdapter.VertexFunctions.Name;
            var edgeFun = DotParserAdapter.EdgeFunctions<string>.VerticesOnly;

            try
            {
                var graph = BidirectionalGraph<string, SEdge<string>>.LoadDot(dotSource, vertexFun, edgeFun);

                var message = $"{graph.VertexCount} vertices.";
                if (_countSymbolsCheckBox.Checked) message = $"{message} {dotSource.Length} symbols read.";
                MessageBox.Show(message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}