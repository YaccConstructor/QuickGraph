using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuickGraph.Algorithms;
using System.Collections.Generic;

namespace QuickGraph.Tests
{
    [TestClass]
    public class DomaticPartitionTest
    {
        public HashSet<HashSet<int>> GetMaxDomaticPartition(int[] vertices, Tuple<int, int>[] edges)
        {
            var graph = new UndirectedGraph<int, Edge<int>>();
            graph.AddVertexRange(vertices);
            foreach (var edge in edges)
            {
                graph.AddVerticesAndEdge(new Edge<int>(edge.Item1, edge.Item2));
            }

            var algo = new DomaticPartition<int, Edge<int>>(graph);
            IEnumerable<IEnumerable<int>> partition = algo.GetMaxSizePartition();

            var hashsetPartition = new HashSet<HashSet<int>>();
            foreach (var set in partition)
            {
                var curSet = new HashSet<int>(set);
                hashsetPartition.Add(curSet);
            }

            return hashsetPartition;
        }

        public bool SetOfSetEquals(HashSet<HashSet<int>> set, List<HashSet<int>> tocompare)
        {
            foreach (var subset in set)
            {
                int i;
                for (i = tocompare.Count - 1; i >= 0; i--)
                {
                    if (tocompare[i].SetEquals(subset))
                    {
                        tocompare.RemoveAt(i);
                        break;
                    }
                }
                if (i == -1) // not found
                {
                    return false;
                }
            }

            return tocompare.Count == 0;
        }

        [TestMethod]
        public void DomaticPartitionTest1()
        {
            int[] vertices = { 1, 2, 3, 4, 5, 6, 7 };
            Tuple<int, int>[] edges =
            {
                new Tuple<int, int>(1, 2),
                new Tuple<int, int>(1, 4),
                new Tuple<int, int>(1, 6),
                new Tuple<int, int>(2, 3),
                new Tuple<int, int>(3, 4),
                new Tuple<int, int>(3, 7),
                new Tuple<int, int>(4, 5),
                new Tuple<int, int>(4, 6),
                new Tuple<int, int>(4, 7),
                new Tuple<int, int>(5, 7),
                new Tuple<int, int>(6, 7),
            };

            var tocompare = new List<HashSet<int>>(new HashSet<int>[] 
            { 
                new HashSet<int>(new int[] { 3, 5, 6 }), 
                new HashSet<int>(new int[] { 1, 4 }),
                new HashSet<int>(new int[] { 2, 7 })
            });

            var partition = GetMaxDomaticPartition(vertices, edges);

            Assert.AreEqual(partition.Count, 3);
            Assert.IsTrue(SetOfSetEquals(partition, tocompare));
        }

        [TestMethod]
        public void DomaticPartitionTest2()
        {
            int[] vertices = { 1, 2, 3, 4, 5, 6, 7, 8 };
            Tuple<int, int>[] edges =
            {
                new Tuple<int, int>(1, 2),
                new Tuple<int, int>(1, 3),
                new Tuple<int, int>(1, 4),
                new Tuple<int, int>(2, 3),
                new Tuple<int, int>(2, 4),
                new Tuple<int, int>(2, 5),
                new Tuple<int, int>(3, 4),
                new Tuple<int, int>(3, 8),
                new Tuple<int, int>(4, 5),
                new Tuple<int, int>(4, 6),
                new Tuple<int, int>(4, 8),
                new Tuple<int, int>(5, 6),
                new Tuple<int, int>(5, 7),
                new Tuple<int, int>(5, 8),
                new Tuple<int, int>(6, 7),
                new Tuple<int, int>(6, 8),
                new Tuple<int, int>(7, 8)
            };

            // All possible domatic partitions for a given graph
            int[,] domatics = 
            { 
                { 1, 5, 2, 8, 4, 6, 3, 7 },
                { 1, 5, 2, 8, 4, 7, 3, 6 },
                { 1, 5, 2, 6, 4, 8, 3, 7 },
                { 1, 5, 2, 6, 4, 7, 3, 8 },
                { 1, 5, 2, 7, 4, 8, 3, 6 },
                { 1, 5, 2, 7, 4, 6, 3, 8 },
                { 1, 8, 2, 5, 4, 6, 3, 7 },
                { 1, 8, 2, 5, 4, 7, 3, 6 },
                { 1, 8, 2, 6, 4, 5, 3, 7 },
                { 1, 8, 2, 6, 4, 7, 3, 5 },
                { 1, 8, 2, 7, 4, 5, 3, 6 },
                { 1, 8, 2, 7, 4, 6, 3, 5 },
                { 1, 6, 2, 5, 4, 8, 3, 7 },
                { 1, 6, 2, 5, 4, 7, 3, 8 },
                { 1, 6, 2, 8, 4, 5, 3, 7 },
                { 1, 6, 2, 8, 4, 7, 3, 5 },
                { 1, 6, 2, 7, 4, 5, 3, 8 },
                { 1, 6, 2, 7, 4, 8, 3, 5 },
                { 1, 7, 2, 5, 4, 8, 3, 6 },
                { 1, 7, 2, 5, 4, 6, 3, 8 },
                { 1, 7, 2, 8, 4, 5, 3, 6 },
                { 1, 7, 2, 8, 4, 6, 3, 5 },
                { 1, 7, 2, 6, 4, 5, 3, 8 },
                { 1, 7, 2, 6, 4, 8, 3, 5 }
            };

            var tocompare = new List<List<HashSet<int>>>();
            for (int i = 0; i < domatics.GetLength(0); i++)
            {
                var curPartition = new List<HashSet<int>>();
                for (int j = 0; j < domatics.GetLength(1) / 2; j++)
                {
                    curPartition.Add(new HashSet<int>(
                        new int[] { domatics[i, 2 * j], domatics[i, 2 * j + 1] }
                    ));
                }
                tocompare.Add(curPartition);
            }

            var partition = GetMaxDomaticPartition(vertices, edges);

            Assert.AreEqual(partition.Count, 4);

            bool found = false;
            foreach (var tocomp in tocompare)
            {
                if (SetOfSetEquals(partition, tocomp))
                {
                    found = true;
                    break;
                }
            }
            Assert.IsTrue(found);
        }

        [TestMethod]
        public void DomaticPartitionTest3()
        {
            int[] vertices = { 1, 2, 3, 4, 5, 6, 7, 8 };
            Tuple<int, int>[] edges =
            {
                new Tuple<int, int>(1, 2),
                new Tuple<int, int>(1, 3),
                new Tuple<int, int>(2, 4),
                new Tuple<int, int>(2, 5),
                new Tuple<int, int>(2, 6),
                new Tuple<int, int>(3, 4),
                new Tuple<int, int>(5, 6),
                new Tuple<int, int>(6, 7),
                new Tuple<int, int>(6, 8),
            };

            var partition = GetMaxDomaticPartition(vertices, edges);

            Assert.AreEqual(partition.Count, 2);
        }
    }
}
