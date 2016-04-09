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

        public bool isEulerian()
        {
            var componentsAlgo = new ConnectedComponentsAlgorithm<TVertex, UndirectedEdge<TVertex>>(this.graph);
            componentsAlgo.Compute();
            
            if (componentsAlgo.ComponentCount == 1)
            {
                // Every vertice has even count of edges
                foreach (var v in graph.Vertices)
                {
                    if (graph.AdjacentEdges(v).Count() % 2 == 1)
                    {
                        return false;
                    }
                }
                return true;
            }
            else 
            {
                // Only one component could contain edges
                bool[] hasEdgesInComponent = new bool[componentsAlgo.ComponentCount];
                foreach (var verticeAndComponent in componentsAlgo.Components)
                {
                    if (graph.AdjacentEdges(verticeAndComponent.Key).Count() > 0) 
                    {
                        hasEdgesInComponent[verticeAndComponent.Value] = true;
                    }
                }
                int firstIndex = Array.FindIndex(hasEdgesInComponent, x => x);
                // More than one component contain edges
                if (firstIndex != Array.FindLastIndex(hasEdgesInComponent, x => x)) 
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
}
