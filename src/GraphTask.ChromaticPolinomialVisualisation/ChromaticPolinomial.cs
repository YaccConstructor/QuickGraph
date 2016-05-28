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
using MessageBox = System.Windows.Forms.MessageBox;
using Point = System.Drawing.Point;

[assembly: Addin]
[assembly: AddinDependency("GraphTasks", "1.0")]

namespace ChromaticPolinomialVisualisation
{
    using Graph = UndirectedGraph<GraphXVertex, GraphXEdge<GraphXVertex>>;
    using DrawGraph = BidirectionalGraph<VertexWithComponent, GraphXEdge<VertexWithComponent>>;
    using GraphArea = BidirectionalGraphArea<VertexWithComponent, GraphXEdge<VertexWithComponent>>;
    using Algorithm = QuickGraph.Algorithms.ChromaticPolynomial;
    using BaseNode = QuickGraph.Algorithms.ChromaticPolynomial.BaseNode<GraphXVertex, GraphXEdge<GraphXVertex>>;
    using Node = QuickGraph.Algorithms.ChromaticPolynomial.Node<GraphXVertex, GraphXEdge<GraphXVertex>>;

    [Extension]
    public class ChromaticPolinomial : IAlgorithm
    {
        private readonly GraphArea _graphArea;
        private readonly GraphXZoomControl _graphZoomControl;
        private readonly GraphArea _treeArea;
        private readonly GraphXZoomControl _treeZoomControl;
        private BaseNode _curNode;
        private BaseNode _rootNode;
        private bool _hasStarted;

        public ChromaticPolinomial()
        {

            _graphArea = new GraphArea();
            _graphArea.ShowAllEdgesArrows(false);
            _graphZoomControl = new GraphXZoomControl { Content = _graphArea };
            _treeArea = new GraphArea();
            _treeZoomControl = new GraphXZoomControl { Content = _treeArea };
            var splitContainer = new SplitContainer();
            splitContainer.SplitterDistance = splitContainer.Width / 2;
            splitContainer.Dock = DockStyle.Fill;
            splitContainer.Orientation = Orientation.Vertical;
            splitContainer.Panel1.Controls.Add(new ElementHost { Dock = DockStyle.Fill, Child = _graphZoomControl });
            splitContainer.Panel2.Controls.Add(new ElementHost { Dock = DockStyle.Fill, Child = _treeZoomControl });
            Output.Controls.Add(splitContainer);
        }

        public string Name => "Chromatic Polinomial";
        public string Author => "Mikhail Tashkinov";

        public string Description =>
            "Counting chromatic polinomial for given graph";

        public Panel Options { get; } = new Panel { Dock = DockStyle.Fill };
        public Panel Output { get; } = new Panel { Dock = DockStyle.Fill };

        public void Run(string dotSource)
        {
            var vertexFun = VertexFactory.Name;
            var edgeFun = EdgeFactory<GraphXVertex>.Weighted(0);

            var graph = Graph.LoadDot(dotSource, vertexFun, edgeFun);
            _rootNode = Algorithm.buildTree(graph, (GraphXVertex x, GraphXVertex y) => new GraphXEdge<GraphXVertex>(x, y));
            _curNode = _rootNode;

            if (!graph.Vertices.Any())
            {
                MessageBox.Show("Graph is empty.");
                return;
            }


            DrawGraph();
            DrawTree();

            _hasStarted = true;
        }

        public void NextStep()
        {
            _curNode = Algorithm.next((Node)_curNode);

            DrawGraph();
            DrawTree();
        }

        public void PreviousStep()
        {
            _curNode = Algorithm.prev((Node)_curNode);

            DrawGraph();
            DrawTree();
        }

