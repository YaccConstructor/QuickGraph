using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Pex.Framework;
using QuickGraph.Serialization;
using QuickGraph.Algorithms.TSP;
using System.IO;
using QuickGraph;

namespace QuickGraph.Tests.Algorithms.TSP
{
    [TestClass]
    public class TSPTest
    {
        [TestMethod]
        public void Compute()
        {
            var g = new BidirectionalGraph<String, EquatableEdge<String>>();
            g.AddVertex("n1");
            g.AddVertex("n2");
            g.AddVertex("n3");
            g.AddVertex("n4");
            g.AddVertex("n5");
            g.AddVertex("n6");

            var weightsDict = new Dictionary<EquatableEdge<string>, double>();

            addEdge("n1", "n2", 10, g, weightsDict);
            addEdge("n2", "n3", 8, g, weightsDict);
            addEdge("n3", "n4", 11, g, weightsDict);
            addEdge("n4", "n5", 6, g, weightsDict);
            addEdge("n5", "n6", 9, g, weightsDict);
            addEdge("n1", "n6", 3, g, weightsDict);
            addEdge("n2", "n6", 5, g, weightsDict);
            addEdge("n3", "n6", 18, g, weightsDict);
            addEdge("n3", "n5", 21, g, weightsDict);


            var tcp = new TSP<String, EquatableEdge<String>, BidirectionalGraph<String, EquatableEdge<String>>>(g, weightsDict);

            tcp.Compute();

        }

        private EquatableEdge<String> createEdge(String n1, String n2)
        {
            return new EquatableEdge<string>(n1, n2);
        }

        private void addEdge(String n1, String n2, double w, BidirectionalGraph<String, EquatableEdge<String>> g, Dictionary<EquatableEdge<string>, double> d)
        {
            g.AddEdge(createEdge(n1, n2));
            g.AddEdge(createEdge(n2, n1));
            d.Add(createEdge(n1, n2), w);
            d.Add(createEdge(n2, n1), w);
        }

    }

}
 