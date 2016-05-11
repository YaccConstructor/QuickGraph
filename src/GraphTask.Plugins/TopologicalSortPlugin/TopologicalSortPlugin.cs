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

namespace TopologicalSortPlugin
{
    using QuickGraph.Algorithms.TopologicalSort;
    using System;
    using System.ComponentModel;
    using Graph = BidirectionalGraph<GraphXVertex, GraphXTaggedEdge<GraphXVertex, int>>;
    using GraphArea = BidirectionalGraphArea<GraphXVertex, GraphXTaggedEdge<GraphXVertex, int>>;

    [Extension]
    public class TopologicalSortPlugin : IAlgorithm
    {
        private ListBox sortedListBox;
        private Label listBoxName;
        private readonly GraphArea _graphArea;
        private readonly GraphXZoomControl _zoomControl;
        private bool _hasStarted;
        private bool _hasFinished;

        private BindingList<String> sortedList;
        private Traversal traversal = new Traversal();
        private LinkedList<Tuple<Traversal.Node, int>> states;
        private TopologicalSortAlgorithm<GraphXVertex, GraphXTaggedEdge<GraphXVertex, int>> sort;
        private int currState;

        public TopologicalSortPlugin()
        {
            listBoxName = new Label { Text = "Sorted order:", Location = new Point(0, 0) };
            sortedListBox = new ListBox { Location = new Point(65, 0) };
            sortedList = new BindingList<String>();
            sortedListBox.DataSource = sortedList;
            Options.Controls.Add(sortedListBox);
            Options.Controls.Add(listBoxName);

            _graphArea = new GraphArea();
            _zoomControl = new GraphXZoomControl { Content = _graphArea };
            Output.Controls.Add(new ElementHost { Dock = DockStyle.Fill, Child = _zoomControl });
        }

        public string Name => "Topological Sort Plugin";
        public string Author => "Artem Gorokhov";
        public string Description => "This plugin demonstrates how Topological Sort works.\n";

        public Panel Options { get; } = new Panel { Dock = DockStyle.Fill };
        public Panel Output { get; } = new Panel { Dock = DockStyle.Fill };

        public void Run(string dotSource)
        {
            sortedList.Clear();
            var vertexFun = VertexFactory.Name;
            var edgeFun = EdgeFactory<GraphXVertex>.Weighted(0);
            var graph = Graph.LoadDot(dotSource, vertexFun, edgeFun);

            states = new LinkedList<Tuple<Traversal.Node, int>>();

            if (!graph.Vertices.Any())
            {
                MessageBox.Show("Graph is empty.");
                return;
            }

            sort = new TopologicalSortAlgorithm<GraphXVertex, GraphXTaggedEdge<GraphXVertex, int>>(graph);

            sort.DiscoverVertex += OnDiscoverVertex;
            sort.FinishVertex += OnFinishVertex;

            currState = -1;

            sort.Compute();

            _graphArea.LogicCore.Graph = graph;
            _graphArea.GenerateGraph();
            _zoomControl.ZoomToFill();

            _hasStarted = true;
            _hasFinished = false;

            /*
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            */
        }

        private void HighlightTraversal()
        {
            var currNode = states.ElementAt(currState).Item1;
            if (currNode != null)
            {
                foreach (Traversal.Node node in currNode.children)
                    _graphArea.VertexList[node.v].Background = new SolidColorBrush(Colors.LightGray);
                while (currNode != null)
                {
                    _graphArea.VertexList[currNode.v].Background = new SolidColorBrush(Colors.YellowGreen);
                    currNode = currNode.parent;
                }
            }
            
        }

        private void UpdateSortedList()
        {
            var sortedListCount = states.ElementAt(currState).Item2;
            
            while (sortedList.Count > sortedListCount)
            {
                sortedList.RemoveAt(0);
            }
            while (sortedList.Count < sortedListCount)
            {
                var sortVerticesIndex = sort.SortedVertices.Count - sortedList.Count - 1;
                sortedList.Insert(0, sort.SortedVertices[sortVerticesIndex].ToString());
            }

        }
        private void DeleteFromSortedList()
        {
            sortedList.RemoveAt(sortedList.Count - 1);
        }

        public void NextStep()
        {
            currState++;
            UpdateSortedList();
            HighlightTraversal();
            if (currState == states.Count - 1)
                _hasFinished = true;
        }

