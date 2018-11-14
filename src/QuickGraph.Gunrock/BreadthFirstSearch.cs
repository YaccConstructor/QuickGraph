using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace QuickGraph.Gunrock
{
    public class BreadthFirstSearch : GunrockAlgorithmWrapper
    {
        [DllImport("gunrockwrapper", EntryPoint="run_bfs")]
        public static extern IntPtr run_bfs(int nodesNum, int edgesNum, int[] rowOffsets, int[] colIndices);
        
        public static Dictionary<int, int> RunBreadthFirstSearch<TEdge, TGraph>(TGraph inputGraph) 
            where TEdge : IEdge<int> 
            where TGraph : IEdgeListGraph<int, TEdge>
        {
            var csrRepresentation = Util.CreateCsrRepresentationFromQuickGraphFast<TEdge, TGraph>(inputGraph);
            return RunBreadthFirstSearch(csrRepresentation);
        }
        
        ///returns predecessor nodes
        public static Dictionary<int, int> RunBreadthFirstSearch(Tuple<int[], int[], int[]> csrRepresentation)
        {
            var rowOffsets = csrRepresentation.Item1;
            var colIndices = csrRepresentation.Item2;
            var nodesNum = rowOffsets.Length - 1;
            var edgesNum = colIndices.Length;
            
            IntPtr labelsPtr = run_bfs(nodesNum, edgesNum, rowOffsets, colIndices);
            
            var labels = ConvertToManagedArray(nodesNum, labelsPtr);
            var predecessors = CreateDictionary(labels);
            
            return predecessors;
        }
    }
}