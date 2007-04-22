using System;
using System.Collections.Generic;
using System.Text;
using MbUnit.Core;

namespace QuickGraph.Tests
{
    class Program
    {
        static int Main(string[] args)
        {
            using (AutoRunner runner = new AutoRunner())
            {
                runner.Load();
                runner.Run();
                runner.ReportToHtml();
                return runner.ExitCode;
            }
        }
    }
}
