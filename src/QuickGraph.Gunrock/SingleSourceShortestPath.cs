using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace QuickGraph.Gunrock
{
    public class SingleSourceShortestPath : GunrockAlgorithmWrapper
    {
        [DllImport("gunrockwrapper", EntryPoint="run_sssp")]
        public static extern IntPtr run_sssp(int nodesNum, int edgesNum, int src, 
            int[] rowOffsets, int[] colIndices, int[] edgeValues);
        
        public static Dictionary<int, int> FindShortestPaths<TEdge, TGraph>(TGraph inputGraph, int src = 1) 
            where TEdge : IEdge<int> 
            where TGraph : IEdgeListGraph<int, TEdge>
        {
            var csrRepresentation = Util.CreateCsrRepresentationFromQuickGraphFast<TEdge, TGraph>(inputGraph);
            return FindShortestPaths(csrRepresentation, src);
        }

        public static Dictionary<int, int> FindShortestPaths(Tuple<int[], int[], int[]> csrRepresentation, 
            int src = 1)
        {
            var rowOffsets = csrRepresentation.Item1;
            var colIndices = csrRepresentation.Item2;
            var edgeValues = csrRepresentation.Item3;
            
            var nodesNum = rowOffsets.Length - 1;
            var edgesNum = colIndices.Length;

            if (edgeValues.Length != edgesNum)
            {
                edgeValues = new int[edgesNum];
                Array.Copy(colIndices, edgeValues, edgesNum);
            }
            
            IntPtr labelsPtr = run_sssp(nodesNum, edgesNum, src, rowOffsets, colIndices, edgeValues);

            int[] labels = ConvertToManagedArray(nodesNum, labelsPtr);

            var pathsLengths = CreateDictionary(labels);
            return pathsLengths;
        }
    }
}