        private void PaintComponents(GraphArea area)
        {
            foreach (KeyValuePair<VertexWithComponent, GraphX.Controls.VertexControl> pair in area.VertexList)
            {
                switch (pair.Key.Component)
                {
                    case Component.LeftChild:
                    case Component.TreeLeftChild:
                        pair.Value.Background = new SolidColorBrush(Colors.Aqua);
                        break;
                    case Component.RightChild:
                    case Component.TreeRightChild:
                        pair.Value.Background = new SolidColorBrush(Colors.YellowGreen);
                        break;
                    case Component.Parent:
                    case Component.TreeParent:
                        pair.Value.Background = new SolidColorBrush(Colors.Orange);
                        break;
                    case Component.TreeRoot:
                        pair.Value.Background = new SolidColorBrush(Colors.Olive);
                        break;
                    case Component.Changed:
                        pair.Value.Background = new SolidColorBrush(Colors.OrangeRed);
                        break;
                }
            }
        }

        private void RemoveArrows()
        {
            foreach (KeyValuePair<GraphXEdge<VertexWithComponent>, GraphX.Controls.EdgeControl> pair in _graphArea.EdgesList)
            {
                pair.Value.ShowArrows = false;
            }

            _graphArea.UpdateAllEdges();
        }

        public bool CanGoBack => _hasStarted && (!Algorithm.isFirstNode(_curNode));
        public bool CanGoFurther => _hasStarted && (!Algorithm.isLastNode(_curNode));

        private void DrawGraph()
        {
            var graphToDraw = new DrawGraph();
            var allEdgesList = new List<GraphXEdge<VertexWithComponent>>();
            var neededEdgesList = new List<GraphXEdge<VertexWithComponent>>();
            
            if (_curNode is Node)
            {
                var leftChild = ((Node)_curNode).LeftChild;
                var rightChild = ((Node)_curNode).RightChild;
                AddComponentToGraph(_curNode.Graph, graphToDraw, Component.Parent, neededEdgesList, allEdgesList, ((Node)_curNode).VerticesToPaintIfParent);
                AddComponentToGraph(leftChild.Graph, graphToDraw, Component.LeftChild, neededEdgesList, allEdgesList, leftChild.VerticesToPaintIfChild);
                AddComponentToGraph(rightChild.Graph, graphToDraw, Component.RightChild, neededEdgesList, allEdgesList, rightChild.VerticesToPaintIfChild);
            }
            else
            {
                AddComponentToGraph(_curNode.Graph, graphToDraw, Component.Parent, neededEdgesList, allEdgesList, _curNode.VerticesToPaintIfChild);
            }
            _graphArea.LogicCore.Graph = graphToDraw;
            _graphArea.GenerateGraph();
            _graphZoomControl.ZoomToFill();

            SetVisibility(allEdgesList, System.Windows.Visibility.Hidden);
            SetVisibility(neededEdgesList, System.Windows.Visibility.Visible);
            RemoveArrows();
            PaintComponents(_graphArea);
        }

        private void DrawTree()
        {
            var treeToDraw = new DrawGraph();
            AddTree(treeToDraw);
            _treeArea.LogicCore.Graph = treeToDraw;
            _treeArea.GenerateGraph();
            _treeZoomControl.ZoomToFill();

            PaintComponents(_treeArea);
        }

        private void AddTree(DrawGraph graph)
        {
            var rootVertex = new VertexWithComponent(string.Join(",", _rootNode.CromaticPolinomial.ToArray()), Component.TreeRoot);
            graph.AddVertex(rootVertex);
            if (_curNode is Node)
            {
                AddNodeChilds(graph, (Node)_rootNode, rootVertex);
            }
        }

