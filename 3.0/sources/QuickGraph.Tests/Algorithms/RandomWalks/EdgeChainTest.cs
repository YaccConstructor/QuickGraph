using System;
using System.Linq;
using System.Collections.Generic;
using QuickGraph.Unit;
using QuickGraph.Algorithms.Observers;

namespace QuickGraph.Algorithms.RandomWalks
{
    [TestFixture]
    public class EdgeChainTest
    {
        [CombinatorialTest]
        public void Generate(
            [UsingFactories(typeof(EdgeChainFactory))] 
            KeyValuePair<IVertexAndEdgeListGraph<string,Edge<string>>,IEdgeChain<string,Edge<String>>> eg
            )
        {
            RandomWalkAlgorithm<string, Edge<string>> walker =
                new RandomWalkAlgorithm<string, Edge<string>>(eg.Key, eg.Value);

            walker.Generate(eg.Key.Vertices.FirstOrDefault());
        }

        [CombinatorialTest]
        public void GenerateWithVisitor(
            [UsingFactories(typeof(EdgeChainFactory))] 
            KeyValuePair<IVertexAndEdgeListGraph<string,Edge<string>>,IEdgeChain<string,Edge<String>>> eg
            )
        {
            RandomWalkAlgorithm<string, Edge<string>> walker =
                new RandomWalkAlgorithm<string, Edge<string>>(eg.Key, eg.Value);

            EdgeRecorderObserver<string, Edge<string>> vis = new EdgeRecorderObserver<string, Edge<string>>();
            vis.Attach(walker);
            walker.Generate(eg.Key.Vertices.FirstOrDefault());
            vis.Detach(walker);
        }

    }
}
