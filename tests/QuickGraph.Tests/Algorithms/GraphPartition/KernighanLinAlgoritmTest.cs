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
        private UndirectedGraph<int, TaggedUndirectedEdge<int, double>> createGraph(List<TaggedUndirectedEdge<int, double>> edges)
        {
            var g = new UndirectedGraph<int, TaggedUndirectedEdge<int, double>>(true);
            foreach (TaggedUndirectedEdge<int, double> edge in edges)
            {
                g.AddVerticesAndEdge(edge);
            }
            return g;
        }

        private bool isEqual(Partition<int> p1, Partition<int> p2){

            return p1.A.SetEquals(p2.A) && p1.B.SetEquals(p2.B) || p1.A.SetEquals(p2.B) && p1.B.SetEquals(p2.A);
        }


        [TestMethod]
        public void GraphPartitioningTest1()
        {
            var edges = new List<TaggedUndirectedEdge<int, double>> { new TaggedUndirectedEdge<int,double>(0, 1, 100),
                                                                      new TaggedUndirectedEdge<int,double>(1, 2, 20),
                                                                      new TaggedUndirectedEdge<int,double>(2, 3, 10),
                                                                      new TaggedUndirectedEdge<int,double>(3, 1, 50)};
            var graph = createGraph(edges);
            var A = new SortedSet<int>();
            var B = new SortedSet<int>();
            A.Add(0); A.Add(1);
            B.Add(3); B.Add(2);
            var expected = new Partition<int>(A, B, 3);

            var algo = new KernighanLinAlgoritm<int, double>(graph, 1);
            var test = algo.Execute();

            Assert.IsTrue(isEqual(test,expected));
        }

        
        [TestMethod]
        public void GraphPartitioningTest2()
        {
            var edges = new List<TaggedUndirectedEdge<int, double>> { new TaggedUndirectedEdge<int, double>(0, 1, 1),
                                                                      new TaggedUndirectedEdge<int, double>(1, 2, 1),
                                                                      new TaggedUndirectedEdge<int, double>(2, 3, 1),
                                                                      new TaggedUndirectedEdge<int, double>(3, 4, 1),
                                                                      new TaggedUndirectedEdge<int, double>(4, 0, 1),
                                                                      new TaggedUndirectedEdge<int, double>(4, 1, 1) };
            
            var graph = createGraph(edges);
            var A = new SortedSet<int>();
            var B = new SortedSet<int>();
            for (int i = 0; i < 5; i++)
            {
                if (i == 2 || i == 3) A.Add(i);
                else B.Add(i);
            }
            var expected = new Partition<int>(A, B, 3);
            var algo = new KernighanLinAlgoritm<int, double>(graph, 1);
            var test = algo.Execute();

            Assert.IsTrue(isEqual(test, expected));
        }


        [TestMethod]
        public void GraphPartitioningTest3()
        {
            var edges = new List<TaggedUndirectedEdge<int, double>>{ new TaggedUndirectedEdge<int, double>(0, 1, 1),
                                                                     new TaggedUndirectedEdge<int, double>(1, 2, 50),
                                                                     new TaggedUndirectedEdge<int, double>(1, 4, 5),
                                                                     new TaggedUndirectedEdge<int, double>(4, 3, 1),
                                                                     new TaggedUndirectedEdge<int, double>(3, 6, 10),
                                                                     new TaggedUndirectedEdge<int, double>(4, 5, 1),
                                                                     new TaggedUndirectedEdge<int, double>(4, 7, 25),
                                                                     new TaggedUndirectedEdge<int, double>(4, 8, 100),
                                                                     new TaggedUndirectedEdge<int, double>(5, 7, 1),
                                                                     new TaggedUndirectedEdge<int, double>(5, 8, 3),
                                                                     new TaggedUndirectedEdge<int, double>(6, 7, 1),
                                                                     new TaggedUndirectedEdge<int, double>(6, 9, 2),
                                                                     new TaggedUndirectedEdge<int, double>(7, 8, 1),
                                                                     new TaggedUndirectedEdge<int, double>(7, 10, 5),
                                                                     new TaggedUndirectedEdge<int, double>(8, 11, 1), };
            var graph = createGraph(edges);
            var A = new SortedSet<int>();
            var B = new SortedSet<int>();
            for (int i = 0; i < 12; i++)
            {
                if (i < 4 || i == 6 || i == 9) B.Add(i);
                else A.Add(i);
            }

            var expected = new Partition<int>(A, B, 3);
            var algo = new KernighanLinAlgoritm<int, double>(graph, 1);
            var test = algo.Execute();

            Assert.IsTrue(isEqual(test, expected));

        }
    }
}
/*
 9   10        11
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
