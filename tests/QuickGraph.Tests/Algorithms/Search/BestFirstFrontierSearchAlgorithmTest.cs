using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Pex.Framework;
using QuickGraph.Algorithms.Search;
using QuickGraph.Algorithms;
using QuickGraph.Serialization;
using QuickGraph.Algorithms.Observers;
using QuickGraph.Algorithms.ShortestPath;
using System.Threading.Tasks;

namespace QuickGraph.Tests.Algorithms.Search
{
    [TestClass, PexClass(typeof(BestFirstFrontierSearchAlgorithm<,>))]
    public partial class BestFirstFrontierSearchAlgorithmTest
    {
        [TestMethod]
        public void KrokFFig2Example()
        {
            var g = new BidirectionalGraph<char, SEquatableEdge<char>>();
            g.AddVerticesAndEdge(new SEquatableEdge<char>('A', 'C'));
            g.AddVerticesAndEdge(new SEquatableEdge<char>('A', 'B'));
            g.AddVerticesAndEdge(new SEquatableEdge<char>('B', 'E'));
            g.AddVerticesAndEdge(new SEquatableEdge<char>('B', 'D'));
            g.AddVerticesAndEdge(new SEquatableEdge<char>('E', 'F'));
            g.AddVerticesAndEdge(new SEquatableEdge<char>('E', 'G'));

            RunSearch(g);
        }

        [TestMethod]
        public void BestFirstFrontierSearchAllGraphs()
        {
            Parallel.ForEach(TestGraphFactory.GetBidirectionalGraphs(), g =>
                RunSearch(g));
        }

        [PexMethod]
        public void RunSearch<TVertex, TEdge>(
            [PexAssumeNotNull]IBidirectionalGraph<TVertex, TEdge> g)
            where TEdge : IEdge<TVertex>
        {
            if (g.VertexCount == 0) return;

            Func<TEdge, double> edgeWeights = e => 1;
            var distanceRelaxer = DistanceRelaxers.ShortestDistance;

            var search = new BestFirstFrontierSearchAlgorithm<TVertex, TEdge>(
                null,
                g,
                edgeWeights,
                distanceRelaxer);
            var root = Enumerable.First(g.Vertices);
            var target = Enumerable.Last(g.Vertices);
            var recorder = new VertexPredecessorRecorderObserver<TVertex, TEdge>();

            using (recorder.Attach(search))
                search.Compute(root, target);

            if (recorder.VertexPredecessors.ContainsKey(target))
            {
                TestConsole.WriteLine("cost: {0}", recorder.VertexPredecessors[target]);
                IEnumerable<TEdge> path;
                Assert.IsTrue(recorder.TryGetPath(target, out path));
            }
#if DEBUG
            TestConsole.WriteLine("operator max count: {0}", search.OperatorMaxCount);
#endif
        }

        [TestMethod]
        public void CompareBestFirstFrontierSearchAllGraphs()
        {
            Parallel.ForEach(TestGraphFactory.GetBidirectionalGraphs(), g =>
            {
                if (g.VertexCount == 0) return;

                var root = g.Vertices.First();
                foreach (var v in g.Vertices)
                    if (!root.Equals(v))
                        CompareSearch(g, root, v);
            });
        }
        
        [PexMethod]
        public void CompareSearch<TVertex, TEdge>(
            [PexAssumeNotNull]IBidirectionalGraph<TVertex, TEdge> g,
            TVertex root, TVertex target)
            where TEdge: IEdge<TVertex>
        {
            Func<TEdge, double> edgeWeights = e => 1;
            var distanceRelaxer = DistanceRelaxers.ShortestDistance;

            var search = new BestFirstFrontierSearchAlgorithm<TVertex, TEdge>(
                null, 
                g, 
                edgeWeights, 
                distanceRelaxer);
            var recorder = new VertexDistanceRecorderObserver<TVertex, TEdge>(edgeWeights);
            using(recorder.Attach(search))
                search.Compute(root, target);

            var dijkstra = new DijkstraShortestPathAlgorithm<TVertex, TEdge>(g, edgeWeights, distanceRelaxer);
            var dijRecorder = new VertexDistanceRecorderObserver<TVertex, TEdge>(edgeWeights);
            using (dijRecorder.Attach(dijkstra))
                dijkstra.Compute(root);

            var fvp = recorder.Distances;
            var dvp = dijRecorder.Distances;
            double cost;
            if (dvp.TryGetValue(target, out cost))
            {
                Assert.IsTrue(fvp.ContainsKey(target), "target {0} not found, should be {1}", target, cost);
                Assert.AreEqual(dvp[target], fvp[target]);
            }
        }
    }
}
