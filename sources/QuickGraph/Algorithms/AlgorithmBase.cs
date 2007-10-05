using System;

namespace QuickGraph.Algorithms
{
    public abstract class AlgorithmBase<TGraph> :
        IAlgorithm<TGraph>
    {
        private TGraph visitedGraph;
        private volatile object syncRoot = new object();
        private int cancelling = 0;
        private volatile ComputationState state = ComputationState.NotRunning;

        public AlgorithmBase(TGraph visitedGraph)
        {
            if (visitedGraph == null)
                throw new ArgumentNullException("visitedGraph");
            this.visitedGraph = visitedGraph;
        }

        public TGraph VisitedGraph
        {
            get { return this.visitedGraph; }
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

        protected bool IsAborting
        {
            get { return this.cancelling>0; }
        }

        public void Compute()
        {
            this.BeginComputation();
            this.InternalCompute();
            this.EndComputation();
        }

        protected abstract void InternalCompute();

        public virtual void Abort()
        {
            bool raise = false;
            lock (this.syncRoot)
            {
                if (this.state == ComputationState.Running)
                {
                    this.state = ComputationState.PendingAbortion;
                    System.Threading.Interlocked.Increment(ref this.cancelling);
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
            lock (this.syncRoot)
            {
                if (this.state != ComputationState.NotRunning)
                    throw new InvalidOperationException();

                this.state = ComputationState.Running;
                this.cancelling = 0;
                this.OnStarted(EventArgs.Empty);
                this.OnStateChanged(EventArgs.Empty);
            }
        }

        protected void EndComputation()
        {
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
                this.cancelling = 0;
                this.OnStateChanged(EventArgs.Empty);
            }
        }
    }
}
