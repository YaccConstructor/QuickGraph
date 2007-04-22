using System;
using System.Collections.Generic;
using System.Threading;
using System.Globalization;
using System.Security.Principal;

namespace QuickGraph.Unit.Monitoring
{
    internal sealed class ThreadMonitor : IMonitor
    {
        private CultureInfo cultureInfo;
        private CultureInfo uiCulture;
        private IPrincipal principal;

        public void Start()
        {
            this.cultureInfo = Thread.CurrentThread.CurrentCulture;
            this.uiCulture = Thread.CurrentThread.CurrentUICulture;
            this.principal = Thread.CurrentPrincipal;
        }

        public void Stop()
        {
            if (this.cultureInfo != null)
            {
                Thread.CurrentThread.CurrentCulture = this.cultureInfo;
                this.cultureInfo = null;
            }
            if (this.uiCulture != null)
            {
                Thread.CurrentThread.CurrentUICulture = this.uiCulture;
                this.uiCulture = null;
            }
            if (this.principal != null)
            {
                Thread.CurrentPrincipal = this.principal;
                this.principal = null;
            }
        }

        public void Dispose()
        {
            Stop();
        }
    }
}
