using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics.Contracts;

using QuickGraph.Collections;

namespace QuickGraph.Algorithms.VertexCover
{
    /// <summary>
    /// A minimum vertex cover approximation algorithm for undirected graphs
    /// </summary>
    /// <remarks>
    /// This is a modified version (by Batov Nikita) of the original 
    /// Mihalis Yannakakis & Fanica Gavril algorithm
    /// </remarks>
    public sealed class MinimumVertexCoverApproxAlgorithm<TVertex, TEdge>
        : AlgorithmBase<IUndirectedGraph<TVertex, TEdge>>
        where TEdge : IEdge<TVertex>
    {
        private VertexList<TVertex> coverSet = new VertexList<TVertex>();
        private Random rnd = new Random();

        public MinimumVertexCoverApproxAlgorithm(IUndirectedGraph<TVertex, TEdge> graph)
            : base(graph)
        { }

        public VertexList<TVertex> CoverSet
        {
            get
            {
                if (this.State == ComputationState.Finished)
                {
                    return this.coverSet;
                }
                else
                {
                    return null;
                }
            }
        }

        protected override void InternalCompute()
        {
            TVertex source = default(TVertex);
            TVertex target = default(TVertex);

            var graph = (UndirectedGraph<TVertex, TEdge>)VisitedGraph;
            Contract.Requires(graph != null);

            while (!graph.IsEdgesEmpty)
            {
                List<TEdge> toRemove = new List<TEdge>();

                //Get random edge
                int rndNum = rnd.Next(graph.Edges.Count() - 1);
                TEdge rndEdge = graph.Edges.ElementAt(rndNum);

                source = rndEdge.Source;
                target = rndEdge.Target;

                if (graph.AdjacentDegree(rndEdge.Source) > 1)
                {
                    if (!coverSet.Contains(source))
                    {
                        coverSet.Add(source);
                    }
                }

                if (graph.AdjacentDegree(rndEdge.Target) > 1)
                {
                    if (!coverSet.Contains(target))
                    {
                        coverSet.Add(target);
                    }
                }

                if ((graph.AdjacentDegree(rndEdge.Target) == 1) && (graph.AdjacentDegree(rndEdge.Source) == 1))
                {
                    if (!coverSet.Contains(source))
                    {
                        coverSet.Add(source);
                    }
                    IEnumerable<TEdge> toDel = graph.AdjacentEdges(source);
                    graph.RemoveEdges(toDel.ToList());
                }
                else
                {
                    IEnumerable<TEdge> toDel = graph.AdjacentEdges(target).Concat(graph.AdjacentEdges(source));
                    graph.RemoveEdges(toDel.ToList());
                }
            }
        }
    }
}
