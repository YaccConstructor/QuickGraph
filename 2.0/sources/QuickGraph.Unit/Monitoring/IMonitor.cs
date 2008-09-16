using System;

namespace QuickGraph.Unit.Monitoring
{
    public interface IMonitor : IDisposable
    {
        void Start();
        void Stop();
    }
}
