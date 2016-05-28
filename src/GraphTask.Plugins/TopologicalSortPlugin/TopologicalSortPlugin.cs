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
using QuickGraph.Algorithms.TopologicalSort;
using System;
using System.ComponentModel;
using MessageBox = System.Windows.Forms.MessageBox;
using Point = System.Drawing.Point;

[assembly: Addin]
[assembly: AddinDependency("GraphTasks", "1.0")]

namespace TopologicalSortPlugin
{
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
        private Traversal<GraphXVertex> traversal = new Traversal<GraphXVertex>();
        private List<Tuple<Traversal<GraphXVertex>.Node, int>> states;
        private TopologicalSortAlgorithm<GraphXVertex, GraphXTaggedEdge<GraphXVertex, int>> sort;
        private int currState;

        byte a = 0xFF, r = 0xE3, g = 0xE3, b = 0xE3;

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

        public bool CanGoBack => currState != 0 && states.Count != 0;
        public bool CanGoFurther => _hasStarted && !_hasFinished;

        public void Run(string dotSource)
        {
            sortedList.Clear();
            traversal.Clear();
            var vertexFun = VertexFactory.Name;
            var edgeFun = EdgeFactory<GraphXVertex>.Weighted(0);
            var graph = Graph.LoadDot(dotSource, vertexFun, edgeFun);

            states = new List<Tuple<Traversal<GraphXVertex>.Node, int>>();

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
        }

        private void HighlightTraversal()
        {
            var currNode = states.ElementAt(currState).Item1;
            var nodes = (currNode == null)? traversal.nodes : currNode.children;
            foreach (Traversal<GraphXVertex>.Node node in nodes)
                _graphArea.VertexList[node.v].Background = new SolidColorBrush(Color.FromArgb(a, r, g, b));
            while (currNode != null)
            {
                _graphArea.VertexList[currNode.v].Background = new SolidColorBrush(Colors.YellowGreen);
                currNode = currNode.parent;
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
            _hasFinished = currState == states.Count - 1;
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
            states.Add(new Tuple<Traversal<GraphXVertex>.Node, int>(traversal.currNode, sort.SortedVertices.Count()));
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