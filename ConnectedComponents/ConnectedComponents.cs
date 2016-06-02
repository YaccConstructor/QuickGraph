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
using MessageBox = System.Windows.Forms.MessageBox;
using Point = System.Drawing.Point;
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

namespace ConnectedComponents
{
    using HelperForKruskalAndPrimVisualisation;
    using QuickGraph.Algorithms.ConnectedComponents;
    using QuickGraph.Algorithms.Search;
    using QuickGraph.Algorithms.TopologicalSort;
    using Graph = BidirectionalGraph<GraphXVertex, GraphXTaggedEdge<GraphXVertex, int>>;
    using GraphArea = BidirectionalGraphArea<GraphXVertex, GraphXTaggedEdge<GraphXVertex, int>>;

    [Extension]
    public class ConnectedComponents : IAlgorithm
    {
        private List<GraphXTaggedEdge<GraphXVertex, int>> edges = new List<GraphXTaggedEdge<GraphXVertex, int>>();
        private List<GraphXVertex> vertex = new List<GraphXVertex>();
        private readonly CheckBox _countSymbolsCheckBox;
        private readonly GraphArea graphArea;
        private readonly GraphXZoomControl _zoomControl;
        private Stack<List<GraphSerializationData>> _steps;
        private bool _hasStarted;
        private bool _hasFinished;
        private int currState;
        private int componentNum;
        private int[] diffBySteps = new int[100];
        private int step;
        private GraphXVertex[] vertices = new GraphXVertex[100];

        public ConnectedComponents()
        {
            _countSymbolsCheckBox = new CheckBox { Text = "Count symbols when start", Location = new Point(12, 6) };
            Options.Controls.Add(_countSymbolsCheckBox);

            graphArea = new GraphArea();
            _zoomControl = new GraphXZoomControl { Content = graphArea };
            Output.Controls.Add(new ElementHost { Dock = DockStyle.Fill, Child = _zoomControl });
        }

        public string Name => "Connected Components";
        public string Author => "Kseniya Gonta";

        public string Description =>
            "This plugin demonstrates how to find connected components.\n";

        public Panel Options { get; } = new Panel { Dock = DockStyle.Fill };
        public Panel Output { get; } = new Panel { Dock = DockStyle.Fill };



        public void Run(string dotSource)
        {
            if (_countSymbolsCheckBox.Checked)
            {
                MessageBox.Show($"Read {dotSource.Length} symbol(s).");
            }

            var vertexFun = VertexFactory.Name;
            var edgeFun = EdgeFactory<GraphXVertex>.Weighted(0);
            var graph = Graph.LoadDot(dotSource, vertexFun, edgeFun);

            _steps = new Stack<List<GraphSerializationData>>();

            if (!graph.Vertices.Any())
            {
                MessageBox.Show("Graph is empty.");
                return;
            }
            var undirected = UndirectedGraphFactory.GetUnderectedGraphFromDot(dotSource);
            var dfsUndir = new UndirectedDepthFirstSearchAlgorithm<GraphXVertex, GraphXTaggedEdge<GraphXVertex, int>>(undirected);
            currState = -1;
            componentNum = 0;
            step = 0;

            dfsUndir.StartVertex += new VertexAction<GraphXVertex>(this.StartVertex);
            dfsUndir.DiscoverVertex += new VertexAction<GraphXVertex>(this.DiscoverVertex);
            dfsUndir.Compute();

            ++this.componentNum;


            graphArea.LogicCore.Graph = graph;
            graphArea.GenerateGraph();
            _zoomControl.ZoomToFill();

            _hasStarted = true;
            _hasFinished = false;
        }


        public void NextStep()
        {
            currState++;
            HighlightNextStep();
            if (!graphArea.LogicCore.Graph.Vertices.Any())
            {
                _hasFinished = true;
                return;
            }
        }

        public void PreviousStep()
        {
            
            HighlightPrevStep();
            currState--;
            _hasFinished = false;
        }

        private void HighlightNextStep()
        {
            foreach (KeyValuePair<GraphXVertex, GraphX.Controls.VertexControl> pair in graphArea.VertexList)
            {
                if (pair.Key.Text == vertices[currState].Text)
                {
                    if (diffBySteps[currState] == 1)
                    {
                        pair.Value.Background = new SolidColorBrush(Colors.Green);
                    }
                    else if (diffBySteps[currState] == 2)
                    {
                        pair.Value.Background = new SolidColorBrush(Colors.Red);
                    }
                    else if (diffBySteps[currState] == 3)
                    {
                        pair.Value.Background = new SolidColorBrush(Colors.Yellow);
                    }
                    else if (diffBySteps[currState] == 4)
                    {
                        pair.Value.Background = new SolidColorBrush(Colors.Pink);
                    }
                    else if (diffBySteps[currState] == 5)
                    {
                        pair.Value.Background = new SolidColorBrush(Colors.PowderBlue);
                    }
                    else if (diffBySteps[currState] == 6)
                    {
                        pair.Value.Background = new SolidColorBrush(Colors.RosyBrown);
                    }
                    else if (diffBySteps[currState] == 7)
                    {
                        pair.Value.Background = new SolidColorBrush(Colors.Salmon);
                    }
                    else if (diffBySteps[currState] == 8)
                    {
                        pair.Value.Background = new SolidColorBrush(Colors.Sienna);
                    }
                }
            }
        }

        private void HighlightPrevStep()
        {
            foreach (KeyValuePair<GraphXVertex, GraphX.Controls.VertexControl> pair in graphArea.VertexList)
            {
                if (pair.Key.Text == vertices[currState].Text)
                {
                    pair.Value.Background = new SolidColorBrush(Colors.LightGray);
                }
            }
        }
        private void StartVertex(GraphXVertex v)
        {
            ++this.componentNum;
        }

        private void DiscoverVertex(GraphXVertex v)
        {
            diffBySteps[step] = componentNum;
            vertices[step] = v;
            step++;
        }
        public bool CanGoBack => currState >= 0;
        public bool CanGoFurther => currState < step - 1;
    }
}