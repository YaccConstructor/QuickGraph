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
using QuickGraph.Algorithms;
using MessageBox = System.Windows.Forms.MessageBox;
using Point = System.Drawing.Point;

// Read more about plugin system on GitHub.
// https://github.com/mono/mono-addins/wiki/Architecture-Overview

// GraphX GraphArea works with classes implementing IMutableBidirectionalGraph only.
// Possible undirected graph workaround:
//   UndirectedGraph<TVertex, TEdge>.LoadDot(...).Edges.ToBidirectionalGraph<TVertex, TEdge>();

// Add your plugin reference to MainForm project for auto-rebuild while working on it.
// Please do not commit this reference, as it will force your plugin to be rebuilt when MainForm rebuilds.


[assembly: Addin]
[assembly: AddinDependency("GraphTasks", "1.0")]

namespace KruskalVisualisation
{
    using System;
    using Graph = BidirectionalGraph<GraphXVertex, GraphXTaggedEdge<GraphXVertex, int>>;
    using GraphArea = BidirectionalGraphArea<GraphXVertex, GraphXTaggedEdge<GraphXVertex, int>>;
    using Attributes = IDictionary<string, string>;

    class EdgeForVisualisation
    {
        public GraphXTaggedEdge<GraphXVertex, int> edge;
        public bool isConteins;
        public int number;
        public EdgeForVisualisation(GraphXTaggedEdge<GraphXVertex, int> a, bool b, int c)
        {
            edge = a;
            isConteins = b;
            number = c;
        }
    }

    [Extension]
    public class KruskalVisualisation : IAlgorithm
    {
        private readonly CheckBox _countSymbolsCheckBox;
        private readonly GraphArea _graphArea;
        private readonly GraphXZoomControl _zoomControl;
        //private Stack<List<GraphSerializationData>> _steps;
        private bool _hasStarted;
        private bool _hasFinished;
        private List<EdgeForVisualisation> edges = new List<EdgeForVisualisation>();
        private int i = -1;

        public KruskalVisualisation()
        {
            _countSymbolsCheckBox = new CheckBox {Text = "Count symbols when start", Location = new Point(12, 6)};
            Options.Controls.Add(_countSymbolsCheckBox);

            _graphArea = new GraphArea();
            _zoomControl = new GraphXZoomControl {Content = _graphArea};
            Output.Controls.Add(new ElementHost {Dock = DockStyle.Fill, Child = _zoomControl});
        }

        public string Name => "Kruskal algorithm";
        public string Author => "Alexander Pihtin";

        public string Description =>
            "THIS VISUALISATION FOR KRUSKAL ALGORITHM.\n" +
            "OBEY ME AND PREPARE YOUR GRAPH.\n";

        public Panel Options { get; } = new Panel {Dock = DockStyle.Fill};
        public Panel Output { get; } = new Panel {Dock = DockStyle.Fill};

        public void Run(string dotSource)
        {
            edges.Clear();
            i = -1;
            if (_countSymbolsCheckBox.Checked)
            {
                MessageBox.Show($"Read {dotSource.Length} symbol(s).");
            }

            var vertexFun = VertexFactory.Name;
            var edgeFun = EdgeFactory<GraphXVertex>.Weighted(0);

            var graph = Graph.LoadDot(dotSource, vertexFun, edgeFun);
            var grap = new UndirectedGraph<GraphXVertex, GraphXTaggedEdge<GraphXVertex, int>>();
            foreach (var i in graph.Vertices)
            {
                if (!grap.ContainsVertex(i))
                    grap.AddVertex(i);
            }
            foreach (var i in graph.Edges)
            {
                var z = grap.Edges.FirstOrDefault(x => (x.Source == i.Target) && (x.Target == i.Source));
                if (grap.Edges.FirstOrDefault(x => (x.Source == i.Target) && (x.Target == i.Source)) == null)
                {
                    grap.AddEdge(i);
                }
            }

            if (!graph.Vertices.Any())
            {
                MessageBox.Show("Graph is empty.");
                return;
            }
            var kruskal = new QuickGraph.Algorithms.MinimumSpanningTree.KruskalMinimumSpanningTreeAlgorithm<GraphXVertex, GraphXTaggedEdge<GraphXVertex, int>>(grap, x => x.Tag);
            kruskal.TreeEdge += Kruskal_TreeEdge;
            kruskal.ExamineEdge += Kruskal_ExamineEdge;
            kruskal.Compute();
            _graphArea.LogicCore.Graph = graph;
            _graphArea.GenerateGraph();
            _zoomControl.ZoomToFill();
            foreach (var i in _graphArea.EdgesList)
            {
                //i.Value.Visibility = System.Windows.Visibility.Hidden;
                //i.Value.Visibility = System.Windows.Visibility.Visible;
                i.Value.ShowLabel = true;
                i.Value.Foreground = new SolidColorBrush(Colors.WhiteSmoke);
                i.Value.ShowArrows = false;
            }

            _hasStarted = true;
            _hasFinished = false;
        }

