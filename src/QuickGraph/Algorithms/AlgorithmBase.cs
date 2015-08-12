using System;
using QuickGraph.Algorithms.Services;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace QuickGraph.Algorithms
{
    public abstract class AlgorithmBase<TGraph> :
        IAlgorithm<TGraph>,
        IAlgorithmComponent
    {
        private readonly TGraph visitedGraph;
        private readonly AlgorithmServices services;
        private volatile object syncRoot = new object();
        private volatile ComputationState state = ComputationState.NotRunning;

        /// <summary>
        /// Creates a new algorithm with an (optional) host.
        /// </summary>
        /// <param name="host">if null, host is set to the this reference</param>
        /// <param name="visitedGraph"></param>
        protected AlgorithmBase(IAlgorithmComponent host, TGraph visitedGraph)
        {
            Contract.Requires(visitedGraph != null);
            if (host == null)
                host = this;
            this.visitedGraph = visitedGraph;
            this.services = new AlgorithmServices(host);
        }

        protected AlgorithmBase(TGraph visitedGraph)
        {
            Contract.Requires(visitedGraph != null);
            this.visitedGraph = visitedGraph;
            this.services = new AlgorithmServices(this);
        }

        public TGraph VisitedGraph
        {
            get { return this.visitedGraph; }
        }

        public IAlgorithmServices Services
        {
            get { return this.services; }
        }

        public Object SyncRoot
        {
            get { return this.syncRoot; }
        }

        public ComputationState State
        {
            get
            {
                lock (this.syncRoot)
                {
                    return this.state;
                }
            }
        }

        public void Compute()
        {
            this.BeginComputation();
            this.Initialize();
            try
            {
                this.InternalCompute();
            }
            finally
            {
                this.Clean();
            }
            this.EndComputation();
        }

        protected virtual void Initialize()
        { }

        protected virtual void Clean()
        { }

        protected abstract void InternalCompute();

        public void Abort()
        {
            bool raise = false;
            lock (this.syncRoot)
            {
                if (this.state == ComputationState.Running)
                {
                    this.state = ComputationState.PendingAbortion;
                    this.Services.CancelManager.Cancel();
                    raise = true;
                }
            }
            if (raise)
                this.OnStateChanged(EventArgs.Empty);
        }

        public event EventHandler StateChanged;
        protected virtual void OnStateChanged(EventArgs e)
        {
            EventHandler eh = this.StateChanged;
            if (eh!=null)
                eh(this, e);
        }

        public event EventHandler Started;
        protected virtual void OnStarted(EventArgs e)
        {
            EventHandler eh = this.Started;
            if (eh != null)
                eh(this, e);
        }

        public event EventHandler Finished;
        protected virtual void OnFinished(EventArgs e)
        {
            EventHandler eh = this.Finished;
            if (eh != null)
                eh(this, e);
        }

        public event EventHandler Aborted;
        protected virtual void OnAborted(EventArgs e)
        {
            EventHandler eh = this.Aborted;
            if (eh != null)
                eh(this, e);
        }

        protected void BeginComputation()
        {
            Contract.Requires(this.State == ComputationState.NotRunning);
            lock (this.syncRoot)
            {
                this.state = ComputationState.Running;
                this.Services.CancelManager.ResetCancel();
                this.OnStarted(EventArgs.Empty);
                this.OnStateChanged(EventArgs.Empty);
            }
        }

        protected void EndComputation()
        {
            Contract.Requires(
                this.State == ComputationState.Running || 
                this.State == ComputationState.Aborted);
            lock (this.syncRoot)
            {
                switch (this.state)
                {
                    case ComputationState.Running:
                        this.state = ComputationState.Finished;
                        this.OnFinished(EventArgs.Empty);
                        break;
                    case ComputationState.PendingAbortion:
                        this.state = ComputationState.Aborted;
                        this.OnAborted(EventArgs.Empty);
                        break;
                    default:
                        throw new InvalidOperationException();
                }
                this.Services.CancelManager.ResetCancel();
                this.OnStateChanged(EventArgs.Empty);
            }
        }

        public T GetService<T>()
            where T : IService
        {
            T service;
            if (!this.TryGetService<T>(out service))
                throw new InvalidOperationException("service not found");
            return service;
        }

        public bool TryGetService<T>(out T service)
            where T : IService
        {
            object serviceObject;
            if (this.TryGetService(typeof(T), out serviceObject))
            {
                service = (T)serviceObject;
                return true;
            }

            service = default(T);
            return false;
        }

        Dictionary<Type, object> _services;
        protected virtual bool TryGetService(Type serviceType, out object service)
        {
            Contract.Requires(serviceType != null);
            lock (this.SyncRoot)
            {
                if (this._services == null)
                    this._services = new Dictionary<Type, object>();
                if (!this._services.TryGetValue(serviceType, out service))
                {
                    if (serviceType == typeof(ICancelManager))
                        this._services[serviceType] = service = new CancelManager();
                    else
                        this._services[serviceType] = service = null;
                }

                return service != null;

            }
        }
    }
}
