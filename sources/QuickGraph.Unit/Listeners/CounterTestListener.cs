using System;
using System.Collections.Generic;
using System.Text;
using QuickGraph.Unit.Serialization;
using QuickGraph.Unit.Core;

namespace QuickGraph.Unit.Listeners
{
    public class CounterTestListener : ITestListener
    {
        private TestCounter counter;
        private TestCounter assemblyCounter;
        private TestCounter fixtureCounter;
        private TestState testState;

        public TestCounter Counter
        {
            get { return this.counter; }
        }

        public virtual void ReportsGenerated(
            string historyXml,
            string historyHtml,
            string batchXml,
            string batchHtml)
        { }

        public virtual void Message(MessageImportance importance, string message)
        { }
        public virtual void Warning(string message)
        { }
        public virtual void Error(string message)
        { }
        public virtual void Error(Exception ex)
        { }

        public virtual void BeforeBatch(TestBatch batch)
        {
            this.counter = new TestCounter(batch.GetTestCount());
        }

        public virtual void AfterBatch(TestBatch batch)
        {
        }

        public virtual void BeforeAssembly(TestAssembly testAssembly)
        {
            this.assemblyCounter = new TestCounter(testAssembly.GetTestCount());
        }

        public virtual void AfterAssembly(TestAssembly testAssembly)
        {
            this.assemblyCounter = null;
        }

        public virtual void AssemblySetUp(Result result)
        {
            if (result.State == TestState.Success)
                return;

            this.counter.FailureCount += this.assemblyCounter.TotalCount;
            this.assemblyCounter = null;
        }

        public virtual void AssemblyTearDown(Result result)
        {
            if (result.State == TestState.Success || this.assemblyCounter==null)
                return;

            this.counter.RollbackResults(this.assemblyCounter);
            this.counter.FailureCount += this.assemblyCounter.TotalCount;
            this.assemblyCounter = null;
        }

        public virtual void BeforeFixture(IFixture fixture, int testCaseCount)
        {
            this.fixtureCounter = new TestCounter(testCaseCount);
        }

        public virtual void AfterFixture(IFixture fixture)
        {
        }

        public virtual void FixtureSetUp(TestResult result)
        {
            if (result.State == TestState.Success)
                return;


            this.counter.FailureCount += this.fixtureCounter.TotalCount;
            this.assemblyCounter.FailureCount += this.fixtureCounter.TotalCount;
            this.fixtureCounter = null;
        }

        public virtual void FixtureTearDown(TestResult result)
        {
            if (result.State == TestState.Success || this.fixtureCounter==null)
                return;

            this.counter.RollbackResults(this.fixtureCounter);
            this.assemblyCounter.RollbackResults(this.fixtureCounter);

            this.counter.FailureCount += this.fixtureCounter.TotalCount;
            this.assemblyCounter.FailureCount += this.fixtureCounter.TotalCount;

            this.fixtureCounter = null;
        }

        public virtual void BeforeTestCase(ITestCase testCase)
        {
            this.testState = TestState.NotRun;
        }

        public virtual void AfterTestCase(ITestCase testCase)
        {
            switch (this.testState)
            {
                case TestState.NotRun:
                    throw new InvalidProgramException();
                case TestState.Failure:
                    this.counter.FailureCount++;
                    this.assemblyCounter.FailureCount++;
                    this.fixtureCounter.FailureCount++;
                    break;
                case TestState.Ignore:
                    this.counter.IgnoreCount++;
                    this.assemblyCounter.IgnoreCount++;
                    this.fixtureCounter.IgnoreCount++;
                    break;
                case TestState.Success:
                    this.counter.SuccessCount++;
                    this.assemblyCounter.SuccessCount++;
                    this.fixtureCounter.SuccessCount++;
                    break;
                default:
                    throw new NotSupportedException();
            }
        }

        public virtual void SetUp(TestResult result)
        {
            testState = result.State;
        }

        public virtual void Test(TestResult result)
        {
            if ((short)testState < (short)result.State)
                testState = result.State;
        }

        public virtual void TearDown(TestResult result)
        {
            if ((short)testState < (short)result.State)
                testState = result.State;
        }
    }
}
