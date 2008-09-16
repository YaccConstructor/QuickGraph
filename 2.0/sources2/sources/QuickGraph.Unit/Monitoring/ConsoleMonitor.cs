using System;
using System.IO;

namespace QuickGraph.Unit.Monitoring
{
    public sealed class ConsoleMonitor : IMonitor
    {
        private TextWriter consoleOut;
        private TextWriter consoleError;
        private StringWriter _out = new StringWriter();
        private StringWriter _error = new StringWriter();

        public ConsoleMonitor()
        {}

        public void Start()
        {
            this.consoleOut = Console.Out;
            this.consoleError = Console.Error;

            this._out = new StringWriter();
            this._error = new StringWriter();
            Console.SetOut(this._out);
            Console.SetError(this._out);
        }

        public string Out
        {
            get { return this._out.ToString(); }
        }

        public string Error
        {
            get { return this._error.ToString(); }
        }

        public void Stop()
        {
            if (this.consoleOut != null)
            {
                Console.SetOut(this.consoleOut);
                this.consoleOut = null;
            }
            if (this.consoleError != null)
            {
                Console.SetError(this.consoleError);
                this.consoleError = null;
            }
        }

        public void Dispose()
        {
            this.Stop();
        }
    }
}
