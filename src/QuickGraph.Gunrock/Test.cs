using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using QuickGraph.Algorithms;
using QuickGraph.Algorithms.ConnectedComponents;
using QuickGraph.Algorithms.Search;
using QuickGraph.Algorithms.ShortestPath;

namespace QuickGraph.Gunrock
{
    public class Test
    {
        public static double MeasureConversionFromQuickGraphToCsrSpeed(string path)
        {
            AdjacencyGraph<int, Edge<int>> g = Util.ReadMatrixMarketFileToQuickGraph(path);
            Stopwatch sw = new Stopwatch();
            var runNum = 15;
            for (int i = 0; i < runNum; i++)
            {
                sw.Start();
                var csr = Util.CreateCsrRepresentationFromQuickGraphFast<Edge<int>, AdjacencyGraph<int, Edge<int>>>(g);
                sw.Stop();
            }
            var averageTime = sw.ElapsedMilliseconds / (double) runNum;
            Console.WriteLine(averageTime);
            return averageTime;
        }

        public static void ReadRightToCsrAndPrint(string path)
        {
            var csrRepresentation = Util.CreateCsrRepresentation(path);
            Util.PrintCsrRepresentation(csrRepresentation);
        }

        //shows the same type of results as gunrock version - useful for testing
        public static void RunQuickGraphSsspShowResults(string path)
        {
            var csrRepresentation = Util.CreateCsrRepresentation(path);
            Dictionary<int, int> findComponents = ConnectedComponents.FindComponents(csrRepresentation);
            Dictionary<int, int> componentsSizes = new Dictionary<int, int>();
            foreach (var keyValuePair in findComponents)
            {
                Console.WriteLine(keyValuePair);
                if (!componentsSizes.ContainsKey(keyValuePair.Value)) componentsSizes.Add(keyValuePair.Value, 1);
                else componentsSizes[keyValuePair.Value] += 1;
            }
            var keyValuePairs = componentsSizes.Values.ToList();
            keyValuePairs.Sort();
            foreach (var keyValuePair in keyValuePairs)
            {
                Console.WriteLine(keyValuePair);
            }
        }

        public static Tuple<double, double> RunCCTests(string path)
        {
            Console.WriteLine(path);
            var qg = MeasureConnectedComponentsQuickGraphSpeed(path);
            var gunrock = MeasureConnectedComponentsGunrockSpeed(path);
            return new Tuple<double, double>(qg, gunrock);
        }

        public static Tuple<double, double> RunBFSTests(string path)
        {
            Console.WriteLine(path);
            var qg = MeasureBFSQuickGraphSpeed(path);
            var gunrock = MeasureBFSGunrockSpeed(path);
            return new Tuple<double, double>(qg, gunrock);
        }
        
        public static Tuple<double, double> RunSSSPTests(string path)
        {
            Console.WriteLine(path);
            var qg = MeasureDijkstraQuickGraphSpeed(path);
            var gunrock = MeasureDijkstraGunrockSpeed(path);
            return new Tuple<double, double>(qg, gunrock);
        }

        private static double MeasureQuickGraphAlgorithmSpeed<TVertex, TEdge>(
            AlgorithmBase<IVertexListGraph<TVertex, TEdge>> algorithm, int iterationNum = 10)
            where TEdge : IEdge<TVertex>
        {
            Stopwatch sw = new Stopwatch();
            for (int i = 0; i < iterationNum; i++)
            {
                sw.Start();
                algorithm.Compute();
                sw.Stop();
                Console.WriteLine("Iteration " + i);
            }
            var elapsedMs = sw.ElapsedMilliseconds / (double) iterationNum;
            Console.WriteLine("QuickGraph: " + elapsedMs + " ms");
            return elapsedMs;
        }

        public static double MeasureConnectedComponentsQuickGraphSpeed(string path)
        {
            AdjacencyGraph<int, Edge<int>> graph = Util.ReadMatrixMarketFileToQuickGraph(path);
            var algo = new WeaklyConnectedComponentsAlgorithm<int, Edge<int>>(graph, new Dictionary<int, int>());
            return MeasureQuickGraphAlgorithmSpeed(algo);
        }

