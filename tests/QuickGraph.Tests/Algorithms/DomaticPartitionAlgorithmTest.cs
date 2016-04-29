using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuickGraph.Algorithms;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

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

        public bool SetOfSetsCheckEqual(HashSet<HashSet<int>> set, List<HashSet<int>> tocompare)
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
                if (i == -1) // subset not found
                {
                    return false;
                }
            }

            return tocompare.Count == 0;
        }

        public Tuple<int[], Tuple<int, int>[]> generateGraph(int vertexCount)
        {
            int[] vertices = Enumerable.Range(1, vertexCount + 1).ToArray();

            Random rnd = new Random();
            var edgesList = new List<Tuple<int, int>>();
            for (int i = 0; i < vertexCount; i++)
            {
                int randVertex1 = rnd.Next(1, vertexCount + 1);
                int randVertex2 = rnd.Next(1, vertexCount + 1);

                bool wasTuple = false;
                foreach (var edge in edgesList)
                {
                    if (edge.Item1 == randVertex1 && edge.Item2 == randVertex2 ||
                        edge.Item1 == randVertex2 && edge.Item2 == randVertex1)
                    {
                        wasTuple = true;
                        break;
                    }
                }

                if (randVertex1 != randVertex2 && !wasTuple)
                {
                    edgesList.Add(new Tuple<int, int>(randVertex1, randVertex2));
                }
            }

            return Tuple.Create(vertices, edgesList.ToArray());
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
            Assert.IsTrue(SetOfSetsCheckEqual(partition, tocompare));
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
                if (SetOfSetsCheckEqual(partition, tocomp))
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

        //[TestMethod] // commented not to take too much time during tests run
        public void DomaticPartitionTestPerf()
        {
            var stopwatch = new System.Diagnostics.Stopwatch();
            int maxVertexCount = 16, statRetriesCount = 5;
            for (int curVertexCount = 0; curVertexCount < maxVertexCount; curVertexCount++)
            {
                double meanTime = 0;
                for (int j = 0; j < statRetriesCount; j++)
                {
                    var graph = generateGraph(curVertexCount);
                    int[] vertices = graph.Item1;
                    Tuple<int, int>[] edges = graph.Item2;

                    stopwatch.Restart();
                    var partition = GetMaxDomaticPartition(vertices, edges);
                    stopwatch.Stop();

                    meanTime += stopwatch.Elapsed.TotalSeconds;
                }
                meanTime /= statRetriesCount;

                Console.WriteLine("Mean time on " + curVertexCount + " vertices = " + meanTime);
            }
        }
    }
}
