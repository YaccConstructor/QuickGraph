using System;
using System.IO;
using System.Diagnostics;
using System.ComponentModel;

namespace QuickGraph.Unit.Reports
{
    public abstract class ReportBase : Component
    {
        private bool generateOnDisposed = false;
        private DateTime creationTime = DateTime.Now;

        public ReportBase(IContainer container)
        {
            container.Add(this);
        }

        public DateTime CreationTime
        {
            get
            {
                return this.creationTime;
            }
        }

        public abstract void Generate();

        public bool GenerateOnDisposed
        {
            get { return this.generateOnDisposed; }
            set { this.generateOnDisposed = value; }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing && this.GenerateOnDisposed)
            {
                this.Generate();
                this.GenerateOnDisposed = false;
            }
        }
    }
}
