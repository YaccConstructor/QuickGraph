using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuickGraph.Algorithms.ConnectedComponents;

namespace QuickGraph.Algorithms
{
    public class IsEulerianGraphAlgorithm<TVertex, TEdge> where TEdge : IUndirectedEdge<TVertex>
    {
        private UndirectedGraph<TVertex, UndirectedEdge<TVertex>> graph;

        public IsEulerianGraphAlgorithm(UndirectedGraph<TVertex, UndirectedEdge<TVertex>> graph)
        {
            this.graph = graph;
        }

        private Tuple<int, int> firstAndSecondIndexOfTrue(bool[] data)
        {
            // if no true elements returns (-1, -1)
            // if only one true element, returns (indexOfTrue, -1)
            int firstIndex = -1, secondIndex = -1;
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i])
                {
                    if (firstIndex == -1)
                    {
                        firstIndex = i;
                    }
                    else
                    {
                        return new Tuple<int, int>(firstIndex, i);
                    } 
                }
            }
            return new Tuple<int, int>(firstIndex, secondIndex);
        }

        public bool isEulerian()
        {
            var componentsAlgo = new ConnectedComponentsAlgorithm<TVertex, UndirectedEdge<TVertex>>(this.graph);
            componentsAlgo.Compute();
            
            // Only one component could contain edges
            bool[] hasEdgesInComponent = new bool[componentsAlgo.ComponentCount];
            foreach (var verticeAndComponent in componentsAlgo.Components)
            {
                if (graph.AdjacentEdges(verticeAndComponent.Key).Count() > 0) 
                {
                    hasEdgesInComponent[verticeAndComponent.Value] = true;
                }
            }
            var t = firstAndSecondIndexOfTrue(hasEdgesInComponent);
            int firstIndex = t.Item1, secondIndex = t.Item2;
            // More than one component contain edges
            if (secondIndex != -1) 
            {
                return false;
            }
            else
            {
                // Now only one component contains edges, check is it an eulerian component
                foreach (var verticeAndComponent in componentsAlgo.Components)
                {   
                    // Vertice in selected component and has even count of edges
                    if (verticeAndComponent.Value == firstIndex && 
                        graph.AdjacentEdges(verticeAndComponent.Key).Count() % 2 == 1)
                    {
                        return false;
                    }
                }
                return true;
            }
        }
    }
}
