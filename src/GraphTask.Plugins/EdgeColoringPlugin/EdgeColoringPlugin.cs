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
using QuickGraph.Algorithms.Search;
using System;
using System.ComponentModel;
using QuickGraph.Algorithms.GraphColoring.VertexColoring;
using Microsoft.FSharp.Control;
using MessageBox = System.Windows.Forms.MessageBox;

[assembly: Addin]
[assembly: AddinDependency("GraphTasks", "1.0")]

namespace VertexColoringPlugin
{
    using BiGraph = BidirectionalGraph<GraphXVertex, GraphXTaggedEdge<GraphXVertex, int>>;
    using UnGraph = UndirectedGraph<GraphXVertex, TaggedEdge<GraphXVertex, int>>;
    using GraphArea = BidirectionalGraphArea<GraphXVertex, GraphXTaggedEdge<GraphXVertex, int>>;
    using System.Windows;
    using QuickGraph.Algorithms;
    [Extension]
    public class VertexColoringPlugin : IAlgorithm
    {
        private readonly GraphArea _graphArea;
        private readonly GraphXZoomControl _zoomControl;
        private bool _hasStarted;
        private bool _hasFinished;
        private List<Tuple<GraphXTaggedEdge<GraphXVertex, int>, int>> states;
        private EdgeColoringAlgorithm<GraphXVertex, TaggedEdge<GraphXVertex, int>> coloringAlgorithm;
        private UndirectedGraph<GraphXVertex, TaggedEdge<GraphXVertex, int>> graphWithColoredVertices;
        private BiGraph bigraph;
        private int currStateNumber;
        private String[] colors;

        public VertexColoringPlugin()
        {
            _graphArea = new GraphArea();
            _zoomControl = new GraphXZoomControl { Content = _graphArea };
            Output.Controls.Add(new ElementHost { Dock = DockStyle.Fill, Child = _zoomControl });
        }

        public string Name => "Edge Coloring Plugin";
        public string Author => "Alex Rabochiy";
        public string Description => "This plugin demonstrates how Edge Coloring works.\n";

        public Panel Options { get; } = new Panel { Dock = DockStyle.Fill };
        public Panel Output { get; } = new Panel { Dock = DockStyle.Fill };

        public bool CanGoBack => currStateNumber != 0 && states.Count != 0;
        public bool CanGoFurther => _hasStarted && !_hasFinished;

        public void Run(string dotSource)
        {
            var input = ReadInputGraph(dotSource);
            if (!input.Vertices.Any())
            {
                MessageBox.Show("Graph is empty.");
                return;
            }
            colors = ColorGenerator.genColors(input.EdgeCount);
            coloringAlgorithm = new EdgeColoringAlgorithm<GraphXVertex, TaggedEdge<GraphXVertex, int>>();

            states = new List<Tuple<GraphXTaggedEdge<GraphXVertex, int>, int>>();

            coloringAlgorithm.Colored += OnColourEdge;
            currStateNumber = -1;

            graphWithColoredVertices = coloringAlgorithm.Compute(input);

            _graphArea.LogicCore.Graph = bigraph;
            _graphArea.GenerateGraph();
            _zoomControl.ZoomToFill();

            _hasStarted = true;
            _hasFinished = false;
        }

        private void OnColourEdge(object sender, TaggedEdge<GraphXVertex, int> edge)
        {
            foreach (var graphXEdge in bigraph.Edges)
            {
                if ((graphXEdge.Source.Equals(edge.Source)) && (graphXEdge.Target.Equals(edge.Target)))
                {
                    states.Add(new Tuple<GraphXTaggedEdge<GraphXVertex, int>, int>(graphXEdge, edge.Tag));
                }
            }
        }

        private void HighlightTraversal()
        {
            var currState = states.ElementAt(currStateNumber);
            var currEdge = currState.Item1;
            var currColor = colors[currState.Item2];
            _graphArea.EdgesList[currEdge].Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(currColor));

            if (currStateNumber + 1 < states.Count)
            {
                var nextState = states.ElementAt(currStateNumber + 1);
                var nextEdge = nextState.Item1;
                _graphArea.EdgesList[nextEdge].Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF000000"));
            }
        }

        public void NextStep()
        {
            currStateNumber++;
            HighlightTraversal();
            _hasFinished = currStateNumber == states.Count - 1;
        }

        public void PreviousStep()
        {
            currStateNumber--;
            HighlightTraversal();
            _hasFinished = false;
        }

        private UndirectedGraph<GraphXVertex, TaggedEdge<GraphXVertex, int>> ReadInputGraph(string dotSource)
        {
            var vertexFun = VertexFactory.Name;
            var edgeFun1 = EdgeFactory<GraphXVertex>.WeightedTaggedEdge(0);
            var edgeFun = EdgeFactory<GraphXVertex>.Weighted(0);
            var unGraph = UnGraph.LoadDot(dotSource, vertexFun, edgeFun1);
            bigraph = new BiGraph();

            bigraph.AddVertexRange(unGraph.Vertices);
            var edges = unGraph.Edges;
            var GraphXedges = new List<GraphXTaggedEdge<GraphXVertex, int>>();
            foreach (var edge in edges)
            {
                var newEdge1 = new GraphXTaggedEdge<GraphXVertex, int>(edge.Source, edge.Target, 0);
                var newEdge2 = new GraphXTaggedEdge<GraphXVertex, int>(edge.Target, edge.Source, 0);
                bigraph.AddEdge(newEdge1);
                bigraph.AddEdge(newEdge2);
            }

            return unGraph;
        }
    }
}