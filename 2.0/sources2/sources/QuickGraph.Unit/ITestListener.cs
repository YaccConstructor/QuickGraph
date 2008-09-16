using System;
using System.Reflection;
using QuickGraph.Unit.Core;

namespace QuickGraph.Unit
{
    public interface ITestListener
    {
        void Message(MessageImportance importance, string message);
        void Warning(string message);
        void Error(string message);
        void Error(Exception ex);
        void ReportsGenerated(
            string historyXml,
            string historyHtml,
            string batchXml,
            string batchHtml);

        void BeforeBatch(TestBatch batch);
        void AfterBatch(TestBatch batch);

        void BeforeAssembly(TestAssembly testAssembly);
        void AfterAssembly(TestAssembly testAssembly);

        void AssemblySetUp(Result result);
        void AssemblyTearDown(Result result);

        void BeforeFixture(IFixture fixture, int testCaseCount);
        void AfterFixture(IFixture fixture);

        void FixtureSetUp(TestResult result);
        void FixtureTearDown(TestResult result);

        void BeforeTestCase(ITestCase testCase);
        void AfterTestCase(ITestCase testCase);

        void SetUp(TestResult result);
        void Test(TestResult result);
        void TearDown(TestResult result);
    }
}
