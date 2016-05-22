using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using System.Windows.Media;
using Common;
using GraphX.Controls;
using Mono.Addins;
using QuickGraph;
using QuickGraph.Algorithms.ShortestPath.Yen;
using QuickGraph.GraphXAdapter;

[assembly: Addin]
[assembly: AddinDependency("GraphTasks", "1.0")]
namespace YenVisualisation
{
    using Graph = BidirectionalGraph<GraphXVertex, GraphXTaggedEdge<GraphXVertex, int>>;
    using GraphArea = BidirectionalGraphArea<GraphXVertex, GraphXTaggedEdge<GraphXVertex, int>>;

    [Extension]
    public class Yen : IAlgorithm
    {
        private GraphArea _graphArea;
        private readonly GraphXZoomControl _zoomControl;

        private readonly TextBox _k;
        private readonly TextBox _startVertex;
        private readonly TextBox _finishVertex;
        private readonly CheckBox _showMessages;

        private List<IEnumerable<TaggedEquatableEdge<char, double>>> _paths;
        private List<TaggedEquatableEdge<char, double>> _removedEdges;
        private int _currentIteration;
        private Dictionary<int, List<YenState>> _states;

        public Yen()
        {
            _k = new TextBox { Text = "100", Location = new Point(12, 6) };
            _startVertex = new TextBox { Location = new Point(12, 30), Text = "1" };
            _finishVertex = new TextBox { Location = new Point(12, 54), Text = "5" };
            _showMessages = new CheckBox { Location = new Point(12, 74), Checked = true };
            var kLabel = new Label { Location = new Point(120, 6), Text = "k" };
            var sLabel = new Label { Location = new Point(120, 30), Text = "start vertex" };
            var fLabel = new Label { Location = new Point(120, 54), Text = "target vertex" };
            var smLabel = new Label { Location = new Point(120, 74), Text = "messages" };

            Options.Controls.AddRange(new Control[] { _k, _startVertex, _finishVertex, kLabel, sLabel, fLabel, _showMessages, smLabel });

            _graphArea = new GraphArea();
            _zoomControl = new GraphXZoomControl { Content = _graphArea };
            Output.Controls.Add(new ElementHost { Dock = DockStyle.Fill, Child = _zoomControl });

            _states = new Dictionary<int, List<YenState>>();
        }

        private void GetYenIterations(Graph graph)
        {
            var yenGraph = ToYenInput(graph);
            var yen = new YenShortestPathsAlgorithm<char>(yenGraph, _startVertex.Text.First(), _finishVertex.Text.First(), int.Parse(_k.Text));
            _paths = yen.Execute().ToList();
            _removedEdges = yen.RemovedEdges.ToList();
        }

        private void EnlightBeginEndVertix()
        {
            _graphArea.VertexList.First(x => x.Key.Text == _startVertex.Text).Value.Background = new SolidColorBrush(Colors.BurlyWood);
            _graphArea.VertexList.First(x => x.Key.Text == _finishVertex.Text).Value.Background = new SolidColorBrush(Colors.DarkSeaGreen);
        }

        // From Yen to Vis
        private KeyValuePair<GraphXTaggedEdge<GraphXVertex, int>, EdgeControl> FindSameEdge(TaggedEquatableEdge<char, double> edge)
          => _graphArea.EdgesList.First(x => x.Key.Source.Text == edge.Source.ToString() && x.Key.Target.Text == edge.Target.ToString()
          || x.Key.Source.Text == edge.Target.ToString() && x.Key.Source.Text == edge.Target.ToString());

        public void PreviousStep()
        {
            ApplyState(--_currentIteration);
        }

        private void ApplyState(int key)
        {
            var states = _states[key];
            int index = 0;
            foreach (var edge in _graphArea.EdgesList.Values)
            {
                edge.Foreground = states[index].Foreground;
                edge.ShowArrows = states[index].ShowArrows;
                edge.ShowLabel = states[index].ShowLabel;
                index++;
            }
        }

