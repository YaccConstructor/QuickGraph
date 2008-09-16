using System;
using System.Diagnostics;
using System.ComponentModel;
using System.Collections.Generic;
using System.Reflection;
using System.IO;
using System.Threading;
using System.Xml;
using QuickGraph.Unit.Serialization;
using QuickGraph.Unit.Listeners;
using QuickGraph.Unit.Filters;
using QuickGraph.CommandLine;
using QuickGraph.Unit.Reports;
using QuickGraph.Unit.Core;

namespace QuickGraph.Unit
{
    public sealed class TestRunner : Component
    {
        private readonly object syncRoot = new object();
        private int successExitCode = 100;
        private int failureExitCode = 101;
        private TestRunnerArguments arguments = new TestRunnerArguments();
        private TestBatchFactory batchFactory = new TestBatchFactory();
        private ConsoleTestListener consoleListener = new ConsoleTestListener();
        private TestListenerCollection testListeners = new TestListenerCollection();
        private UnitTestHtmlReport report = null;
        private string historyReportHtml = null;
        private string historyReportXml = null;

        public TestRunner(IContainer container)
            : this(container, Assembly.GetEntryAssembly())
        {}

        public TestRunner(IContainer container, Assembly assembly)
        {
            if (container == null)
                throw new ArgumentNullException("container");
            container.Add(this);
            if (assembly != null)
                this.BatchFactory.TestAssemblies.Add(assembly);

            this.TestListeners.Add(this.consoleListener);
        }

        public ConsoleTestListener ConsoleListener
        {
            get { return this.consoleListener; }
        }

        public TestBatchFactory BatchFactory
        {
            get { return this.batchFactory; }
        }

        public TestRunnerArguments Arguments
        {
            get { return this.arguments; }
            set 
            {
                if (value == null)
                    throw new ArgumentNullException("Arguments");
                this.arguments = value; 
            }
        }

        public int SuccessExitCode
        {
            get { return this.successExitCode; }
            set { this.successExitCode = value; }
        }

        public int FailureExitCode
        {
            get { return this.failureExitCode; }
            set { this.failureExitCode = value; }
        }

        public UnitTestHtmlReport Report
        {
            get { return this.report; }
        }

        [System.Diagnostics.DebuggerStepThrough]
        public int Run(IEnumerable<string> args)
        {
            this.TestListeners.Message("QuickGraph.Unit v{0}",
                typeof(TestRunner).Assembly.GetName().Version);
            this.TestListeners.Message(
                MessageImportance.High,
                "Test Assembly: {0}", Assembly.GetEntryAssembly().FullName);

            CommandLineParser<TestRunnerArguments> parser = CommandLineParser<TestRunnerArguments>.Create();
            if (!parser.Parse(this.arguments, args))
            {
                this.TestListeners.Error("Error while parsing arguments");
                parser.ShowHelp();
                return this.FailureExitCode;
            }

            if (this.Arguments.Help)
            {
                parser.ShowHelp();
                return this.SuccessExitCode;
            }

            // run tests
            return Run();
        }

        public TestListenerCollection TestListeners
        {
            get { return this.testListeners; }
        }

        [System.Diagnostics.DebuggerStepThrough]
        public int Run()
        {
            if (this.Arguments.BreakOnStart)
                Debugger.Break();

            System.Runtime.CompilerServices.RuntimeHelpers.PrepareConstrainedRegions();
            try
            {
                AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);
                return this.InternalRun();
            }
            finally
            {
                AppDomain.CurrentDomain.AssemblyResolve -= new ResolveEventHandler(CurrentDomain_AssemblyResolve);
            }
        }

        Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            string name = args.Name.Split(',')[0];

            // handling System.Xml.Serializer
            if (name.EndsWith("XmlSerializers"))
                return null;

