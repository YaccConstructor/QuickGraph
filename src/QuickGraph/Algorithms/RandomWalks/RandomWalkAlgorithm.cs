using System;
using System.Collections.Generic;

using QuickGraph.Algorithms.Observers;
using System.Diagnostics.Contracts;

namespace QuickGraph.Algorithms.RandomWalks
{
#if !SILVERLIGHT
    [Serializable]
#endif
    public sealed class RandomWalkAlgorithm<TVertex, TEdge> 
        : ITreeBuilderAlgorithm<TVertex,TEdge>
        where TEdge : IEdge<TVertex>
    {
        private IImplicitGraph<TVertex,TEdge> visitedGraph;
        private EdgePredicate<TVertex,TEdge> endPredicate;
        private IEdgeChain<TVertex,TEdge> edgeChain;

        public RandomWalkAlgorithm(IImplicitGraph<TVertex,TEdge> visitedGraph)
            :this(visitedGraph,new NormalizedMarkovEdgeChain<TVertex,TEdge>())
        {}

        public RandomWalkAlgorithm(
            IImplicitGraph<TVertex,TEdge> visitedGraph,
            IEdgeChain<TVertex,TEdge> edgeChain
            )
        {
            Contract.Requires(visitedGraph != null);
            Contract.Requires(edgeChain != null);

            this.visitedGraph = visitedGraph;
            this.edgeChain = edgeChain;
        }

        public IImplicitGraph<TVertex,TEdge> VisitedGraph
        {
            get
            {
                return this.visitedGraph;
            }
        }

        public IEdgeChain<TVertex,TEdge> EdgeChain
        {
            get
            {
                return this.edgeChain;
            }
            set
            {
                Contract.Requires(value != null);

                this.edgeChain = value;
            }
        }

        public EdgePredicate<TVertex,TEdge> EndPredicate
        {
            get
            {
                return this.endPredicate;
            }
            set
            {
                this.endPredicate = value;
            }
        }

        public event VertexAction<TVertex> StartVertex;
        private void OnStartVertex(TVertex v)
        {
            if (StartVertex != null)
                StartVertex(v);
        }

        public event VertexAction<TVertex> EndVertex;
        private void OnEndVertex(TVertex v)
        {
            if (EndVertex != null)
                EndVertex(v);
        }

        public event EdgeAction<TVertex,TEdge> TreeEdge;
        private void OnTreeEdge(TEdge e)
        {
            if (this.TreeEdge != null)
                this.TreeEdge(e);
        }

        private bool TryGetSuccessor(TVertex u, out TEdge successor)
        {
            return this.EdgeChain.TryGetSuccessor(this.VisitedGraph, u, out successor);
        }

        public void Generate(TVertex root)
        {
            Contract.Requires(root != null);

            Generate(root, 100);
        }

        public void Generate(TVertex root, int walkCount)
        {
            Contract.Requires(root != null);

            int count = 0;
            TEdge e = default(TEdge);
            TVertex v = root;

            OnStartVertex(root);
            while (count < walkCount && this.TryGetSuccessor(v, out e))
            {
                // if dead end stop
                if (e==null)
                    break;
                // if end predicate, test
                if (this.endPredicate != null && this.endPredicate(e))
                    break;
                OnTreeEdge(e);
                v = e.Target;
                // upgrade count
                ++count;
            }
            OnEndVertex(v);
        }

    }
}
