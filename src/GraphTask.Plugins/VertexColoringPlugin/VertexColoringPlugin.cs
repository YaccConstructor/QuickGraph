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
using MessageBox = System.Windows.Forms.MessageBox;

[assembly: Addin]
[assembly: AddinDependency("GraphTasks", "1.0")]

namespace VertexColoringPlugin
{
    using BiGraph = BidirectionalGraph<GraphXVertex, GraphXTaggedEdge<GraphXVertex, int>>;
    using UnGraph = UndirectedGraph<GraphXVertex, GraphXTaggedEdge<GraphXVertex, int>>;
    using GraphArea = BidirectionalGraphArea<GraphXVertex, GraphXTaggedEdge<GraphXVertex, int>>;
    using System.Windows;
    [Extension]
    public class VertexColoringPlugin : IAlgorithm
    {
        private readonly GraphArea _graphArea;
        private readonly GraphXZoomControl _zoomControl;
        private bool _hasStarted;
        private bool _hasFinished;
        private List<GraphXVertex> states;
        private VertexColoringAlgorithm<GraphXVertex, GraphXTaggedEdge<GraphXVertex, int>> coloringAlgorithm;
        private OutputModel<GraphXVertex, GraphXTaggedEdge<GraphXVertex, int>> graphWithColoredVertices;
        private BiGraph bigraph;
        private int currStateNumber;
        private String[] colors;

        byte a = 0xFF, r = 0xE3, g = 0xE3, b = 0xE3;

        public VertexColoringPlugin()
        {
            _graphArea = new GraphArea();
            _zoomControl = new GraphXZoomControl { Content = _graphArea };
            Output.Controls.Add(new ElementHost { Dock = DockStyle.Fill, Child = _zoomControl });
        }

        public string Name => "Vertex Coloring Plugin";
        public string Author => "Alex Rabochiy";
        public string Description => "This plugin demonstrates how Vertex Coloring works.\n";

        public Panel Options { get; } = new Panel { Dock = DockStyle.Fill };
        public Panel Output { get; } = new Panel { Dock = DockStyle.Fill };

        public bool CanGoBack => currStateNumber != 0 && states.Count != 0;
        public bool CanGoFurther => _hasStarted && !_hasFinished;

        public void Run(string dotSource)
        {
            var input = GenerateInputModel(dotSource);
            if (!input.Graph.Vertices.Any())
            {
                MessageBox.Show("Graph is empty.");
                return;
            }
            colors = ColorGenerator.genColors(input.Graph.VertexCount);
            coloringAlgorithm = new VertexColoringAlgorithm<GraphXVertex, GraphXTaggedEdge<GraphXVertex, int>>(input);

            states = new List<GraphXVertex>();

            coloringAlgorithm.ColourVertex += OnColourVertex;
            currStateNumber = -1;

            graphWithColoredVertices = coloringAlgorithm.Compute();

            _graphArea.LogicCore.Graph = bigraph;
            _graphArea.GenerateGraph();
            _zoomControl.ZoomToFill();

            _hasStarted = true;
            _hasFinished = false;
        }

        private void HighlightTraversal()
        {
            var currState = states.ElementAt(currStateNumber);
            GraphXVertex vert = currState;
            foreach (var key in _graphArea.VertexList.Keys)
            {
                if (key.ID.Equals(currState.ID))
                    vert = key;
            }
            
            var color = colors[graphWithColoredVertices.Colors[currState] ?? default(int)];
            _graphArea.VertexList[vert].Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(color));

            if (currStateNumber + 1 < states.Count)
            {
                var nextState = states.ElementAt(currStateNumber + 1);
                foreach (var key in _graphArea.VertexList.Keys)
                {
                    if (key.Text == nextState.Text)
                        vert = key;
                }
                _graphArea.VertexList[vert].Background = new SolidColorBrush(Color.FromArgb(a, r, g, b));
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

        private void OnColourVertex(GraphXVertex vertex)
        {
            states.Add(vertex);
        }

        private InputModel<GraphXVertex, GraphXTaggedEdge<GraphXVertex, int>> GenerateInputModel(string dotSource)
        {
            var vertexFun = VertexFactory.Name;
            var edgeFun = EdgeFactory<GraphXVertex>.Weighted(0);
            var graph = UnGraph.LoadDot(dotSource, vertexFun, edgeFun);
            bigraph = BiGraph.LoadDot(dotSource, vertexFun, edgeFun);

            return new InputModel<GraphXVertex, GraphXTaggedEdge<GraphXVertex, int>>
            {
                Graph = graph
            };
        }
    }
}