            this.TestListeners.Message(
                MessageImportance.Low,
                "Resolving Assembly name: {0}", name);
            foreach (Assembly testAssembly in this.BatchFactory.TestAssemblies)
            {
                string directory = Path.GetDirectoryName(testAssembly.Location);
                this.TestListeners.Message(
                    MessageImportance.Low,
                    "Probing in directory {0}", directory);

                string path = null;
                System.Runtime.CompilerServices.RuntimeHelpers.PrepareConstrainedRegions();
                try
                {
                    this.TestListeners.Message(
                        MessageImportance.Low,
                        "Probing {0}", path);
                    path = Path.Combine(directory, name + ".dll");
                    return Assembly.LoadFrom(path);
                }
                catch (Exception)
                {
                    this.TestListeners.Message(
                        MessageImportance.Low,
                        "Failed to load {0}", path);
                }

                System.Runtime.CompilerServices.RuntimeHelpers.PrepareConstrainedRegions();
                try
                {
                    this.TestListeners.Message(
                        MessageImportance.Low,
                        "Probing {0}", path);
                    path = Path.Combine(directory, name + ".exe");
                    return Assembly.LoadFrom(path);
                }
                catch (Exception)
                {
                    this.TestListeners.Message(
                        MessageImportance.Low,
                        "Failed to load {0}", path);
                    return null;
                }
            }

