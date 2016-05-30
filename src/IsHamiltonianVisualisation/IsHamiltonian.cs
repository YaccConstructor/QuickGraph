using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using System.Windows.Media;
using Common;
using System;
using Mono.Addins;
using QuickGraph;
using QuickGraph.Algorithms;
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
        private List<KeyValuePair<GraphXVertex, GraphX.Controls.VertexControl>> _graphVerticesVisualisation;
        private List<GraphXVertex> _graphVertices;
        private int _currentVertexIndex;
        private bool _isHamiltonian;
        private bool _hasOnlyOneVertex;
        private IsHamiltonianGraphAlgorithm<GraphXVertex, UndirectedEdge<GraphXVertex>> _algo;


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

        public static Func<GraphXVertex, GraphXVertex, IDictionary<string, string>, UndirectedEdge<GraphXVertex>>
            edgeFun = (v1, v2, attrs) => new UndirectedEdge<GraphXVertex>(v1, v2);

        public void Run(string dotSource)
        {
            var vertexFun = VertexFactory.Name;

            var graph = UndirectedGraph<GraphXVertex, UndirectedEdge<GraphXVertex>>.LoadDot(dotSource, vertexFun, edgeFun);
            var graphVisualisation = Graph.LoadDot(dotSource, vertexFun, EdgeFactory<GraphXVertex>.Weighted(0));

            if (!graph.Vertices.Any())
            {
                MessageBox.Show("Graph is empty.");
                return;
            }

            _algo = new IsHamiltonianGraphAlgorithm<GraphXVertex, UndirectedEdge<GraphXVertex>>(graph);
            _isHamiltonian = true;
            _hasOnlyOneVertex = false;

            if (graph.Vertices.Count() == 1)
            {
                _hasOnlyOneVertex = true;
                MessageBox.Show($"Graph contains one vertex. Graph is Hamiltonian");
            }

            _graphArea.LogicCore.Graph = graphVisualisation;
            _graphArea.GenerateGraph();
            _zoomControl.ZoomToFill();
            _graphVertices = graph.Vertices.ToList();
            _graphVerticesVisualisation = _graphArea.VertexList.ToList();
            
            _currentVertexIndex = 0;
        }

        public void NextStep()
        {
            var vertexVisualisation = _graphVerticesVisualisation.ElementAt(_currentVertexIndex);
            var vertex = _graphVertices.ElementAt(_currentVertexIndex);
            vertexVisualisation.Value.Background = new SolidColorBrush(Colors.YellowGreen);

            if ((!_hasOnlyOneVertex) && (!_algo.satisfiesHamiltonianCondition(vertex)))
            {
                _isHamiltonian = false;
                MessageBox.Show($"Vertex {vertexVisualisation.Key} has insufficient count of edges. Graph is not Hamiltonian");
            }

            if (++_currentVertexIndex == _graphVerticesVisualisation.Count)
            {
                MessageBox.Show($"Finished, " + (_isHamiltonian ? $"graph is Hamiltonian" : $"graph is not Hamiltonian"));
            }

            _zoomControl.ZoomToFill();
        }

        public void PreviousStep()
        {
            var vertex = _graphVerticesVisualisation.ElementAt(--_currentVertexIndex);
            vertex.Value.Background = new SolidColorBrush(Colors.LightGray);
            _zoomControl.ZoomToFill();
        }

        public bool CanGoBack => _currentVertexIndex > 0;
        public bool CanGoFurther => (_graphVerticesVisualisation == null) || (_currentVertexIndex < _graphVerticesVisualisation.Count);
    }
}