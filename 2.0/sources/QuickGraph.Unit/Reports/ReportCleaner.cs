using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace QuickGraph.Unit.Reports
{
	public sealed class ReportCleaner
	{
        private Regex pattern;
		private int maxReportCount = 25;

        public ReportCleaner(string testAssemblyName)
        {
            this.pattern = ReportPath.GetRegex(testAssemblyName);
        }

		public int MaxReportCount
		{
			get { return this.maxReportCount; }
			set { this.maxReportCount = value; }
		}

		public void Clean(string path, TestRunner runner)
		{
            // to do implement
		}
	}
}
