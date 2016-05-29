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
using QuickGraph.Algorithms.ConnectedComponents;


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
        private List<KeyValuePair<GraphXVertex, GraphX.Controls.VertexControl>> _graphVertices;
        private int _currentVertexIndex;
        private bool _isEulerian;

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

        private Tuple<int?, int?> firstAndSecondIndexOfTrue(bool[] data)
        {
            // if no true elements returns (null, null)
            // if only one true element, returns (indexOfTrue, null)
            int? firstIndex = null, secondIndex = null;
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i])
                {
                    if (!firstIndex.HasValue)
                    {
                        firstIndex = i;
                    }
                    else
                    {
                        return new Tuple<int?, int?>(firstIndex, i);
                    }
                }
            }
            return new Tuple<int?, int?>(firstIndex, secondIndex);
        }

        public void Run(string dotSource)
        {
            var vertexFun = VertexFactory.Name;
            
            var graph = UndirectedGraph<GraphXVertex, UndirectedEdge<GraphXVertex>>.LoadDot(dotSource, vertexFun, edgeFun);
            var graphVis = Graph.LoadDot(dotSource, vertexFun, EdgeFactory<GraphXVertex>.Weighted(0));

            _isEulerian = true;

            if (!graph.Vertices.Any())
            {
                MessageBox.Show("Graph is empty.");
                return;
            }

            var componentsAlgo = new ConnectedComponentsAlgorithm<GraphXVertex, UndirectedEdge<GraphXVertex>>(graph);
            componentsAlgo.Compute();

            // Only one component could contain edges
            bool[] hasEdgesInComponent = new bool[componentsAlgo.ComponentCount];
            foreach (var verticeAndComponent in componentsAlgo.Components)
            {
                hasEdgesInComponent[verticeAndComponent.Value] = graph.AdjacentEdges(verticeAndComponent.Key).Count() > 0;
            }
            var t = firstAndSecondIndexOfTrue(hasEdgesInComponent);
            int? firstIndex = t.Item1, secondIndex = t.Item2;
            
            if (!firstIndex.HasValue)
            {
                MessageBox.Show($"Graph doesn't contain any edges. Graph is not Eulerian");
                _isEulerian = false;
            }

            if (secondIndex.HasValue)
            {
                MessageBox.Show($"Graph contains more than one connected component with edges. Graph is not Eulerian");
                _isEulerian = false;
            }

            _graphArea.LogicCore.Graph = graphVis;
            _graphArea.GenerateGraph();
            _zoomControl.ZoomToFill();
            _graphVertices = _graphArea.VertexList.ToList();
            _currentVertexIndex = 0;
        }

        public void NextStep()
        {
            var vertex = _graphVertices.ElementAt(_currentVertexIndex);
            vertex.Value.Background = new SolidColorBrush(Colors.YellowGreen);
            if (_graphArea.LogicCore.Graph.OutEdges(vertex.Key).Count() % 2 == 1)
            {
                _isEulerian = false;
                MessageBox.Show($"Vertex {vertex.Key} has odd count of edges. Graph is not Eulerian");
            }

            if (++_currentVertexIndex == _graphVertices.Count)
            {
                MessageBox.Show($"Finished, " + (_isEulerian ? $"graph is Eulerian" : $"graph is not Eulerian"));
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