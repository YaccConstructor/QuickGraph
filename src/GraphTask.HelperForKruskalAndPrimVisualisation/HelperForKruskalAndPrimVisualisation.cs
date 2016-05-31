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

namespace HelperForKruskalAndPrimVisualisation
{
    using System;
    using Graph = BidirectionalGraph<GraphXVertex, GraphXTaggedEdge<GraphXVertex, int>>;
    using GraphArea = BidirectionalGraphArea<GraphXVertex, GraphXTaggedEdge<GraphXVertex, int>>;
    using Attributes = IDictionary<string, string>;

    [Extension]
    static public class HelperForKruskalAndPrimVisualisation
    {
        static private CheckBox _countSymbolsCheckBox;
        static private GraphArea _graphArea;
        static private GraphXZoomControl _zoomControl;
        static private bool hasStarted;
        static private bool hasFinished;
        static private List<EdgeForVisualisation> edges = new List<EdgeForVisualisation>();
        static private int i = -1;

        static public Panel Options { get; } = new Panel { Dock = DockStyle.Fill };
        static public Panel Output { get; } = new Panel { Dock = DockStyle.Fill };

        static public void Run(string dotSource, List<EdgeForVisualisation> e, GraphArea ga, GraphXZoomControl gz, CheckBox cb)
        {
            _graphArea = ga;
            _zoomControl = gz;
            _countSymbolsCheckBox = cb;
            //Output.Controls.Add(new ElementHost { Dock = DockStyle.Fill, Child = _zoomControl });
            if (_countSymbolsCheckBox.Checked)
            {
                MessageBox.Show($"Read {dotSource.Length} symbol(s).");
            }
            edges = e;
            i = -1;

            var vertexFun = VertexFactory.Name;
            var edgeFun = EdgeFactory<GraphXVertex>.Weighted(0);

            var graph = Graph.LoadDot(dotSource, vertexFun, edgeFun);

            if (!graph.Vertices.Any())
            {
                MessageBox.Show("Graph is empty.");
                return;
            }
            _graphArea.LogicCore.Graph = graph;
            _graphArea.GenerateGraph();
            _zoomControl.ZoomToFill();
            foreach (var i in _graphArea.EdgesList)
            {
                i.Value.Foreground = new SolidColorBrush(Colors.WhiteSmoke);
                i.Value.ShowLabel = true;
                i.Value.ShowArrows = false;
            }
            hasStarted = true;
            hasFinished = false;
            CanFuther(true);
            CanBack(false);
        }

        static private bool IsEqual(GraphXVertex a, GraphXVertex b)
        {
            return (a.ID == b.ID) &&
                   (a.Text == b.Text);
        } 

        static public void NextStep()
        {
            i++;
            var edge = _graphArea.EdgesList.First(x => (IsEqual(x.Key.Source, edges[i].edge.Source)) && (IsEqual(x.Key.Target, edges[i].edge.Target)));
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
                hasFinished = true;
                return;
            }
            _zoomControl.ZoomToFill();
            CanBack(i != -1);
            CanFuther(i != edges.Count - 1);
        }

        static public void PreviousStep()
        {
            var edge = _graphArea.EdgesList.First(x => (IsEqual(x.Key.Source, edges[i].edge.Source)) && (IsEqual(x.Key.Target, edges[i].edge.Target)));
            if (edges[i].isConteins)
            {
                var vertex1 = _graphArea.VertexList.First(x => x.Key == edge.Key.Source);
                var vertex2 = _graphArea.VertexList.First(x => x.Key == edge.Key.Target);
                var edge1 = edges.ToList().FirstOrDefault(x => IsEqual(x.edge.Source, vertex1.Key) && x.isConteins && x.number < i)?.edge;
                var edge2 = edges.ToList().FirstOrDefault(x => IsEqual(x.edge.Target, vertex1.Key) && x.isConteins && x.number < i)?.edge;
                var edge3 = edges.ToList().FirstOrDefault(x => IsEqual(x.edge.Source, vertex2.Key) && x.isConteins && x.number < i)?.edge;
                var edge4 = edges.ToList().FirstOrDefault(x => IsEqual(x.edge.Target, vertex2.Key) && x.isConteins && x.number < i)?.edge;
                if ((edge1 == null) && (edge2 == null))
                    vertex1.Value.Background = new SolidColorBrush(Color.FromArgb(255, 227, 227, 227));
                if ((edge3 == null) && (edge4 == null))
                    vertex2.Value.Background = new SolidColorBrush(Color.FromArgb(255, 227, 227, 227));
                edge.Value.Foreground = new SolidColorBrush(Colors.WhiteSmoke);
            }
            else
            {
                edge.Value.Foreground = new SolidColorBrush(Colors.WhiteSmoke);
            }
            i--;
            _zoomControl.ZoomToFill();

            hasFinished = false;
            CanFuther(i != edges.Count - 1);
            CanBack(i != -1);
        }
        static public event EndAction CanBack;
        static private void OnCanBack(bool x)
        {
            var eh = CanBack;
            if (eh != null)
                eh(x);
        }
        static public event EndAction CanFuther;
        static private void OnCanFuther(bool x)
        {
            var eh = CanFuther;
            if (eh != null)
                eh(x);
        }
        static public bool CanGoBack => i != -1;
        static public bool CanGoFurther => i != edges.Count - 1;
        public delegate void EndAction(bool x);
    }
}