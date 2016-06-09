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

namespace PrimVisualisation
{
    using System;
    using Graph = BidirectionalGraph<GraphXVertex, GraphXTaggedEdge<GraphXVertex, int>>;
    using GraphArea = BidirectionalGraphArea<GraphXVertex, GraphXTaggedEdge<GraphXVertex, int>>;
    using Attributes = IDictionary<string, string>;
    using HelperForKruskalAndPrimVisualisation;

    [Extension]
    public class PrimVisualisation : IAlgorithm
    {
        private readonly CheckBox _countSymbolsCheckBox;
        private readonly GraphArea _graphArea;
        private readonly GraphXZoomControl _zoomControl;
        //private Stack<List<GraphSerializationData>> _steps;
        private bool _hasStarted;
        private bool _hasFinished;
        private List<EdgeForVisualisation> edges = new List<EdgeForVisualisation>();
        private int i = -1;
        private bool canBack;
        private bool canFuther;

        public PrimVisualisation()
        {
            _countSymbolsCheckBox = new CheckBox { Text = "Count symbols when start", Location = new Point(12, 6) };
            Options.Controls.Add(_countSymbolsCheckBox);

            _graphArea = new GraphArea();
            _zoomControl = new GraphXZoomControl { Content = _graphArea };
            Output.Controls.Add(new ElementHost { Dock = DockStyle.Fill, Child = _zoomControl });
        }

        public string Name => "Prim algorithm";
        public string Author => "Alexander Pihtin";

        public string Description =>
            "This search minimum spanning tree visualisation .\n" +
            "It used Prim algorithm.\n";

        public Panel Options { get; } = new Panel { Dock = DockStyle.Fill };
        public Panel Output { get; } = new Panel { Dock = DockStyle.Fill };

        public void Run(string dotSource)
        {
            edges.Clear();
            var graph = UndirectedGraphFactory.GetUnderectedGraphFromDot(dotSource);
            var prim = new QuickGraph.Algorithms.MinimumSpanningTree.PrimMinimumSpanningTreeAlgorithm<GraphXVertex, GraphXTaggedEdge<GraphXVertex, int>>(graph, x => x.Tag);
            prim.TreeEdge += Prim_TreeEdge;
            prim.ExamineEdge += Prim_ExamineEdge;
            prim.Compute();
            HelperForKruskalAndPrimVisualisation.CanBack -= HelperForKruskalAndPrimVisualisation_CanBack;
            HelperForKruskalAndPrimVisualisation.CanFuther -= HelperForKruskalAndPrimVisualisation_CanFuther;
            HelperForKruskalAndPrimVisualisation.CanBack += HelperForKruskalAndPrimVisualisation_CanBack;
            HelperForKruskalAndPrimVisualisation.CanFuther += HelperForKruskalAndPrimVisualisation_CanFuther;
            HelperForKruskalAndPrimVisualisation.Run(dotSource, edges, _graphArea, _zoomControl, _countSymbolsCheckBox);
        }

        private void HelperForKruskalAndPrimVisualisation_CanFuther(bool x)
        {
            canFuther = x;
        }

        private void HelperForKruskalAndPrimVisualisation_CanBack(bool x)
        {
            canBack = x;
        }

        private void Prim_ExamineEdge(GraphXTaggedEdge<GraphXVertex, int> e)
        {
            edges.Add(new EdgeForVisualisation(e, false, edges.Count));
        }

        private void Prim_TreeEdge(GraphXTaggedEdge<GraphXVertex, int> e)
        {
            edges.First(x => x.edge == e).isConteins = true;
        }

        public void NextStep()
        {
            HelperForKruskalAndPrimVisualisation.NextStep();
        }

        public void PreviousStep()
        {
            HelperForKruskalAndPrimVisualisation.PreviousStep();
        }

        public bool CanGoBack => canBack;
        public bool CanGoFurther => canFuther;
    }
}