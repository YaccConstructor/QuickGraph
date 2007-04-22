using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using QuickGraph.Unit.Core;

namespace QuickGraph.Unit.Listeners
{
    public sealed class TestListenerCollection : List<ITestListener>,
        ITestListener
    {
        public void ReportsGenerated(
            string historyXml,
            string historyHtml,
            string batchXml,
            string batchHtml)
        {
            foreach(ITestListener listener in this)
                listener.ReportsGenerated(historyXml, historyHtml, batchXml, batchHtml);
        }

        public void Message(string format, params object[] args)
        {
            Message(MessageImportance.Normal, format, args);
        }

        public void Message(string message)
        {
            Message(MessageImportance.Normal, message);
        }

        public void Message(MessageImportance importance, string format, params object[] args)
        {
            Message(importance, string.Format(format, args));
        }

        public void Message(MessageImportance importance, string message)
        {
            foreach (ITestListener testListener in this)
                testListener.Message(importance, message);
        }

        public void Warning(string format, params object[] args)
        {
            Warning(string.Format(format, args));
        }

        public void Warning(string message)
        {
            foreach (ITestListener testListener in this)
                testListener.Warning(message);
        }

        public void Error(string format, params object[] args)
        {
            Error(string.Format(format, args));
        }

        public void Error(string message)
        {
            foreach (ITestListener testListener in this)
                testListener.Error(message);
        }

        public void Error(Exception ex)
        {
            foreach (ITestListener testListener in this)
                testListener.Error(ex);
        }

        public void BeforeBatch(TestBatch batch)
        {
            foreach (ITestListener testListener in this)
                testListener.BeforeBatch(batch);
        }

        public void AfterBatch(TestBatch batch)
        {
            foreach (ITestListener testListener in this)
                testListener.AfterBatch(batch);
        }

        public void BeforeAssembly(TestAssembly testAssembly)
        {
            foreach (ITestListener testListener in this)
                testListener.BeforeAssembly(testAssembly);
        }

        public void AfterAssembly(TestAssembly testAssembly)
        {
            foreach (ITestListener testListener in this)
                testListener.AfterAssembly(testAssembly);
        }

        public void AssemblySetUp(Result result)
        {
            foreach (ITestListener testListener in this)
                testListener.AssemblySetUp(result);
        }

        public void AssemblyTearDown(Result result)
        {
            foreach (ITestListener testListener in this)
                testListener.AssemblyTearDown(result);
        }

        public void BeforeFixture(IFixture fixture, int testCaseCount)
        {
            foreach (ITestListener testListener in this)
                testListener.BeforeFixture(fixture, testCaseCount);
        }

        public void AfterFixture(IFixture fixture)
        {
            foreach (ITestListener testListener in this)
                testListener.AfterFixture(fixture);
        }

        public void FixtureSetUp(TestResult result)
        {
            foreach (ITestListener testListener in this)
                testListener.FixtureSetUp(result);
        }
        public void FixtureTearDown(TestResult result)
        {
            foreach (ITestListener testListener in this)
                testListener.FixtureTearDown(result);
        }

        public void SetUp(TestResult result)
        {
            foreach (ITestListener testListener in this)
                testListener.SetUp(result);
        }
        public void TearDown(TestResult result)
        {
            foreach (ITestListener testListener in this)
                testListener.TearDown(result);
        }

        public void Test(TestResult result)
        {
            foreach (ITestListener testListener in this)
                testListener.Test(result);
        }

        public void BeforeTestCase(ITestCase test)
        {
            foreach (ITestListener testListener in this)
                testListener.BeforeTestCase(test);
        }

        public void AfterTestCase(ITestCase test)
        {
            foreach (ITestListener testListener in this)
                testListener.AfterTestCase(test);
        }
    }
}
