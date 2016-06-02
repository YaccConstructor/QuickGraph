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

namespace StronglyConnectedComponents
{
    using HelperForKruskalAndPrimVisualisation;
    using QuickGraph.Algorithms.ConnectedComponents;
    using QuickGraph.Algorithms.Search;
    using QuickGraph.Algorithms.TopologicalSort;
    using Graph = BidirectionalGraph<GraphXVertex, GraphXTaggedEdge<GraphXVertex, int>>;
    using GraphArea = BidirectionalGraphArea<GraphXVertex, GraphXTaggedEdge<GraphXVertex, int>>;

    [Extension]
    public class StronglyConnectedComponents : IAlgorithm
    {
        private List<GraphXTaggedEdge<GraphXVertex, int>> edges = new List<GraphXTaggedEdge<GraphXVertex, int>>();
        private List<GraphXVertex> vertex = new List<GraphXVertex>();
        private readonly CheckBox _countSymbolsCheckBox;
        private readonly GraphArea graphArea;
        private readonly GraphXZoomControl _zoomControl;
        private Stack<List<GraphSerializationData>> _steps;
        private StronglyConnectedComponentsAlgorithm<GraphXVertex, GraphXTaggedEdge<GraphXVertex, int>> notDFS;
        private int[] diffBySteps = new int[100];
        private int step;
        private GraphXVertex[] vertices = new GraphXVertex[100];
        private int steps;

        public StronglyConnectedComponents()
        {
            _countSymbolsCheckBox = new CheckBox { Text = "Count symbols when start", Location = new Point(12, 6) };
            Options.Controls.Add(_countSymbolsCheckBox);

            graphArea = new GraphArea();
            _zoomControl = new GraphXZoomControl { Content = graphArea };
            Output.Controls.Add(new ElementHost { Dock = DockStyle.Fill, Child = _zoomControl });
        }

        public string Name => "Strongly Connected Components";
        public string Author => "Kseniya Gonta";

        public string Description =>
            "This plugin demonstrates how to find strongly connected components.\n";

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
            notDFS = new StronglyConnectedComponentsAlgorithm<GraphXVertex, GraphXTaggedEdge<GraphXVertex, int>>(graph);
            notDFS.Compute();
            step = 0;
            steps = notDFS.Steps;
            diffBySteps = notDFS.DiffBySteps;
            vertices = notDFS.Vertices; 

            var amountOfComponents = notDFS.ComponentCount;
            var someComponents = notDFS.Components;

            graphArea.LogicCore.Graph = graph;
            graphArea.GenerateGraph();
            _zoomControl.ZoomToFill();
        }


        public void NextStep()
        {
            
            HighlightNextStep();
            if (!graphArea.LogicCore.Graph.Vertices.Any())
            {
                return;
            }
            step++;
        }

        public void PreviousStep()
        {
            HighlightPrevStep();
            step--;
        }

        private void HighlightNextStep()
        {
            foreach (KeyValuePair<GraphXVertex, GraphX.Controls.VertexControl> pair in graphArea.VertexList)
            {
                if (pair.Key.Text == notDFS.Vertices[step].Text)
                {
                    if (diffBySteps[step] == 1)
                    {
                        pair.Value.Background = new SolidColorBrush(Colors.Green);
                    }
                    else if (diffBySteps[step] == 2)
                    {
                        pair.Value.Background = new SolidColorBrush(Colors.Red);
                    }
                    else if (diffBySteps[step] == 3)
                    {
                        pair.Value.Background = new SolidColorBrush(Colors.Yellow);
                    }
                    else if (diffBySteps[step] == 4)
                    {
                        pair.Value.Background = new SolidColorBrush(Colors.Pink);
                    }
                    else if (diffBySteps[step] == 5)
                    {
                        pair.Value.Background = new SolidColorBrush(Colors.PowderBlue);
                    }
                    else if (diffBySteps[step] == 6)
                    {
                        pair.Value.Background = new SolidColorBrush(Colors.RosyBrown);
                    }
                    else if (diffBySteps[step] == 0)
                    {
                        pair.Value.Background = new SolidColorBrush(Colors.Salmon);
                    }
                    else if (diffBySteps[step] == 8)
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
                if (pair.Key.Text == notDFS.Vertices[step].Text)
                {
                    pair.Value.Background = new SolidColorBrush(Colors.LightGray);
                }
            }
        }

        public bool CanGoBack => step > 0;
        public bool CanGoFurther => step < steps;
    }
}