using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.CodeDom.Compiler;
using QuickGraph.Unit.Exceptions;
using QuickGraph.Unit.Logging;
using System.Threading;

namespace QuickGraph.Unit.Core
{
    public sealed class FixtureRunner : IDisposable
    {
        private ITestListener listener;

        private IFixture fixture;
        private ICollection<ITestCase> testCases;

        private Thread workerThread = null;

        public FixtureRunner(
            IFixture fixture, 
            ICollection<ITestCase> testCases,
            ITestListener listener)
        {
            if (fixture == null)
                throw new ArgumentNullException("fixture");
            if (testCases == null)
                throw new ArgumentNullException("testCases");
            if (listener == null)
                throw new ArgumentNullException("listener");

            this.fixture = fixture;
            this.testCases = testCases;
            this.listener = listener;
        }

        public ITestListener TestListener
        {
            get { return this.listener; }
        }

        public IFixture Fixture
        {
            get { return this.fixture; }
        }

        public ICollection<ITestCase> TestCases
        {
            get { return this.testCases; }
        }

        [System.Diagnostics.DebuggerStepThrough]
        public void Run()
        {
            workerThread = null;
            System.Runtime.CompilerServices.RuntimeHelpers.PrepareConstrainedRegions();
            try
            {
                using (FixtureWorker worker = new FixtureWorker(this))
                {
                    workerThread = new Thread(new ThreadStart(worker.RunAsync));
                    workerThread.SetApartmentState(this.Fixture.Apartment);
                    workerThread.IsBackground = true;
                    workerThread.Priority = ThreadPriority.Lowest;

                    int timeOut = 
                        (this.Fixture.TimeOut==int.MaxValue) 
                            ? int.MaxValue : 60 * 1000 * this.Fixture.TimeOut;
                    // if a debugger is attached, no time out
                    if (System.Diagnostics.Debugger.IsAttached)
                        timeOut = int.MaxValue;

                    workerThread.Start();
                    if (!workerThread.Join(timeOut))
                    {
                        // cancel and wait for 10 sec
                        worker.CancelAsync();
                        if (!workerThread.Join(10 * 1000))
                            this.AbortWorkerThread();

                        // store result
                        TestResult result = new TestResult(Fixture.Name, "FixtureTimedOut");
                        result.Start();
                        result.Fail(new FixtureTimedOutException(this.Fixture.Name));
                        this.TestListener.FixtureTearDown(result);
                    }
                    else
                    {
                        if (worker.UnhandledException != null)
                            throw new ApplicationException("Exception is runner", worker.UnhandledException);
                    }
                }
            }
            finally
            {
                this.AbortWorkerThread();
            }
        }

        private void AbortWorkerThread()
        {
            if (this.workerThread == null)
                return;

            workerThread.Abort();
            workerThread = null;
        }

        public void Dispose()
        {
            this.AbortWorkerThread();
        }

        private sealed class FixtureWorker : IDisposable
        {
            private int isCancelPending = 0;
            private Object fixtureInstance;
            private FixtureRunner owner;
            private Exception unhandledException = null;

            public FixtureWorker(FixtureRunner owner)
            {
                this.owner = owner;
            }

            public bool IsCancelPending
            {
                get
                {
                    return this.isCancelPending > 0;
                }
            }

            public void CancelAsync()
            {
                Interlocked.Increment(ref this.isCancelPending);
            }

            public FixtureRunner Owner
            {
                get { return this.owner; }
            }

            public Exception UnhandledException
            {
                get { return this.unhandledException; }
            }

            [System.Diagnostics.DebuggerStepThrough]
            public void RunAsync()
            {
                System.Runtime.CompilerServices.RuntimeHelpers.PrepareConstrainedRegions();
                try
                {
                    this.Owner.TestListener.BeforeFixture(this.Owner.Fixture, this.Owner.TestCases.Count);

                    // fixture setup
                    if (!this.RunTestFixtureSetUp())
                        return;

                    System.Runtime.CompilerServices.RuntimeHelpers.PrepareConstrainedRegions();
                    try
                    {
                        foreach (ITestCase test in this.Owner.TestCases)
                        {
                            if (this.IsCancelPending)
                                return;
                            if (test == null)
                                continue;

                            this.Owner.TestListener.BeforeTestCase(test);

                            // create instance
                            if (this.CreateFixtureInstance())
                            {
                                if (this.RunSetUp())
                                {
                                    this.RunTest(test);
                                    this.RunTearDown();
                                }
                                // disposeinstance
                                this.DisposeFixtureInstance();
                            }

                            this.Owner.TestListener.AfterTestCase(test);
                        }
                    }
                    finally
                    {
                        this.RunTestFixtureTearDown();
                        GC.Collect();
                        GC.WaitForPendingFinalizers();
                        this.Owner.TestListener.AfterFixture(this.Owner.Fixture);
                    }
                }
                catch (Exception ex)
                {
                    this.unhandledException = ex;
                }
            }

            public void Dispose()
            {
                this.DisposeFixtureInstance();
            }

