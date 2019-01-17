using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.FSharp.Collections;
using QuickGraph.Algorithms;

namespace QuickGraph.Tests.Algorithms
{
    [TestClass]
    public class MaxCardinalityTest
    {
        [TestMethod]
        public void SmallTest()
        {
            var g1 = new BidirectionalGraph<int, Edge<int>>();
            var g2 = new BidirectionalGraph<int, Edge<int>>();

            g1.AddVerticesAndEdgeRange(new[] {new Edge<int>(1, 2), new Edge<int>(1, 3), new Edge<int>(2, 4)});

            g2.AddVerticesAndEdgeRange(new[] {new Edge<int>(1, 2), new Edge<int>(1, 3), new Edge<int>(2, 4)});

            var dictionary = new Dictionary<Tuple<int, int>, double>();

            foreach (var i in g1.Vertices)
            {
                foreach (var j in g2.Vertices)
                {
                    if (i == j) dictionary[Tuple.Create(i, j)] = 1.0;
                    else dictionary[Tuple.Create(i, j)] = 0.0;
                }
            }

            var algo = new MaxCardinality<int, Edge<int>>(g1, g2, dictionary, 0.5, (u, v) => new Edge<int>(u, v));
            var res = algo.compMaxCardinality();

            var e = Enumerable.Range(1, 4).Select(x => Tuple.Create(x, x));
            var correctResult = SetModule.OfSeq(e);

            Assert.AreEqual(res, correctResult);
        }

        [TestMethod]
        public void DifferentGraphsTest()
        {

            var g1 = new BidirectionalGraph<int, Edge<int>>();
            var g2 = new BidirectionalGraph<int, Edge<int>>();

            g1.AddVerticesAndEdgeRange(new[]
            {new Edge<int>(1, 2), new Edge<int>(1, 3), new Edge<int>(2, 4), new Edge<int>(2, 5)});

            g2.AddVerticesAndEdgeRange(new[] {new Edge<int>(1, 2), new Edge<int>(2, 3), new Edge<int>(2, 4)});

            var dictionary = new Dictionary<Tuple<int, int>, double>();

            foreach (var i in g1.Vertices)
            {
                foreach (var j in g2.Vertices)
                {
                    dictionary[Tuple.Create(i, j)] = 0.0;
                }
            }

            dictionary[Tuple.Create(1, 1)] = 1.0;
            dictionary[Tuple.Create(2, 2)] = 1.0;
            dictionary[Tuple.Create(3, 2)] = 0.6;
            dictionary[Tuple.Create(4, 3)] = 1.0;
            dictionary[Tuple.Create(5, 4)] = 1.0;

            var algo = new QuickGraph.Algorithms.MaxCardinality<int, Edge<int>>(g1, g2, dictionary, 0.5, (u, v) => new Edge<int>(u, v));
            var res = algo.compMaxCardinality();

            var correctResult =
                SetModule.Empty<Tuple<int, int>>()
                .Add(Tuple.Create(1, 1))
                .Add(Tuple.Create(2, 2))
                .Add(Tuple.Create(3, 2))
                .Add(Tuple.Create(4, 3))
                .Add(Tuple.Create(5, 4));

            Assert.AreEqual(res, correctResult);
        }
    }
}