        public static double MeasureDijkstraQuickGraphSpeed(string path)
        {
            IVertexAndEdgeListGraph<int, Edge<int>> graph = Util.ReadMatrixMarketFileToQuickGraph(path);
            Func<Edge<int>, double> edgeCost = e => 1;
            var dijkstraShortestPathAlgorithm = 
                new DijkstraShortestPathAlgorithm<int, Edge<int>>(graph, edgeCost);
            return MeasureQuickGraphAlgorithmSpeed(dijkstraShortestPathAlgorithm);
        }

        public static double MeasureBFSQuickGraphSpeed(string path)
        {
            AdjacencyGraph<int, Edge<int>> graph = Util.ReadMatrixMarketFileToQuickGraph(path);
            var algo = new BreadthFirstSearchAlgorithm<int,Edge<int>>(graph);
            return MeasureQuickGraphAlgorithmSpeed(algo);
        }
        
        public static double MeasureConnectedComponentsGunrockSpeed(string path)
        {
            Stopwatch sw = new Stopwatch();
            Dictionary<int, int> res = null;
            int iterationNum = 10;
            var csrRepresentation = Util.CreateCsrRepresentation(path);
            for (int i = 0; i < iterationNum; i++)
            {
                sw.Start();
                res = ConnectedComponents.FindComponents(csrRepresentation);
                sw.Stop();
                Console.WriteLine("Iteration " + i);
            }
            var elapsedMs = sw.ElapsedMilliseconds / (double) iterationNum;
            Console.WriteLine("Gunrock: " + elapsedMs + " ms");
            return elapsedMs;
        }
        
        public static double MeasureBFSGunrockSpeed(string path)
        {
            Stopwatch sw = new Stopwatch();
            Dictionary<int, int> res = null;
            int iterationNum = 10;
            var csrRepresentation = Util.CreateCsrRepresentation(path);
            for (int i = 0; i < iterationNum; i++)
            {
                sw.Start();
                res = BreadthFirstSearch.RunBreadthFirstSearch(csrRepresentation);
                sw.Stop();
                Console.WriteLine("Iteration " + i);
            }
            double elapsedMs = sw.ElapsedMilliseconds / (double) iterationNum;
            Console.WriteLine("Gunrock: " + elapsedMs + " ms");
            return elapsedMs;
        }
        
        public static double MeasureDijkstraGunrockSpeed(string path)
        {
            Stopwatch sw = new Stopwatch();
            Dictionary<int, int> res = null;
            int iterationNum = 10;
            var csrRepresentation = Util.CreateCsrRepresentation(path);
            for (int i = 0; i < iterationNum; i++)
            {
                sw.Start();
                res = SingleSourceShortestPath.FindShortestPaths(csrRepresentation);
                sw.Stop();
                Console.WriteLine("Iteration " + i);
            }
            double elapsedMs = sw.ElapsedMilliseconds / (double) iterationNum;
            Console.WriteLine("Gunrock: " + elapsedMs + " ms");
            return elapsedMs;
        }
        
        public static void TestGunrockCcSimpleExampleQuickGraphRepresentation()
        {
            int[][] graph = new int[9][];
            graph[0] = new [] { 1, 2, 3 };
            graph[1] = new [] { 0, 2, 4};
            graph[2] = new [] { 3, 4, 5 };
            graph[3] = new [] { 5, 6 };
            graph[4] = new [] { 2, 5, 6 };
            graph[5] = new [] {6};
            graph[6] = new int[] { };
            graph[7] = new int[] { };
            graph[8] = new int[] { };
           

            DelegateVertexAndEdgeListGraph<int, SEquatableEdge<int>> g = Enumerable.Range(0, graph.Length).ToDelegateVertexAndEdgeListGraph(v => Array.ConvertAll(graph[v], w => new SEquatableEdge<int>(v, w))
            );
            
            var arrayAdjacencyGraph = g.ToArrayAdjacencyGraph();
            var res = ConnectedComponents.FindComponents<SEquatableEdge<int>, IEdgeListGraph<int, SEquatableEdge<int>>>(
                arrayAdjacencyGraph);
            
            foreach (var keyValuePair in res)
            {
                Console.WriteLine(keyValuePair.Key + " is in CC #" + keyValuePair.Value);
            }
        }
    }
}