            return null;
        }

        private int InternalRun()
        {
            if (!this.InitializeTestBatch())
                return this.ExitCode;

            // reflect
            this.TestListeners.Message(
                MessageImportance.Low,
                "Loading tests");
            this.BatchFactory.Create();
            this.TestListeners.Message(
                MessageImportance.Low,
                "Found {0} fixtures, {1} tests",
                this.BatchFactory.Batch.GetFixtureCount(),
                this.BatchFactory.Batch.GetTestCount()
                );

            System.Runtime.CompilerServices.RuntimeHelpers.PrepareConstrainedRegions();
            string currentDirectory = Environment.CurrentDirectory;
            try
            {
                // execute
                this.TestListeners.BeforeBatch(this.BatchFactory.Batch);
                foreach (TestAssembly testAssembly in this.BatchFactory.Batch.TestAssemblies)
                {
                    // set current directory as assembly location
                    Environment.CurrentDirectory = Path.GetDirectoryName(testAssembly.Assembly.Location);
                    this.ExecuteTestAssembly(testAssembly);
                }
            }
            catch (Exception ex)
            {
                this.TestListeners.Error("Error in Test Harness!");
                this.TestListeners.Error(ex);
            }
            finally
            {
                Environment.CurrentDirectory = currentDirectory;
                this.EndTestExecution();
            }
            return this.ExitCode;
        }

        private void PrepareReports()
        {
            if (this.Arguments.GenerateReport == ReportGenerationScenario.None)
                return;

            this.report = new UnitTestHtmlReport(this.Container);
            this.report.GenerateOnDisposed = false;
            this.report.GenerateFixtureInSeparateFile = this.Arguments.GenerateFixtureReportInSeparateFiles;
            this.report.ShowFixturesSummary = this.Arguments.ShowFixturesSummary;
            this.report.EntryAssemblyName = this.BatchFactory.MainAssembly;
            if (this.Arguments.ForceReportOutputPath)
                this.report.SetOutputFolderName(this.Arguments.ReportOutputPath);
            else
                this.report.SetOutputFolderName(this.Arguments.ReportOutputPath, this.BatchFactory.MainAssembly);
            this.report.TestListener.ShowTestCaseOnSuccess = this.Arguments.ShowTestCaseOnSuccess;
            this.TestListeners.Add(this.report.TestListener);

            // we precreate the directory name
            if (!Directory.Exists(this.report.OutputFolderName))
                Directory.CreateDirectory(this.report.OutputFolderName);
            TestConsole.SetReportDirectoryName(this.report.OutputFolderName);

            this.LoadPreviousResult();
        }

        private void LoadPreviousResult()
        {
            if (!this.Arguments.UseLatestHistory)
                return;
            if (!System.IO.Directory.Exists(this.Arguments.ReportOutputPath))
                return;
            do
            {
                string latestResultFile = new ReportHistory(
                    this.Arguments.ReportOutputPath,
                    this.BatchFactory.MainAssembly).GetLatestXmlReport();
                if (latestResultFile != null)
                {
                    this.TestListeners.Message(
                        MessageImportance.Low,
                        "Found previous report: {0}", latestResultFile);
                    System.Runtime.CompilerServices.RuntimeHelpers.PrepareConstrainedRegions();
                    try
                    {
                        this.report.TestListener.SetPreviousTestBatch(latestResultFile);
                        this.TestListeners.Message(
                            MessageImportance.Low,
                            "Loaded {0} fixtures and {1} tests in previous report",
                            this.report.TestListener.TestBatchSearcher.Fixtures.Count,
                            this.report.TestListener.TestBatchSearcher.TestCases.Count);
                        break;
                    }
                    catch (Exception ex)
                    {
                        this.TestListeners.Warning("Failure while loading previous report");
                        this.TestListeners.Message(
                            MessageImportance.Low,
                            "Error: {0}", ex.Message);
                        // deleting previous report
                        ReportCleaner cleaner = new ReportCleaner(this.BatchFactory.MainAssembly);
                        cleaner.Clean(Path.GetDirectoryName(latestResultFile), this);
                    }
                }
                else
                {
                    this.TestListeners.Message(
                        MessageImportance.Low,
                        "Could not find previous result");
                    break;
                }
            } while (true);
        }

		private void CleanOldReports()
		{
			if (!this.Arguments.CleanOldReports)
				return;

            this.TestListeners.Message(
                MessageImportance.Low,
                "Cleaning old reports");
			ReportCleaner cleaner = new ReportCleaner(this.BatchFactory.MainAssembly);
			cleaner.MaxReportCount = this.Arguments.MaxReportCount;
			cleaner.Clean(this.Arguments.ReportOutputPath, this);
		}

        private void GenerateReportHistory()
        {
            if (!this.Arguments.GenerateReportHistory)
                return;

            using (UnitTestHistoryHtmlReport historyReport = new UnitTestHistoryHtmlReport(this.Container))
            {
                historyReport.SetOutputFolderName(this.Arguments.ReportOutputPath);
                historyReport.EntryAssemblyName = this.BatchFactory.MainAssembly;
                historyReport.SetOutputFileName("test_history");
                historyReport.Generate();
                this.historyReportHtml = historyReport.OutputFileName;
                this.historyReportXml = historyReport.OutputXmlFileName;
                this.TestListeners.Message(
                    MessageImportance.High,
                    "History: {0}", new Uri(historyReport.OutputFileName).AbsoluteUri);

                if (this.Arguments.OpenReportHistory)
                {
                     Thread thread = new Thread(delegate(object name) { Process.Start(name.ToString()); });
                     thread.Start(historyReport.OutputFileName);
                }
            }
        }

        private bool InitializeTestBatch()
        {
            this.BatchFactory.FixtureFilter = this.Arguments.GetFixtureFilter();
            this.BatchFactory.TestCaseFilter = this.Arguments.GetTestCaseFilter();

			this.CleanOldReports();
            this.PrepareReports();

            // adding assemblies
            foreach (string assemblyName in this.Arguments.TestAssemblies)
            {
                if (!this.LoadTestAssembly(assemblyName))
                    return false;
            }
            return true;
        }

        private bool LoadTestAssembly(string assemblyName)
        {
            System.Runtime.CompilerServices.RuntimeHelpers.PrepareConstrainedRegions();
            try
            {
                Assembly testAssembly = Assembly.LoadFile(assemblyName);
                this.BatchFactory.TestAssemblies.Add(testAssembly);
                return true;
            }
            catch (Exception ex)
            {
                this.TestListeners.Error("Error while loading {0}", assemblyName);
                this.TestListeners.Error(ex);
                return false;
            }
        }

        [System.Diagnostics.DebuggerStepThrough]
        private void ExecuteTestAssembly(TestAssembly testAssembly)
        {
            this.TestListeners.BeforeAssembly(testAssembly);

            // run assemblysetup
            System.Runtime.CompilerServices.RuntimeHelpers.PrepareConstrainedRegions();
            try
            {
                if (this.RunAssemblySetUp(testAssembly))
                {
                    foreach (IFixture fixture in testAssembly.Fixtures)
                    {
                        using (FixtureRunner runner =
                            new FixtureRunner(
                            fixture,
                            testAssembly.GetTestCasesFromFixture(fixture),
                            this.TestListeners)
                            )
                        {
                            runner.Run();
                        }
                        // collect GC
                        GC.WaitForPendingFinalizers();
                        GC.Collect();
                    }
                }
            }
            finally
            {
                // run assembly teardown
                this.RunAssemblyTearDown(testAssembly);
                this.TestListeners.AfterAssembly(testAssembly);
            }
        }

        private void EndTestExecution()
        {
            // finish
            this.TestListeners.AfterBatch(this.BatchFactory.Batch);

            // generate report
            this.GenerateReport();
            this.GenerateReportHistory();

            this.TestListeners.ReportsGenerated(
                this.historyReportXml,
                this.historyReportHtml,
                this.report.OutputXmlFileName,
                this.report.OutputFileName
                );

            if (!this.Counter.Succeeded)
                this.TestListeners.Message(
                    MessageImportance.High,
                    "Test FAILED");
            else
               this.TestListeners.Message(
                    MessageImportance.High,
                   "Test SUCCESS");
        }

        private bool RunAssemblySetUp(TestAssembly testAssembly)
        {
            if (testAssembly.AssemblySetUp == null)
                return true;

            Result result = new Result(testAssembly.AssemblySetUp.Name);
            System.Runtime.CompilerServices.RuntimeHelpers.PrepareConstrainedRegions();
            try
            {
                result.Start();
                testAssembly.AssemblySetUp.Invoke(null, null);
                result.Success();
            }
            catch (Exception ex)
            {
                Exception current = ex;
                if (current is TargetInvocationException)
                    current = current.InnerException;
                if (current is QuickGraph.Unit.Exceptions.IgnoreException)
                    result.Ignore();
                else
                    result.Fail(current);
            }
            this.TestListeners.AssemblySetUp(result);

            return result.State == TestState.Success;
        }

        private void RunAssemblyTearDown(TestAssembly testAssembly)
        {
            if (testAssembly.AssemblyTearDown == null)
                return;

            Result result = new Result(testAssembly.AssemblyTearDown.Name);
            System.Runtime.CompilerServices.RuntimeHelpers.PrepareConstrainedRegions();
            try
            {
                result.Start();
                testAssembly.AssemblyTearDown.Invoke(null, null);
                result.Success();
            }
            catch (Exception ex)
            {
                Exception current = ex;
                if (current is TargetInvocationException)
                    current = current.InnerException;
                if (current is QuickGraph.Unit.Exceptions.IgnoreException)
                    result.Ignore();
                else
                    result.Fail(current);
            }
            this.TestListeners.AssemblyTearDown(result);
        }

        public TestCounter Counter
        {
            get { return this.consoleListener.Counter; }
        }

        public int ExitCode
        {
            get 
            {
                if (this.consoleListener.Counter == null)
                    return this.FailureExitCode;
                return (this.consoleListener.Counter.HasFailures) ? 
                    this.FailureExitCode: this.SuccessExitCode; 
            }
        }

        private void GenerateReport()
        {
            switch (this.Arguments.GenerateReport)
            {
                case ReportGenerationScenario.None:
                    return;
                case ReportGenerationScenario.OnFailure:
                    if (!this.Counter.HasFailures)
                        return;
                    else
                        break;
            }

            if (this.Arguments.ForceReportOutputPath)
                this.report.SetOutputFileName("TestWithResults");
            else
                this.report.SetOutputFileName(this.BatchFactory.MainAssembly);
            this.report.Generate();
            this.TestListeners.Message(
                MessageImportance.High,
                "Test report: {0}", new Uri(this.report.OutputFileName).AbsoluteUri);
            if (this.Arguments.OpenReport)
            {
                Thread thread = new Thread(new ThreadStart(this.OpenReport));
                thread.Start();
            }
        }

        private void OpenReport()
        {
            System.Diagnostics.Process.Start(report.OutputFileName);
        }

	    public static int TestMain(IEnumerable<string> args)
	    {
            System.Runtime.CompilerServices.RuntimeHelpers.PrepareConstrainedRegions();
            try
            {
                using (UnitContainer container = new UnitContainer())
                {
                    using (TestRunner runner = new TestRunner(container))
                    {
                        runner.Run(args);
                        return runner.ExitCode;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in Test Harness!");
                Console.WriteLine(ex.ToString());
                return 101;
            }
    	}
    }
}
