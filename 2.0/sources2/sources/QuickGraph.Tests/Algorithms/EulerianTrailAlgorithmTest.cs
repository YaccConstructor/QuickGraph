using System;
using System.Collections.Generic;

using QuickGraph.Unit;

namespace QuickGraph.Algorithms
{
//    [TypeFixture(typeof(IVertexAndEdgeListGraph<string,Edge<string>>))]
//    [TypeFactory(typeof(AdjacencyGraphFactory))]
//    [TypeFactory(typeof(BidirectionalGraphFactory))]
    public class EulerianTrailAlgorithmTest
    {
        [Test]
        public void ComputeTrail(IMutableVertexAndEdgeListGraph<string,Edge<string>> g)
        {
            if (g.VertexCount == 0)
                return;

            GraphConsoleSerializer.DisplayGraph(g);

            int oddCount = 0;
            foreach (string v in g.Vertices)
                if (g.OutDegree(v) % 2 == 0)
                    oddCount++;

            int circuitCount = EulerianTrailAlgorithm<string,Edge<string>>.ComputeEulerianPathCount(g);
            if (circuitCount == 0)
                return;

            EulerianTrailAlgorithm<string, Edge<string>> trail = new EulerianTrailAlgorithm<string, Edge<string>>(g);
            trail.AddTemporaryEdges(new EdgeFactory<string>());
            trail.Compute();
            ICollection<ICollection<Edge<string>>> trails = trail.Trails();
            trail.RemoveTemporaryEdges();

            Console.WriteLine("trails: {0}", trails.Count);
            int index = 0;
            foreach (ICollection<Edge<string>> t in trails)
            {
                Console.WriteLine("trail {0}", index++);
                foreach (Edge<string> edge in t)
                    Console.WriteLine("\t{0}", t);
            }

            // lets make sure all the edges are in the trail
            Dictionary<Edge<string>, GraphColor> edgeColors = new Dictionary<Edge<string>, GraphColor>(g.EdgeCount);
            foreach (Edge<string> edge in g.Edges)
                edgeColors.Add(edge, GraphColor.White);
            foreach (ICollection<Edge<string>> t in trails)
                foreach (Edge<string> edge in t)
                    CollectionAssert.ContainsKey(edgeColors, edge);

        }
    }
}
