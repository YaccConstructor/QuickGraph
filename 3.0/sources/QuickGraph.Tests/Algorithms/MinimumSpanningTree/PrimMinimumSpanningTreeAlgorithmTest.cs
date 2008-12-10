using System;
using System.Collections.Generic;
using QuickGraph.Algorithms.Observers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Pex.Framework;
using QuickGraph.Serialization;

namespace QuickGraph.Algorithms.MinimumSpanningTree
{
    [TestClass]
    public class PrimMinimumSpanningTreeAlgorithmTest
    {
        [TestMethod]
        public void PrimMinimumSpanningTreeAll()
        {
            foreach (var g in TestGraphFactory.GetUndirectedGraphs())
                this.Compute(g);
        }

        [PexMethod]
        public void Compute<TVertex, TEdge>([PexAssumeNotNull]IUndirectedGraph<TVertex, TEdge> g)
            where TEdge : IEdge<TVertex>
        {
            var prim = new PrimMinimumSpanningTreeAlgorithm<TVertex, TEdge>(g, e => 1);
            var predecessors = new VertexPredecessorRecorderObserver<TVertex, TEdge>();
            using(ObserverScope.Create(prim, predecessors))
                prim.Compute();

            foreach (var v in g.Vertices)
            {
                TEdge edge;
                if (predecessors.VertexPredecessors.TryGetValue(v, out edge))
                    Console.WriteLine("{0}: {1}", v, edge);
                else
                    Console.WriteLine("{0}", v);
            }
        }
    }
}
