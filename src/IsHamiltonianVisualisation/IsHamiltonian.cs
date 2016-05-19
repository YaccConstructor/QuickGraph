using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using System.Windows.Media;
using Common;
using Mono.Addins;
using QuickGraph;
using QuickGraph.GraphXAdapter;
using MessageBox = System.Windows.Forms.MessageBox;

// Read more about plugin system on GitHub.
// https://github.com/mono/mono-addins/wiki/Architecture-Overview

// GraphX GraphArea works with classes implementing IMutableBidirectionalGraph only.
// Possible undirected graph workaround:
//   UndirectedGraph<TVertex, TEdge>.LoadDot(...).Edges.ToBidirectionalGraph<TVertex, TEdge>();

// Add your plugin reference to MainForm project for auto-rebuild while working on it.
// Please do not commit this reference, as it will force your plugin to be rebuilt when MainForm rebuilds.


[assembly: Addin]
[assembly: AddinDependency("GraphTasks", "1.0")]

namespace IsHamiltonianVisualisation
{
    using Graph = BidirectionalGraph<GraphXVertex, GraphXTaggedEdge<GraphXVertex, int>>;
    using GraphArea = BidirectionalGraphArea<GraphXVertex, GraphXTaggedEdge<GraphXVertex, int>>;

    [Extension]
    public class IsHamiltonian : IAlgorithm
    {
        private readonly GraphArea _graphArea;
        private readonly GraphXZoomControl _zoomControl;
        private List<KeyValuePair<GraphXVertex, GraphX.Controls.VertexControl>> _graphVertices;
        private int _currentVertexIndex;
        private double _threshold;
        private bool _isHamiltonian;

        public IsHamiltonian()
        {
            _graphArea = new GraphArea();
            _zoomControl = new GraphXZoomControl { Content = _graphArea };
            Output.Controls.Add(new ElementHost { Dock = DockStyle.Fill, Child = _zoomControl });
        }

        public string Name => "Is Hamiltonian graph";
        public string Author => "Roman Fedorov";

        public string Description =>
            "This plugin demonstrates how to determinate is graph a Hamiltonian one. " +
            "Using Dirac's theorem: if |vertices| >= 2 and " +
            "for any vertex deg(vertex) >= (|vertices| / 2) then graph is Hamiltonian\n";

        public Panel Options { get; } = new Panel { Dock = DockStyle.Fill };
        public Panel Output { get; } = new Panel { Dock = DockStyle.Fill };

        public void Run(string dotSource)
        {
            var vertexFun = VertexFactory.Name;
            var edgeFun = EdgeFactory<GraphXVertex>.Weighted(0);

            var graph = Graph.LoadDot(dotSource, vertexFun, edgeFun);

            if (!graph.Vertices.Any())
            {
                MessageBox.Show("Graph is empty.");
                return;
            }

            _graphArea.LogicCore.Graph = graph;
            _graphArea.GenerateGraph();
            _zoomControl.ZoomToFill();
            _graphVertices = _graphArea.VertexList.ToList();

            _isHamiltonian = true;
            _currentVertexIndex = 0;
            _threshold = graph.VertexCount / 2.0;
        }

        public void NextStep()
        {
            var vertex = _graphVertices.ElementAt(_currentVertexIndex);
            vertex.Value.Background = new SolidColorBrush(Colors.YellowGreen);
            if (_graphArea.LogicCore.Graph.OutEdges(vertex.Key).Count() < _threshold)
            {
                _isHamiltonian = false;
                MessageBox.Show($"Vertex {vertex.Key} contains insufficient count of edges. Graph is not Hamiltonian");
            }

            if (++_currentVertexIndex == _graphVertices.Count)
            {
                MessageBox.Show($"Finished, " + (_isHamiltonian ? $"graph is Hamiltonian" : $"graph is not Hamiltonian"));
            }

            _zoomControl.ZoomToFill();
        }

        public void PreviousStep()
        {
            var vertex = _graphVertices.ElementAt(--_currentVertexIndex);
            vertex.Value.Background = new SolidColorBrush(Colors.LightGray);
            _zoomControl.ZoomToFill();
        }

        public bool CanGoBack => _currentVertexIndex > 0;
        public bool CanGoFurther => (_graphVertices == null) || (_currentVertexIndex < _graphVertices.Count);
    }
}