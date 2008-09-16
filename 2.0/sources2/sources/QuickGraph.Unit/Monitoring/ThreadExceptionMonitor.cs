using System;
using System.Windows.Forms;
using System.Collections.Generic;

namespace QuickGraph.Unit.Monitoring
{
    public sealed class ThreadExceptionMonitor: IMonitor
    {
        private ILoggerService logger;
        private List<Exception> exceptions = new List<Exception>();

        public ThreadExceptionMonitor(ILoggerService logger)
        {
            if (logger==null)
                throw new ArgumentNullException("logger");
            this.logger = logger;
        }

        public IList<Exception> Exceptions
        {
            get { return this.exceptions; }
        }

        public void Start()
        {
            System.Windows.Forms.Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
        }

        public void Clear()
        {
            this.exceptions.Clear();
        }

        public void Stop()
        {
            System.Windows.Forms.Application.ThreadException -= new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
        }

        public void Dispose()
        {
            this.Stop();
        }

        void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            this.exceptions.Add(e.Exception);
            this.logger.LogError(
                "Thread",
                e.Exception,
                "Exception occured in concurrent thread");
        }
    }
}