        private void AddNodeChilds(DrawGraph graph, Node parent, VertexWithComponent parentVertex)
        {
            VertexWithComponent leftVertex = null;
            VertexWithComponent rightVertex = null;
            if (parent == _curNode)
            {
                parentVertex.Component = Component.TreeParent;
                leftVertex = new VertexWithComponent(string.Join(",", parent.LeftChild.CromaticPolinomial.ToArray()), Component.TreeLeftChild);
                rightVertex = new VertexWithComponent(string.Join(",", parent.RightChild.CromaticPolinomial.ToArray()), Component.TreeRightChild);
            } else if (parentVertex.Component == Component.TreeRoot)
            {
                leftVertex = new VertexWithComponent(string.Join(",", parent.LeftChild.CromaticPolinomial.ToArray()), Component.TreeRest);
                rightVertex = new VertexWithComponent(string.Join(",", parent.RightChild.CromaticPolinomial.ToArray()), Component.TreeRest);
            }
            else
            {
                leftVertex = new VertexWithComponent(string.Join(",", parent.LeftChild.CromaticPolinomial.ToArray()), parentVertex.Component);
                rightVertex = new VertexWithComponent(string.Join(",", parent.RightChild.CromaticPolinomial.ToArray()), parentVertex.Component);
            }
            graph.AddVertex(leftVertex);
            graph.AddVertex(rightVertex);
            graph.AddEdge(new GraphXEdge<VertexWithComponent>(parentVertex, leftVertex));
            graph.AddEdge(new GraphXEdge<VertexWithComponent>(parentVertex, rightVertex));
            if (parent.LeftChild is Node)
            {
                AddNodeChilds(graph, (Node)parent.LeftChild, leftVertex);
            }
            if (parent.RightChild is Node)
            {
                AddNodeChilds(graph, (Node)parent.RightChild, rightVertex);
            }
        }

        private void AddComponentToGraph(Graph component, DrawGraph graph, Component componentName,
            List<GraphXEdge<VertexWithComponent>> neededEdgesList, List<GraphXEdge<VertexWithComponent>> allEdgesList, IEnumerable<GraphXVertex> verticesToPaint)
        {
            var vertexDictionary = new Dictionary<string, VertexWithComponent>();
            var componentEdges = new List<GraphXEdge<VertexWithComponent>>();

            foreach (GraphXVertex vertex in component.Vertices)
            {
                var newVertex = new VertexWithComponent(vertex.Text, componentName);
                if (verticesToPaint.Contains(vertex))
                {
                    newVertex.Component = Component.Changed;
                }
                vertexDictionary.Add(vertex.Text, newVertex);
                graph.AddVertex(newVertex);
            }
            AddAllEdges(graph, vertexDictionary, allEdgesList, componentEdges);
            AddNeededEdges(component, componentEdges, neededEdgesList);
        }

        private void AddAllEdges(DrawGraph graph, Dictionary<string, VertexWithComponent> dictionary, List<GraphXEdge<VertexWithComponent>> allEdgesList,
            List<GraphXEdge<VertexWithComponent>> componentEdges)
        {
            foreach (KeyValuePair<string, VertexWithComponent> pair in dictionary)
            {
                foreach (KeyValuePair<string, VertexWithComponent> innerPair in dictionary)
                {
                    if (!pair.Key.Equals(innerPair.Key))
                    {
                        var edge = new GraphXEdge<VertexWithComponent>(pair.Value, innerPair.Value);
                        graph.AddEdge(edge);
                        allEdgesList.Add(edge);
                        componentEdges.Add(edge);
                    }
                }
            }
        }

        private void AddNeededEdges(Graph graph, List<GraphXEdge<VertexWithComponent>> allEdgesList, List<GraphXEdge<VertexWithComponent>> neededEdgesList)
        {
            foreach (GraphXEdge<GraphXVertex> edge in graph.Edges)
            {
                neededEdgesList.Add(FindEdge(allEdgesList, edge.Source, edge.Target));
                neededEdgesList.Add(FindEdge(allEdgesList, edge.Target, edge.Source));
            }
        }

        private GraphXEdge<VertexWithComponent> FindEdge(List<GraphXEdge<VertexWithComponent>> allEdgesList, GraphXVertex source, GraphXVertex target)
        {
            foreach (GraphXEdge<VertexWithComponent> edge in allEdgesList)
            {
                if (edge.Source.Text.Equals(source.Text) && edge.Target.Text.Equals(target.Text))
                {
                    return edge;
                }
            }
            return null;
        }

        private void SetVisibility(List<GraphXEdge<VertexWithComponent>> edgesList, System.Windows.Visibility visibility)
        {
            foreach (GraphXEdge<VertexWithComponent> edge in edgesList)
            {
                _graphArea.EdgesList[edge].Visibility = visibility;
            }
        }

    }
}

