#pragma warning disable 0067, 0108
// ------------------------------------
// 
// Assembly QuickGraph
// 
// ------------------------------------
namespace QuickGraph.Algorithms.Stubs
{
    /// <summary>Stub of IAlgorithm`1</summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Stubs", "0.1.2.3")]
    [global::System.Serializable]
    [global::System.Diagnostics.DebuggerStepThrough]
    public partial class SAlgorithm<TGraph>
      : global::Microsoft.Stubs.StubBase<global::QuickGraph.Algorithms.Stubs.SAlgorithm<TGraph>>
      , global::QuickGraph.Algorithms.IAlgorithm<TGraph>
    {
        /// <summary>Initializes a new instance of type SAlgorithm</summary>
        [global::System.Diagnostics.DebuggerHidden]
        public SAlgorithm()
        {
        }

        /// <summary>Stub of method System.Void QuickGraph.Algorithms.IComputation.Abort()</summary>
        public global::Microsoft.Stubs.StubDelegates.Action<global::QuickGraph.Algorithms.Stubs.SAlgorithm<TGraph>> Abort;

        /// <summary>Event Aborted</summary>
        public event global::System.EventHandler Aborted;

        /// <summary>Stub of method System.Void QuickGraph.Algorithms.IComputation.Compute()</summary>
        public global::Microsoft.Stubs.StubDelegates.Action<global::QuickGraph.Algorithms.Stubs.SAlgorithm<TGraph>> Compute;

        /// <summary>Event Finished</summary>
        public event global::System.EventHandler Finished;

        /// <summary>Stub of method System.Void QuickGraph.Algorithms.IComputation.Abort()</summary>
        [global::System.Diagnostics.DebuggerHidden]
        void global::QuickGraph.Algorithms.IComputation.Abort()
        {
            global::Microsoft.Stubs.StubDelegates
              .Action<global::QuickGraph.Algorithms.Stubs.SAlgorithm<TGraph>> sh = this.Abort;
            if ((object)sh != (object)null)
              sh.Invoke(this);
            else 
            {
              global::Microsoft.Stubs.IDefaultStub
                  <global::QuickGraph.Algorithms.Stubs.SAlgorithm<TGraph>> stub
                 = ((global::Microsoft.Stubs
                  .IStub<global::QuickGraph.Algorithms.Stubs.SAlgorithm<TGraph>>)this)
                  .DefaultStub;
              stub.VoidResult(this);
            }
        }

        /// <summary>Stub of method System.Void QuickGraph.Algorithms.IComputation.Compute()</summary>
        [global::System.Diagnostics.DebuggerHidden]
        void global::QuickGraph.Algorithms.IComputation.Compute()
        {
            global::Microsoft.Stubs.StubDelegates
              .Action<global::QuickGraph.Algorithms.Stubs.SAlgorithm<TGraph>> sh
               = this.Compute;
            if ((object)sh != (object)null)
              sh.Invoke(this);
            else 
            {
              global::Microsoft.Stubs.IDefaultStub
                  <global::QuickGraph.Algorithms.Stubs.SAlgorithm<TGraph>> stub
                 = ((global::Microsoft.Stubs
                  .IStub<global::QuickGraph.Algorithms.Stubs.SAlgorithm<TGraph>>)this)
                  .DefaultStub;
              stub.VoidResult(this);
            }
        }

        /// <summary>Event Started</summary>
        public event global::System.EventHandler Started;

        /// <summary>Stub of property QuickGraph.Algorithms.IComputation.State</summary>
        global::QuickGraph.Algorithms.ComputationState global::QuickGraph.Algorithms.IComputation.State
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates
                  .Func<global::QuickGraph.Algorithms.Stubs.SAlgorithm<TGraph>, 
                  global::QuickGraph.Algorithms.ComputationState> sh = this.StateGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs.IDefaultStub
                      <global::QuickGraph.Algorithms.Stubs.SAlgorithm<TGraph>> stub
                     = ((global::Microsoft.Stubs
                      .IStub<global::QuickGraph.Algorithms.Stubs.SAlgorithm<TGraph>>)this)
                      .DefaultStub;
                  return stub.Result<global::QuickGraph.Algorithms.ComputationState>(this);
                }
            }
        }

        /// <summary>Event StateChanged</summary>
        public event global::System.EventHandler StateChanged;

        /// <summary>Stub of method QuickGraph.Algorithms.ComputationState QuickGraph.Algorithms.IComputation.get_State()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Algorithms.Stubs.SAlgorithm<TGraph>, global::QuickGraph.Algorithms.ComputationState> StateGet;

        /// <summary>Stub of property QuickGraph.Algorithms.IComputation.SyncRoot</summary>
        object global::QuickGraph.Algorithms.IComputation.SyncRoot
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates
                  .Func<global::QuickGraph.Algorithms.Stubs.SAlgorithm<TGraph>, 
                  object> sh = this.SyncRootGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs.IDefaultStub
                      <global::QuickGraph.Algorithms.Stubs.SAlgorithm<TGraph>> stub
                     = ((global::Microsoft.Stubs
                      .IStub<global::QuickGraph.Algorithms.Stubs.SAlgorithm<TGraph>>)this)
                      .DefaultStub;
                  return stub.Result<object>(this);
                }
            }
        }

        /// <summary>Stub of method System.Object QuickGraph.Algorithms.IComputation.get_SyncRoot()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Algorithms.Stubs.SAlgorithm<TGraph>, object> SyncRootGet;

        /// <summary>Stub of property QuickGraph.Algorithms.IAlgorithm`1.VisitedGraph</summary>
        TGraph global::QuickGraph.Algorithms.IAlgorithm<TGraph>.VisitedGraph
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates
                  .Func<global::QuickGraph.Algorithms.Stubs.SAlgorithm<TGraph>, 
                  TGraph> sh = this.VisitedGraphGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs.IDefaultStub
                      <global::QuickGraph.Algorithms.Stubs.SAlgorithm<TGraph>> stub
                     = ((global::Microsoft.Stubs
                      .IStub<global::QuickGraph.Algorithms.Stubs.SAlgorithm<TGraph>>)this)
                      .DefaultStub;
                  return stub.Result<TGraph>(this);
                }
            }
        }

        /// <summary>Stub of method !0 QuickGraph.Algorithms.IAlgorithm`1.get_VisitedGraph()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Algorithms.Stubs.SAlgorithm<TGraph>, TGraph> VisitedGraphGet;
    }
}
namespace QuickGraph.Algorithms.Services.Stubs
{
    /// <summary>Stub of IAlgorithmComponent</summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Stubs", "0.1.2.3")]
    [global::System.Serializable]
    [global::System.Diagnostics.DebuggerStepThrough]
    public partial class SAlgorithmComponent
      : global::Microsoft.Stubs.StubBase<global::QuickGraph.Algorithms.Services.Stubs.SAlgorithmComponent>
      , global::QuickGraph.Algorithms.Services.IAlgorithmComponent
    {
        /// <summary>Initializes a new instance of type SAlgorithmComponent</summary>
        [global::System.Diagnostics.DebuggerHidden]
        public SAlgorithmComponent()
        {
        }

        /// <summary>Stub of method !!0 QuickGraph.Algorithms.Services.IAlgorithmComponent.GetService()</summary>
        [global::System.Diagnostics.DebuggerHidden]
        T global::QuickGraph.Algorithms.Services.IAlgorithmComponent.GetService<T>()
        {
            global::Microsoft.Stubs.IDefaultStub<
            global::QuickGraph.Algorithms.Services.Stubs.SAlgorithmComponent
            > stub = 
              ((global::Microsoft.Stubs.IStub<global::QuickGraph.Algorithms.Services.Stubs
                .SAlgorithmComponent>)this).DefaultStub;
            return stub.Result<T>(this);
        }

        /// <summary>Stub of method System.Boolean QuickGraph.Algorithms.Services.IAlgorithmComponent.TryGetService(!!0&amp; service)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.Algorithms.Services.IAlgorithmComponent.TryGetService<T>(out T service)
        {
            global::Microsoft.Stubs.IDefaultStub<
            global::QuickGraph.Algorithms.Services.Stubs.SAlgorithmComponent
            > stub = 
              ((global::Microsoft.Stubs.IStub<global::QuickGraph.Algorithms.Services.Stubs
                .SAlgorithmComponent>)this).DefaultStub;
            stub.ValueAtReturn<T>(this, out service);
            return stub.Result<bool>(this);
        }

        /// <summary>Stub of property QuickGraph.Algorithms.Services.IAlgorithmComponent.Services</summary>
        global::QuickGraph.Algorithms.Services.IAlgorithmServices global::QuickGraph.Algorithms.Services.IAlgorithmComponent.Services
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs
                  .StubDelegates.Func<global::QuickGraph.Algorithms.Services.Stubs
                    .SAlgorithmComponent, 
                  global::QuickGraph.Algorithms.Services.IAlgorithmServices> sh = this.ServicesGet
                  ;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs
                    .IDefaultStub<global::QuickGraph.Algorithms.Services.Stubs
                      .SAlgorithmComponent> stub = ((
                  global::Microsoft.Stubs.IStub<global::QuickGraph.Algorithms.Services.Stubs
                    .SAlgorithmComponent>)this).DefaultStub;
                  return 
                    stub.Result<global::QuickGraph.Algorithms.Services.IAlgorithmServices>(this);
                }
            }
        }

        /// <summary>Stub of method QuickGraph.Algorithms.Services.IAlgorithmServices QuickGraph.Algorithms.Services.IAlgorithmComponent.get_Services()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Algorithms.Services.Stubs.SAlgorithmComponent, global::QuickGraph.Algorithms.Services.IAlgorithmServices> ServicesGet;
    }
}
namespace QuickGraph.Algorithms.Services.Stubs
{
    /// <summary>Stub of IAlgorithmServices</summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Stubs", "0.1.2.3")]
    [global::System.Serializable]
    [global::System.Diagnostics.DebuggerStepThrough]
    public partial class SAlgorithmServices
      : global::Microsoft.Stubs.StubBase<global::QuickGraph.Algorithms.Services.Stubs.SAlgorithmServices>
      , global::QuickGraph.Algorithms.Services.IAlgorithmServices
    {
        /// <summary>Initializes a new instance of type SAlgorithmServices</summary>
        [global::System.Diagnostics.DebuggerHidden]
        public SAlgorithmServices()
        {
        }

        /// <summary>Stub of property QuickGraph.Algorithms.Services.IAlgorithmServices.CancelManager</summary>
        global::QuickGraph.Algorithms.Services.ICancelManager global::QuickGraph.Algorithms.Services.IAlgorithmServices.CancelManager
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs
                  .StubDelegates.Func<global::QuickGraph.Algorithms.Services.Stubs
                    .SAlgorithmServices, 
                  global::QuickGraph.Algorithms.Services.ICancelManager> sh
                   = this.CancelManagerGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs
                    .IDefaultStub<global::QuickGraph.Algorithms.Services.Stubs
                      .SAlgorithmServices> stub = ((
                  global::Microsoft.Stubs.IStub<global::QuickGraph.Algorithms.Services.Stubs
                    .SAlgorithmServices>)this).DefaultStub;
                  return 
                    stub.Result<global::QuickGraph.Algorithms.Services.ICancelManager>(this);
                }
            }
        }

        /// <summary>Stub of method QuickGraph.Algorithms.Services.ICancelManager QuickGraph.Algorithms.Services.IAlgorithmServices.get_CancelManager()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Algorithms.Services.Stubs.SAlgorithmServices, global::QuickGraph.Algorithms.Services.ICancelManager> CancelManagerGet;
    }
}
namespace QuickGraph.Petri.Stubs
{
    /// <summary>Stub of IArc`1</summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Stubs", "0.1.2.3")]
    [global::System.Serializable]
    [global::System.Diagnostics.DebuggerStepThrough]
    public partial class SArc<Token>
      : global::Microsoft.Stubs.StubBase<global::QuickGraph.Petri.Stubs.SArc<Token>>
      , global::QuickGraph.Petri.IArc<Token>
    {
        /// <summary>Initializes a new instance of type SArc</summary>
        [global::System.Diagnostics.DebuggerHidden]
        public SArc()
        {
        }

        /// <summary>Stub of property QuickGraph.Petri.IArc`1.Annotation</summary>
        global::QuickGraph.Petri.IExpression<Token> global::QuickGraph.Petri.IArc<Token>.Annotation
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs
                  .StubDelegates.Func<global::QuickGraph.Petri.Stubs.SArc<Token>, 
                  global::QuickGraph.Petri.IExpression<Token>> sh = this.AnnotationGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs
                    .IDefaultStub<global::QuickGraph.Petri.Stubs.SArc<Token>> stub = (
                  (global::Microsoft.Stubs.IStub<global::QuickGraph.Petri.Stubs.SArc<Token>>
                  )this).DefaultStub;
                  return stub.Result<global::QuickGraph.Petri.IExpression<Token>>(this);
                }
            }
            [global::System.Diagnostics.DebuggerHidden]
            set
            {
                global::Microsoft.Stubs
                  .StubDelegates.Action<global::QuickGraph.Petri.Stubs.SArc<Token>, 
                  global::QuickGraph.Petri.IExpression<Token>> sh = this.AnnotationSet;
                if ((object)sh != (object)null)
                  sh.Invoke(this, value);
                else 
                {
                  global::Microsoft.Stubs
                    .IDefaultStub<global::QuickGraph.Petri.Stubs.SArc<Token>> stub = (
                  (global::Microsoft.Stubs.IStub<global::QuickGraph.Petri.Stubs.SArc<Token>>
                  )this).DefaultStub;
                  stub.VoidResult(this);
                }
            }
        }

        /// <summary>Stub of method QuickGraph.Petri.IExpression`1&lt;!0&gt; QuickGraph.Petri.IArc`1.get_Annotation()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Petri.Stubs.SArc<Token>, global::QuickGraph.Petri.IExpression<Token>> AnnotationGet;

        /// <summary>Stub of method System.Void QuickGraph.Petri.IArc`1.set_Annotation(QuickGraph.Petri.IExpression`1&lt;!0&gt; value)</summary>
        public global::Microsoft.Stubs.StubDelegates.Action<global::QuickGraph.Petri.Stubs.SArc<Token>, global::QuickGraph.Petri.IExpression<Token>> AnnotationSet;

        /// <summary>Stub of property QuickGraph.Petri.IArc`1.IsInputArc</summary>
        bool global::QuickGraph.Petri.IArc<Token>.IsInputArc
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs
                  .StubDelegates.Func<global::QuickGraph.Petri.Stubs.SArc<Token>, bool> sh
                   = this.IsInputArcGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs
                    .IDefaultStub<global::QuickGraph.Petri.Stubs.SArc<Token>> stub = (
                  (global::Microsoft.Stubs.IStub<global::QuickGraph.Petri.Stubs.SArc<Token>>
                  )this).DefaultStub;
                  return stub.Result<bool>(this);
                }
            }
        }

        /// <summary>Stub of method System.Boolean QuickGraph.Petri.IArc`1.get_IsInputArc()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Petri.Stubs.SArc<Token>, bool> IsInputArcGet;

        /// <summary>Stub of property QuickGraph.Petri.IArc`1.Place</summary>
        global::QuickGraph.Petri.IPlace<Token> global::QuickGraph.Petri.IArc<Token>.Place
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs
                  .StubDelegates.Func<global::QuickGraph.Petri.Stubs.SArc<Token>, 
                  global::QuickGraph.Petri.IPlace<Token>> sh = this.PlaceGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs
                    .IDefaultStub<global::QuickGraph.Petri.Stubs.SArc<Token>> stub = (
                  (global::Microsoft.Stubs.IStub<global::QuickGraph.Petri.Stubs.SArc<Token>>
                  )this).DefaultStub;
                  return stub.Result<global::QuickGraph.Petri.IPlace<Token>>(this);
                }
            }
        }

        /// <summary>Stub of method QuickGraph.Petri.IPlace`1&lt;!0&gt; QuickGraph.Petri.IArc`1.get_Place()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Petri.Stubs.SArc<Token>, global::QuickGraph.Petri.IPlace<Token>> PlaceGet;

        /// <summary>Stub of property QuickGraph.IEdge`1.Source</summary>
        global::QuickGraph.Petri.IPetriVertex global::QuickGraph.IEdge<global::QuickGraph.Petri.IPetriVertex>.Source
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs
                  .StubDelegates.Func<global::QuickGraph.Petri.Stubs.SArc<Token>, 
                  global::QuickGraph.Petri.IPetriVertex> sh = this.SourceGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs
                    .IDefaultStub<global::QuickGraph.Petri.Stubs.SArc<Token>> stub = (
                  (global::Microsoft.Stubs.IStub<global::QuickGraph.Petri.Stubs.SArc<Token>>
                  )this).DefaultStub;
                  return stub.Result<global::QuickGraph.Petri.IPetriVertex>(this);
                }
            }
        }

        /// <summary>Stub of method !0 QuickGraph.IEdge`1.get_Source()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Petri.Stubs.SArc<Token>, global::QuickGraph.Petri.IPetriVertex> SourceGet;

        /// <summary>Stub of property QuickGraph.IEdge`1.Target</summary>
        global::QuickGraph.Petri.IPetriVertex global::QuickGraph.IEdge<global::QuickGraph.Petri.IPetriVertex>.Target
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs
                  .StubDelegates.Func<global::QuickGraph.Petri.Stubs.SArc<Token>, 
                  global::QuickGraph.Petri.IPetriVertex> sh = this.TargetGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs
                    .IDefaultStub<global::QuickGraph.Petri.Stubs.SArc<Token>> stub = (
                  (global::Microsoft.Stubs.IStub<global::QuickGraph.Petri.Stubs.SArc<Token>>
                  )this).DefaultStub;
                  return stub.Result<global::QuickGraph.Petri.IPetriVertex>(this);
                }
            }
        }

        /// <summary>Stub of method !0 QuickGraph.IEdge`1.get_Target()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Petri.Stubs.SArc<Token>, global::QuickGraph.Petri.IPetriVertex> TargetGet;

        /// <summary>Stub of property QuickGraph.Petri.IArc`1.Transition</summary>
        global::QuickGraph.Petri.ITransition<Token> global::QuickGraph.Petri.IArc<Token>.Transition
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs
                  .StubDelegates.Func<global::QuickGraph.Petri.Stubs.SArc<Token>, 
                  global::QuickGraph.Petri.ITransition<Token>> sh = this.TransitionGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs
                    .IDefaultStub<global::QuickGraph.Petri.Stubs.SArc<Token>> stub = (
                  (global::Microsoft.Stubs.IStub<global::QuickGraph.Petri.Stubs.SArc<Token>>
                  )this).DefaultStub;
                  return stub.Result<global::QuickGraph.Petri.ITransition<Token>>(this);
                }
            }
        }

        /// <summary>Stub of method QuickGraph.Petri.ITransition`1&lt;!0&gt; QuickGraph.Petri.IArc`1.get_Transition()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Petri.Stubs.SArc<Token>, global::QuickGraph.Petri.ITransition<Token>> TransitionGet;
    }
}
namespace QuickGraph.Stubs
{
    /// <summary>Stub of IBidirectionalGraph`2</summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Stubs", "0.1.2.3")]
    [global::System.Serializable]
    [global::System.Diagnostics.DebuggerStepThrough]
    public partial class SBidirectionalGraph<TVertex,TEdge>
      : global::Microsoft.Stubs.StubBase<global::QuickGraph.Stubs.SBidirectionalGraph<TVertex, TEdge>>
      , global::QuickGraph.IBidirectionalGraph<TVertex, TEdge>
        where TEdge : global::QuickGraph.IEdge<TVertex>
    {
        /// <summary>Initializes a new instance of type SBidirectionalGraph</summary>
        [global::System.Diagnostics.DebuggerHidden]
        public SBidirectionalGraph()
        {
        }

        /// <summary>Stub of property QuickGraph.IGraph`2.AllowParallelEdges</summary>
        bool global::QuickGraph.IGraph<TVertex, TEdge>.AllowParallelEdges
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs
                  .SBidirectionalGraph<TVertex, TEdge>, bool> sh = this.AllowParallelEdgesGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs.IDefaultStub
                      <global::QuickGraph.Stubs.SBidirectionalGraph<TVertex, TEdge>> stub
                     = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                      .SBidirectionalGraph<TVertex, TEdge>>)this).DefaultStub;
                  return stub.Result<bool>(this);
                }
            }
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IGraph`2.get_AllowParallelEdges()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SBidirectionalGraph<TVertex, TEdge>, bool> AllowParallelEdgesGet;

        /// <summary>Stub of property QuickGraph.IEdgeSet`2.EdgeCount</summary>
        int global::QuickGraph.IEdgeSet<TVertex, TEdge>.EdgeCount
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs
                  .SBidirectionalGraph<TVertex, TEdge>, int> sh = this.EdgeCountGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs.IDefaultStub
                      <global::QuickGraph.Stubs.SBidirectionalGraph<TVertex, TEdge>> stub
                     = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                      .SBidirectionalGraph<TVertex, TEdge>>)this).DefaultStub;
                  return stub.Result<int>(this);
                }
            }
        }

        /// <summary>Stub of method System.Int32 QuickGraph.IEdgeSet`2.get_EdgeCount()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SBidirectionalGraph<TVertex, TEdge>, int> EdgeCountGet;

        /// <summary>Stub of property QuickGraph.IEdgeSet`2.Edges</summary>
        global::System.Collections.Generic.IEnumerable<TEdge> global::QuickGraph.IEdgeSet<TVertex, TEdge>.Edges
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs
                  .SBidirectionalGraph<TVertex, TEdge>, 
                global::System.Collections.Generic.IEnumerable<TEdge>> sh = this.EdgesGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs.IDefaultStub
                      <global::QuickGraph.Stubs.SBidirectionalGraph<TVertex, TEdge>> stub
                     = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                      .SBidirectionalGraph<TVertex, TEdge>>)this).DefaultStub;
                  return 
                    stub.Result<global::System.Collections.Generic.IEnumerable<TEdge>>(this);
                }
            }
        }

        /// <summary>Stub of method System.Collections.Generic.IEnumerable`1&lt;!1&gt; QuickGraph.IEdgeSet`2.get_Edges()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SBidirectionalGraph<TVertex, TEdge>, global::System.Collections.Generic.IEnumerable<TEdge>> EdgesGet;

        /// <summary>Stub of property QuickGraph.IGraph`2.IsDirected</summary>
        bool global::QuickGraph.IGraph<TVertex, TEdge>.IsDirected
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs
                  .SBidirectionalGraph<TVertex, TEdge>, bool> sh = this.IsDirectedGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs.IDefaultStub
                      <global::QuickGraph.Stubs.SBidirectionalGraph<TVertex, TEdge>> stub
                     = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                      .SBidirectionalGraph<TVertex, TEdge>>)this).DefaultStub;
                  return stub.Result<bool>(this);
                }
            }
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IGraph`2.get_IsDirected()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SBidirectionalGraph<TVertex, TEdge>, bool> IsDirectedGet;

        /// <summary>Stub of property QuickGraph.IEdgeSet`2.IsEdgesEmpty</summary>
        bool global::QuickGraph.IEdgeSet<TVertex, TEdge>.IsEdgesEmpty
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs
                  .SBidirectionalGraph<TVertex, TEdge>, bool> sh = this.IsEdgesEmptyGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs.IDefaultStub
                      <global::QuickGraph.Stubs.SBidirectionalGraph<TVertex, TEdge>> stub
                     = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                      .SBidirectionalGraph<TVertex, TEdge>>)this).DefaultStub;
                  return stub.Result<bool>(this);
                }
            }
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IEdgeSet`2.get_IsEdgesEmpty()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SBidirectionalGraph<TVertex, TEdge>, bool> IsEdgesEmptyGet;

        /// <summary>Stub of property QuickGraph.IVertexSet`1.IsVerticesEmpty</summary>
        bool global::QuickGraph.IVertexSet<TVertex>.IsVerticesEmpty
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs
                  .SBidirectionalGraph<TVertex, TEdge>, bool> sh = this.IsVerticesEmptyGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs.IDefaultStub
                      <global::QuickGraph.Stubs.SBidirectionalGraph<TVertex, TEdge>> stub
                     = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                      .SBidirectionalGraph<TVertex, TEdge>>)this).DefaultStub;
                  return stub.Result<bool>(this);
                }
            }
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IVertexSet`1.get_IsVerticesEmpty()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SBidirectionalGraph<TVertex, TEdge>, bool> IsVerticesEmptyGet;

        /// <summary>Stub of method System.Int32 QuickGraph.IBidirectionalGraph`2.Degree(!0 v)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        int global::QuickGraph.IBidirectionalGraph<TVertex, TEdge>.Degree(TVertex v)
        {
            global::Microsoft.Stubs.IDefaultStub
                <global::QuickGraph.Stubs.SBidirectionalGraph<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs.IStub<
              global::QuickGraph.Stubs.SBidirectionalGraph<TVertex, TEdge>
              >)this).DefaultStub;
            return stub.Result<int>(this);
        }

        /// <summary>Stub of method System.Int32 QuickGraph.IBidirectionalGraph`2.InDegree(!0 v)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        int global::QuickGraph.IBidirectionalGraph<TVertex, TEdge>.InDegree(TVertex v)
        {
            global::Microsoft.Stubs.IDefaultStub
                <global::QuickGraph.Stubs.SBidirectionalGraph<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs.IStub<
              global::QuickGraph.Stubs.SBidirectionalGraph<TVertex, TEdge>
              >)this).DefaultStub;
            return stub.Result<int>(this);
        }

        /// <summary>Stub of method !1 QuickGraph.IBidirectionalGraph`2.InEdge(!0 v, System.Int32 index)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        TEdge global::QuickGraph.IBidirectionalGraph<TVertex, TEdge>.InEdge(TVertex v, int index)
        {
            global::Microsoft.Stubs.IDefaultStub
                <global::QuickGraph.Stubs.SBidirectionalGraph<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs.IStub<
              global::QuickGraph.Stubs.SBidirectionalGraph<TVertex, TEdge>
              >)this).DefaultStub;
            return stub.Result<TEdge>(this);
        }

        /// <summary>Stub of method System.Collections.Generic.IEnumerable`1&lt;!1&gt; QuickGraph.IBidirectionalGraph`2.InEdges(!0 v)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        global::System.Collections.Generic.IEnumerable<TEdge> global::QuickGraph.IBidirectionalGraph<TVertex, TEdge>.InEdges(TVertex v)
        {
            global::Microsoft.Stubs.IDefaultStub
                <global::QuickGraph.Stubs.SBidirectionalGraph<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs.IStub<
              global::QuickGraph.Stubs.SBidirectionalGraph<TVertex, TEdge>
              >)this).DefaultStub;
            return stub.Result<global::System.Collections.Generic.IEnumerable<TEdge>>(this);
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IBidirectionalGraph`2.IsInEdgesEmpty(!0 v)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IBidirectionalGraph<TVertex, TEdge>.IsInEdgesEmpty(TVertex v)
        {
            global::Microsoft.Stubs.IDefaultStub
                <global::QuickGraph.Stubs.SBidirectionalGraph<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs.IStub<
              global::QuickGraph.Stubs.SBidirectionalGraph<TVertex, TEdge>
              >)this).DefaultStub;
            return stub.Result<bool>(this);
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IBidirectionalGraph`2.TryGetInEdges(!0 v, System.Collections.Generic.IEnumerable`1&lt;!1&gt;&amp; edges)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IBidirectionalGraph<TVertex, TEdge>.TryGetInEdges(TVertex v, out global::System.Collections.Generic.IEnumerable<TEdge> edges)
        {
            global::Microsoft.Stubs.IDefaultStub
                <global::QuickGraph.Stubs.SBidirectionalGraph<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs.IStub<
              global::QuickGraph.Stubs.SBidirectionalGraph<TVertex, TEdge>
              >)this).DefaultStub;
            stub.ValueAtReturn<global::System.Collections.Generic.IEnumerable<TEdge>>
                (this, out edges);
            return stub.Result<bool>(this);
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IEdgeSet`2.ContainsEdge(!1 edge)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IEdgeSet<TVertex, TEdge>.ContainsEdge(TEdge edge)
        {
            global::Microsoft.Stubs.IDefaultStub
                <global::QuickGraph.Stubs.SBidirectionalGraph<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs.IStub<
              global::QuickGraph.Stubs.SBidirectionalGraph<TVertex, TEdge>
              >)this).DefaultStub;
            return stub.Result<bool>(this);
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IImplicitGraph`2.IsOutEdgesEmpty(!0 v)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IImplicitGraph<TVertex, TEdge>.IsOutEdgesEmpty(TVertex v)
        {
            global::Microsoft.Stubs.IDefaultStub
                <global::QuickGraph.Stubs.SBidirectionalGraph<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs.IStub<
              global::QuickGraph.Stubs.SBidirectionalGraph<TVertex, TEdge>
              >)this).DefaultStub;
            return stub.Result<bool>(this);
        }

        /// <summary>Stub of method System.Int32 QuickGraph.IImplicitGraph`2.OutDegree(!0 v)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        int global::QuickGraph.IImplicitGraph<TVertex, TEdge>.OutDegree(TVertex v)
        {
            global::Microsoft.Stubs.IDefaultStub
                <global::QuickGraph.Stubs.SBidirectionalGraph<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs.IStub<
              global::QuickGraph.Stubs.SBidirectionalGraph<TVertex, TEdge>
              >)this).DefaultStub;
            return stub.Result<int>(this);
        }

        /// <summary>Stub of method !1 QuickGraph.IImplicitGraph`2.OutEdge(!0 v, System.Int32 index)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        TEdge global::QuickGraph.IImplicitGraph<TVertex, TEdge>.OutEdge(TVertex v, int index)
        {
            global::Microsoft.Stubs.IDefaultStub
                <global::QuickGraph.Stubs.SBidirectionalGraph<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs.IStub<
              global::QuickGraph.Stubs.SBidirectionalGraph<TVertex, TEdge>
              >)this).DefaultStub;
            return stub.Result<TEdge>(this);
        }

        /// <summary>Stub of method System.Collections.Generic.IEnumerable`1&lt;!1&gt; QuickGraph.IImplicitGraph`2.OutEdges(!0 v)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        global::System.Collections.Generic.IEnumerable<TEdge> global::QuickGraph.IImplicitGraph<TVertex, TEdge>.OutEdges(TVertex v)
        {
            global::Microsoft.Stubs.IDefaultStub
                <global::QuickGraph.Stubs.SBidirectionalGraph<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs.IStub<
              global::QuickGraph.Stubs.SBidirectionalGraph<TVertex, TEdge>
              >)this).DefaultStub;
            return stub.Result<global::System.Collections.Generic.IEnumerable<TEdge>>(this);
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IImplicitGraph`2.TryGetOutEdges(!0 v, System.Collections.Generic.IEnumerable`1&lt;!1&gt;&amp; edges)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IImplicitGraph<TVertex, TEdge>.TryGetOutEdges(TVertex v, out global::System.Collections.Generic.IEnumerable<TEdge> edges)
        {
            global::Microsoft.Stubs.IDefaultStub
                <global::QuickGraph.Stubs.SBidirectionalGraph<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs.IStub<
              global::QuickGraph.Stubs.SBidirectionalGraph<TVertex, TEdge>
              >)this).DefaultStub;
            stub.ValueAtReturn<global::System.Collections.Generic.IEnumerable<TEdge>>
                (this, out edges);
            return stub.Result<bool>(this);
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IIncidenceGraph`2.ContainsEdge(!0 source, !0 target)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IIncidenceGraph<TVertex, TEdge>.ContainsEdge(TVertex source, TVertex target)
        {
            global::Microsoft.Stubs.IDefaultStub
                <global::QuickGraph.Stubs.SBidirectionalGraph<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs.IStub<
              global::QuickGraph.Stubs.SBidirectionalGraph<TVertex, TEdge>
              >)this).DefaultStub;
            return stub.Result<bool>(this);
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IIncidenceGraph`2.TryGetEdge(!0 source, !0 target, !1&amp; edge)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IIncidenceGraph<TVertex, TEdge>.TryGetEdge(
            TVertex source,
            TVertex target,
            out TEdge edge
        )
        {
            global::Microsoft.Stubs.IDefaultStub
                <global::QuickGraph.Stubs.SBidirectionalGraph<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs.IStub<
              global::QuickGraph.Stubs.SBidirectionalGraph<TVertex, TEdge>
              >)this).DefaultStub;
            stub.ValueAtReturn<TEdge>(this, out edge);
            return stub.Result<bool>(this);
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IIncidenceGraph`2.TryGetEdges(!0 source, !0 target, System.Collections.Generic.IEnumerable`1&lt;!1&gt;&amp; edges)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IIncidenceGraph<TVertex, TEdge>.TryGetEdges(
            TVertex source,
            TVertex target,
            out global::System.Collections.Generic.IEnumerable<TEdge> edges
        )
        {
            global::Microsoft.Stubs.IDefaultStub
                <global::QuickGraph.Stubs.SBidirectionalGraph<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs.IStub<
              global::QuickGraph.Stubs.SBidirectionalGraph<TVertex, TEdge>
              >)this).DefaultStub;
            stub.ValueAtReturn<global::System.Collections.Generic.IEnumerable<TEdge>>
                (this, out edges);
            return stub.Result<bool>(this);
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IVertexSet`1.ContainsVertex(!0 vertex)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IVertexSet<TVertex>.ContainsVertex(TVertex vertex)
        {
            global::Microsoft.Stubs.IDefaultStub
                <global::QuickGraph.Stubs.SBidirectionalGraph<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs.IStub<
              global::QuickGraph.Stubs.SBidirectionalGraph<TVertex, TEdge>
              >)this).DefaultStub;
            return stub.Result<bool>(this);
        }

        /// <summary>Stub of property QuickGraph.IVertexSet`1.VertexCount</summary>
        int global::QuickGraph.IVertexSet<TVertex>.VertexCount
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs
                  .SBidirectionalGraph<TVertex, TEdge>, int> sh = this.VertexCountGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs.IDefaultStub
                      <global::QuickGraph.Stubs.SBidirectionalGraph<TVertex, TEdge>> stub
                     = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                      .SBidirectionalGraph<TVertex, TEdge>>)this).DefaultStub;
                  return stub.Result<int>(this);
                }
            }
        }

        /// <summary>Stub of method System.Int32 QuickGraph.IVertexSet`1.get_VertexCount()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SBidirectionalGraph<TVertex, TEdge>, int> VertexCountGet;

        /// <summary>Stub of property QuickGraph.IVertexSet`1.Vertices</summary>
        global::System.Collections.Generic.IEnumerable<TVertex> global::QuickGraph.IVertexSet<TVertex>.Vertices
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs
                  .SBidirectionalGraph<TVertex, TEdge>, 
                global::System.Collections.Generic.IEnumerable<TVertex>> sh = this.VerticesGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs.IDefaultStub
                      <global::QuickGraph.Stubs.SBidirectionalGraph<TVertex, TEdge>> stub
                     = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                      .SBidirectionalGraph<TVertex, TEdge>>)this).DefaultStub;
                  return 
                    stub.Result<global::System.Collections.Generic.IEnumerable<TVertex>>(this);
                }
            }
        }

        /// <summary>Stub of method System.Collections.Generic.IEnumerable`1&lt;!0&gt; QuickGraph.IVertexSet`1.get_Vertices()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SBidirectionalGraph<TVertex, TEdge>, global::System.Collections.Generic.IEnumerable<TVertex>> VerticesGet;
    }
}
namespace QuickGraph.Algorithms.Services.Stubs
{
    /// <summary>Stub of ICancelManager</summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Stubs", "0.1.2.3")]
    [global::System.Serializable]
    [global::System.Diagnostics.DebuggerStepThrough]
    public partial class SCancelManager
      : global::Microsoft.Stubs.StubBase<global::QuickGraph.Algorithms.Services.Stubs.SCancelManager>
      , global::QuickGraph.Algorithms.Services.ICancelManager
    {
        /// <summary>Initializes a new instance of type SCancelManager</summary>
        [global::System.Diagnostics.DebuggerHidden]
        public SCancelManager()
        {
        }

        /// <summary>Stub of method System.Void QuickGraph.Algorithms.Services.ICancelManager.Cancel()</summary>
        public global::Microsoft.Stubs.StubDelegates.Action<global::QuickGraph.Algorithms.Services.Stubs.SCancelManager> Cancel;

        /// <summary>Event CancelRequested</summary>
        public event global::System.EventHandler CancelRequested;

        /// <summary>Event CancelReseted</summary>
        public event global::System.EventHandler CancelReseted;

        /// <summary>Stub of property QuickGraph.Algorithms.Services.ICancelManager.IsCancelling</summary>
        bool global::QuickGraph.Algorithms.Services.ICancelManager.IsCancelling
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates.Func
                    <global::QuickGraph.Algorithms.Services.Stubs.SCancelManager, 
                    bool> sh = this.IsCancellingGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs.IDefaultStub
                      <global::QuickGraph.Algorithms.Services.Stubs.SCancelManager> stub = ((
                  global::Microsoft.Stubs.IStub<global::QuickGraph.Algorithms.Services.Stubs
                    .SCancelManager>)this).DefaultStub;
                  return stub.Result<bool>(this);
                }
            }
        }

        /// <summary>Stub of method System.Boolean QuickGraph.Algorithms.Services.ICancelManager.get_IsCancelling()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Algorithms.Services.Stubs.SCancelManager, bool> IsCancellingGet;

        /// <summary>Stub of method System.Void QuickGraph.Algorithms.Services.ICancelManager.Cancel()</summary>
        [global::System.Diagnostics.DebuggerHidden]
        void global::QuickGraph.Algorithms.Services.ICancelManager.Cancel()
        {
            global::Microsoft.Stubs.StubDelegates.Action
                <global::QuickGraph.Algorithms.Services.Stubs.SCancelManager
                > sh = this.Cancel;
            if ((object)sh != (object)null)
              sh.Invoke(this);
            else 
            {
              global::Microsoft.Stubs.IDefaultStub
                  <global::QuickGraph.Algorithms.Services.Stubs.SCancelManager> stub = ((
              global::Microsoft.Stubs.IStub<global::QuickGraph.Algorithms.Services.Stubs
                .SCancelManager>)this).DefaultStub;
              stub.VoidResult(this);
            }
        }

        /// <summary>Stub of method System.Void QuickGraph.Algorithms.Services.ICancelManager.ResetCancel()</summary>
        [global::System.Diagnostics.DebuggerHidden]
        void global::QuickGraph.Algorithms.Services.ICancelManager.ResetCancel()
        {
            global::Microsoft.Stubs.StubDelegates.Action
                <global::QuickGraph.Algorithms.Services.Stubs.SCancelManager
                > sh = this.ResetCancel;
            if ((object)sh != (object)null)
              sh.Invoke(this);
            else 
            {
              global::Microsoft.Stubs.IDefaultStub
                  <global::QuickGraph.Algorithms.Services.Stubs.SCancelManager> stub = ((
              global::Microsoft.Stubs.IStub<global::QuickGraph.Algorithms.Services.Stubs
                .SCancelManager>)this).DefaultStub;
              stub.VoidResult(this);
            }
        }

        /// <summary>Stub of method System.Void QuickGraph.Algorithms.Services.ICancelManager.ResetCancel()</summary>
        public global::Microsoft.Stubs.StubDelegates.Action<global::QuickGraph.Algorithms.Services.Stubs.SCancelManager> ResetCancel;
    }
}
namespace QuickGraph.Stubs
{
    /// <summary>Stub of ICloneableEdge`1</summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Stubs", "0.1.2.3")]
    [global::System.Serializable]
    [global::System.Diagnostics.DebuggerStepThrough]
    public partial class SCloneableEdge<TVertex>
      : global::Microsoft.Stubs.StubBase<global::QuickGraph.Stubs.SCloneableEdge<TVertex>>
      , global::QuickGraph.ICloneableEdge<TVertex>
    {
        /// <summary>Initializes a new instance of type SCloneableEdge</summary>
        [global::System.Diagnostics.DebuggerHidden]
        public SCloneableEdge()
        {
        }

        /// <summary>Stub of method QuickGraph.ICloneableEdge`1&lt;!0&gt; QuickGraph.ICloneableEdge`1.Clone(!0 source, !0 target)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        global::QuickGraph.ICloneableEdge<TVertex> global::QuickGraph.ICloneableEdge<TVertex>.Clone(TVertex source, TVertex target)
        {
            global::Microsoft.Stubs
              .IDefaultStub<global::QuickGraph.Stubs.SCloneableEdge<TVertex>> stub
               = ((global::Microsoft.Stubs
                .IStub<global::QuickGraph.Stubs.SCloneableEdge<TVertex>>)this).DefaultStub;
            return stub.Result<global::QuickGraph.ICloneableEdge<TVertex>>(this);
        }

        /// <summary>Stub of property QuickGraph.IEdge`1.Source</summary>
        TVertex global::QuickGraph.IEdge<TVertex>.Source
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates
                  .Func<global::QuickGraph.Stubs.SCloneableEdge<TVertex>, TVertex> sh
                   = this.SourceGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs
                    .IDefaultStub<global::QuickGraph.Stubs.SCloneableEdge<TVertex>> stub
                     = ((global::Microsoft.Stubs
                      .IStub<global::QuickGraph.Stubs.SCloneableEdge<TVertex>>)this).DefaultStub;
                  return stub.Result<TVertex>(this);
                }
            }
        }

        /// <summary>Stub of method !0 QuickGraph.IEdge`1.get_Source()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SCloneableEdge<TVertex>, TVertex> SourceGet;

        /// <summary>Stub of property QuickGraph.IEdge`1.Target</summary>
        TVertex global::QuickGraph.IEdge<TVertex>.Target
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates
                  .Func<global::QuickGraph.Stubs.SCloneableEdge<TVertex>, TVertex> sh
                   = this.TargetGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs
                    .IDefaultStub<global::QuickGraph.Stubs.SCloneableEdge<TVertex>> stub
                     = ((global::Microsoft.Stubs
                      .IStub<global::QuickGraph.Stubs.SCloneableEdge<TVertex>>)this).DefaultStub;
                  return stub.Result<TVertex>(this);
                }
            }
        }

        /// <summary>Stub of method !0 QuickGraph.IEdge`1.get_Target()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SCloneableEdge<TVertex>, TVertex> TargetGet;
    }
}
namespace QuickGraph.Algorithms.Stubs
{
    /// <summary>Stub of IComputation</summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Stubs", "0.1.2.3")]
    [global::System.Serializable]
    [global::System.Diagnostics.DebuggerStepThrough]
    public partial class SComputation
      : global::Microsoft.Stubs.StubBase<global::QuickGraph.Algorithms.Stubs.SComputation>
      , global::QuickGraph.Algorithms.IComputation
    {
        /// <summary>Initializes a new instance of type SComputation</summary>
        [global::System.Diagnostics.DebuggerHidden]
        public SComputation()
        {
        }

        /// <summary>Stub of method System.Void QuickGraph.Algorithms.IComputation.Abort()</summary>
        public global::Microsoft.Stubs.StubDelegates.Action<global::QuickGraph.Algorithms.Stubs.SComputation> Abort;

        /// <summary>Event Aborted</summary>
        public event global::System.EventHandler Aborted;

        /// <summary>Stub of method System.Void QuickGraph.Algorithms.IComputation.Compute()</summary>
        public global::Microsoft.Stubs.StubDelegates.Action<global::QuickGraph.Algorithms.Stubs.SComputation> Compute;

        /// <summary>Event Finished</summary>
        public event global::System.EventHandler Finished;

        /// <summary>Stub of method System.Void QuickGraph.Algorithms.IComputation.Abort()</summary>
        [global::System.Diagnostics.DebuggerHidden]
        void global::QuickGraph.Algorithms.IComputation.Abort()
        {
            global::Microsoft.Stubs.StubDelegates
              .Action<global::QuickGraph.Algorithms.Stubs.SComputation> sh = this.Abort;
            if ((object)sh != (object)null)
              sh.Invoke(this);
            else 
            {
              global::Microsoft.Stubs
                .IDefaultStub<global::QuickGraph.Algorithms.Stubs.SComputation> stub
                 = ((global::Microsoft.Stubs
                  .IStub<global::QuickGraph.Algorithms.Stubs.SComputation>)this).DefaultStub;
              stub.VoidResult(this);
            }
        }

        /// <summary>Stub of method System.Void QuickGraph.Algorithms.IComputation.Compute()</summary>
        [global::System.Diagnostics.DebuggerHidden]
        void global::QuickGraph.Algorithms.IComputation.Compute()
        {
            global::Microsoft.Stubs.StubDelegates
              .Action<global::QuickGraph.Algorithms.Stubs.SComputation> sh = this.Compute;
            if ((object)sh != (object)null)
              sh.Invoke(this);
            else 
            {
              global::Microsoft.Stubs
                .IDefaultStub<global::QuickGraph.Algorithms.Stubs.SComputation> stub
                 = ((global::Microsoft.Stubs
                  .IStub<global::QuickGraph.Algorithms.Stubs.SComputation>)this).DefaultStub;
              stub.VoidResult(this);
            }
        }

        /// <summary>Event Started</summary>
        public event global::System.EventHandler Started;

        /// <summary>Stub of property QuickGraph.Algorithms.IComputation.State</summary>
        global::QuickGraph.Algorithms.ComputationState global::QuickGraph.Algorithms.IComputation.State
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates
                  .Func<global::QuickGraph.Algorithms.Stubs.SComputation, 
                  global::QuickGraph.Algorithms.ComputationState> sh = this.StateGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs
                    .IDefaultStub<global::QuickGraph.Algorithms.Stubs.SComputation> stub
                     = ((global::Microsoft.Stubs
                      .IStub<global::QuickGraph.Algorithms.Stubs.SComputation>)this).DefaultStub;
                  return stub.Result<global::QuickGraph.Algorithms.ComputationState>(this);
                }
            }
        }

        /// <summary>Event StateChanged</summary>
        public event global::System.EventHandler StateChanged;

        /// <summary>Stub of method QuickGraph.Algorithms.ComputationState QuickGraph.Algorithms.IComputation.get_State()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Algorithms.Stubs.SComputation, global::QuickGraph.Algorithms.ComputationState> StateGet;

        /// <summary>Stub of property QuickGraph.Algorithms.IComputation.SyncRoot</summary>
        object global::QuickGraph.Algorithms.IComputation.SyncRoot
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates
                  .Func<global::QuickGraph.Algorithms.Stubs.SComputation, object> sh
                   = this.SyncRootGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs
                    .IDefaultStub<global::QuickGraph.Algorithms.Stubs.SComputation> stub
                     = ((global::Microsoft.Stubs
                      .IStub<global::QuickGraph.Algorithms.Stubs.SComputation>)this).DefaultStub;
                  return stub.Result<object>(this);
                }
            }
        }

        /// <summary>Stub of method System.Object QuickGraph.Algorithms.IComputation.get_SyncRoot()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Algorithms.Stubs.SComputation, object> SyncRootGet;
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
    /// <summary>Stub of IConnectedComponentAlgorithm`3</summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Stubs", "0.1.2.3")]
    [global::System.Serializable]
    [global::System.Diagnostics.DebuggerStepThrough]
    public partial class SConnectedComponentAlgorithm<TVertex,TEdge,TGraph>
      : global::Microsoft.Stubs.StubBase<global::QuickGraph.Algorithms.Stubs.SConnectedComponentAlgorithm<TVertex, TEdge, TGraph>>
      , global::QuickGraph.Algorithms.IConnectedComponentAlgorithm<TVertex, TEdge, TGraph>
        where TEdge : global::QuickGraph.IEdge<TVertex>
        where TGraph : global::QuickGraph.IGraph<TVertex, TEdge>
    {
        /// <summary>Initializes a new instance of type SConnectedComponentAlgorithm</summary>
        [global::System.Diagnostics.DebuggerHidden]
        public SConnectedComponentAlgorithm()
        {
        }

        /// <summary>Stub of method System.Void QuickGraph.Algorithms.IComputation.Abort()</summary>
        public global::Microsoft.Stubs.StubDelegates.Action<global::QuickGraph.Algorithms.Stubs.SConnectedComponentAlgorithm<TVertex, TEdge, TGraph>> Abort;

        /// <summary>Event Aborted</summary>
        public event global::System.EventHandler Aborted;

        /// <summary>Stub of property QuickGraph.Algorithms.IConnectedComponentAlgorithm`3.ComponentCount</summary>
        int global::QuickGraph.Algorithms.IConnectedComponentAlgorithm<TVertex, TEdge, TGraph>.ComponentCount
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Algorithms.Stubs
                  .SConnectedComponentAlgorithm
                      <TVertex, TEdge, TGraph>, int> sh = this.ComponentCountGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Algorithms.Stubs
                    .SConnectedComponentAlgorithm
                        <TVertex, TEdge, TGraph>> stub
                     = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Algorithms.Stubs
                      .SConnectedComponentAlgorithm
                          <TVertex, TEdge, TGraph>>)this).DefaultStub;
                  return stub.Result<int>(this);
                }
            }
        }

        /// <summary>Stub of method System.Int32 QuickGraph.Algorithms.IConnectedComponentAlgorithm`3.get_ComponentCount()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Algorithms.Stubs.SConnectedComponentAlgorithm<TVertex, TEdge, TGraph>, int> ComponentCountGet;

        /// <summary>Stub of property QuickGraph.Algorithms.IConnectedComponentAlgorithm`3.Components</summary>
        global::System.Collections.Generic.IDictionary<TVertex, int> global::QuickGraph.Algorithms.IConnectedComponentAlgorithm<TVertex, TEdge, TGraph>.Components
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Algorithms.Stubs
                  .SConnectedComponentAlgorithm
                      <TVertex, TEdge, TGraph>, 
                global::System.Collections.Generic
                  .IDictionary<TVertex, int>> sh = this.ComponentsGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Algorithms.Stubs
                    .SConnectedComponentAlgorithm
                        <TVertex, TEdge, TGraph>> stub
                     = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Algorithms.Stubs
                      .SConnectedComponentAlgorithm
                          <TVertex, TEdge, TGraph>>)this).DefaultStub;
                  return 
                    stub.Result<global::System.Collections.Generic.IDictionary<TVertex, int>>
                        (this);
                }
            }
        }

        /// <summary>Stub of method System.Collections.Generic.IDictionary`2&lt;!0,System.Int32&gt; QuickGraph.Algorithms.IConnectedComponentAlgorithm`3.get_Components()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Algorithms.Stubs.SConnectedComponentAlgorithm<TVertex, TEdge, TGraph>, global::System.Collections.Generic.IDictionary<TVertex, int>> ComponentsGet;

        /// <summary>Stub of method System.Void QuickGraph.Algorithms.IComputation.Compute()</summary>
        public global::Microsoft.Stubs.StubDelegates.Action<global::QuickGraph.Algorithms.Stubs.SConnectedComponentAlgorithm<TVertex, TEdge, TGraph>> Compute;

        /// <summary>Event Finished</summary>
        public event global::System.EventHandler Finished;

        /// <summary>Stub of method System.Void QuickGraph.Algorithms.IComputation.Abort()</summary>
        [global::System.Diagnostics.DebuggerHidden]
        void global::QuickGraph.Algorithms.IComputation.Abort()
        {
            global::Microsoft.Stubs.StubDelegates.Action<global::QuickGraph.Algorithms.Stubs
              .SConnectedComponentAlgorithm
                  <TVertex, TEdge, TGraph>> sh = this.Abort;
            if ((object)sh != (object)null)
              sh.Invoke(this);
            else 
            {
              global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Algorithms.Stubs
                .SConnectedComponentAlgorithm
                    <TVertex, TEdge, TGraph>> stub
                 = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Algorithms.Stubs
                  .SConnectedComponentAlgorithm
                      <TVertex, TEdge, TGraph>>)this).DefaultStub;
              stub.VoidResult(this);
            }
        }

        /// <summary>Stub of method System.Void QuickGraph.Algorithms.IComputation.Compute()</summary>
        [global::System.Diagnostics.DebuggerHidden]
        void global::QuickGraph.Algorithms.IComputation.Compute()
        {
            global::Microsoft.Stubs.StubDelegates.Action<global::QuickGraph.Algorithms.Stubs
              .SConnectedComponentAlgorithm
                  <TVertex, TEdge, TGraph>> sh = this.Compute;
            if ((object)sh != (object)null)
              sh.Invoke(this);
            else 
            {
              global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Algorithms.Stubs
                .SConnectedComponentAlgorithm
                    <TVertex, TEdge, TGraph>> stub
                 = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Algorithms.Stubs
                  .SConnectedComponentAlgorithm
                      <TVertex, TEdge, TGraph>>)this).DefaultStub;
              stub.VoidResult(this);
            }
        }

        /// <summary>Event Started</summary>
        public event global::System.EventHandler Started;

        /// <summary>Stub of property QuickGraph.Algorithms.IComputation.State</summary>
        global::QuickGraph.Algorithms.ComputationState global::QuickGraph.Algorithms.IComputation.State
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Algorithms.Stubs
                  .SConnectedComponentAlgorithm
                      <TVertex, TEdge, TGraph>, 
                global::QuickGraph.Algorithms.ComputationState> sh = this.StateGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Algorithms.Stubs
                    .SConnectedComponentAlgorithm
                        <TVertex, TEdge, TGraph>> stub
                     = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Algorithms.Stubs
                      .SConnectedComponentAlgorithm
                          <TVertex, TEdge, TGraph>>)this).DefaultStub;
                  return stub.Result<global::QuickGraph.Algorithms.ComputationState>(this);
                }
            }
        }

        /// <summary>Event StateChanged</summary>
        public event global::System.EventHandler StateChanged;

        /// <summary>Stub of method QuickGraph.Algorithms.ComputationState QuickGraph.Algorithms.IComputation.get_State()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Algorithms.Stubs.SConnectedComponentAlgorithm<TVertex, TEdge, TGraph>, global::QuickGraph.Algorithms.ComputationState> StateGet;

        /// <summary>Stub of property QuickGraph.Algorithms.IComputation.SyncRoot</summary>
        object global::QuickGraph.Algorithms.IComputation.SyncRoot
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Algorithms.Stubs
                  .SConnectedComponentAlgorithm
                      <TVertex, TEdge, TGraph>, object> sh = this.SyncRootGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Algorithms.Stubs
                    .SConnectedComponentAlgorithm
                        <TVertex, TEdge, TGraph>> stub
                     = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Algorithms.Stubs
                      .SConnectedComponentAlgorithm
                          <TVertex, TEdge, TGraph>>)this).DefaultStub;
                  return stub.Result<object>(this);
                }
            }
        }

        /// <summary>Stub of method System.Object QuickGraph.Algorithms.IComputation.get_SyncRoot()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Algorithms.Stubs.SConnectedComponentAlgorithm<TVertex, TEdge, TGraph>, object> SyncRootGet;

        /// <summary>Stub of property QuickGraph.Algorithms.IAlgorithm`1.VisitedGraph</summary>
        TGraph global::QuickGraph.Algorithms.IAlgorithm<TGraph>.VisitedGraph
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Algorithms.Stubs
                  .SConnectedComponentAlgorithm
                      <TVertex, TEdge, TGraph>, TGraph> sh = this.VisitedGraphGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Algorithms.Stubs
                    .SConnectedComponentAlgorithm
                        <TVertex, TEdge, TGraph>> stub
                     = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Algorithms.Stubs
                      .SConnectedComponentAlgorithm
                          <TVertex, TEdge, TGraph>>)this).DefaultStub;
                  return stub.Result<TGraph>(this);
                }
            }
        }

        /// <summary>Stub of method !0 QuickGraph.Algorithms.IAlgorithm`1.get_VisitedGraph()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Algorithms.Stubs.SConnectedComponentAlgorithm<TVertex, TEdge, TGraph>, TGraph> VisitedGraphGet;
    }
}
namespace QuickGraph.Collections.Stubs
{
    /// <summary>Stub of IDisjointSet`1</summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Stubs", "0.1.2.3")]
    [global::System.Serializable]
    [global::System.Diagnostics.DebuggerStepThrough]
    public partial class SDisjointSet<T>
      : global::Microsoft.Stubs.StubBase<global::QuickGraph.Collections.Stubs.SDisjointSet<T>>
      , global::QuickGraph.Collections.IDisjointSet<T>
    {
        /// <summary>Initializes a new instance of type SDisjointSet</summary>
        [global::System.Diagnostics.DebuggerHidden]
        public SDisjointSet()
        {
        }

        /// <summary>Stub of property QuickGraph.Collections.IDisjointSet`1.ElementCount</summary>
        int global::QuickGraph.Collections.IDisjointSet<T>.ElementCount
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates
                  .Func<global::QuickGraph.Collections.Stubs.SDisjointSet<T>, int> sh
                   = this.ElementCountGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs
                    .IDefaultStub<global::QuickGraph.Collections.Stubs.SDisjointSet<T>> stub
                     = ((global::Microsoft.Stubs
                      .IStub<global::QuickGraph.Collections.Stubs.SDisjointSet<T>>)this)
                      .DefaultStub;
                  return stub.Result<int>(this);
                }
            }
        }

        /// <summary>Stub of method System.Int32 QuickGraph.Collections.IDisjointSet`1.get_ElementCount()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Collections.Stubs.SDisjointSet<T>, int> ElementCountGet;

        /// <summary>Stub of method System.Boolean QuickGraph.Collections.IDisjointSet`1.AreInSameSet(!0 left, !0 right)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.Collections.IDisjointSet<T>.AreInSameSet(T left, T right)
        {
            global::Microsoft.Stubs
              .IDefaultStub<global::QuickGraph.Collections.Stubs.SDisjointSet<T>> stub
               = ((global::Microsoft.Stubs
                .IStub<global::QuickGraph.Collections.Stubs.SDisjointSet<T>>)this)
                .DefaultStub;
            return stub.Result<bool>(this);
        }

        /// <summary>Stub of method System.Boolean QuickGraph.Collections.IDisjointSet`1.Contains(!0 value)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.Collections.IDisjointSet<T>.Contains(T value)
        {
            global::Microsoft.Stubs
              .IDefaultStub<global::QuickGraph.Collections.Stubs.SDisjointSet<T>> stub
               = ((global::Microsoft.Stubs
                .IStub<global::QuickGraph.Collections.Stubs.SDisjointSet<T>>)this)
                .DefaultStub;
            return stub.Result<bool>(this);
        }

        /// <summary>Stub of method !0 QuickGraph.Collections.IDisjointSet`1.FindSet(!0 value)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        T global::QuickGraph.Collections.IDisjointSet<T>.FindSet(T value)
        {
            global::Microsoft.Stubs
              .IDefaultStub<global::QuickGraph.Collections.Stubs.SDisjointSet<T>> stub
               = ((global::Microsoft.Stubs
                .IStub<global::QuickGraph.Collections.Stubs.SDisjointSet<T>>)this)
                .DefaultStub;
            return stub.Result<T>(this);
        }

        /// <summary>Stub of method System.Void QuickGraph.Collections.IDisjointSet`1.MakeSet(!0 value)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        void global::QuickGraph.Collections.IDisjointSet<T>.MakeSet(T value)
        {
            global::Microsoft.Stubs
              .IDefaultStub<global::QuickGraph.Collections.Stubs.SDisjointSet<T>> stub
               = ((global::Microsoft.Stubs
                .IStub<global::QuickGraph.Collections.Stubs.SDisjointSet<T>>)this)
                .DefaultStub;
            stub.VoidResult(this);
        }

        /// <summary>Stub of method System.Boolean QuickGraph.Collections.IDisjointSet`1.Union(!0 left, !0 right)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.Collections.IDisjointSet<T>.Union(T left, T right)
        {
            global::Microsoft.Stubs
              .IDefaultStub<global::QuickGraph.Collections.Stubs.SDisjointSet<T>> stub
               = ((global::Microsoft.Stubs
                .IStub<global::QuickGraph.Collections.Stubs.SDisjointSet<T>>)this)
                .DefaultStub;
            return stub.Result<bool>(this);
        }

        /// <summary>Stub of property QuickGraph.Collections.IDisjointSet`1.SetCount</summary>
        int global::QuickGraph.Collections.IDisjointSet<T>.SetCount
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates
                  .Func<global::QuickGraph.Collections.Stubs.SDisjointSet<T>, int> sh
                   = this.SetCountGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs
                    .IDefaultStub<global::QuickGraph.Collections.Stubs.SDisjointSet<T>> stub
                     = ((global::Microsoft.Stubs
                      .IStub<global::QuickGraph.Collections.Stubs.SDisjointSet<T>>)this)
                      .DefaultStub;
                  return stub.Result<int>(this);
                }
            }
        }

        /// <summary>Stub of method System.Int32 QuickGraph.Collections.IDisjointSet`1.get_SetCount()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Collections.Stubs.SDisjointSet<T>, int> SetCountGet;
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
namespace QuickGraph.Algorithms.ShortestPath.Stubs
{
    /// <summary>Stub of IDistanceRelaxer</summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Stubs", "0.1.2.3")]
    [global::System.Serializable]
    [global::System.Diagnostics.DebuggerStepThrough]
    public partial class SDistanceRelaxer
      : global::Microsoft.Stubs.StubBase<global::QuickGraph.Algorithms.ShortestPath.Stubs.SDistanceRelaxer>
      , global::QuickGraph.Algorithms.ShortestPath.IDistanceRelaxer
    {
        /// <summary>Initializes a new instance of type SDistanceRelaxer</summary>
        [global::System.Diagnostics.DebuggerHidden]
        public SDistanceRelaxer()
        {
        }

        /// <summary>Stub of method System.Double QuickGraph.Algorithms.ShortestPath.IDistanceRelaxer.Combine(System.Double distance, System.Double weight)</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Algorithms.ShortestPath.Stubs.SDistanceRelaxer, double, double, double> Combine;

        /// <summary>Stub of method System.Boolean QuickGraph.Algorithms.ShortestPath.IDistanceRelaxer.Compare(System.Double a, System.Double b)</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Algorithms.ShortestPath.Stubs.SDistanceRelaxer, double, double, bool> Compare;

        /// <summary>Stub of property QuickGraph.Algorithms.ShortestPath.IDistanceRelaxer.InitialDistance</summary>
        double global::QuickGraph.Algorithms.ShortestPath.IDistanceRelaxer.InitialDistance
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs
                  .StubDelegates.Func<global::QuickGraph.Algorithms.ShortestPath.Stubs
                    .SDistanceRelaxer, double> sh = this.InitialDistanceGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs
                    .IDefaultStub<global::QuickGraph.Algorithms.ShortestPath.Stubs
                      .SDistanceRelaxer> stub = ((global::Microsoft.Stubs
                    .IStub<global::QuickGraph.Algorithms.ShortestPath.Stubs
                      .SDistanceRelaxer>)this).DefaultStub;
                  return stub.Result<double>(this);
                }
            }
        }

        /// <summary>Stub of method System.Double QuickGraph.Algorithms.ShortestPath.IDistanceRelaxer.get_InitialDistance()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Algorithms.ShortestPath.Stubs.SDistanceRelaxer, double> InitialDistanceGet;

        /// <summary>Stub of method System.Double QuickGraph.Algorithms.ShortestPath.IDistanceRelaxer.Combine(System.Double distance, System.Double weight)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        double global::QuickGraph.Algorithms.ShortestPath.IDistanceRelaxer.Combine(double distance, double weight)
        {
            global::Microsoft.Stubs
              .StubDelegates.Func<global::QuickGraph.Algorithms.ShortestPath.Stubs
                .SDistanceRelaxer, double, double, double> sh = this.Combine;
            if ((object)sh != (object)null)
              return sh.Invoke(this, distance, weight);
            else 
            {
              global::Microsoft.Stubs
                .IDefaultStub<global::QuickGraph.Algorithms.ShortestPath.Stubs
                  .SDistanceRelaxer> stub = ((global::Microsoft.Stubs
                .IStub<global::QuickGraph.Algorithms.ShortestPath.Stubs
                  .SDistanceRelaxer>)this).DefaultStub;
              return stub.Result<double>(this);
            }
        }

        /// <summary>Stub of method System.Boolean QuickGraph.Algorithms.ShortestPath.IDistanceRelaxer.Compare(System.Double a, System.Double b)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.Algorithms.ShortestPath.IDistanceRelaxer.Compare(double a, double b)
        {
            global::Microsoft.Stubs
              .StubDelegates.Func<global::QuickGraph.Algorithms.ShortestPath.Stubs
                .SDistanceRelaxer, double, double, bool> sh = this.Compare;
            if ((object)sh != (object)null)
              return sh.Invoke(this, a, b);
            else 
            {
              global::Microsoft.Stubs
                .IDefaultStub<global::QuickGraph.Algorithms.ShortestPath.Stubs
                  .SDistanceRelaxer> stub = ((global::Microsoft.Stubs
                .IStub<global::QuickGraph.Algorithms.ShortestPath.Stubs
                  .SDistanceRelaxer>)this).DefaultStub;
              return stub.Result<bool>(this);
            }
        }
    }
}
namespace QuickGraph.Stubs
{
    /// <summary>Stub of IEdge`1</summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Stubs", "0.1.2.3")]
    [global::System.Serializable]
    [global::System.Diagnostics.DebuggerStepThrough]
    public partial class SEdge<TVertex>
      : global::Microsoft.Stubs.StubBase<global::QuickGraph.Stubs.SEdge<TVertex>>
      , global::QuickGraph.IEdge<TVertex>
    {
        /// <summary>Initializes a new instance of type SEdge</summary>
        [global::System.Diagnostics.DebuggerHidden]
        public SEdge()
        {
        }

        /// <summary>Stub of property QuickGraph.IEdge`1.Source</summary>
        TVertex global::QuickGraph.IEdge<TVertex>.Source
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs
                  .StubDelegates.Func<global::QuickGraph.Stubs.SEdge<TVertex>, TVertex> sh
                   = this.SourceGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs.SEdge<TVertex>> 
                    stub
                     = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs.SEdge<TVertex>>)
                      this).DefaultStub;
                  return stub.Result<TVertex>(this);
                }
            }
        }

        /// <summary>Stub of method !0 QuickGraph.IEdge`1.get_Source()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SEdge<TVertex>, TVertex> SourceGet;

        /// <summary>Stub of property QuickGraph.IEdge`1.Target</summary>
        TVertex global::QuickGraph.IEdge<TVertex>.Target
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs
                  .StubDelegates.Func<global::QuickGraph.Stubs.SEdge<TVertex>, TVertex> sh
                   = this.TargetGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs.SEdge<TVertex>> 
                    stub
                     = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs.SEdge<TVertex>>)
                      this).DefaultStub;
                  return stub.Result<TVertex>(this);
                }
            }
        }

        /// <summary>Stub of method !0 QuickGraph.IEdge`1.get_Target()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SEdge<TVertex>, TVertex> TargetGet;
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
namespace QuickGraph.Algorithms.Stubs
{
    /// <summary>Stub of IEdgeColorizerAlgorithm`2</summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Stubs", "0.1.2.3")]
    [global::System.Serializable]
    [global::System.Diagnostics.DebuggerStepThrough]
    public partial class SEdgeColorizerAlgorithm<TVertex,TEdge>
      : global::Microsoft.Stubs.StubBase<global::QuickGraph.Algorithms.Stubs.SEdgeColorizerAlgorithm<TVertex, TEdge>>
      , global::QuickGraph.Algorithms.IEdgeColorizerAlgorithm<TVertex, TEdge>
        where TEdge : global::QuickGraph.IEdge<TVertex>
    {
        /// <summary>Initializes a new instance of type SEdgeColorizerAlgorithm</summary>
        [global::System.Diagnostics.DebuggerHidden]
        public SEdgeColorizerAlgorithm()
        {
        }

        /// <summary>Stub of property QuickGraph.Algorithms.IEdgeColorizerAlgorithm`2.EdgeColors</summary>
        global::System.Collections.Generic.IDictionary<TEdge, global::QuickGraph.GraphColor> global::QuickGraph.Algorithms.IEdgeColorizerAlgorithm<TVertex, TEdge>.EdgeColors
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Algorithms.Stubs
                  .SEdgeColorizerAlgorithm<TVertex, TEdge>, 
                global::System.Collections.Generic.IDictionary
                    <TEdge, global::QuickGraph.GraphColor>> sh = this.EdgeColorsGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Algorithms.Stubs
                    .SEdgeColorizerAlgorithm<TVertex, TEdge>> stub
                     = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Algorithms.Stubs
                      .SEdgeColorizerAlgorithm<TVertex, TEdge>>)this).DefaultStub;
                  return stub.Result<global::System.Collections.Generic
                    .IDictionary<TEdge, global::QuickGraph.GraphColor>>(this);
                }
            }
        }

        /// <summary>Stub of method System.Collections.Generic.IDictionary`2&lt;!1,QuickGraph.GraphColor&gt; QuickGraph.Algorithms.IEdgeColorizerAlgorithm`2.get_EdgeColors()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Algorithms.Stubs.SEdgeColorizerAlgorithm<TVertex, TEdge>, global::System.Collections.Generic.IDictionary<TEdge, global::QuickGraph.GraphColor>> EdgeColorsGet;
    }
}
namespace QuickGraph.Stubs
{
    /// <summary>Stub of IEdgeListAndIncidenceGraph`2</summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Stubs", "0.1.2.3")]
    [global::System.Serializable]
    [global::System.Diagnostics.DebuggerStepThrough]
    public partial class SEdgeListAndIncidenceGraph<TVertex,TEdge>
      : global::Microsoft.Stubs.StubBase<global::QuickGraph.Stubs.SEdgeListAndIncidenceGraph<TVertex, TEdge>>
      , global::QuickGraph.IEdgeListAndIncidenceGraph<TVertex, TEdge>
        where TEdge : global::QuickGraph.IEdge<TVertex>
    {
        /// <summary>Initializes a new instance of type SEdgeListAndIncidenceGraph</summary>
        [global::System.Diagnostics.DebuggerHidden]
        public SEdgeListAndIncidenceGraph()
        {
        }

        /// <summary>Stub of property QuickGraph.IGraph`2.AllowParallelEdges</summary>
        bool global::QuickGraph.IGraph<TVertex, TEdge>.AllowParallelEdges
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs
                  .SEdgeListAndIncidenceGraph<TVertex, TEdge>, bool> sh
                   = this.AllowParallelEdgesGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
                    .SEdgeListAndIncidenceGraph<TVertex, TEdge>> stub
                     = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                      .SEdgeListAndIncidenceGraph<TVertex, TEdge>>)this).DefaultStub;
                  return stub.Result<bool>(this);
                }
            }
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IGraph`2.get_AllowParallelEdges()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SEdgeListAndIncidenceGraph<TVertex, TEdge>, bool> AllowParallelEdgesGet;

        /// <summary>Stub of property QuickGraph.IEdgeSet`2.EdgeCount</summary>
        int global::QuickGraph.IEdgeSet<TVertex, TEdge>.EdgeCount
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs
                  .SEdgeListAndIncidenceGraph<TVertex, TEdge>, int> sh = this.EdgeCountGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
                    .SEdgeListAndIncidenceGraph<TVertex, TEdge>> stub
                     = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                      .SEdgeListAndIncidenceGraph<TVertex, TEdge>>)this).DefaultStub;
                  return stub.Result<int>(this);
                }
            }
        }

        /// <summary>Stub of method System.Int32 QuickGraph.IEdgeSet`2.get_EdgeCount()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SEdgeListAndIncidenceGraph<TVertex, TEdge>, int> EdgeCountGet;

        /// <summary>Stub of property QuickGraph.IEdgeSet`2.Edges</summary>
        global::System.Collections.Generic.IEnumerable<TEdge> global::QuickGraph.IEdgeSet<TVertex, TEdge>.Edges
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs
                  .SEdgeListAndIncidenceGraph<TVertex, TEdge>, 
                global::System.Collections.Generic.IEnumerable<TEdge>> sh = this.EdgesGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
                    .SEdgeListAndIncidenceGraph<TVertex, TEdge>> stub
                     = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                      .SEdgeListAndIncidenceGraph<TVertex, TEdge>>)this).DefaultStub;
                  return 
                    stub.Result<global::System.Collections.Generic.IEnumerable<TEdge>>(this);
                }
            }
        }

        /// <summary>Stub of method System.Collections.Generic.IEnumerable`1&lt;!1&gt; QuickGraph.IEdgeSet`2.get_Edges()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SEdgeListAndIncidenceGraph<TVertex, TEdge>, global::System.Collections.Generic.IEnumerable<TEdge>> EdgesGet;

        /// <summary>Stub of property QuickGraph.IGraph`2.IsDirected</summary>
        bool global::QuickGraph.IGraph<TVertex, TEdge>.IsDirected
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs
                  .SEdgeListAndIncidenceGraph<TVertex, TEdge>, bool> sh = this.IsDirectedGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
                    .SEdgeListAndIncidenceGraph<TVertex, TEdge>> stub
                     = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                      .SEdgeListAndIncidenceGraph<TVertex, TEdge>>)this).DefaultStub;
                  return stub.Result<bool>(this);
                }
            }
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IGraph`2.get_IsDirected()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SEdgeListAndIncidenceGraph<TVertex, TEdge>, bool> IsDirectedGet;

        /// <summary>Stub of property QuickGraph.IEdgeSet`2.IsEdgesEmpty</summary>
        bool global::QuickGraph.IEdgeSet<TVertex, TEdge>.IsEdgesEmpty
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs
                  .SEdgeListAndIncidenceGraph<TVertex, TEdge>, bool> sh = this.IsEdgesEmptyGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
                    .SEdgeListAndIncidenceGraph<TVertex, TEdge>> stub
                     = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                      .SEdgeListAndIncidenceGraph<TVertex, TEdge>>)this).DefaultStub;
                  return stub.Result<bool>(this);
                }
            }
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IEdgeSet`2.get_IsEdgesEmpty()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SEdgeListAndIncidenceGraph<TVertex, TEdge>, bool> IsEdgesEmptyGet;

        /// <summary>Stub of method System.Boolean QuickGraph.IEdgeSet`2.ContainsEdge(!1 edge)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IEdgeSet<TVertex, TEdge>.ContainsEdge(TEdge edge)
        {
            global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
              .SEdgeListAndIncidenceGraph<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                .SEdgeListAndIncidenceGraph<TVertex, TEdge>>)this).DefaultStub;
            return stub.Result<bool>(this);
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IImplicitGraph`2.IsOutEdgesEmpty(!0 v)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IImplicitGraph<TVertex, TEdge>.IsOutEdgesEmpty(TVertex v)
        {
            global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
              .SEdgeListAndIncidenceGraph<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                .SEdgeListAndIncidenceGraph<TVertex, TEdge>>)this).DefaultStub;
            return stub.Result<bool>(this);
        }

        /// <summary>Stub of method System.Int32 QuickGraph.IImplicitGraph`2.OutDegree(!0 v)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        int global::QuickGraph.IImplicitGraph<TVertex, TEdge>.OutDegree(TVertex v)
        {
            global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
              .SEdgeListAndIncidenceGraph<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                .SEdgeListAndIncidenceGraph<TVertex, TEdge>>)this).DefaultStub;
            return stub.Result<int>(this);
        }

        /// <summary>Stub of method !1 QuickGraph.IImplicitGraph`2.OutEdge(!0 v, System.Int32 index)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        TEdge global::QuickGraph.IImplicitGraph<TVertex, TEdge>.OutEdge(TVertex v, int index)
        {
            global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
              .SEdgeListAndIncidenceGraph<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                .SEdgeListAndIncidenceGraph<TVertex, TEdge>>)this).DefaultStub;
            return stub.Result<TEdge>(this);
        }

        /// <summary>Stub of method System.Collections.Generic.IEnumerable`1&lt;!1&gt; QuickGraph.IImplicitGraph`2.OutEdges(!0 v)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        global::System.Collections.Generic.IEnumerable<TEdge> global::QuickGraph.IImplicitGraph<TVertex, TEdge>.OutEdges(TVertex v)
        {
            global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
              .SEdgeListAndIncidenceGraph<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                .SEdgeListAndIncidenceGraph<TVertex, TEdge>>)this).DefaultStub;
            return stub.Result<global::System.Collections.Generic.IEnumerable<TEdge>>(this);
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IImplicitGraph`2.TryGetOutEdges(!0 v, System.Collections.Generic.IEnumerable`1&lt;!1&gt;&amp; edges)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IImplicitGraph<TVertex, TEdge>.TryGetOutEdges(TVertex v, out global::System.Collections.Generic.IEnumerable<TEdge> edges)
        {
            global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
              .SEdgeListAndIncidenceGraph<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                .SEdgeListAndIncidenceGraph<TVertex, TEdge>>)this).DefaultStub;
            stub.ValueAtReturn<global::System.Collections.Generic.IEnumerable<TEdge>>
                (this, out edges);
            return stub.Result<bool>(this);
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IIncidenceGraph`2.ContainsEdge(!0 source, !0 target)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IIncidenceGraph<TVertex, TEdge>.ContainsEdge(TVertex source, TVertex target)
        {
            global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
              .SEdgeListAndIncidenceGraph<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                .SEdgeListAndIncidenceGraph<TVertex, TEdge>>)this).DefaultStub;
            return stub.Result<bool>(this);
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IIncidenceGraph`2.TryGetEdge(!0 source, !0 target, !1&amp; edge)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IIncidenceGraph<TVertex, TEdge>.TryGetEdge(
            TVertex source,
            TVertex target,
            out TEdge edge
        )
        {
            global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
              .SEdgeListAndIncidenceGraph<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                .SEdgeListAndIncidenceGraph<TVertex, TEdge>>)this).DefaultStub;
            stub.ValueAtReturn<TEdge>(this, out edge);
            return stub.Result<bool>(this);
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IIncidenceGraph`2.TryGetEdges(!0 source, !0 target, System.Collections.Generic.IEnumerable`1&lt;!1&gt;&amp; edges)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IIncidenceGraph<TVertex, TEdge>.TryGetEdges(
            TVertex source,
            TVertex target,
            out global::System.Collections.Generic.IEnumerable<TEdge> edges
        )
        {
            global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
              .SEdgeListAndIncidenceGraph<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                .SEdgeListAndIncidenceGraph<TVertex, TEdge>>)this).DefaultStub;
            stub.ValueAtReturn<global::System.Collections.Generic.IEnumerable<TEdge>>
                (this, out edges);
            return stub.Result<bool>(this);
        }
    }
}
namespace QuickGraph.Stubs
{
    /// <summary>Stub of IEdgeListGraph`2</summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Stubs", "0.1.2.3")]
    [global::System.Serializable]
    [global::System.Diagnostics.DebuggerStepThrough]
    public partial class SEdgeListGraph<TVertex,TEdge>
      : global::Microsoft.Stubs.StubBase<global::QuickGraph.Stubs.SEdgeListGraph<TVertex, TEdge>>
      , global::QuickGraph.IEdgeListGraph<TVertex, TEdge>
        where TEdge : global::QuickGraph.IEdge<TVertex>
    {
        /// <summary>Initializes a new instance of type SEdgeListGraph</summary>
        [global::System.Diagnostics.DebuggerHidden]
        public SEdgeListGraph()
        {
        }

        /// <summary>Stub of property QuickGraph.IGraph`2.AllowParallelEdges</summary>
        bool global::QuickGraph.IGraph<TVertex, TEdge>.AllowParallelEdges
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates
                  .Func<global::QuickGraph.Stubs.SEdgeListGraph<TVertex, TEdge>, 
                  bool> sh = this.AllowParallelEdgesGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs.IDefaultStub
                      <global::QuickGraph.Stubs.SEdgeListGraph<TVertex, TEdge>> stub
                     = ((global::Microsoft.Stubs
                      .IStub<global::QuickGraph.Stubs.SEdgeListGraph<TVertex, TEdge>>)this)
                      .DefaultStub;
                  return stub.Result<bool>(this);
                }
            }
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IGraph`2.get_AllowParallelEdges()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SEdgeListGraph<TVertex, TEdge>, bool> AllowParallelEdgesGet;

        /// <summary>Stub of property QuickGraph.IEdgeSet`2.EdgeCount</summary>
        int global::QuickGraph.IEdgeSet<TVertex, TEdge>.EdgeCount
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates
                  .Func<global::QuickGraph.Stubs.SEdgeListGraph<TVertex, TEdge>, 
                  int> sh = this.EdgeCountGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs.IDefaultStub
                      <global::QuickGraph.Stubs.SEdgeListGraph<TVertex, TEdge>> stub
                     = ((global::Microsoft.Stubs
                      .IStub<global::QuickGraph.Stubs.SEdgeListGraph<TVertex, TEdge>>)this)
                      .DefaultStub;
                  return stub.Result<int>(this);
                }
            }
        }

        /// <summary>Stub of method System.Int32 QuickGraph.IEdgeSet`2.get_EdgeCount()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SEdgeListGraph<TVertex, TEdge>, int> EdgeCountGet;

        /// <summary>Stub of property QuickGraph.IEdgeSet`2.Edges</summary>
        global::System.Collections.Generic.IEnumerable<TEdge> global::QuickGraph.IEdgeSet<TVertex, TEdge>.Edges
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates
                  .Func<global::QuickGraph.Stubs.SEdgeListGraph<TVertex, TEdge>, 
                  global::System.Collections.Generic.IEnumerable<TEdge>> sh = this.EdgesGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs.IDefaultStub
                      <global::QuickGraph.Stubs.SEdgeListGraph<TVertex, TEdge>> stub
                     = ((global::Microsoft.Stubs
                      .IStub<global::QuickGraph.Stubs.SEdgeListGraph<TVertex, TEdge>>)this)
                      .DefaultStub;
                  return 
                    stub.Result<global::System.Collections.Generic.IEnumerable<TEdge>>(this);
                }
            }
        }

        /// <summary>Stub of method System.Collections.Generic.IEnumerable`1&lt;!1&gt; QuickGraph.IEdgeSet`2.get_Edges()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SEdgeListGraph<TVertex, TEdge>, global::System.Collections.Generic.IEnumerable<TEdge>> EdgesGet;

        /// <summary>Stub of property QuickGraph.IGraph`2.IsDirected</summary>
        bool global::QuickGraph.IGraph<TVertex, TEdge>.IsDirected
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates
                  .Func<global::QuickGraph.Stubs.SEdgeListGraph<TVertex, TEdge>, 
                  bool> sh = this.IsDirectedGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs.IDefaultStub
                      <global::QuickGraph.Stubs.SEdgeListGraph<TVertex, TEdge>> stub
                     = ((global::Microsoft.Stubs
                      .IStub<global::QuickGraph.Stubs.SEdgeListGraph<TVertex, TEdge>>)this)
                      .DefaultStub;
                  return stub.Result<bool>(this);
                }
            }
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IGraph`2.get_IsDirected()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SEdgeListGraph<TVertex, TEdge>, bool> IsDirectedGet;

        /// <summary>Stub of property QuickGraph.IEdgeSet`2.IsEdgesEmpty</summary>
        bool global::QuickGraph.IEdgeSet<TVertex, TEdge>.IsEdgesEmpty
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates
                  .Func<global::QuickGraph.Stubs.SEdgeListGraph<TVertex, TEdge>, 
                  bool> sh = this.IsEdgesEmptyGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs.IDefaultStub
                      <global::QuickGraph.Stubs.SEdgeListGraph<TVertex, TEdge>> stub
                     = ((global::Microsoft.Stubs
                      .IStub<global::QuickGraph.Stubs.SEdgeListGraph<TVertex, TEdge>>)this)
                      .DefaultStub;
                  return stub.Result<bool>(this);
                }
            }
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IEdgeSet`2.get_IsEdgesEmpty()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SEdgeListGraph<TVertex, TEdge>, bool> IsEdgesEmptyGet;

        /// <summary>Stub of method System.Boolean QuickGraph.IEdgeSet`2.ContainsEdge(!1 edge)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IEdgeSet<TVertex, TEdge>.ContainsEdge(TEdge edge)
        {
            global::Microsoft.Stubs.IDefaultStub
                <global::QuickGraph.Stubs.SEdgeListGraph<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs
                .IStub<global::QuickGraph.Stubs.SEdgeListGraph<TVertex, TEdge>>)this)
                .DefaultStub;
            return stub.Result<bool>(this);
        }
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
namespace QuickGraph.Stubs
{
    /// <summary>Stub of IEdgeSet`2</summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Stubs", "0.1.2.3")]
    [global::System.Serializable]
    [global::System.Diagnostics.DebuggerStepThrough]
    public partial class SEdgeSet<TVertex,TEdge>
      : global::Microsoft.Stubs.StubBase<global::QuickGraph.Stubs.SEdgeSet<TVertex, TEdge>>
      , global::QuickGraph.IEdgeSet<TVertex, TEdge>
        where TEdge : global::QuickGraph.IEdge<TVertex>
    {
        /// <summary>Initializes a new instance of type SEdgeSet</summary>
        [global::System.Diagnostics.DebuggerHidden]
        public SEdgeSet()
        {
        }

        /// <summary>Stub of property QuickGraph.IEdgeSet`2.EdgeCount</summary>
        int global::QuickGraph.IEdgeSet<TVertex, TEdge>.EdgeCount
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates
                  .Func<global::QuickGraph.Stubs.SEdgeSet<TVertex, TEdge>, int> sh
                   = this.EdgeCountGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs
                    .IDefaultStub<global::QuickGraph.Stubs.SEdgeSet<TVertex, TEdge>> stub
                     = ((global::Microsoft.Stubs
                      .IStub<global::QuickGraph.Stubs.SEdgeSet<TVertex, TEdge>>)this)
                      .DefaultStub;
                  return stub.Result<int>(this);
                }
            }
        }

        /// <summary>Stub of method System.Int32 QuickGraph.IEdgeSet`2.get_EdgeCount()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SEdgeSet<TVertex, TEdge>, int> EdgeCountGet;

        /// <summary>Stub of property QuickGraph.IEdgeSet`2.Edges</summary>
        global::System.Collections.Generic.IEnumerable<TEdge> global::QuickGraph.IEdgeSet<TVertex, TEdge>.Edges
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates
                  .Func<global::QuickGraph.Stubs.SEdgeSet<TVertex, TEdge>, 
                  global::System.Collections.Generic.IEnumerable<TEdge>> sh = this.EdgesGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs
                    .IDefaultStub<global::QuickGraph.Stubs.SEdgeSet<TVertex, TEdge>> stub
                     = ((global::Microsoft.Stubs
                      .IStub<global::QuickGraph.Stubs.SEdgeSet<TVertex, TEdge>>)this)
                      .DefaultStub;
                  return 
                    stub.Result<global::System.Collections.Generic.IEnumerable<TEdge>>(this);
                }
            }
        }

        /// <summary>Stub of method System.Collections.Generic.IEnumerable`1&lt;!1&gt; QuickGraph.IEdgeSet`2.get_Edges()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SEdgeSet<TVertex, TEdge>, global::System.Collections.Generic.IEnumerable<TEdge>> EdgesGet;

        /// <summary>Stub of property QuickGraph.IEdgeSet`2.IsEdgesEmpty</summary>
        bool global::QuickGraph.IEdgeSet<TVertex, TEdge>.IsEdgesEmpty
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates
                  .Func<global::QuickGraph.Stubs.SEdgeSet<TVertex, TEdge>, bool> sh
                   = this.IsEdgesEmptyGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs
                    .IDefaultStub<global::QuickGraph.Stubs.SEdgeSet<TVertex, TEdge>> stub
                     = ((global::Microsoft.Stubs
                      .IStub<global::QuickGraph.Stubs.SEdgeSet<TVertex, TEdge>>)this)
                      .DefaultStub;
                  return stub.Result<bool>(this);
                }
            }
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IEdgeSet`2.get_IsEdgesEmpty()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SEdgeSet<TVertex, TEdge>, bool> IsEdgesEmptyGet;

        /// <summary>Stub of method System.Boolean QuickGraph.IEdgeSet`2.ContainsEdge(!1 edge)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IEdgeSet<TVertex, TEdge>.ContainsEdge(TEdge edge)
        {
            global::Microsoft.Stubs
              .IDefaultStub<global::QuickGraph.Stubs.SEdgeSet<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs
                .IStub<global::QuickGraph.Stubs.SEdgeSet<TVertex, TEdge>>)this).DefaultStub;
            return stub.Result<bool>(this);
        }
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
namespace QuickGraph.Stubs
{
    /// <summary>Stub of IGraph`2</summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Stubs", "0.1.2.3")]
    [global::System.Serializable]
    [global::System.Diagnostics.DebuggerStepThrough]
    public partial class SGraph<TVertex,TEdge>
      : global::Microsoft.Stubs.StubBase<global::QuickGraph.Stubs.SGraph<TVertex, TEdge>>
      , global::QuickGraph.IGraph<TVertex, TEdge>
        where TEdge : global::QuickGraph.IEdge<TVertex>
    {
        /// <summary>Initializes a new instance of type SGraph</summary>
        [global::System.Diagnostics.DebuggerHidden]
        public SGraph()
        {
        }

        /// <summary>Stub of property QuickGraph.IGraph`2.AllowParallelEdges</summary>
        bool global::QuickGraph.IGraph<TVertex, TEdge>.AllowParallelEdges
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates
                  .Func<global::QuickGraph.Stubs.SGraph<TVertex, TEdge>, bool> sh
                   = this.AllowParallelEdgesGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs
                    .IDefaultStub<global::QuickGraph.Stubs.SGraph<TVertex, TEdge>> stub
                     = ((global::Microsoft.Stubs
                      .IStub<global::QuickGraph.Stubs.SGraph<TVertex, TEdge>>)this).DefaultStub;
                  return stub.Result<bool>(this);
                }
            }
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IGraph`2.get_AllowParallelEdges()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SGraph<TVertex, TEdge>, bool> AllowParallelEdgesGet;

        /// <summary>Stub of property QuickGraph.IGraph`2.IsDirected</summary>
        bool global::QuickGraph.IGraph<TVertex, TEdge>.IsDirected
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates
                  .Func<global::QuickGraph.Stubs.SGraph<TVertex, TEdge>, bool> sh
                   = this.IsDirectedGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs
                    .IDefaultStub<global::QuickGraph.Stubs.SGraph<TVertex, TEdge>> stub
                     = ((global::Microsoft.Stubs
                      .IStub<global::QuickGraph.Stubs.SGraph<TVertex, TEdge>>)this).DefaultStub;
                  return stub.Result<bool>(this);
                }
            }
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IGraph`2.get_IsDirected()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SGraph<TVertex, TEdge>, bool> IsDirectedGet;
    }
}
namespace QuickGraph.Stubs
{
    /// <summary>Stub of IHierarchy`2</summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Stubs", "0.1.2.3")]
    [global::System.Serializable]
    [global::System.Diagnostics.DebuggerStepThrough]
    public partial class SHierarchy<TVertex,TEdge>
      : global::Microsoft.Stubs.StubBase<global::QuickGraph.Stubs.SHierarchy<TVertex, TEdge>>
      , global::QuickGraph.IHierarchy<TVertex, TEdge>
        where TEdge : global::QuickGraph.IEdge<TVertex>
    {
        /// <summary>Initializes a new instance of type SHierarchy</summary>
        [global::System.Diagnostics.DebuggerHidden]
        public SHierarchy()
        {
        }

        /// <summary>Stub of method System.Int32 QuickGraph.IMutableEdgeListGraph`2.AddEdgeRange(System.Collections.Generic.IEnumerable`1&lt;!1&gt; edges)</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SHierarchy<TVertex, TEdge>, global::System.Collections.Generic.IEnumerable<TEdge>, int> AddEdgeRange;

        /// <summary>Stub of method System.Int32 QuickGraph.IMutableVertexSet`1.AddVertexRange(System.Collections.Generic.IEnumerable`1&lt;!0&gt; vertices)</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SHierarchy<TVertex, TEdge>, global::System.Collections.Generic.IEnumerable<TVertex>, int> AddVertexRange;

        /// <summary>Stub of method System.Int32 QuickGraph.IMutableVertexAndEdgeSet`2.AddVerticesAndEdgeRange(System.Collections.Generic.IEnumerable`1&lt;!1&gt; edges)</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SHierarchy<TVertex, TEdge>, global::System.Collections.Generic.IEnumerable<TEdge>, int> AddVerticesAndEdgeRange;

        /// <summary>Stub of property QuickGraph.IGraph`2.AllowParallelEdges</summary>
        bool global::QuickGraph.IGraph<TVertex, TEdge>.AllowParallelEdges
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates
                  .Func<global::QuickGraph.Stubs.SHierarchy<TVertex, TEdge>, bool> sh
                   = this.AllowParallelEdgesGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs
                    .IDefaultStub<global::QuickGraph.Stubs.SHierarchy<TVertex, TEdge>> stub
                     = ((global::Microsoft.Stubs
                      .IStub<global::QuickGraph.Stubs.SHierarchy<TVertex, TEdge>>)this)
                      .DefaultStub;
                  return stub.Result<bool>(this);
                }
            }
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IGraph`2.get_AllowParallelEdges()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SHierarchy<TVertex, TEdge>, bool> AllowParallelEdgesGet;

        /// <summary>Stub of method System.Void QuickGraph.IMutableGraph`2.Clear()</summary>
        public global::Microsoft.Stubs.StubDelegates.Action<global::QuickGraph.Stubs.SHierarchy<TVertex, TEdge>> Clear;

        /// <summary>Event EdgeAdded</summary>
        public event global::QuickGraph.EdgeAction<TVertex, TEdge> EdgeAdded;

        /// <summary>Stub of property QuickGraph.IEdgeSet`2.EdgeCount</summary>
        int global::QuickGraph.IEdgeSet<TVertex, TEdge>.EdgeCount
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates
                  .Func<global::QuickGraph.Stubs.SHierarchy<TVertex, TEdge>, int> sh
                   = this.EdgeCountGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs
                    .IDefaultStub<global::QuickGraph.Stubs.SHierarchy<TVertex, TEdge>> stub
                     = ((global::Microsoft.Stubs
                      .IStub<global::QuickGraph.Stubs.SHierarchy<TVertex, TEdge>>)this)
                      .DefaultStub;
                  return stub.Result<int>(this);
                }
            }
        }

        /// <summary>Stub of method System.Int32 QuickGraph.IEdgeSet`2.get_EdgeCount()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SHierarchy<TVertex, TEdge>, int> EdgeCountGet;

        /// <summary>Event EdgeRemoved</summary>
        public event global::QuickGraph.EdgeAction<TVertex, TEdge> EdgeRemoved;

        /// <summary>Stub of property QuickGraph.IEdgeSet`2.Edges</summary>
        global::System.Collections.Generic.IEnumerable<TEdge> global::QuickGraph.IEdgeSet<TVertex, TEdge>.Edges
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates
                  .Func<global::QuickGraph.Stubs.SHierarchy<TVertex, TEdge>, 
                  global::System.Collections.Generic.IEnumerable<TEdge>> sh = this.EdgesGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs
                    .IDefaultStub<global::QuickGraph.Stubs.SHierarchy<TVertex, TEdge>> stub
                     = ((global::Microsoft.Stubs
                      .IStub<global::QuickGraph.Stubs.SHierarchy<TVertex, TEdge>>)this)
                      .DefaultStub;
                  return 
                    stub.Result<global::System.Collections.Generic.IEnumerable<TEdge>>(this);
                }
            }
        }

        /// <summary>Stub of method System.Collections.Generic.IEnumerable`1&lt;!1&gt; QuickGraph.IEdgeSet`2.get_Edges()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SHierarchy<TVertex, TEdge>, global::System.Collections.Generic.IEnumerable<TEdge>> EdgesGet;

        /// <summary>Stub of property QuickGraph.IGraph`2.IsDirected</summary>
        bool global::QuickGraph.IGraph<TVertex, TEdge>.IsDirected
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates
                  .Func<global::QuickGraph.Stubs.SHierarchy<TVertex, TEdge>, bool> sh
                   = this.IsDirectedGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs
                    .IDefaultStub<global::QuickGraph.Stubs.SHierarchy<TVertex, TEdge>> stub
                     = ((global::Microsoft.Stubs
                      .IStub<global::QuickGraph.Stubs.SHierarchy<TVertex, TEdge>>)this)
                      .DefaultStub;
                  return stub.Result<bool>(this);
                }
            }
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IGraph`2.get_IsDirected()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SHierarchy<TVertex, TEdge>, bool> IsDirectedGet;

        /// <summary>Stub of property QuickGraph.IEdgeSet`2.IsEdgesEmpty</summary>
        bool global::QuickGraph.IEdgeSet<TVertex, TEdge>.IsEdgesEmpty
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates
                  .Func<global::QuickGraph.Stubs.SHierarchy<TVertex, TEdge>, bool> sh
                   = this.IsEdgesEmptyGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs
                    .IDefaultStub<global::QuickGraph.Stubs.SHierarchy<TVertex, TEdge>> stub
                     = ((global::Microsoft.Stubs
                      .IStub<global::QuickGraph.Stubs.SHierarchy<TVertex, TEdge>>)this)
                      .DefaultStub;
                  return stub.Result<bool>(this);
                }
            }
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IEdgeSet`2.get_IsEdgesEmpty()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SHierarchy<TVertex, TEdge>, bool> IsEdgesEmptyGet;

        /// <summary>Stub of property QuickGraph.IVertexSet`1.IsVerticesEmpty</summary>
        bool global::QuickGraph.IVertexSet<TVertex>.IsVerticesEmpty
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates
                  .Func<global::QuickGraph.Stubs.SHierarchy<TVertex, TEdge>, bool> sh
                   = this.IsVerticesEmptyGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs
                    .IDefaultStub<global::QuickGraph.Stubs.SHierarchy<TVertex, TEdge>> stub
                     = ((global::Microsoft.Stubs
                      .IStub<global::QuickGraph.Stubs.SHierarchy<TVertex, TEdge>>)this)
                      .DefaultStub;
                  return stub.Result<bool>(this);
                }
            }
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IVertexSet`1.get_IsVerticesEmpty()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SHierarchy<TVertex, TEdge>, bool> IsVerticesEmptyGet;

        /// <summary>Stub of method System.Boolean QuickGraph.IEdgeSet`2.ContainsEdge(!1 edge)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IEdgeSet<TVertex, TEdge>.ContainsEdge(TEdge edge)
        {
            global::Microsoft.Stubs
              .IDefaultStub<global::QuickGraph.Stubs.SHierarchy<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs
                .IStub<global::QuickGraph.Stubs.SHierarchy<TVertex, TEdge>>)this)
                .DefaultStub;
            return stub.Result<bool>(this);
        }

        /// <summary>Stub of method System.Collections.Generic.IEnumerable`1&lt;!1&gt; QuickGraph.IHierarchy`2.ChildrenEdges(!0 vertex)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        global::System.Collections.Generic.IEnumerable<TEdge> global::QuickGraph.IHierarchy<TVertex, TEdge>.ChildrenEdges(TVertex vertex)
        {
            global::Microsoft.Stubs
              .IDefaultStub<global::QuickGraph.Stubs.SHierarchy<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs
                .IStub<global::QuickGraph.Stubs.SHierarchy<TVertex, TEdge>>)this)
                .DefaultStub;
            return stub.Result<global::System.Collections.Generic.IEnumerable<TEdge>>(this);
        }

        /// <summary>Stub of method System.Collections.Generic.IEnumerable`1&lt;!0&gt; QuickGraph.IHierarchy`2.ChildrenVertices(!0 vertex)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        global::System.Collections.Generic.IEnumerable<TVertex> global::QuickGraph.IHierarchy<TVertex, TEdge>.ChildrenVertices(TVertex vertex)
        {
            global::Microsoft.Stubs
              .IDefaultStub<global::QuickGraph.Stubs.SHierarchy<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs
                .IStub<global::QuickGraph.Stubs.SHierarchy<TVertex, TEdge>>)this)
                .DefaultStub;
            return 
              stub.Result<global::System.Collections.Generic.IEnumerable<TVertex>>(this);
        }

        /// <summary>Stub of method !0 QuickGraph.IHierarchy`2.GetParent(!0 vertex)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        TVertex global::QuickGraph.IHierarchy<TVertex, TEdge>.GetParent(TVertex vertex)
        {
            global::Microsoft.Stubs
              .IDefaultStub<global::QuickGraph.Stubs.SHierarchy<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs
                .IStub<global::QuickGraph.Stubs.SHierarchy<TVertex, TEdge>>)this)
                .DefaultStub;
            return stub.Result<TVertex>(this);
        }

        /// <summary>Stub of method !1 QuickGraph.IHierarchy`2.GetParentEdge(!0 vertex)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        TEdge global::QuickGraph.IHierarchy<TVertex, TEdge>.GetParentEdge(TVertex vertex)
        {
            global::Microsoft.Stubs
              .IDefaultStub<global::QuickGraph.Stubs.SHierarchy<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs
                .IStub<global::QuickGraph.Stubs.SHierarchy<TVertex, TEdge>>)this)
                .DefaultStub;
            return stub.Result<TEdge>(this);
        }

        /// <summary>Stub of method System.Int32 QuickGraph.IHierarchy`2.InducedEdgeCount(!0 source, !0 target)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        int global::QuickGraph.IHierarchy<TVertex, TEdge>.InducedEdgeCount(TVertex source, TVertex target)
        {
            global::Microsoft.Stubs
              .IDefaultStub<global::QuickGraph.Stubs.SHierarchy<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs
                .IStub<global::QuickGraph.Stubs.SHierarchy<TVertex, TEdge>>)this)
                .DefaultStub;
            return stub.Result<int>(this);
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IHierarchy`2.IsCrossEdge(!1 edge)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IHierarchy<TVertex, TEdge>.IsCrossEdge(TEdge edge)
        {
            global::Microsoft.Stubs
              .IDefaultStub<global::QuickGraph.Stubs.SHierarchy<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs
                .IStub<global::QuickGraph.Stubs.SHierarchy<TVertex, TEdge>>)this)
                .DefaultStub;
            return stub.Result<bool>(this);
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IHierarchy`2.IsInnerNode(!0 vertex)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IHierarchy<TVertex, TEdge>.IsInnerNode(TVertex vertex)
        {
            global::Microsoft.Stubs
              .IDefaultStub<global::QuickGraph.Stubs.SHierarchy<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs
                .IStub<global::QuickGraph.Stubs.SHierarchy<TVertex, TEdge>>)this)
                .DefaultStub;
            return stub.Result<bool>(this);
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IHierarchy`2.IsPredecessorOf(!0 source, !0 target)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IHierarchy<TVertex, TEdge>.IsPredecessorOf(TVertex source, TVertex target)
        {
            global::Microsoft.Stubs
              .IDefaultStub<global::QuickGraph.Stubs.SHierarchy<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs
                .IStub<global::QuickGraph.Stubs.SHierarchy<TVertex, TEdge>>)this)
                .DefaultStub;
            return stub.Result<bool>(this);
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IHierarchy`2.IsRealEdge(!1 edge)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IHierarchy<TVertex, TEdge>.IsRealEdge(TEdge edge)
        {
            global::Microsoft.Stubs
              .IDefaultStub<global::QuickGraph.Stubs.SHierarchy<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs
                .IStub<global::QuickGraph.Stubs.SHierarchy<TVertex, TEdge>>)this)
                .DefaultStub;
            return stub.Result<bool>(this);
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IImplicitGraph`2.IsOutEdgesEmpty(!0 v)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IImplicitGraph<TVertex, TEdge>.IsOutEdgesEmpty(TVertex v)
        {
            global::Microsoft.Stubs
              .IDefaultStub<global::QuickGraph.Stubs.SHierarchy<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs
                .IStub<global::QuickGraph.Stubs.SHierarchy<TVertex, TEdge>>)this)
                .DefaultStub;
            return stub.Result<bool>(this);
        }

        /// <summary>Stub of method System.Int32 QuickGraph.IImplicitGraph`2.OutDegree(!0 v)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        int global::QuickGraph.IImplicitGraph<TVertex, TEdge>.OutDegree(TVertex v)
        {
            global::Microsoft.Stubs
              .IDefaultStub<global::QuickGraph.Stubs.SHierarchy<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs
                .IStub<global::QuickGraph.Stubs.SHierarchy<TVertex, TEdge>>)this)
                .DefaultStub;
            return stub.Result<int>(this);
        }

        /// <summary>Stub of method !1 QuickGraph.IImplicitGraph`2.OutEdge(!0 v, System.Int32 index)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        TEdge global::QuickGraph.IImplicitGraph<TVertex, TEdge>.OutEdge(TVertex v, int index)
        {
            global::Microsoft.Stubs
              .IDefaultStub<global::QuickGraph.Stubs.SHierarchy<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs
                .IStub<global::QuickGraph.Stubs.SHierarchy<TVertex, TEdge>>)this)
                .DefaultStub;
            return stub.Result<TEdge>(this);
        }

        /// <summary>Stub of method System.Collections.Generic.IEnumerable`1&lt;!1&gt; QuickGraph.IImplicitGraph`2.OutEdges(!0 v)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        global::System.Collections.Generic.IEnumerable<TEdge> global::QuickGraph.IImplicitGraph<TVertex, TEdge>.OutEdges(TVertex v)
        {
            global::Microsoft.Stubs
              .IDefaultStub<global::QuickGraph.Stubs.SHierarchy<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs
                .IStub<global::QuickGraph.Stubs.SHierarchy<TVertex, TEdge>>)this)
                .DefaultStub;
            return stub.Result<global::System.Collections.Generic.IEnumerable<TEdge>>(this);
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IImplicitGraph`2.TryGetOutEdges(!0 v, System.Collections.Generic.IEnumerable`1&lt;!1&gt;&amp; edges)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IImplicitGraph<TVertex, TEdge>.TryGetOutEdges(TVertex v, out global::System.Collections.Generic.IEnumerable<TEdge> edges)
        {
            global::Microsoft.Stubs
              .IDefaultStub<global::QuickGraph.Stubs.SHierarchy<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs
                .IStub<global::QuickGraph.Stubs.SHierarchy<TVertex, TEdge>>)this)
                .DefaultStub;
            stub.ValueAtReturn<global::System.Collections.Generic.IEnumerable<TEdge>>
                (this, out edges);
            return stub.Result<bool>(this);
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IIncidenceGraph`2.ContainsEdge(!0 source, !0 target)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IIncidenceGraph<TVertex, TEdge>.ContainsEdge(TVertex source, TVertex target)
        {
            global::Microsoft.Stubs
              .IDefaultStub<global::QuickGraph.Stubs.SHierarchy<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs
                .IStub<global::QuickGraph.Stubs.SHierarchy<TVertex, TEdge>>)this)
                .DefaultStub;
            return stub.Result<bool>(this);
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IIncidenceGraph`2.TryGetEdge(!0 source, !0 target, !1&amp; edge)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IIncidenceGraph<TVertex, TEdge>.TryGetEdge(
            TVertex source,
            TVertex target,
            out TEdge edge
        )
        {
            global::Microsoft.Stubs
              .IDefaultStub<global::QuickGraph.Stubs.SHierarchy<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs
                .IStub<global::QuickGraph.Stubs.SHierarchy<TVertex, TEdge>>)this)
                .DefaultStub;
            stub.ValueAtReturn<TEdge>(this, out edge);
            return stub.Result<bool>(this);
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IIncidenceGraph`2.TryGetEdges(!0 source, !0 target, System.Collections.Generic.IEnumerable`1&lt;!1&gt;&amp; edges)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IIncidenceGraph<TVertex, TEdge>.TryGetEdges(
            TVertex source,
            TVertex target,
            out global::System.Collections.Generic.IEnumerable<TEdge> edges
        )
        {
            global::Microsoft.Stubs
              .IDefaultStub<global::QuickGraph.Stubs.SHierarchy<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs
                .IStub<global::QuickGraph.Stubs.SHierarchy<TVertex, TEdge>>)this)
                .DefaultStub;
            stub.ValueAtReturn<global::System.Collections.Generic.IEnumerable<TEdge>>
                (this, out edges);
            return stub.Result<bool>(this);
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IMutableEdgeListGraph`2.AddEdge(!1 edge)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IMutableEdgeListGraph<TVertex, TEdge>.AddEdge(TEdge edge)
        {
            global::Microsoft.Stubs
              .IDefaultStub<global::QuickGraph.Stubs.SHierarchy<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs
                .IStub<global::QuickGraph.Stubs.SHierarchy<TVertex, TEdge>>)this)
                .DefaultStub;
            return stub.Result<bool>(this);
        }

        /// <summary>Stub of method System.Int32 QuickGraph.IMutableEdgeListGraph`2.AddEdgeRange(System.Collections.Generic.IEnumerable`1&lt;!1&gt; edges)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        int global::QuickGraph.IMutableEdgeListGraph<TVertex, TEdge>.AddEdgeRange(global::System.Collections.Generic.IEnumerable<TEdge> edges)
        {
            global::Microsoft.Stubs.StubDelegates
              .Func<global::QuickGraph.Stubs.SHierarchy<TVertex, TEdge>, 
              global::System.Collections.Generic.IEnumerable<TEdge>, int> sh
               = this.AddEdgeRange;
            if ((object)sh != (object)null)
              return sh.Invoke(this, edges);
            else 
            {
              global::Microsoft.Stubs
                .IDefaultStub<global::QuickGraph.Stubs.SHierarchy<TVertex, TEdge>> stub
                 = ((global::Microsoft.Stubs
                  .IStub<global::QuickGraph.Stubs.SHierarchy<TVertex, TEdge>>)this)
                  .DefaultStub;
              return stub.Result<int>(this);
            }
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IMutableEdgeListGraph`2.RemoveEdge(!1 edge)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IMutableEdgeListGraph<TVertex, TEdge>.RemoveEdge(TEdge edge)
        {
            global::Microsoft.Stubs
              .IDefaultStub<global::QuickGraph.Stubs.SHierarchy<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs
                .IStub<global::QuickGraph.Stubs.SHierarchy<TVertex, TEdge>>)this)
                .DefaultStub;
            return stub.Result<bool>(this);
        }

        /// <summary>Stub of method System.Int32 QuickGraph.IMutableEdgeListGraph`2.RemoveEdgeIf(QuickGraph.EdgePredicate`2&lt;!0,!1&gt; predicate)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        int global::QuickGraph.IMutableEdgeListGraph<TVertex, TEdge>.RemoveEdgeIf(global::QuickGraph.EdgePredicate<TVertex, TEdge> predicate)
        {
            global::Microsoft.Stubs.StubDelegates
              .Func<global::QuickGraph.Stubs.SHierarchy<TVertex, TEdge>, 
              global::QuickGraph.EdgePredicate<TVertex, TEdge>, int> sh = this.RemoveEdgeIf;
            if ((object)sh != (object)null)
              return sh.Invoke(this, predicate);
            else 
            {
              global::Microsoft.Stubs
                .IDefaultStub<global::QuickGraph.Stubs.SHierarchy<TVertex, TEdge>> stub
                 = ((global::Microsoft.Stubs
                  .IStub<global::QuickGraph.Stubs.SHierarchy<TVertex, TEdge>>)this)
                  .DefaultStub;
              return stub.Result<int>(this);
            }
        }

        /// <summary>Stub of method System.Void QuickGraph.IMutableGraph`2.Clear()</summary>
        [global::System.Diagnostics.DebuggerHidden]
        void global::QuickGraph.IMutableGraph<TVertex, TEdge>.Clear()
        {
            global::Microsoft.Stubs.StubDelegates
              .Action<global::QuickGraph.Stubs.SHierarchy<TVertex, TEdge>> sh = this.Clear;
            if ((object)sh != (object)null)
              sh.Invoke(this);
            else 
            {
              global::Microsoft.Stubs
                .IDefaultStub<global::QuickGraph.Stubs.SHierarchy<TVertex, TEdge>> stub
                 = ((global::Microsoft.Stubs
                  .IStub<global::QuickGraph.Stubs.SHierarchy<TVertex, TEdge>>)this)
                  .DefaultStub;
              stub.VoidResult(this);
            }
        }

        /// <summary>Stub of method System.Void QuickGraph.IMutableIncidenceGraph`2.ClearOutEdges(!0 v)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        void global::QuickGraph.IMutableIncidenceGraph<TVertex, TEdge>.ClearOutEdges(TVertex v)
        {
            global::Microsoft.Stubs
              .IDefaultStub<global::QuickGraph.Stubs.SHierarchy<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs
                .IStub<global::QuickGraph.Stubs.SHierarchy<TVertex, TEdge>>)this)
                .DefaultStub;
            stub.VoidResult(this);
        }

        /// <summary>Stub of method System.Int32 QuickGraph.IMutableIncidenceGraph`2.RemoveOutEdgeIf(!0 v, QuickGraph.EdgePredicate`2&lt;!0,!1&gt; predicate)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        int global::QuickGraph.IMutableIncidenceGraph<TVertex, TEdge>.RemoveOutEdgeIf(TVertex v, global::QuickGraph.EdgePredicate<TVertex, TEdge> predicate)
        {
            global::Microsoft.Stubs
              .IDefaultStub<global::QuickGraph.Stubs.SHierarchy<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs
                .IStub<global::QuickGraph.Stubs.SHierarchy<TVertex, TEdge>>)this)
                .DefaultStub;
            return stub.Result<int>(this);
        }

        /// <summary>Stub of method System.Void QuickGraph.IMutableIncidenceGraph`2.TrimEdgeExcess()</summary>
        [global::System.Diagnostics.DebuggerHidden]
        void global::QuickGraph.IMutableIncidenceGraph<TVertex, TEdge>.TrimEdgeExcess()
        {
            global::Microsoft.Stubs.StubDelegates
              .Action<global::QuickGraph.Stubs.SHierarchy<TVertex, TEdge>> sh
               = this.TrimEdgeExcess;
            if ((object)sh != (object)null)
              sh.Invoke(this);
            else 
            {
              global::Microsoft.Stubs
                .IDefaultStub<global::QuickGraph.Stubs.SHierarchy<TVertex, TEdge>> stub
                 = ((global::Microsoft.Stubs
                  .IStub<global::QuickGraph.Stubs.SHierarchy<TVertex, TEdge>>)this)
                  .DefaultStub;
              stub.VoidResult(this);
            }
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IMutableVertexAndEdgeSet`2.AddVerticesAndEdge(!1 edge)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IMutableVertexAndEdgeSet<TVertex, TEdge>.AddVerticesAndEdge(TEdge edge)
        {
            global::Microsoft.Stubs
              .IDefaultStub<global::QuickGraph.Stubs.SHierarchy<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs
                .IStub<global::QuickGraph.Stubs.SHierarchy<TVertex, TEdge>>)this)
                .DefaultStub;
            return stub.Result<bool>(this);
        }

        /// <summary>Stub of method System.Int32 QuickGraph.IMutableVertexAndEdgeSet`2.AddVerticesAndEdgeRange(System.Collections.Generic.IEnumerable`1&lt;!1&gt; edges)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        int global::QuickGraph.IMutableVertexAndEdgeSet<TVertex, TEdge>.AddVerticesAndEdgeRange(global::System.Collections.Generic.IEnumerable<TEdge> edges)
        {
            global::Microsoft.Stubs.StubDelegates
              .Func<global::QuickGraph.Stubs.SHierarchy<TVertex, TEdge>, 
              global::System.Collections.Generic.IEnumerable<TEdge>, int> sh
               = this.AddVerticesAndEdgeRange;
            if ((object)sh != (object)null)
              return sh.Invoke(this, edges);
            else 
            {
              global::Microsoft.Stubs
                .IDefaultStub<global::QuickGraph.Stubs.SHierarchy<TVertex, TEdge>> stub
                 = ((global::Microsoft.Stubs
                  .IStub<global::QuickGraph.Stubs.SHierarchy<TVertex, TEdge>>)this)
                  .DefaultStub;
              return stub.Result<int>(this);
            }
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IMutableVertexSet`1.AddVertex(!0 v)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IMutableVertexSet<TVertex>.AddVertex(TVertex v)
        {
            global::Microsoft.Stubs
              .IDefaultStub<global::QuickGraph.Stubs.SHierarchy<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs
                .IStub<global::QuickGraph.Stubs.SHierarchy<TVertex, TEdge>>)this)
                .DefaultStub;
            return stub.Result<bool>(this);
        }

        /// <summary>Stub of method System.Int32 QuickGraph.IMutableVertexSet`1.AddVertexRange(System.Collections.Generic.IEnumerable`1&lt;!0&gt; vertices)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        int global::QuickGraph.IMutableVertexSet<TVertex>.AddVertexRange(global::System.Collections.Generic.IEnumerable<TVertex> vertices)
        {
            global::Microsoft.Stubs.StubDelegates
              .Func<global::QuickGraph.Stubs.SHierarchy<TVertex, TEdge>, 
              global::System.Collections.Generic.IEnumerable<TVertex>, 
              int> sh = this.AddVertexRange;
            if ((object)sh != (object)null)
              return sh.Invoke(this, vertices);
            else 
            {
              global::Microsoft.Stubs
                .IDefaultStub<global::QuickGraph.Stubs.SHierarchy<TVertex, TEdge>> stub
                 = ((global::Microsoft.Stubs
                  .IStub<global::QuickGraph.Stubs.SHierarchy<TVertex, TEdge>>)this)
                  .DefaultStub;
              return stub.Result<int>(this);
            }
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IMutableVertexSet`1.RemoveVertex(!0 v)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IMutableVertexSet<TVertex>.RemoveVertex(TVertex v)
        {
            global::Microsoft.Stubs
              .IDefaultStub<global::QuickGraph.Stubs.SHierarchy<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs
                .IStub<global::QuickGraph.Stubs.SHierarchy<TVertex, TEdge>>)this)
                .DefaultStub;
            return stub.Result<bool>(this);
        }

        /// <summary>Stub of method System.Int32 QuickGraph.IMutableVertexSet`1.RemoveVertexIf(QuickGraph.VertexPredicate`1&lt;!0&gt; pred)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        int global::QuickGraph.IMutableVertexSet<TVertex>.RemoveVertexIf(global::QuickGraph.VertexPredicate<TVertex> pred)
        {
            global::Microsoft.Stubs.StubDelegates
              .Func<global::QuickGraph.Stubs.SHierarchy<TVertex, TEdge>, 
              global::QuickGraph.VertexPredicate<TVertex>, int> sh = this.RemoveVertexIf;
            if ((object)sh != (object)null)
              return sh.Invoke(this, pred);
            else 
            {
              global::Microsoft.Stubs
                .IDefaultStub<global::QuickGraph.Stubs.SHierarchy<TVertex, TEdge>> stub
                 = ((global::Microsoft.Stubs
                  .IStub<global::QuickGraph.Stubs.SHierarchy<TVertex, TEdge>>)this)
                  .DefaultStub;
              return stub.Result<int>(this);
            }
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IVertexSet`1.ContainsVertex(!0 vertex)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IVertexSet<TVertex>.ContainsVertex(TVertex vertex)
        {
            global::Microsoft.Stubs
              .IDefaultStub<global::QuickGraph.Stubs.SHierarchy<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs
                .IStub<global::QuickGraph.Stubs.SHierarchy<TVertex, TEdge>>)this)
                .DefaultStub;
            return stub.Result<bool>(this);
        }

        /// <summary>Stub of method System.Int32 QuickGraph.IMutableEdgeListGraph`2.RemoveEdgeIf(QuickGraph.EdgePredicate`2&lt;!0,!1&gt; predicate)</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SHierarchy<TVertex, TEdge>, global::QuickGraph.EdgePredicate<TVertex, TEdge>, int> RemoveEdgeIf;

        /// <summary>Stub of method System.Int32 QuickGraph.IMutableVertexSet`1.RemoveVertexIf(QuickGraph.VertexPredicate`1&lt;!0&gt; pred)</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SHierarchy<TVertex, TEdge>, global::QuickGraph.VertexPredicate<TVertex>, int> RemoveVertexIf;

        /// <summary>Stub of property QuickGraph.IHierarchy`2.Root</summary>
        TVertex global::QuickGraph.IHierarchy<TVertex, TEdge>.Root
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates
                  .Func<global::QuickGraph.Stubs.SHierarchy<TVertex, TEdge>, 
                  TVertex> sh = this.RootGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs
                    .IDefaultStub<global::QuickGraph.Stubs.SHierarchy<TVertex, TEdge>> stub
                     = ((global::Microsoft.Stubs
                      .IStub<global::QuickGraph.Stubs.SHierarchy<TVertex, TEdge>>)this)
                      .DefaultStub;
                  return stub.Result<TVertex>(this);
                }
            }
        }

        /// <summary>Stub of method !0 QuickGraph.IHierarchy`2.get_Root()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SHierarchy<TVertex, TEdge>, TVertex> RootGet;

        /// <summary>Stub of method System.Void QuickGraph.IMutableIncidenceGraph`2.TrimEdgeExcess()</summary>
        public global::Microsoft.Stubs.StubDelegates.Action<global::QuickGraph.Stubs.SHierarchy<TVertex, TEdge>> TrimEdgeExcess;

        /// <summary>Event VertexAdded</summary>
        public event global::QuickGraph.VertexAction<TVertex> VertexAdded;

        /// <summary>Stub of property QuickGraph.IVertexSet`1.VertexCount</summary>
        int global::QuickGraph.IVertexSet<TVertex>.VertexCount
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates
                  .Func<global::QuickGraph.Stubs.SHierarchy<TVertex, TEdge>, int> sh
                   = this.VertexCountGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs
                    .IDefaultStub<global::QuickGraph.Stubs.SHierarchy<TVertex, TEdge>> stub
                     = ((global::Microsoft.Stubs
                      .IStub<global::QuickGraph.Stubs.SHierarchy<TVertex, TEdge>>)this)
                      .DefaultStub;
                  return stub.Result<int>(this);
                }
            }
        }

        /// <summary>Stub of method System.Int32 QuickGraph.IVertexSet`1.get_VertexCount()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SHierarchy<TVertex, TEdge>, int> VertexCountGet;

        /// <summary>Event VertexRemoved</summary>
        public event global::QuickGraph.VertexAction<TVertex> VertexRemoved;

        /// <summary>Stub of property QuickGraph.IVertexSet`1.Vertices</summary>
        global::System.Collections.Generic.IEnumerable<TVertex> global::QuickGraph.IVertexSet<TVertex>.Vertices
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates
                  .Func<global::QuickGraph.Stubs.SHierarchy<TVertex, TEdge>, 
                  global::System.Collections.Generic.IEnumerable<TVertex>> sh = this.VerticesGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs
                    .IDefaultStub<global::QuickGraph.Stubs.SHierarchy<TVertex, TEdge>> stub
                     = ((global::Microsoft.Stubs
                      .IStub<global::QuickGraph.Stubs.SHierarchy<TVertex, TEdge>>)this)
                      .DefaultStub;
                  return 
                    stub.Result<global::System.Collections.Generic.IEnumerable<TVertex>>(this);
                }
            }
        }

        /// <summary>Stub of method System.Collections.Generic.IEnumerable`1&lt;!0&gt; QuickGraph.IVertexSet`1.get_Vertices()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SHierarchy<TVertex, TEdge>, global::System.Collections.Generic.IEnumerable<TVertex>> VerticesGet;
    }
}
namespace QuickGraph.Stubs
{
    /// <summary>Stub of IHyperEdge`1</summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Stubs", "0.1.2.3")]
    [global::System.Serializable]
    [global::System.Diagnostics.DebuggerStepThrough]
    public partial class SHyperEdge<TVertex>
      : global::Microsoft.Stubs.StubBase<global::QuickGraph.Stubs.SHyperEdge<TVertex>>
      , global::QuickGraph.IHyperEdge<TVertex>
    {
        /// <summary>Initializes a new instance of type SHyperEdge</summary>
        [global::System.Diagnostics.DebuggerHidden]
        public SHyperEdge()
        {
        }

        /// <summary>Stub of property QuickGraph.IHyperEdge`1.EndPointCount</summary>
        int global::QuickGraph.IHyperEdge<TVertex>.EndPointCount
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates
                  .Func<global::QuickGraph.Stubs.SHyperEdge<TVertex>, int> sh
                   = this.EndPointCountGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs
                    .IDefaultStub<global::QuickGraph.Stubs.SHyperEdge<TVertex>> stub
                     = ((global::Microsoft.Stubs
                      .IStub<global::QuickGraph.Stubs.SHyperEdge<TVertex>>)this).DefaultStub;
                  return stub.Result<int>(this);
                }
            }
        }

        /// <summary>Stub of method System.Int32 QuickGraph.IHyperEdge`1.get_EndPointCount()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SHyperEdge<TVertex>, int> EndPointCountGet;

        /// <summary>Stub of property QuickGraph.IHyperEdge`1.EndPoints</summary>
        global::System.Collections.Generic.IEnumerable<TVertex> global::QuickGraph.IHyperEdge<TVertex>.EndPoints
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs
                  .StubDelegates.Func<global::QuickGraph.Stubs.SHyperEdge<TVertex>, 
                  global::System.Collections.Generic.IEnumerable<TVertex>> sh = this.EndPointsGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs
                    .IDefaultStub<global::QuickGraph.Stubs.SHyperEdge<TVertex>> stub
                     = ((global::Microsoft.Stubs
                      .IStub<global::QuickGraph.Stubs.SHyperEdge<TVertex>>)this).DefaultStub;
                  return 
                    stub.Result<global::System.Collections.Generic.IEnumerable<TVertex>>(this);
                }
            }
        }

        /// <summary>Stub of method System.Collections.Generic.IEnumerable`1&lt;!0&gt; QuickGraph.IHyperEdge`1.get_EndPoints()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SHyperEdge<TVertex>, global::System.Collections.Generic.IEnumerable<TVertex>> EndPointsGet;
    }
}
namespace QuickGraph.Stubs
{
    /// <summary>Stub of IIdentifiable</summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Stubs", "0.1.2.3")]
    [global::System.Serializable]
    [global::System.Diagnostics.DebuggerStepThrough]
    public partial class SIdentifiable
      : global::Microsoft.Stubs.StubBase<global::QuickGraph.Stubs.SIdentifiable>
      , global::QuickGraph.IIdentifiable
    {
        /// <summary>Initializes a new instance of type SIdentifiable</summary>
        [global::System.Diagnostics.DebuggerHidden]
        public SIdentifiable()
        {
        }

        /// <summary>Stub of property QuickGraph.IIdentifiable.ID</summary>
        string global::QuickGraph.IIdentifiable.ID
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs
                  .StubDelegates.Func<global::QuickGraph.Stubs.SIdentifiable, string> sh
                   = this.IDGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs.SIdentifiable> 
                    stub
                     = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs.SIdentifiable>)
                      this).DefaultStub;
                  return stub.Result<string>(this);
                }
            }
        }

        /// <summary>Stub of method System.String QuickGraph.IIdentifiable.get_ID()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SIdentifiable, string> IDGet;
    }
}
namespace QuickGraph.Stubs
{
    /// <summary>Stub of IImplicitGraph`2</summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Stubs", "0.1.2.3")]
    [global::System.Serializable]
    [global::System.Diagnostics.DebuggerStepThrough]
    public partial class SImplicitGraph<TVertex,TEdge>
      : global::Microsoft.Stubs.StubBase<global::QuickGraph.Stubs.SImplicitGraph<TVertex, TEdge>>
      , global::QuickGraph.IImplicitGraph<TVertex, TEdge>
        where TEdge : global::QuickGraph.IEdge<TVertex>
    {
        /// <summary>Initializes a new instance of type SImplicitGraph</summary>
        [global::System.Diagnostics.DebuggerHidden]
        public SImplicitGraph()
        {
        }

        /// <summary>Stub of property QuickGraph.IGraph`2.AllowParallelEdges</summary>
        bool global::QuickGraph.IGraph<TVertex, TEdge>.AllowParallelEdges
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates
                  .Func<global::QuickGraph.Stubs.SImplicitGraph<TVertex, TEdge>, 
                  bool> sh = this.AllowParallelEdgesGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs.IDefaultStub
                      <global::QuickGraph.Stubs.SImplicitGraph<TVertex, TEdge>> stub
                     = ((global::Microsoft.Stubs
                      .IStub<global::QuickGraph.Stubs.SImplicitGraph<TVertex, TEdge>>)this)
                      .DefaultStub;
                  return stub.Result<bool>(this);
                }
            }
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IGraph`2.get_AllowParallelEdges()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SImplicitGraph<TVertex, TEdge>, bool> AllowParallelEdgesGet;

        /// <summary>Stub of property QuickGraph.IGraph`2.IsDirected</summary>
        bool global::QuickGraph.IGraph<TVertex, TEdge>.IsDirected
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates
                  .Func<global::QuickGraph.Stubs.SImplicitGraph<TVertex, TEdge>, 
                  bool> sh = this.IsDirectedGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs.IDefaultStub
                      <global::QuickGraph.Stubs.SImplicitGraph<TVertex, TEdge>> stub
                     = ((global::Microsoft.Stubs
                      .IStub<global::QuickGraph.Stubs.SImplicitGraph<TVertex, TEdge>>)this)
                      .DefaultStub;
                  return stub.Result<bool>(this);
                }
            }
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IGraph`2.get_IsDirected()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SImplicitGraph<TVertex, TEdge>, bool> IsDirectedGet;

        /// <summary>Stub of method System.Boolean QuickGraph.IImplicitGraph`2.IsOutEdgesEmpty(!0 v)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IImplicitGraph<TVertex, TEdge>.IsOutEdgesEmpty(TVertex v)
        {
            global::Microsoft.Stubs.IDefaultStub
                <global::QuickGraph.Stubs.SImplicitGraph<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs
                .IStub<global::QuickGraph.Stubs.SImplicitGraph<TVertex, TEdge>>)this)
                .DefaultStub;
            return stub.Result<bool>(this);
        }

        /// <summary>Stub of method System.Int32 QuickGraph.IImplicitGraph`2.OutDegree(!0 v)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        int global::QuickGraph.IImplicitGraph<TVertex, TEdge>.OutDegree(TVertex v)
        {
            global::Microsoft.Stubs.IDefaultStub
                <global::QuickGraph.Stubs.SImplicitGraph<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs
                .IStub<global::QuickGraph.Stubs.SImplicitGraph<TVertex, TEdge>>)this)
                .DefaultStub;
            return stub.Result<int>(this);
        }

        /// <summary>Stub of method !1 QuickGraph.IImplicitGraph`2.OutEdge(!0 v, System.Int32 index)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        TEdge global::QuickGraph.IImplicitGraph<TVertex, TEdge>.OutEdge(TVertex v, int index)
        {
            global::Microsoft.Stubs.IDefaultStub
                <global::QuickGraph.Stubs.SImplicitGraph<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs
                .IStub<global::QuickGraph.Stubs.SImplicitGraph<TVertex, TEdge>>)this)
                .DefaultStub;
            return stub.Result<TEdge>(this);
        }

        /// <summary>Stub of method System.Collections.Generic.IEnumerable`1&lt;!1&gt; QuickGraph.IImplicitGraph`2.OutEdges(!0 v)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        global::System.Collections.Generic.IEnumerable<TEdge> global::QuickGraph.IImplicitGraph<TVertex, TEdge>.OutEdges(TVertex v)
        {
            global::Microsoft.Stubs.IDefaultStub
                <global::QuickGraph.Stubs.SImplicitGraph<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs
                .IStub<global::QuickGraph.Stubs.SImplicitGraph<TVertex, TEdge>>)this)
                .DefaultStub;
            return stub.Result<global::System.Collections.Generic.IEnumerable<TEdge>>(this);
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IImplicitGraph`2.TryGetOutEdges(!0 v, System.Collections.Generic.IEnumerable`1&lt;!1&gt;&amp; edges)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IImplicitGraph<TVertex, TEdge>.TryGetOutEdges(TVertex v, out global::System.Collections.Generic.IEnumerable<TEdge> edges)
        {
            global::Microsoft.Stubs.IDefaultStub
                <global::QuickGraph.Stubs.SImplicitGraph<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs
                .IStub<global::QuickGraph.Stubs.SImplicitGraph<TVertex, TEdge>>)this)
                .DefaultStub;
            stub.ValueAtReturn<global::System.Collections.Generic.IEnumerable<TEdge>>
                (this, out edges);
            return stub.Result<bool>(this);
        }
    }
}
namespace QuickGraph.Stubs
{
    /// <summary>Stub of IIncidenceGraph`2</summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Stubs", "0.1.2.3")]
    [global::System.Serializable]
    [global::System.Diagnostics.DebuggerStepThrough]
    public partial class SIncidenceGraph<TVertex,TEdge>
      : global::Microsoft.Stubs.StubBase<global::QuickGraph.Stubs.SIncidenceGraph<TVertex, TEdge>>
      , global::QuickGraph.IIncidenceGraph<TVertex, TEdge>
        where TEdge : global::QuickGraph.IEdge<TVertex>
    {
        /// <summary>Initializes a new instance of type SIncidenceGraph</summary>
        [global::System.Diagnostics.DebuggerHidden]
        public SIncidenceGraph()
        {
        }

        /// <summary>Stub of property QuickGraph.IGraph`2.AllowParallelEdges</summary>
        bool global::QuickGraph.IGraph<TVertex, TEdge>.AllowParallelEdges
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates
                  .Func<global::QuickGraph.Stubs.SIncidenceGraph<TVertex, TEdge>, 
                  bool> sh = this.AllowParallelEdgesGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs.IDefaultStub
                      <global::QuickGraph.Stubs.SIncidenceGraph<TVertex, TEdge>> stub
                     = ((global::Microsoft.Stubs.IStub
                        <global::QuickGraph.Stubs.SIncidenceGraph<TVertex, TEdge>>)this)
                      .DefaultStub;
                  return stub.Result<bool>(this);
                }
            }
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IGraph`2.get_AllowParallelEdges()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SIncidenceGraph<TVertex, TEdge>, bool> AllowParallelEdgesGet;

        /// <summary>Stub of property QuickGraph.IGraph`2.IsDirected</summary>
        bool global::QuickGraph.IGraph<TVertex, TEdge>.IsDirected
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates
                  .Func<global::QuickGraph.Stubs.SIncidenceGraph<TVertex, TEdge>, 
                  bool> sh = this.IsDirectedGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs.IDefaultStub
                      <global::QuickGraph.Stubs.SIncidenceGraph<TVertex, TEdge>> stub
                     = ((global::Microsoft.Stubs.IStub
                        <global::QuickGraph.Stubs.SIncidenceGraph<TVertex, TEdge>>)this)
                      .DefaultStub;
                  return stub.Result<bool>(this);
                }
            }
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IGraph`2.get_IsDirected()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SIncidenceGraph<TVertex, TEdge>, bool> IsDirectedGet;

        /// <summary>Stub of method System.Boolean QuickGraph.IImplicitGraph`2.IsOutEdgesEmpty(!0 v)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IImplicitGraph<TVertex, TEdge>.IsOutEdgesEmpty(TVertex v)
        {
            global::Microsoft.Stubs.IDefaultStub
                <global::QuickGraph.Stubs.SIncidenceGraph<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs
                .IStub<global::QuickGraph.Stubs.SIncidenceGraph<TVertex, TEdge>>)this)
                .DefaultStub;
            return stub.Result<bool>(this);
        }

        /// <summary>Stub of method System.Int32 QuickGraph.IImplicitGraph`2.OutDegree(!0 v)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        int global::QuickGraph.IImplicitGraph<TVertex, TEdge>.OutDegree(TVertex v)
        {
            global::Microsoft.Stubs.IDefaultStub
                <global::QuickGraph.Stubs.SIncidenceGraph<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs
                .IStub<global::QuickGraph.Stubs.SIncidenceGraph<TVertex, TEdge>>)this)
                .DefaultStub;
            return stub.Result<int>(this);
        }

        /// <summary>Stub of method !1 QuickGraph.IImplicitGraph`2.OutEdge(!0 v, System.Int32 index)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        TEdge global::QuickGraph.IImplicitGraph<TVertex, TEdge>.OutEdge(TVertex v, int index)
        {
            global::Microsoft.Stubs.IDefaultStub
                <global::QuickGraph.Stubs.SIncidenceGraph<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs
                .IStub<global::QuickGraph.Stubs.SIncidenceGraph<TVertex, TEdge>>)this)
                .DefaultStub;
            return stub.Result<TEdge>(this);
        }

        /// <summary>Stub of method System.Collections.Generic.IEnumerable`1&lt;!1&gt; QuickGraph.IImplicitGraph`2.OutEdges(!0 v)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        global::System.Collections.Generic.IEnumerable<TEdge> global::QuickGraph.IImplicitGraph<TVertex, TEdge>.OutEdges(TVertex v)
        {
            global::Microsoft.Stubs.IDefaultStub
                <global::QuickGraph.Stubs.SIncidenceGraph<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs
                .IStub<global::QuickGraph.Stubs.SIncidenceGraph<TVertex, TEdge>>)this)
                .DefaultStub;
            return stub.Result<global::System.Collections.Generic.IEnumerable<TEdge>>(this);
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IImplicitGraph`2.TryGetOutEdges(!0 v, System.Collections.Generic.IEnumerable`1&lt;!1&gt;&amp; edges)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IImplicitGraph<TVertex, TEdge>.TryGetOutEdges(TVertex v, out global::System.Collections.Generic.IEnumerable<TEdge> edges)
        {
            global::Microsoft.Stubs.IDefaultStub
                <global::QuickGraph.Stubs.SIncidenceGraph<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs
                .IStub<global::QuickGraph.Stubs.SIncidenceGraph<TVertex, TEdge>>)this)
                .DefaultStub;
            stub.ValueAtReturn<global::System.Collections.Generic.IEnumerable<TEdge>>
                (this, out edges);
            return stub.Result<bool>(this);
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IIncidenceGraph`2.ContainsEdge(!0 source, !0 target)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IIncidenceGraph<TVertex, TEdge>.ContainsEdge(TVertex source, TVertex target)
        {
            global::Microsoft.Stubs.IDefaultStub
                <global::QuickGraph.Stubs.SIncidenceGraph<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs
                .IStub<global::QuickGraph.Stubs.SIncidenceGraph<TVertex, TEdge>>)this)
                .DefaultStub;
            return stub.Result<bool>(this);
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IIncidenceGraph`2.TryGetEdge(!0 source, !0 target, !1&amp; edge)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IIncidenceGraph<TVertex, TEdge>.TryGetEdge(
            TVertex source,
            TVertex target,
            out TEdge edge
        )
        {
            global::Microsoft.Stubs.IDefaultStub
                <global::QuickGraph.Stubs.SIncidenceGraph<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs
                .IStub<global::QuickGraph.Stubs.SIncidenceGraph<TVertex, TEdge>>)this)
                .DefaultStub;
            stub.ValueAtReturn<TEdge>(this, out edge);
            return stub.Result<bool>(this);
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IIncidenceGraph`2.TryGetEdges(!0 source, !0 target, System.Collections.Generic.IEnumerable`1&lt;!1&gt;&amp; edges)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IIncidenceGraph<TVertex, TEdge>.TryGetEdges(
            TVertex source,
            TVertex target,
            out global::System.Collections.Generic.IEnumerable<TEdge> edges
        )
        {
            global::Microsoft.Stubs.IDefaultStub
                <global::QuickGraph.Stubs.SIncidenceGraph<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs
                .IStub<global::QuickGraph.Stubs.SIncidenceGraph<TVertex, TEdge>>)this)
                .DefaultStub;
            stub.ValueAtReturn<global::System.Collections.Generic.IEnumerable<TEdge>>
                (this, out edges);
            return stub.Result<bool>(this);
        }
    }
}
namespace QuickGraph.Algorithms.RandomWalks.Stubs
{
    /// <summary>Stub of IMarkovEdgeChain`2</summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Stubs", "0.1.2.3")]
    [global::System.Serializable]
    [global::System.Diagnostics.DebuggerStepThrough]
    public partial class SMarkovEdgeChain<TVertex,TEdge>
      : global::Microsoft.Stubs.StubBase<global::QuickGraph.Algorithms.RandomWalks.Stubs.SMarkovEdgeChain<TVertex, TEdge>>
      , global::QuickGraph.Algorithms.RandomWalks.IMarkovEdgeChain<TVertex, TEdge>
        where TEdge : global::QuickGraph.IEdge<TVertex>
    {
        /// <summary>Initializes a new instance of type SMarkovEdgeChain</summary>
        [global::System.Diagnostics.DebuggerHidden]
        public SMarkovEdgeChain()
        {
        }

        /// <summary>Stub of method !1 QuickGraph.Algorithms.RandomWalks.IEdgeChain`2.Successor(QuickGraph.IImplicitGraph`2&lt;!0,!1&gt; g, !0 u)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        TEdge global::QuickGraph.Algorithms.RandomWalks.IEdgeChain<TVertex, TEdge>.Successor(global::QuickGraph.IImplicitGraph<TVertex, TEdge> g, TVertex u)
        {
            global::Microsoft.Stubs
              .IDefaultStub<global::QuickGraph.Algorithms.RandomWalks.Stubs
                .SMarkovEdgeChain<TVertex, TEdge>> stub = ((global::Microsoft.Stubs
              .IStub<global::QuickGraph.Algorithms.RandomWalks.Stubs
                .SMarkovEdgeChain<TVertex, TEdge>>)this).DefaultStub;
            return stub.Result<TEdge>(this);
        }

        /// <summary>Stub of property QuickGraph.Algorithms.RandomWalks.IMarkovEdgeChain`2.Rand</summary>
        global::System.Random global::QuickGraph.Algorithms.RandomWalks.IMarkovEdgeChain<TVertex, TEdge>.Rand
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs
                  .StubDelegates.Func<global::QuickGraph.Algorithms.RandomWalks.Stubs
                    .SMarkovEdgeChain<TVertex, TEdge>, global::System.Random> sh = this.RandGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs
                    .IDefaultStub<global::QuickGraph.Algorithms.RandomWalks.Stubs
                      .SMarkovEdgeChain<TVertex, TEdge>> stub = ((global::Microsoft.Stubs
                    .IStub<global::QuickGraph.Algorithms.RandomWalks.Stubs
                      .SMarkovEdgeChain<TVertex, TEdge>>)this).DefaultStub;
                  return stub.Result<global::System.Random>(this);
                }
            }
            [global::System.Diagnostics.DebuggerHidden]
            set
            {
                global::Microsoft.Stubs
                  .StubDelegates.Action<global::QuickGraph.Algorithms.RandomWalks.Stubs
                    .SMarkovEdgeChain<TVertex, TEdge>, global::System.Random> sh = this.RandSet;
                if ((object)sh != (object)null)
                  sh.Invoke(this, value);
                else 
                {
                  global::Microsoft.Stubs
                    .IDefaultStub<global::QuickGraph.Algorithms.RandomWalks.Stubs
                      .SMarkovEdgeChain<TVertex, TEdge>> stub = ((global::Microsoft.Stubs
                    .IStub<global::QuickGraph.Algorithms.RandomWalks.Stubs
                      .SMarkovEdgeChain<TVertex, TEdge>>)this).DefaultStub;
                  stub.VoidResult(this);
                }
            }
        }

        /// <summary>Stub of method System.Random QuickGraph.Algorithms.RandomWalks.IMarkovEdgeChain`2.get_Rand()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Algorithms.RandomWalks.Stubs.SMarkovEdgeChain<TVertex, TEdge>, global::System.Random> RandGet;

        /// <summary>Stub of method System.Void QuickGraph.Algorithms.RandomWalks.IMarkovEdgeChain`2.set_Rand(System.Random value)</summary>
        public global::Microsoft.Stubs.StubDelegates.Action<global::QuickGraph.Algorithms.RandomWalks.Stubs.SMarkovEdgeChain<TVertex, TEdge>, global::System.Random> RandSet;
    }
}
namespace QuickGraph.Algorithms.MinimumSpanningTree.Stubs
{
    /// <summary>Stub of IMinimumSpanningTreeAlgorithm`2</summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Stubs", "0.1.2.3")]
    [global::System.Serializable]
    [global::System.Diagnostics.DebuggerStepThrough]
    public partial class SMinimumSpanningTreeAlgorithm<TVertex,TEdge>
      : global::Microsoft.Stubs.StubBase<global::QuickGraph.Algorithms.MinimumSpanningTree.Stubs.SMinimumSpanningTreeAlgorithm<TVertex, TEdge>>
      , global::QuickGraph.Algorithms.MinimumSpanningTree.IMinimumSpanningTreeAlgorithm<TVertex, TEdge>
        where TEdge : global::QuickGraph.IEdge<TVertex>
    {
        /// <summary>Initializes a new instance of type SMinimumSpanningTreeAlgorithm</summary>
        [global::System.Diagnostics.DebuggerHidden]
        public SMinimumSpanningTreeAlgorithm()
        {
        }

        /// <summary>Stub of method System.Void QuickGraph.Algorithms.IComputation.Abort()</summary>
        public global::Microsoft.Stubs.StubDelegates.Action<global::QuickGraph.Algorithms.MinimumSpanningTree.Stubs.SMinimumSpanningTreeAlgorithm<TVertex, TEdge>> Abort;

        /// <summary>Event Aborted</summary>
        public event global::System.EventHandler Aborted;

        /// <summary>Stub of method System.Void QuickGraph.Algorithms.IComputation.Compute()</summary>
        public global::Microsoft.Stubs.StubDelegates.Action<global::QuickGraph.Algorithms.MinimumSpanningTree.Stubs.SMinimumSpanningTreeAlgorithm<TVertex, TEdge>> Compute;

        /// <summary>Event Finished</summary>
        public event global::System.EventHandler Finished;

        /// <summary>Stub of method System.Void QuickGraph.Algorithms.IComputation.Abort()</summary>
        [global::System.Diagnostics.DebuggerHidden]
        void global::QuickGraph.Algorithms.IComputation.Abort()
        {
            global::Microsoft.Stubs.StubDelegates
              .Action<global::QuickGraph.Algorithms.MinimumSpanningTree.Stubs
                .SMinimumSpanningTreeAlgorithm<TVertex, TEdge>> sh = this.Abort;
            if ((object)sh != (object)null)
              sh.Invoke(this);
            else 
            {
              global::Microsoft.Stubs.IDefaultStub
                  <global::QuickGraph.Algorithms.MinimumSpanningTree.Stubs
                    .SMinimumSpanningTreeAlgorithm<TVertex, TEdge>> stub
                 = ((global::Microsoft.Stubs
                  .IStub<global::QuickGraph.Algorithms.MinimumSpanningTree.Stubs
                    .SMinimumSpanningTreeAlgorithm<TVertex, TEdge>>)this).DefaultStub;
              stub.VoidResult(this);
            }
        }

        /// <summary>Stub of method System.Void QuickGraph.Algorithms.IComputation.Compute()</summary>
        [global::System.Diagnostics.DebuggerHidden]
        void global::QuickGraph.Algorithms.IComputation.Compute()
        {
            global::Microsoft.Stubs.StubDelegates
              .Action<global::QuickGraph.Algorithms.MinimumSpanningTree.Stubs
                .SMinimumSpanningTreeAlgorithm<TVertex, TEdge>> sh = this.Compute;
            if ((object)sh != (object)null)
              sh.Invoke(this);
            else 
            {
              global::Microsoft.Stubs.IDefaultStub
                  <global::QuickGraph.Algorithms.MinimumSpanningTree.Stubs
                    .SMinimumSpanningTreeAlgorithm<TVertex, TEdge>> stub
                 = ((global::Microsoft.Stubs
                  .IStub<global::QuickGraph.Algorithms.MinimumSpanningTree.Stubs
                    .SMinimumSpanningTreeAlgorithm<TVertex, TEdge>>)this).DefaultStub;
              stub.VoidResult(this);
            }
        }

        /// <summary>Event Started</summary>
        public event global::System.EventHandler Started;

        /// <summary>Stub of property QuickGraph.Algorithms.IComputation.State</summary>
        global::QuickGraph.Algorithms.ComputationState global::QuickGraph.Algorithms.IComputation.State
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates
                  .Func<global::QuickGraph.Algorithms.MinimumSpanningTree.Stubs
                    .SMinimumSpanningTreeAlgorithm<TVertex, TEdge>, 
                  global::QuickGraph.Algorithms.ComputationState> sh = this.StateGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs.IDefaultStub
                      <global::QuickGraph.Algorithms.MinimumSpanningTree.Stubs
                        .SMinimumSpanningTreeAlgorithm<TVertex, TEdge>> stub
                     = ((global::Microsoft.Stubs
                      .IStub<global::QuickGraph.Algorithms.MinimumSpanningTree.Stubs
                        .SMinimumSpanningTreeAlgorithm<TVertex, TEdge>>)this).DefaultStub;
                  return stub.Result<global::QuickGraph.Algorithms.ComputationState>(this);
                }
            }
        }

        /// <summary>Event StateChanged</summary>
        public event global::System.EventHandler StateChanged;

        /// <summary>Stub of method QuickGraph.Algorithms.ComputationState QuickGraph.Algorithms.IComputation.get_State()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Algorithms.MinimumSpanningTree.Stubs.SMinimumSpanningTreeAlgorithm<TVertex, TEdge>, global::QuickGraph.Algorithms.ComputationState> StateGet;

        /// <summary>Stub of property QuickGraph.Algorithms.IComputation.SyncRoot</summary>
        object global::QuickGraph.Algorithms.IComputation.SyncRoot
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates
                  .Func<global::QuickGraph.Algorithms.MinimumSpanningTree.Stubs
                    .SMinimumSpanningTreeAlgorithm<TVertex, TEdge>, object> sh = this.SyncRootGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs.IDefaultStub
                      <global::QuickGraph.Algorithms.MinimumSpanningTree.Stubs
                        .SMinimumSpanningTreeAlgorithm<TVertex, TEdge>> stub
                     = ((global::Microsoft.Stubs
                      .IStub<global::QuickGraph.Algorithms.MinimumSpanningTree.Stubs
                        .SMinimumSpanningTreeAlgorithm<TVertex, TEdge>>)this).DefaultStub;
                  return stub.Result<object>(this);
                }
            }
        }

        /// <summary>Stub of method System.Object QuickGraph.Algorithms.IComputation.get_SyncRoot()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Algorithms.MinimumSpanningTree.Stubs.SMinimumSpanningTreeAlgorithm<TVertex, TEdge>, object> SyncRootGet;

        /// <summary>Event TreeEdge</summary>
        public event global::QuickGraph.EdgeAction<TVertex, TEdge> TreeEdge;

        /// <summary>Stub of property QuickGraph.Algorithms.IAlgorithm`1.VisitedGraph</summary>
        global::QuickGraph.IUndirectedGraph<TVertex, TEdge> global::QuickGraph.Algorithms.IAlgorithm<global::QuickGraph.IUndirectedGraph<TVertex, TEdge>>.VisitedGraph
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates
                  .Func<global::QuickGraph.Algorithms.MinimumSpanningTree.Stubs
                    .SMinimumSpanningTreeAlgorithm<TVertex, TEdge>, 
                  global::QuickGraph.IUndirectedGraph<TVertex, TEdge>> sh = this.VisitedGraphGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs.IDefaultStub
                      <global::QuickGraph.Algorithms.MinimumSpanningTree.Stubs
                        .SMinimumSpanningTreeAlgorithm<TVertex, TEdge>> stub
                     = ((global::Microsoft.Stubs
                      .IStub<global::QuickGraph.Algorithms.MinimumSpanningTree.Stubs
                        .SMinimumSpanningTreeAlgorithm<TVertex, TEdge>>)this).DefaultStub;
                  return stub.Result<global::QuickGraph.IUndirectedGraph<TVertex, TEdge>>(this);
                }
            }
        }

        /// <summary>Stub of method !0 QuickGraph.Algorithms.IAlgorithm`1.get_VisitedGraph()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Algorithms.MinimumSpanningTree.Stubs.SMinimumSpanningTreeAlgorithm<TVertex, TEdge>, global::QuickGraph.IUndirectedGraph<TVertex, TEdge>> VisitedGraphGet;
    }
}
namespace QuickGraph.Stubs
{
    /// <summary>Stub of IMutableBidirectionalGraph`2</summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Stubs", "0.1.2.3")]
    [global::System.Serializable]
    [global::System.Diagnostics.DebuggerStepThrough]
    public partial class SMutableBidirectionalGraph<TVertex,TEdge>
      : global::Microsoft.Stubs.StubBase<global::QuickGraph.Stubs.SMutableBidirectionalGraph<TVertex, TEdge>>
      , global::QuickGraph.IMutableBidirectionalGraph<TVertex, TEdge>
        where TEdge : global::QuickGraph.IEdge<TVertex>
    {
        /// <summary>Initializes a new instance of type SMutableBidirectionalGraph</summary>
        [global::System.Diagnostics.DebuggerHidden]
        public SMutableBidirectionalGraph()
        {
        }

        /// <summary>Stub of method System.Int32 QuickGraph.IMutableEdgeListGraph`2.AddEdgeRange(System.Collections.Generic.IEnumerable`1&lt;!1&gt; edges)</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SMutableBidirectionalGraph<TVertex, TEdge>, global::System.Collections.Generic.IEnumerable<TEdge>, int> AddEdgeRange;

        /// <summary>Stub of method System.Int32 QuickGraph.IMutableVertexSet`1.AddVertexRange(System.Collections.Generic.IEnumerable`1&lt;!0&gt; vertices)</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SMutableBidirectionalGraph<TVertex, TEdge>, global::System.Collections.Generic.IEnumerable<TVertex>, int> AddVertexRange;

        /// <summary>Stub of method System.Int32 QuickGraph.IMutableVertexAndEdgeSet`2.AddVerticesAndEdgeRange(System.Collections.Generic.IEnumerable`1&lt;!1&gt; edges)</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SMutableBidirectionalGraph<TVertex, TEdge>, global::System.Collections.Generic.IEnumerable<TEdge>, int> AddVerticesAndEdgeRange;

        /// <summary>Stub of property QuickGraph.IGraph`2.AllowParallelEdges</summary>
        bool global::QuickGraph.IGraph<TVertex, TEdge>.AllowParallelEdges
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs
                  .SMutableBidirectionalGraph<TVertex, TEdge>, bool> sh
                   = this.AllowParallelEdgesGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
                    .SMutableBidirectionalGraph<TVertex, TEdge>> stub
                     = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                      .SMutableBidirectionalGraph<TVertex, TEdge>>)this).DefaultStub;
                  return stub.Result<bool>(this);
                }
            }
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IGraph`2.get_AllowParallelEdges()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SMutableBidirectionalGraph<TVertex, TEdge>, bool> AllowParallelEdgesGet;

        /// <summary>Stub of method System.Void QuickGraph.IMutableGraph`2.Clear()</summary>
        public global::Microsoft.Stubs.StubDelegates.Action<global::QuickGraph.Stubs.SMutableBidirectionalGraph<TVertex, TEdge>> Clear;

        /// <summary>Event EdgeAdded</summary>
        public event global::QuickGraph.EdgeAction<TVertex, TEdge> EdgeAdded;

        /// <summary>Stub of property QuickGraph.IEdgeSet`2.EdgeCount</summary>
        int global::QuickGraph.IEdgeSet<TVertex, TEdge>.EdgeCount
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs
                  .SMutableBidirectionalGraph<TVertex, TEdge>, int> sh = this.EdgeCountGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
                    .SMutableBidirectionalGraph<TVertex, TEdge>> stub
                     = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                      .SMutableBidirectionalGraph<TVertex, TEdge>>)this).DefaultStub;
                  return stub.Result<int>(this);
                }
            }
        }

        /// <summary>Stub of method System.Int32 QuickGraph.IEdgeSet`2.get_EdgeCount()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SMutableBidirectionalGraph<TVertex, TEdge>, int> EdgeCountGet;

        /// <summary>Event EdgeRemoved</summary>
        public event global::QuickGraph.EdgeAction<TVertex, TEdge> EdgeRemoved;

        /// <summary>Stub of property QuickGraph.IEdgeSet`2.Edges</summary>
        global::System.Collections.Generic.IEnumerable<TEdge> global::QuickGraph.IEdgeSet<TVertex, TEdge>.Edges
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs
                  .SMutableBidirectionalGraph<TVertex, TEdge>, 
                global::System.Collections.Generic.IEnumerable<TEdge>> sh = this.EdgesGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
                    .SMutableBidirectionalGraph<TVertex, TEdge>> stub
                     = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                      .SMutableBidirectionalGraph<TVertex, TEdge>>)this).DefaultStub;
                  return 
                    stub.Result<global::System.Collections.Generic.IEnumerable<TEdge>>(this);
                }
            }
        }

        /// <summary>Stub of method System.Collections.Generic.IEnumerable`1&lt;!1&gt; QuickGraph.IEdgeSet`2.get_Edges()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SMutableBidirectionalGraph<TVertex, TEdge>, global::System.Collections.Generic.IEnumerable<TEdge>> EdgesGet;

        /// <summary>Stub of property QuickGraph.IGraph`2.IsDirected</summary>
        bool global::QuickGraph.IGraph<TVertex, TEdge>.IsDirected
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs
                  .SMutableBidirectionalGraph<TVertex, TEdge>, bool> sh = this.IsDirectedGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
                    .SMutableBidirectionalGraph<TVertex, TEdge>> stub
                     = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                      .SMutableBidirectionalGraph<TVertex, TEdge>>)this).DefaultStub;
                  return stub.Result<bool>(this);
                }
            }
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IGraph`2.get_IsDirected()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SMutableBidirectionalGraph<TVertex, TEdge>, bool> IsDirectedGet;

        /// <summary>Stub of property QuickGraph.IEdgeSet`2.IsEdgesEmpty</summary>
        bool global::QuickGraph.IEdgeSet<TVertex, TEdge>.IsEdgesEmpty
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs
                  .SMutableBidirectionalGraph<TVertex, TEdge>, bool> sh = this.IsEdgesEmptyGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
                    .SMutableBidirectionalGraph<TVertex, TEdge>> stub
                     = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                      .SMutableBidirectionalGraph<TVertex, TEdge>>)this).DefaultStub;
                  return stub.Result<bool>(this);
                }
            }
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IEdgeSet`2.get_IsEdgesEmpty()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SMutableBidirectionalGraph<TVertex, TEdge>, bool> IsEdgesEmptyGet;

        /// <summary>Stub of property QuickGraph.IVertexSet`1.IsVerticesEmpty</summary>
        bool global::QuickGraph.IVertexSet<TVertex>.IsVerticesEmpty
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs
                  .SMutableBidirectionalGraph<TVertex, TEdge>, bool> sh = this.IsVerticesEmptyGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
                    .SMutableBidirectionalGraph<TVertex, TEdge>> stub
                     = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                      .SMutableBidirectionalGraph<TVertex, TEdge>>)this).DefaultStub;
                  return stub.Result<bool>(this);
                }
            }
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IVertexSet`1.get_IsVerticesEmpty()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SMutableBidirectionalGraph<TVertex, TEdge>, bool> IsVerticesEmptyGet;

        /// <summary>Stub of method System.Int32 QuickGraph.IBidirectionalGraph`2.Degree(!0 v)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        int global::QuickGraph.IBidirectionalGraph<TVertex, TEdge>.Degree(TVertex v)
        {
            global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
              .SMutableBidirectionalGraph<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                .SMutableBidirectionalGraph<TVertex, TEdge>>)this).DefaultStub;
            return stub.Result<int>(this);
        }

        /// <summary>Stub of method System.Int32 QuickGraph.IBidirectionalGraph`2.InDegree(!0 v)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        int global::QuickGraph.IBidirectionalGraph<TVertex, TEdge>.InDegree(TVertex v)
        {
            global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
              .SMutableBidirectionalGraph<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                .SMutableBidirectionalGraph<TVertex, TEdge>>)this).DefaultStub;
            return stub.Result<int>(this);
        }

        /// <summary>Stub of method !1 QuickGraph.IBidirectionalGraph`2.InEdge(!0 v, System.Int32 index)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        TEdge global::QuickGraph.IBidirectionalGraph<TVertex, TEdge>.InEdge(TVertex v, int index)
        {
            global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
              .SMutableBidirectionalGraph<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                .SMutableBidirectionalGraph<TVertex, TEdge>>)this).DefaultStub;
            return stub.Result<TEdge>(this);
        }

        /// <summary>Stub of method System.Collections.Generic.IEnumerable`1&lt;!1&gt; QuickGraph.IBidirectionalGraph`2.InEdges(!0 v)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        global::System.Collections.Generic.IEnumerable<TEdge> global::QuickGraph.IBidirectionalGraph<TVertex, TEdge>.InEdges(TVertex v)
        {
            global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
              .SMutableBidirectionalGraph<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                .SMutableBidirectionalGraph<TVertex, TEdge>>)this).DefaultStub;
            return stub.Result<global::System.Collections.Generic.IEnumerable<TEdge>>(this);
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IBidirectionalGraph`2.IsInEdgesEmpty(!0 v)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IBidirectionalGraph<TVertex, TEdge>.IsInEdgesEmpty(TVertex v)
        {
            global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
              .SMutableBidirectionalGraph<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                .SMutableBidirectionalGraph<TVertex, TEdge>>)this).DefaultStub;
            return stub.Result<bool>(this);
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IBidirectionalGraph`2.TryGetInEdges(!0 v, System.Collections.Generic.IEnumerable`1&lt;!1&gt;&amp; edges)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IBidirectionalGraph<TVertex, TEdge>.TryGetInEdges(TVertex v, out global::System.Collections.Generic.IEnumerable<TEdge> edges)
        {
            global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
              .SMutableBidirectionalGraph<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                .SMutableBidirectionalGraph<TVertex, TEdge>>)this).DefaultStub;
            stub.ValueAtReturn<global::System.Collections.Generic.IEnumerable<TEdge>>
                (this, out edges);
            return stub.Result<bool>(this);
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IEdgeSet`2.ContainsEdge(!1 edge)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IEdgeSet<TVertex, TEdge>.ContainsEdge(TEdge edge)
        {
            global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
              .SMutableBidirectionalGraph<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                .SMutableBidirectionalGraph<TVertex, TEdge>>)this).DefaultStub;
            return stub.Result<bool>(this);
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IImplicitGraph`2.IsOutEdgesEmpty(!0 v)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IImplicitGraph<TVertex, TEdge>.IsOutEdgesEmpty(TVertex v)
        {
            global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
              .SMutableBidirectionalGraph<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                .SMutableBidirectionalGraph<TVertex, TEdge>>)this).DefaultStub;
            return stub.Result<bool>(this);
        }

        /// <summary>Stub of method System.Int32 QuickGraph.IImplicitGraph`2.OutDegree(!0 v)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        int global::QuickGraph.IImplicitGraph<TVertex, TEdge>.OutDegree(TVertex v)
        {
            global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
              .SMutableBidirectionalGraph<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                .SMutableBidirectionalGraph<TVertex, TEdge>>)this).DefaultStub;
            return stub.Result<int>(this);
        }

        /// <summary>Stub of method !1 QuickGraph.IImplicitGraph`2.OutEdge(!0 v, System.Int32 index)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        TEdge global::QuickGraph.IImplicitGraph<TVertex, TEdge>.OutEdge(TVertex v, int index)
        {
            global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
              .SMutableBidirectionalGraph<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                .SMutableBidirectionalGraph<TVertex, TEdge>>)this).DefaultStub;
            return stub.Result<TEdge>(this);
        }

        /// <summary>Stub of method System.Collections.Generic.IEnumerable`1&lt;!1&gt; QuickGraph.IImplicitGraph`2.OutEdges(!0 v)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        global::System.Collections.Generic.IEnumerable<TEdge> global::QuickGraph.IImplicitGraph<TVertex, TEdge>.OutEdges(TVertex v)
        {
            global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
              .SMutableBidirectionalGraph<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                .SMutableBidirectionalGraph<TVertex, TEdge>>)this).DefaultStub;
            return stub.Result<global::System.Collections.Generic.IEnumerable<TEdge>>(this);
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IImplicitGraph`2.TryGetOutEdges(!0 v, System.Collections.Generic.IEnumerable`1&lt;!1&gt;&amp; edges)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IImplicitGraph<TVertex, TEdge>.TryGetOutEdges(TVertex v, out global::System.Collections.Generic.IEnumerable<TEdge> edges)
        {
            global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
              .SMutableBidirectionalGraph<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                .SMutableBidirectionalGraph<TVertex, TEdge>>)this).DefaultStub;
            stub.ValueAtReturn<global::System.Collections.Generic.IEnumerable<TEdge>>
                (this, out edges);
            return stub.Result<bool>(this);
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IIncidenceGraph`2.ContainsEdge(!0 source, !0 target)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IIncidenceGraph<TVertex, TEdge>.ContainsEdge(TVertex source, TVertex target)
        {
            global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
              .SMutableBidirectionalGraph<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                .SMutableBidirectionalGraph<TVertex, TEdge>>)this).DefaultStub;
            return stub.Result<bool>(this);
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IIncidenceGraph`2.TryGetEdge(!0 source, !0 target, !1&amp; edge)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IIncidenceGraph<TVertex, TEdge>.TryGetEdge(
            TVertex source,
            TVertex target,
            out TEdge edge
        )
        {
            global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
              .SMutableBidirectionalGraph<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                .SMutableBidirectionalGraph<TVertex, TEdge>>)this).DefaultStub;
            stub.ValueAtReturn<TEdge>(this, out edge);
            return stub.Result<bool>(this);
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IIncidenceGraph`2.TryGetEdges(!0 source, !0 target, System.Collections.Generic.IEnumerable`1&lt;!1&gt;&amp; edges)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IIncidenceGraph<TVertex, TEdge>.TryGetEdges(
            TVertex source,
            TVertex target,
            out global::System.Collections.Generic.IEnumerable<TEdge> edges
        )
        {
            global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
              .SMutableBidirectionalGraph<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                .SMutableBidirectionalGraph<TVertex, TEdge>>)this).DefaultStub;
            stub.ValueAtReturn<global::System.Collections.Generic.IEnumerable<TEdge>>
                (this, out edges);
            return stub.Result<bool>(this);
        }

        /// <summary>Stub of method System.Void QuickGraph.IMutableBidirectionalGraph`2.ClearEdges(!0 v)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        void global::QuickGraph.IMutableBidirectionalGraph<TVertex, TEdge>.ClearEdges(TVertex v)
        {
            global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
              .SMutableBidirectionalGraph<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                .SMutableBidirectionalGraph<TVertex, TEdge>>)this).DefaultStub;
            stub.VoidResult(this);
        }

        /// <summary>Stub of method System.Void QuickGraph.IMutableBidirectionalGraph`2.ClearInEdges(!0 v)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        void global::QuickGraph.IMutableBidirectionalGraph<TVertex, TEdge>.ClearInEdges(TVertex v)
        {
            global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
              .SMutableBidirectionalGraph<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                .SMutableBidirectionalGraph<TVertex, TEdge>>)this).DefaultStub;
            stub.VoidResult(this);
        }

        /// <summary>Stub of method System.Int32 QuickGraph.IMutableBidirectionalGraph`2.RemoveInEdgeIf(!0 v, QuickGraph.EdgePredicate`2&lt;!0,!1&gt; edgePredicate)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        int global::QuickGraph.IMutableBidirectionalGraph<TVertex, TEdge>.RemoveInEdgeIf(TVertex v, global::QuickGraph.EdgePredicate<TVertex, TEdge> edgePredicate)
        {
            global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
              .SMutableBidirectionalGraph<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                .SMutableBidirectionalGraph<TVertex, TEdge>>)this).DefaultStub;
            return stub.Result<int>(this);
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IMutableEdgeListGraph`2.AddEdge(!1 edge)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IMutableEdgeListGraph<TVertex, TEdge>.AddEdge(TEdge edge)
        {
            global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
              .SMutableBidirectionalGraph<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                .SMutableBidirectionalGraph<TVertex, TEdge>>)this).DefaultStub;
            return stub.Result<bool>(this);
        }

        /// <summary>Stub of method System.Int32 QuickGraph.IMutableEdgeListGraph`2.AddEdgeRange(System.Collections.Generic.IEnumerable`1&lt;!1&gt; edges)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        int global::QuickGraph.IMutableEdgeListGraph<TVertex, TEdge>.AddEdgeRange(global::System.Collections.Generic.IEnumerable<TEdge> edges)
        {
            global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs
              .SMutableBidirectionalGraph<TVertex, TEdge>, 
            global::System.Collections.Generic.IEnumerable<TEdge>, int> sh
               = this.AddEdgeRange;
            if ((object)sh != (object)null)
              return sh.Invoke(this, edges);
            else 
            {
              global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
                .SMutableBidirectionalGraph<TVertex, TEdge>> stub
                 = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                  .SMutableBidirectionalGraph<TVertex, TEdge>>)this).DefaultStub;
              return stub.Result<int>(this);
            }
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IMutableEdgeListGraph`2.RemoveEdge(!1 edge)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IMutableEdgeListGraph<TVertex, TEdge>.RemoveEdge(TEdge edge)
        {
            global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
              .SMutableBidirectionalGraph<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                .SMutableBidirectionalGraph<TVertex, TEdge>>)this).DefaultStub;
            return stub.Result<bool>(this);
        }

        /// <summary>Stub of method System.Int32 QuickGraph.IMutableEdgeListGraph`2.RemoveEdgeIf(QuickGraph.EdgePredicate`2&lt;!0,!1&gt; predicate)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        int global::QuickGraph.IMutableEdgeListGraph<TVertex, TEdge>.RemoveEdgeIf(global::QuickGraph.EdgePredicate<TVertex, TEdge> predicate)
        {
            global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs
              .SMutableBidirectionalGraph<TVertex, TEdge>, 
            global::QuickGraph.EdgePredicate<TVertex, TEdge>, int> sh = this.RemoveEdgeIf;
            if ((object)sh != (object)null)
              return sh.Invoke(this, predicate);
            else 
            {
              global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
                .SMutableBidirectionalGraph<TVertex, TEdge>> stub
                 = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                  .SMutableBidirectionalGraph<TVertex, TEdge>>)this).DefaultStub;
              return stub.Result<int>(this);
            }
        }

        /// <summary>Stub of method System.Void QuickGraph.IMutableGraph`2.Clear()</summary>
        [global::System.Diagnostics.DebuggerHidden]
        void global::QuickGraph.IMutableGraph<TVertex, TEdge>.Clear()
        {
            global::Microsoft.Stubs.StubDelegates.Action<global::QuickGraph.Stubs
              .SMutableBidirectionalGraph<TVertex, TEdge>> sh = this.Clear;
            if ((object)sh != (object)null)
              sh.Invoke(this);
            else 
            {
              global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
                .SMutableBidirectionalGraph<TVertex, TEdge>> stub
                 = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                  .SMutableBidirectionalGraph<TVertex, TEdge>>)this).DefaultStub;
              stub.VoidResult(this);
            }
        }

        /// <summary>Stub of method System.Void QuickGraph.IMutableIncidenceGraph`2.ClearOutEdges(!0 v)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        void global::QuickGraph.IMutableIncidenceGraph<TVertex, TEdge>.ClearOutEdges(TVertex v)
        {
            global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
              .SMutableBidirectionalGraph<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                .SMutableBidirectionalGraph<TVertex, TEdge>>)this).DefaultStub;
            stub.VoidResult(this);
        }

        /// <summary>Stub of method System.Int32 QuickGraph.IMutableIncidenceGraph`2.RemoveOutEdgeIf(!0 v, QuickGraph.EdgePredicate`2&lt;!0,!1&gt; predicate)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        int global::QuickGraph.IMutableIncidenceGraph<TVertex, TEdge>.RemoveOutEdgeIf(TVertex v, global::QuickGraph.EdgePredicate<TVertex, TEdge> predicate)
        {
            global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
              .SMutableBidirectionalGraph<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                .SMutableBidirectionalGraph<TVertex, TEdge>>)this).DefaultStub;
            return stub.Result<int>(this);
        }

        /// <summary>Stub of method System.Void QuickGraph.IMutableIncidenceGraph`2.TrimEdgeExcess()</summary>
        [global::System.Diagnostics.DebuggerHidden]
        void global::QuickGraph.IMutableIncidenceGraph<TVertex, TEdge>.TrimEdgeExcess()
        {
            global::Microsoft.Stubs.StubDelegates.Action<global::QuickGraph.Stubs
              .SMutableBidirectionalGraph<TVertex, TEdge>> sh = this.TrimEdgeExcess;
            if ((object)sh != (object)null)
              sh.Invoke(this);
            else 
            {
              global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
                .SMutableBidirectionalGraph<TVertex, TEdge>> stub
                 = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                  .SMutableBidirectionalGraph<TVertex, TEdge>>)this).DefaultStub;
              stub.VoidResult(this);
            }
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IMutableVertexAndEdgeSet`2.AddVerticesAndEdge(!1 edge)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IMutableVertexAndEdgeSet<TVertex, TEdge>.AddVerticesAndEdge(TEdge edge)
        {
            global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
              .SMutableBidirectionalGraph<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                .SMutableBidirectionalGraph<TVertex, TEdge>>)this).DefaultStub;
            return stub.Result<bool>(this);
        }

        /// <summary>Stub of method System.Int32 QuickGraph.IMutableVertexAndEdgeSet`2.AddVerticesAndEdgeRange(System.Collections.Generic.IEnumerable`1&lt;!1&gt; edges)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        int global::QuickGraph.IMutableVertexAndEdgeSet<TVertex, TEdge>.AddVerticesAndEdgeRange(global::System.Collections.Generic.IEnumerable<TEdge> edges)
        {
            global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs
              .SMutableBidirectionalGraph<TVertex, TEdge>, 
            global::System.Collections.Generic.IEnumerable<TEdge>, int> sh
               = this.AddVerticesAndEdgeRange;
            if ((object)sh != (object)null)
              return sh.Invoke(this, edges);
            else 
            {
              global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
                .SMutableBidirectionalGraph<TVertex, TEdge>> stub
                 = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                  .SMutableBidirectionalGraph<TVertex, TEdge>>)this).DefaultStub;
              return stub.Result<int>(this);
            }
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IMutableVertexSet`1.AddVertex(!0 v)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IMutableVertexSet<TVertex>.AddVertex(TVertex v)
        {
            global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
              .SMutableBidirectionalGraph<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                .SMutableBidirectionalGraph<TVertex, TEdge>>)this).DefaultStub;
            return stub.Result<bool>(this);
        }

        /// <summary>Stub of method System.Int32 QuickGraph.IMutableVertexSet`1.AddVertexRange(System.Collections.Generic.IEnumerable`1&lt;!0&gt; vertices)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        int global::QuickGraph.IMutableVertexSet<TVertex>.AddVertexRange(global::System.Collections.Generic.IEnumerable<TVertex> vertices)
        {
            global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs
              .SMutableBidirectionalGraph<TVertex, TEdge>, 
            global::System.Collections.Generic.IEnumerable<TVertex>, 
            int> sh = this.AddVertexRange;
            if ((object)sh != (object)null)
              return sh.Invoke(this, vertices);
            else 
            {
              global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
                .SMutableBidirectionalGraph<TVertex, TEdge>> stub
                 = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                  .SMutableBidirectionalGraph<TVertex, TEdge>>)this).DefaultStub;
              return stub.Result<int>(this);
            }
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IMutableVertexSet`1.RemoveVertex(!0 v)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IMutableVertexSet<TVertex>.RemoveVertex(TVertex v)
        {
            global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
              .SMutableBidirectionalGraph<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                .SMutableBidirectionalGraph<TVertex, TEdge>>)this).DefaultStub;
            return stub.Result<bool>(this);
        }

        /// <summary>Stub of method System.Int32 QuickGraph.IMutableVertexSet`1.RemoveVertexIf(QuickGraph.VertexPredicate`1&lt;!0&gt; pred)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        int global::QuickGraph.IMutableVertexSet<TVertex>.RemoveVertexIf(global::QuickGraph.VertexPredicate<TVertex> pred)
        {
            global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs
              .SMutableBidirectionalGraph<TVertex, TEdge>, 
            global::QuickGraph.VertexPredicate<TVertex>, int> sh = this.RemoveVertexIf;
            if ((object)sh != (object)null)
              return sh.Invoke(this, pred);
            else 
            {
              global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
                .SMutableBidirectionalGraph<TVertex, TEdge>> stub
                 = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                  .SMutableBidirectionalGraph<TVertex, TEdge>>)this).DefaultStub;
              return stub.Result<int>(this);
            }
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IVertexSet`1.ContainsVertex(!0 vertex)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IVertexSet<TVertex>.ContainsVertex(TVertex vertex)
        {
            global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
              .SMutableBidirectionalGraph<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                .SMutableBidirectionalGraph<TVertex, TEdge>>)this).DefaultStub;
            return stub.Result<bool>(this);
        }

        /// <summary>Stub of method System.Int32 QuickGraph.IMutableEdgeListGraph`2.RemoveEdgeIf(QuickGraph.EdgePredicate`2&lt;!0,!1&gt; predicate)</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SMutableBidirectionalGraph<TVertex, TEdge>, global::QuickGraph.EdgePredicate<TVertex, TEdge>, int> RemoveEdgeIf;

        /// <summary>Stub of method System.Int32 QuickGraph.IMutableVertexSet`1.RemoveVertexIf(QuickGraph.VertexPredicate`1&lt;!0&gt; pred)</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SMutableBidirectionalGraph<TVertex, TEdge>, global::QuickGraph.VertexPredicate<TVertex>, int> RemoveVertexIf;

        /// <summary>Stub of method System.Void QuickGraph.IMutableIncidenceGraph`2.TrimEdgeExcess()</summary>
        public global::Microsoft.Stubs.StubDelegates.Action<global::QuickGraph.Stubs.SMutableBidirectionalGraph<TVertex, TEdge>> TrimEdgeExcess;

        /// <summary>Event VertexAdded</summary>
        public event global::QuickGraph.VertexAction<TVertex> VertexAdded;

        /// <summary>Stub of property QuickGraph.IVertexSet`1.VertexCount</summary>
        int global::QuickGraph.IVertexSet<TVertex>.VertexCount
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs
                  .SMutableBidirectionalGraph<TVertex, TEdge>, int> sh = this.VertexCountGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
                    .SMutableBidirectionalGraph<TVertex, TEdge>> stub
                     = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                      .SMutableBidirectionalGraph<TVertex, TEdge>>)this).DefaultStub;
                  return stub.Result<int>(this);
                }
            }
        }

        /// <summary>Stub of method System.Int32 QuickGraph.IVertexSet`1.get_VertexCount()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SMutableBidirectionalGraph<TVertex, TEdge>, int> VertexCountGet;

        /// <summary>Event VertexRemoved</summary>
        public event global::QuickGraph.VertexAction<TVertex> VertexRemoved;

        /// <summary>Stub of property QuickGraph.IVertexSet`1.Vertices</summary>
        global::System.Collections.Generic.IEnumerable<TVertex> global::QuickGraph.IVertexSet<TVertex>.Vertices
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs
                  .SMutableBidirectionalGraph<TVertex, TEdge>, 
                global::System.Collections.Generic.IEnumerable<TVertex>> sh = this.VerticesGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
                    .SMutableBidirectionalGraph<TVertex, TEdge>> stub
                     = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                      .SMutableBidirectionalGraph<TVertex, TEdge>>)this).DefaultStub;
                  return 
                    stub.Result<global::System.Collections.Generic.IEnumerable<TVertex>>(this);
                }
            }
        }

        /// <summary>Stub of method System.Collections.Generic.IEnumerable`1&lt;!0&gt; QuickGraph.IVertexSet`1.get_Vertices()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SMutableBidirectionalGraph<TVertex, TEdge>, global::System.Collections.Generic.IEnumerable<TVertex>> VerticesGet;
    }
}
namespace QuickGraph.Stubs
{
    /// <summary>Stub of IMutableEdgeListGraph`2</summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Stubs", "0.1.2.3")]
    [global::System.Serializable]
    [global::System.Diagnostics.DebuggerStepThrough]
    public partial class SMutableEdgeListGraph<TVertex,TEdge>
      : global::Microsoft.Stubs.StubBase<global::QuickGraph.Stubs.SMutableEdgeListGraph<TVertex, TEdge>>
      , global::QuickGraph.IMutableEdgeListGraph<TVertex, TEdge>
        where TEdge : global::QuickGraph.IEdge<TVertex>
    {
        /// <summary>Initializes a new instance of type SMutableEdgeListGraph</summary>
        [global::System.Diagnostics.DebuggerHidden]
        public SMutableEdgeListGraph()
        {
        }

        /// <summary>Stub of method System.Int32 QuickGraph.IMutableEdgeListGraph`2.AddEdgeRange(System.Collections.Generic.IEnumerable`1&lt;!1&gt; edges)</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SMutableEdgeListGraph<TVertex, TEdge>, global::System.Collections.Generic.IEnumerable<TEdge>, int> AddEdgeRange;

        /// <summary>Stub of property QuickGraph.IGraph`2.AllowParallelEdges</summary>
        bool global::QuickGraph.IGraph<TVertex, TEdge>.AllowParallelEdges
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs
                  .SMutableEdgeListGraph<TVertex, TEdge>, bool> sh = this.AllowParallelEdgesGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs.IDefaultStub<
                  global::QuickGraph.Stubs.SMutableEdgeListGraph<TVertex, TEdge>
                  > stub = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                    .SMutableEdgeListGraph<TVertex, TEdge>>)this).DefaultStub;
                  return stub.Result<bool>(this);
                }
            }
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IGraph`2.get_AllowParallelEdges()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SMutableEdgeListGraph<TVertex, TEdge>, bool> AllowParallelEdgesGet;

        /// <summary>Stub of method System.Void QuickGraph.IMutableGraph`2.Clear()</summary>
        public global::Microsoft.Stubs.StubDelegates.Action<global::QuickGraph.Stubs.SMutableEdgeListGraph<TVertex, TEdge>> Clear;

        /// <summary>Event EdgeAdded</summary>
        public event global::QuickGraph.EdgeAction<TVertex, TEdge> EdgeAdded;

        /// <summary>Stub of property QuickGraph.IEdgeSet`2.EdgeCount</summary>
        int global::QuickGraph.IEdgeSet<TVertex, TEdge>.EdgeCount
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs
                  .SMutableEdgeListGraph<TVertex, TEdge>, int> sh = this.EdgeCountGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs.IDefaultStub<
                  global::QuickGraph.Stubs.SMutableEdgeListGraph<TVertex, TEdge>
                  > stub = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                    .SMutableEdgeListGraph<TVertex, TEdge>>)this).DefaultStub;
                  return stub.Result<int>(this);
                }
            }
        }

        /// <summary>Stub of method System.Int32 QuickGraph.IEdgeSet`2.get_EdgeCount()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SMutableEdgeListGraph<TVertex, TEdge>, int> EdgeCountGet;

        /// <summary>Event EdgeRemoved</summary>
        public event global::QuickGraph.EdgeAction<TVertex, TEdge> EdgeRemoved;

        /// <summary>Stub of property QuickGraph.IEdgeSet`2.Edges</summary>
        global::System.Collections.Generic.IEnumerable<TEdge> global::QuickGraph.IEdgeSet<TVertex, TEdge>.Edges
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs
                  .SMutableEdgeListGraph<TVertex, TEdge>, 
                global::System.Collections.Generic.IEnumerable<TEdge>> sh = this.EdgesGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs.IDefaultStub<
                  global::QuickGraph.Stubs.SMutableEdgeListGraph<TVertex, TEdge>
                  > stub = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                    .SMutableEdgeListGraph<TVertex, TEdge>>)this).DefaultStub;
                  return 
                    stub.Result<global::System.Collections.Generic.IEnumerable<TEdge>>(this);
                }
            }
        }

        /// <summary>Stub of method System.Collections.Generic.IEnumerable`1&lt;!1&gt; QuickGraph.IEdgeSet`2.get_Edges()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SMutableEdgeListGraph<TVertex, TEdge>, global::System.Collections.Generic.IEnumerable<TEdge>> EdgesGet;

        /// <summary>Stub of property QuickGraph.IGraph`2.IsDirected</summary>
        bool global::QuickGraph.IGraph<TVertex, TEdge>.IsDirected
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs
                  .SMutableEdgeListGraph<TVertex, TEdge>, bool> sh = this.IsDirectedGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs.IDefaultStub<
                  global::QuickGraph.Stubs.SMutableEdgeListGraph<TVertex, TEdge>
                  > stub = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                    .SMutableEdgeListGraph<TVertex, TEdge>>)this).DefaultStub;
                  return stub.Result<bool>(this);
                }
            }
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IGraph`2.get_IsDirected()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SMutableEdgeListGraph<TVertex, TEdge>, bool> IsDirectedGet;

        /// <summary>Stub of property QuickGraph.IEdgeSet`2.IsEdgesEmpty</summary>
        bool global::QuickGraph.IEdgeSet<TVertex, TEdge>.IsEdgesEmpty
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs
                  .SMutableEdgeListGraph<TVertex, TEdge>, bool> sh = this.IsEdgesEmptyGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs.IDefaultStub<
                  global::QuickGraph.Stubs.SMutableEdgeListGraph<TVertex, TEdge>
                  > stub = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                    .SMutableEdgeListGraph<TVertex, TEdge>>)this).DefaultStub;
                  return stub.Result<bool>(this);
                }
            }
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IEdgeSet`2.get_IsEdgesEmpty()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SMutableEdgeListGraph<TVertex, TEdge>, bool> IsEdgesEmptyGet;

        /// <summary>Stub of method System.Boolean QuickGraph.IEdgeSet`2.ContainsEdge(!1 edge)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IEdgeSet<TVertex, TEdge>.ContainsEdge(TEdge edge)
        {
            global::Microsoft.Stubs.IDefaultStub
                <global::QuickGraph.Stubs.SMutableEdgeListGraph<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                .SMutableEdgeListGraph<TVertex, TEdge>>)this).DefaultStub;
            return stub.Result<bool>(this);
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IMutableEdgeListGraph`2.AddEdge(!1 edge)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IMutableEdgeListGraph<TVertex, TEdge>.AddEdge(TEdge edge)
        {
            global::Microsoft.Stubs.IDefaultStub
                <global::QuickGraph.Stubs.SMutableEdgeListGraph<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                .SMutableEdgeListGraph<TVertex, TEdge>>)this).DefaultStub;
            return stub.Result<bool>(this);
        }

        /// <summary>Stub of method System.Int32 QuickGraph.IMutableEdgeListGraph`2.AddEdgeRange(System.Collections.Generic.IEnumerable`1&lt;!1&gt; edges)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        int global::QuickGraph.IMutableEdgeListGraph<TVertex, TEdge>.AddEdgeRange(global::System.Collections.Generic.IEnumerable<TEdge> edges)
        {
            global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs
              .SMutableEdgeListGraph<TVertex, TEdge>, 
            global::System.Collections.Generic.IEnumerable<TEdge>, int> sh
               = this.AddEdgeRange;
            if ((object)sh != (object)null)
              return sh.Invoke(this, edges);
            else 
            {
              global::Microsoft.Stubs.IDefaultStub<
              global::QuickGraph.Stubs.SMutableEdgeListGraph<TVertex, TEdge>
              > stub = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                .SMutableEdgeListGraph<TVertex, TEdge>>)this).DefaultStub;
              return stub.Result<int>(this);
            }
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IMutableEdgeListGraph`2.RemoveEdge(!1 edge)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IMutableEdgeListGraph<TVertex, TEdge>.RemoveEdge(TEdge edge)
        {
            global::Microsoft.Stubs.IDefaultStub
                <global::QuickGraph.Stubs.SMutableEdgeListGraph<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                .SMutableEdgeListGraph<TVertex, TEdge>>)this).DefaultStub;
            return stub.Result<bool>(this);
        }

        /// <summary>Stub of method System.Int32 QuickGraph.IMutableEdgeListGraph`2.RemoveEdgeIf(QuickGraph.EdgePredicate`2&lt;!0,!1&gt; predicate)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        int global::QuickGraph.IMutableEdgeListGraph<TVertex, TEdge>.RemoveEdgeIf(global::QuickGraph.EdgePredicate<TVertex, TEdge> predicate)
        {
            global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs
              .SMutableEdgeListGraph<TVertex, TEdge>, 
            global::QuickGraph.EdgePredicate<TVertex, TEdge>, int> sh = this.RemoveEdgeIf;
            if ((object)sh != (object)null)
              return sh.Invoke(this, predicate);
            else 
            {
              global::Microsoft.Stubs.IDefaultStub<
              global::QuickGraph.Stubs.SMutableEdgeListGraph<TVertex, TEdge>
              > stub = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                .SMutableEdgeListGraph<TVertex, TEdge>>)this).DefaultStub;
              return stub.Result<int>(this);
            }
        }

        /// <summary>Stub of method System.Void QuickGraph.IMutableGraph`2.Clear()</summary>
        [global::System.Diagnostics.DebuggerHidden]
        void global::QuickGraph.IMutableGraph<TVertex, TEdge>.Clear()
        {
            global::Microsoft.Stubs.StubDelegates.Action<global::QuickGraph.Stubs
              .SMutableEdgeListGraph<TVertex, TEdge>> sh = this.Clear;
            if ((object)sh != (object)null)
              sh.Invoke(this);
            else 
            {
              global::Microsoft.Stubs.IDefaultStub<
              global::QuickGraph.Stubs.SMutableEdgeListGraph<TVertex, TEdge>
              > stub = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                .SMutableEdgeListGraph<TVertex, TEdge>>)this).DefaultStub;
              stub.VoidResult(this);
            }
        }

        /// <summary>Stub of method System.Int32 QuickGraph.IMutableEdgeListGraph`2.RemoveEdgeIf(QuickGraph.EdgePredicate`2&lt;!0,!1&gt; predicate)</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SMutableEdgeListGraph<TVertex, TEdge>, global::QuickGraph.EdgePredicate<TVertex, TEdge>, int> RemoveEdgeIf;
    }
}
namespace QuickGraph.Stubs
{
    /// <summary>Stub of IMutableGraph`2</summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Stubs", "0.1.2.3")]
    [global::System.Serializable]
    [global::System.Diagnostics.DebuggerStepThrough]
    public partial class SMutableGraph<TVertex,TEdge>
      : global::Microsoft.Stubs.StubBase<global::QuickGraph.Stubs.SMutableGraph<TVertex, TEdge>>
      , global::QuickGraph.IMutableGraph<TVertex, TEdge>
        where TEdge : global::QuickGraph.IEdge<TVertex>
    {
        /// <summary>Initializes a new instance of type SMutableGraph</summary>
        [global::System.Diagnostics.DebuggerHidden]
        public SMutableGraph()
        {
        }

        /// <summary>Stub of property QuickGraph.IGraph`2.AllowParallelEdges</summary>
        bool global::QuickGraph.IGraph<TVertex, TEdge>.AllowParallelEdges
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates
                  .Func<global::QuickGraph.Stubs.SMutableGraph<TVertex, TEdge>, 
                  bool> sh = this.AllowParallelEdgesGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs.IDefaultStub
                      <global::QuickGraph.Stubs.SMutableGraph<TVertex, TEdge>> stub
                     = ((global::Microsoft.Stubs
                      .IStub<global::QuickGraph.Stubs.SMutableGraph<TVertex, TEdge>>)this)
                      .DefaultStub;
                  return stub.Result<bool>(this);
                }
            }
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IGraph`2.get_AllowParallelEdges()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SMutableGraph<TVertex, TEdge>, bool> AllowParallelEdgesGet;

        /// <summary>Stub of method System.Void QuickGraph.IMutableGraph`2.Clear()</summary>
        public global::Microsoft.Stubs.StubDelegates.Action<global::QuickGraph.Stubs.SMutableGraph<TVertex, TEdge>> Clear;

        /// <summary>Stub of property QuickGraph.IGraph`2.IsDirected</summary>
        bool global::QuickGraph.IGraph<TVertex, TEdge>.IsDirected
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates
                  .Func<global::QuickGraph.Stubs.SMutableGraph<TVertex, TEdge>, 
                  bool> sh = this.IsDirectedGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs.IDefaultStub
                      <global::QuickGraph.Stubs.SMutableGraph<TVertex, TEdge>> stub
                     = ((global::Microsoft.Stubs
                      .IStub<global::QuickGraph.Stubs.SMutableGraph<TVertex, TEdge>>)this)
                      .DefaultStub;
                  return stub.Result<bool>(this);
                }
            }
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IGraph`2.get_IsDirected()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SMutableGraph<TVertex, TEdge>, bool> IsDirectedGet;

        /// <summary>Stub of method System.Void QuickGraph.IMutableGraph`2.Clear()</summary>
        [global::System.Diagnostics.DebuggerHidden]
        void global::QuickGraph.IMutableGraph<TVertex, TEdge>.Clear()
        {
            global::Microsoft.Stubs.StubDelegates
              .Action<global::QuickGraph.Stubs.SMutableGraph<TVertex, TEdge>> sh = this.Clear;
            if ((object)sh != (object)null)
              sh.Invoke(this);
            else 
            {
              global::Microsoft.Stubs.IDefaultStub
                  <global::QuickGraph.Stubs.SMutableGraph<TVertex, TEdge>> stub
                 = ((global::Microsoft.Stubs
                  .IStub<global::QuickGraph.Stubs.SMutableGraph<TVertex, TEdge>>)this)
                  .DefaultStub;
              stub.VoidResult(this);
            }
        }
    }
}
namespace QuickGraph.Stubs
{
    /// <summary>Stub of IMutableIncidenceGraph`2</summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Stubs", "0.1.2.3")]
    [global::System.Serializable]
    [global::System.Diagnostics.DebuggerStepThrough]
    public partial class SMutableIncidenceGraph<TVertex,TEdge>
      : global::Microsoft.Stubs.StubBase<global::QuickGraph.Stubs.SMutableIncidenceGraph<TVertex, TEdge>>
      , global::QuickGraph.IMutableIncidenceGraph<TVertex, TEdge>
        where TEdge : global::QuickGraph.IEdge<TVertex>
    {
        /// <summary>Initializes a new instance of type SMutableIncidenceGraph</summary>
        [global::System.Diagnostics.DebuggerHidden]
        public SMutableIncidenceGraph()
        {
        }

        /// <summary>Stub of property QuickGraph.IGraph`2.AllowParallelEdges</summary>
        bool global::QuickGraph.IGraph<TVertex, TEdge>.AllowParallelEdges
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs
                  .SMutableIncidenceGraph<TVertex, TEdge>, bool> sh = this.AllowParallelEdgesGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
                    .SMutableIncidenceGraph<TVertex, TEdge>> stub
                     = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                      .SMutableIncidenceGraph<TVertex, TEdge>>)this).DefaultStub;
                  return stub.Result<bool>(this);
                }
            }
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IGraph`2.get_AllowParallelEdges()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SMutableIncidenceGraph<TVertex, TEdge>, bool> AllowParallelEdgesGet;

        /// <summary>Stub of method System.Void QuickGraph.IMutableGraph`2.Clear()</summary>
        public global::Microsoft.Stubs.StubDelegates.Action<global::QuickGraph.Stubs.SMutableIncidenceGraph<TVertex, TEdge>> Clear;

        /// <summary>Stub of property QuickGraph.IGraph`2.IsDirected</summary>
        bool global::QuickGraph.IGraph<TVertex, TEdge>.IsDirected
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs
                  .SMutableIncidenceGraph<TVertex, TEdge>, bool> sh = this.IsDirectedGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
                    .SMutableIncidenceGraph<TVertex, TEdge>> stub
                     = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                      .SMutableIncidenceGraph<TVertex, TEdge>>)this).DefaultStub;
                  return stub.Result<bool>(this);
                }
            }
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IGraph`2.get_IsDirected()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SMutableIncidenceGraph<TVertex, TEdge>, bool> IsDirectedGet;

        /// <summary>Stub of method System.Boolean QuickGraph.IImplicitGraph`2.IsOutEdgesEmpty(!0 v)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IImplicitGraph<TVertex, TEdge>.IsOutEdgesEmpty(TVertex v)
        {
            global::Microsoft.Stubs.IDefaultStub
                <global::QuickGraph.Stubs.SMutableIncidenceGraph<TVertex, TEdge>
                > stub = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
              .SMutableIncidenceGraph<TVertex, TEdge>>)this).DefaultStub;
            return stub.Result<bool>(this);
        }

        /// <summary>Stub of method System.Int32 QuickGraph.IImplicitGraph`2.OutDegree(!0 v)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        int global::QuickGraph.IImplicitGraph<TVertex, TEdge>.OutDegree(TVertex v)
        {
            global::Microsoft.Stubs.IDefaultStub
                <global::QuickGraph.Stubs.SMutableIncidenceGraph<TVertex, TEdge>
                > stub = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
              .SMutableIncidenceGraph<TVertex, TEdge>>)this).DefaultStub;
            return stub.Result<int>(this);
        }

        /// <summary>Stub of method !1 QuickGraph.IImplicitGraph`2.OutEdge(!0 v, System.Int32 index)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        TEdge global::QuickGraph.IImplicitGraph<TVertex, TEdge>.OutEdge(TVertex v, int index)
        {
            global::Microsoft.Stubs.IDefaultStub
                <global::QuickGraph.Stubs.SMutableIncidenceGraph<TVertex, TEdge>
                > stub = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
              .SMutableIncidenceGraph<TVertex, TEdge>>)this).DefaultStub;
            return stub.Result<TEdge>(this);
        }

        /// <summary>Stub of method System.Collections.Generic.IEnumerable`1&lt;!1&gt; QuickGraph.IImplicitGraph`2.OutEdges(!0 v)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        global::System.Collections.Generic.IEnumerable<TEdge> global::QuickGraph.IImplicitGraph<TVertex, TEdge>.OutEdges(TVertex v)
        {
            global::Microsoft.Stubs.IDefaultStub
                <global::QuickGraph.Stubs.SMutableIncidenceGraph<TVertex, TEdge>
                > stub = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
              .SMutableIncidenceGraph<TVertex, TEdge>>)this).DefaultStub;
            return stub.Result<global::System.Collections.Generic.IEnumerable<TEdge>>(this);
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IImplicitGraph`2.TryGetOutEdges(!0 v, System.Collections.Generic.IEnumerable`1&lt;!1&gt;&amp; edges)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IImplicitGraph<TVertex, TEdge>.TryGetOutEdges(TVertex v, out global::System.Collections.Generic.IEnumerable<TEdge> edges)
        {
            global::Microsoft.Stubs.IDefaultStub
                <global::QuickGraph.Stubs.SMutableIncidenceGraph<TVertex, TEdge>
                > stub = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
              .SMutableIncidenceGraph<TVertex, TEdge>>)this).DefaultStub;
            stub.ValueAtReturn<global::System.Collections.Generic.IEnumerable<TEdge>>
                (this, out edges);
            return stub.Result<bool>(this);
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IIncidenceGraph`2.ContainsEdge(!0 source, !0 target)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IIncidenceGraph<TVertex, TEdge>.ContainsEdge(TVertex source, TVertex target)
        {
            global::Microsoft.Stubs.IDefaultStub
                <global::QuickGraph.Stubs.SMutableIncidenceGraph<TVertex, TEdge>
                > stub = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
              .SMutableIncidenceGraph<TVertex, TEdge>>)this).DefaultStub;
            return stub.Result<bool>(this);
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IIncidenceGraph`2.TryGetEdge(!0 source, !0 target, !1&amp; edge)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IIncidenceGraph<TVertex, TEdge>.TryGetEdge(
            TVertex source,
            TVertex target,
            out TEdge edge
        )
        {
            global::Microsoft.Stubs.IDefaultStub
                <global::QuickGraph.Stubs.SMutableIncidenceGraph<TVertex, TEdge>
                > stub = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
              .SMutableIncidenceGraph<TVertex, TEdge>>)this).DefaultStub;
            stub.ValueAtReturn<TEdge>(this, out edge);
            return stub.Result<bool>(this);
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IIncidenceGraph`2.TryGetEdges(!0 source, !0 target, System.Collections.Generic.IEnumerable`1&lt;!1&gt;&amp; edges)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IIncidenceGraph<TVertex, TEdge>.TryGetEdges(
            TVertex source,
            TVertex target,
            out global::System.Collections.Generic.IEnumerable<TEdge> edges
        )
        {
            global::Microsoft.Stubs.IDefaultStub
                <global::QuickGraph.Stubs.SMutableIncidenceGraph<TVertex, TEdge>
                > stub = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
              .SMutableIncidenceGraph<TVertex, TEdge>>)this).DefaultStub;
            stub.ValueAtReturn<global::System.Collections.Generic.IEnumerable<TEdge>>
                (this, out edges);
            return stub.Result<bool>(this);
        }

        /// <summary>Stub of method System.Void QuickGraph.IMutableGraph`2.Clear()</summary>
        [global::System.Diagnostics.DebuggerHidden]
        void global::QuickGraph.IMutableGraph<TVertex, TEdge>.Clear()
        {
            global::Microsoft.Stubs.StubDelegates.Action<global::QuickGraph.Stubs
              .SMutableIncidenceGraph<TVertex, TEdge>> sh = this.Clear;
            if ((object)sh != (object)null)
              sh.Invoke(this);
            else 
            {
              global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
                .SMutableIncidenceGraph<TVertex, TEdge>> stub
                 = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                  .SMutableIncidenceGraph<TVertex, TEdge>>)this).DefaultStub;
              stub.VoidResult(this);
            }
        }

        /// <summary>Stub of method System.Void QuickGraph.IMutableIncidenceGraph`2.ClearOutEdges(!0 v)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        void global::QuickGraph.IMutableIncidenceGraph<TVertex, TEdge>.ClearOutEdges(TVertex v)
        {
            global::Microsoft.Stubs.IDefaultStub
                <global::QuickGraph.Stubs.SMutableIncidenceGraph<TVertex, TEdge>
                > stub = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
              .SMutableIncidenceGraph<TVertex, TEdge>>)this).DefaultStub;
            stub.VoidResult(this);
        }

        /// <summary>Stub of method System.Int32 QuickGraph.IMutableIncidenceGraph`2.RemoveOutEdgeIf(!0 v, QuickGraph.EdgePredicate`2&lt;!0,!1&gt; predicate)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        int global::QuickGraph.IMutableIncidenceGraph<TVertex, TEdge>.RemoveOutEdgeIf(TVertex v, global::QuickGraph.EdgePredicate<TVertex, TEdge> predicate)
        {
            global::Microsoft.Stubs.IDefaultStub
                <global::QuickGraph.Stubs.SMutableIncidenceGraph<TVertex, TEdge>
                > stub = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
              .SMutableIncidenceGraph<TVertex, TEdge>>)this).DefaultStub;
            return stub.Result<int>(this);
        }

        /// <summary>Stub of method System.Void QuickGraph.IMutableIncidenceGraph`2.TrimEdgeExcess()</summary>
        [global::System.Diagnostics.DebuggerHidden]
        void global::QuickGraph.IMutableIncidenceGraph<TVertex, TEdge>.TrimEdgeExcess()
        {
            global::Microsoft.Stubs.StubDelegates.Action<global::QuickGraph.Stubs
              .SMutableIncidenceGraph<TVertex, TEdge>> sh = this.TrimEdgeExcess;
            if ((object)sh != (object)null)
              sh.Invoke(this);
            else 
            {
              global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
                .SMutableIncidenceGraph<TVertex, TEdge>> stub
                 = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                  .SMutableIncidenceGraph<TVertex, TEdge>>)this).DefaultStub;
              stub.VoidResult(this);
            }
        }

        /// <summary>Stub of method System.Void QuickGraph.IMutableIncidenceGraph`2.TrimEdgeExcess()</summary>
        public global::Microsoft.Stubs.StubDelegates.Action<global::QuickGraph.Stubs.SMutableIncidenceGraph<TVertex, TEdge>> TrimEdgeExcess;
    }
}
namespace QuickGraph.Petri.Stubs
{
    /// <summary>Stub of IMutablePetriNet`1</summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Stubs", "0.1.2.3")]
    [global::System.Serializable]
    [global::System.Diagnostics.DebuggerStepThrough]
    public partial class SMutablePetriNet<Token>
      : global::Microsoft.Stubs.StubBase<global::QuickGraph.Petri.Stubs.SMutablePetriNet<Token>>
      , global::QuickGraph.Petri.IMutablePetriNet<Token>
    {
        /// <summary>Initializes a new instance of type SMutablePetriNet</summary>
        [global::System.Diagnostics.DebuggerHidden]
        public SMutablePetriNet()
        {
        }

        /// <summary>Stub of method QuickGraph.Petri.IArc`1&lt;!0&gt; QuickGraph.Petri.IMutablePetriNet`1.AddArc(QuickGraph.Petri.IPlace`1&lt;!0&gt; place, QuickGraph.Petri.ITransition`1&lt;!0&gt; transition)</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Petri.Stubs.SMutablePetriNet<Token>, global::QuickGraph.Petri.IPlace<Token>, global::QuickGraph.Petri.ITransition<Token>, global::QuickGraph.Petri.IArc<Token>> AddArcIPlace0ITransition0;

        /// <summary>Stub of method QuickGraph.Petri.IArc`1&lt;!0&gt; QuickGraph.Petri.IMutablePetriNet`1.AddArc(QuickGraph.Petri.ITransition`1&lt;!0&gt; transition, QuickGraph.Petri.IPlace`1&lt;!0&gt; place)</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Petri.Stubs.SMutablePetriNet<Token>, global::QuickGraph.Petri.ITransition<Token>, global::QuickGraph.Petri.IPlace<Token>, global::QuickGraph.Petri.IArc<Token>> AddArcITransition0IPlace0;

        /// <summary>Stub of method QuickGraph.Petri.IPlace`1&lt;!0&gt; QuickGraph.Petri.IMutablePetriNet`1.AddPlace(System.String name)</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Petri.Stubs.SMutablePetriNet<Token>, string, global::QuickGraph.Petri.IPlace<Token>> AddPlace;

        /// <summary>Stub of method QuickGraph.Petri.ITransition`1&lt;!0&gt; QuickGraph.Petri.IMutablePetriNet`1.AddTransition(System.String name)</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Petri.Stubs.SMutablePetriNet<Token>, string, global::QuickGraph.Petri.ITransition<Token>> AddTransition;

        /// <summary>Stub of property QuickGraph.Petri.IPetriNet`1.Arcs</summary>
        global::System.Collections.Generic.IList<global::QuickGraph.Petri.IArc<Token>> global::QuickGraph.Petri.IPetriNet<Token>.Arcs
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates
                  .Func<global::QuickGraph.Petri.Stubs.SMutablePetriNet<Token>, 
                  global::System.Collections.Generic
                    .IList<global::QuickGraph.Petri.IArc<Token>>> sh = this.ArcsGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs.IDefaultStub
                      <global::QuickGraph.Petri.Stubs.SMutablePetriNet<Token>> stub
                     = ((global::Microsoft.Stubs
                      .IStub<global::QuickGraph.Petri.Stubs.SMutablePetriNet<Token>>)this)
                      .DefaultStub;
                  return stub.Result<global::System.Collections.Generic
                    .IList<global::QuickGraph.Petri.IArc<Token>>>(this);
                }
            }
        }

        /// <summary>Stub of method System.Collections.Generic.IList`1&lt;QuickGraph.Petri.IArc`1&lt;!0&gt;&gt; QuickGraph.Petri.IPetriNet`1.get_Arcs()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Petri.Stubs.SMutablePetriNet<Token>, global::System.Collections.Generic.IList<global::QuickGraph.Petri.IArc<Token>>> ArcsGet;

        /// <summary>Stub of property QuickGraph.Petri.IPetriNet`1.Graph</summary>
        global::QuickGraph.Petri.IPetriGraph<Token> global::QuickGraph.Petri.IPetriNet<Token>.Graph
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates
                  .Func<global::QuickGraph.Petri.Stubs.SMutablePetriNet<Token>, 
                  global::QuickGraph.Petri.IPetriGraph<Token>> sh = this.GraphGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs.IDefaultStub
                      <global::QuickGraph.Petri.Stubs.SMutablePetriNet<Token>> stub
                     = ((global::Microsoft.Stubs
                      .IStub<global::QuickGraph.Petri.Stubs.SMutablePetriNet<Token>>)this)
                      .DefaultStub;
                  return stub.Result<global::QuickGraph.Petri.IPetriGraph<Token>>(this);
                }
            }
        }

        /// <summary>Stub of method QuickGraph.Petri.IPetriGraph`1&lt;!0&gt; QuickGraph.Petri.IPetriNet`1.get_Graph()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Petri.Stubs.SMutablePetriNet<Token>, global::QuickGraph.Petri.IPetriGraph<Token>> GraphGet;

        /// <summary>Stub of property QuickGraph.Petri.IPetriNet`1.Places</summary>
        global::System.Collections.Generic.IList<global::QuickGraph.Petri.IPlace<Token>> global::QuickGraph.Petri.IPetriNet<Token>.Places
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates
                  .Func<global::QuickGraph.Petri.Stubs.SMutablePetriNet<Token>, 
                  global::System.Collections.Generic
                    .IList<global::QuickGraph.Petri.IPlace<Token>>> sh = this.PlacesGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs.IDefaultStub
                      <global::QuickGraph.Petri.Stubs.SMutablePetriNet<Token>> stub
                     = ((global::Microsoft.Stubs
                      .IStub<global::QuickGraph.Petri.Stubs.SMutablePetriNet<Token>>)this)
                      .DefaultStub;
                  return stub.Result<global::System.Collections.Generic
                    .IList<global::QuickGraph.Petri.IPlace<Token>>>(this);
                }
            }
        }

        /// <summary>Stub of method System.Collections.Generic.IList`1&lt;QuickGraph.Petri.IPlace`1&lt;!0&gt;&gt; QuickGraph.Petri.IPetriNet`1.get_Places()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Petri.Stubs.SMutablePetriNet<Token>, global::System.Collections.Generic.IList<global::QuickGraph.Petri.IPlace<Token>>> PlacesGet;

        /// <summary>Stub of method QuickGraph.Petri.IArc`1&lt;!0&gt; QuickGraph.Petri.IMutablePetriNet`1.AddArc(QuickGraph.Petri.ITransition`1&lt;!0&gt; transition, QuickGraph.Petri.IPlace`1&lt;!0&gt; place)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        global::QuickGraph.Petri.IArc<Token> global::QuickGraph.Petri.IMutablePetriNet<Token>.AddArc(global::QuickGraph.Petri.ITransition<Token> transition, global::QuickGraph.Petri.IPlace<Token> place)
        {
            global::Microsoft.Stubs.StubDelegates
              .Func<global::QuickGraph.Petri.Stubs.SMutablePetriNet<Token>, 
              global::QuickGraph.Petri.ITransition<Token>, 
              global::QuickGraph.Petri.IPlace<Token>, 
              global::QuickGraph.Petri.IArc<Token>> sh = this.AddArcITransition0IPlace0;
            if ((object)sh != (object)null)
              return sh.Invoke(this, transition, place);
            else 
            {
              global::Microsoft.Stubs.IDefaultStub
                  <global::QuickGraph.Petri.Stubs.SMutablePetriNet<Token>> stub
                 = ((global::Microsoft.Stubs
                  .IStub<global::QuickGraph.Petri.Stubs.SMutablePetriNet<Token>>)this)
                  .DefaultStub;
              return stub.Result<global::QuickGraph.Petri.IArc<Token>>(this);
            }
        }

        /// <summary>Stub of method QuickGraph.Petri.IArc`1&lt;!0&gt; QuickGraph.Petri.IMutablePetriNet`1.AddArc(QuickGraph.Petri.IPlace`1&lt;!0&gt; place, QuickGraph.Petri.ITransition`1&lt;!0&gt; transition)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        global::QuickGraph.Petri.IArc<Token> global::QuickGraph.Petri.IMutablePetriNet<Token>.AddArc(global::QuickGraph.Petri.IPlace<Token> place, global::QuickGraph.Petri.ITransition<Token> transition)
        {
            global::Microsoft.Stubs.StubDelegates
              .Func<global::QuickGraph.Petri.Stubs.SMutablePetriNet<Token>, 
              global::QuickGraph.Petri.IPlace<Token>, 
              global::QuickGraph.Petri.ITransition<Token>, 
              global::QuickGraph.Petri.IArc<Token>> sh = this.AddArcIPlace0ITransition0;
            if ((object)sh != (object)null)
              return sh.Invoke(this, place, transition);
            else 
            {
              global::Microsoft.Stubs.IDefaultStub
                  <global::QuickGraph.Petri.Stubs.SMutablePetriNet<Token>> stub
                 = ((global::Microsoft.Stubs
                  .IStub<global::QuickGraph.Petri.Stubs.SMutablePetriNet<Token>>)this)
                  .DefaultStub;
              return stub.Result<global::QuickGraph.Petri.IArc<Token>>(this);
            }
        }

        /// <summary>Stub of method QuickGraph.Petri.IPlace`1&lt;!0&gt; QuickGraph.Petri.IMutablePetriNet`1.AddPlace(System.String name)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        global::QuickGraph.Petri.IPlace<Token> global::QuickGraph.Petri.IMutablePetriNet<Token>.AddPlace(string name)
        {
            global::Microsoft.Stubs.StubDelegates
              .Func<global::QuickGraph.Petri.Stubs.SMutablePetriNet<Token>, 
              string, global::QuickGraph.Petri.IPlace<Token>> sh = this.AddPlace;
            if ((object)sh != (object)null)
              return sh.Invoke(this, name);
            else 
            {
              global::Microsoft.Stubs.IDefaultStub
                  <global::QuickGraph.Petri.Stubs.SMutablePetriNet<Token>> stub
                 = ((global::Microsoft.Stubs
                  .IStub<global::QuickGraph.Petri.Stubs.SMutablePetriNet<Token>>)this)
                  .DefaultStub;
              return stub.Result<global::QuickGraph.Petri.IPlace<Token>>(this);
            }
        }

        /// <summary>Stub of method QuickGraph.Petri.ITransition`1&lt;!0&gt; QuickGraph.Petri.IMutablePetriNet`1.AddTransition(System.String name)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        global::QuickGraph.Petri.ITransition<Token> global::QuickGraph.Petri.IMutablePetriNet<Token>.AddTransition(string name)
        {
            global::Microsoft.Stubs.StubDelegates
              .Func<global::QuickGraph.Petri.Stubs.SMutablePetriNet<Token>, 
              string, global::QuickGraph.Petri.ITransition<Token>> sh = this.AddTransition;
            if ((object)sh != (object)null)
              return sh.Invoke(this, name);
            else 
            {
              global::Microsoft.Stubs.IDefaultStub
                  <global::QuickGraph.Petri.Stubs.SMutablePetriNet<Token>> stub
                 = ((global::Microsoft.Stubs
                  .IStub<global::QuickGraph.Petri.Stubs.SMutablePetriNet<Token>>)this)
                  .DefaultStub;
              return stub.Result<global::QuickGraph.Petri.ITransition<Token>>(this);
            }
        }

        /// <summary>Stub of property QuickGraph.Petri.IPetriNet`1.Transitions</summary>
        global::System.Collections.Generic.IList<global::QuickGraph.Petri.ITransition<Token>> global::QuickGraph.Petri.IPetriNet<Token>.Transitions
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates
                  .Func<global::QuickGraph.Petri.Stubs.SMutablePetriNet<Token>, 
                  global::System.Collections.Generic.IList<
                  global::QuickGraph.Petri.ITransition<Token>
                  >> sh = this.TransitionsGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs.IDefaultStub
                      <global::QuickGraph.Petri.Stubs.SMutablePetriNet<Token>> stub
                     = ((global::Microsoft.Stubs
                      .IStub<global::QuickGraph.Petri.Stubs.SMutablePetriNet<Token>>)this)
                      .DefaultStub;
                  return stub.Result<global::System.Collections.Generic
                    .IList<global::QuickGraph.Petri.ITransition<Token>>>(this);
                }
            }
        }

        /// <summary>Stub of method System.Collections.Generic.IList`1&lt;QuickGraph.Petri.ITransition`1&lt;!0&gt;&gt; QuickGraph.Petri.IPetriNet`1.get_Transitions()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Petri.Stubs.SMutablePetriNet<Token>, global::System.Collections.Generic.IList<global::QuickGraph.Petri.ITransition<Token>>> TransitionsGet;
    }
}
namespace QuickGraph.Stubs
{
    /// <summary>Stub of IMutableUndirectedGraph`2</summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Stubs", "0.1.2.3")]
    [global::System.Serializable]
    [global::System.Diagnostics.DebuggerStepThrough]
    public partial class SMutableUndirectedGraph<TVertex,TEdge>
      : global::Microsoft.Stubs.StubBase<global::QuickGraph.Stubs.SMutableUndirectedGraph<TVertex, TEdge>>
      , global::QuickGraph.IMutableUndirectedGraph<TVertex, TEdge>
        where TEdge : global::QuickGraph.IEdge<TVertex>
    {
        /// <summary>Initializes a new instance of type SMutableUndirectedGraph</summary>
        [global::System.Diagnostics.DebuggerHidden]
        public SMutableUndirectedGraph()
        {
        }

        /// <summary>Stub of method System.Int32 QuickGraph.IMutableEdgeListGraph`2.AddEdgeRange(System.Collections.Generic.IEnumerable`1&lt;!1&gt; edges)</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SMutableUndirectedGraph<TVertex, TEdge>, global::System.Collections.Generic.IEnumerable<TEdge>, int> AddEdgeRange;

        /// <summary>Stub of method System.Int32 QuickGraph.IMutableVertexSet`1.AddVertexRange(System.Collections.Generic.IEnumerable`1&lt;!0&gt; vertices)</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SMutableUndirectedGraph<TVertex, TEdge>, global::System.Collections.Generic.IEnumerable<TVertex>, int> AddVertexRange;

        /// <summary>Stub of method System.Int32 QuickGraph.IMutableVertexAndEdgeSet`2.AddVerticesAndEdgeRange(System.Collections.Generic.IEnumerable`1&lt;!1&gt; edges)</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SMutableUndirectedGraph<TVertex, TEdge>, global::System.Collections.Generic.IEnumerable<TEdge>, int> AddVerticesAndEdgeRange;

        /// <summary>Stub of property QuickGraph.IGraph`2.AllowParallelEdges</summary>
        bool global::QuickGraph.IGraph<TVertex, TEdge>.AllowParallelEdges
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs
                  .SMutableUndirectedGraph<TVertex, TEdge>, bool> sh = this.AllowParallelEdgesGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
                    .SMutableUndirectedGraph<TVertex, TEdge>> stub
                     = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                      .SMutableUndirectedGraph<TVertex, TEdge>>)this).DefaultStub;
                  return stub.Result<bool>(this);
                }
            }
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IGraph`2.get_AllowParallelEdges()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SMutableUndirectedGraph<TVertex, TEdge>, bool> AllowParallelEdgesGet;

        /// <summary>Stub of method System.Void QuickGraph.IMutableGraph`2.Clear()</summary>
        public global::Microsoft.Stubs.StubDelegates.Action<global::QuickGraph.Stubs.SMutableUndirectedGraph<TVertex, TEdge>> Clear;

        /// <summary>Event EdgeAdded</summary>
        public event global::QuickGraph.EdgeAction<TVertex, TEdge> EdgeAdded;

        /// <summary>Stub of property QuickGraph.IEdgeSet`2.EdgeCount</summary>
        int global::QuickGraph.IEdgeSet<TVertex, TEdge>.EdgeCount
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs
                  .SMutableUndirectedGraph<TVertex, TEdge>, int> sh = this.EdgeCountGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
                    .SMutableUndirectedGraph<TVertex, TEdge>> stub
                     = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                      .SMutableUndirectedGraph<TVertex, TEdge>>)this).DefaultStub;
                  return stub.Result<int>(this);
                }
            }
        }

        /// <summary>Stub of method System.Int32 QuickGraph.IEdgeSet`2.get_EdgeCount()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SMutableUndirectedGraph<TVertex, TEdge>, int> EdgeCountGet;

        /// <summary>Event EdgeRemoved</summary>
        public event global::QuickGraph.EdgeAction<TVertex, TEdge> EdgeRemoved;

        /// <summary>Stub of property QuickGraph.IEdgeSet`2.Edges</summary>
        global::System.Collections.Generic.IEnumerable<TEdge> global::QuickGraph.IEdgeSet<TVertex, TEdge>.Edges
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs
                  .SMutableUndirectedGraph<TVertex, TEdge>, 
                global::System.Collections.Generic.IEnumerable<TEdge>> sh = this.EdgesGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
                    .SMutableUndirectedGraph<TVertex, TEdge>> stub
                     = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                      .SMutableUndirectedGraph<TVertex, TEdge>>)this).DefaultStub;
                  return 
                    stub.Result<global::System.Collections.Generic.IEnumerable<TEdge>>(this);
                }
            }
        }

        /// <summary>Stub of method System.Collections.Generic.IEnumerable`1&lt;!1&gt; QuickGraph.IEdgeSet`2.get_Edges()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SMutableUndirectedGraph<TVertex, TEdge>, global::System.Collections.Generic.IEnumerable<TEdge>> EdgesGet;

        /// <summary>Stub of property QuickGraph.IGraph`2.IsDirected</summary>
        bool global::QuickGraph.IGraph<TVertex, TEdge>.IsDirected
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs
                  .SMutableUndirectedGraph<TVertex, TEdge>, bool> sh = this.IsDirectedGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
                    .SMutableUndirectedGraph<TVertex, TEdge>> stub
                     = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                      .SMutableUndirectedGraph<TVertex, TEdge>>)this).DefaultStub;
                  return stub.Result<bool>(this);
                }
            }
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IGraph`2.get_IsDirected()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SMutableUndirectedGraph<TVertex, TEdge>, bool> IsDirectedGet;

        /// <summary>Stub of property QuickGraph.IEdgeSet`2.IsEdgesEmpty</summary>
        bool global::QuickGraph.IEdgeSet<TVertex, TEdge>.IsEdgesEmpty
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs
                  .SMutableUndirectedGraph<TVertex, TEdge>, bool> sh = this.IsEdgesEmptyGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
                    .SMutableUndirectedGraph<TVertex, TEdge>> stub
                     = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                      .SMutableUndirectedGraph<TVertex, TEdge>>)this).DefaultStub;
                  return stub.Result<bool>(this);
                }
            }
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IEdgeSet`2.get_IsEdgesEmpty()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SMutableUndirectedGraph<TVertex, TEdge>, bool> IsEdgesEmptyGet;

        /// <summary>Stub of property QuickGraph.IVertexSet`1.IsVerticesEmpty</summary>
        bool global::QuickGraph.IVertexSet<TVertex>.IsVerticesEmpty
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs
                  .SMutableUndirectedGraph<TVertex, TEdge>, bool> sh = this.IsVerticesEmptyGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
                    .SMutableUndirectedGraph<TVertex, TEdge>> stub
                     = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                      .SMutableUndirectedGraph<TVertex, TEdge>>)this).DefaultStub;
                  return stub.Result<bool>(this);
                }
            }
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IVertexSet`1.get_IsVerticesEmpty()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SMutableUndirectedGraph<TVertex, TEdge>, bool> IsVerticesEmptyGet;

        /// <summary>Stub of method System.Boolean QuickGraph.IEdgeSet`2.ContainsEdge(!1 edge)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IEdgeSet<TVertex, TEdge>.ContainsEdge(TEdge edge)
        {
            global::Microsoft.Stubs.IDefaultStub<
            global::QuickGraph.Stubs.SMutableUndirectedGraph<TVertex, TEdge>
            > stub = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
              .SMutableUndirectedGraph<TVertex, TEdge>>)this).DefaultStub;
            return stub.Result<bool>(this);
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IMutableEdgeListGraph`2.AddEdge(!1 edge)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IMutableEdgeListGraph<TVertex, TEdge>.AddEdge(TEdge edge)
        {
            global::Microsoft.Stubs.IDefaultStub<
            global::QuickGraph.Stubs.SMutableUndirectedGraph<TVertex, TEdge>
            > stub = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
              .SMutableUndirectedGraph<TVertex, TEdge>>)this).DefaultStub;
            return stub.Result<bool>(this);
        }

        /// <summary>Stub of method System.Int32 QuickGraph.IMutableEdgeListGraph`2.AddEdgeRange(System.Collections.Generic.IEnumerable`1&lt;!1&gt; edges)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        int global::QuickGraph.IMutableEdgeListGraph<TVertex, TEdge>.AddEdgeRange(global::System.Collections.Generic.IEnumerable<TEdge> edges)
        {
            global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs
              .SMutableUndirectedGraph<TVertex, TEdge>, 
            global::System.Collections.Generic.IEnumerable<TEdge>, int> sh
               = this.AddEdgeRange;
            if ((object)sh != (object)null)
              return sh.Invoke(this, edges);
            else 
            {
              global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
                .SMutableUndirectedGraph<TVertex, TEdge>> stub
                 = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                  .SMutableUndirectedGraph<TVertex, TEdge>>)this).DefaultStub;
              return stub.Result<int>(this);
            }
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IMutableEdgeListGraph`2.RemoveEdge(!1 edge)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IMutableEdgeListGraph<TVertex, TEdge>.RemoveEdge(TEdge edge)
        {
            global::Microsoft.Stubs.IDefaultStub<
            global::QuickGraph.Stubs.SMutableUndirectedGraph<TVertex, TEdge>
            > stub = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
              .SMutableUndirectedGraph<TVertex, TEdge>>)this).DefaultStub;
            return stub.Result<bool>(this);
        }

        /// <summary>Stub of method System.Int32 QuickGraph.IMutableEdgeListGraph`2.RemoveEdgeIf(QuickGraph.EdgePredicate`2&lt;!0,!1&gt; predicate)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        int global::QuickGraph.IMutableEdgeListGraph<TVertex, TEdge>.RemoveEdgeIf(global::QuickGraph.EdgePredicate<TVertex, TEdge> predicate)
        {
            global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs
              .SMutableUndirectedGraph<TVertex, TEdge>, 
            global::QuickGraph.EdgePredicate<TVertex, TEdge>, int> sh = this.RemoveEdgeIf;
            if ((object)sh != (object)null)
              return sh.Invoke(this, predicate);
            else 
            {
              global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
                .SMutableUndirectedGraph<TVertex, TEdge>> stub
                 = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                  .SMutableUndirectedGraph<TVertex, TEdge>>)this).DefaultStub;
              return stub.Result<int>(this);
            }
        }

        /// <summary>Stub of method System.Void QuickGraph.IMutableGraph`2.Clear()</summary>
        [global::System.Diagnostics.DebuggerHidden]
        void global::QuickGraph.IMutableGraph<TVertex, TEdge>.Clear()
        {
            global::Microsoft.Stubs.StubDelegates.Action<global::QuickGraph.Stubs
              .SMutableUndirectedGraph<TVertex, TEdge>> sh = this.Clear;
            if ((object)sh != (object)null)
              sh.Invoke(this);
            else 
            {
              global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
                .SMutableUndirectedGraph<TVertex, TEdge>> stub
                 = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                  .SMutableUndirectedGraph<TVertex, TEdge>>)this).DefaultStub;
              stub.VoidResult(this);
            }
        }

        /// <summary>Stub of method System.Void QuickGraph.IMutableUndirectedGraph`2.ClearAdjacentEdges(!0 vertex)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        void global::QuickGraph.IMutableUndirectedGraph<TVertex, TEdge>.ClearAdjacentEdges(TVertex vertex)
        {
            global::Microsoft.Stubs.IDefaultStub<
            global::QuickGraph.Stubs.SMutableUndirectedGraph<TVertex, TEdge>
            > stub = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
              .SMutableUndirectedGraph<TVertex, TEdge>>)this).DefaultStub;
            stub.VoidResult(this);
        }

        /// <summary>Stub of method System.Int32 QuickGraph.IMutableUndirectedGraph`2.RemoveAdjacentEdgeIf(!0 vertex, QuickGraph.EdgePredicate`2&lt;!0,!1&gt; predicate)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        int global::QuickGraph.IMutableUndirectedGraph<TVertex, TEdge>.RemoveAdjacentEdgeIf(TVertex vertex, global::QuickGraph.EdgePredicate<TVertex, TEdge> predicate)
        {
            global::Microsoft.Stubs.IDefaultStub<
            global::QuickGraph.Stubs.SMutableUndirectedGraph<TVertex, TEdge>
            > stub = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
              .SMutableUndirectedGraph<TVertex, TEdge>>)this).DefaultStub;
            return stub.Result<int>(this);
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IMutableVertexAndEdgeSet`2.AddVerticesAndEdge(!1 edge)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IMutableVertexAndEdgeSet<TVertex, TEdge>.AddVerticesAndEdge(TEdge edge)
        {
            global::Microsoft.Stubs.IDefaultStub<
            global::QuickGraph.Stubs.SMutableUndirectedGraph<TVertex, TEdge>
            > stub = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
              .SMutableUndirectedGraph<TVertex, TEdge>>)this).DefaultStub;
            return stub.Result<bool>(this);
        }

        /// <summary>Stub of method System.Int32 QuickGraph.IMutableVertexAndEdgeSet`2.AddVerticesAndEdgeRange(System.Collections.Generic.IEnumerable`1&lt;!1&gt; edges)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        int global::QuickGraph.IMutableVertexAndEdgeSet<TVertex, TEdge>.AddVerticesAndEdgeRange(global::System.Collections.Generic.IEnumerable<TEdge> edges)
        {
            global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs
              .SMutableUndirectedGraph<TVertex, TEdge>, 
            global::System.Collections.Generic.IEnumerable<TEdge>, int> sh
               = this.AddVerticesAndEdgeRange;
            if ((object)sh != (object)null)
              return sh.Invoke(this, edges);
            else 
            {
              global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
                .SMutableUndirectedGraph<TVertex, TEdge>> stub
                 = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                  .SMutableUndirectedGraph<TVertex, TEdge>>)this).DefaultStub;
              return stub.Result<int>(this);
            }
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IMutableVertexSet`1.AddVertex(!0 v)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IMutableVertexSet<TVertex>.AddVertex(TVertex v)
        {
            global::Microsoft.Stubs.IDefaultStub<
            global::QuickGraph.Stubs.SMutableUndirectedGraph<TVertex, TEdge>
            > stub = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
              .SMutableUndirectedGraph<TVertex, TEdge>>)this).DefaultStub;
            return stub.Result<bool>(this);
        }

        /// <summary>Stub of method System.Int32 QuickGraph.IMutableVertexSet`1.AddVertexRange(System.Collections.Generic.IEnumerable`1&lt;!0&gt; vertices)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        int global::QuickGraph.IMutableVertexSet<TVertex>.AddVertexRange(global::System.Collections.Generic.IEnumerable<TVertex> vertices)
        {
            global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs
              .SMutableUndirectedGraph<TVertex, TEdge>, 
            global::System.Collections.Generic.IEnumerable<TVertex>, 
            int> sh = this.AddVertexRange;
            if ((object)sh != (object)null)
              return sh.Invoke(this, vertices);
            else 
            {
              global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
                .SMutableUndirectedGraph<TVertex, TEdge>> stub
                 = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                  .SMutableUndirectedGraph<TVertex, TEdge>>)this).DefaultStub;
              return stub.Result<int>(this);
            }
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IMutableVertexSet`1.RemoveVertex(!0 v)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IMutableVertexSet<TVertex>.RemoveVertex(TVertex v)
        {
            global::Microsoft.Stubs.IDefaultStub<
            global::QuickGraph.Stubs.SMutableUndirectedGraph<TVertex, TEdge>
            > stub = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
              .SMutableUndirectedGraph<TVertex, TEdge>>)this).DefaultStub;
            return stub.Result<bool>(this);
        }

        /// <summary>Stub of method System.Int32 QuickGraph.IMutableVertexSet`1.RemoveVertexIf(QuickGraph.VertexPredicate`1&lt;!0&gt; pred)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        int global::QuickGraph.IMutableVertexSet<TVertex>.RemoveVertexIf(global::QuickGraph.VertexPredicate<TVertex> pred)
        {
            global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs
              .SMutableUndirectedGraph<TVertex, TEdge>, 
            global::QuickGraph.VertexPredicate<TVertex>, int> sh = this.RemoveVertexIf;
            if ((object)sh != (object)null)
              return sh.Invoke(this, pred);
            else 
            {
              global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
                .SMutableUndirectedGraph<TVertex, TEdge>> stub
                 = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                  .SMutableUndirectedGraph<TVertex, TEdge>>)this).DefaultStub;
              return stub.Result<int>(this);
            }
        }

        /// <summary>Stub of method System.Int32 QuickGraph.IUndirectedGraph`2.AdjacentDegree(!0 v)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        int global::QuickGraph.IUndirectedGraph<TVertex, TEdge>.AdjacentDegree(TVertex v)
        {
            global::Microsoft.Stubs.IDefaultStub<
            global::QuickGraph.Stubs.SMutableUndirectedGraph<TVertex, TEdge>
            > stub = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
              .SMutableUndirectedGraph<TVertex, TEdge>>)this).DefaultStub;
            return stub.Result<int>(this);
        }

        /// <summary>Stub of method !1 QuickGraph.IUndirectedGraph`2.AdjacentEdge(!0 v, System.Int32 index)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        TEdge global::QuickGraph.IUndirectedGraph<TVertex, TEdge>.AdjacentEdge(TVertex v, int index)
        {
            global::Microsoft.Stubs.IDefaultStub<
            global::QuickGraph.Stubs.SMutableUndirectedGraph<TVertex, TEdge>
            > stub = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
              .SMutableUndirectedGraph<TVertex, TEdge>>)this).DefaultStub;
            return stub.Result<TEdge>(this);
        }

        /// <summary>Stub of method System.Collections.Generic.IEnumerable`1&lt;!1&gt; QuickGraph.IUndirectedGraph`2.AdjacentEdges(!0 v)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        global::System.Collections.Generic.IEnumerable<TEdge> global::QuickGraph.IUndirectedGraph<TVertex, TEdge>.AdjacentEdges(TVertex v)
        {
            global::Microsoft.Stubs.IDefaultStub<
            global::QuickGraph.Stubs.SMutableUndirectedGraph<TVertex, TEdge>
            > stub = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
              .SMutableUndirectedGraph<TVertex, TEdge>>)this).DefaultStub;
            return stub.Result<global::System.Collections.Generic.IEnumerable<TEdge>>(this);
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IUndirectedGraph`2.ContainsEdge(!0 source, !0 target)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IUndirectedGraph<TVertex, TEdge>.ContainsEdge(TVertex source, TVertex target)
        {
            global::Microsoft.Stubs.IDefaultStub<
            global::QuickGraph.Stubs.SMutableUndirectedGraph<TVertex, TEdge>
            > stub = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
              .SMutableUndirectedGraph<TVertex, TEdge>>)this).DefaultStub;
            return stub.Result<bool>(this);
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IUndirectedGraph`2.IsAdjacentEdgesEmpty(!0 v)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IUndirectedGraph<TVertex, TEdge>.IsAdjacentEdgesEmpty(TVertex v)
        {
            global::Microsoft.Stubs.IDefaultStub<
            global::QuickGraph.Stubs.SMutableUndirectedGraph<TVertex, TEdge>
            > stub = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
              .SMutableUndirectedGraph<TVertex, TEdge>>)this).DefaultStub;
            return stub.Result<bool>(this);
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IVertexSet`1.ContainsVertex(!0 vertex)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IVertexSet<TVertex>.ContainsVertex(TVertex vertex)
        {
            global::Microsoft.Stubs.IDefaultStub<
            global::QuickGraph.Stubs.SMutableUndirectedGraph<TVertex, TEdge>
            > stub = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
              .SMutableUndirectedGraph<TVertex, TEdge>>)this).DefaultStub;
            return stub.Result<bool>(this);
        }

        /// <summary>Stub of method System.Int32 QuickGraph.IMutableEdgeListGraph`2.RemoveEdgeIf(QuickGraph.EdgePredicate`2&lt;!0,!1&gt; predicate)</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SMutableUndirectedGraph<TVertex, TEdge>, global::QuickGraph.EdgePredicate<TVertex, TEdge>, int> RemoveEdgeIf;

        /// <summary>Stub of method System.Int32 QuickGraph.IMutableVertexSet`1.RemoveVertexIf(QuickGraph.VertexPredicate`1&lt;!0&gt; pred)</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SMutableUndirectedGraph<TVertex, TEdge>, global::QuickGraph.VertexPredicate<TVertex>, int> RemoveVertexIf;

        /// <summary>Event VertexAdded</summary>
        public event global::QuickGraph.VertexAction<TVertex> VertexAdded;

        /// <summary>Stub of property QuickGraph.IVertexSet`1.VertexCount</summary>
        int global::QuickGraph.IVertexSet<TVertex>.VertexCount
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs
                  .SMutableUndirectedGraph<TVertex, TEdge>, int> sh = this.VertexCountGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
                    .SMutableUndirectedGraph<TVertex, TEdge>> stub
                     = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                      .SMutableUndirectedGraph<TVertex, TEdge>>)this).DefaultStub;
                  return stub.Result<int>(this);
                }
            }
        }

        /// <summary>Stub of method System.Int32 QuickGraph.IVertexSet`1.get_VertexCount()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SMutableUndirectedGraph<TVertex, TEdge>, int> VertexCountGet;

        /// <summary>Event VertexRemoved</summary>
        public event global::QuickGraph.VertexAction<TVertex> VertexRemoved;

        /// <summary>Stub of property QuickGraph.IVertexSet`1.Vertices</summary>
        global::System.Collections.Generic.IEnumerable<TVertex> global::QuickGraph.IVertexSet<TVertex>.Vertices
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs
                  .SMutableUndirectedGraph<TVertex, TEdge>, 
                global::System.Collections.Generic.IEnumerable<TVertex>> sh = this.VerticesGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
                    .SMutableUndirectedGraph<TVertex, TEdge>> stub
                     = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                      .SMutableUndirectedGraph<TVertex, TEdge>>)this).DefaultStub;
                  return 
                    stub.Result<global::System.Collections.Generic.IEnumerable<TVertex>>(this);
                }
            }
        }

        /// <summary>Stub of method System.Collections.Generic.IEnumerable`1&lt;!0&gt; QuickGraph.IVertexSet`1.get_Vertices()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SMutableUndirectedGraph<TVertex, TEdge>, global::System.Collections.Generic.IEnumerable<TVertex>> VerticesGet;
    }
}
namespace QuickGraph.Stubs
{
    /// <summary>Stub of IMutableVertexAndEdgeListGraph`2</summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Stubs", "0.1.2.3")]
    [global::System.Serializable]
    [global::System.Diagnostics.DebuggerStepThrough]
    public partial class SMutableVertexAndEdgeListGraph<TVertex,TEdge>
      : global::Microsoft.Stubs.StubBase<global::QuickGraph.Stubs.SMutableVertexAndEdgeListGraph<TVertex, TEdge>>
      , global::QuickGraph.IMutableVertexAndEdgeListGraph<TVertex, TEdge>
        where TEdge : global::QuickGraph.IEdge<TVertex>
    {
        /// <summary>Initializes a new instance of type SMutableVertexAndEdgeListGraph</summary>
        [global::System.Diagnostics.DebuggerHidden]
        public SMutableVertexAndEdgeListGraph()
        {
        }

        /// <summary>Stub of method System.Int32 QuickGraph.IMutableEdgeListGraph`2.AddEdgeRange(System.Collections.Generic.IEnumerable`1&lt;!1&gt; edges)</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SMutableVertexAndEdgeListGraph<TVertex, TEdge>, global::System.Collections.Generic.IEnumerable<TEdge>, int> AddEdgeRange;

        /// <summary>Stub of method System.Int32 QuickGraph.IMutableVertexSet`1.AddVertexRange(System.Collections.Generic.IEnumerable`1&lt;!0&gt; vertices)</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SMutableVertexAndEdgeListGraph<TVertex, TEdge>, global::System.Collections.Generic.IEnumerable<TVertex>, int> AddVertexRange;

        /// <summary>Stub of method System.Int32 QuickGraph.IMutableVertexAndEdgeSet`2.AddVerticesAndEdgeRange(System.Collections.Generic.IEnumerable`1&lt;!1&gt; edges)</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SMutableVertexAndEdgeListGraph<TVertex, TEdge>, global::System.Collections.Generic.IEnumerable<TEdge>, int> AddVerticesAndEdgeRange;

        /// <summary>Stub of property QuickGraph.IGraph`2.AllowParallelEdges</summary>
        bool global::QuickGraph.IGraph<TVertex, TEdge>.AllowParallelEdges
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs
                  .SMutableVertexAndEdgeListGraph<TVertex, TEdge>, bool> sh
                   = this.AllowParallelEdgesGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
                    .SMutableVertexAndEdgeListGraph<TVertex, TEdge>> stub
                     = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                      .SMutableVertexAndEdgeListGraph<TVertex, TEdge>>)this).DefaultStub;
                  return stub.Result<bool>(this);
                }
            }
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IGraph`2.get_AllowParallelEdges()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SMutableVertexAndEdgeListGraph<TVertex, TEdge>, bool> AllowParallelEdgesGet;

        /// <summary>Stub of method System.Void QuickGraph.IMutableGraph`2.Clear()</summary>
        public global::Microsoft.Stubs.StubDelegates.Action<global::QuickGraph.Stubs.SMutableVertexAndEdgeListGraph<TVertex, TEdge>> Clear;

        /// <summary>Event EdgeAdded</summary>
        public event global::QuickGraph.EdgeAction<TVertex, TEdge> EdgeAdded;

        /// <summary>Stub of property QuickGraph.IEdgeSet`2.EdgeCount</summary>
        int global::QuickGraph.IEdgeSet<TVertex, TEdge>.EdgeCount
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs
                  .SMutableVertexAndEdgeListGraph<TVertex, TEdge>, int> sh = this.EdgeCountGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
                    .SMutableVertexAndEdgeListGraph<TVertex, TEdge>> stub
                     = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                      .SMutableVertexAndEdgeListGraph<TVertex, TEdge>>)this).DefaultStub;
                  return stub.Result<int>(this);
                }
            }
        }

        /// <summary>Stub of method System.Int32 QuickGraph.IEdgeSet`2.get_EdgeCount()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SMutableVertexAndEdgeListGraph<TVertex, TEdge>, int> EdgeCountGet;

        /// <summary>Event EdgeRemoved</summary>
        public event global::QuickGraph.EdgeAction<TVertex, TEdge> EdgeRemoved;

        /// <summary>Stub of property QuickGraph.IEdgeSet`2.Edges</summary>
        global::System.Collections.Generic.IEnumerable<TEdge> global::QuickGraph.IEdgeSet<TVertex, TEdge>.Edges
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs
                  .SMutableVertexAndEdgeListGraph<TVertex, TEdge>, 
                global::System.Collections.Generic.IEnumerable<TEdge>> sh = this.EdgesGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
                    .SMutableVertexAndEdgeListGraph<TVertex, TEdge>> stub
                     = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                      .SMutableVertexAndEdgeListGraph<TVertex, TEdge>>)this).DefaultStub;
                  return 
                    stub.Result<global::System.Collections.Generic.IEnumerable<TEdge>>(this);
                }
            }
        }

        /// <summary>Stub of method System.Collections.Generic.IEnumerable`1&lt;!1&gt; QuickGraph.IEdgeSet`2.get_Edges()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SMutableVertexAndEdgeListGraph<TVertex, TEdge>, global::System.Collections.Generic.IEnumerable<TEdge>> EdgesGet;

        /// <summary>Stub of property QuickGraph.IGraph`2.IsDirected</summary>
        bool global::QuickGraph.IGraph<TVertex, TEdge>.IsDirected
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs
                  .SMutableVertexAndEdgeListGraph<TVertex, TEdge>, bool> sh = this.IsDirectedGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
                    .SMutableVertexAndEdgeListGraph<TVertex, TEdge>> stub
                     = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                      .SMutableVertexAndEdgeListGraph<TVertex, TEdge>>)this).DefaultStub;
                  return stub.Result<bool>(this);
                }
            }
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IGraph`2.get_IsDirected()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SMutableVertexAndEdgeListGraph<TVertex, TEdge>, bool> IsDirectedGet;

        /// <summary>Stub of property QuickGraph.IEdgeSet`2.IsEdgesEmpty</summary>
        bool global::QuickGraph.IEdgeSet<TVertex, TEdge>.IsEdgesEmpty
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs
                  .SMutableVertexAndEdgeListGraph<TVertex, TEdge>, bool> sh = this.IsEdgesEmptyGet
                  ;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
                    .SMutableVertexAndEdgeListGraph<TVertex, TEdge>> stub
                     = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                      .SMutableVertexAndEdgeListGraph<TVertex, TEdge>>)this).DefaultStub;
                  return stub.Result<bool>(this);
                }
            }
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IEdgeSet`2.get_IsEdgesEmpty()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SMutableVertexAndEdgeListGraph<TVertex, TEdge>, bool> IsEdgesEmptyGet;

        /// <summary>Stub of property QuickGraph.IVertexSet`1.IsVerticesEmpty</summary>
        bool global::QuickGraph.IVertexSet<TVertex>.IsVerticesEmpty
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs
                  .SMutableVertexAndEdgeListGraph<TVertex, TEdge>, bool> sh
                   = this.IsVerticesEmptyGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
                    .SMutableVertexAndEdgeListGraph<TVertex, TEdge>> stub
                     = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                      .SMutableVertexAndEdgeListGraph<TVertex, TEdge>>)this).DefaultStub;
                  return stub.Result<bool>(this);
                }
            }
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IVertexSet`1.get_IsVerticesEmpty()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SMutableVertexAndEdgeListGraph<TVertex, TEdge>, bool> IsVerticesEmptyGet;

        /// <summary>Stub of method System.Boolean QuickGraph.IEdgeSet`2.ContainsEdge(!1 edge)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IEdgeSet<TVertex, TEdge>.ContainsEdge(TEdge edge)
        {
            global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
              .SMutableVertexAndEdgeListGraph<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                .SMutableVertexAndEdgeListGraph<TVertex, TEdge>>)this).DefaultStub;
            return stub.Result<bool>(this);
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IImplicitGraph`2.IsOutEdgesEmpty(!0 v)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IImplicitGraph<TVertex, TEdge>.IsOutEdgesEmpty(TVertex v)
        {
            global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
              .SMutableVertexAndEdgeListGraph<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                .SMutableVertexAndEdgeListGraph<TVertex, TEdge>>)this).DefaultStub;
            return stub.Result<bool>(this);
        }

        /// <summary>Stub of method System.Int32 QuickGraph.IImplicitGraph`2.OutDegree(!0 v)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        int global::QuickGraph.IImplicitGraph<TVertex, TEdge>.OutDegree(TVertex v)
        {
            global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
              .SMutableVertexAndEdgeListGraph<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                .SMutableVertexAndEdgeListGraph<TVertex, TEdge>>)this).DefaultStub;
            return stub.Result<int>(this);
        }

        /// <summary>Stub of method !1 QuickGraph.IImplicitGraph`2.OutEdge(!0 v, System.Int32 index)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        TEdge global::QuickGraph.IImplicitGraph<TVertex, TEdge>.OutEdge(TVertex v, int index)
        {
            global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
              .SMutableVertexAndEdgeListGraph<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                .SMutableVertexAndEdgeListGraph<TVertex, TEdge>>)this).DefaultStub;
            return stub.Result<TEdge>(this);
        }

        /// <summary>Stub of method System.Collections.Generic.IEnumerable`1&lt;!1&gt; QuickGraph.IImplicitGraph`2.OutEdges(!0 v)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        global::System.Collections.Generic.IEnumerable<TEdge> global::QuickGraph.IImplicitGraph<TVertex, TEdge>.OutEdges(TVertex v)
        {
            global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
              .SMutableVertexAndEdgeListGraph<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                .SMutableVertexAndEdgeListGraph<TVertex, TEdge>>)this).DefaultStub;
            return stub.Result<global::System.Collections.Generic.IEnumerable<TEdge>>(this);
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IImplicitGraph`2.TryGetOutEdges(!0 v, System.Collections.Generic.IEnumerable`1&lt;!1&gt;&amp; edges)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IImplicitGraph<TVertex, TEdge>.TryGetOutEdges(TVertex v, out global::System.Collections.Generic.IEnumerable<TEdge> edges)
        {
            global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
              .SMutableVertexAndEdgeListGraph<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                .SMutableVertexAndEdgeListGraph<TVertex, TEdge>>)this).DefaultStub;
            stub.ValueAtReturn<global::System.Collections.Generic.IEnumerable<TEdge>>
                (this, out edges);
            return stub.Result<bool>(this);
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IIncidenceGraph`2.ContainsEdge(!0 source, !0 target)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IIncidenceGraph<TVertex, TEdge>.ContainsEdge(TVertex source, TVertex target)
        {
            global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
              .SMutableVertexAndEdgeListGraph<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                .SMutableVertexAndEdgeListGraph<TVertex, TEdge>>)this).DefaultStub;
            return stub.Result<bool>(this);
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IIncidenceGraph`2.TryGetEdge(!0 source, !0 target, !1&amp; edge)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IIncidenceGraph<TVertex, TEdge>.TryGetEdge(
            TVertex source,
            TVertex target,
            out TEdge edge
        )
        {
            global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
              .SMutableVertexAndEdgeListGraph<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                .SMutableVertexAndEdgeListGraph<TVertex, TEdge>>)this).DefaultStub;
            stub.ValueAtReturn<TEdge>(this, out edge);
            return stub.Result<bool>(this);
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IIncidenceGraph`2.TryGetEdges(!0 source, !0 target, System.Collections.Generic.IEnumerable`1&lt;!1&gt;&amp; edges)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IIncidenceGraph<TVertex, TEdge>.TryGetEdges(
            TVertex source,
            TVertex target,
            out global::System.Collections.Generic.IEnumerable<TEdge> edges
        )
        {
            global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
              .SMutableVertexAndEdgeListGraph<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                .SMutableVertexAndEdgeListGraph<TVertex, TEdge>>)this).DefaultStub;
            stub.ValueAtReturn<global::System.Collections.Generic.IEnumerable<TEdge>>
                (this, out edges);
            return stub.Result<bool>(this);
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IMutableEdgeListGraph`2.AddEdge(!1 edge)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IMutableEdgeListGraph<TVertex, TEdge>.AddEdge(TEdge edge)
        {
            global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
              .SMutableVertexAndEdgeListGraph<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                .SMutableVertexAndEdgeListGraph<TVertex, TEdge>>)this).DefaultStub;
            return stub.Result<bool>(this);
        }

        /// <summary>Stub of method System.Int32 QuickGraph.IMutableEdgeListGraph`2.AddEdgeRange(System.Collections.Generic.IEnumerable`1&lt;!1&gt; edges)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        int global::QuickGraph.IMutableEdgeListGraph<TVertex, TEdge>.AddEdgeRange(global::System.Collections.Generic.IEnumerable<TEdge> edges)
        {
            global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs
              .SMutableVertexAndEdgeListGraph<TVertex, TEdge>, 
            global::System.Collections.Generic.IEnumerable<TEdge>, int> sh
               = this.AddEdgeRange;
            if ((object)sh != (object)null)
              return sh.Invoke(this, edges);
            else 
            {
              global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
                .SMutableVertexAndEdgeListGraph<TVertex, TEdge>> stub
                 = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                  .SMutableVertexAndEdgeListGraph<TVertex, TEdge>>)this).DefaultStub;
              return stub.Result<int>(this);
            }
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IMutableEdgeListGraph`2.RemoveEdge(!1 edge)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IMutableEdgeListGraph<TVertex, TEdge>.RemoveEdge(TEdge edge)
        {
            global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
              .SMutableVertexAndEdgeListGraph<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                .SMutableVertexAndEdgeListGraph<TVertex, TEdge>>)this).DefaultStub;
            return stub.Result<bool>(this);
        }

        /// <summary>Stub of method System.Int32 QuickGraph.IMutableEdgeListGraph`2.RemoveEdgeIf(QuickGraph.EdgePredicate`2&lt;!0,!1&gt; predicate)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        int global::QuickGraph.IMutableEdgeListGraph<TVertex, TEdge>.RemoveEdgeIf(global::QuickGraph.EdgePredicate<TVertex, TEdge> predicate)
        {
            global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs
              .SMutableVertexAndEdgeListGraph<TVertex, TEdge>, 
            global::QuickGraph.EdgePredicate<TVertex, TEdge>, int> sh = this.RemoveEdgeIf;
            if ((object)sh != (object)null)
              return sh.Invoke(this, predicate);
            else 
            {
              global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
                .SMutableVertexAndEdgeListGraph<TVertex, TEdge>> stub
                 = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                  .SMutableVertexAndEdgeListGraph<TVertex, TEdge>>)this).DefaultStub;
              return stub.Result<int>(this);
            }
        }

        /// <summary>Stub of method System.Void QuickGraph.IMutableGraph`2.Clear()</summary>
        [global::System.Diagnostics.DebuggerHidden]
        void global::QuickGraph.IMutableGraph<TVertex, TEdge>.Clear()
        {
            global::Microsoft.Stubs.StubDelegates.Action<global::QuickGraph.Stubs
              .SMutableVertexAndEdgeListGraph<TVertex, TEdge>> sh = this.Clear;
            if ((object)sh != (object)null)
              sh.Invoke(this);
            else 
            {
              global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
                .SMutableVertexAndEdgeListGraph<TVertex, TEdge>> stub
                 = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                  .SMutableVertexAndEdgeListGraph<TVertex, TEdge>>)this).DefaultStub;
              stub.VoidResult(this);
            }
        }

        /// <summary>Stub of method System.Void QuickGraph.IMutableIncidenceGraph`2.ClearOutEdges(!0 v)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        void global::QuickGraph.IMutableIncidenceGraph<TVertex, TEdge>.ClearOutEdges(TVertex v)
        {
            global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
              .SMutableVertexAndEdgeListGraph<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                .SMutableVertexAndEdgeListGraph<TVertex, TEdge>>)this).DefaultStub;
            stub.VoidResult(this);
        }

        /// <summary>Stub of method System.Int32 QuickGraph.IMutableIncidenceGraph`2.RemoveOutEdgeIf(!0 v, QuickGraph.EdgePredicate`2&lt;!0,!1&gt; predicate)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        int global::QuickGraph.IMutableIncidenceGraph<TVertex, TEdge>.RemoveOutEdgeIf(TVertex v, global::QuickGraph.EdgePredicate<TVertex, TEdge> predicate)
        {
            global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
              .SMutableVertexAndEdgeListGraph<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                .SMutableVertexAndEdgeListGraph<TVertex, TEdge>>)this).DefaultStub;
            return stub.Result<int>(this);
        }

        /// <summary>Stub of method System.Void QuickGraph.IMutableIncidenceGraph`2.TrimEdgeExcess()</summary>
        [global::System.Diagnostics.DebuggerHidden]
        void global::QuickGraph.IMutableIncidenceGraph<TVertex, TEdge>.TrimEdgeExcess()
        {
            global::Microsoft.Stubs.StubDelegates.Action<global::QuickGraph.Stubs
              .SMutableVertexAndEdgeListGraph<TVertex, TEdge>> sh = this.TrimEdgeExcess;
            if ((object)sh != (object)null)
              sh.Invoke(this);
            else 
            {
              global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
                .SMutableVertexAndEdgeListGraph<TVertex, TEdge>> stub
                 = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                  .SMutableVertexAndEdgeListGraph<TVertex, TEdge>>)this).DefaultStub;
              stub.VoidResult(this);
            }
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IMutableVertexAndEdgeSet`2.AddVerticesAndEdge(!1 edge)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IMutableVertexAndEdgeSet<TVertex, TEdge>.AddVerticesAndEdge(TEdge edge)
        {
            global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
              .SMutableVertexAndEdgeListGraph<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                .SMutableVertexAndEdgeListGraph<TVertex, TEdge>>)this).DefaultStub;
            return stub.Result<bool>(this);
        }

        /// <summary>Stub of method System.Int32 QuickGraph.IMutableVertexAndEdgeSet`2.AddVerticesAndEdgeRange(System.Collections.Generic.IEnumerable`1&lt;!1&gt; edges)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        int global::QuickGraph.IMutableVertexAndEdgeSet<TVertex, TEdge>.AddVerticesAndEdgeRange(global::System.Collections.Generic.IEnumerable<TEdge> edges)
        {
            global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs
              .SMutableVertexAndEdgeListGraph<TVertex, TEdge>, 
            global::System.Collections.Generic.IEnumerable<TEdge>, int> sh
               = this.AddVerticesAndEdgeRange;
            if ((object)sh != (object)null)
              return sh.Invoke(this, edges);
            else 
            {
              global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
                .SMutableVertexAndEdgeListGraph<TVertex, TEdge>> stub
                 = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                  .SMutableVertexAndEdgeListGraph<TVertex, TEdge>>)this).DefaultStub;
              return stub.Result<int>(this);
            }
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IMutableVertexSet`1.AddVertex(!0 v)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IMutableVertexSet<TVertex>.AddVertex(TVertex v)
        {
            global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
              .SMutableVertexAndEdgeListGraph<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                .SMutableVertexAndEdgeListGraph<TVertex, TEdge>>)this).DefaultStub;
            return stub.Result<bool>(this);
        }

        /// <summary>Stub of method System.Int32 QuickGraph.IMutableVertexSet`1.AddVertexRange(System.Collections.Generic.IEnumerable`1&lt;!0&gt; vertices)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        int global::QuickGraph.IMutableVertexSet<TVertex>.AddVertexRange(global::System.Collections.Generic.IEnumerable<TVertex> vertices)
        {
            global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs
              .SMutableVertexAndEdgeListGraph<TVertex, TEdge>, 
            global::System.Collections.Generic.IEnumerable<TVertex>, 
            int> sh = this.AddVertexRange;
            if ((object)sh != (object)null)
              return sh.Invoke(this, vertices);
            else 
            {
              global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
                .SMutableVertexAndEdgeListGraph<TVertex, TEdge>> stub
                 = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                  .SMutableVertexAndEdgeListGraph<TVertex, TEdge>>)this).DefaultStub;
              return stub.Result<int>(this);
            }
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IMutableVertexSet`1.RemoveVertex(!0 v)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IMutableVertexSet<TVertex>.RemoveVertex(TVertex v)
        {
            global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
              .SMutableVertexAndEdgeListGraph<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                .SMutableVertexAndEdgeListGraph<TVertex, TEdge>>)this).DefaultStub;
            return stub.Result<bool>(this);
        }

        /// <summary>Stub of method System.Int32 QuickGraph.IMutableVertexSet`1.RemoveVertexIf(QuickGraph.VertexPredicate`1&lt;!0&gt; pred)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        int global::QuickGraph.IMutableVertexSet<TVertex>.RemoveVertexIf(global::QuickGraph.VertexPredicate<TVertex> pred)
        {
            global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs
              .SMutableVertexAndEdgeListGraph<TVertex, TEdge>, 
            global::QuickGraph.VertexPredicate<TVertex>, int> sh = this.RemoveVertexIf;
            if ((object)sh != (object)null)
              return sh.Invoke(this, pred);
            else 
            {
              global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
                .SMutableVertexAndEdgeListGraph<TVertex, TEdge>> stub
                 = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                  .SMutableVertexAndEdgeListGraph<TVertex, TEdge>>)this).DefaultStub;
              return stub.Result<int>(this);
            }
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IVertexSet`1.ContainsVertex(!0 vertex)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IVertexSet<TVertex>.ContainsVertex(TVertex vertex)
        {
            global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
              .SMutableVertexAndEdgeListGraph<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                .SMutableVertexAndEdgeListGraph<TVertex, TEdge>>)this).DefaultStub;
            return stub.Result<bool>(this);
        }

        /// <summary>Stub of method System.Int32 QuickGraph.IMutableEdgeListGraph`2.RemoveEdgeIf(QuickGraph.EdgePredicate`2&lt;!0,!1&gt; predicate)</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SMutableVertexAndEdgeListGraph<TVertex, TEdge>, global::QuickGraph.EdgePredicate<TVertex, TEdge>, int> RemoveEdgeIf;

        /// <summary>Stub of method System.Int32 QuickGraph.IMutableVertexSet`1.RemoveVertexIf(QuickGraph.VertexPredicate`1&lt;!0&gt; pred)</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SMutableVertexAndEdgeListGraph<TVertex, TEdge>, global::QuickGraph.VertexPredicate<TVertex>, int> RemoveVertexIf;

        /// <summary>Stub of method System.Void QuickGraph.IMutableIncidenceGraph`2.TrimEdgeExcess()</summary>
        public global::Microsoft.Stubs.StubDelegates.Action<global::QuickGraph.Stubs.SMutableVertexAndEdgeListGraph<TVertex, TEdge>> TrimEdgeExcess;

        /// <summary>Event VertexAdded</summary>
        public event global::QuickGraph.VertexAction<TVertex> VertexAdded;

        /// <summary>Stub of property QuickGraph.IVertexSet`1.VertexCount</summary>
        int global::QuickGraph.IVertexSet<TVertex>.VertexCount
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs
                  .SMutableVertexAndEdgeListGraph<TVertex, TEdge>, int> sh = this.VertexCountGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
                    .SMutableVertexAndEdgeListGraph<TVertex, TEdge>> stub
                     = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                      .SMutableVertexAndEdgeListGraph<TVertex, TEdge>>)this).DefaultStub;
                  return stub.Result<int>(this);
                }
            }
        }

        /// <summary>Stub of method System.Int32 QuickGraph.IVertexSet`1.get_VertexCount()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SMutableVertexAndEdgeListGraph<TVertex, TEdge>, int> VertexCountGet;

        /// <summary>Event VertexRemoved</summary>
        public event global::QuickGraph.VertexAction<TVertex> VertexRemoved;

        /// <summary>Stub of property QuickGraph.IVertexSet`1.Vertices</summary>
        global::System.Collections.Generic.IEnumerable<TVertex> global::QuickGraph.IVertexSet<TVertex>.Vertices
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs
                  .SMutableVertexAndEdgeListGraph<TVertex, TEdge>, 
                global::System.Collections.Generic.IEnumerable<TVertex>> sh = this.VerticesGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
                    .SMutableVertexAndEdgeListGraph<TVertex, TEdge>> stub
                     = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                      .SMutableVertexAndEdgeListGraph<TVertex, TEdge>>)this).DefaultStub;
                  return 
                    stub.Result<global::System.Collections.Generic.IEnumerable<TVertex>>(this);
                }
            }
        }

        /// <summary>Stub of method System.Collections.Generic.IEnumerable`1&lt;!0&gt; QuickGraph.IVertexSet`1.get_Vertices()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SMutableVertexAndEdgeListGraph<TVertex, TEdge>, global::System.Collections.Generic.IEnumerable<TVertex>> VerticesGet;
    }
}
namespace QuickGraph.Stubs
{
    /// <summary>Stub of IMutableVertexAndEdgeSet`2</summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Stubs", "0.1.2.3")]
    [global::System.Serializable]
    [global::System.Diagnostics.DebuggerStepThrough]
    public partial class SMutableVertexAndEdgeSet<TVertex,TEdge>
      : global::Microsoft.Stubs.StubBase<global::QuickGraph.Stubs.SMutableVertexAndEdgeSet<TVertex, TEdge>>
      , global::QuickGraph.IMutableVertexAndEdgeSet<TVertex, TEdge>
        where TEdge : global::QuickGraph.IEdge<TVertex>
    {
        /// <summary>Initializes a new instance of type SMutableVertexAndEdgeSet</summary>
        [global::System.Diagnostics.DebuggerHidden]
        public SMutableVertexAndEdgeSet()
        {
        }

        /// <summary>Stub of method System.Int32 QuickGraph.IMutableEdgeListGraph`2.AddEdgeRange(System.Collections.Generic.IEnumerable`1&lt;!1&gt; edges)</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SMutableVertexAndEdgeSet<TVertex, TEdge>, global::System.Collections.Generic.IEnumerable<TEdge>, int> AddEdgeRange;

        /// <summary>Stub of method System.Int32 QuickGraph.IMutableVertexSet`1.AddVertexRange(System.Collections.Generic.IEnumerable`1&lt;!0&gt; vertices)</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SMutableVertexAndEdgeSet<TVertex, TEdge>, global::System.Collections.Generic.IEnumerable<TVertex>, int> AddVertexRange;

        /// <summary>Stub of method System.Int32 QuickGraph.IMutableVertexAndEdgeSet`2.AddVerticesAndEdgeRange(System.Collections.Generic.IEnumerable`1&lt;!1&gt; edges)</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SMutableVertexAndEdgeSet<TVertex, TEdge>, global::System.Collections.Generic.IEnumerable<TEdge>, int> AddVerticesAndEdgeRange;

        /// <summary>Stub of property QuickGraph.IGraph`2.AllowParallelEdges</summary>
        bool global::QuickGraph.IGraph<TVertex, TEdge>.AllowParallelEdges
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs
                  .SMutableVertexAndEdgeSet<TVertex, TEdge>, bool> sh = this.AllowParallelEdgesGet
                  ;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
                    .SMutableVertexAndEdgeSet<TVertex, TEdge>> stub
                     = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                      .SMutableVertexAndEdgeSet<TVertex, TEdge>>)this).DefaultStub;
                  return stub.Result<bool>(this);
                }
            }
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IGraph`2.get_AllowParallelEdges()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SMutableVertexAndEdgeSet<TVertex, TEdge>, bool> AllowParallelEdgesGet;

        /// <summary>Stub of method System.Void QuickGraph.IMutableGraph`2.Clear()</summary>
        public global::Microsoft.Stubs.StubDelegates.Action<global::QuickGraph.Stubs.SMutableVertexAndEdgeSet<TVertex, TEdge>> Clear;

        /// <summary>Event EdgeAdded</summary>
        public event global::QuickGraph.EdgeAction<TVertex, TEdge> EdgeAdded;

        /// <summary>Stub of property QuickGraph.IEdgeSet`2.EdgeCount</summary>
        int global::QuickGraph.IEdgeSet<TVertex, TEdge>.EdgeCount
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs
                  .SMutableVertexAndEdgeSet<TVertex, TEdge>, int> sh = this.EdgeCountGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
                    .SMutableVertexAndEdgeSet<TVertex, TEdge>> stub
                     = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                      .SMutableVertexAndEdgeSet<TVertex, TEdge>>)this).DefaultStub;
                  return stub.Result<int>(this);
                }
            }
        }

        /// <summary>Stub of method System.Int32 QuickGraph.IEdgeSet`2.get_EdgeCount()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SMutableVertexAndEdgeSet<TVertex, TEdge>, int> EdgeCountGet;

        /// <summary>Event EdgeRemoved</summary>
        public event global::QuickGraph.EdgeAction<TVertex, TEdge> EdgeRemoved;

        /// <summary>Stub of property QuickGraph.IEdgeSet`2.Edges</summary>
        global::System.Collections.Generic.IEnumerable<TEdge> global::QuickGraph.IEdgeSet<TVertex, TEdge>.Edges
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs
                  .SMutableVertexAndEdgeSet<TVertex, TEdge>, 
                global::System.Collections.Generic.IEnumerable<TEdge>> sh = this.EdgesGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
                    .SMutableVertexAndEdgeSet<TVertex, TEdge>> stub
                     = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                      .SMutableVertexAndEdgeSet<TVertex, TEdge>>)this).DefaultStub;
                  return 
                    stub.Result<global::System.Collections.Generic.IEnumerable<TEdge>>(this);
                }
            }
        }

        /// <summary>Stub of method System.Collections.Generic.IEnumerable`1&lt;!1&gt; QuickGraph.IEdgeSet`2.get_Edges()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SMutableVertexAndEdgeSet<TVertex, TEdge>, global::System.Collections.Generic.IEnumerable<TEdge>> EdgesGet;

        /// <summary>Stub of property QuickGraph.IGraph`2.IsDirected</summary>
        bool global::QuickGraph.IGraph<TVertex, TEdge>.IsDirected
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs
                  .SMutableVertexAndEdgeSet<TVertex, TEdge>, bool> sh = this.IsDirectedGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
                    .SMutableVertexAndEdgeSet<TVertex, TEdge>> stub
                     = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                      .SMutableVertexAndEdgeSet<TVertex, TEdge>>)this).DefaultStub;
                  return stub.Result<bool>(this);
                }
            }
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IGraph`2.get_IsDirected()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SMutableVertexAndEdgeSet<TVertex, TEdge>, bool> IsDirectedGet;

        /// <summary>Stub of property QuickGraph.IEdgeSet`2.IsEdgesEmpty</summary>
        bool global::QuickGraph.IEdgeSet<TVertex, TEdge>.IsEdgesEmpty
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs
                  .SMutableVertexAndEdgeSet<TVertex, TEdge>, bool> sh = this.IsEdgesEmptyGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
                    .SMutableVertexAndEdgeSet<TVertex, TEdge>> stub
                     = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                      .SMutableVertexAndEdgeSet<TVertex, TEdge>>)this).DefaultStub;
                  return stub.Result<bool>(this);
                }
            }
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IEdgeSet`2.get_IsEdgesEmpty()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SMutableVertexAndEdgeSet<TVertex, TEdge>, bool> IsEdgesEmptyGet;

        /// <summary>Stub of property QuickGraph.IVertexSet`1.IsVerticesEmpty</summary>
        bool global::QuickGraph.IVertexSet<TVertex>.IsVerticesEmpty
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs
                  .SMutableVertexAndEdgeSet<TVertex, TEdge>, bool> sh = this.IsVerticesEmptyGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
                    .SMutableVertexAndEdgeSet<TVertex, TEdge>> stub
                     = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                      .SMutableVertexAndEdgeSet<TVertex, TEdge>>)this).DefaultStub;
                  return stub.Result<bool>(this);
                }
            }
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IVertexSet`1.get_IsVerticesEmpty()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SMutableVertexAndEdgeSet<TVertex, TEdge>, bool> IsVerticesEmptyGet;

        /// <summary>Stub of method System.Boolean QuickGraph.IEdgeSet`2.ContainsEdge(!1 edge)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IEdgeSet<TVertex, TEdge>.ContainsEdge(TEdge edge)
        {
            global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
              .SMutableVertexAndEdgeSet<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                .SMutableVertexAndEdgeSet<TVertex, TEdge>>)this).DefaultStub;
            return stub.Result<bool>(this);
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IMutableEdgeListGraph`2.AddEdge(!1 edge)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IMutableEdgeListGraph<TVertex, TEdge>.AddEdge(TEdge edge)
        {
            global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
              .SMutableVertexAndEdgeSet<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                .SMutableVertexAndEdgeSet<TVertex, TEdge>>)this).DefaultStub;
            return stub.Result<bool>(this);
        }

        /// <summary>Stub of method System.Int32 QuickGraph.IMutableEdgeListGraph`2.AddEdgeRange(System.Collections.Generic.IEnumerable`1&lt;!1&gt; edges)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        int global::QuickGraph.IMutableEdgeListGraph<TVertex, TEdge>.AddEdgeRange(global::System.Collections.Generic.IEnumerable<TEdge> edges)
        {
            global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs
              .SMutableVertexAndEdgeSet<TVertex, TEdge>, 
            global::System.Collections.Generic.IEnumerable<TEdge>, int> sh
               = this.AddEdgeRange;
            if ((object)sh != (object)null)
              return sh.Invoke(this, edges);
            else 
            {
              global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
                .SMutableVertexAndEdgeSet<TVertex, TEdge>> stub
                 = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                  .SMutableVertexAndEdgeSet<TVertex, TEdge>>)this).DefaultStub;
              return stub.Result<int>(this);
            }
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IMutableEdgeListGraph`2.RemoveEdge(!1 edge)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IMutableEdgeListGraph<TVertex, TEdge>.RemoveEdge(TEdge edge)
        {
            global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
              .SMutableVertexAndEdgeSet<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                .SMutableVertexAndEdgeSet<TVertex, TEdge>>)this).DefaultStub;
            return stub.Result<bool>(this);
        }

        /// <summary>Stub of method System.Int32 QuickGraph.IMutableEdgeListGraph`2.RemoveEdgeIf(QuickGraph.EdgePredicate`2&lt;!0,!1&gt; predicate)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        int global::QuickGraph.IMutableEdgeListGraph<TVertex, TEdge>.RemoveEdgeIf(global::QuickGraph.EdgePredicate<TVertex, TEdge> predicate)
        {
            global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs
              .SMutableVertexAndEdgeSet<TVertex, TEdge>, 
            global::QuickGraph.EdgePredicate<TVertex, TEdge>, int> sh = this.RemoveEdgeIf;
            if ((object)sh != (object)null)
              return sh.Invoke(this, predicate);
            else 
            {
              global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
                .SMutableVertexAndEdgeSet<TVertex, TEdge>> stub
                 = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                  .SMutableVertexAndEdgeSet<TVertex, TEdge>>)this).DefaultStub;
              return stub.Result<int>(this);
            }
        }

        /// <summary>Stub of method System.Void QuickGraph.IMutableGraph`2.Clear()</summary>
        [global::System.Diagnostics.DebuggerHidden]
        void global::QuickGraph.IMutableGraph<TVertex, TEdge>.Clear()
        {
            global::Microsoft.Stubs.StubDelegates.Action<global::QuickGraph.Stubs
              .SMutableVertexAndEdgeSet<TVertex, TEdge>> sh = this.Clear;
            if ((object)sh != (object)null)
              sh.Invoke(this);
            else 
            {
              global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
                .SMutableVertexAndEdgeSet<TVertex, TEdge>> stub
                 = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                  .SMutableVertexAndEdgeSet<TVertex, TEdge>>)this).DefaultStub;
              stub.VoidResult(this);
            }
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IMutableVertexAndEdgeSet`2.AddVerticesAndEdge(!1 edge)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IMutableVertexAndEdgeSet<TVertex, TEdge>.AddVerticesAndEdge(TEdge edge)
        {
            global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
              .SMutableVertexAndEdgeSet<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                .SMutableVertexAndEdgeSet<TVertex, TEdge>>)this).DefaultStub;
            return stub.Result<bool>(this);
        }

        /// <summary>Stub of method System.Int32 QuickGraph.IMutableVertexAndEdgeSet`2.AddVerticesAndEdgeRange(System.Collections.Generic.IEnumerable`1&lt;!1&gt; edges)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        int global::QuickGraph.IMutableVertexAndEdgeSet<TVertex, TEdge>.AddVerticesAndEdgeRange(global::System.Collections.Generic.IEnumerable<TEdge> edges)
        {
            global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs
              .SMutableVertexAndEdgeSet<TVertex, TEdge>, 
            global::System.Collections.Generic.IEnumerable<TEdge>, int> sh
               = this.AddVerticesAndEdgeRange;
            if ((object)sh != (object)null)
              return sh.Invoke(this, edges);
            else 
            {
              global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
                .SMutableVertexAndEdgeSet<TVertex, TEdge>> stub
                 = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                  .SMutableVertexAndEdgeSet<TVertex, TEdge>>)this).DefaultStub;
              return stub.Result<int>(this);
            }
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IMutableVertexSet`1.AddVertex(!0 v)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IMutableVertexSet<TVertex>.AddVertex(TVertex v)
        {
            global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
              .SMutableVertexAndEdgeSet<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                .SMutableVertexAndEdgeSet<TVertex, TEdge>>)this).DefaultStub;
            return stub.Result<bool>(this);
        }

        /// <summary>Stub of method System.Int32 QuickGraph.IMutableVertexSet`1.AddVertexRange(System.Collections.Generic.IEnumerable`1&lt;!0&gt; vertices)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        int global::QuickGraph.IMutableVertexSet<TVertex>.AddVertexRange(global::System.Collections.Generic.IEnumerable<TVertex> vertices)
        {
            global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs
              .SMutableVertexAndEdgeSet<TVertex, TEdge>, 
            global::System.Collections.Generic.IEnumerable<TVertex>, 
            int> sh = this.AddVertexRange;
            if ((object)sh != (object)null)
              return sh.Invoke(this, vertices);
            else 
            {
              global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
                .SMutableVertexAndEdgeSet<TVertex, TEdge>> stub
                 = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                  .SMutableVertexAndEdgeSet<TVertex, TEdge>>)this).DefaultStub;
              return stub.Result<int>(this);
            }
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IMutableVertexSet`1.RemoveVertex(!0 v)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IMutableVertexSet<TVertex>.RemoveVertex(TVertex v)
        {
            global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
              .SMutableVertexAndEdgeSet<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                .SMutableVertexAndEdgeSet<TVertex, TEdge>>)this).DefaultStub;
            return stub.Result<bool>(this);
        }

        /// <summary>Stub of method System.Int32 QuickGraph.IMutableVertexSet`1.RemoveVertexIf(QuickGraph.VertexPredicate`1&lt;!0&gt; pred)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        int global::QuickGraph.IMutableVertexSet<TVertex>.RemoveVertexIf(global::QuickGraph.VertexPredicate<TVertex> pred)
        {
            global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs
              .SMutableVertexAndEdgeSet<TVertex, TEdge>, 
            global::QuickGraph.VertexPredicate<TVertex>, int> sh = this.RemoveVertexIf;
            if ((object)sh != (object)null)
              return sh.Invoke(this, pred);
            else 
            {
              global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
                .SMutableVertexAndEdgeSet<TVertex, TEdge>> stub
                 = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                  .SMutableVertexAndEdgeSet<TVertex, TEdge>>)this).DefaultStub;
              return stub.Result<int>(this);
            }
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IVertexSet`1.ContainsVertex(!0 vertex)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IVertexSet<TVertex>.ContainsVertex(TVertex vertex)
        {
            global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
              .SMutableVertexAndEdgeSet<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                .SMutableVertexAndEdgeSet<TVertex, TEdge>>)this).DefaultStub;
            return stub.Result<bool>(this);
        }

        /// <summary>Stub of method System.Int32 QuickGraph.IMutableEdgeListGraph`2.RemoveEdgeIf(QuickGraph.EdgePredicate`2&lt;!0,!1&gt; predicate)</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SMutableVertexAndEdgeSet<TVertex, TEdge>, global::QuickGraph.EdgePredicate<TVertex, TEdge>, int> RemoveEdgeIf;

        /// <summary>Stub of method System.Int32 QuickGraph.IMutableVertexSet`1.RemoveVertexIf(QuickGraph.VertexPredicate`1&lt;!0&gt; pred)</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SMutableVertexAndEdgeSet<TVertex, TEdge>, global::QuickGraph.VertexPredicate<TVertex>, int> RemoveVertexIf;

        /// <summary>Event VertexAdded</summary>
        public event global::QuickGraph.VertexAction<TVertex> VertexAdded;

        /// <summary>Stub of property QuickGraph.IVertexSet`1.VertexCount</summary>
        int global::QuickGraph.IVertexSet<TVertex>.VertexCount
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs
                  .SMutableVertexAndEdgeSet<TVertex, TEdge>, int> sh = this.VertexCountGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
                    .SMutableVertexAndEdgeSet<TVertex, TEdge>> stub
                     = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                      .SMutableVertexAndEdgeSet<TVertex, TEdge>>)this).DefaultStub;
                  return stub.Result<int>(this);
                }
            }
        }

        /// <summary>Stub of method System.Int32 QuickGraph.IVertexSet`1.get_VertexCount()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SMutableVertexAndEdgeSet<TVertex, TEdge>, int> VertexCountGet;

        /// <summary>Event VertexRemoved</summary>
        public event global::QuickGraph.VertexAction<TVertex> VertexRemoved;

        /// <summary>Stub of property QuickGraph.IVertexSet`1.Vertices</summary>
        global::System.Collections.Generic.IEnumerable<TVertex> global::QuickGraph.IVertexSet<TVertex>.Vertices
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs
                  .SMutableVertexAndEdgeSet<TVertex, TEdge>, 
                global::System.Collections.Generic.IEnumerable<TVertex>> sh = this.VerticesGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
                    .SMutableVertexAndEdgeSet<TVertex, TEdge>> stub
                     = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                      .SMutableVertexAndEdgeSet<TVertex, TEdge>>)this).DefaultStub;
                  return 
                    stub.Result<global::System.Collections.Generic.IEnumerable<TVertex>>(this);
                }
            }
        }

        /// <summary>Stub of method System.Collections.Generic.IEnumerable`1&lt;!0&gt; QuickGraph.IVertexSet`1.get_Vertices()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SMutableVertexAndEdgeSet<TVertex, TEdge>, global::System.Collections.Generic.IEnumerable<TVertex>> VerticesGet;
    }
}
namespace QuickGraph.Stubs
{
    /// <summary>Stub of IMutableVertexListGraph`2</summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Stubs", "0.1.2.3")]
    [global::System.Serializable]
    [global::System.Diagnostics.DebuggerStepThrough]
    public partial class SMutableVertexListGraph<TVertex,TEdge>
      : global::Microsoft.Stubs.StubBase<global::QuickGraph.Stubs.SMutableVertexListGraph<TVertex, TEdge>>
      , global::QuickGraph.IMutableVertexListGraph<TVertex, TEdge>
        where TEdge : global::QuickGraph.IEdge<TVertex>
    {
        /// <summary>Initializes a new instance of type SMutableVertexListGraph</summary>
        [global::System.Diagnostics.DebuggerHidden]
        public SMutableVertexListGraph()
        {
        }

        /// <summary>Stub of method System.Int32 QuickGraph.IMutableVertexSet`1.AddVertexRange(System.Collections.Generic.IEnumerable`1&lt;!0&gt; vertices)</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SMutableVertexListGraph<TVertex, TEdge>, global::System.Collections.Generic.IEnumerable<TVertex>, int> AddVertexRange;

        /// <summary>Stub of property QuickGraph.IGraph`2.AllowParallelEdges</summary>
        bool global::QuickGraph.IGraph<TVertex, TEdge>.AllowParallelEdges
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs
                  .SMutableVertexListGraph<TVertex, TEdge>, bool> sh = this.AllowParallelEdgesGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
                    .SMutableVertexListGraph<TVertex, TEdge>> stub
                     = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                      .SMutableVertexListGraph<TVertex, TEdge>>)this).DefaultStub;
                  return stub.Result<bool>(this);
                }
            }
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IGraph`2.get_AllowParallelEdges()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SMutableVertexListGraph<TVertex, TEdge>, bool> AllowParallelEdgesGet;

        /// <summary>Stub of method System.Void QuickGraph.IMutableGraph`2.Clear()</summary>
        public global::Microsoft.Stubs.StubDelegates.Action<global::QuickGraph.Stubs.SMutableVertexListGraph<TVertex, TEdge>> Clear;

        /// <summary>Stub of property QuickGraph.IGraph`2.IsDirected</summary>
        bool global::QuickGraph.IGraph<TVertex, TEdge>.IsDirected
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs
                  .SMutableVertexListGraph<TVertex, TEdge>, bool> sh = this.IsDirectedGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
                    .SMutableVertexListGraph<TVertex, TEdge>> stub
                     = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                      .SMutableVertexListGraph<TVertex, TEdge>>)this).DefaultStub;
                  return stub.Result<bool>(this);
                }
            }
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IGraph`2.get_IsDirected()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SMutableVertexListGraph<TVertex, TEdge>, bool> IsDirectedGet;

        /// <summary>Stub of property QuickGraph.IVertexSet`1.IsVerticesEmpty</summary>
        bool global::QuickGraph.IVertexSet<TVertex>.IsVerticesEmpty
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs
                  .SMutableVertexListGraph<TVertex, TEdge>, bool> sh = this.IsVerticesEmptyGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
                    .SMutableVertexListGraph<TVertex, TEdge>> stub
                     = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                      .SMutableVertexListGraph<TVertex, TEdge>>)this).DefaultStub;
                  return stub.Result<bool>(this);
                }
            }
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IVertexSet`1.get_IsVerticesEmpty()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SMutableVertexListGraph<TVertex, TEdge>, bool> IsVerticesEmptyGet;

        /// <summary>Stub of method System.Boolean QuickGraph.IImplicitGraph`2.IsOutEdgesEmpty(!0 v)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IImplicitGraph<TVertex, TEdge>.IsOutEdgesEmpty(TVertex v)
        {
            global::Microsoft.Stubs.IDefaultStub<
            global::QuickGraph.Stubs.SMutableVertexListGraph<TVertex, TEdge>
            > stub = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
              .SMutableVertexListGraph<TVertex, TEdge>>)this).DefaultStub;
            return stub.Result<bool>(this);
        }

        /// <summary>Stub of method System.Int32 QuickGraph.IImplicitGraph`2.OutDegree(!0 v)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        int global::QuickGraph.IImplicitGraph<TVertex, TEdge>.OutDegree(TVertex v)
        {
            global::Microsoft.Stubs.IDefaultStub<
            global::QuickGraph.Stubs.SMutableVertexListGraph<TVertex, TEdge>
            > stub = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
              .SMutableVertexListGraph<TVertex, TEdge>>)this).DefaultStub;
            return stub.Result<int>(this);
        }

        /// <summary>Stub of method !1 QuickGraph.IImplicitGraph`2.OutEdge(!0 v, System.Int32 index)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        TEdge global::QuickGraph.IImplicitGraph<TVertex, TEdge>.OutEdge(TVertex v, int index)
        {
            global::Microsoft.Stubs.IDefaultStub<
            global::QuickGraph.Stubs.SMutableVertexListGraph<TVertex, TEdge>
            > stub = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
              .SMutableVertexListGraph<TVertex, TEdge>>)this).DefaultStub;
            return stub.Result<TEdge>(this);
        }

        /// <summary>Stub of method System.Collections.Generic.IEnumerable`1&lt;!1&gt; QuickGraph.IImplicitGraph`2.OutEdges(!0 v)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        global::System.Collections.Generic.IEnumerable<TEdge> global::QuickGraph.IImplicitGraph<TVertex, TEdge>.OutEdges(TVertex v)
        {
            global::Microsoft.Stubs.IDefaultStub<
            global::QuickGraph.Stubs.SMutableVertexListGraph<TVertex, TEdge>
            > stub = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
              .SMutableVertexListGraph<TVertex, TEdge>>)this).DefaultStub;
            return stub.Result<global::System.Collections.Generic.IEnumerable<TEdge>>(this);
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IImplicitGraph`2.TryGetOutEdges(!0 v, System.Collections.Generic.IEnumerable`1&lt;!1&gt;&amp; edges)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IImplicitGraph<TVertex, TEdge>.TryGetOutEdges(TVertex v, out global::System.Collections.Generic.IEnumerable<TEdge> edges)
        {
            global::Microsoft.Stubs.IDefaultStub<
            global::QuickGraph.Stubs.SMutableVertexListGraph<TVertex, TEdge>
            > stub = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
              .SMutableVertexListGraph<TVertex, TEdge>>)this).DefaultStub;
            stub.ValueAtReturn<global::System.Collections.Generic.IEnumerable<TEdge>>
                (this, out edges);
            return stub.Result<bool>(this);
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IIncidenceGraph`2.ContainsEdge(!0 source, !0 target)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IIncidenceGraph<TVertex, TEdge>.ContainsEdge(TVertex source, TVertex target)
        {
            global::Microsoft.Stubs.IDefaultStub<
            global::QuickGraph.Stubs.SMutableVertexListGraph<TVertex, TEdge>
            > stub = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
              .SMutableVertexListGraph<TVertex, TEdge>>)this).DefaultStub;
            return stub.Result<bool>(this);
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IIncidenceGraph`2.TryGetEdge(!0 source, !0 target, !1&amp; edge)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IIncidenceGraph<TVertex, TEdge>.TryGetEdge(
            TVertex source,
            TVertex target,
            out TEdge edge
        )
        {
            global::Microsoft.Stubs.IDefaultStub<
            global::QuickGraph.Stubs.SMutableVertexListGraph<TVertex, TEdge>
            > stub = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
              .SMutableVertexListGraph<TVertex, TEdge>>)this).DefaultStub;
            stub.ValueAtReturn<TEdge>(this, out edge);
            return stub.Result<bool>(this);
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IIncidenceGraph`2.TryGetEdges(!0 source, !0 target, System.Collections.Generic.IEnumerable`1&lt;!1&gt;&amp; edges)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IIncidenceGraph<TVertex, TEdge>.TryGetEdges(
            TVertex source,
            TVertex target,
            out global::System.Collections.Generic.IEnumerable<TEdge> edges
        )
        {
            global::Microsoft.Stubs.IDefaultStub<
            global::QuickGraph.Stubs.SMutableVertexListGraph<TVertex, TEdge>
            > stub = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
              .SMutableVertexListGraph<TVertex, TEdge>>)this).DefaultStub;
            stub.ValueAtReturn<global::System.Collections.Generic.IEnumerable<TEdge>>
                (this, out edges);
            return stub.Result<bool>(this);
        }

        /// <summary>Stub of method System.Void QuickGraph.IMutableGraph`2.Clear()</summary>
        [global::System.Diagnostics.DebuggerHidden]
        void global::QuickGraph.IMutableGraph<TVertex, TEdge>.Clear()
        {
            global::Microsoft.Stubs.StubDelegates.Action<global::QuickGraph.Stubs
              .SMutableVertexListGraph<TVertex, TEdge>> sh = this.Clear;
            if ((object)sh != (object)null)
              sh.Invoke(this);
            else 
            {
              global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
                .SMutableVertexListGraph<TVertex, TEdge>> stub
                 = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                  .SMutableVertexListGraph<TVertex, TEdge>>)this).DefaultStub;
              stub.VoidResult(this);
            }
        }

        /// <summary>Stub of method System.Void QuickGraph.IMutableIncidenceGraph`2.ClearOutEdges(!0 v)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        void global::QuickGraph.IMutableIncidenceGraph<TVertex, TEdge>.ClearOutEdges(TVertex v)
        {
            global::Microsoft.Stubs.IDefaultStub<
            global::QuickGraph.Stubs.SMutableVertexListGraph<TVertex, TEdge>
            > stub = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
              .SMutableVertexListGraph<TVertex, TEdge>>)this).DefaultStub;
            stub.VoidResult(this);
        }

        /// <summary>Stub of method System.Int32 QuickGraph.IMutableIncidenceGraph`2.RemoveOutEdgeIf(!0 v, QuickGraph.EdgePredicate`2&lt;!0,!1&gt; predicate)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        int global::QuickGraph.IMutableIncidenceGraph<TVertex, TEdge>.RemoveOutEdgeIf(TVertex v, global::QuickGraph.EdgePredicate<TVertex, TEdge> predicate)
        {
            global::Microsoft.Stubs.IDefaultStub<
            global::QuickGraph.Stubs.SMutableVertexListGraph<TVertex, TEdge>
            > stub = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
              .SMutableVertexListGraph<TVertex, TEdge>>)this).DefaultStub;
            return stub.Result<int>(this);
        }

        /// <summary>Stub of method System.Void QuickGraph.IMutableIncidenceGraph`2.TrimEdgeExcess()</summary>
        [global::System.Diagnostics.DebuggerHidden]
        void global::QuickGraph.IMutableIncidenceGraph<TVertex, TEdge>.TrimEdgeExcess()
        {
            global::Microsoft.Stubs.StubDelegates.Action<global::QuickGraph.Stubs
              .SMutableVertexListGraph<TVertex, TEdge>> sh = this.TrimEdgeExcess;
            if ((object)sh != (object)null)
              sh.Invoke(this);
            else 
            {
              global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
                .SMutableVertexListGraph<TVertex, TEdge>> stub
                 = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                  .SMutableVertexListGraph<TVertex, TEdge>>)this).DefaultStub;
              stub.VoidResult(this);
            }
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IMutableVertexSet`1.AddVertex(!0 v)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IMutableVertexSet<TVertex>.AddVertex(TVertex v)
        {
            global::Microsoft.Stubs.IDefaultStub<
            global::QuickGraph.Stubs.SMutableVertexListGraph<TVertex, TEdge>
            > stub = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
              .SMutableVertexListGraph<TVertex, TEdge>>)this).DefaultStub;
            return stub.Result<bool>(this);
        }

        /// <summary>Stub of method System.Int32 QuickGraph.IMutableVertexSet`1.AddVertexRange(System.Collections.Generic.IEnumerable`1&lt;!0&gt; vertices)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        int global::QuickGraph.IMutableVertexSet<TVertex>.AddVertexRange(global::System.Collections.Generic.IEnumerable<TVertex> vertices)
        {
            global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs
              .SMutableVertexListGraph<TVertex, TEdge>, 
            global::System.Collections.Generic.IEnumerable<TVertex>, 
            int> sh = this.AddVertexRange;
            if ((object)sh != (object)null)
              return sh.Invoke(this, vertices);
            else 
            {
              global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
                .SMutableVertexListGraph<TVertex, TEdge>> stub
                 = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                  .SMutableVertexListGraph<TVertex, TEdge>>)this).DefaultStub;
              return stub.Result<int>(this);
            }
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IMutableVertexSet`1.RemoveVertex(!0 v)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IMutableVertexSet<TVertex>.RemoveVertex(TVertex v)
        {
            global::Microsoft.Stubs.IDefaultStub<
            global::QuickGraph.Stubs.SMutableVertexListGraph<TVertex, TEdge>
            > stub = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
              .SMutableVertexListGraph<TVertex, TEdge>>)this).DefaultStub;
            return stub.Result<bool>(this);
        }

        /// <summary>Stub of method System.Int32 QuickGraph.IMutableVertexSet`1.RemoveVertexIf(QuickGraph.VertexPredicate`1&lt;!0&gt; pred)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        int global::QuickGraph.IMutableVertexSet<TVertex>.RemoveVertexIf(global::QuickGraph.VertexPredicate<TVertex> pred)
        {
            global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs
              .SMutableVertexListGraph<TVertex, TEdge>, 
            global::QuickGraph.VertexPredicate<TVertex>, int> sh = this.RemoveVertexIf;
            if ((object)sh != (object)null)
              return sh.Invoke(this, pred);
            else 
            {
              global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
                .SMutableVertexListGraph<TVertex, TEdge>> stub
                 = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                  .SMutableVertexListGraph<TVertex, TEdge>>)this).DefaultStub;
              return stub.Result<int>(this);
            }
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IVertexSet`1.ContainsVertex(!0 vertex)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IVertexSet<TVertex>.ContainsVertex(TVertex vertex)
        {
            global::Microsoft.Stubs.IDefaultStub<
            global::QuickGraph.Stubs.SMutableVertexListGraph<TVertex, TEdge>
            > stub = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
              .SMutableVertexListGraph<TVertex, TEdge>>)this).DefaultStub;
            return stub.Result<bool>(this);
        }

        /// <summary>Stub of method System.Int32 QuickGraph.IMutableVertexSet`1.RemoveVertexIf(QuickGraph.VertexPredicate`1&lt;!0&gt; pred)</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SMutableVertexListGraph<TVertex, TEdge>, global::QuickGraph.VertexPredicate<TVertex>, int> RemoveVertexIf;

        /// <summary>Stub of method System.Void QuickGraph.IMutableIncidenceGraph`2.TrimEdgeExcess()</summary>
        public global::Microsoft.Stubs.StubDelegates.Action<global::QuickGraph.Stubs.SMutableVertexListGraph<TVertex, TEdge>> TrimEdgeExcess;

        /// <summary>Event VertexAdded</summary>
        public event global::QuickGraph.VertexAction<TVertex> VertexAdded;

        /// <summary>Stub of property QuickGraph.IVertexSet`1.VertexCount</summary>
        int global::QuickGraph.IVertexSet<TVertex>.VertexCount
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs
                  .SMutableVertexListGraph<TVertex, TEdge>, int> sh = this.VertexCountGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
                    .SMutableVertexListGraph<TVertex, TEdge>> stub
                     = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                      .SMutableVertexListGraph<TVertex, TEdge>>)this).DefaultStub;
                  return stub.Result<int>(this);
                }
            }
        }

        /// <summary>Stub of method System.Int32 QuickGraph.IVertexSet`1.get_VertexCount()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SMutableVertexListGraph<TVertex, TEdge>, int> VertexCountGet;

        /// <summary>Event VertexRemoved</summary>
        public event global::QuickGraph.VertexAction<TVertex> VertexRemoved;

        /// <summary>Stub of property QuickGraph.IVertexSet`1.Vertices</summary>
        global::System.Collections.Generic.IEnumerable<TVertex> global::QuickGraph.IVertexSet<TVertex>.Vertices
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs
                  .SMutableVertexListGraph<TVertex, TEdge>, 
                global::System.Collections.Generic.IEnumerable<TVertex>> sh = this.VerticesGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
                    .SMutableVertexListGraph<TVertex, TEdge>> stub
                     = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                      .SMutableVertexListGraph<TVertex, TEdge>>)this).DefaultStub;
                  return 
                    stub.Result<global::System.Collections.Generic.IEnumerable<TVertex>>(this);
                }
            }
        }

        /// <summary>Stub of method System.Collections.Generic.IEnumerable`1&lt;!0&gt; QuickGraph.IVertexSet`1.get_Vertices()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SMutableVertexListGraph<TVertex, TEdge>, global::System.Collections.Generic.IEnumerable<TVertex>> VerticesGet;
    }
}
namespace QuickGraph.Stubs
{
    /// <summary>Stub of IMutableVertexSet`1</summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Stubs", "0.1.2.3")]
    [global::System.Serializable]
    [global::System.Diagnostics.DebuggerStepThrough]
    public partial class SMutableVertexSet<TVertex>
      : global::Microsoft.Stubs.StubBase<global::QuickGraph.Stubs.SMutableVertexSet<TVertex>>
      , global::QuickGraph.IMutableVertexSet<TVertex>
    {
        /// <summary>Initializes a new instance of type SMutableVertexSet</summary>
        [global::System.Diagnostics.DebuggerHidden]
        public SMutableVertexSet()
        {
        }

        /// <summary>Stub of method System.Int32 QuickGraph.IMutableVertexSet`1.AddVertexRange(System.Collections.Generic.IEnumerable`1&lt;!0&gt; vertices)</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SMutableVertexSet<TVertex>, global::System.Collections.Generic.IEnumerable<TVertex>, int> AddVertexRange;

        /// <summary>Stub of property QuickGraph.IVertexSet`1.IsVerticesEmpty</summary>
        bool global::QuickGraph.IVertexSet<TVertex>.IsVerticesEmpty
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates
                  .Func<global::QuickGraph.Stubs.SMutableVertexSet<TVertex>, bool> sh
                   = this.IsVerticesEmptyGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs
                    .IDefaultStub<global::QuickGraph.Stubs.SMutableVertexSet<TVertex>> stub
                     = ((global::Microsoft.Stubs
                      .IStub<global::QuickGraph.Stubs.SMutableVertexSet<TVertex>>)this)
                      .DefaultStub;
                  return stub.Result<bool>(this);
                }
            }
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IVertexSet`1.get_IsVerticesEmpty()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SMutableVertexSet<TVertex>, bool> IsVerticesEmptyGet;

        /// <summary>Stub of method System.Boolean QuickGraph.IMutableVertexSet`1.AddVertex(!0 v)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IMutableVertexSet<TVertex>.AddVertex(TVertex v)
        {
            global::Microsoft.Stubs
              .IDefaultStub<global::QuickGraph.Stubs.SMutableVertexSet<TVertex>> stub
               = ((global::Microsoft.Stubs
                .IStub<global::QuickGraph.Stubs.SMutableVertexSet<TVertex>>)this)
                .DefaultStub;
            return stub.Result<bool>(this);
        }

        /// <summary>Stub of method System.Int32 QuickGraph.IMutableVertexSet`1.AddVertexRange(System.Collections.Generic.IEnumerable`1&lt;!0&gt; vertices)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        int global::QuickGraph.IMutableVertexSet<TVertex>.AddVertexRange(global::System.Collections.Generic.IEnumerable<TVertex> vertices)
        {
            global::Microsoft.Stubs.StubDelegates
              .Func<global::QuickGraph.Stubs.SMutableVertexSet<TVertex>, 
              global::System.Collections.Generic.IEnumerable<TVertex>, 
              int> sh = this.AddVertexRange;
            if ((object)sh != (object)null)
              return sh.Invoke(this, vertices);
            else 
            {
              global::Microsoft.Stubs
                .IDefaultStub<global::QuickGraph.Stubs.SMutableVertexSet<TVertex>> stub
                 = ((global::Microsoft.Stubs
                  .IStub<global::QuickGraph.Stubs.SMutableVertexSet<TVertex>>)this)
                  .DefaultStub;
              return stub.Result<int>(this);
            }
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IMutableVertexSet`1.RemoveVertex(!0 v)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IMutableVertexSet<TVertex>.RemoveVertex(TVertex v)
        {
            global::Microsoft.Stubs
              .IDefaultStub<global::QuickGraph.Stubs.SMutableVertexSet<TVertex>> stub
               = ((global::Microsoft.Stubs
                .IStub<global::QuickGraph.Stubs.SMutableVertexSet<TVertex>>)this)
                .DefaultStub;
            return stub.Result<bool>(this);
        }

        /// <summary>Stub of method System.Int32 QuickGraph.IMutableVertexSet`1.RemoveVertexIf(QuickGraph.VertexPredicate`1&lt;!0&gt; pred)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        int global::QuickGraph.IMutableVertexSet<TVertex>.RemoveVertexIf(global::QuickGraph.VertexPredicate<TVertex> pred)
        {
            global::Microsoft.Stubs.StubDelegates
              .Func<global::QuickGraph.Stubs.SMutableVertexSet<TVertex>, 
              global::QuickGraph.VertexPredicate<TVertex>, int> sh = this.RemoveVertexIf;
            if ((object)sh != (object)null)
              return sh.Invoke(this, pred);
            else 
            {
              global::Microsoft.Stubs
                .IDefaultStub<global::QuickGraph.Stubs.SMutableVertexSet<TVertex>> stub
                 = ((global::Microsoft.Stubs
                  .IStub<global::QuickGraph.Stubs.SMutableVertexSet<TVertex>>)this)
                  .DefaultStub;
              return stub.Result<int>(this);
            }
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IVertexSet`1.ContainsVertex(!0 vertex)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IVertexSet<TVertex>.ContainsVertex(TVertex vertex)
        {
            global::Microsoft.Stubs
              .IDefaultStub<global::QuickGraph.Stubs.SMutableVertexSet<TVertex>> stub
               = ((global::Microsoft.Stubs
                .IStub<global::QuickGraph.Stubs.SMutableVertexSet<TVertex>>)this)
                .DefaultStub;
            return stub.Result<bool>(this);
        }

        /// <summary>Stub of method System.Int32 QuickGraph.IMutableVertexSet`1.RemoveVertexIf(QuickGraph.VertexPredicate`1&lt;!0&gt; pred)</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SMutableVertexSet<TVertex>, global::QuickGraph.VertexPredicate<TVertex>, int> RemoveVertexIf;

        /// <summary>Event VertexAdded</summary>
        public event global::QuickGraph.VertexAction<TVertex> VertexAdded;

        /// <summary>Stub of property QuickGraph.IVertexSet`1.VertexCount</summary>
        int global::QuickGraph.IVertexSet<TVertex>.VertexCount
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates
                  .Func<global::QuickGraph.Stubs.SMutableVertexSet<TVertex>, int> sh
                   = this.VertexCountGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs
                    .IDefaultStub<global::QuickGraph.Stubs.SMutableVertexSet<TVertex>> stub
                     = ((global::Microsoft.Stubs
                      .IStub<global::QuickGraph.Stubs.SMutableVertexSet<TVertex>>)this)
                      .DefaultStub;
                  return stub.Result<int>(this);
                }
            }
        }

        /// <summary>Stub of method System.Int32 QuickGraph.IVertexSet`1.get_VertexCount()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SMutableVertexSet<TVertex>, int> VertexCountGet;

        /// <summary>Event VertexRemoved</summary>
        public event global::QuickGraph.VertexAction<TVertex> VertexRemoved;

        /// <summary>Stub of property QuickGraph.IVertexSet`1.Vertices</summary>
        global::System.Collections.Generic.IEnumerable<TVertex> global::QuickGraph.IVertexSet<TVertex>.Vertices
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates
                  .Func<global::QuickGraph.Stubs.SMutableVertexSet<TVertex>, 
                  global::System.Collections.Generic.IEnumerable<TVertex>> sh = this.VerticesGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs
                    .IDefaultStub<global::QuickGraph.Stubs.SMutableVertexSet<TVertex>> stub
                     = ((global::Microsoft.Stubs
                      .IStub<global::QuickGraph.Stubs.SMutableVertexSet<TVertex>>)this)
                      .DefaultStub;
                  return 
                    stub.Result<global::System.Collections.Generic.IEnumerable<TVertex>>(this);
                }
            }
        }

        /// <summary>Stub of method System.Collections.Generic.IEnumerable`1&lt;!0&gt; QuickGraph.IVertexSet`1.get_Vertices()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SMutableVertexSet<TVertex>, global::System.Collections.Generic.IEnumerable<TVertex>> VerticesGet;
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
namespace QuickGraph.Petri.Stubs
{
    /// <summary>Stub of IPetriGraph`1</summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Stubs", "0.1.2.3")]
    [global::System.Serializable]
    [global::System.Diagnostics.DebuggerStepThrough]
    public partial class SPetriGraph<Token>
      : global::Microsoft.Stubs.StubBase<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>>
      , global::QuickGraph.Petri.IPetriGraph<Token>
    {
        /// <summary>Initializes a new instance of type SPetriGraph</summary>
        [global::System.Diagnostics.DebuggerHidden]
        public SPetriGraph()
        {
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IMutableEdgeListGraph`2.AddEdge(!1 edge)</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>, global::QuickGraph.Petri.IArc<Token>, bool> AddEdge;

        /// <summary>Stub of method System.Int32 QuickGraph.IMutableEdgeListGraph`2.AddEdgeRange(System.Collections.Generic.IEnumerable`1&lt;!1&gt; edges)</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>, global::System.Collections.Generic.IEnumerable<global::QuickGraph.Petri.IArc<Token>>, int> AddEdgeRange;

        /// <summary>Stub of method System.Boolean QuickGraph.IMutableVertexSet`1.AddVertex(!0 v)</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>, global::QuickGraph.Petri.IPetriVertex, bool> AddVertex;

        /// <summary>Stub of method System.Int32 QuickGraph.IMutableVertexSet`1.AddVertexRange(System.Collections.Generic.IEnumerable`1&lt;!0&gt; vertices)</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>, global::System.Collections.Generic.IEnumerable<global::QuickGraph.Petri.IPetriVertex>, int> AddVertexRange;

        /// <summary>Stub of method System.Boolean QuickGraph.IMutableVertexAndEdgeSet`2.AddVerticesAndEdge(!1 edge)</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>, global::QuickGraph.Petri.IArc<Token>, bool> AddVerticesAndEdge;

        /// <summary>Stub of method System.Int32 QuickGraph.IMutableVertexAndEdgeSet`2.AddVerticesAndEdgeRange(System.Collections.Generic.IEnumerable`1&lt;!1&gt; edges)</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>, global::System.Collections.Generic.IEnumerable<global::QuickGraph.Petri.IArc<Token>>, int> AddVerticesAndEdgeRange;

        /// <summary>Stub of property QuickGraph.IGraph`2.AllowParallelEdges</summary>
        bool global::QuickGraph.IGraph<global::QuickGraph.Petri.IPetriVertex, global::QuickGraph.Petri.IArc<Token>>.AllowParallelEdges
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates
                  .Func<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>, bool> sh
                   = this.AllowParallelEdgesGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs
                    .IDefaultStub<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>> stub
                     = ((global::Microsoft.Stubs
                      .IStub<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>>)this)
                      .DefaultStub;
                  return stub.Result<bool>(this);
                }
            }
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IGraph`2.get_AllowParallelEdges()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>, bool> AllowParallelEdgesGet;

        /// <summary>Stub of method System.Void QuickGraph.IMutableGraph`2.Clear()</summary>
        public global::Microsoft.Stubs.StubDelegates.Action<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>> Clear;

        /// <summary>Stub of method System.Void QuickGraph.IMutableBidirectionalGraph`2.ClearEdges(!0 v)</summary>
        public global::Microsoft.Stubs.StubDelegates.Action<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>, global::QuickGraph.Petri.IPetriVertex> ClearEdges;

        /// <summary>Stub of method System.Void QuickGraph.IMutableBidirectionalGraph`2.ClearInEdges(!0 v)</summary>
        public global::Microsoft.Stubs.StubDelegates.Action<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>, global::QuickGraph.Petri.IPetriVertex> ClearInEdges;

        /// <summary>Stub of method System.Void QuickGraph.IMutableIncidenceGraph`2.ClearOutEdges(!0 v)</summary>
        public global::Microsoft.Stubs.StubDelegates.Action<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>, global::QuickGraph.Petri.IPetriVertex> ClearOutEdges;

        /// <summary>Stub of method System.Boolean QuickGraph.IIncidenceGraph`2.ContainsEdge(!0 source, !0 target)</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>, global::QuickGraph.Petri.IPetriVertex, global::QuickGraph.Petri.IPetriVertex, bool> ContainsEdge;

        /// <summary>Stub of method System.Boolean QuickGraph.IEdgeSet`2.ContainsEdge(!1 edge)</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>, global::QuickGraph.Petri.IArc<Token>, bool> ContainsEdgeIArc0;

        /// <summary>Stub of method System.Boolean QuickGraph.IVertexSet`1.ContainsVertex(!0 vertex)</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>, global::QuickGraph.Petri.IPetriVertex, bool> ContainsVertex;

        /// <summary>Stub of method System.Int32 QuickGraph.IBidirectionalGraph`2.Degree(!0 v)</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>, global::QuickGraph.Petri.IPetriVertex, int> Degree;

        /// <summary>Event EdgeAdded</summary>
        public event global::QuickGraph.EdgeAction<global::QuickGraph.Petri.IPetriVertex, global::QuickGraph.Petri.IArc<Token>> EdgeAdded;

        /// <summary>Stub of property QuickGraph.IEdgeSet`2.EdgeCount</summary>
        int global::QuickGraph.IEdgeSet<global::QuickGraph.Petri.IPetriVertex, global::QuickGraph.Petri.IArc<Token>>.EdgeCount
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates
                  .Func<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>, int> sh
                   = this.EdgeCountGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs
                    .IDefaultStub<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>> stub
                     = ((global::Microsoft.Stubs
                      .IStub<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>>)this)
                      .DefaultStub;
                  return stub.Result<int>(this);
                }
            }
        }

        /// <summary>Stub of method System.Int32 QuickGraph.IEdgeSet`2.get_EdgeCount()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>, int> EdgeCountGet;

        /// <summary>Event EdgeRemoved</summary>
        public event global::QuickGraph.EdgeAction<global::QuickGraph.Petri.IPetriVertex, global::QuickGraph.Petri.IArc<Token>> EdgeRemoved;

        /// <summary>Stub of property QuickGraph.IEdgeSet`2.Edges</summary>
        global::System.Collections.Generic.IEnumerable<global::QuickGraph.Petri.IArc<Token>> global::QuickGraph.IEdgeSet<global::QuickGraph.Petri.IPetriVertex, global::QuickGraph.Petri.IArc<Token>>.Edges
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates
                  .Func<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>, 
                  global::System.Collections.Generic.IEnumerable
                      <global::QuickGraph.Petri.IArc<Token>>> sh = this.EdgesGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs
                    .IDefaultStub<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>> stub
                     = ((global::Microsoft.Stubs
                      .IStub<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>>)this)
                      .DefaultStub;
                  return stub.Result<global::System.Collections.Generic
                    .IEnumerable<global::QuickGraph.Petri.IArc<Token>>>(this);
                }
            }
        }

        /// <summary>Stub of method System.Collections.Generic.IEnumerable`1&lt;!1&gt; QuickGraph.IEdgeSet`2.get_Edges()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>, global::System.Collections.Generic.IEnumerable<global::QuickGraph.Petri.IArc<Token>>> EdgesGet;

        /// <summary>Stub of method System.Int32 QuickGraph.IBidirectionalGraph`2.InDegree(!0 v)</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>, global::QuickGraph.Petri.IPetriVertex, int> InDegree;

        /// <summary>Stub of method !1 QuickGraph.IBidirectionalGraph`2.InEdge(!0 v, System.Int32 index)</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>, global::QuickGraph.Petri.IPetriVertex, int, global::QuickGraph.Petri.IArc<Token>> InEdge;

        /// <summary>Stub of method System.Collections.Generic.IEnumerable`1&lt;!1&gt; QuickGraph.IBidirectionalGraph`2.InEdges(!0 v)</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>, global::QuickGraph.Petri.IPetriVertex, global::System.Collections.Generic.IEnumerable<global::QuickGraph.Petri.IArc<Token>>> InEdges;

        /// <summary>Stub of property QuickGraph.IGraph`2.IsDirected</summary>
        bool global::QuickGraph.IGraph<global::QuickGraph.Petri.IPetriVertex, global::QuickGraph.Petri.IArc<Token>>.IsDirected
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates
                  .Func<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>, bool> sh
                   = this.IsDirectedGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs
                    .IDefaultStub<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>> stub
                     = ((global::Microsoft.Stubs
                      .IStub<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>>)this)
                      .DefaultStub;
                  return stub.Result<bool>(this);
                }
            }
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IGraph`2.get_IsDirected()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>, bool> IsDirectedGet;

        /// <summary>Stub of property QuickGraph.IEdgeSet`2.IsEdgesEmpty</summary>
        bool global::QuickGraph.IEdgeSet<global::QuickGraph.Petri.IPetriVertex, global::QuickGraph.Petri.IArc<Token>>.IsEdgesEmpty
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates
                  .Func<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>, bool> sh
                   = this.IsEdgesEmptyGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs
                    .IDefaultStub<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>> stub
                     = ((global::Microsoft.Stubs
                      .IStub<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>>)this)
                      .DefaultStub;
                  return stub.Result<bool>(this);
                }
            }
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IEdgeSet`2.get_IsEdgesEmpty()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>, bool> IsEdgesEmptyGet;

        /// <summary>Stub of method System.Boolean QuickGraph.IBidirectionalGraph`2.IsInEdgesEmpty(!0 v)</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>, global::QuickGraph.Petri.IPetriVertex, bool> IsInEdgesEmpty;

        /// <summary>Stub of method System.Boolean QuickGraph.IImplicitGraph`2.IsOutEdgesEmpty(!0 v)</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>, global::QuickGraph.Petri.IPetriVertex, bool> IsOutEdgesEmpty;

        /// <summary>Stub of property QuickGraph.IVertexSet`1.IsVerticesEmpty</summary>
        bool global::QuickGraph.IVertexSet<global::QuickGraph.Petri.IPetriVertex>.IsVerticesEmpty
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates
                  .Func<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>, bool> sh
                   = this.IsVerticesEmptyGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs
                    .IDefaultStub<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>> stub
                     = ((global::Microsoft.Stubs
                      .IStub<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>>)this)
                      .DefaultStub;
                  return stub.Result<bool>(this);
                }
            }
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IVertexSet`1.get_IsVerticesEmpty()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>, bool> IsVerticesEmptyGet;

        /// <summary>Stub of method System.Int32 QuickGraph.IImplicitGraph`2.OutDegree(!0 v)</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>, global::QuickGraph.Petri.IPetriVertex, int> OutDegree;

        /// <summary>Stub of method !1 QuickGraph.IImplicitGraph`2.OutEdge(!0 v, System.Int32 index)</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>, global::QuickGraph.Petri.IPetriVertex, int, global::QuickGraph.Petri.IArc<Token>> OutEdge;

        /// <summary>Stub of method System.Collections.Generic.IEnumerable`1&lt;!1&gt; QuickGraph.IImplicitGraph`2.OutEdges(!0 v)</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>, global::QuickGraph.Petri.IPetriVertex, global::System.Collections.Generic.IEnumerable<global::QuickGraph.Petri.IArc<Token>>> OutEdges;

        /// <summary>Stub of method System.Int32 QuickGraph.IBidirectionalGraph`2.Degree(!0 v)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        int global::QuickGraph.IBidirectionalGraph<global::QuickGraph.Petri.IPetriVertex, global::QuickGraph.Petri.IArc<Token>>.Degree(global::QuickGraph.Petri.IPetriVertex v)
        {
            global::Microsoft.Stubs.StubDelegates
              .Func<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>, 
              global::QuickGraph.Petri.IPetriVertex, int> sh = this.Degree;
            if ((object)sh != (object)null)
              return sh.Invoke(this, v);
            else 
            {
              global::Microsoft.Stubs
                .IDefaultStub<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>> stub
                 = ((global::Microsoft.Stubs
                  .IStub<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>>)this)
                  .DefaultStub;
              return stub.Result<int>(this);
            }
        }

        /// <summary>Stub of method System.Int32 QuickGraph.IBidirectionalGraph`2.InDegree(!0 v)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        int global::QuickGraph.IBidirectionalGraph<global::QuickGraph.Petri.IPetriVertex, global::QuickGraph.Petri.IArc<Token>>.InDegree(global::QuickGraph.Petri.IPetriVertex v)
        {
            global::Microsoft.Stubs.StubDelegates
              .Func<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>, 
              global::QuickGraph.Petri.IPetriVertex, int> sh = this.InDegree;
            if ((object)sh != (object)null)
              return sh.Invoke(this, v);
            else 
            {
              global::Microsoft.Stubs
                .IDefaultStub<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>> stub
                 = ((global::Microsoft.Stubs
                  .IStub<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>>)this)
                  .DefaultStub;
              return stub.Result<int>(this);
            }
        }

        /// <summary>Stub of method !1 QuickGraph.IBidirectionalGraph`2.InEdge(!0 v, System.Int32 index)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        global::QuickGraph.Petri.IArc<Token> global::QuickGraph.IBidirectionalGraph<global::QuickGraph.Petri.IPetriVertex, global::QuickGraph.Petri.IArc<Token>>.InEdge(global::QuickGraph.Petri.IPetriVertex v, int index)
        {
            global::Microsoft.Stubs.StubDelegates
              .Func<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>, 
              global::QuickGraph.Petri.IPetriVertex, 
              int, global::QuickGraph.Petri.IArc<Token>> sh = this.InEdge;
            if ((object)sh != (object)null)
              return sh.Invoke(this, v, index);
            else 
            {
              global::Microsoft.Stubs
                .IDefaultStub<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>> stub
                 = ((global::Microsoft.Stubs
                  .IStub<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>>)this)
                  .DefaultStub;
              return stub.Result<global::QuickGraph.Petri.IArc<Token>>(this);
            }
        }

        /// <summary>Stub of method System.Collections.Generic.IEnumerable`1&lt;!1&gt; QuickGraph.IBidirectionalGraph`2.InEdges(!0 v)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        global::System.Collections.Generic.IEnumerable<global::QuickGraph.Petri.IArc<Token>> global::QuickGraph.IBidirectionalGraph<global::QuickGraph.Petri.IPetriVertex, global::QuickGraph.Petri.IArc<Token>>.InEdges(global::QuickGraph.Petri.IPetriVertex v)
        {
            global::Microsoft.Stubs.StubDelegates
              .Func<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>, 
              global::QuickGraph.Petri.IPetriVertex, 
              global::System.Collections.Generic.IEnumerable
                  <global::QuickGraph.Petri.IArc<Token>>> sh = this.InEdges;
            if ((object)sh != (object)null)
              return sh.Invoke(this, v);
            else 
            {
              global::Microsoft.Stubs
                .IDefaultStub<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>> stub
                 = ((global::Microsoft.Stubs
                  .IStub<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>>)this)
                  .DefaultStub;
              return stub.Result<global::System.Collections.Generic
                .IEnumerable<global::QuickGraph.Petri.IArc<Token>>>(this);
            }
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IBidirectionalGraph`2.IsInEdgesEmpty(!0 v)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IBidirectionalGraph<global::QuickGraph.Petri.IPetriVertex, global::QuickGraph.Petri.IArc<Token>>.IsInEdgesEmpty(global::QuickGraph.Petri.IPetriVertex v)
        {
            global::Microsoft.Stubs.StubDelegates
              .Func<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>, 
              global::QuickGraph.Petri.IPetriVertex, bool> sh = this.IsInEdgesEmpty;
            if ((object)sh != (object)null)
              return sh.Invoke(this, v);
            else 
            {
              global::Microsoft.Stubs
                .IDefaultStub<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>> stub
                 = ((global::Microsoft.Stubs
                  .IStub<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>>)this)
                  .DefaultStub;
              return stub.Result<bool>(this);
            }
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IBidirectionalGraph`2.TryGetInEdges(!0 v, System.Collections.Generic.IEnumerable`1&lt;!1&gt;&amp; edges)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IBidirectionalGraph<global::QuickGraph.Petri.IPetriVertex, global::QuickGraph.Petri.IArc<Token>>.TryGetInEdges(global::QuickGraph.Petri.IPetriVertex v, out global::System.Collections.Generic.IEnumerable<global::QuickGraph.Petri.IArc<Token>> edges)
        {
            global::Microsoft.Stubs.StubDelegates
              .OutFunc<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>, 
              global::QuickGraph.Petri.IPetriVertex, 
              global::System.Collections.Generic.IEnumerable
                  <global::QuickGraph.Petri.IArc<Token>>, bool> sh = this.TryGetInEdges;
            if ((object)sh != (object)null)
              return sh.Invoke(this, v, out edges);
            else 
            {
              global::Microsoft.Stubs
                .IDefaultStub<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>> stub
                 = ((global::Microsoft.Stubs
                  .IStub<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>>)this)
                  .DefaultStub;
              stub.ValueAtReturn<global::System.Collections.Generic
                .IEnumerable<global::QuickGraph.Petri.IArc<Token>>>(this, out edges);
              return stub.Result<bool>(this);
            }
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IEdgeSet`2.ContainsEdge(!1 edge)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IEdgeSet<global::QuickGraph.Petri.IPetriVertex, global::QuickGraph.Petri.IArc<Token>>.ContainsEdge(global::QuickGraph.Petri.IArc<Token> edge)
        {
            global::Microsoft.Stubs.StubDelegates
              .Func<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>, 
              global::QuickGraph.Petri.IArc<Token>, bool> sh = this.ContainsEdgeIArc0;
            if ((object)sh != (object)null)
              return sh.Invoke(this, edge);
            else 
            {
              global::Microsoft.Stubs
                .IDefaultStub<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>> stub
                 = ((global::Microsoft.Stubs
                  .IStub<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>>)this)
                  .DefaultStub;
              return stub.Result<bool>(this);
            }
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IImplicitGraph`2.IsOutEdgesEmpty(!0 v)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IImplicitGraph<global::QuickGraph.Petri.IPetriVertex, global::QuickGraph.Petri.IArc<Token>>.IsOutEdgesEmpty(global::QuickGraph.Petri.IPetriVertex v)
        {
            global::Microsoft.Stubs.StubDelegates
              .Func<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>, 
              global::QuickGraph.Petri.IPetriVertex, bool> sh = this.IsOutEdgesEmpty;
            if ((object)sh != (object)null)
              return sh.Invoke(this, v);
            else 
            {
              global::Microsoft.Stubs
                .IDefaultStub<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>> stub
                 = ((global::Microsoft.Stubs
                  .IStub<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>>)this)
                  .DefaultStub;
              return stub.Result<bool>(this);
            }
        }

        /// <summary>Stub of method System.Int32 QuickGraph.IImplicitGraph`2.OutDegree(!0 v)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        int global::QuickGraph.IImplicitGraph<global::QuickGraph.Petri.IPetriVertex, global::QuickGraph.Petri.IArc<Token>>.OutDegree(global::QuickGraph.Petri.IPetriVertex v)
        {
            global::Microsoft.Stubs.StubDelegates
              .Func<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>, 
              global::QuickGraph.Petri.IPetriVertex, int> sh = this.OutDegree;
            if ((object)sh != (object)null)
              return sh.Invoke(this, v);
            else 
            {
              global::Microsoft.Stubs
                .IDefaultStub<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>> stub
                 = ((global::Microsoft.Stubs
                  .IStub<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>>)this)
                  .DefaultStub;
              return stub.Result<int>(this);
            }
        }

        /// <summary>Stub of method !1 QuickGraph.IImplicitGraph`2.OutEdge(!0 v, System.Int32 index)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        global::QuickGraph.Petri.IArc<Token> global::QuickGraph.IImplicitGraph<global::QuickGraph.Petri.IPetriVertex, global::QuickGraph.Petri.IArc<Token>>.OutEdge(global::QuickGraph.Petri.IPetriVertex v, int index)
        {
            global::Microsoft.Stubs.StubDelegates
              .Func<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>, 
              global::QuickGraph.Petri.IPetriVertex, 
              int, global::QuickGraph.Petri.IArc<Token>> sh = this.OutEdge;
            if ((object)sh != (object)null)
              return sh.Invoke(this, v, index);
            else 
            {
              global::Microsoft.Stubs
                .IDefaultStub<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>> stub
                 = ((global::Microsoft.Stubs
                  .IStub<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>>)this)
                  .DefaultStub;
              return stub.Result<global::QuickGraph.Petri.IArc<Token>>(this);
            }
        }

        /// <summary>Stub of method System.Collections.Generic.IEnumerable`1&lt;!1&gt; QuickGraph.IImplicitGraph`2.OutEdges(!0 v)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        global::System.Collections.Generic.IEnumerable<global::QuickGraph.Petri.IArc<Token>> global::QuickGraph.IImplicitGraph<global::QuickGraph.Petri.IPetriVertex, global::QuickGraph.Petri.IArc<Token>>.OutEdges(global::QuickGraph.Petri.IPetriVertex v)
        {
            global::Microsoft.Stubs.StubDelegates
              .Func<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>, 
              global::QuickGraph.Petri.IPetriVertex, 
              global::System.Collections.Generic.IEnumerable
                  <global::QuickGraph.Petri.IArc<Token>>> sh = this.OutEdges;
            if ((object)sh != (object)null)
              return sh.Invoke(this, v);
            else 
            {
              global::Microsoft.Stubs
                .IDefaultStub<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>> stub
                 = ((global::Microsoft.Stubs
                  .IStub<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>>)this)
                  .DefaultStub;
              return stub.Result<global::System.Collections.Generic
                .IEnumerable<global::QuickGraph.Petri.IArc<Token>>>(this);
            }
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IImplicitGraph`2.TryGetOutEdges(!0 v, System.Collections.Generic.IEnumerable`1&lt;!1&gt;&amp; edges)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IImplicitGraph<global::QuickGraph.Petri.IPetriVertex, global::QuickGraph.Petri.IArc<Token>>.TryGetOutEdges(global::QuickGraph.Petri.IPetriVertex v, out global::System.Collections.Generic.IEnumerable<global::QuickGraph.Petri.IArc<Token>> edges)
        {
            global::Microsoft.Stubs.StubDelegates
              .OutFunc<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>, 
              global::QuickGraph.Petri.IPetriVertex, 
              global::System.Collections.Generic.IEnumerable
                  <global::QuickGraph.Petri.IArc<Token>>, bool> sh = this.TryGetOutEdges;
            if ((object)sh != (object)null)
              return sh.Invoke(this, v, out edges);
            else 
            {
              global::Microsoft.Stubs
                .IDefaultStub<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>> stub
                 = ((global::Microsoft.Stubs
                  .IStub<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>>)this)
                  .DefaultStub;
              stub.ValueAtReturn<global::System.Collections.Generic
                .IEnumerable<global::QuickGraph.Petri.IArc<Token>>>(this, out edges);
              return stub.Result<bool>(this);
            }
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IIncidenceGraph`2.ContainsEdge(!0 source, !0 target)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IIncidenceGraph<global::QuickGraph.Petri.IPetriVertex, global::QuickGraph.Petri.IArc<Token>>.ContainsEdge(global::QuickGraph.Petri.IPetriVertex source, global::QuickGraph.Petri.IPetriVertex target)
        {
            global::Microsoft.Stubs.StubDelegates
              .Func<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>, 
              global::QuickGraph.Petri.IPetriVertex, 
              global::QuickGraph.Petri.IPetriVertex, bool> sh = this.ContainsEdge;
            if ((object)sh != (object)null)
              return sh.Invoke(this, source, target);
            else 
            {
              global::Microsoft.Stubs
                .IDefaultStub<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>> stub
                 = ((global::Microsoft.Stubs
                  .IStub<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>>)this)
                  .DefaultStub;
              return stub.Result<bool>(this);
            }
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IIncidenceGraph`2.TryGetEdge(!0 source, !0 target, !1&amp; edge)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IIncidenceGraph<global::QuickGraph.Petri.IPetriVertex, global::QuickGraph.Petri.IArc<Token>>.TryGetEdge(
            global::QuickGraph.Petri.IPetriVertex source,
            global::QuickGraph.Petri.IPetriVertex target,
            out global::QuickGraph.Petri.IArc<Token> edge
        )
        {
            global::Microsoft.Stubs.StubDelegates
              .OutFunc<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>, 
              global::QuickGraph.Petri.IPetriVertex, 
              global::QuickGraph.Petri.IPetriVertex, 
              global::QuickGraph.Petri.IArc<Token>, bool> sh = this.TryGetEdge;
            if ((object)sh != (object)null)
              return sh.Invoke(this, source, target, out edge);
            else 
            {
              global::Microsoft.Stubs
                .IDefaultStub<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>> stub
                 = ((global::Microsoft.Stubs
                  .IStub<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>>)this)
                  .DefaultStub;
              stub.ValueAtReturn<global::QuickGraph.Petri.IArc<Token>>(this, out edge);
              return stub.Result<bool>(this);
            }
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IIncidenceGraph`2.TryGetEdges(!0 source, !0 target, System.Collections.Generic.IEnumerable`1&lt;!1&gt;&amp; edges)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IIncidenceGraph<global::QuickGraph.Petri.IPetriVertex, global::QuickGraph.Petri.IArc<Token>>.TryGetEdges(
            global::QuickGraph.Petri.IPetriVertex source,
            global::QuickGraph.Petri.IPetriVertex target,
            out global::System.Collections.Generic.IEnumerable<global::QuickGraph.Petri.IArc<Token>> edges
        )
        {
            global::Microsoft.Stubs.StubDelegates
              .OutFunc<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>, 
              global::QuickGraph.Petri.IPetriVertex, 
              global::QuickGraph.Petri.IPetriVertex, 
              global::System.Collections.Generic.IEnumerable
                  <global::QuickGraph.Petri.IArc<Token>>, bool> sh = this.TryGetEdges;
            if ((object)sh != (object)null)
              return sh.Invoke(this, source, target, out edges);
            else 
            {
              global::Microsoft.Stubs
                .IDefaultStub<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>> stub
                 = ((global::Microsoft.Stubs
                  .IStub<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>>)this)
                  .DefaultStub;
              stub.ValueAtReturn<global::System.Collections.Generic
                .IEnumerable<global::QuickGraph.Petri.IArc<Token>>>(this, out edges);
              return stub.Result<bool>(this);
            }
        }

        /// <summary>Stub of method System.Void QuickGraph.IMutableBidirectionalGraph`2.ClearEdges(!0 v)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        void global::QuickGraph.IMutableBidirectionalGraph<global::QuickGraph.Petri.IPetriVertex, global::QuickGraph.Petri.IArc<Token>>.ClearEdges(global::QuickGraph.Petri.IPetriVertex v)
        {
            global::Microsoft.Stubs.StubDelegates
              .Action<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>, 
              global::QuickGraph.Petri.IPetriVertex> sh = this.ClearEdges;
            if ((object)sh != (object)null)
              sh.Invoke(this, v);
            else 
            {
              global::Microsoft.Stubs
                .IDefaultStub<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>> stub
                 = ((global::Microsoft.Stubs
                  .IStub<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>>)this)
                  .DefaultStub;
              stub.VoidResult(this);
            }
        }

        /// <summary>Stub of method System.Void QuickGraph.IMutableBidirectionalGraph`2.ClearInEdges(!0 v)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        void global::QuickGraph.IMutableBidirectionalGraph<global::QuickGraph.Petri.IPetriVertex, global::QuickGraph.Petri.IArc<Token>>.ClearInEdges(global::QuickGraph.Petri.IPetriVertex v)
        {
            global::Microsoft.Stubs.StubDelegates
              .Action<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>, 
              global::QuickGraph.Petri.IPetriVertex> sh = this.ClearInEdges;
            if ((object)sh != (object)null)
              sh.Invoke(this, v);
            else 
            {
              global::Microsoft.Stubs
                .IDefaultStub<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>> stub
                 = ((global::Microsoft.Stubs
                  .IStub<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>>)this)
                  .DefaultStub;
              stub.VoidResult(this);
            }
        }

        /// <summary>Stub of method System.Int32 QuickGraph.IMutableBidirectionalGraph`2.RemoveInEdgeIf(!0 v, QuickGraph.EdgePredicate`2&lt;!0,!1&gt; edgePredicate)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        int global::QuickGraph.IMutableBidirectionalGraph<global::QuickGraph.Petri.IPetriVertex, global::QuickGraph.Petri.IArc<Token>>.RemoveInEdgeIf(global::QuickGraph.Petri.IPetriVertex v, global::QuickGraph.EdgePredicate<global::QuickGraph.Petri.IPetriVertex, global::QuickGraph.Petri.IArc<Token>> edgePredicate)
        {
            global::Microsoft.Stubs.StubDelegates
              .Func<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>, 
              global::QuickGraph.Petri.IPetriVertex, 
              global::QuickGraph.EdgePredicate
                  <global::QuickGraph.Petri.IPetriVertex, 
                  global::QuickGraph.Petri.IArc<Token>>, int> sh = this.RemoveInEdgeIf;
            if ((object)sh != (object)null)
              return sh.Invoke(this, v, edgePredicate);
            else 
            {
              global::Microsoft.Stubs
                .IDefaultStub<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>> stub
                 = ((global::Microsoft.Stubs
                  .IStub<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>>)this)
                  .DefaultStub;
              return stub.Result<int>(this);
            }
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IMutableEdgeListGraph`2.AddEdge(!1 edge)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IMutableEdgeListGraph<global::QuickGraph.Petri.IPetriVertex, global::QuickGraph.Petri.IArc<Token>>.AddEdge(global::QuickGraph.Petri.IArc<Token> edge)
        {
            global::Microsoft.Stubs.StubDelegates
              .Func<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>, 
              global::QuickGraph.Petri.IArc<Token>, bool> sh = this.AddEdge;
            if ((object)sh != (object)null)
              return sh.Invoke(this, edge);
            else 
            {
              global::Microsoft.Stubs
                .IDefaultStub<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>> stub
                 = ((global::Microsoft.Stubs
                  .IStub<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>>)this)
                  .DefaultStub;
              return stub.Result<bool>(this);
            }
        }

        /// <summary>Stub of method System.Int32 QuickGraph.IMutableEdgeListGraph`2.AddEdgeRange(System.Collections.Generic.IEnumerable`1&lt;!1&gt; edges)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        int global::QuickGraph.IMutableEdgeListGraph<global::QuickGraph.Petri.IPetriVertex, global::QuickGraph.Petri.IArc<Token>>.AddEdgeRange(global::System.Collections.Generic.IEnumerable<global::QuickGraph.Petri.IArc<Token>> edges)
        {
            global::Microsoft.Stubs.StubDelegates
              .Func<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>, 
              global::System.Collections.Generic.IEnumerable
                  <global::QuickGraph.Petri.IArc<Token>>, int> sh = this.AddEdgeRange;
            if ((object)sh != (object)null)
              return sh.Invoke(this, edges);
            else 
            {
              global::Microsoft.Stubs
                .IDefaultStub<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>> stub
                 = ((global::Microsoft.Stubs
                  .IStub<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>>)this)
                  .DefaultStub;
              return stub.Result<int>(this);
            }
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IMutableEdgeListGraph`2.RemoveEdge(!1 edge)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IMutableEdgeListGraph<global::QuickGraph.Petri.IPetriVertex, global::QuickGraph.Petri.IArc<Token>>.RemoveEdge(global::QuickGraph.Petri.IArc<Token> edge)
        {
            global::Microsoft.Stubs.StubDelegates
              .Func<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>, 
              global::QuickGraph.Petri.IArc<Token>, bool> sh = this.RemoveEdge;
            if ((object)sh != (object)null)
              return sh.Invoke(this, edge);
            else 
            {
              global::Microsoft.Stubs
                .IDefaultStub<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>> stub
                 = ((global::Microsoft.Stubs
                  .IStub<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>>)this)
                  .DefaultStub;
              return stub.Result<bool>(this);
            }
        }

        /// <summary>Stub of method System.Int32 QuickGraph.IMutableEdgeListGraph`2.RemoveEdgeIf(QuickGraph.EdgePredicate`2&lt;!0,!1&gt; predicate)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        int global::QuickGraph.IMutableEdgeListGraph<global::QuickGraph.Petri.IPetriVertex, global::QuickGraph.Petri.IArc<Token>>.RemoveEdgeIf(global::QuickGraph.EdgePredicate<global::QuickGraph.Petri.IPetriVertex, global::QuickGraph.Petri.IArc<Token>> predicate)
        {
            global::Microsoft.Stubs.StubDelegates
              .Func<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>, 
              global::QuickGraph.EdgePredicate
                  <global::QuickGraph.Petri.IPetriVertex, 
                  global::QuickGraph.Petri.IArc<Token>>, int> sh = this.RemoveEdgeIf;
            if ((object)sh != (object)null)
              return sh.Invoke(this, predicate);
            else 
            {
              global::Microsoft.Stubs
                .IDefaultStub<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>> stub
                 = ((global::Microsoft.Stubs
                  .IStub<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>>)this)
                  .DefaultStub;
              return stub.Result<int>(this);
            }
        }

        /// <summary>Stub of method System.Void QuickGraph.IMutableGraph`2.Clear()</summary>
        [global::System.Diagnostics.DebuggerHidden]
        void global::QuickGraph.IMutableGraph<global::QuickGraph.Petri.IPetriVertex, global::QuickGraph.Petri.IArc<Token>>.Clear()
        {
            global::Microsoft.Stubs.StubDelegates
              .Action<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>> sh = this.Clear;
            if ((object)sh != (object)null)
              sh.Invoke(this);
            else 
            {
              global::Microsoft.Stubs
                .IDefaultStub<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>> stub
                 = ((global::Microsoft.Stubs
                  .IStub<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>>)this)
                  .DefaultStub;
              stub.VoidResult(this);
            }
        }

        /// <summary>Stub of method System.Void QuickGraph.IMutableIncidenceGraph`2.ClearOutEdges(!0 v)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        void global::QuickGraph.IMutableIncidenceGraph<global::QuickGraph.Petri.IPetriVertex, global::QuickGraph.Petri.IArc<Token>>.ClearOutEdges(global::QuickGraph.Petri.IPetriVertex v)
        {
            global::Microsoft.Stubs.StubDelegates
              .Action<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>, 
              global::QuickGraph.Petri.IPetriVertex> sh = this.ClearOutEdges;
            if ((object)sh != (object)null)
              sh.Invoke(this, v);
            else 
            {
              global::Microsoft.Stubs
                .IDefaultStub<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>> stub
                 = ((global::Microsoft.Stubs
                  .IStub<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>>)this)
                  .DefaultStub;
              stub.VoidResult(this);
            }
        }

        /// <summary>Stub of method System.Int32 QuickGraph.IMutableIncidenceGraph`2.RemoveOutEdgeIf(!0 v, QuickGraph.EdgePredicate`2&lt;!0,!1&gt; predicate)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        int global::QuickGraph.IMutableIncidenceGraph<global::QuickGraph.Petri.IPetriVertex, global::QuickGraph.Petri.IArc<Token>>.RemoveOutEdgeIf(global::QuickGraph.Petri.IPetriVertex v, global::QuickGraph.EdgePredicate<global::QuickGraph.Petri.IPetriVertex, global::QuickGraph.Petri.IArc<Token>> predicate)
        {
            global::Microsoft.Stubs.StubDelegates
              .Func<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>, 
              global::QuickGraph.Petri.IPetriVertex, 
              global::QuickGraph.EdgePredicate
                  <global::QuickGraph.Petri.IPetriVertex, 
                  global::QuickGraph.Petri.IArc<Token>>, int> sh = this.RemoveOutEdgeIf;
            if ((object)sh != (object)null)
              return sh.Invoke(this, v, predicate);
            else 
            {
              global::Microsoft.Stubs
                .IDefaultStub<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>> stub
                 = ((global::Microsoft.Stubs
                  .IStub<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>>)this)
                  .DefaultStub;
              return stub.Result<int>(this);
            }
        }

        /// <summary>Stub of method System.Void QuickGraph.IMutableIncidenceGraph`2.TrimEdgeExcess()</summary>
        [global::System.Diagnostics.DebuggerHidden]
        void global::QuickGraph.IMutableIncidenceGraph<global::QuickGraph.Petri.IPetriVertex, global::QuickGraph.Petri.IArc<Token>>.TrimEdgeExcess()
        {
            global::Microsoft.Stubs.StubDelegates
              .Action<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>> sh
               = this.TrimEdgeExcess;
            if ((object)sh != (object)null)
              sh.Invoke(this);
            else 
            {
              global::Microsoft.Stubs
                .IDefaultStub<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>> stub
                 = ((global::Microsoft.Stubs
                  .IStub<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>>)this)
                  .DefaultStub;
              stub.VoidResult(this);
            }
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IMutableVertexAndEdgeSet`2.AddVerticesAndEdge(!1 edge)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IMutableVertexAndEdgeSet<global::QuickGraph.Petri.IPetriVertex, global::QuickGraph.Petri.IArc<Token>>.AddVerticesAndEdge(global::QuickGraph.Petri.IArc<Token> edge)
        {
            global::Microsoft.Stubs.StubDelegates
              .Func<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>, 
              global::QuickGraph.Petri.IArc<Token>, bool> sh = this.AddVerticesAndEdge;
            if ((object)sh != (object)null)
              return sh.Invoke(this, edge);
            else 
            {
              global::Microsoft.Stubs
                .IDefaultStub<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>> stub
                 = ((global::Microsoft.Stubs
                  .IStub<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>>)this)
                  .DefaultStub;
              return stub.Result<bool>(this);
            }
        }

        /// <summary>Stub of method System.Int32 QuickGraph.IMutableVertexAndEdgeSet`2.AddVerticesAndEdgeRange(System.Collections.Generic.IEnumerable`1&lt;!1&gt; edges)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        int global::QuickGraph.IMutableVertexAndEdgeSet<global::QuickGraph.Petri.IPetriVertex, global::QuickGraph.Petri.IArc<Token>>.AddVerticesAndEdgeRange(global::System.Collections.Generic.IEnumerable<global::QuickGraph.Petri.IArc<Token>> edges)
        {
            global::Microsoft.Stubs.StubDelegates
              .Func<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>, 
              global::System.Collections.Generic.IEnumerable
                  <global::QuickGraph.Petri.IArc<Token>>, int> sh = this.AddVerticesAndEdgeRange;
            if ((object)sh != (object)null)
              return sh.Invoke(this, edges);
            else 
            {
              global::Microsoft.Stubs
                .IDefaultStub<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>> stub
                 = ((global::Microsoft.Stubs
                  .IStub<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>>)this)
                  .DefaultStub;
              return stub.Result<int>(this);
            }
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IMutableVertexSet`1.AddVertex(!0 v)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IMutableVertexSet<global::QuickGraph.Petri.IPetriVertex>.AddVertex(global::QuickGraph.Petri.IPetriVertex v)
        {
            global::Microsoft.Stubs.StubDelegates
              .Func<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>, 
              global::QuickGraph.Petri.IPetriVertex, bool> sh = this.AddVertex;
            if ((object)sh != (object)null)
              return sh.Invoke(this, v);
            else 
            {
              global::Microsoft.Stubs
                .IDefaultStub<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>> stub
                 = ((global::Microsoft.Stubs
                  .IStub<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>>)this)
                  .DefaultStub;
              return stub.Result<bool>(this);
            }
        }

        /// <summary>Stub of method System.Int32 QuickGraph.IMutableVertexSet`1.AddVertexRange(System.Collections.Generic.IEnumerable`1&lt;!0&gt; vertices)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        int global::QuickGraph.IMutableVertexSet<global::QuickGraph.Petri.IPetriVertex>.AddVertexRange(global::System.Collections.Generic.IEnumerable<global::QuickGraph.Petri.IPetriVertex> vertices)
        {
            global::Microsoft.Stubs.StubDelegates
              .Func<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>, 
              global::System.Collections.Generic.IEnumerable
                  <global::QuickGraph.Petri.IPetriVertex>, int> sh = this.AddVertexRange;
            if ((object)sh != (object)null)
              return sh.Invoke(this, vertices);
            else 
            {
              global::Microsoft.Stubs
                .IDefaultStub<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>> stub
                 = ((global::Microsoft.Stubs
                  .IStub<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>>)this)
                  .DefaultStub;
              return stub.Result<int>(this);
            }
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IMutableVertexSet`1.RemoveVertex(!0 v)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IMutableVertexSet<global::QuickGraph.Petri.IPetriVertex>.RemoveVertex(global::QuickGraph.Petri.IPetriVertex v)
        {
            global::Microsoft.Stubs.StubDelegates
              .Func<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>, 
              global::QuickGraph.Petri.IPetriVertex, bool> sh = this.RemoveVertex;
            if ((object)sh != (object)null)
              return sh.Invoke(this, v);
            else 
            {
              global::Microsoft.Stubs
                .IDefaultStub<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>> stub
                 = ((global::Microsoft.Stubs
                  .IStub<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>>)this)
                  .DefaultStub;
              return stub.Result<bool>(this);
            }
        }

        /// <summary>Stub of method System.Int32 QuickGraph.IMutableVertexSet`1.RemoveVertexIf(QuickGraph.VertexPredicate`1&lt;!0&gt; pred)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        int global::QuickGraph.IMutableVertexSet<global::QuickGraph.Petri.IPetriVertex>.RemoveVertexIf(global::QuickGraph.VertexPredicate<global::QuickGraph.Petri.IPetriVertex> pred)
        {
            global::Microsoft.Stubs.StubDelegates
              .Func<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>, 
              global::QuickGraph.VertexPredicate
                  <global::QuickGraph.Petri.IPetriVertex>, int> sh = this.RemoveVertexIf;
            if ((object)sh != (object)null)
              return sh.Invoke(this, pred);
            else 
            {
              global::Microsoft.Stubs
                .IDefaultStub<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>> stub
                 = ((global::Microsoft.Stubs
                  .IStub<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>>)this)
                  .DefaultStub;
              return stub.Result<int>(this);
            }
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IVertexSet`1.ContainsVertex(!0 vertex)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IVertexSet<global::QuickGraph.Petri.IPetriVertex>.ContainsVertex(global::QuickGraph.Petri.IPetriVertex vertex)
        {
            global::Microsoft.Stubs.StubDelegates
              .Func<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>, 
              global::QuickGraph.Petri.IPetriVertex, bool> sh = this.ContainsVertex;
            if ((object)sh != (object)null)
              return sh.Invoke(this, vertex);
            else 
            {
              global::Microsoft.Stubs
                .IDefaultStub<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>> stub
                 = ((global::Microsoft.Stubs
                  .IStub<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>>)this)
                  .DefaultStub;
              return stub.Result<bool>(this);
            }
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IMutableEdgeListGraph`2.RemoveEdge(!1 edge)</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>, global::QuickGraph.Petri.IArc<Token>, bool> RemoveEdge;

        /// <summary>Stub of method System.Int32 QuickGraph.IMutableEdgeListGraph`2.RemoveEdgeIf(QuickGraph.EdgePredicate`2&lt;!0,!1&gt; predicate)</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>, global::QuickGraph.EdgePredicate<global::QuickGraph.Petri.IPetriVertex, global::QuickGraph.Petri.IArc<Token>>, int> RemoveEdgeIf;

        /// <summary>Stub of method System.Int32 QuickGraph.IMutableBidirectionalGraph`2.RemoveInEdgeIf(!0 v, QuickGraph.EdgePredicate`2&lt;!0,!1&gt; edgePredicate)</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>, global::QuickGraph.Petri.IPetriVertex, global::QuickGraph.EdgePredicate<global::QuickGraph.Petri.IPetriVertex, global::QuickGraph.Petri.IArc<Token>>, int> RemoveInEdgeIf;

        /// <summary>Stub of method System.Int32 QuickGraph.IMutableIncidenceGraph`2.RemoveOutEdgeIf(!0 v, QuickGraph.EdgePredicate`2&lt;!0,!1&gt; predicate)</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>, global::QuickGraph.Petri.IPetriVertex, global::QuickGraph.EdgePredicate<global::QuickGraph.Petri.IPetriVertex, global::QuickGraph.Petri.IArc<Token>>, int> RemoveOutEdgeIf;

        /// <summary>Stub of method System.Boolean QuickGraph.IMutableVertexSet`1.RemoveVertex(!0 v)</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>, global::QuickGraph.Petri.IPetriVertex, bool> RemoveVertex;

        /// <summary>Stub of method System.Int32 QuickGraph.IMutableVertexSet`1.RemoveVertexIf(QuickGraph.VertexPredicate`1&lt;!0&gt; pred)</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>, global::QuickGraph.VertexPredicate<global::QuickGraph.Petri.IPetriVertex>, int> RemoveVertexIf;

        /// <summary>Stub of method System.Void QuickGraph.IMutableIncidenceGraph`2.TrimEdgeExcess()</summary>
        public global::Microsoft.Stubs.StubDelegates.Action<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>> TrimEdgeExcess;

        /// <summary>Stub of method System.Boolean QuickGraph.IIncidenceGraph`2.TryGetEdge(!0 source, !0 target, !1&amp; edge)</summary>
        public global::Microsoft.Stubs.StubDelegates.OutFunc<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>, global::QuickGraph.Petri.IPetriVertex, global::QuickGraph.Petri.IPetriVertex, global::QuickGraph.Petri.IArc<Token>, bool> TryGetEdge;

        /// <summary>Stub of method System.Boolean QuickGraph.IIncidenceGraph`2.TryGetEdges(!0 source, !0 target, System.Collections.Generic.IEnumerable`1&lt;!1&gt;&amp; edges)</summary>
        public global::Microsoft.Stubs.StubDelegates.OutFunc<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>, global::QuickGraph.Petri.IPetriVertex, global::QuickGraph.Petri.IPetriVertex, global::System.Collections.Generic.IEnumerable<global::QuickGraph.Petri.IArc<Token>>, bool> TryGetEdges;

        /// <summary>Stub of method System.Boolean QuickGraph.IBidirectionalGraph`2.TryGetInEdges(!0 v, System.Collections.Generic.IEnumerable`1&lt;!1&gt;&amp; edges)</summary>
        public global::Microsoft.Stubs.StubDelegates.OutFunc<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>, global::QuickGraph.Petri.IPetriVertex, global::System.Collections.Generic.IEnumerable<global::QuickGraph.Petri.IArc<Token>>, bool> TryGetInEdges;

        /// <summary>Stub of method System.Boolean QuickGraph.IImplicitGraph`2.TryGetOutEdges(!0 v, System.Collections.Generic.IEnumerable`1&lt;!1&gt;&amp; edges)</summary>
        public global::Microsoft.Stubs.StubDelegates.OutFunc<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>, global::QuickGraph.Petri.IPetriVertex, global::System.Collections.Generic.IEnumerable<global::QuickGraph.Petri.IArc<Token>>, bool> TryGetOutEdges;

        /// <summary>Event VertexAdded</summary>
        public event global::QuickGraph.VertexAction<global::QuickGraph.Petri.IPetriVertex> VertexAdded;

        /// <summary>Stub of property QuickGraph.IVertexSet`1.VertexCount</summary>
        int global::QuickGraph.IVertexSet<global::QuickGraph.Petri.IPetriVertex>.VertexCount
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates
                  .Func<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>, int> sh
                   = this.VertexCountGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs
                    .IDefaultStub<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>> stub
                     = ((global::Microsoft.Stubs
                      .IStub<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>>)this)
                      .DefaultStub;
                  return stub.Result<int>(this);
                }
            }
        }

        /// <summary>Stub of method System.Int32 QuickGraph.IVertexSet`1.get_VertexCount()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>, int> VertexCountGet;

        /// <summary>Event VertexRemoved</summary>
        public event global::QuickGraph.VertexAction<global::QuickGraph.Petri.IPetriVertex> VertexRemoved;

        /// <summary>Stub of property QuickGraph.IVertexSet`1.Vertices</summary>
        global::System.Collections.Generic.IEnumerable<global::QuickGraph.Petri.IPetriVertex> global::QuickGraph.IVertexSet<global::QuickGraph.Petri.IPetriVertex>.Vertices
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates
                  .Func<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>, 
                  global::System.Collections.Generic.IEnumerable
                      <global::QuickGraph.Petri.IPetriVertex>> sh = this.VerticesGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs
                    .IDefaultStub<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>> stub
                     = ((global::Microsoft.Stubs
                      .IStub<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>>)this)
                      .DefaultStub;
                  return stub.Result<global::System.Collections.Generic
                    .IEnumerable<global::QuickGraph.Petri.IPetriVertex>>(this);
                }
            }
        }

        /// <summary>Stub of method System.Collections.Generic.IEnumerable`1&lt;!0&gt; QuickGraph.IVertexSet`1.get_Vertices()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Petri.Stubs.SPetriGraph<Token>, global::System.Collections.Generic.IEnumerable<global::QuickGraph.Petri.IPetriVertex>> VerticesGet;
    }
}
namespace QuickGraph.Petri.Stubs
{
    /// <summary>Stub of IPetriNet`1</summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Stubs", "0.1.2.3")]
    [global::System.Serializable]
    [global::System.Diagnostics.DebuggerStepThrough]
    public partial class SPetriNet<Token>
      : global::Microsoft.Stubs.StubBase<global::QuickGraph.Petri.Stubs.SPetriNet<Token>>
      , global::QuickGraph.Petri.IPetriNet<Token>
    {
        /// <summary>Initializes a new instance of type SPetriNet</summary>
        [global::System.Diagnostics.DebuggerHidden]
        public SPetriNet()
        {
        }

        /// <summary>Stub of property QuickGraph.Petri.IPetriNet`1.Arcs</summary>
        global::System.Collections.Generic.IList<global::QuickGraph.Petri.IArc<Token>> global::QuickGraph.Petri.IPetriNet<Token>.Arcs
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs
                  .StubDelegates.Func<global::QuickGraph.Petri.Stubs.SPetriNet<Token>, 
                  global::System.Collections.Generic
                    .IList<global::QuickGraph.Petri.IArc<Token>>> sh = this.ArcsGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs
                    .IDefaultStub<global::QuickGraph.Petri.Stubs.SPetriNet<Token>> stub
                     = ((global::Microsoft.Stubs
                      .IStub<global::QuickGraph.Petri.Stubs.SPetriNet<Token>>)this).DefaultStub;
                  return stub.Result<global::System.Collections.Generic
                    .IList<global::QuickGraph.Petri.IArc<Token>>>(this);
                }
            }
        }

        /// <summary>Stub of method System.Collections.Generic.IList`1&lt;QuickGraph.Petri.IArc`1&lt;!0&gt;&gt; QuickGraph.Petri.IPetriNet`1.get_Arcs()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Petri.Stubs.SPetriNet<Token>, global::System.Collections.Generic.IList<global::QuickGraph.Petri.IArc<Token>>> ArcsGet;

        /// <summary>Stub of property QuickGraph.Petri.IPetriNet`1.Graph</summary>
        global::QuickGraph.Petri.IPetriGraph<Token> global::QuickGraph.Petri.IPetriNet<Token>.Graph
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs
                  .StubDelegates.Func<global::QuickGraph.Petri.Stubs.SPetriNet<Token>, 
                  global::QuickGraph.Petri.IPetriGraph<Token>> sh = this.GraphGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs
                    .IDefaultStub<global::QuickGraph.Petri.Stubs.SPetriNet<Token>> stub
                     = ((global::Microsoft.Stubs
                      .IStub<global::QuickGraph.Petri.Stubs.SPetriNet<Token>>)this).DefaultStub;
                  return stub.Result<global::QuickGraph.Petri.IPetriGraph<Token>>(this);
                }
            }
        }

        /// <summary>Stub of method QuickGraph.Petri.IPetriGraph`1&lt;!0&gt; QuickGraph.Petri.IPetriNet`1.get_Graph()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Petri.Stubs.SPetriNet<Token>, global::QuickGraph.Petri.IPetriGraph<Token>> GraphGet;

        /// <summary>Stub of property QuickGraph.Petri.IPetriNet`1.Places</summary>
        global::System.Collections.Generic.IList<global::QuickGraph.Petri.IPlace<Token>> global::QuickGraph.Petri.IPetriNet<Token>.Places
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs
                  .StubDelegates.Func<global::QuickGraph.Petri.Stubs.SPetriNet<Token>, 
                  global::System.Collections.Generic
                    .IList<global::QuickGraph.Petri.IPlace<Token>>> sh = this.PlacesGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs
                    .IDefaultStub<global::QuickGraph.Petri.Stubs.SPetriNet<Token>> stub
                     = ((global::Microsoft.Stubs
                      .IStub<global::QuickGraph.Petri.Stubs.SPetriNet<Token>>)this).DefaultStub;
                  return stub.Result<global::System.Collections.Generic
                    .IList<global::QuickGraph.Petri.IPlace<Token>>>(this);
                }
            }
        }

        /// <summary>Stub of method System.Collections.Generic.IList`1&lt;QuickGraph.Petri.IPlace`1&lt;!0&gt;&gt; QuickGraph.Petri.IPetriNet`1.get_Places()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Petri.Stubs.SPetriNet<Token>, global::System.Collections.Generic.IList<global::QuickGraph.Petri.IPlace<Token>>> PlacesGet;

        /// <summary>Stub of property QuickGraph.Petri.IPetriNet`1.Transitions</summary>
        global::System.Collections.Generic.IList<global::QuickGraph.Petri.ITransition<Token>> global::QuickGraph.Petri.IPetriNet<Token>.Transitions
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs
                  .StubDelegates.Func<global::QuickGraph.Petri.Stubs.SPetriNet<Token>, 
                  global::System.Collections.Generic.IList<
                  global::QuickGraph.Petri.ITransition<Token>
                  >> sh = this.TransitionsGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs
                    .IDefaultStub<global::QuickGraph.Petri.Stubs.SPetriNet<Token>> stub
                     = ((global::Microsoft.Stubs
                      .IStub<global::QuickGraph.Petri.Stubs.SPetriNet<Token>>)this).DefaultStub;
                  return stub.Result<global::System.Collections.Generic
                    .IList<global::QuickGraph.Petri.ITransition<Token>>>(this);
                }
            }
        }

        /// <summary>Stub of method System.Collections.Generic.IList`1&lt;QuickGraph.Petri.ITransition`1&lt;!0&gt;&gt; QuickGraph.Petri.IPetriNet`1.get_Transitions()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Petri.Stubs.SPetriNet<Token>, global::System.Collections.Generic.IList<global::QuickGraph.Petri.ITransition<Token>>> TransitionsGet;
    }
}
namespace QuickGraph.Petri.Stubs
{
    /// <summary>Stub of IPetriVertex</summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Stubs", "0.1.2.3")]
    [global::System.Serializable]
    [global::System.Diagnostics.DebuggerStepThrough]
    public partial class SPetriVertex
      : global::Microsoft.Stubs.StubBase<global::QuickGraph.Petri.Stubs.SPetriVertex>
      , global::QuickGraph.Petri.IPetriVertex
    {
        /// <summary>Initializes a new instance of type SPetriVertex</summary>
        [global::System.Diagnostics.DebuggerHidden]
        public SPetriVertex()
        {
        }

        /// <summary>Stub of property QuickGraph.Petri.IPetriVertex.Name</summary>
        string global::QuickGraph.Petri.IPetriVertex.Name
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates
                  .Func<global::QuickGraph.Petri.Stubs.SPetriVertex, string> sh = this.NameGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs
                    .IDefaultStub<global::QuickGraph.Petri.Stubs.SPetriVertex> stub = ((
                  global::Microsoft.Stubs.IStub<global::QuickGraph.Petri.Stubs.SPetriVertex>
                  )this).DefaultStub;
                  return stub.Result<string>(this);
                }
            }
        }

        /// <summary>Stub of method System.String QuickGraph.Petri.IPetriVertex.get_Name()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Petri.Stubs.SPetriVertex, string> NameGet;
    }
}
namespace QuickGraph.Petri.Stubs
{
    /// <summary>Stub of IPlace`1</summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Stubs", "0.1.2.3")]
    [global::System.Serializable]
    [global::System.Diagnostics.DebuggerStepThrough]
    public partial class SPlace<Token>
      : global::Microsoft.Stubs.StubBase<global::QuickGraph.Petri.Stubs.SPlace<Token>>
      , global::QuickGraph.Petri.IPlace<Token>
    {
        /// <summary>Initializes a new instance of type SPlace</summary>
        [global::System.Diagnostics.DebuggerHidden]
        public SPlace()
        {
        }

        /// <summary>Stub of property QuickGraph.Petri.IPlace`1.Marking</summary>
        global::System.Collections.Generic.IList<Token> global::QuickGraph.Petri.IPlace<Token>.Marking
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs
                  .StubDelegates.Func<global::QuickGraph.Petri.Stubs.SPlace<Token>, 
                  global::System.Collections.Generic.IList<Token>> sh = this.MarkingGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs
                    .IDefaultStub<global::QuickGraph.Petri.Stubs.SPlace<Token>> stub
                     = ((global::Microsoft.Stubs
                      .IStub<global::QuickGraph.Petri.Stubs.SPlace<Token>>)this).DefaultStub;
                  return stub.Result<global::System.Collections.Generic.IList<Token>>(this);
                }
            }
        }

        /// <summary>Stub of method System.Collections.Generic.IList`1&lt;!0&gt; QuickGraph.Petri.IPlace`1.get_Marking()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Petri.Stubs.SPlace<Token>, global::System.Collections.Generic.IList<Token>> MarkingGet;

        /// <summary>Stub of property QuickGraph.Petri.IPetriVertex.Name</summary>
        string global::QuickGraph.Petri.IPetriVertex.Name
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates
                  .Func<global::QuickGraph.Petri.Stubs.SPlace<Token>, string> sh = this.NameGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs
                    .IDefaultStub<global::QuickGraph.Petri.Stubs.SPlace<Token>> stub
                     = ((global::Microsoft.Stubs
                      .IStub<global::QuickGraph.Petri.Stubs.SPlace<Token>>)this).DefaultStub;
                  return stub.Result<string>(this);
                }
            }
        }

        /// <summary>Stub of method System.String QuickGraph.Petri.IPetriVertex.get_Name()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Petri.Stubs.SPlace<Token>, string> NameGet;

        /// <summary>Stub of method System.String QuickGraph.Petri.IPlace`1.ToStringWithMarking()</summary>
        [global::System.Diagnostics.DebuggerHidden]
        string global::QuickGraph.Petri.IPlace<Token>.ToStringWithMarking()
        {
            global::Microsoft.Stubs.StubDelegates
              .Func<global::QuickGraph.Petri.Stubs.SPlace<Token>, string> sh
               = this.ToStringWithMarking;
            if ((object)sh != (object)null)
              return sh.Invoke(this);
            else 
            {
              global::Microsoft.Stubs
                .IDefaultStub<global::QuickGraph.Petri.Stubs.SPlace<Token>> stub
                 = ((global::Microsoft.Stubs
                  .IStub<global::QuickGraph.Petri.Stubs.SPlace<Token>>)this).DefaultStub;
              return stub.Result<string>(this);
            }
        }

        /// <summary>Stub of method System.String QuickGraph.Petri.IPlace`1.ToStringWithMarking()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Petri.Stubs.SPlace<Token>, string> ToStringWithMarking;
    }
}
namespace QuickGraph.Collections.Stubs
{
    /// <summary>Stub of IPriorityQueue`1</summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Stubs", "0.1.2.3")]
    [global::System.Serializable]
    [global::System.Diagnostics.DebuggerStepThrough]
    public partial class SPriorityQueue<T>
      : global::Microsoft.Stubs.StubBase<global::QuickGraph.Collections.Stubs.SPriorityQueue<T>>
      , global::QuickGraph.Collections.IPriorityQueue<T>
    {
        /// <summary>Initializes a new instance of type SPriorityQueue</summary>
        [global::System.Diagnostics.DebuggerHidden]
        public SPriorityQueue()
        {
        }

        /// <summary>Stub of property QuickGraph.Collections.IQueue`1.Count</summary>
        int global::QuickGraph.Collections.IQueue<T>.Count
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates.Func
                    <global::QuickGraph.Collections.Stubs.SPriorityQueue<T>, int> sh = this.CountGet
                  ;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs.IDefaultStub
                      <global::QuickGraph.Collections.Stubs.SPriorityQueue<T>> stub
                     = ((global::Microsoft.Stubs
                      .IStub<global::QuickGraph.Collections.Stubs.SPriorityQueue<T>>)this)
                      .DefaultStub;
                  return stub.Result<int>(this);
                }
            }
        }

        /// <summary>Stub of method System.Int32 QuickGraph.Collections.IQueue`1.get_Count()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Collections.Stubs.SPriorityQueue<T>, int> CountGet;

        /// <summary>Stub of method !0 QuickGraph.Collections.IQueue`1.Dequeue()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Collections.Stubs.SPriorityQueue<T>, T> Dequeue;

        /// <summary>Stub of method !0 QuickGraph.Collections.IQueue`1.Peek()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Collections.Stubs.SPriorityQueue<T>, T> Peek;

        /// <summary>Stub of method System.Void QuickGraph.Collections.IPriorityQueue`1.Update(!0 value)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        void global::QuickGraph.Collections.IPriorityQueue<T>.Update(T value)
        {
            global::Microsoft.Stubs
              .IDefaultStub<global::QuickGraph.Collections.Stubs.SPriorityQueue<T>> stub
               = ((global::Microsoft.Stubs
                .IStub<global::QuickGraph.Collections.Stubs.SPriorityQueue<T>>)this)
                .DefaultStub;
            stub.VoidResult(this);
        }

        /// <summary>Stub of method System.Boolean QuickGraph.Collections.IQueue`1.Contains(!0 value)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.Collections.IQueue<T>.Contains(T value)
        {
            global::Microsoft.Stubs
              .IDefaultStub<global::QuickGraph.Collections.Stubs.SPriorityQueue<T>> stub
               = ((global::Microsoft.Stubs
                .IStub<global::QuickGraph.Collections.Stubs.SPriorityQueue<T>>)this)
                .DefaultStub;
            return stub.Result<bool>(this);
        }

        /// <summary>Stub of method !0 QuickGraph.Collections.IQueue`1.Dequeue()</summary>
        [global::System.Diagnostics.DebuggerHidden]
        T global::QuickGraph.Collections.IQueue<T>.Dequeue()
        {
            global::Microsoft.Stubs.StubDelegates
              .Func<global::QuickGraph.Collections.Stubs.SPriorityQueue<T>, T> sh
               = this.Dequeue;
            if ((object)sh != (object)null)
              return sh.Invoke(this);
            else 
            {
              global::Microsoft.Stubs.IDefaultStub
                  <global::QuickGraph.Collections.Stubs.SPriorityQueue<T>> stub
                 = ((global::Microsoft.Stubs
                  .IStub<global::QuickGraph.Collections.Stubs.SPriorityQueue<T>>)this)
                  .DefaultStub;
              return stub.Result<T>(this);
            }
        }

        /// <summary>Stub of method System.Void QuickGraph.Collections.IQueue`1.Enqueue(!0 value)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        void global::QuickGraph.Collections.IQueue<T>.Enqueue(T value)
        {
            global::Microsoft.Stubs
              .IDefaultStub<global::QuickGraph.Collections.Stubs.SPriorityQueue<T>> stub
               = ((global::Microsoft.Stubs
                .IStub<global::QuickGraph.Collections.Stubs.SPriorityQueue<T>>)this)
                .DefaultStub;
            stub.VoidResult(this);
        }

        /// <summary>Stub of method !0 QuickGraph.Collections.IQueue`1.Peek()</summary>
        [global::System.Diagnostics.DebuggerHidden]
        T global::QuickGraph.Collections.IQueue<T>.Peek()
        {
            global::Microsoft.Stubs.StubDelegates
              .Func<global::QuickGraph.Collections.Stubs.SPriorityQueue<T>, T> sh = this.Peek;
            if ((object)sh != (object)null)
              return sh.Invoke(this);
            else 
            {
              global::Microsoft.Stubs.IDefaultStub
                  <global::QuickGraph.Collections.Stubs.SPriorityQueue<T>> stub
                 = ((global::Microsoft.Stubs
                  .IStub<global::QuickGraph.Collections.Stubs.SPriorityQueue<T>>)this)
                  .DefaultStub;
              return stub.Result<T>(this);
            }
        }

        /// <summary>Stub of method !0[] QuickGraph.Collections.IQueue`1.ToArray()</summary>
        [global::System.Diagnostics.DebuggerHidden]
        T[] global::QuickGraph.Collections.IQueue<T>.ToArray()
        {
            global::Microsoft.Stubs.StubDelegates.Func
                <global::QuickGraph.Collections.Stubs.SPriorityQueue<T>, T[]> sh = this.ToArray;
            if ((object)sh != (object)null)
              return sh.Invoke(this);
            else 
            {
              global::Microsoft.Stubs.IDefaultStub
                  <global::QuickGraph.Collections.Stubs.SPriorityQueue<T>> stub
                 = ((global::Microsoft.Stubs
                  .IStub<global::QuickGraph.Collections.Stubs.SPriorityQueue<T>>)this)
                  .DefaultStub;
              return stub.Result<T[]>(this);
            }
        }

        /// <summary>Stub of method !0[] QuickGraph.Collections.IQueue`1.ToArray()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Collections.Stubs.SPriorityQueue<T>, T[]> ToArray;
    }
}
namespace QuickGraph.Collections.Stubs
{
    /// <summary>Stub of IQueue`1</summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Stubs", "0.1.2.3")]
    [global::System.Serializable]
    [global::System.Diagnostics.DebuggerStepThrough]
    public partial class SQueue<T>
      : global::Microsoft.Stubs.StubBase<global::QuickGraph.Collections.Stubs.SQueue<T>>
      , global::QuickGraph.Collections.IQueue<T>
    {
        /// <summary>Initializes a new instance of type SQueue</summary>
        [global::System.Diagnostics.DebuggerHidden]
        public SQueue()
        {
        }

        /// <summary>Stub of property QuickGraph.Collections.IQueue`1.Count</summary>
        int global::QuickGraph.Collections.IQueue<T>.Count
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates
                  .Func<global::QuickGraph.Collections.Stubs.SQueue<T>, int> sh = this.CountGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs
                    .IDefaultStub<global::QuickGraph.Collections.Stubs.SQueue<T>> stub
                     = ((global::Microsoft.Stubs
                      .IStub<global::QuickGraph.Collections.Stubs.SQueue<T>>)this).DefaultStub;
                  return stub.Result<int>(this);
                }
            }
        }

        /// <summary>Stub of method System.Int32 QuickGraph.Collections.IQueue`1.get_Count()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Collections.Stubs.SQueue<T>, int> CountGet;

        /// <summary>Stub of method !0 QuickGraph.Collections.IQueue`1.Dequeue()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Collections.Stubs.SQueue<T>, T> Dequeue;

        /// <summary>Stub of method !0 QuickGraph.Collections.IQueue`1.Peek()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Collections.Stubs.SQueue<T>, T> Peek;

        /// <summary>Stub of method System.Boolean QuickGraph.Collections.IQueue`1.Contains(!0 value)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.Collections.IQueue<T>.Contains(T value)
        {
            global::Microsoft.Stubs
              .IDefaultStub<global::QuickGraph.Collections.Stubs.SQueue<T>> stub
               = ((global::Microsoft.Stubs
                .IStub<global::QuickGraph.Collections.Stubs.SQueue<T>>)this).DefaultStub;
            return stub.Result<bool>(this);
        }

        /// <summary>Stub of method !0 QuickGraph.Collections.IQueue`1.Dequeue()</summary>
        [global::System.Diagnostics.DebuggerHidden]
        T global::QuickGraph.Collections.IQueue<T>.Dequeue()
        {
            global::Microsoft.Stubs.StubDelegates
              .Func<global::QuickGraph.Collections.Stubs.SQueue<T>, T> sh = this.Dequeue;
            if ((object)sh != (object)null)
              return sh.Invoke(this);
            else 
            {
              global::Microsoft.Stubs
                .IDefaultStub<global::QuickGraph.Collections.Stubs.SQueue<T>> stub
                 = ((global::Microsoft.Stubs
                  .IStub<global::QuickGraph.Collections.Stubs.SQueue<T>>)this).DefaultStub;
              return stub.Result<T>(this);
            }
        }

        /// <summary>Stub of method System.Void QuickGraph.Collections.IQueue`1.Enqueue(!0 value)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        void global::QuickGraph.Collections.IQueue<T>.Enqueue(T value)
        {
            global::Microsoft.Stubs
              .IDefaultStub<global::QuickGraph.Collections.Stubs.SQueue<T>> stub
               = ((global::Microsoft.Stubs
                .IStub<global::QuickGraph.Collections.Stubs.SQueue<T>>)this).DefaultStub;
            stub.VoidResult(this);
        }

        /// <summary>Stub of method !0 QuickGraph.Collections.IQueue`1.Peek()</summary>
        [global::System.Diagnostics.DebuggerHidden]
        T global::QuickGraph.Collections.IQueue<T>.Peek()
        {
            global::Microsoft.Stubs.StubDelegates
              .Func<global::QuickGraph.Collections.Stubs.SQueue<T>, T> sh = this.Peek;
            if ((object)sh != (object)null)
              return sh.Invoke(this);
            else 
            {
              global::Microsoft.Stubs
                .IDefaultStub<global::QuickGraph.Collections.Stubs.SQueue<T>> stub
                 = ((global::Microsoft.Stubs
                  .IStub<global::QuickGraph.Collections.Stubs.SQueue<T>>)this).DefaultStub;
              return stub.Result<T>(this);
            }
        }

        /// <summary>Stub of method !0[] QuickGraph.Collections.IQueue`1.ToArray()</summary>
        [global::System.Diagnostics.DebuggerHidden]
        T[] global::QuickGraph.Collections.IQueue<T>.ToArray()
        {
            global::Microsoft.Stubs.StubDelegates
              .Func<global::QuickGraph.Collections.Stubs.SQueue<T>, T[]> sh = this.ToArray;
            if ((object)sh != (object)null)
              return sh.Invoke(this);
            else 
            {
              global::Microsoft.Stubs
                .IDefaultStub<global::QuickGraph.Collections.Stubs.SQueue<T>> stub
                 = ((global::Microsoft.Stubs
                  .IStub<global::QuickGraph.Collections.Stubs.SQueue<T>>)this).DefaultStub;
              return stub.Result<T[]>(this);
            }
        }

        /// <summary>Stub of method !0[] QuickGraph.Collections.IQueue`1.ToArray()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Collections.Stubs.SQueue<T>, T[]> ToArray;
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
namespace QuickGraph.Petri.Stubs
{
    /// <summary>Stub of ITransition`1</summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Stubs", "0.1.2.3")]
    [global::System.Serializable]
    [global::System.Diagnostics.DebuggerStepThrough]
    public partial class STransition<Token>
      : global::Microsoft.Stubs.StubBase<global::QuickGraph.Petri.Stubs.STransition<Token>>
      , global::QuickGraph.Petri.ITransition<Token>
    {
        /// <summary>Initializes a new instance of type STransition</summary>
        [global::System.Diagnostics.DebuggerHidden]
        public STransition()
        {
        }

        /// <summary>Stub of property QuickGraph.Petri.ITransition`1.Condition</summary>
        global::QuickGraph.Petri.IConditionExpression<Token> global::QuickGraph.Petri.ITransition<Token>.Condition
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates
                  .Func<global::QuickGraph.Petri.Stubs.STransition<Token>, 
                  global::QuickGraph.Petri.IConditionExpression<Token>> sh = this.ConditionGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs
                    .IDefaultStub<global::QuickGraph.Petri.Stubs.STransition<Token>> stub
                     = ((global::Microsoft.Stubs
                      .IStub<global::QuickGraph.Petri.Stubs.STransition<Token>>)this)
                      .DefaultStub;
                  return stub.Result<global::QuickGraph.Petri.IConditionExpression<Token>>(this);
                }
            }
            [global::System.Diagnostics.DebuggerHidden]
            set
            {
                global::Microsoft.Stubs.StubDelegates
                  .Action<global::QuickGraph.Petri.Stubs.STransition<Token>, 
                  global::QuickGraph.Petri.IConditionExpression<Token>> sh = this.ConditionSet;
                if ((object)sh != (object)null)
                  sh.Invoke(this, value);
                else 
                {
                  global::Microsoft.Stubs
                    .IDefaultStub<global::QuickGraph.Petri.Stubs.STransition<Token>> stub
                     = ((global::Microsoft.Stubs
                      .IStub<global::QuickGraph.Petri.Stubs.STransition<Token>>)this)
                      .DefaultStub;
                  stub.VoidResult(this);
                }
            }
        }

        /// <summary>Stub of method QuickGraph.Petri.IConditionExpression`1&lt;!0&gt; QuickGraph.Petri.ITransition`1.get_Condition()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Petri.Stubs.STransition<Token>, global::QuickGraph.Petri.IConditionExpression<Token>> ConditionGet;

        /// <summary>Stub of method System.Void QuickGraph.Petri.ITransition`1.set_Condition(QuickGraph.Petri.IConditionExpression`1&lt;!0&gt; value)</summary>
        public global::Microsoft.Stubs.StubDelegates.Action<global::QuickGraph.Petri.Stubs.STransition<Token>, global::QuickGraph.Petri.IConditionExpression<Token>> ConditionSet;

        /// <summary>Stub of property QuickGraph.Petri.IPetriVertex.Name</summary>
        string global::QuickGraph.Petri.IPetriVertex.Name
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates
                  .Func<global::QuickGraph.Petri.Stubs.STransition<Token>, string> sh
                   = this.NameGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs
                    .IDefaultStub<global::QuickGraph.Petri.Stubs.STransition<Token>> stub
                     = ((global::Microsoft.Stubs
                      .IStub<global::QuickGraph.Petri.Stubs.STransition<Token>>)this)
                      .DefaultStub;
                  return stub.Result<string>(this);
                }
            }
        }

        /// <summary>Stub of method System.String QuickGraph.Petri.IPetriVertex.get_Name()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Petri.Stubs.STransition<Token>, string> NameGet;
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
    /// <summary>Stub of IUndirectedGraph`2</summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Stubs", "0.1.2.3")]
    [global::System.Serializable]
    [global::System.Diagnostics.DebuggerStepThrough]
    public partial class SUndirectedGraph<TVertex,TEdge>
      : global::Microsoft.Stubs.StubBase<global::QuickGraph.Stubs.SUndirectedGraph<TVertex, TEdge>>
      , global::QuickGraph.IUndirectedGraph<TVertex, TEdge>
        where TEdge : global::QuickGraph.IEdge<TVertex>
    {
        /// <summary>Initializes a new instance of type SUndirectedGraph</summary>
        [global::System.Diagnostics.DebuggerHidden]
        public SUndirectedGraph()
        {
        }

        /// <summary>Stub of property QuickGraph.IGraph`2.AllowParallelEdges</summary>
        bool global::QuickGraph.IGraph<TVertex, TEdge>.AllowParallelEdges
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates
                  .Func<global::QuickGraph.Stubs.SUndirectedGraph<TVertex, TEdge>, 
                  bool> sh = this.AllowParallelEdgesGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs.IDefaultStub
                      <global::QuickGraph.Stubs.SUndirectedGraph<TVertex, TEdge>> stub
                     = ((global::Microsoft.Stubs.IStub
                        <global::QuickGraph.Stubs.SUndirectedGraph<TVertex, TEdge>
                        >)this).DefaultStub;
                  return stub.Result<bool>(this);
                }
            }
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IGraph`2.get_AllowParallelEdges()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SUndirectedGraph<TVertex, TEdge>, bool> AllowParallelEdgesGet;

        /// <summary>Stub of property QuickGraph.IEdgeSet`2.EdgeCount</summary>
        int global::QuickGraph.IEdgeSet<TVertex, TEdge>.EdgeCount
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates
                  .Func<global::QuickGraph.Stubs.SUndirectedGraph<TVertex, TEdge>, 
                  int> sh = this.EdgeCountGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs.IDefaultStub
                      <global::QuickGraph.Stubs.SUndirectedGraph<TVertex, TEdge>> stub
                     = ((global::Microsoft.Stubs.IStub
                        <global::QuickGraph.Stubs.SUndirectedGraph<TVertex, TEdge>
                        >)this).DefaultStub;
                  return stub.Result<int>(this);
                }
            }
        }

        /// <summary>Stub of method System.Int32 QuickGraph.IEdgeSet`2.get_EdgeCount()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SUndirectedGraph<TVertex, TEdge>, int> EdgeCountGet;

        /// <summary>Stub of property QuickGraph.IEdgeSet`2.Edges</summary>
        global::System.Collections.Generic.IEnumerable<TEdge> global::QuickGraph.IEdgeSet<TVertex, TEdge>.Edges
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates
                  .Func<global::QuickGraph.Stubs.SUndirectedGraph<TVertex, TEdge>, 
                  global::System.Collections.Generic.IEnumerable<TEdge>> sh = this.EdgesGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs.IDefaultStub
                      <global::QuickGraph.Stubs.SUndirectedGraph<TVertex, TEdge>> stub
                     = ((global::Microsoft.Stubs.IStub
                        <global::QuickGraph.Stubs.SUndirectedGraph<TVertex, TEdge>
                        >)this).DefaultStub;
                  return 
                    stub.Result<global::System.Collections.Generic.IEnumerable<TEdge>>(this);
                }
            }
        }

        /// <summary>Stub of method System.Collections.Generic.IEnumerable`1&lt;!1&gt; QuickGraph.IEdgeSet`2.get_Edges()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SUndirectedGraph<TVertex, TEdge>, global::System.Collections.Generic.IEnumerable<TEdge>> EdgesGet;

        /// <summary>Stub of property QuickGraph.IGraph`2.IsDirected</summary>
        bool global::QuickGraph.IGraph<TVertex, TEdge>.IsDirected
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates
                  .Func<global::QuickGraph.Stubs.SUndirectedGraph<TVertex, TEdge>, 
                  bool> sh = this.IsDirectedGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs.IDefaultStub
                      <global::QuickGraph.Stubs.SUndirectedGraph<TVertex, TEdge>> stub
                     = ((global::Microsoft.Stubs.IStub
                        <global::QuickGraph.Stubs.SUndirectedGraph<TVertex, TEdge>
                        >)this).DefaultStub;
                  return stub.Result<bool>(this);
                }
            }
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IGraph`2.get_IsDirected()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SUndirectedGraph<TVertex, TEdge>, bool> IsDirectedGet;

        /// <summary>Stub of property QuickGraph.IEdgeSet`2.IsEdgesEmpty</summary>
        bool global::QuickGraph.IEdgeSet<TVertex, TEdge>.IsEdgesEmpty
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates
                  .Func<global::QuickGraph.Stubs.SUndirectedGraph<TVertex, TEdge>, 
                  bool> sh = this.IsEdgesEmptyGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs.IDefaultStub
                      <global::QuickGraph.Stubs.SUndirectedGraph<TVertex, TEdge>> stub
                     = ((global::Microsoft.Stubs.IStub
                        <global::QuickGraph.Stubs.SUndirectedGraph<TVertex, TEdge>
                        >)this).DefaultStub;
                  return stub.Result<bool>(this);
                }
            }
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IEdgeSet`2.get_IsEdgesEmpty()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SUndirectedGraph<TVertex, TEdge>, bool> IsEdgesEmptyGet;

        /// <summary>Stub of property QuickGraph.IVertexSet`1.IsVerticesEmpty</summary>
        bool global::QuickGraph.IVertexSet<TVertex>.IsVerticesEmpty
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates
                  .Func<global::QuickGraph.Stubs.SUndirectedGraph<TVertex, TEdge>, 
                  bool> sh = this.IsVerticesEmptyGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs.IDefaultStub
                      <global::QuickGraph.Stubs.SUndirectedGraph<TVertex, TEdge>> stub
                     = ((global::Microsoft.Stubs.IStub
                        <global::QuickGraph.Stubs.SUndirectedGraph<TVertex, TEdge>
                        >)this).DefaultStub;
                  return stub.Result<bool>(this);
                }
            }
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IVertexSet`1.get_IsVerticesEmpty()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SUndirectedGraph<TVertex, TEdge>, bool> IsVerticesEmptyGet;

        /// <summary>Stub of method System.Boolean QuickGraph.IEdgeSet`2.ContainsEdge(!1 edge)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IEdgeSet<TVertex, TEdge>.ContainsEdge(TEdge edge)
        {
            global::Microsoft.Stubs.IDefaultStub
                <global::QuickGraph.Stubs.SUndirectedGraph<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs
                .IStub<global::QuickGraph.Stubs.SUndirectedGraph<TVertex, TEdge>>)this)
                .DefaultStub;
            return stub.Result<bool>(this);
        }

        /// <summary>Stub of method System.Int32 QuickGraph.IUndirectedGraph`2.AdjacentDegree(!0 v)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        int global::QuickGraph.IUndirectedGraph<TVertex, TEdge>.AdjacentDegree(TVertex v)
        {
            global::Microsoft.Stubs.IDefaultStub
                <global::QuickGraph.Stubs.SUndirectedGraph<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs
                .IStub<global::QuickGraph.Stubs.SUndirectedGraph<TVertex, TEdge>>)this)
                .DefaultStub;
            return stub.Result<int>(this);
        }

        /// <summary>Stub of method !1 QuickGraph.IUndirectedGraph`2.AdjacentEdge(!0 v, System.Int32 index)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        TEdge global::QuickGraph.IUndirectedGraph<TVertex, TEdge>.AdjacentEdge(TVertex v, int index)
        {
            global::Microsoft.Stubs.IDefaultStub
                <global::QuickGraph.Stubs.SUndirectedGraph<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs
                .IStub<global::QuickGraph.Stubs.SUndirectedGraph<TVertex, TEdge>>)this)
                .DefaultStub;
            return stub.Result<TEdge>(this);
        }

        /// <summary>Stub of method System.Collections.Generic.IEnumerable`1&lt;!1&gt; QuickGraph.IUndirectedGraph`2.AdjacentEdges(!0 v)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        global::System.Collections.Generic.IEnumerable<TEdge> global::QuickGraph.IUndirectedGraph<TVertex, TEdge>.AdjacentEdges(TVertex v)
        {
            global::Microsoft.Stubs.IDefaultStub
                <global::QuickGraph.Stubs.SUndirectedGraph<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs
                .IStub<global::QuickGraph.Stubs.SUndirectedGraph<TVertex, TEdge>>)this)
                .DefaultStub;
            return stub.Result<global::System.Collections.Generic.IEnumerable<TEdge>>(this);
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IUndirectedGraph`2.ContainsEdge(!0 source, !0 target)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IUndirectedGraph<TVertex, TEdge>.ContainsEdge(TVertex source, TVertex target)
        {
            global::Microsoft.Stubs.IDefaultStub
                <global::QuickGraph.Stubs.SUndirectedGraph<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs
                .IStub<global::QuickGraph.Stubs.SUndirectedGraph<TVertex, TEdge>>)this)
                .DefaultStub;
            return stub.Result<bool>(this);
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IUndirectedGraph`2.IsAdjacentEdgesEmpty(!0 v)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IUndirectedGraph<TVertex, TEdge>.IsAdjacentEdgesEmpty(TVertex v)
        {
            global::Microsoft.Stubs.IDefaultStub
                <global::QuickGraph.Stubs.SUndirectedGraph<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs
                .IStub<global::QuickGraph.Stubs.SUndirectedGraph<TVertex, TEdge>>)this)
                .DefaultStub;
            return stub.Result<bool>(this);
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IVertexSet`1.ContainsVertex(!0 vertex)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IVertexSet<TVertex>.ContainsVertex(TVertex vertex)
        {
            global::Microsoft.Stubs.IDefaultStub
                <global::QuickGraph.Stubs.SUndirectedGraph<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs
                .IStub<global::QuickGraph.Stubs.SUndirectedGraph<TVertex, TEdge>>)this)
                .DefaultStub;
            return stub.Result<bool>(this);
        }

        /// <summary>Stub of property QuickGraph.IVertexSet`1.VertexCount</summary>
        int global::QuickGraph.IVertexSet<TVertex>.VertexCount
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates
                  .Func<global::QuickGraph.Stubs.SUndirectedGraph<TVertex, TEdge>, 
                  int> sh = this.VertexCountGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs.IDefaultStub
                      <global::QuickGraph.Stubs.SUndirectedGraph<TVertex, TEdge>> stub
                     = ((global::Microsoft.Stubs.IStub
                        <global::QuickGraph.Stubs.SUndirectedGraph<TVertex, TEdge>
                        >)this).DefaultStub;
                  return stub.Result<int>(this);
                }
            }
        }

        /// <summary>Stub of method System.Int32 QuickGraph.IVertexSet`1.get_VertexCount()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SUndirectedGraph<TVertex, TEdge>, int> VertexCountGet;

        /// <summary>Stub of property QuickGraph.IVertexSet`1.Vertices</summary>
        global::System.Collections.Generic.IEnumerable<TVertex> global::QuickGraph.IVertexSet<TVertex>.Vertices
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates
                  .Func<global::QuickGraph.Stubs.SUndirectedGraph<TVertex, TEdge>, 
                  global::System.Collections.Generic.IEnumerable<TVertex>> sh = this.VerticesGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs.IDefaultStub
                      <global::QuickGraph.Stubs.SUndirectedGraph<TVertex, TEdge>> stub
                     = ((global::Microsoft.Stubs.IStub
                        <global::QuickGraph.Stubs.SUndirectedGraph<TVertex, TEdge>
                        >)this).DefaultStub;
                  return 
                    stub.Result<global::System.Collections.Generic.IEnumerable<TVertex>>(this);
                }
            }
        }

        /// <summary>Stub of method System.Collections.Generic.IEnumerable`1&lt;!0&gt; QuickGraph.IVertexSet`1.get_Vertices()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SUndirectedGraph<TVertex, TEdge>, global::System.Collections.Generic.IEnumerable<TVertex>> VerticesGet;
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
        public event global::QuickGraph.UndirectedEdgeEventHandler<TVertex, TEdge> TreeEdge;
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
        public event global::QuickGraph.UndirectedEdgeEventHandler<TVertex, TEdge> TreeEdge;
    }
}
namespace QuickGraph.Stubs
{
    /// <summary>Stub of IVertexAndEdgeListGraph`2</summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Stubs", "0.1.2.3")]
    [global::System.Serializable]
    [global::System.Diagnostics.DebuggerStepThrough]
    public partial class SVertexAndEdgeListGraph<TVertex,TEdge>
      : global::Microsoft.Stubs.StubBase<global::QuickGraph.Stubs.SVertexAndEdgeListGraph<TVertex, TEdge>>
      , global::QuickGraph.IVertexAndEdgeListGraph<TVertex, TEdge>
        where TEdge : global::QuickGraph.IEdge<TVertex>
    {
        /// <summary>Initializes a new instance of type SVertexAndEdgeListGraph</summary>
        [global::System.Diagnostics.DebuggerHidden]
        public SVertexAndEdgeListGraph()
        {
        }

        /// <summary>Stub of property QuickGraph.IGraph`2.AllowParallelEdges</summary>
        bool global::QuickGraph.IGraph<TVertex, TEdge>.AllowParallelEdges
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs
                  .SVertexAndEdgeListGraph<TVertex, TEdge>, bool> sh = this.AllowParallelEdgesGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
                    .SVertexAndEdgeListGraph<TVertex, TEdge>> stub
                     = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                      .SVertexAndEdgeListGraph<TVertex, TEdge>>)this).DefaultStub;
                  return stub.Result<bool>(this);
                }
            }
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IGraph`2.get_AllowParallelEdges()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SVertexAndEdgeListGraph<TVertex, TEdge>, bool> AllowParallelEdgesGet;

        /// <summary>Stub of property QuickGraph.IEdgeSet`2.EdgeCount</summary>
        int global::QuickGraph.IEdgeSet<TVertex, TEdge>.EdgeCount
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs
                  .SVertexAndEdgeListGraph<TVertex, TEdge>, int> sh = this.EdgeCountGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
                    .SVertexAndEdgeListGraph<TVertex, TEdge>> stub
                     = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                      .SVertexAndEdgeListGraph<TVertex, TEdge>>)this).DefaultStub;
                  return stub.Result<int>(this);
                }
            }
        }

        /// <summary>Stub of method System.Int32 QuickGraph.IEdgeSet`2.get_EdgeCount()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SVertexAndEdgeListGraph<TVertex, TEdge>, int> EdgeCountGet;

        /// <summary>Stub of property QuickGraph.IEdgeSet`2.Edges</summary>
        global::System.Collections.Generic.IEnumerable<TEdge> global::QuickGraph.IEdgeSet<TVertex, TEdge>.Edges
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs
                  .SVertexAndEdgeListGraph<TVertex, TEdge>, 
                global::System.Collections.Generic.IEnumerable<TEdge>> sh = this.EdgesGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
                    .SVertexAndEdgeListGraph<TVertex, TEdge>> stub
                     = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                      .SVertexAndEdgeListGraph<TVertex, TEdge>>)this).DefaultStub;
                  return 
                    stub.Result<global::System.Collections.Generic.IEnumerable<TEdge>>(this);
                }
            }
        }

        /// <summary>Stub of method System.Collections.Generic.IEnumerable`1&lt;!1&gt; QuickGraph.IEdgeSet`2.get_Edges()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SVertexAndEdgeListGraph<TVertex, TEdge>, global::System.Collections.Generic.IEnumerable<TEdge>> EdgesGet;

        /// <summary>Stub of property QuickGraph.IGraph`2.IsDirected</summary>
        bool global::QuickGraph.IGraph<TVertex, TEdge>.IsDirected
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs
                  .SVertexAndEdgeListGraph<TVertex, TEdge>, bool> sh = this.IsDirectedGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
                    .SVertexAndEdgeListGraph<TVertex, TEdge>> stub
                     = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                      .SVertexAndEdgeListGraph<TVertex, TEdge>>)this).DefaultStub;
                  return stub.Result<bool>(this);
                }
            }
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IGraph`2.get_IsDirected()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SVertexAndEdgeListGraph<TVertex, TEdge>, bool> IsDirectedGet;

        /// <summary>Stub of property QuickGraph.IEdgeSet`2.IsEdgesEmpty</summary>
        bool global::QuickGraph.IEdgeSet<TVertex, TEdge>.IsEdgesEmpty
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs
                  .SVertexAndEdgeListGraph<TVertex, TEdge>, bool> sh = this.IsEdgesEmptyGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
                    .SVertexAndEdgeListGraph<TVertex, TEdge>> stub
                     = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                      .SVertexAndEdgeListGraph<TVertex, TEdge>>)this).DefaultStub;
                  return stub.Result<bool>(this);
                }
            }
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IEdgeSet`2.get_IsEdgesEmpty()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SVertexAndEdgeListGraph<TVertex, TEdge>, bool> IsEdgesEmptyGet;

        /// <summary>Stub of property QuickGraph.IVertexSet`1.IsVerticesEmpty</summary>
        bool global::QuickGraph.IVertexSet<TVertex>.IsVerticesEmpty
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs
                  .SVertexAndEdgeListGraph<TVertex, TEdge>, bool> sh = this.IsVerticesEmptyGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
                    .SVertexAndEdgeListGraph<TVertex, TEdge>> stub
                     = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                      .SVertexAndEdgeListGraph<TVertex, TEdge>>)this).DefaultStub;
                  return stub.Result<bool>(this);
                }
            }
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IVertexSet`1.get_IsVerticesEmpty()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SVertexAndEdgeListGraph<TVertex, TEdge>, bool> IsVerticesEmptyGet;

        /// <summary>Stub of method System.Boolean QuickGraph.IEdgeSet`2.ContainsEdge(!1 edge)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IEdgeSet<TVertex, TEdge>.ContainsEdge(TEdge edge)
        {
            global::Microsoft.Stubs.IDefaultStub<
            global::QuickGraph.Stubs.SVertexAndEdgeListGraph<TVertex, TEdge>
            > stub = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
              .SVertexAndEdgeListGraph<TVertex, TEdge>>)this).DefaultStub;
            return stub.Result<bool>(this);
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IImplicitGraph`2.IsOutEdgesEmpty(!0 v)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IImplicitGraph<TVertex, TEdge>.IsOutEdgesEmpty(TVertex v)
        {
            global::Microsoft.Stubs.IDefaultStub<
            global::QuickGraph.Stubs.SVertexAndEdgeListGraph<TVertex, TEdge>
            > stub = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
              .SVertexAndEdgeListGraph<TVertex, TEdge>>)this).DefaultStub;
            return stub.Result<bool>(this);
        }

        /// <summary>Stub of method System.Int32 QuickGraph.IImplicitGraph`2.OutDegree(!0 v)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        int global::QuickGraph.IImplicitGraph<TVertex, TEdge>.OutDegree(TVertex v)
        {
            global::Microsoft.Stubs.IDefaultStub<
            global::QuickGraph.Stubs.SVertexAndEdgeListGraph<TVertex, TEdge>
            > stub = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
              .SVertexAndEdgeListGraph<TVertex, TEdge>>)this).DefaultStub;
            return stub.Result<int>(this);
        }

        /// <summary>Stub of method !1 QuickGraph.IImplicitGraph`2.OutEdge(!0 v, System.Int32 index)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        TEdge global::QuickGraph.IImplicitGraph<TVertex, TEdge>.OutEdge(TVertex v, int index)
        {
            global::Microsoft.Stubs.IDefaultStub<
            global::QuickGraph.Stubs.SVertexAndEdgeListGraph<TVertex, TEdge>
            > stub = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
              .SVertexAndEdgeListGraph<TVertex, TEdge>>)this).DefaultStub;
            return stub.Result<TEdge>(this);
        }

        /// <summary>Stub of method System.Collections.Generic.IEnumerable`1&lt;!1&gt; QuickGraph.IImplicitGraph`2.OutEdges(!0 v)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        global::System.Collections.Generic.IEnumerable<TEdge> global::QuickGraph.IImplicitGraph<TVertex, TEdge>.OutEdges(TVertex v)
        {
            global::Microsoft.Stubs.IDefaultStub<
            global::QuickGraph.Stubs.SVertexAndEdgeListGraph<TVertex, TEdge>
            > stub = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
              .SVertexAndEdgeListGraph<TVertex, TEdge>>)this).DefaultStub;
            return stub.Result<global::System.Collections.Generic.IEnumerable<TEdge>>(this);
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IImplicitGraph`2.TryGetOutEdges(!0 v, System.Collections.Generic.IEnumerable`1&lt;!1&gt;&amp; edges)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IImplicitGraph<TVertex, TEdge>.TryGetOutEdges(TVertex v, out global::System.Collections.Generic.IEnumerable<TEdge> edges)
        {
            global::Microsoft.Stubs.IDefaultStub<
            global::QuickGraph.Stubs.SVertexAndEdgeListGraph<TVertex, TEdge>
            > stub = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
              .SVertexAndEdgeListGraph<TVertex, TEdge>>)this).DefaultStub;
            stub.ValueAtReturn<global::System.Collections.Generic.IEnumerable<TEdge>>
                (this, out edges);
            return stub.Result<bool>(this);
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IIncidenceGraph`2.ContainsEdge(!0 source, !0 target)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IIncidenceGraph<TVertex, TEdge>.ContainsEdge(TVertex source, TVertex target)
        {
            global::Microsoft.Stubs.IDefaultStub<
            global::QuickGraph.Stubs.SVertexAndEdgeListGraph<TVertex, TEdge>
            > stub = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
              .SVertexAndEdgeListGraph<TVertex, TEdge>>)this).DefaultStub;
            return stub.Result<bool>(this);
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IIncidenceGraph`2.TryGetEdge(!0 source, !0 target, !1&amp; edge)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IIncidenceGraph<TVertex, TEdge>.TryGetEdge(
            TVertex source,
            TVertex target,
            out TEdge edge
        )
        {
            global::Microsoft.Stubs.IDefaultStub<
            global::QuickGraph.Stubs.SVertexAndEdgeListGraph<TVertex, TEdge>
            > stub = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
              .SVertexAndEdgeListGraph<TVertex, TEdge>>)this).DefaultStub;
            stub.ValueAtReturn<TEdge>(this, out edge);
            return stub.Result<bool>(this);
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IIncidenceGraph`2.TryGetEdges(!0 source, !0 target, System.Collections.Generic.IEnumerable`1&lt;!1&gt;&amp; edges)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IIncidenceGraph<TVertex, TEdge>.TryGetEdges(
            TVertex source,
            TVertex target,
            out global::System.Collections.Generic.IEnumerable<TEdge> edges
        )
        {
            global::Microsoft.Stubs.IDefaultStub<
            global::QuickGraph.Stubs.SVertexAndEdgeListGraph<TVertex, TEdge>
            > stub = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
              .SVertexAndEdgeListGraph<TVertex, TEdge>>)this).DefaultStub;
            stub.ValueAtReturn<global::System.Collections.Generic.IEnumerable<TEdge>>
                (this, out edges);
            return stub.Result<bool>(this);
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IVertexSet`1.ContainsVertex(!0 vertex)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IVertexSet<TVertex>.ContainsVertex(TVertex vertex)
        {
            global::Microsoft.Stubs.IDefaultStub<
            global::QuickGraph.Stubs.SVertexAndEdgeListGraph<TVertex, TEdge>
            > stub = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
              .SVertexAndEdgeListGraph<TVertex, TEdge>>)this).DefaultStub;
            return stub.Result<bool>(this);
        }

        /// <summary>Stub of property QuickGraph.IVertexSet`1.VertexCount</summary>
        int global::QuickGraph.IVertexSet<TVertex>.VertexCount
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs
                  .SVertexAndEdgeListGraph<TVertex, TEdge>, int> sh = this.VertexCountGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
                    .SVertexAndEdgeListGraph<TVertex, TEdge>> stub
                     = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                      .SVertexAndEdgeListGraph<TVertex, TEdge>>)this).DefaultStub;
                  return stub.Result<int>(this);
                }
            }
        }

        /// <summary>Stub of method System.Int32 QuickGraph.IVertexSet`1.get_VertexCount()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SVertexAndEdgeListGraph<TVertex, TEdge>, int> VertexCountGet;

        /// <summary>Stub of property QuickGraph.IVertexSet`1.Vertices</summary>
        global::System.Collections.Generic.IEnumerable<TVertex> global::QuickGraph.IVertexSet<TVertex>.Vertices
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs
                  .SVertexAndEdgeListGraph<TVertex, TEdge>, 
                global::System.Collections.Generic.IEnumerable<TVertex>> sh = this.VerticesGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs.IDefaultStub<global::QuickGraph.Stubs
                    .SVertexAndEdgeListGraph<TVertex, TEdge>> stub
                     = ((global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs
                      .SVertexAndEdgeListGraph<TVertex, TEdge>>)this).DefaultStub;
                  return 
                    stub.Result<global::System.Collections.Generic.IEnumerable<TVertex>>(this);
                }
            }
        }

        /// <summary>Stub of method System.Collections.Generic.IEnumerable`1&lt;!0&gt; QuickGraph.IVertexSet`1.get_Vertices()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SVertexAndEdgeListGraph<TVertex, TEdge>, global::System.Collections.Generic.IEnumerable<TVertex>> VerticesGet;
    }
}
namespace QuickGraph.Stubs
{
    /// <summary>Stub of IVertexAndEdgeSet`2</summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Stubs", "0.1.2.3")]
    [global::System.Serializable]
    [global::System.Diagnostics.DebuggerStepThrough]
    public partial class SVertexAndEdgeSet<TVertex,TEdge>
      : global::Microsoft.Stubs.StubBase<global::QuickGraph.Stubs.SVertexAndEdgeSet<TVertex, TEdge>>
      , global::QuickGraph.IVertexAndEdgeSet<TVertex, TEdge>
        where TEdge : global::QuickGraph.IEdge<TVertex>
    {
        /// <summary>Initializes a new instance of type SVertexAndEdgeSet</summary>
        [global::System.Diagnostics.DebuggerHidden]
        public SVertexAndEdgeSet()
        {
        }

        /// <summary>Stub of property QuickGraph.IGraph`2.AllowParallelEdges</summary>
        bool global::QuickGraph.IGraph<TVertex, TEdge>.AllowParallelEdges
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates.Func
                    <global::QuickGraph.Stubs.SVertexAndEdgeSet<TVertex, TEdge>, 
                    bool> sh = this.AllowParallelEdgesGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs.IDefaultStub
                      <global::QuickGraph.Stubs.SVertexAndEdgeSet<TVertex, TEdge>> stub
                     = ((global::Microsoft.Stubs.IStub<
                    global::QuickGraph.Stubs.SVertexAndEdgeSet<TVertex, TEdge>
                    >)this).DefaultStub;
                  return stub.Result<bool>(this);
                }
            }
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IGraph`2.get_AllowParallelEdges()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SVertexAndEdgeSet<TVertex, TEdge>, bool> AllowParallelEdgesGet;

        /// <summary>Stub of property QuickGraph.IEdgeSet`2.EdgeCount</summary>
        int global::QuickGraph.IEdgeSet<TVertex, TEdge>.EdgeCount
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates.Func
                    <global::QuickGraph.Stubs.SVertexAndEdgeSet<TVertex, TEdge>, 
                    int> sh = this.EdgeCountGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs.IDefaultStub
                      <global::QuickGraph.Stubs.SVertexAndEdgeSet<TVertex, TEdge>> stub
                     = ((global::Microsoft.Stubs.IStub<
                    global::QuickGraph.Stubs.SVertexAndEdgeSet<TVertex, TEdge>
                    >)this).DefaultStub;
                  return stub.Result<int>(this);
                }
            }
        }

        /// <summary>Stub of method System.Int32 QuickGraph.IEdgeSet`2.get_EdgeCount()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SVertexAndEdgeSet<TVertex, TEdge>, int> EdgeCountGet;

        /// <summary>Stub of property QuickGraph.IEdgeSet`2.Edges</summary>
        global::System.Collections.Generic.IEnumerable<TEdge> global::QuickGraph.IEdgeSet<TVertex, TEdge>.Edges
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates.Func
                    <global::QuickGraph.Stubs.SVertexAndEdgeSet<TVertex, TEdge>, 
                    global::System.Collections.Generic.IEnumerable<TEdge>> sh = this.EdgesGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs.IDefaultStub
                      <global::QuickGraph.Stubs.SVertexAndEdgeSet<TVertex, TEdge>> stub
                     = ((global::Microsoft.Stubs.IStub<
                    global::QuickGraph.Stubs.SVertexAndEdgeSet<TVertex, TEdge>
                    >)this).DefaultStub;
                  return 
                    stub.Result<global::System.Collections.Generic.IEnumerable<TEdge>>(this);
                }
            }
        }

        /// <summary>Stub of method System.Collections.Generic.IEnumerable`1&lt;!1&gt; QuickGraph.IEdgeSet`2.get_Edges()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SVertexAndEdgeSet<TVertex, TEdge>, global::System.Collections.Generic.IEnumerable<TEdge>> EdgesGet;

        /// <summary>Stub of property QuickGraph.IGraph`2.IsDirected</summary>
        bool global::QuickGraph.IGraph<TVertex, TEdge>.IsDirected
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates.Func
                    <global::QuickGraph.Stubs.SVertexAndEdgeSet<TVertex, TEdge>, 
                    bool> sh = this.IsDirectedGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs.IDefaultStub
                      <global::QuickGraph.Stubs.SVertexAndEdgeSet<TVertex, TEdge>> stub
                     = ((global::Microsoft.Stubs.IStub<
                    global::QuickGraph.Stubs.SVertexAndEdgeSet<TVertex, TEdge>
                    >)this).DefaultStub;
                  return stub.Result<bool>(this);
                }
            }
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IGraph`2.get_IsDirected()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SVertexAndEdgeSet<TVertex, TEdge>, bool> IsDirectedGet;

        /// <summary>Stub of property QuickGraph.IEdgeSet`2.IsEdgesEmpty</summary>
        bool global::QuickGraph.IEdgeSet<TVertex, TEdge>.IsEdgesEmpty
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates.Func
                    <global::QuickGraph.Stubs.SVertexAndEdgeSet<TVertex, TEdge>, 
                    bool> sh = this.IsEdgesEmptyGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs.IDefaultStub
                      <global::QuickGraph.Stubs.SVertexAndEdgeSet<TVertex, TEdge>> stub
                     = ((global::Microsoft.Stubs.IStub<
                    global::QuickGraph.Stubs.SVertexAndEdgeSet<TVertex, TEdge>
                    >)this).DefaultStub;
                  return stub.Result<bool>(this);
                }
            }
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IEdgeSet`2.get_IsEdgesEmpty()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SVertexAndEdgeSet<TVertex, TEdge>, bool> IsEdgesEmptyGet;

        /// <summary>Stub of property QuickGraph.IVertexSet`1.IsVerticesEmpty</summary>
        bool global::QuickGraph.IVertexSet<TVertex>.IsVerticesEmpty
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates.Func
                    <global::QuickGraph.Stubs.SVertexAndEdgeSet<TVertex, TEdge>, 
                    bool> sh = this.IsVerticesEmptyGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs.IDefaultStub
                      <global::QuickGraph.Stubs.SVertexAndEdgeSet<TVertex, TEdge>> stub
                     = ((global::Microsoft.Stubs.IStub<
                    global::QuickGraph.Stubs.SVertexAndEdgeSet<TVertex, TEdge>
                    >)this).DefaultStub;
                  return stub.Result<bool>(this);
                }
            }
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IVertexSet`1.get_IsVerticesEmpty()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SVertexAndEdgeSet<TVertex, TEdge>, bool> IsVerticesEmptyGet;

        /// <summary>Stub of method System.Boolean QuickGraph.IEdgeSet`2.ContainsEdge(!1 edge)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IEdgeSet<TVertex, TEdge>.ContainsEdge(TEdge edge)
        {
            global::Microsoft.Stubs.IDefaultStub
                <global::QuickGraph.Stubs.SVertexAndEdgeSet<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs.IStub
                  <global::QuickGraph.Stubs.SVertexAndEdgeSet<TVertex, TEdge>>)this)
                .DefaultStub;
            return stub.Result<bool>(this);
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IVertexSet`1.ContainsVertex(!0 vertex)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IVertexSet<TVertex>.ContainsVertex(TVertex vertex)
        {
            global::Microsoft.Stubs.IDefaultStub
                <global::QuickGraph.Stubs.SVertexAndEdgeSet<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs.IStub
                  <global::QuickGraph.Stubs.SVertexAndEdgeSet<TVertex, TEdge>>)this)
                .DefaultStub;
            return stub.Result<bool>(this);
        }

        /// <summary>Stub of property QuickGraph.IVertexSet`1.VertexCount</summary>
        int global::QuickGraph.IVertexSet<TVertex>.VertexCount
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates.Func
                    <global::QuickGraph.Stubs.SVertexAndEdgeSet<TVertex, TEdge>, 
                    int> sh = this.VertexCountGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs.IDefaultStub
                      <global::QuickGraph.Stubs.SVertexAndEdgeSet<TVertex, TEdge>> stub
                     = ((global::Microsoft.Stubs.IStub<
                    global::QuickGraph.Stubs.SVertexAndEdgeSet<TVertex, TEdge>
                    >)this).DefaultStub;
                  return stub.Result<int>(this);
                }
            }
        }

        /// <summary>Stub of method System.Int32 QuickGraph.IVertexSet`1.get_VertexCount()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SVertexAndEdgeSet<TVertex, TEdge>, int> VertexCountGet;

        /// <summary>Stub of property QuickGraph.IVertexSet`1.Vertices</summary>
        global::System.Collections.Generic.IEnumerable<TVertex> global::QuickGraph.IVertexSet<TVertex>.Vertices
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates.Func
                    <global::QuickGraph.Stubs.SVertexAndEdgeSet<TVertex, TEdge>, 
                    global::System.Collections.Generic.IEnumerable<TVertex>> sh = this.VerticesGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs.IDefaultStub
                      <global::QuickGraph.Stubs.SVertexAndEdgeSet<TVertex, TEdge>> stub
                     = ((global::Microsoft.Stubs.IStub<
                    global::QuickGraph.Stubs.SVertexAndEdgeSet<TVertex, TEdge>
                    >)this).DefaultStub;
                  return 
                    stub.Result<global::System.Collections.Generic.IEnumerable<TVertex>>(this);
                }
            }
        }

        /// <summary>Stub of method System.Collections.Generic.IEnumerable`1&lt;!0&gt; QuickGraph.IVertexSet`1.get_Vertices()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SVertexAndEdgeSet<TVertex, TEdge>, global::System.Collections.Generic.IEnumerable<TVertex>> VerticesGet;
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
namespace QuickGraph.Stubs
{
    /// <summary>Stub of IVertexListGraph`2</summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Stubs", "0.1.2.3")]
    [global::System.Serializable]
    [global::System.Diagnostics.DebuggerStepThrough]
    public partial class SVertexListGraph<TVertex,TEdge>
      : global::Microsoft.Stubs.StubBase<global::QuickGraph.Stubs.SVertexListGraph<TVertex, TEdge>>
      , global::QuickGraph.IVertexListGraph<TVertex, TEdge>
        where TEdge : global::QuickGraph.IEdge<TVertex>
    {
        /// <summary>Initializes a new instance of type SVertexListGraph</summary>
        [global::System.Diagnostics.DebuggerHidden]
        public SVertexListGraph()
        {
        }

        /// <summary>Stub of property QuickGraph.IGraph`2.AllowParallelEdges</summary>
        bool global::QuickGraph.IGraph<TVertex, TEdge>.AllowParallelEdges
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates
                  .Func<global::QuickGraph.Stubs.SVertexListGraph<TVertex, TEdge>, 
                  bool> sh = this.AllowParallelEdgesGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs.IDefaultStub
                      <global::QuickGraph.Stubs.SVertexListGraph<TVertex, TEdge>> stub
                     = ((global::Microsoft.Stubs.IStub
                        <global::QuickGraph.Stubs.SVertexListGraph<TVertex, TEdge>
                        >)this).DefaultStub;
                  return stub.Result<bool>(this);
                }
            }
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IGraph`2.get_AllowParallelEdges()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SVertexListGraph<TVertex, TEdge>, bool> AllowParallelEdgesGet;

        /// <summary>Stub of property QuickGraph.IGraph`2.IsDirected</summary>
        bool global::QuickGraph.IGraph<TVertex, TEdge>.IsDirected
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates
                  .Func<global::QuickGraph.Stubs.SVertexListGraph<TVertex, TEdge>, 
                  bool> sh = this.IsDirectedGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs.IDefaultStub
                      <global::QuickGraph.Stubs.SVertexListGraph<TVertex, TEdge>> stub
                     = ((global::Microsoft.Stubs.IStub
                        <global::QuickGraph.Stubs.SVertexListGraph<TVertex, TEdge>
                        >)this).DefaultStub;
                  return stub.Result<bool>(this);
                }
            }
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IGraph`2.get_IsDirected()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SVertexListGraph<TVertex, TEdge>, bool> IsDirectedGet;

        /// <summary>Stub of property QuickGraph.IVertexSet`1.IsVerticesEmpty</summary>
        bool global::QuickGraph.IVertexSet<TVertex>.IsVerticesEmpty
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates
                  .Func<global::QuickGraph.Stubs.SVertexListGraph<TVertex, TEdge>, 
                  bool> sh = this.IsVerticesEmptyGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs.IDefaultStub
                      <global::QuickGraph.Stubs.SVertexListGraph<TVertex, TEdge>> stub
                     = ((global::Microsoft.Stubs.IStub
                        <global::QuickGraph.Stubs.SVertexListGraph<TVertex, TEdge>
                        >)this).DefaultStub;
                  return stub.Result<bool>(this);
                }
            }
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IVertexSet`1.get_IsVerticesEmpty()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SVertexListGraph<TVertex, TEdge>, bool> IsVerticesEmptyGet;

        /// <summary>Stub of method System.Boolean QuickGraph.IImplicitGraph`2.IsOutEdgesEmpty(!0 v)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IImplicitGraph<TVertex, TEdge>.IsOutEdgesEmpty(TVertex v)
        {
            global::Microsoft.Stubs.IDefaultStub
                <global::QuickGraph.Stubs.SVertexListGraph<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs
                .IStub<global::QuickGraph.Stubs.SVertexListGraph<TVertex, TEdge>>)this)
                .DefaultStub;
            return stub.Result<bool>(this);
        }

        /// <summary>Stub of method System.Int32 QuickGraph.IImplicitGraph`2.OutDegree(!0 v)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        int global::QuickGraph.IImplicitGraph<TVertex, TEdge>.OutDegree(TVertex v)
        {
            global::Microsoft.Stubs.IDefaultStub
                <global::QuickGraph.Stubs.SVertexListGraph<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs
                .IStub<global::QuickGraph.Stubs.SVertexListGraph<TVertex, TEdge>>)this)
                .DefaultStub;
            return stub.Result<int>(this);
        }

        /// <summary>Stub of method !1 QuickGraph.IImplicitGraph`2.OutEdge(!0 v, System.Int32 index)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        TEdge global::QuickGraph.IImplicitGraph<TVertex, TEdge>.OutEdge(TVertex v, int index)
        {
            global::Microsoft.Stubs.IDefaultStub
                <global::QuickGraph.Stubs.SVertexListGraph<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs
                .IStub<global::QuickGraph.Stubs.SVertexListGraph<TVertex, TEdge>>)this)
                .DefaultStub;
            return stub.Result<TEdge>(this);
        }

        /// <summary>Stub of method System.Collections.Generic.IEnumerable`1&lt;!1&gt; QuickGraph.IImplicitGraph`2.OutEdges(!0 v)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        global::System.Collections.Generic.IEnumerable<TEdge> global::QuickGraph.IImplicitGraph<TVertex, TEdge>.OutEdges(TVertex v)
        {
            global::Microsoft.Stubs.IDefaultStub
                <global::QuickGraph.Stubs.SVertexListGraph<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs
                .IStub<global::QuickGraph.Stubs.SVertexListGraph<TVertex, TEdge>>)this)
                .DefaultStub;
            return stub.Result<global::System.Collections.Generic.IEnumerable<TEdge>>(this);
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IImplicitGraph`2.TryGetOutEdges(!0 v, System.Collections.Generic.IEnumerable`1&lt;!1&gt;&amp; edges)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IImplicitGraph<TVertex, TEdge>.TryGetOutEdges(TVertex v, out global::System.Collections.Generic.IEnumerable<TEdge> edges)
        {
            global::Microsoft.Stubs.IDefaultStub
                <global::QuickGraph.Stubs.SVertexListGraph<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs
                .IStub<global::QuickGraph.Stubs.SVertexListGraph<TVertex, TEdge>>)this)
                .DefaultStub;
            stub.ValueAtReturn<global::System.Collections.Generic.IEnumerable<TEdge>>
                (this, out edges);
            return stub.Result<bool>(this);
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IIncidenceGraph`2.ContainsEdge(!0 source, !0 target)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IIncidenceGraph<TVertex, TEdge>.ContainsEdge(TVertex source, TVertex target)
        {
            global::Microsoft.Stubs.IDefaultStub
                <global::QuickGraph.Stubs.SVertexListGraph<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs
                .IStub<global::QuickGraph.Stubs.SVertexListGraph<TVertex, TEdge>>)this)
                .DefaultStub;
            return stub.Result<bool>(this);
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IIncidenceGraph`2.TryGetEdge(!0 source, !0 target, !1&amp; edge)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IIncidenceGraph<TVertex, TEdge>.TryGetEdge(
            TVertex source,
            TVertex target,
            out TEdge edge
        )
        {
            global::Microsoft.Stubs.IDefaultStub
                <global::QuickGraph.Stubs.SVertexListGraph<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs
                .IStub<global::QuickGraph.Stubs.SVertexListGraph<TVertex, TEdge>>)this)
                .DefaultStub;
            stub.ValueAtReturn<TEdge>(this, out edge);
            return stub.Result<bool>(this);
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IIncidenceGraph`2.TryGetEdges(!0 source, !0 target, System.Collections.Generic.IEnumerable`1&lt;!1&gt;&amp; edges)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IIncidenceGraph<TVertex, TEdge>.TryGetEdges(
            TVertex source,
            TVertex target,
            out global::System.Collections.Generic.IEnumerable<TEdge> edges
        )
        {
            global::Microsoft.Stubs.IDefaultStub
                <global::QuickGraph.Stubs.SVertexListGraph<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs
                .IStub<global::QuickGraph.Stubs.SVertexListGraph<TVertex, TEdge>>)this)
                .DefaultStub;
            stub.ValueAtReturn<global::System.Collections.Generic.IEnumerable<TEdge>>
                (this, out edges);
            return stub.Result<bool>(this);
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IVertexSet`1.ContainsVertex(!0 vertex)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IVertexSet<TVertex>.ContainsVertex(TVertex vertex)
        {
            global::Microsoft.Stubs.IDefaultStub
                <global::QuickGraph.Stubs.SVertexListGraph<TVertex, TEdge>> stub
               = ((global::Microsoft.Stubs
                .IStub<global::QuickGraph.Stubs.SVertexListGraph<TVertex, TEdge>>)this)
                .DefaultStub;
            return stub.Result<bool>(this);
        }

        /// <summary>Stub of property QuickGraph.IVertexSet`1.VertexCount</summary>
        int global::QuickGraph.IVertexSet<TVertex>.VertexCount
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates
                  .Func<global::QuickGraph.Stubs.SVertexListGraph<TVertex, TEdge>, 
                  int> sh = this.VertexCountGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs.IDefaultStub
                      <global::QuickGraph.Stubs.SVertexListGraph<TVertex, TEdge>> stub
                     = ((global::Microsoft.Stubs.IStub
                        <global::QuickGraph.Stubs.SVertexListGraph<TVertex, TEdge>
                        >)this).DefaultStub;
                  return stub.Result<int>(this);
                }
            }
        }

        /// <summary>Stub of method System.Int32 QuickGraph.IVertexSet`1.get_VertexCount()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SVertexListGraph<TVertex, TEdge>, int> VertexCountGet;

        /// <summary>Stub of property QuickGraph.IVertexSet`1.Vertices</summary>
        global::System.Collections.Generic.IEnumerable<TVertex> global::QuickGraph.IVertexSet<TVertex>.Vertices
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates
                  .Func<global::QuickGraph.Stubs.SVertexListGraph<TVertex, TEdge>, 
                  global::System.Collections.Generic.IEnumerable<TVertex>> sh = this.VerticesGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs.IDefaultStub
                      <global::QuickGraph.Stubs.SVertexListGraph<TVertex, TEdge>> stub
                     = ((global::Microsoft.Stubs.IStub
                        <global::QuickGraph.Stubs.SVertexListGraph<TVertex, TEdge>
                        >)this).DefaultStub;
                  return 
                    stub.Result<global::System.Collections.Generic.IEnumerable<TVertex>>(this);
                }
            }
        }

        /// <summary>Stub of method System.Collections.Generic.IEnumerable`1&lt;!0&gt; QuickGraph.IVertexSet`1.get_Vertices()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SVertexListGraph<TVertex, TEdge>, global::System.Collections.Generic.IEnumerable<TVertex>> VerticesGet;
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
namespace QuickGraph.Stubs
{
    /// <summary>Stub of IVertexSet`1</summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Stubs", "0.1.2.3")]
    [global::System.Serializable]
    [global::System.Diagnostics.DebuggerStepThrough]
    public partial class SVertexSet<TVertex>
      : global::Microsoft.Stubs.StubBase<global::QuickGraph.Stubs.SVertexSet<TVertex>>
      , global::QuickGraph.IVertexSet<TVertex>
    {
        /// <summary>Initializes a new instance of type SVertexSet</summary>
        [global::System.Diagnostics.DebuggerHidden]
        public SVertexSet()
        {
        }

        /// <summary>Stub of property QuickGraph.IVertexSet`1.IsVerticesEmpty</summary>
        bool global::QuickGraph.IVertexSet<TVertex>.IsVerticesEmpty
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates
                  .Func<global::QuickGraph.Stubs.SVertexSet<TVertex>, bool> sh
                   = this.IsVerticesEmptyGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs
                    .IDefaultStub<global::QuickGraph.Stubs.SVertexSet<TVertex>> stub
                     = ((global::Microsoft.Stubs
                      .IStub<global::QuickGraph.Stubs.SVertexSet<TVertex>>)this).DefaultStub;
                  return stub.Result<bool>(this);
                }
            }
        }

        /// <summary>Stub of method System.Boolean QuickGraph.IVertexSet`1.get_IsVerticesEmpty()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SVertexSet<TVertex>, bool> IsVerticesEmptyGet;

        /// <summary>Stub of method System.Boolean QuickGraph.IVertexSet`1.ContainsVertex(!0 vertex)</summary>
        [global::System.Diagnostics.DebuggerHidden]
        bool global::QuickGraph.IVertexSet<TVertex>.ContainsVertex(TVertex vertex)
        {
            global::Microsoft.Stubs
              .IDefaultStub<global::QuickGraph.Stubs.SVertexSet<TVertex>> stub = (
            (global::Microsoft.Stubs.IStub<global::QuickGraph.Stubs.SVertexSet<TVertex>>
            )this).DefaultStub;
            return stub.Result<bool>(this);
        }

        /// <summary>Stub of property QuickGraph.IVertexSet`1.VertexCount</summary>
        int global::QuickGraph.IVertexSet<TVertex>.VertexCount
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs.StubDelegates
                  .Func<global::QuickGraph.Stubs.SVertexSet<TVertex>, int> sh
                   = this.VertexCountGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs
                    .IDefaultStub<global::QuickGraph.Stubs.SVertexSet<TVertex>> stub
                     = ((global::Microsoft.Stubs
                      .IStub<global::QuickGraph.Stubs.SVertexSet<TVertex>>)this).DefaultStub;
                  return stub.Result<int>(this);
                }
            }
        }

        /// <summary>Stub of method System.Int32 QuickGraph.IVertexSet`1.get_VertexCount()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SVertexSet<TVertex>, int> VertexCountGet;

        /// <summary>Stub of property QuickGraph.IVertexSet`1.Vertices</summary>
        global::System.Collections.Generic.IEnumerable<TVertex> global::QuickGraph.IVertexSet<TVertex>.Vertices
        {
            [global::System.Diagnostics.DebuggerHidden]
            get
            {
                global::Microsoft.Stubs
                  .StubDelegates.Func<global::QuickGraph.Stubs.SVertexSet<TVertex>, 
                  global::System.Collections.Generic.IEnumerable<TVertex>> sh = this.VerticesGet;
                if ((object)sh != (object)null)
                  return sh.Invoke(this);
                else 
                {
                  global::Microsoft.Stubs
                    .IDefaultStub<global::QuickGraph.Stubs.SVertexSet<TVertex>> stub
                     = ((global::Microsoft.Stubs
                      .IStub<global::QuickGraph.Stubs.SVertexSet<TVertex>>)this).DefaultStub;
                  return 
                    stub.Result<global::System.Collections.Generic.IEnumerable<TVertex>>(this);
                }
            }
        }

        /// <summary>Stub of method System.Collections.Generic.IEnumerable`1&lt;!0&gt; QuickGraph.IVertexSet`1.get_Vertices()</summary>
        public global::Microsoft.Stubs.StubDelegates.Func<global::QuickGraph.Stubs.SVertexSet<TVertex>, global::System.Collections.Generic.IEnumerable<TVertex>> VerticesGet;
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

