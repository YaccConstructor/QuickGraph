using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using QuickGraph;

namespace CallC
{
    public class BreadthFirstSearch
    {
        [DllImport("rungunrock", EntryPoint="run_bfs")]
        public static extern IntPtr run_bfs(int nodesNum, int edgesNum, int[] rowOffsets, int[] colIndices);
        
        public static Dictionary<int, int> RunBreadthFirstSearch<TEdge, TGraph>(TGraph inputGraph) 
            where TEdge : IEdge<int> 
            where TGraph : IVertexListGraph<int, TEdge>
        {
            var csrRepresentation = Util.CreateCsrRepresentation<TEdge, TGraph>(inputGraph);
            return RunBreadthFirstSearch(csrRepresentation);
        }
        
        ///returns predecessor nodes for each node 
        public static Dictionary<int, int> RunBreadthFirstSearch(
            Tuple<int[], int[], IEnumerable<int>> csrRepresentation)
        {
            var rowOffsets = csrRepresentation.Item1;
            var colIndices = csrRepresentation.Item2;
            var nodesNum = rowOffsets.Length - 1;
            var edgesNum = colIndices.Length;
            
            IntPtr labelsPtr = run_bfs(nodesNum, edgesNum, rowOffsets, colIndices);
            
            int[] labels = new int[nodesNum];
            Marshal.Copy(labelsPtr, labels, 0, nodesNum);
            Util.release_memory(labelsPtr);

            var predecessors = new Dictionary<int, int>();
            for (int i = 0; i < labels.Length; i++)
            {
                predecessors.Add(i, labels[i]);
            }
            return predecessors;
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