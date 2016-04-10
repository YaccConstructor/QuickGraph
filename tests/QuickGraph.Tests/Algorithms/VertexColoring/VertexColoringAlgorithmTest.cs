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
        public void VertexColoringComputeSimpleGraph()
        {
            /* 
                                                  (1)
                                                 / | \ 
            Generate undirected simple graph: (0)  |  (3)-(4)
                                                 \ | /
                                                  (2)
            */
            var input = GenerateInputSimple();
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
            
            // and corresponding colors of vertices
            Assert.AreEqual(0, result[0]); // 0 vertex = 0 color
            Assert.AreEqual(1, result[1]); // 1 vertex = 1 color
            Assert.AreEqual(2, result[2]); // 2 vertex = 2 color
            Assert.AreEqual(0, result[3]); // 3 vertex = 0 color
            Assert.AreEqual(1, result[4]); // 4 vertex = 1 color
        }

        private InputModel<char, Edge<char>> GenerateInputSimple()
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

        [TestMethod]
        public void VertexColoringComputeEmptyGraph()
        {
            /* 
                                      (1)
                                                     
            Generate empty graph: (0)     (3) (4)
                                                     
                                      (2)
            */
            var input = GenerateInputEmpty();
            var grafWithColoredVertices = new VertexColoringAlgorithm<char, Edge<char>>(input).Compute();

            // Graph doesn't have first vertex color
            Assert.IsFalse(grafWithColoredVertices.Colors.Values.Contains(1));

            var result = grafWithColoredVertices.Colors.Values.ToArray();

            // Expecting to get only 1 color
            Assert.AreEqual(1, result.Max() + 1);

            // not equal to null 
            foreach (var color in result)
            {
                Assert.AreNotEqual(null, color);
            }

            // and corresponding colors of vertices
            Assert.AreEqual(0, result[0]); // 0 vertex = 0 color 
            Assert.AreEqual(0, result[1]); // 1 vertex = 0 color 
            Assert.AreEqual(0, result[2]); // 2 vertex = 0 color 
            Assert.AreEqual(0, result[3]); // 3 vertex = 0 color 
            Assert.AreEqual(0, result[4]); // 4 vertex = 0 color 
        }

        private InputModel<char, Edge<char>> GenerateInputEmpty()
        {
            var g = new UndirectedGraph<char, Edge<char>>(true);

            g.AddVertex('0'); // 1 Vertex
            g.AddVertex('1'); // 2 Vertex
            g.AddVertex('2'); // 3 Vertex
            g.AddVertex('3'); // 4 Vertex
            g.AddVertex('4'); // 5 Vertex

            return new InputModel<char, Edge<char>>
            {
                Graph = g
            };
        }

        [TestMethod]
        public void VertexColoringComputeFullGraph()
        {
            /* 
                                                _____(2)_____
                                               /    / | \    \
            Generate undirected full graph:  (0)-(1)--+--(4)-(5)  + edges: (0-4), (0-5) and (1-5)
                                               \    \ | /    /
                                                \____(3)____/
            */
            var input = GenerateInputFull();
            var grafWithColoredVertices = new VertexColoringAlgorithm<char, Edge<char>>(input).Compute();

            // Graph doesn't have sixth vertex color
            Assert.IsFalse(grafWithColoredVertices.Colors.Values.Contains(6));

            var result = grafWithColoredVertices.Colors.Values.ToArray();

            // Expecting to get 6 diferent colors
            Assert.AreEqual(6, result.Max() + 1);

            // not equal to null 
            foreach (var color in result)
            {
                Assert.AreNotEqual(null, color);
            }

            // and corresponding colors of vertices
            Assert.AreEqual(0, result[0]); // 0 vertex = 0 color
            Assert.AreEqual(1, result[1]); // 1 vertex = 1 color
            Assert.AreEqual(2, result[2]); // 2 vertex = 2 color
            Assert.AreEqual(3, result[3]); // 3 vertex = 3 color
            Assert.AreEqual(4, result[4]); // 4 vertex = 4 color
            Assert.AreEqual(5, result[5]); // 5 vertex = 5 color
        }

        private InputModel<char, Edge<char>> GenerateInputFull()
        {
            var g = new UndirectedGraph<char, Edge<char>>(true);

            g.AddVertex('0'); // 1 Vertex
            g.AddVertex('1'); // 2 Vertex
            g.AddVertex('2'); // 3 Vertex
            g.AddVertex('3'); // 4 Vertex
            g.AddVertex('4'); // 5 Vertex
            g.AddVertex('5'); // 6 Vertex

            g.AddEdge(new Edge<char>('0', '1')); // 1  Edge
            g.AddEdge(new Edge<char>('0', '2')); // 2  Edge
            g.AddEdge(new Edge<char>('0', '3')); // 3  Edge
            g.AddEdge(new Edge<char>('0', '4')); // 4  Edge
            g.AddEdge(new Edge<char>('0', '5')); // 5  Edge
            g.AddEdge(new Edge<char>('1', '2')); // 6  Edge
            g.AddEdge(new Edge<char>('1', '3')); // 7  Edge
            g.AddEdge(new Edge<char>('1', '4')); // 8  Edge
            g.AddEdge(new Edge<char>('1', '5')); // 9  Edge
            g.AddEdge(new Edge<char>('2', '3')); // 10 Edge
            g.AddEdge(new Edge<char>('2', '4')); // 11 Edge
            g.AddEdge(new Edge<char>('2', '5')); // 12 Edge
            g.AddEdge(new Edge<char>('3', '4')); // 13 Edge
            g.AddEdge(new Edge<char>('3', '5')); // 14 Edge
            g.AddEdge(new Edge<char>('4', '5')); // 15 Edge

            return new InputModel<char, Edge<char>>
            {
                Graph = g
            };
        }

        [TestMethod]
        public void VertexColoringComputeBipartiteGraph()
        {
            /*                                   
                                                 (3)
                                                / 
                                             (1)-(4)
                                                X     
            Generate undirected empty graph: (0)-(5)    + edges: (1-6) and (2-4)
                                                /     
                                             (2)-(6)
            
            */

            var input = GenerateInputBipartite();
            var grafWithColoredVertices = new VertexColoringAlgorithm<char, Edge<char>>(input).Compute();

            // Graph doesn't have second vertex color
            Assert.IsFalse(grafWithColoredVertices.Colors.Values.Contains(2));

            var result = grafWithColoredVertices.Colors.Values.ToArray();

            // Expecting to get 2 diferent colors
            Assert.AreEqual(2, result.Max() + 1);

            // not equal to null 
            foreach (var color in result)
            {
                Assert.AreNotEqual(null, color);
            }

            // and corresponding colors of vertices
            Assert.AreEqual(0, result[0]); // 0 vertex = 0 color
            Assert.AreEqual(0, result[1]); // 1 vertex = 0 color
            Assert.AreEqual(0, result[2]); // 2 vertex = 0 color
            Assert.AreEqual(1, result[3]); // 3 vertex = 1 color
            Assert.AreEqual(1, result[4]); // 4 vertex = 1 color
            Assert.AreEqual(1, result[5]); // 5 vertex = 1 color
            Assert.AreEqual(1, result[6]); // 6 vertex = 1 color
        }

        private InputModel<char, Edge<char>> GenerateInputBipartite()
        {
            var g = new UndirectedGraph<char, Edge<char>>(true);

            g.AddVertex('0'); // 1 Vertex
            g.AddVertex('1'); // 2 Vertex
            g.AddVertex('2'); // 3 Vertex
            g.AddVertex('3'); // 4 Vertex
            g.AddVertex('4'); // 5 Vertex
            g.AddVertex('5'); // 6 Vertex
            g.AddVertex('6'); // 7 Vertex

            g.AddEdge(new Edge<char>('0', '4')); // 1 Edge
            g.AddEdge(new Edge<char>('0', '5')); // 2 Edge
            g.AddEdge(new Edge<char>('1', '3')); // 3 Edge
            g.AddEdge(new Edge<char>('1', '4')); // 4 Edge
            g.AddEdge(new Edge<char>('1', '5')); // 5 Edge
            g.AddEdge(new Edge<char>('1', '6')); // 6 Edge
            g.AddEdge(new Edge<char>('2', '5')); // 7 Edge
            g.AddEdge(new Edge<char>('2', '6')); // 8 Edge
            g.AddEdge(new Edge<char>('2', '4')); // 9 Edge

            return new InputModel<char, Edge<char>>
            {
                Graph = g
            };
        }

        [TestMethod]
        public void VertexColoringComputeTestGraph()
        {
            /* 
                                                  (2)      (7)-(5)
                                                 /   \     /
            Generate undirected some graph:    (1)   (4)-(0)
                                                 \   /
                                             (6)  (3)
            
            (this graph has a minimum number of vertex colors only if to swap (1) and (4) vertices)
            */
            var input = GenerateInputTest();
            var grafWithColoredVertices = new VertexColoringAlgorithm<char, Edge<char>>(input).Compute();

            // Graph doesn't have third vertex color
            Assert.IsFalse(grafWithColoredVertices.Colors.Values.Contains(3));

            var result = grafWithColoredVertices.Colors.Values.ToArray();

            // Expecting to get 3 diferent colors
            Assert.AreEqual(3, result.Max() + 1);

            // not equal to null 
            foreach (var color in result)
            {
                Assert.AreNotEqual(null, color);
            }

            //and corresponding colors of vertices
            Assert.AreEqual(0, result[0]); // 0 vertex = 0 color
            Assert.AreEqual(0, result[1]); // 1 vertex = 0 color
            Assert.AreEqual(1, result[2]); // 2 vertex = 1 color
            Assert.AreEqual(1, result[3]); // 3 vertex = 1 color
            Assert.AreEqual(2, result[4]); // 4 vertex = 2 color
            Assert.AreEqual(0, result[5]); // 5 vertex = 0 color
            Assert.AreEqual(0, result[6]); // 6 vertex = 0 color
            Assert.AreEqual(1, result[7]); // 7 vertex = 1 color
        }

        private InputModel<char, Edge<char>> GenerateInputTest()
        {
            var g = new UndirectedGraph<char, Edge<char>>(true);

            g.AddVertex('0'); // 1 Vertex
            g.AddVertex('1'); // 2 Vertex
            g.AddVertex('2'); // 3 Vertex
            g.AddVertex('3'); // 4 Vertex
            g.AddVertex('4'); // 5 Vertex
            g.AddVertex('5'); // 6 Vertex
            g.AddVertex('6'); // 7 Vertex
            g.AddVertex('7'); // 8 Vertex

            g.AddEdge(new Edge<char>('0', '4')); // 1 Edge
            g.AddEdge(new Edge<char>('1', '2')); // 2 Edge
            g.AddEdge(new Edge<char>('1', '3')); // 3 Edge
            g.AddEdge(new Edge<char>('2', '4')); // 4 Edge
            g.AddEdge(new Edge<char>('3', '4')); // 5 Edge
            g.AddEdge(new Edge<char>('5', '7')); // 6 Edge
            g.AddEdge(new Edge<char>('7', '0')); // 7 Edge

            return new InputModel<char, Edge<char>>
            {
                Graph = g
            };
        }
    }
}