using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using System.Windows.Media;
using Mono.Addins;
using QuickGraph.GraphXAdapter;
using Point = System.Drawing.Point;

[assembly: Addin]
[assembly: AddinDependency("GraphTasks", "1.0")]

namespace ConnectedComponents
{
    using GraphArea = BidirectionalGraphArea<GraphXVertex, GraphXTaggedEdge<GraphXVertex, int>>;
    abstract public class ConnectedComponents //: IAlgorithm
    {
       // public ConnectedComponents()
        //{ }
        protected int steps;
        protected int step;
        protected int componentNum;
        protected List<int> diffBySteps;
        protected readonly GraphArea graphArea = new GraphArea();
        protected List<GraphXVertex> vertices;
        protected readonly CheckBox _countSymbolsCheckBox;
        protected readonly GraphXZoomControl _zoomControl;

        public ConnectedComponents()
        {
            _countSymbolsCheckBox = new CheckBox { Text = "Count symbols when start", Location = new Point(12, 6) };
            Options.Controls.Add(_countSymbolsCheckBox);

            _zoomControl = new GraphXZoomControl { Content = graphArea };
            Output.Controls.Add(new ElementHost { Dock = DockStyle.Fill, Child = _zoomControl });
        }

        public Panel Options { get; } = new Panel { Dock = DockStyle.Fill };
        public Panel Output { get; } = new Panel { Dock = DockStyle.Fill };


        public void NextStep()
        {
            step++;
            HighlightNextStep();
            if (!graphArea.LogicCore.Graph.Vertices.Any())
            {
                return;
            }
        }

        public void PreviousStep()
        {
            HighlightPrevStep();
            step--;
        }

        private void HighlightNextStep()
        {
            foreach (KeyValuePair<GraphXVertex, GraphX.Controls.VertexControl> pair in graphArea.VertexList)
            {
                if (pair.Key.Text == vertices.ElementAt(step).Text)
                {
                    switch (diffBySteps.ElementAt(step))
                    {
                        case 1:
                            pair.Value.Background = new SolidColorBrush(Colors.Green); break;
                        case 2:
                            pair.Value.Background = new SolidColorBrush(Colors.Red); break;
                        case 3:
                            pair.Value.Background = new SolidColorBrush(Colors.Yellow); break;
                        case 4:
                            pair.Value.Background = new SolidColorBrush(Colors.Pink); break;
                        case 5:
                            pair.Value.Background = new SolidColorBrush(Colors.PowderBlue); break;
                        case 6:
                            pair.Value.Background = new SolidColorBrush(Colors.RosyBrown); break;
                        case 7:
                            pair.Value.Background = new SolidColorBrush(Colors.Salmon); break;
                        case 0:
                            pair.Value.Background = new SolidColorBrush(Colors.Sienna); break;
                    }
                }
            }
        }

        private void HighlightPrevStep()
        {
            foreach (KeyValuePair<GraphXVertex, GraphX.Controls.VertexControl> pair in graphArea.VertexList)
            {
                if (pair.Key.Text == vertices.ElementAt(step).Text)
                {
                    pair.Value.Background = new SolidColorBrush(Colors.LightGray);
                }
            }

        }
        public bool CanGoBack => step >= 0;
        public bool CanGoFurther => step < steps - 1;
    }
}
