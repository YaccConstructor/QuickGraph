using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using System.Windows.Media;
using Common;
using GraphX.PCL.Common.Models;
using Mono.Addins;
using QuickGraph;
using QuickGraph.GraphXAdapter;
using QuickGraph.Algorithms.Search;
using System;
using System.ComponentModel;
using MessageBox = System.Windows.Forms.MessageBox;
using Point = System.Drawing.Point;

[assembly: Addin]
[assembly: AddinDependency("GraphTasks", "1.0")]

namespace BFSPlugin
{
    using Graph = BidirectionalGraph<GraphXVertex, GraphXTaggedEdge<GraphXVertex, int>>;
    using GraphArea = BidirectionalGraphArea<GraphXVertex, GraphXTaggedEdge<GraphXVertex, int>>;

    [Extension]
    public class BFSPlugin : IAlgorithm
    {
        private readonly GraphArea _graphArea;
        private readonly GraphXZoomControl _zoomControl;
        private bool _hasStarted;
        private bool _hasFinished;
        private List<GraphXVertex> states;
        private BreadthFirstSearchAlgorithm<GraphXVertex, GraphXTaggedEdge<GraphXVertex, int>> bfs;
        private int currState;

        byte a = 0xFF, r = 0xE3, g = 0xE3, b = 0xE3;

        public BFSPlugin()
        {
            _graphArea = new GraphArea();
            _zoomControl = new GraphXZoomControl { Content = _graphArea };
            Output.Controls.Add(new ElementHost { Dock = DockStyle.Fill, Child = _zoomControl });
        }

        public string Name => "Breadth First Search Plugin";
        public string Author => "Anastasia Sulyagina";
        public string Description => "This plugin demonstrates how BFS works.\n";

        public Panel Options { get; } = new Panel { Dock = DockStyle.Fill };
        public Panel Output { get; } = new Panel { Dock = DockStyle.Fill };

        public bool CanGoBack => currState != 0 && states.Count != 0;
        public bool CanGoFurther => _hasStarted && !_hasFinished;

        public void Run(string dotSource)
        {
            var vertexFun = VertexFactory.Name;
            var edgeFun = EdgeFactory<GraphXVertex>.Weighted(0);
            var graph = Graph.LoadDot(dotSource, vertexFun, edgeFun);

            states = new List<GraphXVertex>();

            if (!graph.Vertices.Any())
            {
                MessageBox.Show("Graph is empty.");
                return;
            }

            bfs = new BreadthFirstSearchAlgorithm<GraphXVertex, GraphXTaggedEdge<GraphXVertex, int>>(graph);

            bfs.DiscoverVertex += OnDiscoverVertex;

            currState = -1;

            bfs.Compute();

            _graphArea.LogicCore.Graph = graph;
            _graphArea.GenerateGraph();
            _zoomControl.ZoomToFill();

            _hasStarted = true;
            _hasFinished = false;
        }

        private void HighlightTraversal()
        {
            var currNode = states.ElementAt(currState);
            _graphArea.VertexList[currNode].Background = new SolidColorBrush(Colors.YellowGreen);
            if (!_hasFinished)
            {
                var nextNode = states.ElementAt(currState + 1);
                _graphArea.VertexList[nextNode].Background = new SolidColorBrush(Color.FromArgb(a, r, g, b));
            }
        }

        public void NextStep()
        {
            currState++;
            _hasFinished = currState == states.Count - 1;
            HighlightTraversal();
        }

        public void PreviousStep()
        {
            currState--;
            _hasFinished = false;
            HighlightTraversal();
        }

        private void OnDiscoverVertex(GraphXVertex vertex)
        {
            states.Add(vertex);
        }
    }
}