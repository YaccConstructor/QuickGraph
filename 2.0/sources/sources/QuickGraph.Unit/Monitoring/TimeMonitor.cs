using System;

namespace QuickGraph.Unit.Monitoring
{
    public sealed class TimeMonitor : IMonitor
    {
        private DateTime startTime;
        private DateTime endTime;
        private bool running;

        public TimeMonitor()
        {}

        public void Start()
        {
            this.startTime = this.endTime = DateTime.Now;
            this.running = true;
        }

        public void Stop()
        {
            if (running)
            {
                this.endTime = DateTime.Now;
                this.running = false;
            }
        }

        public DateTime StartTime
        {
            get { return this.startTime; }
        }

        public DateTime EndTime
        {
            get { return this.endTime; }
        }

        public double Duration
        {
            get
            {
                TimeSpan ts = this.endTime - this.startTime;
                return ts.TotalSeconds;
           }
        }

        public void Dispose() { }
    }
}