        public void PreviousStep()
        {
            currState--;
            UpdateSortedList();
            HighlightTraversal();
            _hasFinished = false;
        }
        private void memorizeState()
        {
            states.AddLast(new Tuple<Traversal.Node, int>(traversal.currNode, sort.SortedVertices.Count()));
        }
        private void OnFinishVertex(GraphXVertex vertex)
        {
            try
            {
                traversal.Pop();
                memorizeState();
            }
            catch (Exception e)
            {
                string s = e.ToString();
            }
        }
        private void OnDiscoverVertex(GraphXVertex vertex)
        {
            try
            {
                traversal.Push(vertex);
                memorizeState();
                
            }
            catch (Exception e)
            {
                string s = e.ToString();
            }

        }

        public bool CanGoBack => currState != 0 && states.Count != 0;
        public bool CanGoFurther => _hasStarted && !_hasFinished;
    }
}
/*using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using Common;
using GraphX.Controls;
using GraphX.Controls.Models;
using GraphX.PCL.Common.Enums;
using GraphX.PCL.Logic.Algorithms.OverlapRemoval;
using GraphX.PCL.Logic.Models;
using Mono.Addins;
using QuickGraph;
using QuickGraph.GraphXAdapter;
using QuickGraph.Algorithms.TopologicalSort;
using MessageBox = System.Windows.Forms.MessageBox;
using Point = System.Drawing.Point;

[assembly: Addin]
[assembly: AddinDependency("GraphTasks", "1.0")]

namespace TopologicalSortPlugin
{
    using Graph = BidirectionalGraph<GraphXVertex, GraphXTaggedEdge<GraphXVertex, int>>;

    [Extension]
    public class TopologicalSortPlugin : IAlgorithm
    {
        private readonly CheckBox _countSymbolsCheckBox;
        private readonly ElementHost _wpfHost;
        private readonly GraphArea<GraphXVertex, GraphXTaggedEdge<GraphXVertex, int>, Graph> _graphArea;
        private readonly GXLogicCore<GraphXVertex, GraphXTaggedEdge<GraphXVertex, int>, Graph> _logic;
        private readonly ZoomControl _zoomControl;
        private Graph graph;
        private bool hasStarted;
        private bool hasFinished;

        private Traversal traversal = new Traversal();
        private LinkedList<Tuple<Traversal.Node, int>> states;
        private TopologicalSortAlgorithm<GraphXVertex, GraphXTaggedEdge<GraphXVertex, int>> sort;
        private int currState;

        public TopologicalSortPlugin()
        {
            /*_countSymbolsCheckBox = new CheckBox
            {
                Text = "Count symbols",
                Location = new Point(12, 20)
            };
            */
