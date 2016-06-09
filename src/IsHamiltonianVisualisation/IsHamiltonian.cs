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
        private IDictionary<GraphXVertex, GraphX.Controls.VertexControl> _graphVerticesVisualisation;
        private UndirectedGraph<GraphXVertex, UndirectedEdge<GraphXVertex>> _graph;
        private int _currentVertexIndex;
        private int _currentPathIndex;
        private bool _isHamiltonian;
        private List<List<GraphXVertex>> _pathes;
        private List<GraphXVertex> _currentPath;
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

            if (graph.IsVerticesEmpty)
            {
                MessageBox.Show("Graph is empty.");
                return;
            }

            _algo = new IsHamiltonianGraphAlgorithm<GraphXVertex, UndirectedEdge<GraphXVertex>>(graph);
            _algo.GetPermutations();
            _pathes = _algo.permutations;
            _currentPath = _pathes[0];
            _isHamiltonian = false;
            
            _graphArea.LogicCore.Graph = graphVisualisation;
            _graphArea.GenerateGraph();
            _zoomControl.ZoomToFill();
            _graph = graph;
            _graphVerticesVisualisation = _graphArea.VertexList;
            
            _currentVertexIndex = 0;
            _currentPathIndex = 0;
        }

        public void NextStep()
        {
            var currentVertex = _currentPath[_currentVertexIndex];
            GraphX.Controls.VertexControl vertexVisualisation;
            _graphVerticesVisualisation.TryGetValue(_graphVerticesVisualisation.Keys.First(a => a.Text.Equals(currentVertex.Text)), out vertexVisualisation);
            vertexVisualisation.Background = new SolidColorBrush(Colors.YellowGreen);
            if (_currentVertexIndex > 0) {
                var previousVertex = _currentPath[_currentVertexIndex - 1];
                if (!_graph.AdjacentVertices(previousVertex).Contains(currentVertex))
                {
                    MessageBox.Show($"This path is not Hamiltonian");
                    _currentVertexIndex = _currentPath.Count; // Move to next path
                }
            }
            if (_currentVertexIndex + 1 < _currentPath.Count)
            {
                _currentVertexIndex++;
            }
            else if (_currentPathIndex + 1 < _pathes.Count)
            {
                if (_currentVertexIndex == _currentPath.Count - 1)
                {
                    MessageBox.Show("This path is hamiltonian and graph is hamiltonian!");
                    _isHamiltonian = true;
                }
                foreach (var vertex in _graphVerticesVisualisation.Values)
                {
                    vertex.Background = new SolidColorBrush(Colors.LightGray);
                }
                _currentPathIndex++;
                _currentPath = _pathes[_currentPathIndex];
                _currentVertexIndex = 0;
            }
            else
            {
                MessageBox.Show($"Finished, " + (_isHamiltonian ? $"graph is Hamiltonian" : $"graph is not Hamiltonian"));
            }
           
            _zoomControl.ZoomToFill();
        }

        public void PreviousStep()
        {
            var currentVertex = _currentPath[--_currentVertexIndex];
            GraphX.Controls.VertexControl vertexVisualisation;
            _graphVerticesVisualisation.TryGetValue(_graphVerticesVisualisation.Keys.First(a => a.Text.Equals(currentVertex.Text)), out vertexVisualisation);
            vertexVisualisation.Background = new SolidColorBrush(Colors.LightGray);

            _zoomControl.ZoomToFill();
        }

        public bool CanGoBack => _currentVertexIndex > 0;
        public bool CanGoFurther => (_graphVerticesVisualisation == null) || (_currentVertexIndex < _graphVerticesVisualisation.Count);
    }
}