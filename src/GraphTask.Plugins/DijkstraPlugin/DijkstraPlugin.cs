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

namespace DijkstraPlugin
{
    using QuickGraph.Algorithms.ShortestPath;
    using Graph = BidirectionalGraph<GraphXVertex, GraphXTaggedEdge<GraphXVertex, int>>;
    using GraphArea = BidirectionalGraphArea<GraphXVertex, GraphXTaggedEdge<GraphXVertex, int>>;

    [Extension]
    public class DijkstraPlugin : IAlgorithm
    {
        private readonly GraphArea _graphArea;
        private readonly GraphXZoomControl _zoomControl;
        private bool _hasStarted;
        private bool _hasFinished;
        private List<State> states;
        private DijkstraShortestPathAlgorithm<GraphXVertex, GraphXTaggedEdge<GraphXVertex, int>> dijkstra;
        private int currState;

        byte a = 0xFF, r = 0xE3, g = 0xE3, b = 0xE3;

        private class State {
            public State(GraphXVertex v, SolidColorBrush c)
            {
                this.v = v;
                this.c = c;
            }
            public GraphXVertex v;
            public SolidColorBrush c;
        }


        public DijkstraPlugin()
        {
            _graphArea = new GraphArea();
            _zoomControl = new GraphXZoomControl { Content = _graphArea };
            Output.Controls.Add(new ElementHost { Dock = DockStyle.Fill, Child = _zoomControl });
        }

        public string Name => "Dijkstra algorithm Plugin";
        public string Author => "Anastasia Sulyagina";
        public string Description => "This plugin demonstrates how Dijkstra alg works.\n";

        public Panel Options { get; } = new Panel { Dock = DockStyle.Fill };
        public Panel Output { get; } = new Panel { Dock = DockStyle.Fill };

        public bool CanGoBack => currState != 0 && states.Count != 0;
        public bool CanGoFurther => _hasStarted && !_hasFinished;

        public void Run(string dotSource)
        {
            var vertexFun = VertexFactory.Name;
            var edgeFun = EdgeFactory<GraphXVertex>.Weighted(0);
            var graph = Graph.LoadDot(dotSource, vertexFun, edgeFun);

            states = new List<State>();

            if (graph.IsVerticesEmpty)
            {
                MessageBox.Show("Graph is empty.");
                return;
            }

            dijkstra = new DijkstraShortestPathAlgorithm<GraphXVertex, GraphXTaggedEdge<GraphXVertex, int>>(graph, x=>x.Tag);

            dijkstra.DiscoverVertex += OnDiscoverVertex;
            dijkstra.FinishVertex += OnStartVertex;

            currState = -1;

            dijkstra.Compute();

            _graphArea.LogicCore.Graph = graph;
            _graphArea.GenerateGraph();
            _zoomControl.ZoomToFill();
            foreach (var e in _graphArea.EdgesList)
            {
                e.Value.ShowLabel = true;
            }
            _hasStarted = true;
            _hasFinished = false;
        }

        private void HighlightTraversal()
        {
            var currNode = states[currState].v;
            _graphArea.VertexList[currNode].Background = states[currState].c;

        }

        public void NextStep()
        {
            currState++;
            _hasFinished = currState == states.Count - 1;

            HighlightTraversal();
        }

        public void PreviousStep()
        {
            _hasFinished = false;
            var currNode = states[currState];
            if (currNode.c.Color.Equals(Colors.DarkTurquoise))
            {
                _graphArea.VertexList[currNode.v].Background = new SolidColorBrush(Colors.YellowGreen);
            }
            else
            {
                _graphArea.VertexList[currNode.v].Background = new SolidColorBrush(Color.FromArgb(a, r, g, b));
            }
            currState--;
        }

        private void OnDiscoverVertex(GraphXVertex vertex)
        {
            states.Add(new State(vertex, new SolidColorBrush(Colors.YellowGreen)));
        }

        private void OnStartVertex(GraphXVertex vertex)
        {
            states.Add(new State(vertex, new SolidColorBrush(Colors.DarkTurquoise)));
        }
    }
}