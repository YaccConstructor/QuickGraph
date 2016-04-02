using System;
using System.Collections.Generic;
using System.Linq;
using QuickGraph.Algorithms.KernighanLinAlgoritm;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace QuickGraph.Tests.Algorithms.GraphPartitioning
{

    [TestClass]
    public class KernighanLinAlgoritmTest
    {
        private bool isEqual(Partition<string> p1, Partition<string> p2)
        {
            return p1.A.SetEquals(p2.A) && p1.B.SetEquals(p2.B);
        }
        [TestMethod]
        public void GraphPartitioningTest1()
        {
            var g = new UndirectedGraph<string, Edge<string>>(true);
            for (int i = 0; i < 4; i++)
            {
                g.AddVertex(i.ToString());

            }
            Edge<string> ed0_1 = new Edge<string>("0", "1");
            Edge<string> ed1_2 = new Edge<string>("1", "2");
            Edge<string> ed2_3 = new Edge<string>("2", "3");
            Edge<string> ed3_1 = new Edge<string>("3", "1");
            g.AddEdge(ed0_1);
            g.AddEdge(ed1_2);
            g.AddEdge(ed2_3);
            g.AddEdge(ed3_1);
            Dictionary<Edge<string>, int> edgeCost = new Dictionary<Edge<string>, int>(g.EdgeCount);
            edgeCost.Add(ed0_1, 1);
            edgeCost.Add(ed1_2, 1);
            edgeCost.Add(ed2_3, 1);
            edgeCost.Add(ed3_1, 5);
            var graph = new GraphWithWeights<string, Edge<string>>(g, edgeCost);
            var A = new SortedSet<string>();
            var B = new SortedSet<string>();
            for (int i = 0; i < 4; i++)
            {
                if (i == 1 || i == 3) A.Add(i.ToString());
                else B.Add(i.ToString());
            }
            var expected = new Partition<string>(A, B, 3);
            var algo = new KernighanLinAlgoritm<string, Edge<string>>(graph, 1);
            var test = algo.Execute();

            Assert.IsTrue(isEqual(test, expected));
        }

        [TestMethod]
        public void GraphPartitioningTest2()
        {
            var g = new UndirectedGraph<string, Edge<string>>(true);
            for (int i = 0; i < 5; i++)
            {
                g.AddVertex(i.ToString());

            }
            Edge<string> ed0_1 = new Edge<string>("0", "1");
            Edge<string> ed1_2 = new Edge<string>("1", "2");
            Edge<string> ed2_3 = new Edge<string>("2", "3");
            Edge<string> ed3_4 = new Edge<string>("3", "4");
            Edge<string> ed4_0 = new Edge<string>("4", "0");
            Edge<string> ed4_1 = new Edge<string>("4", "1");
            g.AddEdge(ed0_1);
            g.AddEdge(ed1_2);
            g.AddEdge(ed2_3);
            g.AddEdge(ed3_4);
            g.AddEdge(ed4_0);
            g.AddEdge(ed4_1);
            Dictionary<Edge<string>, int> edgeCost = new Dictionary<Edge<string>, int>(g.EdgeCount);
            edgeCost.Add(ed0_1, 1);
            edgeCost.Add(ed1_2, 1);
            edgeCost.Add(ed2_3, 1);
            edgeCost.Add(ed3_4, 1);
            edgeCost.Add(ed4_0, 1);
            edgeCost.Add(ed4_1, 1);
            var graph = new GraphWithWeights<string, Edge<string>>(g, edgeCost);
            var A = new SortedSet<string>();
            var B = new SortedSet<string>();
            for (int i = 0; i < 5; i++)
            {
                if (i == 2 || i == 3) A.Add(i.ToString());
                else B.Add(i.ToString());
            }
            var expected = new Partition<string>(A, B, 3);
            var algo = new KernighanLinAlgoritm<string, Edge<string>>(graph, 1);
            var test = algo.Execute();

            Assert.IsTrue(isEqual(test, expected));
        }


        [TestMethod]
        public void GraphPartitioningTest3()
        {
            var g = new UndirectedGraph<string, Edge<string>>(true);
            for (int i = 0; i < 13; i++)
            {
                g.AddVertex(i.ToString());

            }
            Edge<string> ed0_1 = new Edge<string>("0", "1");
            Edge<string> ed1_2 = new Edge<string>("1", "2");
            Edge<string> ed1_4 = new Edge<string>("1", "4");
            Edge<string> ed4_3 = new Edge<string>("4", "3");
            Edge<string> ed3_6 = new Edge<string>("3", "6");
            Edge<string> ed4_5 = new Edge<string>("4", "5");
            Edge<string> ed4_7 = new Edge<string>("4", "7");
            Edge<string> ed4_8 = new Edge<string>("4", "8");
            Edge<string> ed5_7 = new Edge<string>("5", "7");
            Edge<string> ed5_8 = new Edge<string>("5", "8");
            Edge<string> ed6_7 = new Edge<string>("6", "7");
            Edge<string> ed6_9 = new Edge<string>("6", "9");
            Edge<string> ed7_8 = new Edge<string>("7", "8");
            Edge<string> ed7_10 = new Edge<string>("7", "10");
            Edge<string> ed8_11 = new Edge<string>("8", "11");
            Edge<string> ed9_12 = new Edge<string>("9", "12");
            g.AddEdge(ed0_1);
            g.AddEdge(ed1_2);
            g.AddEdge(ed1_4);
            g.AddEdge(ed4_3);
            g.AddEdge(ed4_5);
            g.AddEdge(ed4_7);
            g.AddEdge(ed4_8);
            g.AddEdge(ed3_6);
            g.AddEdge(ed5_7);
            g.AddEdge(ed5_8);
            g.AddEdge(ed6_7);
            g.AddEdge(ed6_9);
            g.AddEdge(ed7_8);
            g.AddEdge(ed7_10);
            g.AddEdge(ed8_11);
            g.AddEdge(ed9_12);
            Dictionary<Edge<string>, int> edgeCost = new Dictionary<Edge<string>, int>(g.EdgeCount);
            edgeCost.Add(ed0_1, 1);
            edgeCost.Add(ed1_2, 1);
            edgeCost.Add(ed1_4, 1);
            edgeCost.Add(ed4_3, 1);
            edgeCost.Add(ed4_5, 1);
            edgeCost.Add(ed4_7, 1);
            edgeCost.Add(ed4_8, 1);
            edgeCost.Add(ed3_6, 1);
            edgeCost.Add(ed5_7, 1);
            edgeCost.Add(ed5_8, 1);
            edgeCost.Add(ed6_7, 1);
            edgeCost.Add(ed6_9, 1);
            edgeCost.Add(ed7_8, 1);
            edgeCost.Add(ed7_10, 1);
            edgeCost.Add(ed8_11, 1);
            edgeCost.Add(ed9_12, 1);
            var graph = new GraphWithWeights<string, Edge<string>>(g, edgeCost);
            var A = new SortedSet<string>();
            var B = new SortedSet<string>();
            for (int i = 0; i < 13; i++)
            {
                if (i < 4 || i == 6 || i == 9 || i == 12) B.Add(i.ToString());
                else A.Add(i.ToString());
            }

            var expected = new Partition<string>(A, B, 3);
            var algo = new KernighanLinAlgoritm<string, Edge<string>>(graph, 1);
            var test = algo.Execute();

            Assert.IsTrue(isEqual(test, expected));

        }
    }
}
/*
 12
 * 
 * 
 * 
 9****10********11
 *    *         *
 *    *         *
 6****7*********8
 *    **       **
 *    *  *   *  *  
 *    *    *    *
 *    *  *   *  *
 *    **       **
 3****4*********5
      *         
      *         
 0****1*********2
*/
