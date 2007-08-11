using System;
using System.Collections.Generic;

using QuickGraph.Algorithms.Observers;

namespace QuickGraph.Algorithms.RandomWalks
{
    [Serializable]
    public sealed class RandomWalkAlgorithm<Vertex, Edge> :
        ITreeBuilderAlgorithm<Vertex,Edge>
        where Edge : IEdge<Vertex>
    {
        private IImplicitGraph<Vertex,Edge> visitedGraph;
        private EdgePredicate<Vertex,Edge> endPredicate;
        private IEdgeChain<Vertex,Edge> edgeChain;

        public RandomWalkAlgorithm(IImplicitGraph<Vertex,Edge> visitedGraph)
            :this(visitedGraph,new NormalizedMarkovEdgeChain<Vertex,Edge>())
        {}

        public RandomWalkAlgorithm(
            IImplicitGraph<Vertex,Edge> visitedGraph,
            IEdgeChain<Vertex,Edge> edgeChain
            )
        {
            if (visitedGraph == null)
                throw new ArgumentNullException("visitedGraph");
            if (edgeChain == null)
                throw new ArgumentNullException("edgeChain");
            this.visitedGraph = visitedGraph;
            this.edgeChain = edgeChain;
        }

        public IImplicitGraph<Vertex,Edge> VisitedGraph
        {
            get
            {
                return this.visitedGraph;
            }
        }

        public IEdgeChain<Vertex,Edge> EdgeChain
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

        public EdgePredicate<Vertex,Edge> EndPredicate
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

        public event VertexEventHandler<Vertex> StartVertex;
        private void OnStartVertex(Vertex v)
        {
            if (StartVertex != null)
                StartVertex(this, new VertexEventArgs<Vertex>(v));
        }

        public event VertexEventHandler<Vertex> EndVertex;
        private void OnEndVertex(Vertex v)
        {
            if (EndVertex != null)
                EndVertex(this, new VertexEventArgs<Vertex>(v));
        }

        public event EdgeEventHandler<Vertex,Edge> TreeEdge;
        private void OnTreeEdge(Edge e)
        {
            if (this.TreeEdge != null)
                this.TreeEdge(this, new EdgeEventArgs<Vertex,Edge>(e));
        }

        private Edge Successor(Vertex u)
        {
            return this.EdgeChain.Successor(this.VisitedGraph, u);
        }

        public void Generate(Vertex root)
        {
            if (root == null)
                throw new ArgumentNullException("root");
            Generate(root, 100);
        }

        public void Generate(Vertex root, int walkCount)
        {
            if (root == null)
                throw new ArgumentNullException("root");

            int count = 0;
            Edge e = default(Edge);
            Vertex v = root;

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