            private void DisposeFixtureInstance()
            {
                if (this.fixtureInstance != null)
                {
                    IDisposable disposable = this.fixtureInstance as IDisposable;
                    if (disposable != null)
                        disposable.Dispose();
                    this.fixtureInstance = null;
                }
            }

            private bool CreateFixtureInstance()
            {
                TestResult result = new TestResult(this.Owner.Fixture.Name, "Create");
                System.Runtime.CompilerServices.RuntimeHelpers.PrepareConstrainedRegions();
                try
                {
                    this.fixtureInstance = this.Owner.Fixture.CreateInstance();
                    return true;
                }
                catch (Exception ex)
                {
                    Exception current = ex;
                    if (current is TargetInvocationException)
                        current = ex.InnerException;
                    result.Fail(current);
                    this.Owner.TestListener.SetUp(result);
                    return false;
                }
            }

            [System.Diagnostics.DebuggerStepThrough]
            private bool RunTestFixtureSetUp()
            {
                TestResult result = null; ;
                System.Runtime.CompilerServices.RuntimeHelpers.PrepareConstrainedRegions();
                try
                {
                    if (this.Owner.Fixture.FixtureSetUp == null)
                        return true;

                    result = new TestResult(this.Owner.Fixture.Name, this.Owner.Fixture.FixtureSetUp);
                    result.Start();
                    this.Owner.Fixture.FixtureSetUp.Invoke(null, null);
                    result.Success();
                    this.Owner.TestListener.FixtureSetUp(result);

                    return true;
                }
                catch (Exception ex)
                {
                    if (result != null)
                    {
                        Exception current = ex;
                        if (current is TargetInvocationException)
                            current = ex.InnerException;
                        result.Fail(current);
                        this.Owner.TestListener.FixtureSetUp(result);
                    }
                    return false;
                }
            }


            [System.Diagnostics.DebuggerStepThrough]
            private bool RunTestFixtureTearDown()
            {
                TestResult result = null;
                System.Runtime.CompilerServices.RuntimeHelpers.PrepareConstrainedRegions();
                try
                {
                    if (this.Owner.Fixture.FixtureTearDown == null)
                        return true;

                    result = new TestResult(this.Owner.Fixture.Name, this.Owner.Fixture.FixtureTearDown);
                    result.Start();
                    this.Owner.Fixture.FixtureTearDown.Invoke(null, null);
                    result.Success();
                    return true;
                }
                catch (Exception ex)
                {
                    if (result != null)
                    {
                        Exception current = ex;
                        if (current is TargetInvocationException)
                            current = ex.InnerException;
                        result.Fail(current);
                        this.Owner.TestListener.FixtureTearDown(result);
                    }
                    return false;
                }
            }

            [System.Diagnostics.DebuggerStepThrough]
            private bool RunSetUp()
            {
                TestResult result = null;
                try
                {
                    if (this.Owner.Fixture.SetUp == null)
                        return true;

                    result = new TestResult(this.Owner.Fixture.Name, this.Owner.Fixture.SetUp);
                    result.Start();
                    this.Owner.Fixture.SetUp.Invoke(this.fixtureInstance,null);
                    result.Success();
                    this.Owner.TestListener.SetUp(result);

                    return true;
                }
                catch (Exception ex)
                {
                    if (result != null)
                    {
                        Exception current = ex;
                        if (current is TargetInvocationException)
                            current = ex.InnerException;
                        result.Fail(current);
                        this.Owner.TestListener.SetUp(result);
                    }
                    return false;
                }
            }

            [System.Diagnostics.DebuggerStepThrough]
            private void RunTearDown()
            {
                TestResult result = null;
                System.Runtime.CompilerServices.RuntimeHelpers.PrepareConstrainedRegions();
                try
                {
                    if (this.Owner.Fixture.TearDown == null)
                        return;

                    result = new TestResult(this.Owner.Fixture.Name, this.Owner.Fixture.TearDown);
                    result.Start();
                    this.Owner.Fixture.TearDown.Invoke(this.fixtureInstance,null);
                    result.Success();
                }
                catch (Exception ex)
                {
                    if (result != null)
                    {
                        Exception current = ex;
                        if (current is TargetInvocationException)
                            current = ex.InnerException;
                        result.Fail(current);
                        this.Owner.TestListener.TearDown(result);
                    }
                }
            }

            [System.Diagnostics.DebuggerStepThrough]
            private void RunTest(ITestCase test)
            {
                TestResult result = new TestResult(test);
                System.Runtime.CompilerServices.RuntimeHelpers.PrepareConstrainedRegions();
                try
                {
                    result.Start();
                    test.Run(this.fixtureInstance);
                    result.Success();
                }
                catch (Exception ex)
                {
                    Exception current = ex;
                    if (current is TargetInvocationException)
                        current = current.InnerException;
                    if (current is IgnoreException)
                        result.Ignore();
                    else if (current is AssumptionFailureException)
                        result.Success();
                    else
                        result.Fail(current);
                }
                this.Owner.TestListener.Test(result);
            }
        }
    }
}
