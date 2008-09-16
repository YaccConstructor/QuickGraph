using System;
using System.Collections.Generic;

using QuickGraph.Predicates;
using QuickGraph.Algorithms.Services;

namespace QuickGraph.Algorithms.Exploration
{
    public sealed class CloneableVertexGraphExplorerAlgorithm<TVertex,TEdge> :
        RootedAlgorithmBase<TVertex,IMutableVertexAndEdgeListGraph<TVertex, TEdge>>,
        ITreeBuilderAlgorithm<TVertex,TEdge>
        where TVertex : ICloneable, IComparable<TVertex>
        where TEdge : IEdge<TVertex>
    {
        private IList<ITransitionFactory<TVertex, TEdge>> transitionFactories = new List<ITransitionFactory<TVertex, TEdge>>();

        private Queue<TVertex> unexploredVertices = new Queue<TVertex>();

        private VertexPredicate<TVertex> addVertexPredicate = new AnyVertexPredicate<TVertex>().Test;
        private VertexPredicate<TVertex> exploreVertexPredicate = new AnyVertexPredicate<TVertex>().Test;
        private EdgePredicate<TVertex, TEdge> addEdgePredicate = new AnyEdgePredicate<TVertex, TEdge>().Test;
        private IPredicate<CloneableVertexGraphExplorerAlgorithm<TVertex, TEdge>> finishedPredicate =
            new DefaultFinishedPredicate();
        private bool finishedSuccessfully;

        public CloneableVertexGraphExplorerAlgorithm(
            IMutableVertexAndEdgeListGraph<TVertex, TEdge> visitedGraph
            )
            : this(null, visitedGraph)
        { }

        public CloneableVertexGraphExplorerAlgorithm(
            IAlgorithmComponent host,
            IMutableVertexAndEdgeListGraph<TVertex, TEdge> visitedGraph
            )
            :base(host, visitedGraph)
        {}

        public IList<ITransitionFactory<TVertex, TEdge>> TransitionFactories
        {
            get { return this.transitionFactories; }
        }

        public VertexPredicate<TVertex> AddVertexPredicate
        {
            get { return this.addVertexPredicate; }
            set { this.addVertexPredicate = value; }
        }

        public VertexPredicate<TVertex> ExploreVertexPredicate
        {
            get { return this.exploreVertexPredicate; }
            set { this.exploreVertexPredicate = value; }
        }

        public EdgePredicate<TVertex, TEdge> AddEdgePredicate
        {
            get { return this.addEdgePredicate; }
            set { this.addEdgePredicate = value; }
        }

        public IPredicate<CloneableVertexGraphExplorerAlgorithm<TVertex, TEdge>> FinishedPredicate
        {
            get { return this.finishedPredicate; }
            set { this.finishedPredicate = value; }
        }

        public IEnumerable<TVertex> UnexploredVertices
        {
            get { return this.unexploredVertices; }
        }

        public bool FinishedSuccessfully
        {
            get { return this.finishedSuccessfully; }
        }

        public event VertexEventHandler<TVertex> DiscoverVertex;
        private void OnDiscoverVertex(TVertex v)
        {
            this.VisitedGraph.AddVertex(v);
            this.unexploredVertices.Enqueue(v);
            if (this.DiscoverVertex != null)
                this.DiscoverVertex(this, new VertexEventArgs<TVertex>(v));
        }
        public event EdgeEventHandler<TVertex,TEdge> TreeEdge;
        private void OnTreeEdge(TEdge e)
        {
            if (this.TreeEdge != null)
                this.TreeEdge(this, new EdgeEventArgs<TVertex, TEdge>(e));
        }
        public event EdgeEventHandler<TVertex, TEdge> BackEdge;
        private void OnBackEdge(TEdge e)
        {
            if (this.BackEdge != null)
                this.BackEdge(this, new EdgeEventArgs<TVertex, TEdge>(e));
        }
        public event EdgeEventHandler<TVertex, TEdge> EdgeSkipped;
        private void OnEdgeSkipped(TEdge e)
        {
            if (this.EdgeSkipped != null)
                this.EdgeSkipped(this, new EdgeEventArgs<TVertex, TEdge>(e));
        }

        protected override void  InternalCompute()
        {
            TVertex rootVertex;
            if (!this.TryGetRootVertex(out rootVertex))
                throw new InvalidOperationException("RootVertex is not specified");

            this.VisitedGraph.Clear();
            this.unexploredVertices.Clear();
            this.finishedSuccessfully = false;

            if (!this.AddVertexPredicate(rootVertex))
                throw new ArgumentException("StartVertex does not satisfy AddVertexPredicate");
            this.OnDiscoverVertex(rootVertex);

            while (unexploredVertices.Count > 0)
            {
                // are we done yet ?
                if (!this.FinishedPredicate.Test(this))
                {
                    this.finishedSuccessfully = false;
                    return;
                }

                TVertex current = unexploredVertices.Dequeue();
                TVertex clone = (TVertex)current.Clone();

                // let's make sure we want to explore this one
                if (!this.ExploreVertexPredicate(clone))
                    continue;

                foreach (ITransitionFactory<TVertex, TEdge> transitionFactory in this.TransitionFactories)
                {
                    GenerateFromTransitionFactory(clone, transitionFactory);
                }
            }

            this.finishedSuccessfully = true;
        }

        private void GenerateFromTransitionFactory(
            TVertex current,
            ITransitionFactory<TVertex, TEdge> transitionFactory
            )
        {
            if (!transitionFactory.IsValid(current))
                return;

            foreach (var transition in transitionFactory.Apply(current))
            {
                if (    
                    !this.AddVertexPredicate(transition.Target)
                 || !this.AddEdgePredicate(transition))
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
            IPredicate<CloneableVertexGraphExplorerAlgorithm<TVertex, TEdge>>
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

            public bool Test(CloneableVertexGraphExplorerAlgorithm<TVertex, TEdge> t)
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
