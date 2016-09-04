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

namespace BellmanFordShortestPathPlugin
{
    using QuickGraph.Algorithms.ShortestPath;
    using Graph = BidirectionalGraph<GraphXVertex, GraphXTaggedEdge<GraphXVertex, int>>;
    using GraphArea = BidirectionalGraphArea<GraphXVertex, GraphXTaggedEdge<GraphXVertex, int>>;

    [Extension]
    public class BellmanFordShortestPathPlugin : IAlgorithm
    {
        private readonly GraphArea _graphArea;
        private readonly GraphXZoomControl _zoomControl;
        private bool _hasStarted;
        private bool _hasFinished;
        private Label listVBoxName;
        private Label listEBoxName;
        private ListBox algOrderListBox;
        private ListBox algEdgesListBox;
        private readonly TextBox startVertex;
        private Label sLabel;
        private BindingList<String> algEdgesList;
        private BindingList<String> algOrderList;
        private List<EdgeState> estates;
        private List<Color> colors;
        private BellmanFordShortestPathAlgorithm<GraphXVertex, GraphXTaggedEdge<GraphXVertex, int>> bellman;
        private int currState;
        private String distances;

        byte a = 0xFF, r = 0xE3, g = 0xE3, b = 0xE3;

        private class EdgeState
        {
            public EdgeState(GraphXTaggedEdge<GraphXVertex, int> e, SolidColorBrush c)
            {
                this.e = e;
                this.v = e.Target;
                this.c = c;
            }
            public GraphXTaggedEdge<GraphXVertex, int> e;
            public GraphXVertex v;
            public SolidColorBrush c;
        }



        public BellmanFordShortestPathPlugin()
        {

            _graphArea = new GraphArea();
            _zoomControl = new GraphXZoomControl { Content = _graphArea };
            Output.Controls.Add(new ElementHost { Dock = DockStyle.Fill, Child = _zoomControl });

            sLabel = new Label { Location = new Point(200, 0), Text = "Start vertex:" };
            listVBoxName = new Label { Text = "Vertices:", Location = new Point(10, 0), Width = 50 };
            listEBoxName = new Label { Text = "Edges:", Location = new Point(100, 0), Width = 50 };
            startVertex = new TextBox { Location = new Point(200, 25) };
            algOrderListBox = new ListBox { Location = new Point(10, 25), Width = 50, Height = 80 };
            algEdgesListBox = new ListBox { Location = new Point(100, 25), Width = 50, Height = 80 };

            algOrderList = new BindingList<String>();
            algEdgesList = new BindingList<String>();
            algOrderListBox.DataSource = algOrderList;
            algEdgesListBox.DataSource = algEdgesList;

            Options.Controls.AddRange(new Control[] { algOrderListBox, listVBoxName, algEdgesListBox, listEBoxName, sLabel, startVertex });
        }

        public string Name => "Bellman Ford algorithm Plugin";
        public string Author => "Alisa Meteleva";
        public string Description => "This plugin demonstrates how Bellman Ford alg works.\n";

        public Panel Options { get; } = new Panel { Dock = DockStyle.Fill };
        public Panel Output { get; } = new Panel { Dock = DockStyle.Fill };

        public bool CanGoBack => currState != 0 && estates.Count != 0;
        public bool CanGoFurther => _hasStarted && !_hasFinished;

        public void Run(string dotSource)
        {

            if (startVertex.Text == "")
            {
                MessageBox.Show("Please specify start vertix first!\n");
                return;
            }

            algOrderList.Clear();
            algEdgesList.Clear();

            var vertexFun = VertexFactory.Name;
            var edgeFun = EdgeFactory<GraphXVertex>.Weighted(0);
            var graph = Graph.LoadDot(dotSource, vertexFun, edgeFun);
            distances = "";
            estates = new List<EdgeState>();
            colors = new List<Color> { Colors.LightGreen, Colors.Lime, Colors.DarkGreen, Colors.DarkOliveGreen };

            if (graph.IsVerticesEmpty)
            {
                MessageBox.Show("Graph is empty.");
                return;
            }


            bellman = new BellmanFordShortestPathAlgorithm<GraphXVertex, GraphXTaggedEdge<GraphXVertex, int>>(graph, x => x.Tag);
            bellman.ExamineEdge += OnExamineEdge;


            var sVertix = graph.Vertices.Single(x => x.ToString() == startVertex.Text);
            bellman.Compute(sVertix);
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

            foreach (var v in bellman.VisitedGraph.Vertices)
            {
                distances += "Shortest distance for vertex" + v.ToString() + " equals " + bellman.Distances[v].ToString() + "\n";
            }

        }

        private void HighlightTraversal()
        {
            if (currState == 0)
            {
                _graphArea.VertexList[estates[currState].e.Source].Background = new SolidColorBrush(Colors.Cyan);
            }


            var currColorE = estates[currState].c.Color;
            var currEdge = estates[currState];

            var timeEdge = algEdgesList.Count(x => x == EdgeToString(currEdge.e));
            if (timeEdge != 0)
            {
                estates[currState].c = new SolidColorBrush(colors[timeEdge % colors.Count]);
            }
            _graphArea.EdgesList[currEdge.e].Foreground = estates[currState].c;
            _graphArea.VertexList[currEdge.v].Background = estates[currState].c;



        }

        public void NextStep()
        {

            currState++;

            var toadd = estates[currState].v.ToString();

            HighlightTraversal();

            algOrderList.Insert(currState, toadd);
            algEdgesList.Insert(currState, EdgeToString(estates[currState].e));

            _hasFinished = currState == estates.Count - 1;
            if (_hasFinished)
            {
                MessageBox.Show(distances);
                startVertex.Text = "";
            }

        }

        public void PreviousStep()
        {
            _hasFinished = false;

            algOrderList.RemoveAt(currState);

            var color = Color.FromArgb(a, r, g, b);
            var currEdge = estates[currState];
            var timeEdge = algEdgesList.Count(x => x == EdgeToString(currEdge.e));

            if (!currEdge.c.Color.Equals(Colors.YellowGreen))
            {
                color = colors[(timeEdge - 2) % colors.Count];
            }

            _graphArea.EdgesList[currEdge.e].Foreground = new SolidColorBrush(color);
            _graphArea.VertexList[currEdge.v].Background = new SolidColorBrush(color);
            algEdgesList.RemoveAt(currState);

            currState--;
        }

        private void OnExamineEdge(GraphXTaggedEdge<GraphXVertex, int> edge)
        {
            estates.Add(new EdgeState(edge, new SolidColorBrush(Colors.YellowGreen)));
        }

        private String EdgeToString(GraphXTaggedEdge<GraphXVertex, int> edge)
        {
            return edge.Source.ToString() + edge.Target.ToString();
        }

    }
}