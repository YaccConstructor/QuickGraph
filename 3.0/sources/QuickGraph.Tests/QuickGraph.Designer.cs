#pragma warning disable 0067, 0108
// ------------------------------------
// 
// Assembly QuickGraph
// 
// ------------------------------------
namespace QuickGraph.Stubs
{
    /// <summary>Stub of AdjacencyGraph`2</summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Stubs", "0.1.2.3")]
    [global::System.Serializable]
    [global::System.Diagnostics.DebuggerStepThrough]
    public partial class SAdjacencyGraph<TVertex,TEdge>
      : global::QuickGraph.AdjacencyGraph<TVertex, TEdge>
      , global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs.SAdjacencyGraph<TVertex, TEdge>>
        where TEdge : global::QuickGraph.IEdge<TVertex>
    {
        /// <summary>Initializes a new instance of type SAdjacencyGraph</summary>
        [global::System.Diagnostics.DebuggerHidden]
        public SAdjacencyGraph()
        {
            this.defaultStub = global::Microsoft.Stubs.DefaultStub
                <global::QuickGraph.Stubs.SAdjacencyGraph<TVertex, TEdge>>.Current;
        }

        /// <summary>Gets or sets the default stub implementation.</summary>
        public global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs.SAdjacencyGraph<TVertex, TEdge>> DefaultStub
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                return this.defaultStub;
            }
            [global::System.Diagnostics.DebuggerHidden]
            set
            {
                this.defaultStub = value;
            }
        }

        private global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs.SAdjacencyGraph<TVertex, TEdge>> defaultStub;
    }
}
namespace QuickGraph.Stubs
{
    /// <summary>Stub of BidirectionalGraph`2</summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Stubs", "0.1.2.3")]
    [global::System.Serializable]
    [global::System.Diagnostics.DebuggerStepThrough]
    public partial class SBidirectionalGraph<TVertex,TEdge>
      : global::QuickGraph.BidirectionalGraph<TVertex, TEdge>
      , global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs.SBidirectionalGraph<TVertex, TEdge>>
        where TEdge : global::QuickGraph.IEdge<TVertex>
    {
        /// <summary>Initializes a new instance of type SBidirectionalGraph</summary>
        [global::System.Diagnostics.DebuggerHidden]
        public SBidirectionalGraph()
        {
            this.defaultStub = global::Microsoft.Stubs.DefaultStub
                <global::QuickGraph.Stubs.SBidirectionalGraph<TVertex, TEdge>>.Current;
        }

        /// <summary>Gets or sets the default stub implementation.</summary>
        public global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs.SBidirectionalGraph<TVertex, TEdge>> DefaultStub
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                return this.defaultStub;
            }
            [global::System.Diagnostics.DebuggerHidden]
            set
            {
                this.defaultStub = value;
            }
        }

        private global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs.SBidirectionalGraph<TVertex, TEdge>> defaultStub;
    }
}
namespace QuickGraph.Collections.Stubs
{
    /// <summary>Stub of BinaryHeap`2</summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Stubs", "0.1.2.3")]
    [global::System.Serializable]
    [global::System.Diagnostics.DebuggerStepThrough]
    public partial class SBinaryHeap<TPriority,TValue>
      : global::QuickGraph.Collections.BinaryHeap<TPriority, TValue>
      , global::Microsoft.Stubs.IStub<global::QuickGraph.Collections.Stubs.SBinaryHeap<TPriority, TValue>>
    {
        /// <summary>Initializes a new instance of type SBinaryHeap</summary>
        [global::System.Diagnostics.DebuggerHidden]
        public SBinaryHeap()
        {
            this.defaultStub =
              global::Microsoft.Stubs.DefaultStub<global::QuickGraph.Collections.Stubs
                .SBinaryHeap<TPriority, TValue>>.Current;
        }

        /// <summary>Gets or sets the default stub implementation.</summary>
        public global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Collections.Stubs.SBinaryHeap<TPriority, TValue>> DefaultStub
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                return this.defaultStub;
            }
            [global::System.Diagnostics.DebuggerHidden]
            set
            {
                this.defaultStub = value;
            }
        }

        private global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Collections.Stubs.SBinaryHeap<TPriority, TValue>> defaultStub;
    }
}
namespace QuickGraph.Petri.Stubs
{
    /// <summary>Stub of IConditionExpression`1</summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Stubs", "0.1.2.3")]
    [global::System.Serializable]
    [global::System.Diagnostics.DebuggerStepThrough]
    public partial class SConditionExpression<Token>
      : global::Microsoft.Stubs.StubBase<global::QuickGraph.Petri.Stubs.SConditionExpression<Token>>
      , global::QuickGraph.Petri.IConditionExpression<Token>
    {
        /// <summary>Initializes a new instance of type SConditionExpression</summary>
        [global::System.Diagnostics.DebuggerHidden]
        public SConditionExpression()
        {
        }

        /// <summary>Stub of method System.Boolean QuickGraph.Petri.IConditionExpression`1.IsEnabled(System.Collections.Generic.IList`1&lt;!0&gt; tokens)</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Petri.Stubs.SConditionExpression<Token>, global::System.Collections.Generic.IList<Token>, bool> IsEnabled;

        /// <summary>Stub of method System.Boolean QuickGraph.Petri.IConditionExpression`1.IsEnabled(System.Collections.Generic.IList`1&lt;!0&gt; tokens)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.Petri.IConditionExpression<Token>.IsEnabled(global::System.Collections.Generic.IList<Token> tokens)
        {
            global::Microsoft.Stubs.StubDelegates.Func
                <global::QuickGraph.Petri.Stubs.SConditionExpression<Token>, 
                global::System.Collections.Generic.IList<Token>, bool> sh = this.IsEnabled;
            if ((object)sh != (object)null)
              return sh.Invoke(this, tokens);
            else 
            {
              global::Microsoft.Stubs.IDefaultStub
                  <global::QuickGraph.Petri.Stubs.SConditionExpression<Token>> stub
                 = ((global::Microsoft.Stubs.IStub<
                global::QuickGraph.Petri.Stubs.SConditionExpression<Token>
                >)this).DefaultStub;
              return stub.Result<bool>(this);
            }
        }
    }
}
namespace QuickGraph.Algorithms.Stubs
{
    /// <summary>Stub of IDistanceRecorderAlgorithm`2</summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Stubs", "0.1.2.3")]
    [global::System.Serializable]
    [global::System.Diagnostics.DebuggerStepThrough]
    public partial class SDistanceRecorderAlgorithm<TVertex,TEdge>
      : global::Microsoft.Stubs.StubBase<global::QuickGraph.Algorithms.Stubs.SDistanceRecorderAlgorithm<TVertex, TEdge>>
      , global::QuickGraph.Algorithms.IDistanceRecorderAlgorithm<TVertex, TEdge>
        where TEdge : global::QuickGraph.IEdge<TVertex>
    {
        /// <summary>Initializes a new instance of type SDistanceRecorderAlgorithm</summary>
        [global::System.Diagnostics.DebuggerHidden]
        public SDistanceRecorderAlgorithm()
        {
        }

        /// <summary>Event DiscoverVertex</summary>
        public event global::QuickGraph.VertexAction<TVertex> DiscoverVertex;

        /// <summary>Event InitializeVertex</summary>
        public event global::QuickGraph.VertexAction<TVertex> InitializeVertex;
    }
}
namespace QuickGraph.Algorithms.RandomWalks.Stubs
{
    /// <summary>Stub of IEdgeChain`2</summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Stubs", "0.1.2.3")]
    [global::System.Serializable]
    [global::System.Diagnostics.DebuggerStepThrough]
    public partial class SEdgeChain<TVertex,TEdge>
      : global::Microsoft.Stubs.StubBase<global::QuickGraph.Algorithms.RandomWalks.Stubs.SEdgeChain<TVertex, TEdge>>
      , global::QuickGraph.Algorithms.RandomWalks.IEdgeChain<TVertex, TEdge>
        where TEdge : global::QuickGraph.IEdge<TVertex>
    {
        /// <summary>Initializes a new instance of type SEdgeChain</summary>
        [global::System.Diagnostics.DebuggerHidden]
        public SEdgeChain()
        {
        }

        /// <summary>Stub of method !1 QuickGraph.Algorithms.RandomWalks.IEdgeChain`2.Successor(QuickGraph.IImplicitGraph`2&lt;!0,!1&gt; g, !0 u)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        TEdge global::QuickGraph.Algorithms.RandomWalks.IEdgeChain<TVertex, TEdge>.Successor(global::QuickGraph.IImplicitGraph<TVertex, TEdge> g, TVertex u)
        {
            global::Microsoft.Stubs
              .IDefaultStub<global::QuickGraph.Algorithms.RandomWalks.Stubs
                .SEdgeChain<TVertex, TEdge>> stub = ((global::Microsoft.Stubs
              .IStub<global::QuickGraph.Algorithms.RandomWalks.Stubs
                .SEdgeChain<TVertex, TEdge>>)this).DefaultStub;
            return stub.Result<TEdge>(this);
        }
    }
}
namespace QuickGraph.Collections.Stubs
{
    /// <summary>Stub of EdgeEdgeDictionary`2</summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Stubs", "0.1.2.3")]
    [global::System.Serializable]
    [global::System.Diagnostics.DebuggerStepThrough]
    [global::System.Reflection.DefaultMember("Item")]
    public partial class SEdgeEdgeDictionary<TVertex,TEdge>
      : global::QuickGraph.Collections.EdgeEdgeDictionary<TVertex, TEdge>
      , global::Microsoft.Stubs.IStub<global::QuickGraph.Collections.Stubs.SEdgeEdgeDictionary<TVertex, TEdge>>
        where TEdge : global::QuickGraph.IEdge<TVertex>
    {
        /// <summary>Initializes a new instance of type SEdgeEdgeDictionary</summary>
        [global::System.Diagnostics.DebuggerHidden]
        public SEdgeEdgeDictionary()
        {
            this.defaultStub =
              global::Microsoft.Stubs.DefaultStub<global::QuickGraph.Collections.Stubs
                .SEdgeEdgeDictionary<TVertex, TEdge>>.Current;
        }

        /// <summary>Gets or sets the default stub implementation.</summary>
        public global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Collections.Stubs.SEdgeEdgeDictionary<TVertex, TEdge>> DefaultStub
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                return this.defaultStub;
            }
            [global::System.Diagnostics.DebuggerHidden]
            set
            {
                this.defaultStub = value;
            }
        }

        private global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Collections.Stubs.SEdgeEdgeDictionary<TVertex, TEdge>> defaultStub;
    }
}
namespace QuickGraph.Stubs
{
    /// <summary>Stub of EdgeListGraph`2</summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Stubs", "0.1.2.3")]
    [global::System.Serializable]
    [global::System.Diagnostics.DebuggerStepThrough]
    public partial class SEdgeListGraph<TVertex,TEdge>
      : global::QuickGraph.EdgeListGraph<TVertex, TEdge>
      , global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs.SEdgeListGraph<TVertex, TEdge>>
        where TEdge : global::QuickGraph.IEdge<TVertex>
    {
        /// <summary>Initializes a new instance of type SEdgeListGraph</summary>
        [global::System.Diagnostics.DebuggerHidden]
        public SEdgeListGraph()
        {
            this.defaultStub = global::Microsoft.Stubs.DefaultStub
                <global::QuickGraph.Stubs.SEdgeListGraph<TVertex, TEdge>>.Current;
        }

        /// <summary>Gets or sets the default stub implementation.</summary>
        public global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs.SEdgeListGraph<TVertex, TEdge>> DefaultStub
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                return this.defaultStub;
            }
            [global::System.Diagnostics.DebuggerHidden]
            set
            {
                this.defaultStub = value;
            }
        }

        private global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs.SEdgeListGraph<TVertex, TEdge>> defaultStub;
    }
}
namespace QuickGraph.Algorithms.Stubs
{
    /// <summary>Stub of IEdgePredecessorRecorderAlgorithm`2</summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Stubs", "0.1.2.3")]
    [global::System.Serializable]
    [global::System.Diagnostics.DebuggerStepThrough]
    public partial class SEdgePredecessorRecorderAlgorithm<TVertex,TEdge>
      : global::Microsoft.Stubs.StubBase<global::QuickGraph.Algorithms.Stubs.SEdgePredecessorRecorderAlgorithm<TVertex, TEdge>>
      , global::QuickGraph.Algorithms.IEdgePredecessorRecorderAlgorithm<TVertex, TEdge>
        where TEdge : global::QuickGraph.IEdge<TVertex>
    {
        /// <summary>Initializes a new instance of type SEdgePredecessorRecorderAlgorithm</summary>
        [global::System.Diagnostics.DebuggerHidden]
        public SEdgePredecessorRecorderAlgorithm()
        {
        }

        /// <summary>Event DiscoverTreeEdge</summary>
        public event global::QuickGraph.EdgeEdgeAction<TVertex, TEdge> DiscoverTreeEdge;

        /// <summary>Event FinishEdge</summary>
        public event global::QuickGraph.EdgeAction<TVertex, TEdge> FinishEdge;
    }
}
namespace QuickGraph.Algorithms.Stubs
{
    /// <summary>Stub of IEndPathEdgeRecorderAlgorithm`2</summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Stubs", "0.1.2.3")]
    [global::System.Serializable]
    [global::System.Diagnostics.DebuggerStepThrough]
    public partial class SEndPathEdgeRecorderAlgorithm<TVertex,TEdge>
      : global::Microsoft.Stubs.StubBase<global::QuickGraph.Algorithms.Stubs.SEndPathEdgeRecorderAlgorithm<TVertex, TEdge>>
      , global::QuickGraph.Algorithms.IEndPathEdgeRecorderAlgorithm<TVertex, TEdge>
        where TEdge : global::QuickGraph.IEdge<TVertex>
    {
        /// <summary>Initializes a new instance of type SEndPathEdgeRecorderAlgorithm</summary>
        [global::System.Diagnostics.DebuggerHidden]
        public SEndPathEdgeRecorderAlgorithm()
        {
        }
    }
}
namespace QuickGraph.Petri.Stubs
{
    /// <summary>Stub of IExpression`1</summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Stubs", "0.1.2.3")]
    [global::System.Serializable]
    [global::System.Diagnostics.DebuggerStepThrough]
    public partial class SExpression<Token>
      : global::Microsoft.Stubs.StubBase<global::QuickGraph.Petri.Stubs.SExpression<Token>>
      , global::QuickGraph.Petri.IExpression<Token>
    {
        /// <summary>Initializes a new instance of type SExpression</summary>
        [global::System.Diagnostics.DebuggerHidden]
        public SExpression()
        {
        }

        /// <summary>Stub of method System.Collections.Generic.IList`1&lt;!0&gt; QuickGraph.Petri.IExpression`1.Eval(System.Collections.Generic.IList`1&lt;!0&gt; marking)</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Petri.Stubs.SExpression<Token>, global::System.Collections.Generic.IList<Token>, global::System.Collections.Generic.IList<Token>> Eval;

        /// <summary>Stub of method System.Collections.Generic.IList`1&lt;!0&gt; QuickGraph.Petri.IExpression`1.Eval(System.Collections.Generic.IList`1&lt;!0&gt; marking)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        global::System.Collections.Generic.IList<Token> global::QuickGraph.Petri.IExpression<Token>.Eval(global::System.Collections.Generic.IList<Token> marking)
        {
            global::Microsoft.Stubs.StubDelegates
              .Func<global::QuickGraph.Petri.Stubs.SExpression<Token>, 
              global::System.Collections.Generic.IList<Token>, 
              global::System.Collections.Generic.IList<Token>> sh = this.Eval;
            if ((object)sh != (object)null)
              return sh.Invoke(this, marking);
            else 
            {
              global::Microsoft.Stubs
                .IDefaultStub<global::QuickGraph.Petri.Stubs.SExpression<Token>> stub
                 = ((global::Microsoft.Stubs
                  .IStub<global::QuickGraph.Petri.Stubs.SExpression<Token>>)this)
                  .DefaultStub;
              return stub.Result<global::System.Collections.Generic.IList<Token>>(this);
            }
        }
    }
}
namespace QuickGraph.Collections.Stubs
{
    /// <summary>Stub of ForestDisjointSet`1</summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Stubs", "0.1.2.3")]
    [global::System.Serializable]
    [global::System.Diagnostics.DebuggerStepThrough]
    public partial class SForestDisjointSet<T>
      : global::QuickGraph.Collections.ForestDisjointSet<T>
      , global::Microsoft.Stubs.IStub<global::QuickGraph.Collections.Stubs.SForestDisjointSet<T>>
    {
        /// <summary>Initializes a new instance of type SForestDisjointSet</summary>
        [global::System.Diagnostics.DebuggerHidden]
        public SForestDisjointSet()
        {
            this.defaultStub = global::Microsoft.Stubs.DefaultStub
                <global::QuickGraph.Collections.Stubs.SForestDisjointSet<T>>.Current;
        }

        /// <summary>Gets or sets the default stub implementation.</summary>
        public global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Collections.Stubs.SForestDisjointSet<T>> DefaultStub
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                return this.defaultStub;
            }
            [global::System.Diagnostics.DebuggerHidden]
            set
            {
                this.defaultStub = value;
            }
        }

        private global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Collections.Stubs.SForestDisjointSet<T>> defaultStub;
    }
}
namespace QuickGraph.Algorithms.RandomWalks.Stubs
{
    /// <summary>Stub of MarkovEdgeChainBase`2</summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Stubs", "0.1.2.3")]
    [global::System.Serializable]
    [global::System.Diagnostics.DebuggerStepThrough]
    public partial class SMarkovEdgeChain<TVertex,TEdge>
      : global::QuickGraph.Algorithms.RandomWalks.MarkovEdgeChainBase<TVertex, TEdge>
      , global::Microsoft.Stubs.IStub<global::QuickGraph.Algorithms.RandomWalks.Stubs.SMarkovEdgeChain<TVertex, TEdge>>
        where TEdge : global::QuickGraph.IEdge<TVertex>
    {
        /// <summary>Initializes a new instance of type SMarkovEdgeChain</summary>
        [global::System.Diagnostics.DebuggerHidden]
        public SMarkovEdgeChain()
        {
            this.defaultStub = global::Microsoft.Stubs
              .DefaultStub<global::QuickGraph.Algorithms.RandomWalks.Stubs
                .SMarkovEdgeChain<TVertex, TEdge>>.Current;
        }

        /// <summary>Gets or sets the default stub implementation.</summary>
        public global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Algorithms.RandomWalks.Stubs.SMarkovEdgeChain<TVertex, TEdge>> DefaultStub
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                return this.defaultStub;
            }
            [global::System.Diagnostics.DebuggerHidden]
            set
            {
                this.defaultStub = value;
            }
        }

        /// <summary>Stub of method !1 QuickGraph.Algorithms.RandomWalks.MarkovEdgeChainBase`2.Successor(QuickGraph.IImplicitGraph`2&lt;!0,!1&gt; g, !0 u)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        public override TEdge Successor(global::QuickGraph.IImplicitGraph<TVertex, TEdge> g, TVertex u)
        {
            global::Microsoft.Stubs
              .IDefaultStub<global::QuickGraph.Algorithms.RandomWalks.Stubs
                .SMarkovEdgeChain<TVertex, TEdge>> stub = ((global::Microsoft.Stubs
              .IStub<global::QuickGraph.Algorithms.RandomWalks.Stubs
                .SMarkovEdgeChain<TVertex, TEdge>>)this).DefaultStub;
            return stub.Result<TEdge>(this);
        }

        private global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Algorithms.RandomWalks.Stubs.SMarkovEdgeChain<TVertex, TEdge>> defaultStub;
    }
}
namespace QuickGraph.Stubs
{
    /// <summary>Stub of NegativeCycleGraphException</summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Stubs", "0.1.2.3")]
    [global::System.Serializable]
    [global::System.Diagnostics.DebuggerStepThrough]
    public partial class SNegativeCycleGraphException
      : global::QuickGraph.NegativeCycleGraphException
      , global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs.SNegativeCycleGraphException>
    {
        /// <summary>Initializes a new instance of type SNegativeCycleGraphException</summary>
        [global::System.Diagnostics.DebuggerHidden]
        public SNegativeCycleGraphException()
        {
            this.defaultStub = global::Microsoft.Stubs
              .DefaultStub<global::QuickGraph.Stubs.SNegativeCycleGraphException>.Current;
        }

        /// <summary>Gets or sets the default stub implementation.</summary>
        public global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs.SNegativeCycleGraphException> DefaultStub
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                return this.defaultStub;
            }
            [global::System.Diagnostics.DebuggerHidden]
            set
            {
                this.defaultStub = value;
            }
        }

        private global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs.SNegativeCycleGraphException> defaultStub;
    }
}
namespace QuickGraph.Stubs
{
    /// <summary>Stub of NegativeWeightException</summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Stubs", "0.1.2.3")]
    [global::System.Serializable]
    [global::System.Diagnostics.DebuggerStepThrough]
    public partial class SNegativeWeightException
      : global::QuickGraph.NegativeWeightException
      , global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs.SNegativeWeightException>
    {
        /// <summary>Initializes a new instance of type SNegativeWeightException</summary>
        [global::System.Diagnostics.DebuggerHidden]
        public SNegativeWeightException()
        {
            this.defaultStub = global::Microsoft.Stubs
              .DefaultStub<global::QuickGraph.Stubs.SNegativeWeightException>.Current;
        }

        /// <summary>Gets or sets the default stub implementation.</summary>
        public global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs.SNegativeWeightException> DefaultStub
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                return this.defaultStub;
            }
            [global::System.Diagnostics.DebuggerHidden]
            set
            {
                this.defaultStub = value;
            }
        }

        private global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs.SNegativeWeightException> defaultStub;
    }
}
namespace QuickGraph.Stubs
{
    /// <summary>Stub of NonAcyclicGraphException</summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Stubs", "0.1.2.3")]
    [global::System.Serializable]
    [global::System.Diagnostics.DebuggerStepThrough]
    public partial class SNonAcyclicGraphException
      : global::QuickGraph.NonAcyclicGraphException
      , global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs.SNonAcyclicGraphException>
    {
        /// <summary>Initializes a new instance of type SNonAcyclicGraphException</summary>
        [global::System.Diagnostics.DebuggerHidden]
        public SNonAcyclicGraphException()
        {
            this.defaultStub = global::Microsoft.Stubs
              .DefaultStub<global::QuickGraph.Stubs.SNonAcyclicGraphException>.Current;
        }

        /// <summary>Gets or sets the default stub implementation.</summary>
        public global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs.SNonAcyclicGraphException> DefaultStub
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                return this.defaultStub;
            }
            [global::System.Diagnostics.DebuggerHidden]
            set
            {
                this.defaultStub = value;
            }
        }

        private global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs.SNonAcyclicGraphException> defaultStub;
    }
}
namespace QuickGraph.Stubs
{
    /// <summary>Stub of NonStronglyConnectedGraphException</summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Stubs", "0.1.2.3")]
    [global::System.Serializable]
    [global::System.Diagnostics.DebuggerStepThrough]
    public partial class SNonStronglyConnectedGraphException
      : global::QuickGraph.NonStronglyConnectedGraphException
      , global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs.SNonStronglyConnectedGraphException>
    {
        /// <summary>Initializes a new instance of type SNonStronglyConnectedGraphException</summary>
        [global::System.Diagnostics.DebuggerHidden]
        public SNonStronglyConnectedGraphException()
        {
            this.defaultStub = global::Microsoft.Stubs.DefaultStub
                <global::QuickGraph.Stubs.SNonStronglyConnectedGraphException>.Current;
        }

        /// <summary>Gets or sets the default stub implementation.</summary>
        public global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs.SNonStronglyConnectedGraphException> DefaultStub
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                return this.defaultStub;
            }
            [global::System.Diagnostics.DebuggerHidden]
            set
            {
                this.defaultStub = value;
            }
        }

        private global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs.SNonStronglyConnectedGraphException> defaultStub;
    }
}
namespace QuickGraph.Algorithms.Observers.Stubs
{
    /// <summary>Stub of IObserver`1</summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Stubs", "0.1.2.3")]
    [global::System.Serializable]
    [global::System.Diagnostics.DebuggerStepThrough]
    public partial class SObserver<TAlgorithm>
      : global::Microsoft.Stubs.StubBase<global::QuickGraph.Algorithms.Observers.Stubs.SObserver<TAlgorithm>>
      , global::QuickGraph.Algorithms.Observers.IObserver<TAlgorithm>
    {
        /// <summary>Initializes a new instance of type SObserver</summary>
        [global::System.Diagnostics.DebuggerHidden]
        public SObserver()
        {
        }

        /// <summary>Stub of method System.Void QuickGraph.Algorithms.Observers.IObserver`1.Attach(!0 algorithm)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        void global::QuickGraph.Algorithms.Observers.IObserver<TAlgorithm>.Attach(TAlgorithm algorithm)
        {
            global::Microsoft.Stubs
              .IDefaultStub<global::QuickGraph.Algorithms.Observers.Stubs
                .SObserver<TAlgorithm>> stub = (
            (global::Microsoft.Stubs.IStub<global::QuickGraph.Algorithms.Observers.Stubs
              .SObserver<TAlgorithm>>)this).DefaultStub;
            stub.VoidResult(this);
        }

        /// <summary>Stub of method System.Void QuickGraph.Algorithms.Observers.IObserver`1.Detach(!0 algorithm)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        void global::QuickGraph.Algorithms.Observers.IObserver<TAlgorithm>.Detach(TAlgorithm algorithm)
        {
            global::Microsoft.Stubs
              .IDefaultStub<global::QuickGraph.Algorithms.Observers.Stubs
                .SObserver<TAlgorithm>> stub = (
            (global::Microsoft.Stubs.IStub<global::QuickGraph.Algorithms.Observers.Stubs
              .SObserver<TAlgorithm>>)this).DefaultStub;
            stub.VoidResult(this);
        }
    }
}
namespace QuickGraph.Stubs
{
    /// <summary>Stub of ParallelEdgeNotAllowedException</summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Stubs", "0.1.2.3")]
    [global::System.Serializable]
    [global::System.Diagnostics.DebuggerStepThrough]
    public partial class SParallelEdgeNotAllowedException
      : global::QuickGraph.ParallelEdgeNotAllowedException
      , global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs.SParallelEdgeNotAllowedException>
    {
        /// <summary>Initializes a new instance of type SParallelEdgeNotAllowedException</summary>
        [global::System.Diagnostics.DebuggerHidden]
        public SParallelEdgeNotAllowedException()
        {
            this.defaultStub = global::Microsoft.Stubs.DefaultStub
                <global::QuickGraph.Stubs.SParallelEdgeNotAllowedException>.Current;
        }

        /// <summary>Gets or sets the default stub implementation.</summary>
        public global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs.SParallelEdgeNotAllowedException> DefaultStub
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                return this.defaultStub;
            }
            [global::System.Diagnostics.DebuggerHidden]
            set
            {
                this.defaultStub = value;
            }
        }

        private global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs.SParallelEdgeNotAllowedException> defaultStub;
    }
}
namespace QuickGraph.Stubs
{
    /// <summary>Stub of QuickGraphException</summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Stubs", "0.1.2.3")]
    [global::System.Serializable]
    [global::System.Diagnostics.DebuggerStepThrough]
    public partial class SQuickGraphException
      : global::QuickGraph.QuickGraphException
      , global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs.SQuickGraphException>
    {
        /// <summary>Initializes a new instance of type SQuickGraphException</summary>
        [global::System.Diagnostics.DebuggerHidden]
        public SQuickGraphException()
        {
            this.defaultStub = global::Microsoft.Stubs
              .DefaultStub<global::QuickGraph.Stubs.SQuickGraphException>.Current;
        }

        /// <summary>Gets or sets the default stub implementation.</summary>
        public global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs.SQuickGraphException> DefaultStub
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                return this.defaultStub;
            }
            [global::System.Diagnostics.DebuggerHidden]
            set
            {
                this.defaultStub = value;
            }
        }

        private global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs.SQuickGraphException> defaultStub;
    }
}
namespace QuickGraph.Stubs
{
    /// <summary>Stub of RootVertexNotSpecifiedException</summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Stubs", "0.1.2.3")]
    [global::System.Serializable]
    [global::System.Diagnostics.DebuggerStepThrough]
    public partial class SRootVertexNotSpecifiedException
      : global::QuickGraph.RootVertexNotSpecifiedException
      , global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs.SRootVertexNotSpecifiedException>
    {
        /// <summary>Initializes a new instance of type SRootVertexNotSpecifiedException</summary>
        [global::System.Diagnostics.DebuggerHidden]
        public SRootVertexNotSpecifiedException()
        {
            this.defaultStub = global::Microsoft.Stubs.DefaultStub
                <global::QuickGraph.Stubs.SRootVertexNotSpecifiedException>.Current;
        }

        /// <summary>Gets or sets the default stub implementation.</summary>
        public global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs.SRootVertexNotSpecifiedException> DefaultStub
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                return this.defaultStub;
            }
            [global::System.Diagnostics.DebuggerHidden]
            set
            {
                this.defaultStub = value;
            }
        }

        private global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs.SRootVertexNotSpecifiedException> defaultStub;
    }
}
namespace QuickGraph.Serialization.Stubs
{
    /// <summary>Stub of SerializerBase`2</summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Stubs", "0.1.2.3")]
    [global::System.Serializable]
    [global::System.Diagnostics.DebuggerStepThrough]
    public partial class SSerializer<TVertex,TEdge>
      : global::QuickGraph.Serialization.SerializerBase<TVertex, TEdge>
      , global::Microsoft.Stubs.IStub<global::QuickGraph.Serialization.Stubs.SSerializer<TVertex, TEdge>>
        where TEdge : global::QuickGraph.IEdge<TVertex>
    {
        /// <summary>Initializes a new instance of type SSerializer</summary>
        [global::System.Diagnostics.DebuggerHidden]
        public SSerializer()
        {
            this.defaultStub =
              global::Microsoft.Stubs.DefaultStub<global::QuickGraph.Serialization.Stubs
                .SSerializer<TVertex, TEdge>>.Current;
        }

        /// <summary>Gets or sets the default stub implementation.</summary>
        public global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Serialization.Stubs.SSerializer<TVertex, TEdge>> DefaultStub
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                return this.defaultStub;
            }
            [global::System.Diagnostics.DebuggerHidden]
            set
            {
                this.defaultStub = value;
            }
        }

        private global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Serialization.Stubs.SSerializer<TVertex, TEdge>> defaultStub;
    }
}
namespace QuickGraph.Algorithms.Services.Stubs
{
    /// <summary>Stub of IService</summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Stubs", "0.1.2.3")]
    [global::System.Serializable]
    [global::System.Diagnostics.DebuggerStepThrough]
    public partial class SService
      : global::Microsoft.Stubs.StubBase<global::QuickGraph.Algorithms.Services.Stubs.SService>
      , global::QuickGraph.Algorithms.Services.IService
    {
        /// <summary>Initializes a new instance of type SService</summary>
        [global::System.Diagnostics.DebuggerHidden]
        public SService()
        {
        }
    }
}
namespace QuickGraph.Algorithms.Exploration.Stubs
{
    /// <summary>Stub of ITransitionFactory`2</summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Stubs", "0.1.2.3")]
    [global::System.Serializable]
    [global::System.Diagnostics.DebuggerStepThrough]
    public partial class STransitionFactory<TVertex,TEdge>
      : global::Microsoft.Stubs.StubBase<global::QuickGraph.Algorithms.Exploration.Stubs.STransitionFactory<TVertex, TEdge>>
      , global::QuickGraph.Algorithms.Exploration.ITransitionFactory<TVertex, TEdge>
        where TVertex : global::System.ICloneable
        where TEdge : global::QuickGraph.IEdge<TVertex>
    {
        /// <summary>Initializes a new instance of type STransitionFactory</summary>
        [global::System.Diagnostics.DebuggerHidden]
        public STransitionFactory()
        {
        }

        /// <summary>Stub of method System.Collections.Generic.IEnumerable`1&lt;!1&gt; QuickGraph.Algorithms.Exploration.ITransitionFactory`2.Apply(!0 source)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        global::System.Collections.Generic.IEnumerable<TEdge> global::QuickGraph.Algorithms.Exploration.ITransitionFactory<TVertex, TEdge>.Apply(TVertex source)
        {
            global::Microsoft.Stubs
              .IDefaultStub<global::QuickGraph.Algorithms.Exploration.Stubs
                .STransitionFactory<TVertex, TEdge>> stub = ((global::Microsoft.Stubs
              .IStub<global::QuickGraph.Algorithms.Exploration.Stubs
                .STransitionFactory<TVertex, TEdge>>)this).DefaultStub;
            return stub.Result<global::System.Collections.Generic.IEnumerable<TEdge>>(this);
        }

        /// <summary>Stub of method System.Boolean QuickGraph.Algorithms.Exploration.ITransitionFactory`2.IsValid(!0 v)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.Algorithms.Exploration.ITransitionFactory<TVertex, TEdge>.IsValid(TVertex v)
        {
            global::Microsoft.Stubs
              .IDefaultStub<global::QuickGraph.Algorithms.Exploration.Stubs
                .STransitionFactory<TVertex, TEdge>> stub = ((global::Microsoft.Stubs
              .IStub<global::QuickGraph.Algorithms.Exploration.Stubs
                .STransitionFactory<TVertex, TEdge>>)this).DefaultStub;
            return stub.Result<bool>(this);
        }
    }
}
namespace QuickGraph.Algorithms.Stubs
{
    /// <summary>Stub of ITreeBuilderAlgorithm`2</summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Stubs", "0.1.2.3")]
    [global::System.Serializable]
    [global::System.Diagnostics.DebuggerStepThrough]
    public partial class STreeBuilderAlgorithm<TVertex,TEdge>
      : global::Microsoft.Stubs.StubBase<global::QuickGraph.Algorithms.Stubs.STreeBuilderAlgorithm<TVertex, TEdge>>
      , global::QuickGraph.Algorithms.ITreeBuilderAlgorithm<TVertex, TEdge>
        where TEdge : global::QuickGraph.IEdge<TVertex>
    {
        /// <summary>Initializes a new instance of type STreeBuilderAlgorithm</summary>
        [global::System.Diagnostics.DebuggerHidden]
        public STreeBuilderAlgorithm()
        {
        }

        /// <summary>Event TreeEdge</summary>
        public event global::QuickGraph.EdgeAction<TVertex, TEdge> TreeEdge;
    }
}
namespace QuickGraph.Stubs
{
    /// <summary>Stub of UndirectedGraph`2</summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Stubs", "0.1.2.3")]
    [global::System.Serializable]
    [global::System.Diagnostics.DebuggerStepThrough]
    public partial class SUndirectedGraph<TVertex,TEdge>
      : global::QuickGraph.UndirectedGraph<TVertex, TEdge>
      , global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs.SUndirectedGraph<TVertex, TEdge>>
        where TEdge : global::QuickGraph.IEdge<TVertex>
    {
        /// <summary>Initializes a new instance of type SUndirectedGraph</summary>
        [global::System.Diagnostics.DebuggerHidden]
        public SUndirectedGraph()
        {
            this.defaultStub = global::Microsoft.Stubs.DefaultStub
                <global::QuickGraph.Stubs.SUndirectedGraph<TVertex, TEdge>>.Current;
        }

        /// <summary>Gets or sets the default stub implementation.</summary>
        public global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs.SUndirectedGraph<TVertex, TEdge>> DefaultStub
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                return this.defaultStub;
            }
            [global::System.Diagnostics.DebuggerHidden]
            set
            {
                this.defaultStub = value;
            }
        }

        private global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs.SUndirectedGraph<TVertex, TEdge>> defaultStub;
    }
}
namespace QuickGraph.Algorithms.Stubs
{
    /// <summary>Stub of IUndirectedTreeBuilderAlgorithm`2</summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Stubs", "0.1.2.3")]
    [global::System.Serializable]
    [global::System.Diagnostics.DebuggerStepThrough]
    public partial class SUndirectedTreeBuilderAlgorithm<TVertex,TEdge>
      : global::Microsoft.Stubs.StubBase<global::QuickGraph.Algorithms.Stubs.SUndirectedTreeBuilderAlgorithm<TVertex, TEdge>>
      , global::QuickGraph.Algorithms.IUndirectedTreeBuilderAlgorithm<TVertex, TEdge>
        where TEdge : global::QuickGraph.IEdge<TVertex>
    {
        /// <summary>Initializes a new instance of type SUndirectedTreeBuilderAlgorithm</summary>
        [global::System.Diagnostics.DebuggerHidden]
        public SUndirectedTreeBuilderAlgorithm()
        {
        }

        /// <summary>Event TreeEdge</summary>
        public event global::QuickGraph.UndirectedEdgeAction<TVertex, TEdge> TreeEdge;
    }
}
namespace QuickGraph.Algorithms.Stubs
{
    /// <summary>Stub of IUndirectedVertexPredecessorRecorderAlgorithm`2</summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Stubs", "0.1.2.3")]
    [global::System.Serializable]
    [global::System.Diagnostics.DebuggerStepThrough]
    public partial class SUndirectedVertexPredecessorRecorderAlgorithm<TVertex,TEdge>
      : global::Microsoft.Stubs.StubBase<global::QuickGraph.Algorithms.Stubs.SUndirectedVertexPredecessorRecorderAlgorithm<TVertex, TEdge>>
      , global::QuickGraph.Algorithms.IUndirectedVertexPredecessorRecorderAlgorithm<TVertex, TEdge>
        where TEdge : global::QuickGraph.IEdge<TVertex>
    {
        /// <summary>Initializes a new instance of type SUndirectedVertexPredecessorRecorderAlgorithm</summary>
        [global::System.Diagnostics.DebuggerHidden]
        public SUndirectedVertexPredecessorRecorderAlgorithm()
        {
        }

        /// <summary>Event FinishVertex</summary>
        public event global::QuickGraph.VertexAction<TVertex> FinishVertex;

        /// <summary>Event StartVertex</summary>
        public event global::QuickGraph.VertexAction<TVertex> StartVertex;

        /// <summary>Event TreeEdge</summary>
        public event global::QuickGraph.UndirectedEdgeAction<TVertex, TEdge> TreeEdge;
    }
}
namespace QuickGraph.Algorithms.Stubs
{
    /// <summary>Stub of IVertexColorizerAlgorithm`2</summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Stubs", "0.1.2.3")]
    [global::System.Serializable]
    [global::System.Diagnostics.DebuggerStepThrough]
    public partial class SVertexColorizerAlgorithm<TVertex,TEdge>
      : global::Microsoft.Stubs.StubBase<global::QuickGraph.Algorithms.Stubs.SVertexColorizerAlgorithm<TVertex, TEdge>>
      , global::QuickGraph.Algorithms.IVertexColorizerAlgorithm<TVertex, TEdge>
        where TEdge : global::QuickGraph.IEdge<TVertex>
    {
        /// <summary>Initializes a new instance of type SVertexColorizerAlgorithm</summary>
        [global::System.Diagnostics.DebuggerHidden]
        public SVertexColorizerAlgorithm()
        {
        }

        /// <summary>Stub of method QuickGraph.GraphColor QuickGraph.Algorithms.IVertexColorizerAlgorithm`2.GetVertexColor(!0 v)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        global::QuickGraph.GraphColor global::QuickGraph.Algorithms.IVertexColorizerAlgorithm<TVertex, TEdge>.GetVertexColor(TVertex v)
        {
            global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Algorithms.Stubs
              .SVertexColorizerAlgorithm<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Algorithms.Stubs
                .SVertexColorizerAlgorithm<TVertex, TEdge>>)this).DefaultStub;
            return stub.Result<global::QuickGraph.GraphColor>(this);
        }
    }
}
namespace QuickGraph.Algorithms.Stubs
{
    /// <summary>Stub of IVertexPredecessorRecorderAlgorithm`2</summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Stubs", "0.1.2.3")]
    [global::System.Serializable]
    [global::System.Diagnostics.DebuggerStepThrough]
    public partial class SVertexPredecessorRecorderAlgorithm<TVertex,TEdge>
      : global::Microsoft.Stubs.StubBase<global::QuickGraph.Algorithms.Stubs.SVertexPredecessorRecorderAlgorithm<TVertex, TEdge>>
      , global::QuickGraph.Algorithms.IVertexPredecessorRecorderAlgorithm<TVertex, TEdge>
        where TEdge : global::QuickGraph.IEdge<TVertex>
    {
        /// <summary>Initializes a new instance of type SVertexPredecessorRecorderAlgorithm</summary>
        [global::System.Diagnostics.DebuggerHidden]
        public SVertexPredecessorRecorderAlgorithm()
        {
        }

        /// <summary>Event FinishVertex</summary>
        public event global::QuickGraph.VertexAction<TVertex> FinishVertex;

        /// <summary>Event StartVertex</summary>
        public event global::QuickGraph.VertexAction<TVertex> StartVertex;

        /// <summary>Event TreeEdge</summary>
        public event global::QuickGraph.EdgeAction<TVertex, TEdge> TreeEdge;
    }
}
namespace QuickGraph.Algorithms.Stubs
{
    /// <summary>Stub of IVertexTimeStamperAlgorithm`2</summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Stubs", "0.1.2.3")]
    [global::System.Serializable]
    [global::System.Diagnostics.DebuggerStepThrough]
    public partial class SVertexTimeStamperAlgorithm<TVertex,TEdge>
      : global::Microsoft.Stubs.StubBase<global::QuickGraph.Algorithms.Stubs.SVertexTimeStamperAlgorithm<TVertex, TEdge>>
      , global::QuickGraph.Algorithms.IVertexTimeStamperAlgorithm<TVertex, TEdge>
        where TEdge : global::QuickGraph.IEdge<TVertex>
    {
        /// <summary>Initializes a new instance of type SVertexTimeStamperAlgorithm</summary>
        [global::System.Diagnostics.DebuggerHidden]
        public SVertexTimeStamperAlgorithm()
        {
        }

        /// <summary>Event DiscoverVertex</summary>
        public event global::QuickGraph.VertexAction<TVertex> DiscoverVertex;

        /// <summary>Event FinishVertex</summary>
        public event global::QuickGraph.VertexAction<TVertex> FinishVertex;
    }
}