        private void SaveState(int key)
        {
            if (!_states.ContainsKey(key))
            {
                _states[key] = new List<YenState>();
                foreach (var edge in _graphArea.EdgesList.Values)
                {
                    _states[key].Add(new YenState
                    {
                        Foreground = edge.Foreground,
                        ShowArrows = edge.ShowArrows,
                        ShowLabel = edge.ShowLabel
                    });
                }
            }
        }

        public void NextStep()
        {
            var index = _currentIteration / 3;
            SaveState(_currentIteration);
            switch (_currentIteration % 3)
            {
                case 0:
                    foreach (var edge in _paths[index])
                    {
                        FindSameEdge(edge).Value.Foreground = new SolidColorBrush(Colors.Red);
                    }
                    if (_showMessages.Checked)
                    {
                        MessageBox.Show("Found the shortest way![red]\nIt will be the current shortest way\n");
                    }
                    break;
                case 1:
                    FindSameEdge(_removedEdges[index]).Value.Foreground = new SolidColorBrush(Colors.Yellow);
                    if (_showMessages.Checked)
                    {
                        MessageBox.Show("We have analysed all edges of the current shortest way!\n We want to pick one of this edges for excluding it from" +
                                        " the graph. If we deleted the yellow one, it would give us the new best shortest path. If we deleted one of rest red" +
                                        " edges it give us longer way or even no way to the target vertex.");
                    }
                    break;
                default:
                    foreach (var edge in _paths[index])
                    {
                        FindSameEdge(edge).Value.Foreground = new SolidColorBrush(Colors.Black);
                    }
                    FindSameEdge(_removedEdges[index]).Value.Foreground = new SolidColorBrush(Colors.White);
                    FindSameEdge(_removedEdges[index]).Value.ShowLabel = false;
                    FindSameEdge(_removedEdges[index]).Value.ShowArrows = false;
                    if (_showMessages.Checked)
                    {
                        MessageBox.Show("The yellow edge is removed from our graph!\n");
                    }
                    break;
            }
            _currentIteration++;
        }

        public void Run(string dotSource)
        {
            var graph = Graph.LoadDot(dotSource, VertexFactory.Name, EdgeFactory<GraphXVertex>.Weighted(0));
            try
            {
                GetYenIterations(graph);
            }
            catch (Exception e)
            {
                MessageBox.Show("Sorry! \n Wrong graph type! \n");
            }

            _currentIteration = 0;

            _graphArea.LogicCore.Graph = graph;
            _graphArea.GenerateGraph();
            _zoomControl.ZoomToFill();

            ShowWeights();
            EnlightBeginEndVertix();
        }

        private void ShowWeights()
        {
            foreach (var i in _graphArea.EdgesList)
            {
                i.Value.ShowLabel = true;
            }
        }

        public string Name =>
          "Yen algorithm";
        public string Author =>
          "Vlad Evtushenko";

        public string Description =>
          "Find k shortest ways in directed graph only\n";

        public Panel Options { get; } =
          new Panel { Dock = DockStyle.Fill };
        public Panel Output { get; } =
          new Panel { Dock = DockStyle.Fill };

        public bool CanGoBack =>
          _currentIteration > 0;

        public bool CanGoFurther =>
            _paths != null && _currentIteration  < (_paths.Count - 1) * 3 + 1; 

        private AdjacencyGraph<char, TaggedEquatableEdge<char, double>> ToYenInput(Graph graph)
        {
            var formatedGraph = new AdjacencyGraph<char, TaggedEquatableEdge<char, double>>(true);
            formatedGraph.AddVertexRange(graph.Vertices.Select(x => x.Text.First()));
            formatedGraph.AddEdgeRange(
              graph.Edges.Select(
                x => new TaggedEquatableEdge<char, double>(x.Source.Text.First(), x.Target.Text.First(), x.Tag)));
            return formatedGraph;
        }
    }
}
