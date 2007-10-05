using System;
using System.Collections.Generic;

using QuickGraph.Algorithms.Observers;

namespace QuickGraph.Algorithms.RandomWalks
{
    [Serializable]
    public sealed class RandomWalkAlgorithm<TVertex, TEdge> :
        ITreeBuilderAlgorithm<TVertex,TEdge>
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
            if (visitedGraph == null)
                throw new ArgumentNullException("visitedGraph");
            if (edgeChain == null)
                throw new ArgumentNullException("edgeChain");
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
                if (value == null)
                    throw new ArgumentNullException("edgeChain");
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

        public event VertexEventHandler<TVertex> StartVertex;
        private void OnStartVertex(TVertex v)
        {
            if (StartVertex != null)
                StartVertex(this, new VertexEventArgs<TVertex>(v));
        }

        public event VertexEventHandler<TVertex> EndVertex;
        private void OnEndVertex(TVertex v)
        {
            if (EndVertex != null)
                EndVertex(this, new VertexEventArgs<TVertex>(v));
        }

        public event EdgeEventHandler<TVertex,TEdge> TreeEdge;
        private void OnTreeEdge(TEdge e)
        {
            if (this.TreeEdge != null)
                this.TreeEdge(this, new EdgeEventArgs<TVertex,TEdge>(e));
        }

        private TEdge Successor(TVertex u)
        {
            return this.EdgeChain.Successor(this.VisitedGraph, u);
        }

        public void Generate(TVertex root)
        {
            if (root == null)
                throw new ArgumentNullException("root");
            Generate(root, 100);
        }

        public void Generate(TVertex root, int walkCount)
        {
            if (root == null)
                throw new ArgumentNullException("root");

            int count = 0;
            TEdge e = default(TEdge);
            TVertex v = root;

            OnStartVertex(root);
            while (count < walkCount)
            {
                e = Successor(v);
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
