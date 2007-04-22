using System;
using System.Collections.Generic;

using QuickGraph.Predicates;

namespace QuickGraph.Algorithms.Exploration
{
    public sealed class CloneableVertexGraphExplorerAlgorithm<Vertex,Edge> :
        RootedAlgorithmBase<Vertex,IMutableVertexAndEdgeListGraph<Vertex, Edge>>,
        ITreeBuilderAlgorithm<Vertex,Edge>
        where Vertex : ICloneable, IComparable<Vertex>
        where Edge : IEdge<Vertex>
    {
        private IList<ITransitionFactory<Vertex, Edge>> transitionFactories = new List<ITransitionFactory<Vertex, Edge>>();

        private Queue<Vertex> unexploredVertices = new Queue<Vertex>();

        private IVertexPredicate<Vertex> addVertexPredicate = new AnyVertexPredicate<Vertex>();
        private IVertexPredicate<Vertex> exploreVertexPredicate = new AnyVertexPredicate<Vertex>();
        private IEdgePredicate<Vertex, Edge> addEdgePredicate = new AnyEdgePredicate<Vertex, Edge>();
        private IPredicate<CloneableVertexGraphExplorerAlgorithm<Vertex, Edge>> finishedPredicate =
            new DefaultFinishedPredicate();
        private bool finishedSuccessfully;

        public CloneableVertexGraphExplorerAlgorithm(
            IMutableVertexAndEdgeListGraph<Vertex, Edge> visitedGraph
            )
            :base(visitedGraph)
        {}

        public IList<ITransitionFactory<Vertex, Edge>> TransitionFactories
        {
            get { return this.transitionFactories; }
        }

        public IVertexPredicate<Vertex> AddVertexPredicate
        {
            get { return this.addVertexPredicate; }
            set { this.addVertexPredicate = value; }
        }

        public IVertexPredicate<Vertex> ExploreVertexPredicate
        {
            get { return this.exploreVertexPredicate; }
            set { this.exploreVertexPredicate = value; }
        }

        public IEdgePredicate<Vertex, Edge> AddEdgePredicate
        {
            get { return this.addEdgePredicate; }
            set { this.addEdgePredicate = value; }
        }

        public IPredicate<CloneableVertexGraphExplorerAlgorithm<Vertex, Edge>> FinishedPredicate
        {
            get { return this.finishedPredicate; }
            set { this.finishedPredicate = value; }
        }

        public IEnumerable<Vertex> UnexploredVertices
        {
            get { return this.unexploredVertices; }
        }

        public bool FinishedSuccessfully
        {
            get { return this.finishedSuccessfully; }
        }

        public event VertexEventHandler<Vertex> DiscoverVertex;
        private void OnDiscoverVertex(Vertex v)
        {
            this.VisitedGraph.AddVertex(v);
            this.unexploredVertices.Enqueue(v);
            if (this.DiscoverVertex != null)
                this.DiscoverVertex(this, new VertexEventArgs<Vertex>(v));
        }
        public event EdgeEventHandler<Vertex,Edge> TreeEdge;
        private void OnTreeEdge(Edge e)
        {
            if (this.TreeEdge != null)
                this.TreeEdge(this, new EdgeEventArgs<Vertex, Edge>(e));
        }
        public event EdgeEventHandler<Vertex, Edge> BackEdge;
        private void OnBackEdge(Edge e)
        {
            if (this.BackEdge != null)
                this.BackEdge(this, new EdgeEventArgs<Vertex, Edge>(e));
        }
        public event EdgeEventHandler<Vertex, Edge> EdgeSkipped;
        private void OnEdgeSkipped(Edge e)
        {
            if (this.EdgeSkipped != null)
                this.EdgeSkipped(this, new EdgeEventArgs<Vertex, Edge>(e));
        }

        protected override void  InternalCompute()
        {
            if (this.RootVertex == null)
                throw new InvalidOperationException("RootVertex is not specified");

            this.VisitedGraph.Clear();
            this.unexploredVertices.Clear();
            this.finishedSuccessfully = false;

            if (!this.AddVertexPredicate.Test(this.RootVertex))
                throw new ArgumentException("StartVertex does not satisfy AddVertexPredicate");
            this.OnDiscoverVertex(this.RootVertex);

            while (unexploredVertices.Count > 0)
            {
                // are we done yet ?
                if (!this.FinishedPredicate.Test(this))
                {
                    this.finishedSuccessfully = false;
                    return;
                }

                Vertex current = unexploredVertices.Dequeue();
                Vertex clone = (Vertex)current.Clone();

                // let's make sure we want to explore this one
                if (!this.ExploreVertexPredicate.Test(clone))
                    continue;

                foreach (ITransitionFactory<Vertex, Edge> transitionFactory in this.TransitionFactories)
                {
                    GenerateFromTransitionFactory(clone, transitionFactory);
                }
            }

            this.finishedSuccessfully = true;
        }

        private void GenerateFromTransitionFactory(
            Vertex current,
            ITransitionFactory<Vertex, Edge> transitionFactory
            )
        {
            if (!transitionFactory.IsValid(current))
                return;

            foreach (Edge transition in transitionFactory.Apply(current))
            {
                if (    
                    !this.AddVertexPredicate.Test(transition.Target)
                 || !this.AddEdgePredicate.Test(transition)
                 )
                {
                    this.OnEdgeSkipped(transition);
                    continue;
                }

                bool backEdge = this.VisitedGraph.ContainsVertex(transition.Target);
                if (!backEdge)
                    this.OnDiscoverVertex(transition.Target);

                this.VisitedGraph.AddEdge(transition);
                if (backEdge)
                    this.OnBackEdge(transition);
                else
                    this.OnTreeEdge(transition);
            }
        }

        public sealed class DefaultFinishedPredicate :
            IPredicate<CloneableVertexGraphExplorerAlgorithm<Vertex, Edge>>
        {
            private int maxVertexCount = 1000;
            private int maxEdgeCount = 1000;

            public DefaultFinishedPredicate()
            { }

            public DefaultFinishedPredicate(
                int maxVertexCount,
                int maxEdgeCount)
            {
                this.maxVertexCount = maxVertexCount;
                this.maxEdgeCount = maxEdgeCount;
            }

            public int MaxVertexCount
            {
                get { return this.maxVertexCount; }
                set { this.maxVertexCount = value; }
            }

            public int MaxEdgeCount
            {
                get { return this.maxEdgeCount; }
                set { this.maxEdgeCount = value; }
            }

            public bool Test(CloneableVertexGraphExplorerAlgorithm<Vertex, Edge> t)
            {
                if (t.VisitedGraph.VertexCount > this.MaxVertexCount)
                    return false;
                if (t.VisitedGraph.EdgeCount > this.MaxEdgeCount)
                    return false;
                return true;
            }
        }
    }
}
