using System;
using System.Diagnostics;
using QuickGraph.Unit.Monitoring;

namespace QuickGraph.Unit.Core
{
    [Serializable]
    public class Result
    {
        private TestState state = TestState.NotRun;
        private string name;
        private Exception exception = null;
        private TestMonitor monitor;

        public Result(string name)
        {
            if (name == null)
                throw new ArgumentNullException("name");
            this.name = name;
            this.monitor = new TestMonitor();
        }

        public TestState State
        {
            get { return this.state; }
        }

        public string Name
        {
            get { return this.name; }
        }

        public virtual string FullName
        {
            get { return this.Name; }
        }

        public string Out
        {
            get { return this.monitor.Console.Out; }
        }
        public string Error
        {
            get { return this.monitor.Console.Error; }
        }
        public double Duration
        {
            get { return this.monitor.Timer.Duration; }
        }
        public Exception Exception
        {
            get { return this.exception; }
        }
        public DateTime StartTime
        {
            get { return this.Monitor.Timer.StartTime; }
        }
        public DateTime StopTime
        {
            get { return this.Monitor.Timer.EndTime; }
        }

        public TestMonitor Monitor
        {
            get { return this.monitor; }
        }

        public void Start()
        {
            this.Monitor.Start();
        }

        private void Stop()
        {
            this.Monitor.Stop();

        }

        [DebuggerStepThrough]
        public void Fail(Exception exception)
        {
            if (exception == null)
                throw new ArgumentNullException("exception");
            this.Stop();
            this.state = TestState.Failure;
            this.exception = exception;
        }

        [DebuggerStepThrough]
        public void Success()
        {
            this.Stop();
            this.state = TestState.Success;
        }

        [DebuggerStepThrough]
        public void Ignore()
        {
            this.Stop();
            this.state = TestState.Ignore;
        }
    }
}
