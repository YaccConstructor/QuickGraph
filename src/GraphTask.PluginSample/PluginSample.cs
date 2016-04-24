using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
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
        private readonly CheckBox _countSymbolsCheckBox;
        private readonly ElementHost _wpfHost;
        private Stack<BidirectionalGraph<string, SEdge<string>>> _steps;
        private BidirectionalGraph<string, SEdge<string>> _graph;
        private bool _hasStarted;
        private bool _hasFinished;

        public PluginSample()
        {
            _countSymbolsCheckBox = new CheckBox
            {
                Text = "Count symbols",
                Location = new Point(12, 20)
            };

            _wpfHost = new ElementHost()
            {
                Dock = DockStyle.Fill
            };

            Options.Controls.Add(_countSymbolsCheckBox);
//            Output.Controls.Add(_wpfHost);
            Output.Controls.Add(new CheckBox {Text = "Yo."});
        }

        public string Name => "Sample Plugin";
        public string Author => "Eugene Auduchinok";

        public string Description =>
            "This plugin demonstrates how to use plugin system.\n" +
            "Algorithms removes one vertex on each step until there's no vertices.\n";

        public Panel Options { get; } = new Panel();
        public Panel Output { get; } = new Panel { Dock = DockStyle.Fill };

        public void Run(string dotSource)
        {
            // These functions create vertices and edges from raw graph data.
            // Implement new ones if in need.
            var vertexFun = DotParserAdapter.VertexFunctions.Name;
            var edgeFun = DotParserAdapter.EdgeFunctions<string>.VerticesOnly;

            try
            {
                _graph = BidirectionalGraph<string, SEdge<string>>.LoadDot(dotSource, vertexFun, edgeFun);
                _steps = new Stack<BidirectionalGraph<string, SEdge<string>>>();

                var message = $"{_graph.VertexCount} vertices.";
                if (_countSymbolsCheckBox.Checked) message = $"{message} {dotSource.Length} symbols read.";
                MessageBox.Show(message);

                _hasStarted = true;
                _hasFinished = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void NextStep()
        {
            _steps.Push(_graph.Clone());

            var v = _graph.Vertices.First();
            _graph.RemoveVertex(v);
            ShowResults();

            if (!_graph.Vertices.Any()) _hasFinished = true;
        }

        public void PreviousStep()
        {
            _graph = _steps.Pop();
            ShowResults();

            _hasFinished = false;
        }

        private void ShowResults()
        {
            MessageBox.Show($"{_graph.VertexCount} vertices.");
        }

        public bool CanGoBack => _steps != null && _steps.Count != 0;
        public bool CanGoFurther => _hasStarted && !_hasFinished;
    }
}