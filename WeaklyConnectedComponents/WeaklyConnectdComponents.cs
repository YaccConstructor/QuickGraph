using System.Collections.Generic;
using System.Linq;
using Common;
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

namespace WeaklyConnectedComponents
{
    using HelperForKruskalAndPrimVisualisation;
    using ConnectedComponents;
    using QuickGraph.Algorithms.Search;
    using Graph = BidirectionalGraph<GraphXVertex, GraphXTaggedEdge<GraphXVertex, int>>;

    [Extension]
    public class WeaklyConnectedComponents : ConnectedComponents , IAlgorithm
    {
        private List<GraphXTaggedEdge<GraphXVertex, int>> edges = new List<GraphXTaggedEdge<GraphXVertex, int>>();
        private List<GraphXVertex> vertex = new List<GraphXVertex>();

        public string Name => "Weakly Connected Components";
        public string Author => "Kseniya Gonta";
        public string Description => "This plugin demonstrates how to find connected components.\n";

        public void Run(string dotSource)
        {
            if (_countSymbolsCheckBox.Checked)
            {
                MessageBox.Show($"Read {dotSource.Length} symbol(s).");
            }

            var vertexFun = VertexFactory.Name;
            var edgeFun = EdgeFactory<GraphXVertex>.Weighted(0);
            var graph = Graph.LoadDot(dotSource, vertexFun, edgeFun);

            if (!graph.Vertices.Any())
            {
                MessageBox.Show("Graph is empty.");
                return;
            }
            var undirected = UndirectedGraphFactory.GetUnderectedGraphFromDot(dotSource);
            var dfsUndir = new UndirectedDepthFirstSearchAlgorithm<GraphXVertex, GraphXTaggedEdge<GraphXVertex, int>>(undirected);
            step = -1;
            componentNum = 0;
            steps = 0;
            diffBySteps = new List<int>();
            vertices = new List<GraphXVertex>();

            dfsUndir.StartVertex += new VertexAction<GraphXVertex>(this.StartVertex);
            dfsUndir.DiscoverVertex += new VertexAction<GraphXVertex>(this.DiscoverVertex);
            dfsUndir.Compute();

            ++this.componentNum;


            graphArea.LogicCore.Graph = graph;
            graphArea.GenerateGraph();
            _zoomControl.ZoomToFill();

        }

        private void StartVertex(GraphXVertex v)
        {
            ++this.componentNum;
        }

        private void DiscoverVertex(GraphXVertex v)
        {
            diffBySteps.Add(componentNum);
            vertices.Add(v);
            steps++;
        }
    }
}