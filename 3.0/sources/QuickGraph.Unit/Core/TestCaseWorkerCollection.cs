using System;
using System.Collections.Generic;
using System.Threading;

namespace QuickGraph.Unit.Core
{
    public sealed class TestCaseWorkerCollection : IEnumerable<TestCaseWorker>
    {
        private List<WorkerPair> workerPairs = new List<WorkerPair>();

        private class WorkerPair
        {
            public TestCaseWorker Worker;
            public Thread WorkerThread;
            public void SetWorkerThread(Thread thread)
            {
                this.WorkerThread = thread;
            }
        }

        public void Add(TestCaseWorker worker)
        {
            WorkerPair pair = new WorkerPair();
            pair.Worker = worker;
            this.workerPairs.Add(pair);
        }

        public void StartAll(object fixture)
        {
            // creating threads
            foreach (WorkerPair pair in this.workerPairs)
            {
                if (pair.WorkerThread != null)
                    throw new InvalidOperationException("Thread were not terminated before starting");
                pair.SetWorkerThread(new Thread(new ParameterizedThreadStart(pair.Worker.Start)));
                pair.WorkerThread.Name = pair.Worker.Name;
            }

            // starting threads
            foreach (WorkerPair pair in this.workerPairs)
            {
                pair.WorkerThread.Start(fixture);
            }
        }

        public void WaitAll()
        {
            while (IsAnyAlive())
            {
                Thread.Sleep(1000);
            }
        }

        public bool IsAnyAlive()
        {
            bool isAlive = false;
            foreach (WorkerPair pair in this.workerPairs)
            {
                if (pair.WorkerThread != null)
                {
                    if (pair.WorkerThread.IsAlive)
                        isAlive = true;
                    else
                        pair.SetWorkerThread(null);
                }
            }

            return isAlive;
        }

        public bool ContainsAll(ICollection<string> threadNames)
        {
            // let's make sure not worker died
            foreach (WorkerPair pair in this.workerPairs)
            {
                // did the thread fail ?
                if (pair.Worker.ThrowedException!=null)
                    return true;
            }

            foreach (WorkerPair pair in this.workerPairs)
            {
                if (!threadNames.Contains(pair.WorkerThread.Name))
                    return false;
            }

            return true;
        }

        public void CloseAll()
        {
            foreach (WorkerPair pair in this.workerPairs)
            {
                if (pair.WorkerThread != null)
                {
                    if (pair.WorkerThread.IsAlive)
                        pair.WorkerThread.Abort();
                    pair.SetWorkerThread(null);
                }
            }
        }

        public void Verify()
        {
            int exceptionCount = 0;
            foreach (TestCaseWorker worker in this)
            {
                if (worker.ThrowedException != null)
                {
                    exceptionCount++;
                    Console.WriteLine("Error in worker {0}",worker.Name);
                    Console.WriteLine(worker.ThrowedException);
                }
            }

            if (exceptionCount > 0)
                Assert.Fail("There were {0} exceptions during the run.", exceptionCount);
        }

        public IEnumerator<TestCaseWorker> GetEnumerator()
        {
            foreach (WorkerPair pair in this.workerPairs)
                yield return pair.Worker;
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
