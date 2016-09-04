using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using System.Windows.Media;
using Common;
using Mono.Addins;
using QuickGraph;
using QuickGraph.GraphXAdapter;
using System;
using System.ComponentModel;
using MessageBox = System.Windows.Forms.MessageBox;
using Point = System.Drawing.Point;

[assembly: Addin]
[assembly: AddinDependency("GraphTasks", "1.0")]

namespace AStarShortestPathPlugin
{
    using QuickGraph.Algorithms.ShortestPath;
    using Graph = BidirectionalGraph<GraphXVertex, GraphXTaggedEdge<GraphXVertex, int>>;
    using GraphArea = BidirectionalGraphArea<GraphXVertex, GraphXTaggedEdge<GraphXVertex, int>>;

    [Extension]
    public class AStarShortestPathPlugin : IAlgorithm
    {
        private readonly GraphArea _graphArea;
        private readonly GraphXZoomControl _zoomControl;
        private bool _hasStarted;
        private bool _hasFinished;
        private readonly TextBox _startVertex;
        private Label listBoxName;
        private ListBox algOrderListBox;
        private BindingList<String> algOrderList;
        private List<State> states;
        private AStarShortestPathAlgorithm<GraphXVertex, GraphXTaggedEdge<GraphXVertex, int>> aStar;
        private int currState;
        private String distances;

        byte a = 0xFF, r = 0xE3, g = 0xE3, b = 0xE3;

        private class State
        {
            public State(GraphXVertex v, SolidColorBrush c)
            {
                this.v = v;
                this.c = c;
            }
            public GraphXVertex v;
            public SolidColorBrush c;
        }


        public AStarShortestPathPlugin()
        {
            _graphArea = new GraphArea();
            _zoomControl = new GraphXZoomControl { Content = _graphArea };
            _startVertex = new TextBox { Location = new Point(220, 20) };

            Output.Controls.Add(new ElementHost { Dock = DockStyle.Fill, Child = _zoomControl });
            var sLabel = new Label { Location = new Point(225, 0), Text = "Start vertex:" };
            listBoxName = new Label { Text = "Order of chosen vertices:", Location = new Point(10, 0) };
            algOrderListBox = new ListBox { Location = new Point(10, 20), Width = 160, Height = 80 };
            algOrderList = new BindingList<String>();
            algOrderListBox.DataSource = algOrderList;
            Options.Controls.AddRange(new Control[] { _startVertex, sLabel, algOrderListBox, listBoxName });

        }

        public string Name => "A * algorithm Plugin";
        public string Author => "Alisa Meteleva";
        public string Description => "This plugin demonstrates how A* alg works.\n";

        public Panel Options { get; } = new Panel { Dock = DockStyle.Fill };
        public Panel Output { get; } = new Panel { Dock = DockStyle.Fill };

        public bool CanGoBack => currState != 0 && states.Count != 0;
        public bool CanGoFurther => _hasStarted && !_hasFinished;

        public void Run(string dotSource)
        {

            if (_startVertex.Text == "")
            {
                MessageBox.Show("Please specify start vertix first!\n");
                return;
            }

            var vertexFun = VertexFactory.Name;
            var edgeFun = EdgeFactory<GraphXVertex>.Weighted(0);
            var graph = Graph.LoadDot(dotSource, vertexFun, edgeFun);
            var grpah2 = graph.ToArrayAdjacencyGraph();
            var startVertix = graph.Vertices.Single(x => x.ToString() == _startVertex.Text);
            states = new List<State>();
            distances = "";



            if (graph.IsVerticesEmpty)
            {
                MessageBox.Show("Graph is empty.");
                return;
            }

            aStar = new AStarShortestPathAlgorithm<GraphXVertex, GraphXTaggedEdge<GraphXVertex, int>>(graph, e => e.Tag, v => 0);
            aStar.DiscoverVertex += OnDiscoverVertex;
            aStar.FinishVertex += OnStartVertex;
            aStar.Compute(startVertix);

            currState = -1;

            _graphArea.LogicCore.Graph = graph;
            _graphArea.GenerateGraph();
            _zoomControl.ZoomToFill();

            foreach (var e in _graphArea.EdgesList)
            {
                e.Value.ShowLabel = true;
            }

            _hasStarted = true;
            _hasFinished = false;
            foreach (var v in aStar.VisitedGraph.Vertices)
            {
                distances += "Shortest distance for vertix" + v.ToString() + " equals " + aStar.Distances[v].ToString() + "\n";
            }
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
            var toadd = states[currState].v.ToString();
            var shortDistance = aStar.Distances[states[currState].v];

            HighlightTraversal();

            if (shortDistance == double.MaxValue)
            {
                toadd += " Unreachable from start vertex ";
                _graphArea.VertexList[states[currState].v].Background = new SolidColorBrush(Colors.DarkSlateBlue);
            }
            else
                 if (states[currState].c.Color.Equals(Colors.Magenta))
            {
                toadd += "  shortest path is   " + shortDistance;
            }

            algOrderList.Insert(currState, toadd);
            if (_hasFinished)
            {
                MessageBox.Show(distances);
                _startVertex.Text = "";
            }

        }

        public void PreviousStep()
        {
            _hasFinished = false;
            var currNode = states[currState];

            if (currNode.c.Color.Equals(Colors.Magenta))
            {
                _graphArea.VertexList[currNode.v].Background = new SolidColorBrush(Colors.YellowGreen);
            }
            else
            {
                _graphArea.VertexList[currNode.v].Background = new SolidColorBrush(Color.FromArgb(a, r, g, b));
            }

            algOrderList.RemoveAt(currState);
            currState--;
        }

        private void OnDiscoverVertex(GraphXVertex vertex)
        {
            states.Add(new State(vertex, new SolidColorBrush(Colors.YellowGreen)));
        }

        private void OnStartVertex(GraphXVertex vertex)
        {
            states.Add(new State(vertex, new SolidColorBrush(Colors.Magenta)));
        }
    }
}