/*
// GraphX integration
_logic = new GXLogicCore<GraphXVertex, GraphXTaggedEdge<GraphXVertex, int>, Graph>
            {
                DefaultLayoutAlgorithm = LayoutAlgorithmTypeEnum.LinLog,
                DefaultOverlapRemovalAlgorithm = OverlapRemovalAlgorithmTypeEnum.FSA,
                DefaultEdgeRoutingAlgorithm = EdgeRoutingAlgorithmTypeEnum.None,
                AsyncAlgorithmCompute = false

            };
            _graphArea = new GraphArea<GraphXVertex, GraphXTaggedEdge<GraphXVertex, int>, Graph>
            {
                EnableWinFormsHostingMode = true,
                LogicCore = _logic,
                EdgeLabelFactory = new DefaultEdgelabelFactory(),
            };

            _zoomControl = new ZoomControl
            {
                Content = _graphArea,
                Visibility = Visibility.Visible,
            };
            ZoomControl.SetViewFinderVisibility(_zoomControl, Visibility.Visible);

            _wpfHost = new ElementHost()
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Child = _zoomControl
            };

            _graphArea.ShowAllEdgesLabels();
            _logic.DefaultLayoutAlgorithmParams = _logic.AlgorithmFactory.CreateLayoutParameters(LayoutAlgorithmTypeEnum.LinLog);
            _logic.DefaultOverlapRemovalAlgorithmParams = _logic.AlgorithmFactory.CreateOverlapRemovalParameters(OverlapRemovalAlgorithmTypeEnum.FSA);
            ((OverlapRemovalParameters)_logic.DefaultOverlapRemovalAlgorithmParams).HorizontalGap = 50;
            ((OverlapRemovalParameters)_logic.DefaultOverlapRemovalAlgorithmParams).VerticalGap = 50;

            MessageBox.Show(Directory.GetCurrentDirectory());
            MessageBox.Show(new Uri(Directory.GetCurrentDirectory()).ToString());

            var templatePath = Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + "GraphXTemplate.xaml";
            _zoomControl.Resources.MergedDictionaries.Add(new ResourceDictionary { Source = new Uri(templatePath) });

            ///Options.Controls.Add(_countSymbolsCheckBox);
            Output.Controls.Add(_wpfHost);
        }

        public string Name => "Topological Sort Plugin";
        public string Author => "Artem Gorokhov";
        public string Description =>
            "This plugin demonstrates how Topological Sort works.\n";

        public Panel Options { get; } = new Panel();
        public Panel Output { get; } = new Panel();

        public void Run(string dotSource)
        {
            var vertexFun = VertexFactory.Name;
            var edgeFun = EdgeFactory<GraphXVertex>.Weighted(0);

            try
            {
                graph = Graph.LoadDot(dotSource, vertexFun, edgeFun);
                states = new LinkedList<Tuple<Traversal.Node, int>>();

                //BidirectionalGraph<GraphXVertex, GraphXTaggedEdge<GraphXVertex, int>>;
                //BidirectionalGraph<int, Edge<int>>
                sort = new TopologicalSortAlgorithm<GraphXVertex, GraphXTaggedEdge<GraphXVertex, int>>(graph);

                sort.StartVertex += OnStartVertex;
                sort.FinishVertex += OnFinishVertex;

                currState = -1;

                sort.Compute();

                _logic.Graph = graph;
                _graphArea.GenerateGraph();
                _graphArea.SetVerticesDrag(true, true);
                _graphArea.RelayoutGraph(true);
                _zoomControl.ZoomToFill();
                _wpfHost.Refresh();
                _wpfHost.Update();


                //                var message = $"{_graph.VertexCount} vertices.";
                //                if (_countSymbolsCheckBox.Checked) message = $"{message} {dotSource.Length} symbols read.";
                //                MessageBox.Show(message);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void redrawGraph()
        {

        }

        public void NextStep()
        {
            currState++;
            redrawGraph();
        }

        public void PreviousStep()
        {
            currState--;
            redrawGraph();
        }
        private void memorizeState()
        {
            states.AddLast(new Tuple<Traversal.Node, int>(traversal.currNode, sort.SortedVertices.Count()));
        }
        private void OnFinishVertex(GraphXVertex vertex)
        {
            try
            {
                memorizeState();
                traversal.Pop();
            }
            catch (Exception e)
            {
                string s = e.ToString();
            }
        }
        private void OnStartVertex(GraphXVertex vertex)
        {
            try
            {
                memorizeState();
                traversal.Push(vertex);
            }
            catch (Exception e)
            {
                string s = e.ToString();
            }

        }

        private void ShowResults()
        {
            MessageBox.Show($"{graph.VertexCount} vertices.\nTotal edges weight {graph.Edges.Sum(edge => edge.Tag)}.");
        }

        public bool CanGoBack => currState > 0;
        public bool CanGoFurther => currState + 1 < states.Count;
    }

    public class Traversal
    {
        public class Node
        {
            public LinkedList<Node> children;
            public GraphXVertex v;
            public Node parent;

            public Node(GraphXVertex v, Node parent)
            {
                this.parent = parent;
                this.v = v;
            }

            public Node AddChild(GraphXVertex v)
            {
                Node child = new Node(v, this);
                children.AddLast(child);
                return child;
            }
        }
        public Node currNode;
        private LinkedList<Node> nodes = new LinkedList<Node>();
        public Traversal()
        { }

        public void Push(GraphXVertex v)
        {
            if (currNode == null)
            {
                currNode = new Node(v, null);
                nodes.AddLast(currNode);
            }
            else
                currNode = currNode.AddChild(v);
        }

        public void Pop()
        {
            if (currNode == null)
                currNode = null;
            else
                currNode = currNode.parent;
        }

    }
}
*/