using System;
using System.Collections.Generic;

namespace QuickGraph.Algorithms.RandomWalks
{
    /// <summary>
    /// Wilson-Propp Cycle-Popping Algorithm for Random Tree Generation.
    /// </summary>
    [Serializable]
    public sealed class CyclePoppingRandomTreeAlgorithm<Vertex, Edge> :
        RootedAlgorithmBase<Vertex,IVertexListGraph<Vertex,Edge>>
        where Edge : IEdge<Vertex>
    {
        private IDictionary<Vertex, GraphColor> vertexColors = new Dictionary<Vertex, GraphColor>();
        private IMarkovEdgeChain<Vertex,Edge> edgeChain = new NormalizedMarkovEdgeChain<Vertex,Edge>();
        private IDictionary<Vertex, Edge> successors = new Dictionary<Vertex, Edge>();
        private Random rnd = new Random((int)DateTime.Now.Ticks);

        public CyclePoppingRandomTreeAlgorithm(IVertexListGraph<Vertex,Edge> visitedGraph)
            :base(visitedGraph)
        {}

        public CyclePoppingRandomTreeAlgorithm(
            IVertexListGraph<Vertex,Edge> visitedGraph,
            IMarkovEdgeChain<Vertex,Edge> edgeChain
            )
            :base(visitedGraph)
        {
            if (edgeChain == null)
                throw new ArgumentNullException("edgeChain");

            this.edgeChain = edgeChain;
        }

        public IDictionary<Vertex,GraphColor> VertexColors
        {
            get
            {
                return this.vertexColors;
            }
        }

        public IMarkovEdgeChain<Vertex,Edge> EdgeChain
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

        public IDictionary<Vertex,Edge> Successors
        {
            get
            {
                return this.successors;
            }
        }

        public event VertexEventHandler<Vertex> InitializeVertex;
        private void OnInitializeVertex(Vertex v)
        {
            if (this.InitializeVertex != null)
                this.InitializeVertex(this, new VertexEventArgs<Vertex>(v));
        }

        public event VertexEventHandler<Vertex> FinishVertex;
        private void OnFinishVertex(Vertex v)
        {
            if (this.FinishVertex != null)
                this.FinishVertex(this, new VertexEventArgs<Vertex>(v));
        }

        public event EdgeEventHandler<Vertex,Edge> TreeEdge;
        private void OnTreeEdge(Edge e)
        {
            if (this.TreeEdge != null)
                this.TreeEdge(this, new EdgeEventArgs<Vertex,Edge>(e));
        }

        public event VertexEventHandler<Vertex> ClearTreeVertex;
        private void OnClearTreeVertex(Vertex v)
        {
            if (this.ClearTreeVertex != null)
                this.ClearTreeVertex(this, new VertexEventArgs<Vertex>(v));
        }

        private void Initialize()
        {
            this.successors.Clear();
            this.vertexColors.Clear();
            foreach (Vertex v in this.VisitedGraph.Vertices)
            {
                this.vertexColors.Add(v,GraphColor.White);
                OnInitializeVertex(v);
            }
        }

        private bool NotInTree(Vertex u)
        {
            GraphColor color = this.vertexColors[u];
            return color == GraphColor.White;
        }

        private void SetInTree(Vertex u)
        {
            this.vertexColors[u] = GraphColor.Black;
            OnFinishVertex(u);
        }

        private Edge RandomSuccessor(Vertex u)
        {
            return this.EdgeChain.Successor(this.VisitedGraph, u);
        }

        private void Tree(Vertex u, Edge next)
        {
            this.successors[u] = next;
            if (next == null)
                return;
            OnTreeEdge(next);
        }

        private Vertex NextInTree(Vertex u)
        {
            Edge next = this.successors[u];
            if (next == null)
                return default(Vertex);
            else
                return next.Target;
        }

        private bool Chance(double eps)
        {
            return this.rnd.NextDouble() <= eps;
        }

        private void ClearTree(Vertex u)
        {
            this.successors[u] = default(Edge);
            OnClearTreeVertex(u);
        }

        public void RandomTreeWithRoot(Vertex root)
        {
            if (root == null)
                throw new ArgumentNullException("root");
            this.RootVertex = root;
            this.Compute();
        }
        
        protected override void  InternalCompute()
        {
            if (this.RootVertex == null)
                throw new InvalidOperationException("RootVertex not specified");
            // initialize vertices to white
            Initialize();

            // process root
            ClearTree(this.RootVertex);
            SetInTree(this.RootVertex);

            Vertex u;
            foreach (Vertex i in this.VisitedGraph.Vertices)
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

            Vertex u;
            foreach (Vertex i in this.VisitedGraph.Vertices)
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
