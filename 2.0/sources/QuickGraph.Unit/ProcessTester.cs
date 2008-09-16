using System.Collections;
using System.Collections.Specialized;
using System;
using System.Diagnostics;
using System.IO;

namespace QuickGraph.Unit
{
    public class ProcessTester : IDisposable
    {
        private string fileName;
        private string[] args;
        private int expectedExitCode = 0;
        private int exitCode;
        private bool throwOnWrongExitCode = true;
        private int timeOut = int.MaxValue;
        private Process process;
        private bool useShellExecute = false;
        private bool redirectConsole = true;
        private string consoleOut;
        private string consoleError;
        private bool dumpConsoleOnSuccess = false;
        private StringDictionary environmentVariables = new StringDictionary();

        public static void StartAndRun(string fileName, params string[] args)
        {
            using (ProcessTester tester = new ProcessTester(fileName, args))
            {
                tester.ThrowOnWrongExitCode = false;
                tester.Run();
            }
        }

        public static void StartAndRun(string fileName, int exitCode, params string[] args)
        {
            using (ProcessTester tester = new ProcessTester(fileName, args))
            {
                tester.ExpectedExitCode = exitCode;
                tester.Run();
            }
        }

        public ProcessTester()
        { }

        public ProcessTester(
            string fileName,
            params string[] args)
        {
            if (fileName == null)
                throw new ArgumentNullException("fileName");
            this.fileName = fileName;
            this.args = args;
        }

        public void Dispose()
        {
            if (this.process != null)
            {
                if (!this.process.HasExited)
                    this.process.Kill();
                this.process.Dispose();
                this.process = null;
                GC.SuppressFinalize(this);
            }
        }

        public string ConsoleOut
        {
            get 
            {
                return this.consoleOut; 
            }
        }

        public string ConsoleError
        {
            get 
            {
                return this.consoleError; 
            }
        }

        public bool UseShellExecute
        {
            get { return this.useShellExecute; }
            set { this.useShellExecute = value; }
        }

        public bool DumpConsoleOnSuccess
        {
            get { return this.dumpConsoleOnSuccess; }
            set { this.dumpConsoleOnSuccess = value; }
        }

        public StringDictionary EnvironmentVariables
        {
            get { return this.environmentVariables; }
        }

        public bool ThrowOnWrongExitCode
        {
            get { return this.throwOnWrongExitCode; }
            set { this.throwOnWrongExitCode = value; }
        }

        public int ExitCode
        {
            get { return this.exitCode; }
        }

        public int ExpectedExitCode
        {
            get { return this.expectedExitCode; }
            set { this.expectedExitCode = value; }
        }

        public int TimeOut
        {
            get { return this.timeOut; }
            set { this.timeOut = value; }
        }

        public bool RedirectConsole
        {
            get { return this.redirectConsole; }
            set { this.redirectConsole = value; }
        }

        public string GetArguments()
        {
            StringWriter sw = new StringWriter();
            foreach (string arg in this.args)
                sw.Write(" {0} ", arg);
            return sw.ToString();
        }

        public bool Run(string fileName, params string[] args)
        {
            if (string.IsNullOrEmpty(fileName))
                throw new ArgumentNullException("fileName");
            this.fileName = fileName;
            this.args = args;

            return this.Run();
        }

        public bool Run()
        {
            ProcessStartInfo info = new ProcessStartInfo(
                this.fileName,
                this.GetArguments()
                );
            info.CreateNoWindow = true;
            info.UseShellExecute = this.UseShellExecute;
            if (!this.UseShellExecute && this.RedirectConsole)
            {
                info.RedirectStandardOutput = true;
                info.RedirectStandardError = true;
                this.consoleError = "";
                this.consoleOut = "";
            }
            else
            {
                info.RedirectStandardOutput = false;
                info.RedirectStandardError = false;
            }
            foreach (DictionaryEntry de in this.EnvironmentVariables)
                info.EnvironmentVariables.Add((string)de.Key, (string)de.Value);

            Console.WriteLine("\"{0}\" {1}", info.FileName, info.Arguments);

            this.process = Process.Start(info);
            this.process.Start();

            // getting console output
            if (!this.UseShellExecute && this.RedirectConsole)
            {
                this.consoleOut += this.process.StandardOutput.ReadToEnd();
                this.consoleError += this.process.StandardError.ReadToEnd();
            }

            bool timedOut = process.WaitForExit(this.TimeOut);
            this.process.Refresh();

            // kill process if necessary
            if (!process.HasExited)
                process.Kill();

            // getting console output
            if (!this.UseShellExecute && this.RedirectConsole)
            {
                this.consoleOut += this.process.StandardOutput.ReadToEnd();
                this.consoleError += this.process.StandardError.ReadToEnd();
            }

            // check time out
            if (!timedOut)
            {
                this.DumpConsoles();
                Assert.Fail("Process timed out");
            }

            // check return value
            this.exitCode = process.ExitCode;

            if (this.ExitCode!= this.ExpectedExitCode || this.DumpConsoleOnSuccess)
                this.DumpConsoles();

            if (this.ThrowOnWrongExitCode)
            {
                Assert.AreEqual(this.ExpectedExitCode, this.ExitCode,
                    "Process ExitCode ({0}) does not match expected ExitCode ({1})",
                    this.ExitCode, this.ExpectedExitCode);
            }

            return this.ExitCode == this.ExpectedExitCode;
        }

        private void DumpConsoles()
        {
            if (!string.IsNullOrEmpty(this.ConsoleOut))
                Console.Out.WriteLine(this.ConsoleOut);
            if (!string.IsNullOrEmpty(this.ConsoleError))
                Console.Error.WriteLine(this.ConsoleError);
        }
    }
}
