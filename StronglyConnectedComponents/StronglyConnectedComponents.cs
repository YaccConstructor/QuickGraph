using System.Collections.Generic;
using System.Linq;
using Common;
using GraphX.PCL.Common.Models;
using Mono.Addins;
using QuickGraph;
using QuickGraph.GraphXAdapter;
using MessageBox = System.Windows.Forms.MessageBox;

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
    using ConnectedComponents;
    using Graph = BidirectionalGraph<GraphXVertex, GraphXTaggedEdge<GraphXVertex, int>>;

    [Extension]
    public class StronglyConnectedComponents : ConnectedComponents, IAlgorithm
    {
        private List<GraphXTaggedEdge<GraphXVertex, int>> edges = new List<GraphXTaggedEdge<GraphXVertex, int>>();
        private List<GraphXVertex> vertex = new List<GraphXVertex>();
        private Stack<List<GraphSerializationData>> _steps;
        private StronglyConnectedComponentsAlgorithm<GraphXVertex, GraphXTaggedEdge<GraphXVertex, int>> notDFS;

        public string Name => "Strongly Connected Components";
        public string Author => "Kseniya Gonta";

        public string Description =>
            "This plugin demonstrates how to find strongly connected components.\n";

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
            step = -1;
            var graphs = notDFS.Graphs;
            steps = notDFS.Steps;
            diffBySteps = notDFS.DiffBySteps;
            vertices = notDFS.Vertices;

            var amountOfComponents = notDFS.ComponentCount;
            var someComponents = notDFS.Components;

            graphArea.LogicCore.Graph = graph;
            graphArea.GenerateGraph();
            _zoomControl.ZoomToFill();
        }
    }
}