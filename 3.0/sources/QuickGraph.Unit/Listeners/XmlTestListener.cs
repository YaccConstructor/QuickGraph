using System;
using System.Collections.Generic;
using System.Reflection;

using QuickGraph.Unit.Serialization;
using QuickGraph.Unit.Reports;
using QuickGraph.Unit.Core;

namespace QuickGraph.Unit.Listeners
{
    public sealed class XmlTestListener : ITestListener
    {
        private XmlTestBatchSearcher testBatchSearcher = null;
        private XmlTestBatch testBatch = new XmlTestBatch();
        private bool showTestCaseOnSuccess = false;
        private XmlTestAssembly currentTestAssembly = null;
        private XmlFixture currentFixture = null;
        private XmlTestCase currentTest = null;

        public bool ShowTestCaseOnSuccess
        {
            get { return this.showTestCaseOnSuccess; }
            set { this.showTestCaseOnSuccess = value; }
        }

        public XmlTestBatch TestBatch
        {
            get { return this.testBatch; }
        }

        public void SetPreviousTestBatch(string fileName)
        {
            XmlTestBatch testBatch = UnitSerializer.Deserialize(fileName);
            if (testBatch != null)
                SetPreviousTestBatch(testBatch);
        }

        public void SetPreviousTestBatch(XmlTestBatch testBatch)
        {
            if (testBatch == null)
                throw new ArgumentNullException("testBatch");
            this.testBatchSearcher = new XmlTestBatchSearcher(testBatch);
        }

        public XmlTestBatchSearcher TestBatchSearcher
        {
            get { return this.testBatchSearcher; }
        }

        public void ReportsGenerated(
            string historyXml,
            string historyHtml,
            string batchXml,
            string batchHtml)
        {}

        public void Message(MessageImportance importance, string message)
        {
            if (this.TestBatch != null)
                this.TestBatch.Log.LogEntries.Add(new XmlLogEntry(LogLevel.Message, message));
        }

        public void Message(string message)
        {
            if (this.TestBatch!=null)
                this.TestBatch.Log.LogEntries.Add(new XmlLogEntry(LogLevel.Message, message));
        }

        public void Warning(string message)
        {
            if (this.TestBatch != null)
                this.TestBatch.Log.LogEntries.Add(new XmlLogEntry(LogLevel.Warning, message));
        }

        public void Error(string message)
        {
            if (this.TestBatch != null)
                this.TestBatch.Log.LogEntries.Add(new XmlLogEntry(LogLevel.Error, message));
        }

        public void Error(Exception ex)
        {
            if (this.TestBatch != null)
                this.TestBatch.Log.LogEntries.Add(new XmlLogEntry(ex));
        }

        public void BeforeBatch(TestBatch batch)
        {
            this.testBatch.SetMainAssembly(batch.MainTestAssembly.Assembly);
            if (this.testBatchSearcher!=null)
                this.testBatch.HasHistory = true;
            this.testBatch.StartTime = DateTime.Now.ToString("u");
        }

        public void AfterBatch(TestBatch batch)
        {
            if (this.testBatch != null)
            {
                this.testBatch.EndTime = DateTime.Now.ToString("u");
                this.testBatch.UpdateCounter();
            }
		}

        public void BeforeAssembly(TestAssembly testAssembly)
        {
            this.currentTestAssembly = new XmlTestAssembly(testAssembly.Assembly.GetName(), testAssembly.Assembly.Location);
            this.currentTestAssembly.StartTime = DateTime.Now;
            this.testBatch.TestAssemblies.Add(this.currentTestAssembly);
        }

        public void AfterAssembly(TestAssembly testAssembly)
        {
            if (this.currentTestAssembly != null)
            {
                this.currentTestAssembly.EndTime = DateTime.Now;
                this.currentTestAssembly = null;
            }
        }

        public void AssemblySetUp(Result result)
        {
            if (this.currentTestAssembly!=null)
                this.currentTestAssembly.AssemblySetUp = new XmlResult(result);
        }

        public void AssemblyTearDown(Result result)
        {
            if (this.currentTestAssembly != null)
                this.currentTestAssembly.AssemblyTearDown = new XmlResult(result);
        }

        public void BeforeFixture(IFixture fixture, int testCaseCount)
        {
            this.currentFixture = new XmlFixture(fixture, testCaseCount, 
                GetCategories(fixture), this.currentTestAssembly.Fixtures.Count);
	        this.currentTestAssembly.Fixtures.Add(this.currentFixture);
        }

        private static string GetCategories(IFixture fixture)
        {
            using (System.IO.StringWriter sw = new System.IO.StringWriter())
            {
                bool first = true;
                foreach (string category in fixture.Categories)
                {
                    if (first)
                    {
                        sw.Write(category);
                        first = false;
                    }
                    else
                        sw.Write(";{0}", category);
                }
                return sw.ToString();
            }
        }

        public void AfterFixture(IFixture fixture)
        {
			if (this.currentFixture != null)
			{
				this.currentFixture.UpdateCounter();
				this.currentFixture = null;
			}
        }

        public void FixtureSetUp(TestResult result)
        {
            if (this.currentFixture != null)
            {
                this.currentFixture.FixtureSetUp = new XmlResult(result);
                this.currentFixture.Duration += result.Duration;
            }
        }

        public void FixtureTearDown(TestResult result)
        {
            if (this.currentFixture != null)
            {
                this.currentFixture.FixtureTearDown = new XmlResult(result);
                this.currentFixture.Duration += result.Duration;
            }
        }

        public void SetUp(TestResult result)
        {
            if (this.currentTest != null)
            {
                this.currentTest.SetUp = new XmlResult(result);
                this.currentFixture.Duration += result.Duration;
            }
        }

        public void Test(TestResult result)
        {
            if (this.currentTest != null)
            {
                this.currentTest.Test = new XmlResult(result);
                this.currentFixture.Duration += result.Duration;
            }
        }

        public void TearDown(TestResult result)
        {
            if (this.currentTest != null)
            {
                this.currentTest.TearDown = new XmlResult(result);
                this.currentFixture.Duration += result.Duration;
            }
        }

        public void BeforeTestCase(ITestCase test)
        {
            this.currentTest = new XmlTestCase(String.Format("{0}t{1}",
                        this.currentFixture.ID,
                        this.currentFixture.TestCases.Count),
                        test.Name);
            this.currentFixture.TestCases.Add(this.currentTest);
        }

        public void AfterTestCase(ITestCase test)
        {
            if (this.TestBatchSearcher != null)
            {
                XmlTestCase previous = this.TestBatchSearcher.GetTestCase(this.currentFixture, this.currentTest);
                if (previous == null)
                {
                    this.currentTest.History = XmlTestHistory.New;
                }
                else
                {
                    if (
                        this.currentTest.State == TestState.Success
                        && previous.State != TestState.Success)
                    {
                        this.currentTest.History = XmlTestHistory.Fixed;
                    }
                    else if (
                        this.currentTest.State == TestState.Failure &&
                        previous.State != TestState.Failure
                        )
                    {
                        this.currentTest.History = XmlTestHistory.Failure;
                    }
                }
            }

            if (!this.ShowTestCaseOnSuccess && this.currentTest.State == TestState.Success)
            {
                this.currentTest.SetUp = null;
                this.currentTest.Test = null;
                this.currentTest.TearDown = null;
            }
            this.currentTest = null;
        }
    }
}
