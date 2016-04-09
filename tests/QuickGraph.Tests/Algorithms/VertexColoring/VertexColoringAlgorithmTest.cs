using System;
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

            // Graph doesn't have third vertex color
            Assert.IsFalse(grafWithColoredVertices.Colors.Values.Contains(3));

            var result = grafWithColoredVertices.Colors.Values.ToArray();

            // Expecting to get 3 diferent colors
            Assert.AreEqual(3, result.Max() + 1);

            // not equal to null 
            foreach(var color in result)
            {
                Assert.AreNotEqual(null, color);
            }            
            
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

            g.AddVertex('0'); // 1 Vertex
            g.AddVertex('1'); // 2 Vertex
            g.AddVertex('2'); // 3 Vertex
            g.AddVertex('3'); // 4 Vertex
            g.AddVertex('4'); // 5 Vertex

            g.AddEdge(new Edge<char>('0', '1')); // 1 Edge
            g.AddEdge(new Edge<char>('0', '2')); // 2 Edge
            g.AddEdge(new Edge<char>('1', '2')); // 3 Edge
            g.AddEdge(new Edge<char>('1', '3')); // 4 Edge
            g.AddEdge(new Edge<char>('2', '3')); // 5 Edge
            g.AddEdge(new Edge<char>('3', '4')); // 6 Edge

            return new InputModel<char, Edge<char>>
            {
                Graph = g
            };
        }
    }
}