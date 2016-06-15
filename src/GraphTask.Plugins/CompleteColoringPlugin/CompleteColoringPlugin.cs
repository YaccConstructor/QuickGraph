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
    using UnGraph = UndirectedGraph<GraphXVertex, GraphXTaggedEdge<GraphXVertex, int>>;
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
        private List<Tuple<GraphXVertex, int>> states;
        private ApproxCompleteColoringAlgorithm<GraphXVertex, GraphXTaggedEdge<GraphXVertex, int>> coloringAlgorithm;
        private List<List<GraphXVertex>> coloring;
        private BiGraph bigraph;
        private int currStateNumber;
        private String[] colors;

        public VertexColoringPlugin()
        {
            _graphArea = new GraphArea();
            _zoomControl = new GraphXZoomControl { Content = _graphArea };
            Output.Controls.Add(new ElementHost { Dock = DockStyle.Fill, Child = _zoomControl });
        }

        public string Name => "Complete Coloring Plugin";
        public string Author => "Alex Rabochiy";
        public string Description => "This plugin demonstrates how Complete Coloring works.\n";

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
            colors = ColorGenerator.genColors(input.VertexCount);
            coloringAlgorithm = new ApproxCompleteColoringAlgorithm<GraphXVertex, GraphXTaggedEdge<GraphXVertex, int>>();

            states = new List<Tuple<GraphXVertex, int>>();

            coloringAlgorithm.Colored += OnColourVertex; ;
            currStateNumber = -1;

            coloring = coloringAlgorithm.Compute(input);

            _graphArea.LogicCore.Graph = bigraph;
            _graphArea.GenerateGraph();
            _zoomControl.ZoomToFill();

            _hasStarted = true;
            _hasFinished = false;
        }

        private void OnColourVertex(object sender, Tuple<GraphXVertex, int> args)
        {
            states.Add(args);
        }

        private void HighlightTraversal()
        {
            var currState = states.ElementAt(currStateNumber);
            GraphXVertex currVertex = currState.Item1;

            var color = colors[currState.Item2];
            _graphArea.VertexList[currVertex].Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(color));

            if (currStateNumber + 1 < states.Count)
            {
                var nextState = states.ElementAt(currStateNumber + 1);
                GraphXVertex nextVertex = nextState.Item1;
                _graphArea.VertexList[nextVertex].Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFE3E3E3"));
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

        private UndirectedGraph<GraphXVertex, GraphXTaggedEdge<GraphXVertex, int>> ReadInputGraph(string dotSource)
        {
            var vertexFun = VertexFactory.Name;
            var edgeFun1 = EdgeFactory<GraphXVertex>.WeightedTaggedEdge(0);
            var edgeFun = EdgeFactory<GraphXVertex>.Weighted(0);
            var graph = UnGraph.LoadDot(dotSource, vertexFun, edgeFun);
            bigraph = graph.ToBidirectionalGraph();

            return graph;
        }
    }
}