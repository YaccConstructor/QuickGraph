using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Runtime.InteropServices;
using QuickGraph;


namespace CallC
{
    internal class Util
    {
        [DllImport("rungunrock", EntryPoint = "release_memory")]
        public static extern void release_memory(IntPtr ptr);

        public static void Main(string[] args)
        {
            
//            Test.TestCsrConverter();
//            return;
//            Test.RunSSSPTests("/home/alex/gunrock2/dataset/large/roadNet-CA/roadNet-CA.mtx");
//            return;

            string[] graphDatasets = 
            {
                "/home/alex/gunrock2/dataset/small/chesapeake.mtx", //16K
                "/home/alex/Downloads/Email-Enron.txt", //3,9M
                "/home/alex/gunrock2/dataset/large/roadNet-CA/roadNet-CA.mtx", //40M
                "/home/alex/gunrock2/dataset/large/delaunay_n21/delaunay_n21.mtx", //90M
                "/home/alex/gunrock2/dataset/large/cit-Patents/cit-Patents.mtx"//250M
            };
//            foreach (var graphDataset in graphDatasets)
//            {
//                Test.TestConversionFromQGtoCsrSpeed(graphDataset);
//            }
            
            var bfsResults = new List<Tuple<double, double>>();
            var ccResults = new List<Tuple<double, double>>();
            var ssspResults = new List<Tuple<double, double>>();
            var conversionResults = new List<double>();
            foreach (var graphDataset in graphDatasets)
            {
//                bfsResults.Add(Test.RunBFSTests(graphDataset));
//                ccResults.Add(Test.RunCCTests(graphDataset));
//                ssspResults.Add(Test.RunSSSPTests(graphDataset));
                conversionResults.Add(Test.TestConversionFromQGtoCsrSpeed(graphDataset));
            }
//            for (var i = 0; i < graphDatasets.Length; i++)
//            {
//                Console.WriteLine(graphDatasets[i]);
//                Console.WriteLine("(QG, Gunrock");
//                Console.WriteLine("BFS");
//                Console.WriteLine(bfsResults[i]);
//                Console.WriteLine("CC");
//                Console.WriteLine(ccResults[i]);
//            }
            Console.WriteLine("QuickGraph, Gunrock");
//            Console.WriteLine("BFS");
//            for (var i = 0; i < graphDatasets.Length; i++)
//            {
//                Console.WriteLine(graphDatasets[i]);
//                Console.WriteLine(bfsResults[i].Item1 + ", " + bfsResults[i].Item2);
//            }
//            Console.WriteLine("CC");
//            for (var i = 0; i < graphDatasets.Length; i++)
//            {
//                Console.WriteLine(graphDatasets[i]);
//                Console.WriteLine(ccResults[i].Item1 + ", " + ccResults[i].Item2);
//            }
//            Console.WriteLine("SSSP");
//            for (var i = 0; i < graphDatasets.Length; i++)
//            {
//                Console.WriteLine(graphDatasets[i]);
//                Console.WriteLine(ssspResults[i].Item1 + ", " + ssspResults[i].Item2);
//            }
            Console.WriteLine("Conversion speed");
            for (var i = 0; i < graphDatasets.Length; i++)
            {
                Console.WriteLine(graphDatasets[i]);
                Console.WriteLine(conversionResults[i]);
            }
            
//            Test.MeasureBFSQuickGraphSpeed("/home/alex/Downloads/Email-Enron.txt");
//            Test.TestCsrConverter();
//            Test.MeasureConnectedComponentsQuickGraphSpeed("/home/alex/Downloads/Email-Enron.txt");
//            Test.MeasureConnectedComponentsGunrockSpeed("/home/alex/Downloads/Email-Enron.txt");
//            Test.RunCCTests("/home/alex/gunrock2/dataset/large/roadNet-CA/roadNet-CA.mtx");
//            Test.RunBFSTests("/home/alex/Downloads/Email-Enron.txt");
//            Test.MeasureConnectedComponentsQuickGraphSpeed("/home/alex/gunrock2/dataset/large/road_usa/road_usa.mtx");
//           Test.MeasureConnectedComponentsGunrockSpeed("/home/alex/gunrock2/dataset/large/road_usa/road_usa.mtx");
//            Test.RunCCTests("/home/alex/gunrock2/dataset/large/road_usa/road_usa.mtx");
//            Test.RunCCTests("/home/alex/gunrock2/dataset/large/delaunay_n21/delaunay_n21.mtx");
//            ReadMatrixMarketFile("/home/alex/Downloads/roadNet-CA.txt");
//            ReadMatrixMarketFileToEdgeList("/home/alex/Downloads/roadNet-CA.txt");
//            Test.TestReadRightToCsr("/home/alex/gunrock2/dataset/small/test_cc.mtx");
//            Test.TestReadRightToCsr("/home/alex/gunrock2/dataset/large/road_usa/road_usa.mtx");
//            Test.TestReadRightToCsr("/home/alex/gunrock2/dataset/small/chesapeake.mtx");
//            Test.TestReadRightToCsr("/home/alex/Downloads/Email-Enron.txt");
           

//            int nodesNum = 39;
//            int edgesNum = 340;
//            IntPtr labelsPtr = ConnectedComponents.run_cc(nodesNum, edgesNum, new int[] { }, new int[] { } );
//
//            int[] labels = new int[nodesNum];
//            Marshal.Copy(labelsPtr, labels, 0, nodesNum);
//            Util.release_memory(labelsPtr);
//
//            var verticesToLabels = new Dictionary<int, int>();
//            for (int i = 0; i < labels.Length; i++)
//            {
//                
//                verticesToLabels.Add(i, labels[i]);
//            }
//            
//            foreach (var keyValuePair in verticesToLabels)
//            {
//                Console.WriteLine(keyValuePair);
//            }
        }
        
