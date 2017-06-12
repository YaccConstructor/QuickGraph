using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace QuickGraph.Gunrock
{
    internal class Util
    {
        public static void Main(string[] args)
        {
            List<string> graphDatasets = new List<string>();
            using (var sr = new StreamReader(args[0]))
            {
                var line = sr.ReadLine();
                for (; line != null; line = sr.ReadLine())
                {
                    graphDatasets.Add(line.Trim());    
                }
            }
//            string[] graphDatasets = {
//                "/home/alex/gunrock2/dataset/small/chesapeake.mtx", //16K
//                "/home/alex/Downloads/Email-Enron.txt", //3,9M
//                "/home/alex/gunrock2/dataset/large/roadNet-CA/roadNet-CA.mtx", //40M
//                "/home/alex/gunrock2/dataset/large/delaunay_n21/delaunay_n21.mtx", //90M
//                "/home/alex/gunrock2/dataset/large/cit-Patents/cit-Patents.mtx"//250M
//            };
            var bfsResults = new List<Tuple<double, double>>();
            var ccResults = new List<Tuple<double, double>>();
            var ssspResults = new List<Tuple<double, double>>();
            var conversionResults = new List<double>();
            foreach (var graphDataset in graphDatasets)
            {
//                bfsResults.Add(Test.RunBFSTests(graphDataset));
//                ccResults.Add(Test.RunCCTests(graphDataset));
//                ssspResults.Add(Test.RunSSSPTests(graphDataset));
                conversionResults.Add(Test.MeasureConversionFromQuickGraphToCsrSpeed(graphDataset));
            }
            Console.WriteLine("QuickGraph, Gunrock");
//            Console.WriteLine("BFS");
//            for (var i = 0; i < graphDatasets.Count; i++)
//            {
//                Console.WriteLine(graphDatasets[i]);
//                Console.WriteLine(bfsResults[i].Item1 + ", " + bfsResults[i].Item2);
//            }
//            Console.WriteLine("CC");
//            for (var i = 0; i < graphDatasets.Count; i++)
//            {
//                Console.WriteLine(graphDatasets[i]);
//                Console.WriteLine(ccResults[i].Item1 + ", " + ccResults[i].Item2);
//            }
//            Console.WriteLine("SSSP");
//            for (var i = 0; i < graphDatasets.Count; i++)
//            {
//                Console.WriteLine(graphDatasets[i]);
//                Console.WriteLine(ssspResults[i].Item1 + ", " + ssspResults[i].Item2);
//            }
            Console.WriteLine("Conversion speed");
            for (var i = 0; i < graphDatasets.Count; i++)
            {
                Console.WriteLine(graphDatasets[i]);
                Console.WriteLine(conversionResults[i]);
            }
        }
        
        public static Tuple<int[], int[], int[]> CreateCsrRepresentationFromQuickGraphFast<TEdge, TGraph>(
            TGraph inputGraph) 
            where TEdge : IEdge<int> 
            where TGraph : IEdgeListGraph<int, TEdge>
        {
            var edges = inputGraph.Edges.AsParallel().OrderBy(x => x.Source).ToArray();
            var vertexNum = inputGraph.VertexCount;
            var edgeNum = inputGraph.EdgeCount;
            
            return CreateCsrRepresentationFromEdgeList(vertexNum, edgeNum, edges);
        }
        
        //works horribly slow - left in case someone wants to try such approach
        public static Tuple<int[], int[], int[]> CreateCsrRepresentationFromQuickGraphSlow<TEdge, TGraph>(
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
                foreach (var outEdge in inputGraph.OutEdges(inputGraph.Vertices.ElementAt(i)))
                {
                    colIndices.Add(outEdge.Target);
                    curOffset++;
                }
                if (i % 100000 == 0) Console.WriteLine(i);
            }
            rowOffsets[rowOffsets.Length - 1] = rowOffsets[rowOffsets.Length - 2];
            return new Tuple<int[], int[], int[]>(rowOffsets, colIndices.ToArray(), inputGraph.Vertices.ToArray());
        }

        private static Tuple<int[], int[], int[]> CreateCsrRepresentationFromEdgeList<TEdge>(int vertexNum, int edgeNum,
            TEdge[] edges)
            where TEdge : IEdge<int> 
        {
            int[] rowOffsets = new int[vertexNum + 1];
            int[] colIndices = new int[edgeNum];

            int curOffset = 0;
            int curEdgeIndex = 0;
            int prevEdgeIndex = 0;
            int columnIndex = 0;
            int duplicatesNum = 0;
            for (int i = 1; i < vertexNum + 1; i++)
            {
                rowOffsets[i - 1] = curOffset;
                for (; curEdgeIndex < edgeNum && edges[curEdgeIndex].Source == i; curEdgeIndex++)
                {
                    var curEdge = edges[curEdgeIndex];
                    var prevEdge = edges[prevEdgeIndex];
                    if (curEdge.Target == prevEdge.Target && curEdge.Source == prevEdge.Source && curEdgeIndex > 0)
                    {
                        duplicatesNum++;
                        continue;
                    }
                    colIndices[columnIndex++] = curEdge.Target - 1;
                    curOffset++;
                    prevEdgeIndex = curEdgeIndex;
                }

                if (i % 1000000 == 0) Console.WriteLine(i);
            }
            Array.Resize(ref colIndices, colIndices.Length - duplicatesNum);

            rowOffsets[rowOffsets.Length - 1] = curOffset;
            return new Tuple<int[], int[], int[]>(rowOffsets, colIndices, colIndices);
        }

        public static Tuple<int[], int[], int[]> CreateCsrRepresentation(string path)
        {
            Tuple<Edge<int>[], int, int> edgesAndMetadata = ReadMatrixMarketFileToEdgeList(path);
            var edges = edgesAndMetadata.Item1;
            var vertexNum = edgesAndMetadata.Item2;
            var edgeNum = edgesAndMetadata.Item3;
            return CreateCsrRepresentationFromEdgeList(vertexNum, edgeNum, edges);
        }

        private static Tuple<Edge<int>[], int, int> ReadMatrixMarketFileToEdgeList(string path)
        {
            bool isSymmetrical = true;
            int edgeNum = -1;
            int vertixNum = -1;
            List<Edge<int>> coordinatesList = new List<Edge<int>>();
            
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
                        coordinatesList.Add(new Edge<int>(coordinates[0], coordinates[1]));
                        if (isSymmetrical)
                        {
                            coordinatesList.Add(new Edge<int>(coordinates[1], coordinates[0]));
                        }
                    }
                    if (processedLines++ % 100000 == 0) Console.WriteLine(processedLines);
                }
            }
            var coordinatesSorted = coordinatesList.AsParallel().OrderBy(x => x.Source).ToArray();
            Console.WriteLine("Sorted!");
            
            return new Tuple<Edge<int>[], int, int>(coordinatesSorted, vertixNum, edgeNum);
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
                //isSymmetrical = line.Split(' ')[4] == "symmetric";
                    //above line isn't wrong, but it turns out, gunrock treats all mtx files as symmetric.
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

        internal static void PrintCsrRepresentation(Tuple<int[], int[], int[]> csrRepresentation)
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