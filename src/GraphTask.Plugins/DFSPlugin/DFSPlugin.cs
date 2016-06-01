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
using PluginsCommon;
using QuickGraph.Algorithms.Search;
using System;
using System.ComponentModel;
using MessageBox = System.Windows.Forms.MessageBox;
using Point = System.Drawing.Point;

[assembly: Addin]
[assembly: AddinDependency("GraphTasks", "1.0")]

namespace DFSPlugin
{
    using Graph = BidirectionalGraph<GraphXVertex, GraphXTaggedEdge<GraphXVertex, int>>;
    using GraphArea = BidirectionalGraphArea<GraphXVertex, GraphXTaggedEdge<GraphXVertex, int>>;

    [Extension]
    public class DFSPlugin : IAlgorithm
    {
        private readonly GraphArea _graphArea;
        private readonly GraphXZoomControl _zoomControl;
        private bool _hasStarted;
        private bool _hasFinished;
        private Traversal<GraphXVertex> traversal = new Traversal<GraphXVertex>();
        private List<Traversal<GraphXVertex>.Node> states;
        private DepthFirstSearchAlgorithm<GraphXVertex, GraphXTaggedEdge<GraphXVertex, int>> dfs;
        private int currState;

        byte a = 0xFF, r = 0xE3, g = 0xE3, b = 0xE3;

        public DFSPlugin()
        {
            _graphArea = new GraphArea();
            _zoomControl = new GraphXZoomControl { Content = _graphArea };
            Output.Controls.Add(new ElementHost { Dock = DockStyle.Fill, Child = _zoomControl });
        }

        public string Name => "Depth First Search Plugin";
        public string Author => "Anastasia Sulyagina";
        public string Description => "This plugin demonstrates how DFS works.\n";

        public Panel Options { get; } = new Panel { Dock = DockStyle.Fill };
        public Panel Output { get; } = new Panel { Dock = DockStyle.Fill };

        public bool CanGoBack => currState != 0 && states.Count != 0;
        public bool CanGoFurther => _hasStarted && !_hasFinished;

        public void Run(string dotSource)
        {  
            traversal.Clear();
            var vertexFun = VertexFactory.Name;
            var edgeFun = EdgeFactory<GraphXVertex>.Weighted(0);
            var graph = Graph.LoadDot(dotSource, vertexFun, edgeFun);

            states = new List<Traversal<GraphXVertex>.Node>();

            if (!graph.Vertices.Any())
            {
                MessageBox.Show("Graph is empty.");
                return;
            }

            dfs = new DepthFirstSearchAlgorithm<GraphXVertex, GraphXTaggedEdge<GraphXVertex, int>>(graph);

            dfs.DiscoverVertex += OnDiscoverVertex;
            dfs.FinishVertex += OnFinishVertex;

            currState = -1;

            dfs.Compute();

            _graphArea.LogicCore.Graph = graph;
            _graphArea.GenerateGraph();
            _zoomControl.ZoomToFill();

            _hasStarted = true;
            _hasFinished = false;
        }

        private void HighlightTraversal()
        {
            var currNode = states.ElementAt(currState);
            var nodes = (currNode == null) ? traversal.nodes : currNode.children;
            foreach (Traversal<GraphXVertex>.Node node in nodes)
                _graphArea.VertexList[node.v].Background = new SolidColorBrush(Color.FromArgb(a, r, g, b));
            while (currNode != null)
            {
                _graphArea.VertexList[currNode.v].Background = new SolidColorBrush(Colors.YellowGreen);
                currNode = currNode.parent;
            }
        }

        public void NextStep()
        {
            currState++;
            HighlightTraversal();
            _hasFinished = currState == states.Count - 1;
        }

        public void PreviousStep()
        {
            currState--;
            HighlightTraversal();
            _hasFinished = false;
        }

        private void memorizeState()
        {
            states.Add(traversal.currNode);
        }

        private void OnFinishVertex(GraphXVertex vertex)
        {
            traversal.Pop();
            memorizeState();
        }

        private void OnDiscoverVertex(GraphXVertex vertex)
        {
            traversal.Push(vertex);
            memorizeState();
        }
    }
}