using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using System.Windows.Media;
using Common;
using GraphX.Controls;
using GraphX.PCL.Common.Enums;
using GraphX.PCL.Common.Models;
using Mono.Addins;
using QuickGraph;
using QuickGraph.GraphXAdapter;
using Color = System.Drawing.Color;
using MessageBox = System.Windows.Forms.MessageBox;
using Point = System.Drawing.Point;

// This plugin demonstrates how to use plugin system.
// Add your plugin reference to MainForm project for auto-rebuild while working on it.
// Please do not commit this reference, as it will force every plugin to rebuild when MainForm rebuilds.
// Read more at Mono.Addins wiki page on GitHub.
// https://github.com/mono/mono-addins/wiki/Architecture-Overview


[assembly: Addin]
[assembly: AddinDependency("GraphTasks", "1.0")]

namespace PluginSample
{
    using Graph = BidirectionalGraph<GraphXVertex, GraphXTaggedEdge<GraphXVertex, int>>;
    using GraphArea = BidirectedGraphArea<GraphXVertex, GraphXTaggedEdge<GraphXVertex, int>>;

    [Extension]
    public class PluginSample : IAlgorithm
    {
        private readonly CheckBox _countSymbolsCheckBox;
        private readonly GraphArea _graphArea;
        private readonly GraphXZoomControl _zoomControl;
        private Stack<List<GraphSerializationData>> _steps;
        private bool _hasStarted;
        private bool _hasFinished;

        public PluginSample()
        {
            _countSymbolsCheckBox = new CheckBox {Text = "Count symbols when start", Location = new Point(12, 6)};
            Options.Controls.Add(_countSymbolsCheckBox);

            _graphArea = new GraphArea();
            _zoomControl = new GraphXZoomControl {Content = _graphArea};
            Output.Controls.Add(new ElementHost {Dock = DockStyle.Fill, Child = _zoomControl});
        }

        public string Name => "Sample Plugin";
        public string Author => "Eugene Auduchinok";

        public string Description =>
            "This plugin demonstrates how to use plugin system.\n" +
            "Algorithms removes one vertex on each step until graph's empty.\n";

        public Panel Options { get; } = new Panel {Dock = DockStyle.Fill};
        public Panel Output { get; } = new Panel {Dock = DockStyle.Fill};

        public void Run(string dotSource)
        {
            if (_countSymbolsCheckBox.Checked)
            {
                MessageBox.Show($"Read {dotSource.Length} symbol(s).");
            }

            var vertexFun = VertexFactory.Name;
            var edgeFun = EdgeFactory<GraphXVertex>.Weighted(0);

            var graph = Graph.LoadDot(dotSource, vertexFun, edgeFun);
            _steps = new Stack<List<GraphSerializationData>>();

            if (!graph.Vertices.Any())
            {
                MessageBox.Show("Graph is empty.");
                return;
            }

            _graphArea.LogicCore.Graph = graph;
            _graphArea.GenerateGraph();
            _zoomControl.ZoomToFill();

            HighlightToRemove();

            _hasStarted = true;
            _hasFinished = false;
        }

        public void NextStep()
        {
            _steps.Push(_graphArea.ExtractSerializationData());

            _graphArea.RemoveVertexAndEdges(_graphArea.VertexList.Keys.Last());
            if (!_graphArea.LogicCore.Graph.Vertices.Any())
            {
                _hasFinished = true;
                return;
            }

            _zoomControl.ZoomToFill();
            HighlightToRemove();
        }

        public void PreviousStep()
        {
            _graphArea.RebuildFromSerializationData(_steps.Pop());
            HighlightToRemove();
            _zoomControl.ZoomToFill();

            _hasFinished = false;
        }

        private void HighlightToRemove()
        {
            _graphArea.VertexList.Last().Value.Background = new SolidColorBrush(Colors.YellowGreen);
        }

        public bool CanGoBack => _steps != null && _steps.Count != 0;
        public bool CanGoFurther => _hasStarted && !_hasFinished;
    }
}