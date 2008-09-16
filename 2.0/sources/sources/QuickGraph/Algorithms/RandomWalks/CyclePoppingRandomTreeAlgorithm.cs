using System;
using System.Collections.Generic;
using QuickGraph.Algorithms.Services;

namespace QuickGraph.Algorithms.RandomWalks
{
    /// <summary>
    /// Wilson-Propp Cycle-Popping Algorithm for Random Tree Generation.
    /// </summary>
    [Serializable]
    public sealed class CyclePoppingRandomTreeAlgorithm<TVertex, TEdge> :
        RootedAlgorithmBase<TVertex,IVertexListGraph<TVertex,TEdge>>
        where TEdge : IEdge<TVertex>
    {
        private IDictionary<TVertex, GraphColor> vertexColors = new Dictionary<TVertex, GraphColor>();
        private IMarkovEdgeChain<TVertex,TEdge> edgeChain = new NormalizedMarkovEdgeChain<TVertex,TEdge>();
        private IDictionary<TVertex, TEdge> successors = new Dictionary<TVertex, TEdge>();
        private Random rnd = new Random((int)DateTime.Now.Ticks);

        public CyclePoppingRandomTreeAlgorithm(
            IVertexListGraph<TVertex, TEdge> visitedGraph)
            : this(visitedGraph, new NormalizedMarkovEdgeChain<TVertex, TEdge>())
        { }

        public CyclePoppingRandomTreeAlgorithm(
            IVertexListGraph<TVertex, TEdge> visitedGraph,
            IMarkovEdgeChain<TVertex, TEdge> edgeChain)
            : this(null, visitedGraph, edgeChain)
        { }

        public CyclePoppingRandomTreeAlgorithm(
            IAlgorithmComponent host,
            IVertexListGraph<TVertex,TEdge> visitedGraph,
            IMarkovEdgeChain<TVertex,TEdge> edgeChain
            )
            :base(host, visitedGraph)
        {
            if (edgeChain == null)
                throw new ArgumentNullException("edgeChain");
            this.edgeChain = edgeChain;
        }

        public IDictionary<TVertex,GraphColor> VertexColors
        {
            get
            {
                return this.vertexColors;
            }
        }

        public IMarkovEdgeChain<TVertex,TEdge> EdgeChain
        {
            get
            {
                return this.edgeChain;
            }
        }

        /// <summary>
        /// Gets or sets the random number generator used in <c>RandomTree</c>.
        /// </summary>
        /// <value>
        /// <see cref="Random"/> number generator
        /// </value>
        public Random Rnd
        {
            get
            {
                return this.rnd;
            }
            set
            {
                this.rnd = value;
            }
        }

        public IDictionary<TVertex,TEdge> Successors
        {
            get
            {
                return this.successors;
            }
        }

        public event VertexEventHandler<TVertex> InitializeVertex;
        private void OnInitializeVertex(TVertex v)
        {
            if (this.InitializeVertex != null)
                this.InitializeVertex(this, new VertexEventArgs<TVertex>(v));
        }

        public event VertexEventHandler<TVertex> FinishVertex;
        private void OnFinishVertex(TVertex v)
        {
            if (this.FinishVertex != null)
                this.FinishVertex(this, new VertexEventArgs<TVertex>(v));
        }

        public event EdgeEventHandler<TVertex,TEdge> TreeEdge;
        private void OnTreeEdge(TEdge e)
        {
            if (this.TreeEdge != null)
                this.TreeEdge(this, new EdgeEventArgs<TVertex,TEdge>(e));
        }

        public event VertexEventHandler<TVertex> ClearTreeVertex;
        private void OnClearTreeVertex(TVertex v)
        {
            if (this.ClearTreeVertex != null)
                this.ClearTreeVertex(this, new VertexEventArgs<TVertex>(v));
        }

        private void Initialize()
        {
            this.successors.Clear();
            this.vertexColors.Clear();
            foreach (var v in this.VisitedGraph.Vertices)
            {
                this.vertexColors.Add(v,GraphColor.White);
                OnInitializeVertex(v);
            }
        }

        private bool NotInTree(TVertex u)
        {
            GraphColor color = this.vertexColors[u];
            return color == GraphColor.White;
        }

        private void SetInTree(TVertex u)
        {
            this.vertexColors[u] = GraphColor.Black;
            OnFinishVertex(u);
        }

        private TEdge RandomSuccessor(TVertex u)
        {
            return this.EdgeChain.Successor(this.VisitedGraph, u);
        }

        private void Tree(TVertex u, TEdge next)
        {
            this.successors[u] = next;
            if (next == null)
                return;
            OnTreeEdge(next);
        }

        private TVertex NextInTree(TVertex u)
        {
            TEdge next = this.successors[u];
            if (next == null)
                return default(TVertex);
            else
                return next.Target;
        }

        private bool Chance(double eps)
        {
            return this.rnd.NextDouble() <= eps;
        }

        private void ClearTree(TVertex u)
        {
            this.successors[u] = default(TEdge);
            OnClearTreeVertex(u);
        }

        public void RandomTreeWithRoot(TVertex root)
        {
            if (root == null)
                throw new ArgumentNullException("root");
            this.SetRootVertex(root);
            this.Compute();
        }
        
        protected override void  InternalCompute()
        {
            TVertex rootVertex;
            if (!this.TryGetRootVertex(out rootVertex))
                throw new InvalidOperationException("RootVertex not specified");
            // initialize vertices to white
            Initialize();

            // process root
            ClearTree(rootVertex);
            SetInTree(rootVertex);

            TVertex u;
            foreach (var i in this.VisitedGraph.Vertices)
            {
                u = i;

                // first pass exploring
                while (u != null && NotInTree(u))
                {
                    Tree(u, RandomSuccessor(u));
                    u = NextInTree(u);
                }

                // second pass, coloring
                u = i;
                while (u != null && NotInTree(u))
                {
                    SetInTree(u);
                    u = NextInTree(u);
                }
            }
        }

        public void RandomTree()
        {
            double eps = 1;
            bool success;
            do
            {
                eps /= 2;
                success = Attempt(eps);
            } while (!success);
        }

        private bool Attempt(double eps)
        {
            Initialize();
            int numRoots = 0;

            TVertex u;
            foreach (var i in this.VisitedGraph.Vertices)
            {
                u = i;

                // first pass exploring
                while (u != null && NotInTree(u))
                {
                    if (Chance(eps))
                    {
                        ClearTree(u);
                        SetInTree(u);
                        ++numRoots;
                        if (numRoots > 1)
                            return false;
                    }
                    else
                    {
                        Tree(u, RandomSuccessor(u));
                        u = NextInTree(u);
                    }
                }

                // second pass, coloring
                u = i;
                while (u != null && NotInTree(u))
                {
                    SetInTree(u);
                    u = NextInTree(u);
                }
            }
            return true;
        }
    }
}
