using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using QuickGraph;

namespace CallC
{
    public class SingleSourceShortestPath
    {
        [DllImport("rungunrock", EntryPoint="run_sssp")]
        public static extern IntPtr run_sssp(int nodesNum, int edgesNum, int src, 
            int[] rowOffsets, int[] colIndices, int[] edge_values);
        
        public static Dictionary<int, int> FindShortestPaths<TEdge, TGraph>(TGraph inputGraph, int src = 1) 
            where TEdge : IEdge<int> 
            where TGraph : IVertexListGraph<int, TEdge>
        {
            var csrRepresentation = Util.CreateCsrRepresentation<TEdge, TGraph>(inputGraph);
            return FindShortestPaths(csrRepresentation, src);
        }

        public static Dictionary<int, int> FindShortestPaths(Tuple<int[], int[], IEnumerable<int>> csrRepresentation, 
            int src = 1)
        {
            var rowOffsets = csrRepresentation.Item1;
            var colIndices = csrRepresentation.Item2;
            var edgeValues = csrRepresentation.Item3.ToArray();
            
            
            var nodesNum = rowOffsets.Length - 1;
            var edgesNum = colIndices.Length;

            if (edgeValues.Length != edgesNum)
            {
                edgeValues = new int[edgesNum];
                Array.Copy(colIndices, edgeValues, edgesNum);
            }
            
            IntPtr labelsPtr = run_sssp(nodesNum, edgesNum, src, rowOffsets, colIndices, edgeValues);
            
            int[] labels = new int[nodesNum];
            Marshal.Copy(labelsPtr, labels, 0, nodesNum);
            Util.release_memory(labelsPtr);

            var pathsLengths = new Dictionary<int, int>();
            for (int i = 0; i < labels.Length; i++)
            {
                pathsLengths.Add(i, labels[i]);
            }
            return pathsLengths;
        } 
        
        /*
        int[] rowOffsets = {0, 3, 6, 9, 11, 14, 15, 15};
            int[] colIndices = {1, 2, 3, 0, 2, 4, 3, 4, 5, 5, 6, 2, 5, 6, 6};
            var nodesNum = rowOffsets.Length - 1;
            var edgesNum = colIndices.Length;

            IntPtr labelsPtr = run_cc_test(nodesNum, edgesNum, rowOffsets, colIndices);
            int[] labels = new int[nodesNum];
            Marshal.Copy(labelsPtr, labels, 0, nodesNum);
            release_memory(labelsPtr);
        */
    }
}