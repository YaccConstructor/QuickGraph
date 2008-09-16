using System;
using System.Collections.Generic;
using System.Threading;

namespace QuickGraph.Unit
{
    [Serializable]
    public sealed class TestSynchronizer : IDisposable
    {
        private readonly object syncRoot = new object();
        private String name;
        private ManualResetEvent barrier;
        private List<string> threadNames = new List<string>();

        public TestSynchronizer(string name)
        {
            if (String.IsNullOrEmpty(name))
                throw new ArgumentNullException("name");
            this.name = name;
        }

        public ICollection<string> GetThreadNames()
        {
            lock(syncRoot)
            {
                return  new List<string>(this.threadNames); 
            }
        }

        public String Name
        {
            get { return this.name; }
        }

        public void Block()
        {
            // make sure we cleaned up
            this.Close();
            this.barrier = new ManualResetEvent(false);
        }

        public void Release()
        {
            if (this.barrier == null)
                throw new InvalidOperationException("Block must be called first");
            this.barrier.Set();
        }

        public void Synchronize()
        {
            if (this.barrier == null)
                throw new InvalidOperationException("Block must be called first");
            // add thread name
            lock (syncRoot)
            {
                this.threadNames.Add(Thread.CurrentThread.Name);
            }
            this.barrier.WaitOne();
        }

        public void Close()
        {
            if (this.barrier != null)
            {
                IDisposable disposable = this.barrier as IDisposable;
                if (disposable != null)
                    disposable.Dispose();
                this.barrier = null;
            }
        }

        public void Dispose()
        {
            this.Close();
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