        private void Kruskal_ExamineEdge(GraphXTaggedEdge<GraphXVertex, int> e)
        {
            edges.Add(new EdgeForVisualisation(e, false, edges.Count));
        }

        private void Kruskal_TreeEdge(GraphXTaggedEdge<GraphXVertex, int> e)
        {
            edges.First(x => x.edge == e).isConteins = true;
        }

        public void NextStep()
        {
            i++;
            var edge = _graphArea.EdgesList.First(x => (x.Key.Source == edges[i].edge.Source) && (x.Key.Target == edges[i].edge.Target));
            if (edges[i].isConteins)
            {
                var vertex1 = _graphArea.VertexList.First(x => x.Key == edge.Key.Source);
                var vertex2 = _graphArea.VertexList.First(x => x.Key == edge.Key.Target);
                vertex1.Value.Background = new SolidColorBrush(Colors.Aquamarine);
                vertex2.Value.Background = new SolidColorBrush(Colors.Aquamarine);
                edge.Value.Visibility = System.Windows.Visibility.Visible;
                edge.Value.Foreground = new SolidColorBrush(Colors.Aquamarine);
                edge.Value.ShowLabel = true;
                edge.Value.ShowArrows = false;
            }
            else
            {
                edge.Value.Visibility = System.Windows.Visibility.Visible;
                edge.Value.Foreground = new SolidColorBrush(Colors.BurlyWood);
                edge.Value.ShowLabel = true;
                edge.Value.ShowArrows = false;
            }
            if (!_graphArea.LogicCore.Graph.Vertices.Any())
            {
                _hasFinished = true;
                return;
            }
            _zoomControl.ZoomToFill();
        }

        public void PreviousStep()
        {
            var edge = _graphArea.EdgesList.First(x => (x.Key.Source == edges[i].edge.Source) && (x.Key.Target == edges[i].edge.Target));
            if (edges[i].isConteins)
            {
                var vertex1 = _graphArea.VertexList.First(x => x.Key == edge.Key.Source);
                var vertex2 = _graphArea.VertexList.First(x => x.Key == edge.Key.Target);
                var edge1 = edges.ToList().FirstOrDefault(x => x.edge.Source == vertex1.Key && x.isConteins && x.number < i)?.edge;
                var edge2 = edges.ToList().FirstOrDefault(x => x.edge.Target == vertex1.Key && x.isConteins && x.number < i)?.edge;
                var edge3 = edges.ToList().FirstOrDefault(x => x.edge.Source == vertex2.Key && x.isConteins && x.number < i)?.edge;
                var edge4 = edges.ToList().FirstOrDefault(x => x.edge.Target == vertex2.Key && x.isConteins && x.number < i)?.edge;
                if ((edge1 == null) && (edge2 == null))
                    vertex1.Value.Background = new SolidColorBrush(Color.FromArgb(255, 227, 227, 227));
                if ((edge3 == null) && (edge4 == null))
                    vertex2.Value.Background = new SolidColorBrush(Color.FromArgb(255, 227, 227, 227));
                edge.Value.Foreground = new SolidColorBrush(Colors.WhiteSmoke);
                //edge.Value.ShowLabel = false;
                //edge.Value.Visibility = System.Windows.Visibility.Hidden;
            }
            else
            {
                edge.Value.Foreground = new SolidColorBrush(Colors.WhiteSmoke);
                //edge.Value.ShowLabel = false;
                //edge.Value.Visibility = System.Windows.Visibility.Hidden;
            }
            i--;
            _zoomControl.ZoomToFill();

            _hasFinished = false;
        }

        public bool CanGoBack => i != -1;
        public bool CanGoFurther => i != edges.Count-1;
    }
}