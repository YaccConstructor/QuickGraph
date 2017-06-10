using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.IsolatedStorage;
using System.Linq;
using QuickGraph;
using QuickGraph.Algorithms;

namespace CallC
{
    public class Test
    {

        public static double TestConversionFromQGtoCsrSpeed(string path)
        {
            AdjacencyGraph<int, Edge<int>> g = Util.ReadMatrixMarketFileToQuickGraph(path);
            Stopwatch sw = new Stopwatch();
            Console.WriteLine("started conversion from qg to csr");
            var runNum = 10;
            for (int i = 0; i < runNum; i++)
            {
                sw.Start();
                var csr = Util.CreateCsrRepresentationFast<Edge<int>, AdjacencyGraph<int, Edge<int>>>(g);
                sw.Stop();
            }

            var averageTime = sw.ElapsedMilliseconds / (double) runNum;
            Console.WriteLine(averageTime);
            return averageTime;
        }

        public static void TestReadRightToCsr(string path)
        {
            var csrRepresentation = Util.CreateCsrRepresentation(path);
//            Util.PrintCsrRepresentation(csrRepresentation);
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

        public static double MeasureConnectedComponentsQuickGraphSpeed(string path)
        {
            AdjacencyGraph<int, Edge<int>> graph = Util.ReadMatrixMarketFileToQuickGraph(path);
            Stopwatch sw = new Stopwatch();
            int iterationNum = 10;
            for (int i = 0; i < iterationNum; i++)
            {
                sw.Start();
                var weaklyConnectedComponents = graph.WeaklyConnectedComponents(new Dictionary<int, int>());
                sw.Stop();
                Console.WriteLine("Iteration " + i);
            }
            var elapsedMs = sw.ElapsedMilliseconds / (double) iterationNum;
            Console.WriteLine("QuickGraph: " + elapsedMs + " ms");
            return elapsedMs;
        }

        public static double MeasureDijkstraQuickGraphSpeed(string path)
        {
            Console.WriteLine("started conversion to quickgraph");
            IVertexAndEdgeListGraph<int, Edge<int>> graph = Util.ReadMatrixMarketFileToQuickGraph(path);
            Console.WriteLine("finished conversion to quickgraph");
            Func<Edge<int>, double> edgeCost = e => 1;
            int root = 1;
            Stopwatch sw = new Stopwatch();
            int iterationNum = 10;
            var dijkstraShortestPathAlgorithm = 
                new QuickGraph.Algorithms.ShortestPath.DijkstraShortestPathAlgorithm<int, Edge<int>>(graph, edgeCost);
            
            for (int i = 0; i < iterationNum; i++)
            {
                sw.Start();
                dijkstraShortestPathAlgorithm.Compute(root);
                sw.Stop();
                Console.WriteLine("Iteration " + i);
            }
            var elapsedMs = sw.ElapsedMilliseconds / (double) iterationNum;
            Console.WriteLine("QuickGraph: " + elapsedMs + " ms");
            return elapsedMs;
        }

        public static double MeasureBFSQuickGraphSpeed(string path)
        {
            AdjacencyGraph<int, Edge<int>> graph = Util.ReadMatrixMarketFileToQuickGraph(path);
            var algo = new QuickGraph.Algorithms.Search.BreadthFirstSearchAlgorithm<int,Edge<int>>(graph);
            int sourceVertex = 1;
            
            Stopwatch sw = new Stopwatch();
            int iterationNum = 10;
            for (int i = 0; i < iterationNum; i++)
            {
                sw.Start();
                algo.Compute(sourceVertex);
                sw.Stop();
                Console.WriteLine("Iteration " + i);
            }
            var elapsedMs = sw.ElapsedMilliseconds / (double) iterationNum;
            Console.WriteLine("QuickGraph: " + elapsedMs + " ms");
            return elapsedMs;
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
        
        public static void TestCsrConverter()
        {
            int[][] graph = new int[9][];
            graph[0] = new int[] { 1, 2, 3 };
            graph[1] = new int[] { 0, 2, 4};
            graph[2] = new int[] { 3, 4, 5 };
            graph[3] = new int[] { 5, 6 };
            graph[4] = new int[] { 2, 5, 6 };
            graph[5] = new    [] {6};
            graph[6] = new int[] { };
            graph[7] = new int[] { };
            graph[8] = new int[] { };
           

            DelegateVertexAndEdgeListGraph<int, SEquatableEdge<int>> g = GraphExtensions.ToDelegateVertexAndEdgeListGraph(
                Enumerable.Range(0, graph.Length),
                v => Array.ConvertAll(graph[v], w => new SEquatableEdge<int>(v, w))
            );
            
            var arrayAdjacencyGraph = g.ToArrayAdjacencyGraph();
//            var connectedComponentsAlgorithm = new QuickGraph.Algorithms.ConnectedComponents.WeaklyConnectedComponentsAlgorithm<int, SEquatableEdge<int>>(arrayAdjacencyGraph);

            Stopwatch sw = new Stopwatch();
            Dictionary<int, int> res = null;
            for (int i = 0; i < 10; i++)
            {
                sw.Start();
                
                res = ConnectedComponents.FindComponents<SEquatableEdge<int>, IVertexListGraph<int, SEquatableEdge<int>>>(arrayAdjacencyGraph);
//                var weaklyConnectedComponents = arrayAdjacencyGraph.WeaklyConnectedComponents(new Dictionary<int, int>());
                sw.Stop();
                Console.WriteLine(sw.ElapsedMilliseconds);
            }
            
            Console.WriteLine(sw.ElapsedMilliseconds / 10.0);
            foreach (var keyValuePair in res)
            {
                Console.WriteLine(keyValuePair.Key + " is in CC #" + keyValuePair.Value);
            }
        }
    }
}