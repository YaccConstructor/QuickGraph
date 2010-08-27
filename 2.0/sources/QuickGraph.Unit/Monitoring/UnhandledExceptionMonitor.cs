using System;
using System.Collections.Generic;
using System.Threading;

namespace QuickGraph.Unit.Monitoring
{
    public sealed class UnhandledExceptionMonitor : IMonitor
    {
        private bool disposed = false;
        public void Start()
        {
            AppDomain.CurrentDomain.UnhandledException+=new UnhandledExceptionEventHandler(UnhandledException);
        }

        public void Stop()
        {
            AppDomain.CurrentDomain.UnhandledException -= new UnhandledExceptionEventHandler(UnhandledException);
        }

        public void Dispose()
        {
            if (!disposed)
            {
                this.Stop();
                GC.SuppressFinalize(this);
                this.disposed = true;
            }
        }

        private void UnhandledException(Object sender, UnhandledExceptionEventArgs args)
        {
            if (args.IsTerminating)
                return;

            Assert.Fail("Unhandled Exception was catched\n", args.ExceptionObject);
        }
    }
}
