using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using Common;
using Mono.Addins;
using System;
using System.Windows.Media;
using QuickGraph;
using QuickGraph.GraphXAdapter;
using QuickGraph.Algorithms;

// Read more about plugin system on GitHub.
// https://github.com/mono/mono-addins/wiki/Architecture-Overview

// GraphX GraphArea works with classes implementing IMutableBidirectionalGraph only.
// Possible undirected graph workaround:
//   UndirectedGraph<TVertex, TEdge>.LoadDot(...).Edges.ToBidirectionalGraph<TVertex, TEdge>();

// Add your plugin reference to MainForm project for auto-rebuild while working on it.
// Please do not commit this reference, as it will force your plugin to be rebuilt when MainForm rebuilds.


[assembly: Addin]
[assembly: AddinDependency("GraphTasks", "1.0")]

namespace IsEulerianVisualisation
{
    using Graph = BidirectionalGraph<GraphXVertex, GraphXTaggedEdge<GraphXVertex, int>>;
    using GraphArea = BidirectionalGraphArea<GraphXVertex, GraphXTaggedEdge<GraphXVertex, int>>;

    [Extension]
    public class IsEulerian : IAlgorithm
    {
        private readonly GraphArea _graphArea;
        private readonly GraphXZoomControl _zoomControl;
        private List<KeyValuePair<GraphXVertex, GraphX.Controls.VertexControl>> _graphVerticesVisualisation;
        private List<GraphXVertex> _graphVertices;
        private int _currentVertexIndex;
        private bool _isEulerian;
        private IsEulerianGraphAlgorithm<GraphXVertex, UndirectedEdge<GraphXVertex>> _algo;
        private ComponentWithEdges _graphProperty;

        public IsEulerian()
        {
            _graphArea = new GraphArea();
            _zoomControl = new GraphXZoomControl { Content = _graphArea };
            Output.Controls.Add(new ElementHost { Dock = DockStyle.Fill, Child = _zoomControl });
        }

        public string Name => "Is Eulerian graph";
        public string Author => "Roman Fedorov";

        public string Description =>
            "This plugin demonstrates how to determinate is graph a Eulerian one. " +
            "Graph is Eulerian if and only if it contains one connected component, and " +
            "each vertex in this component has even count of edges.";

        public Panel Options { get; } = new Panel { Dock = DockStyle.Fill };
        public Panel Output { get; } = new Panel { Dock = DockStyle.Fill };

        public static Func<GraphXVertex, GraphXVertex, IDictionary<string, string>, UndirectedEdge<GraphXVertex>>
            edgeFun = (v1, v2, attrs) => new UndirectedEdge<GraphXVertex>(v1, v2);

        public void Run(string dotSource)
        {
            var vertexFun = VertexFactory.Name;
            
            var graph = UndirectedGraph<GraphXVertex, UndirectedEdge<GraphXVertex>>.LoadDot(dotSource, vertexFun, edgeFun);
            var graphVisualisation = Graph.LoadDot(dotSource, vertexFun, EdgeFactory<GraphXVertex>.Weighted(0));

            if (graph.Vertices.Count() == 0)
            {
                MessageBox.Show("Graph is empty.");
                return;
            }

            _algo = new IsEulerianGraphAlgorithm<GraphXVertex, UndirectedEdge<GraphXVertex>>(graph);
            _graphProperty = _algo.checkComponentsWithEdges();

            switch (_graphProperty)
            {
                case ComponentWithEdges.ManyComponents:
                    {
                        MessageBox.Show($"Graph contains more than one connected component with edges. Graph is not Eulerian");
                        _isEulerian = false;
                        break;
                    }
                case ComponentWithEdges.NoComponent:
                    {
                        MessageBox.Show($"Graph doesn't contain any edges. Graph is not Eulerian");
                        _isEulerian = false;
                        break;
                    }
                case ComponentWithEdges.OneComponentWithOneVertex:
                    {
                        MessageBox.Show($"Graph contains one component with one vertex. Graph is Eulerian");
                        _isEulerian = true;
                        break;
                    }
                case ComponentWithEdges.OneComponentWithManyVertices:
                    {
                        // Check every vertice with NextStep()
                        _isEulerian = true;
                        break;
                    }
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

            if ((_graphProperty == ComponentWithEdges.OneComponentWithManyVertices) && (!_algo.satisfiesEulerianCondition(vertex)))
            {
                _isEulerian = false;
                MessageBox.Show($"Vertex {vertexVisualisation.Key} has odd count of edges. Graph is not Eulerian");
            }

            if (++_currentVertexIndex == _graphVerticesVisualisation.Count)
            {
                MessageBox.Show($"Finished, " + (_isEulerian ? $"graph is Eulerian" : $"graph is not Eulerian"));
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