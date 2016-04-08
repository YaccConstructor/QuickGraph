using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuickGraph.Algorithms.GraphColoring.VertexColoring;

namespace QuickGraph.Tests.Algorithms.GraphColoring
{
    [TestClass]
    public class VertexColoringAlgorithmTest
    {
        [TestMethod]
        public void VertexColoringCompute()
        {
            /* 
                                                 (1)
                                                / | \ 
            Generate undirected simle graph: (0)  |  (3)-(4)
                                                \ | /
                                                 (2)
            */
            var input = GenerateInput();
            var grafWithColoredVertices = new VertexColoringAlgorithm<char, Edge<char>>(input).Compute();
            var result = grafWithColoredVertices.Colors.Values.ToArray();

            // Expecting to get 3 diferent colors
            Assert.AreEqual(3, result.Max() + 1);

            /* 
            and corresponding colors of vertices:
            0 vertex = 0 color
            1 vertex = 1 color
            2 vertex = 2 color
            3 vertex = 0 color
            4 vertex = 1 color
            */
            Assert.AreEqual(0, result[0]);
            Assert.AreEqual(1, result[1]);
            Assert.AreEqual(2, result[2]);
            Assert.AreEqual(0, result[3]);
            Assert.AreEqual(1, result[4]);
        }

        private InputModel<char, Edge<char>> GenerateInput()
        {
            var g = new UndirectedGraph<char, Edge<char>>(true);
            var colorsOfVertex = new Dictionary<char, int>();


            AddColoredVertex(g, colorsOfVertex, '0'); // 1 Vertex
            AddColoredVertex(g, colorsOfVertex, '1'); // 2 Vertex
            AddColoredVertex(g, colorsOfVertex, '2'); // 3 Vertex
            AddColoredVertex(g, colorsOfVertex, '3'); // 4 Vertex
            AddColoredVertex(g, colorsOfVertex, '4'); // 5 Vertex

            AddEdgeBnColoredVertices(g, '0', '1'); // 1 Edge
            AddEdgeBnColoredVertices(g, '0', '2'); // 2 Edge
            AddEdgeBnColoredVertices(g, '1', '2'); // 3 Edge
            AddEdgeBnColoredVertices(g, '1', '3'); // 4 Edge
            AddEdgeBnColoredVertices(g, '2', '3'); // 5 Edge
            AddEdgeBnColoredVertices(g, '3', '4'); // 6 Edge

            return new InputModel<char, Edge<char>>
            {
                Graph = g,
                Colors = colorsOfVertex
            };
        }
        private void AddColoredVertex(
               UndirectedGraph<char, Edge<char>> g,
               Dictionary<char, int> colorsOfVertex,
               char vertex)
        {
            var ac = vertex;
            colorsOfVertex[ac] = 0;
            g.AddVertex(ac);
        }
        private void AddEdgeBnColoredVertices(
              UndirectedGraph<char, Edge<char>> g,
              char source, char target)
        {
            var ac = new Edge<char>(source, target);
            g.AddEdge(ac);
        }

    }
}