        public static Tuple<int[], int[], IEnumerable<int>> CreateCsrRepresentationFast<TEdge, TGraph>(
            TGraph inputGraph) 
            where TEdge : IEdge<int> 
            where TGraph : IEdgeListGraph<int, TEdge>
        {
            var edges = inputGraph.Edges.AsParallel().OrderBy(x => x.Source).ToArray();
            var vertexNum = inputGraph.VertexCount;
            var edgeNum = inputGraph.EdgeCount;
            
            int[] rowOffsets = new int[vertexNum + 1];
            int[] colIndices = new int[edgeNum];
            
            int curOffset = 0;
            int curEdge = 0;
            int prevEdge = 0;
            int columnIndex = 0;
            int duplicatesNum = 0;
            for (int i = 1; i < vertexNum + 1; i++)
            {
                rowOffsets[i - 1] = curOffset;
                for (; curEdge < edgeNum && edges[curEdge].Source == i; curEdge++)
                {
                    if (edges[curEdge].Equals(edges[prevEdge]) && curEdge > 0)
                    {
                        duplicatesNum++;
                        continue;
                    }
                    colIndices[columnIndex++] = edges[curEdge].Target - 1;
                    curOffset++;
                    prevEdge = curEdge;
                }
                
                if (i % 1000000 == 0) Console.WriteLine(i);
            }
            Array.Resize(ref colIndices, colIndices.Length - duplicatesNum);
            
            rowOffsets[rowOffsets.Length - 1] = curOffset;
            return new Tuple<int[], int[], IEnumerable<int>>(rowOffsets, colIndices, new List<int>(colIndices));
        }
        

        public static Tuple<int[], int[], IEnumerable<int>> CreateCsrRepresentation<TEdge, TGraph>(
            TGraph inputGraph) 
            where TEdge : IEdge<int> 
            where TGraph : IVertexListGraph<int, TEdge>
        {
            int[] rowOffsets = new int[inputGraph.VertexCount + 1];
            List<int> colIndices = new List<int>();
            
            int curOffset = 0;
            for (int i = 0, cnt = inputGraph.Vertices.Count(); i < cnt; i++)
            {
                rowOffsets[i] = curOffset;
//                var temp = new List<int>();
                foreach (var outEdge in inputGraph.OutEdges(inputGraph.Vertices.ElementAt(i)))
                {
                    colIndices.Add(outEdge.Target);
//                    temp.Add(outEdge.Target);
                    
                    curOffset++;
                }
//                temp.Sort();
//                colIndices.AddRange(temp);
                if (i % 1000 == 0) Console.WriteLine(i);
            }
            rowOffsets[rowOffsets.Length - 1] = rowOffsets[rowOffsets.Length - 2];
            return new Tuple<int[], int[], IEnumerable<int>>(rowOffsets, colIndices.ToArray(), inputGraph.Vertices);
        }

