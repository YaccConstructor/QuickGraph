using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using QuickGraph.CommandLine;
using QuickGraph.Unit.Filters;
using QuickGraph.Unit.Reports;
using QuickGraph.Unit.Serialization;
using QuickGraph.Unit.Core;

namespace QuickGraph.Unit
{
    [XmlRoot("unit")]
    public sealed class TestRunnerArguments
    {
        [Argument(
            ShortName = "h",
            LongName = "help",
            Description = "Display help")]
        [XmlAttribute("help")]
        public bool Help = false;

        [Argument(
            ShortName = "gr",
            LongName = "generate-report",
            Description = "specify if a report has to be generated"
            )]
        [XmlAttribute("generate-report")]
        public ReportGenerationScenario GenerateReport = ReportGenerationScenario.Always;

        [Argument(
            ShortName="stcos",
            LongName="show-test-case-on-success",
            Description="Show test case on success in the reports"
            )]
        [XmlAttribute("show-test-case-on-success")]
        public bool ShowTestCaseOnSuccess = true;

        [Argument(
            ShortName = "or",
            LongName = "open-report",
            Description = "open report after created"
            )]
        [XmlAttribute("open-report")]
        public bool OpenReport = false;

        [Argument(
            ShortName = "rf",
            LongName = "report-folder",
            Description = "specify report folder"
            )]
        [XmlAttribute("report-folder")]
        public string ReportOutputPath = "Reports";

        [Argument(
            ShortName="frop",
            LongName="force-report-folder",
            Description="Disable automatic report folder name generation"
            )]
        public bool ForceReportOutputPath = false;

		[Argument(
			ShortName = "gfrisf",
			LongName="generate-fixture-report-in-separate-files",
			Description="Generates fixture reports in separate HTML files"
			)]
        [XmlAttribute("generate-fixture-report-in-separate-files")]
		public bool GenerateFixtureReportInSeparateFiles = false;

        [Argument(
            ShortName="sfs",
            LongName="show-fixtures-summary",
            Description="Shows a summary of the fixtures")]
        public bool ShowFixturesSummary = true;

        [Argument(
            ShortName = "ulh",
            LongName="use-latest-history",
            Description="Use latest test report to determine new failures, fixed tests"
            )]
        [XmlAttribute("use-latest-history")]
        public bool UseLatestHistory = true;

		[Argument(
			ShortName="cor",
			LongName="clean-old-reports",
			Description="Automatically deletes old report folders"
			)]
        [XmlAttribute("clean-old-reports")]
		public bool CleanOldReports = true;

		[Argument(
			ShortName="mrc",
			LongName="max-report-count",
			Description="Number of old report stored on disk. Older reports are automatically deleted (if clean-reports is true)"
			)]
        [XmlAttribute("max-report-count")]
		public int MaxReportCount = 25;

        [Argument(
            ShortName="grh",
            LongName="generate-report-history",
            Description="Generates an history of the previous and current results"
            )]
        [XmlAttribute("generate-report-history")]
        public bool GenerateReportHistory = true;

        [Argument(
            ShortName="orh",
            LongName="open-report-history",
            Description="Opens the report history on completion"
            )]
        [XmlAttribute("open-report-history")]
        public bool OpenReportHistory = true;

        [Argument(
            ShortName = "bs",
            LongName = "break-on-start",
            Description = "Debugger break on start"
            )]
        [XmlAttribute("break-on-start")]
        public bool BreakOnStart = false;

        [Argument(
            ShortName ="rfa",
            LongName = "run-failures",
            Description="Run failures from report"
            )]
        [XmlArray("run-failures")]
        [XmlArrayItem("run-failure")]
        public List<string> RunFailures = new List<string>();

        [Argument(
            ShortName = "ff",
            LongName = "fixture-filter",
            Description = "fixture name filters (fixture name must contain the filter string)"
            )]
        [XmlArray("fixture-filters")]
        [XmlArrayItem("fixture-filter")]
        public List<string> FixtureFilters = new List<string>();

        [Argument(
            ShortName = "tcf",
            LongName = "test-case-filter",
            Description = "test case name filters (test case name must contain the filter string"
            )]
        [XmlArray("test-case-filters")]
        [XmlArrayItem("test-case-filter")]
        public List<string> TestCaseFilters = new List<string>();

        [Argument(
            ShortName="c",
            LongName="category",
            Description="Test categories to run"
            )]
        [XmlArray("categories")]
        [XmlArrayItem("category")]
        public List<string> Categories = new List<string>();

        [Argument(
            ShortName = "ta",
            LongName = "test-assembly",
            Description = "Test assemblies")]
        [XmlArray("test-assemblies")]
        [XmlArrayItem("test-assembly")]
        public List<String> TestAssemblies = new List<string>();

        [Argument(
            ShortName="ucf",
            LongName="use-current-fixures",
            Description="Run fixture tagged with [CurrentFixture] only")]
        public bool UseCurrentFixtures = false;

        public IFixtureFilter GetFixtureFilter()
        {
            IFixtureFilter filter=null;
            if (this.FixtureFilters.Count != 0)
                filter = new ScopeFixtureFilter(this.FixtureFilters);

            if (this.Categories.Count != 0)
            {
                if (filter == null)
                    filter = new CategoryFixtureFilter(this.Categories);
                else
                    filter = new AndFixtureFilter(
                        filter,
                        new CategoryFixtureFilter(this.Categories)
                        );
            }

            if (this.RunFailures.Count != 0)
            {
                foreach (string runFailureReport in this.RunFailures)
                {
                    if (!File.Exists(runFailureReport))
                        throw new ArgumentException("Could not find failure file "+ runFailureReport);
                    XmlTestBatch testBatch = UnitSerializer.Deserialize(runFailureReport);
                    if (testBatch != null)
                    {
                        FailureFilter failureFilter = new FailureFilter(testBatch);
                        if (filter == null)
                            filter = failureFilter;
                        else
                            filter = new AndFixtureFilter(filter, failureFilter);
                    }
                }
            }

            if (this.UseCurrentFixtures)
            {
                if (filter == null)
                    filter = new CurrentFixtureFilter();
                else
                    filter = new AndFixtureFilter(filter, new CurrentFixtureFilter());
            }

            if (filter == null)
                filter = new AnyFixtureFilter();
            return filter;
        }

        public ITestCaseFilter GetTestCaseFilter()
        {
            ITestCaseFilter filter = null;

            if (this.TestCaseFilters.Count != 0)
                return new ScopeTestCaseFilter(this.TestCaseFilters);

            if (this.RunFailures.Count != 0)
            {
                foreach (string runFailureReport in this.RunFailures)
                {
                    if (!File.Exists(runFailureReport))
                        throw new ArgumentException("Could not find failure file " + runFailureReport);
                    XmlTestBatch testBatch = UnitSerializer.Deserialize(runFailureReport);
                    if (testBatch != null)
                    {
                        FailureFilter failureFilter = new FailureFilter(testBatch);
                        if (filter == null)
                            filter = failureFilter;
                        else
                            filter = new AndTestCaseFilter(filter, failureFilter);
                    }
                }
            }

            if (filter==null)
                filter = new AnyTestCaseFilter();
            return filter;
        }
    }
}
