using System.Linq;
using System.Windows.Forms;
using System.Windows.Media;
using Common;
using Mono.Addins;
using QuickGraph;
using QuickGraph.GraphXAdapter;
using System;
using MessageBox = System.Windows.Forms.MessageBox;
using Point = System.Drawing.Point;

[assembly: Addin]
[assembly: AddinDependency("GraphTasks", "1.0")]

namespace FloydWarshallShortestPathPlugin
{
    using QuickGraph.Algorithms.ShortestPath;
    using Graph = BidirectionalGraph<GraphXVertex, GraphXTaggedEdge<GraphXVertex, int>>;
    using GraphArea = BidirectionalGraphArea<GraphXVertex, GraphXTaggedEdge<GraphXVertex, int>>;

    [Extension]
    public class FloydWarshallShortestPathPlugin : IAlgorithm
    {

        private int dimension;
        private bool _hasStarted;
        private bool _hasFinished;
        private FloydWarshallAllShortestPathAlgorithm<GraphXVertex, GraphXTaggedEdge<GraphXVertex, int>> floyd;
        private int currState;

        public FloydWarshallShortestPathPlugin()
        {
            Options.Controls.AddRange(new Control[] { });
        }

        public string Name => "Floyd Warshall algorithm Plugin";
        public string Author => "Alisa Meteleva";
        public string Description => "This plugin demonstrates how Floyd Warshall alg works.\n";

        public Panel Options { get; } = new Panel { Dock = DockStyle.Fill };
        public Panel Output { get; } = new Panel { Dock = DockStyle.Fill };

        public bool CanGoBack => currState != 0;
        public bool CanGoFurther => _hasStarted && !_hasFinished;

        public void Run(string dotSource)
        {
            Output.Controls.Clear();

            var vertexFun = VertexFactory.Name;
            var edgeFun = EdgeFactory<GraphXVertex>.Weighted(0);
            var graph = Graph.LoadDot(dotSource, vertexFun, edgeFun);

            dimension = graph.Vertices.Count();

            if (graph.IsVerticesEmpty)
            {
                MessageBox.Show("Graph is empty.");
                return;
            }

            //add cells for distances
            for (var i = 1; i <= dimension; i++)
            {
                for (var j = 1; j <= graph.Vertices.Count(); j++)
                {
                    var cell = new TextBox { Text = "0", Location = new Point(j * 60, i * 30), Height = 20, Width = 50 };
                    Output.Controls.Add(cell);
                }

            }

            floyd = new FloydWarshallAllShortestPathAlgorithm<GraphXVertex, GraphXTaggedEdge<GraphXVertex, int>>(graph, x => x.Tag);

            //add labels above cells
            var k = 1;
            foreach (var v in graph.Vertices)
            {
                Output.Controls.Add(new Label { Text = v.ToString(), Location = new Point(k * 60, 0), Width = 50 });
                Output.Controls.Add(new Label { Text = v.ToString(), Location = new Point(0, k * 30), Width = 50 });
                k++;
            }

            floyd.Compute();

            currState = -1;
            _hasStarted = true;
            _hasFinished = false;

        }


        public void NextStep()
        {

            currState++;
            var t = 0;
            var i = floyd.steps[currState].Source;
            var j = floyd.steps[currState].Target;
            var dst = floyd.steps[currState].Distance;
            var k = floyd.steps[currState].Predecessor;

            var ij = (TextBox)Output.Controls[i * dimension + j];
            ij.Text = dst.ToString();
            ij.BackColor = System.Drawing.Color.LightBlue;



            if (k > 0)
            {
                Random rand = new Random();
                var randomColor = System.Drawing.Color.FromArgb(Colors.AliceBlue.A, rand.Next(255),
                                                                 rand.Next(255), rand.Next(255));

                var ik = (TextBox)Output.Controls[i * dimension + k];
                ik.BackColor = randomColor;

                var kj = (TextBox)Output.Controls[k * dimension + j];
                kj.BackColor = randomColor;
            }


            _hasFinished = currState == floyd.steps.Count() - 1;


        }

        public void PreviousStep()
        {
            _hasFinished = false;


            var i = floyd.steps[currState].Source;
            var j = floyd.steps[currState].Target;
            var dst = floyd.steps[currState].Distance;
            var k = floyd.steps[currState].Predecessor;

            var ij = (TextBox)Output.Controls[i * dimension + j];

            var edgePrevDist = floyd.steps.LastOrDefault(x => (x.Key < currState) && (x.Value.Source == i) && (x.Value.Target == j)).Value.Distance;
            ij.Text = edgePrevDist.ToString();

            if (edgePrevDist == 0)
            {
                ij.BackColor = System.Drawing.Color.White;
            }

            if (k > 0)
            {
                var ik = (TextBox)Output.Controls[i * dimension + k];
                ik.BackColor = System.Drawing.Color.LightBlue;

                var kj = (TextBox)Output.Controls[k * dimension + j];
                kj.BackColor = System.Drawing.Color.LightBlue;
            }

            currState--;

        }

    }
}