        public static Tuple<int[], int[], IEnumerable<int>> CreateCsrRepresentation(string path)
        {
            Tuple<Tuple<int, int>[], int, int> edgesAndMetadata = ReadMatrixMarketFileToEdgeList(path);
            var edges = edgesAndMetadata.Item1;
            var vertexNum = edgesAndMetadata.Item2;
            var edgeNum = edgesAndMetadata.Item3;
            
            int[] rowOffsets = new int[vertexNum + 1];
            int[] colIndices = new int[edgeNum];
            
            int curOffset = 0;
            int curEdge = 0;
            int prevEdge = 0;
            int columnIndex = 0;
            int duplicatesNum = 0;
            for (int i = 1; i < vertexNum + 1; i++)
            {
                rowOffsets[i - 1] = curOffset;
                for (; curEdge < edgeNum && edges[curEdge].Item1 == i; curEdge++)
                {
                    if (edges[curEdge].Equals(edges[prevEdge]) && curEdge > 0)
                    {
                        duplicatesNum++;
                        continue;
                    }
                    colIndices[columnIndex++] = edges[curEdge].Item2 - 1;
                    curOffset++;
                    prevEdge = curEdge;
                }
                
                if (i % 10000 == 0) Console.WriteLine(i);
            }
            Array.Resize(ref colIndices, colIndices.Length - duplicatesNum);
            
            rowOffsets[rowOffsets.Length - 1] = curOffset;
            return new Tuple<int[], int[], IEnumerable<int>>(rowOffsets, colIndices, new List<int>(colIndices));
        }

        private static Tuple<Tuple<int, int>[], int, int> ReadMatrixMarketFileToEdgeList(string path)
        {
            bool isSymmetrical = true;
            int edgeNum = -1;
            int vertixNum = -1;
            List<Tuple<int, int>> coordinatesList = new List<Tuple<int, int>>();
            
            using (var sr = new StreamReader(path))
            {
                string line = sr.ReadLine(); //header
//                isSymmetrical = line.Split(' ')[4] == "symmetric";
                line = sr.ReadLine();
                int processedLines = 0;
                for (; line != null; line = sr.ReadLine())
                {
                    if (line.Length < 3 || line[0] == '%') continue;//if line.Length==3 then it's possible that 
                        //it contains "digit space another_digit". If length is less than 3 then the line is garbage
                    var coordinates = line.Split(' ').Select(Int32.Parse).ToList();
                    if (coordinates.Count > 2) //another mtx header - may be not on the second line but lower
                    {
                        vertixNum = coordinates[0];
                        edgeNum = coordinates[2] * (isSymmetrical ? 2 : 1);
                    }
                    else
                    {
                        coordinatesList.Add(new Tuple<int, int>(coordinates[0], coordinates[1]));
                        if (isSymmetrical)
                        {
                            coordinatesList.Add(new Tuple<int, int>(coordinates[1], coordinates[0]));
                        }
                    }
                    if (processedLines++ % 100000 == 0) Console.WriteLine(processedLines);
                }
            }
            var coordinatesSorted = coordinatesList.AsParallel().OrderBy(x => x.Item1).ToArray();
            Console.WriteLine("Sorted!");
            
            return new Tuple<Tuple<int, int>[], int, int>(coordinatesSorted, vertixNum, edgeNum);
        }

        /// Can read only sparse integer matrix from .mtx file. Otherwise will fail horribly.
        public static AdjacencyGraph<int, Edge<int>> ReadMatrixMarketFileToQuickGraph(string path)
        {
            var graph = new AdjacencyGraph<int, Edge<int>>();   
            bool isSymmetrical = true;
            int edgeNum = -1;
            int vertixNum = -1;
            
            using (var sr = new StreamReader(path))
            {
                string line = sr.ReadLine(); //header
//                isSymmetrical = line.Split(' ')[4] == "symmetric";
                line = sr.ReadLine();
                int processedLines = 0;
                for (; line != null; line = sr.ReadLine())
                {
                    if (line.Length < 3 || line[0] == '%') continue;//if line.Length==3 then it's possible that 
                        //it contains "digit space another_digit". If length is less than 3 then the line is garbage
                    var coordinates = line.Split(' ').Select(Int32.Parse).ToList();
                    if (coordinates.Count <= 2) //the header with edge and vertex counts is useless here and will be ignored
                    {
                        if (!graph.ContainsVertex(coordinates[0])) graph.AddVertex(coordinates[0]);
                        if (!graph.ContainsVertex(coordinates[1])) graph.AddVertex(coordinates[1]);
                        graph.AddEdge(new Edge<int>(coordinates[0], coordinates[1]));
                        if (isSymmetrical)
                        {
                            graph.AddEdge(new Edge<int>(coordinates[1], coordinates[0]));
                        }
                    }
                    if (++processedLines % 100000 == 0) Console.WriteLine(processedLines);
                }
            }

            return graph;
        }

        internal static void PrintCsrRepresentation(Tuple<int[], int[], IEnumerable<int>> csrRepresentation)
        {
            csrRepresentation.Item1.ToList().ForEach(x => Console.Write(x + " "));
            Console.WriteLine();
            csrRepresentation.Item2.ToList().ForEach(x => Console.Write(x + " "));
            Console.WriteLine();
            csrRepresentation.Item3.ToList().ForEach(x => Console.Write(x + " "));
            Console.WriteLine();
        }
    }
}