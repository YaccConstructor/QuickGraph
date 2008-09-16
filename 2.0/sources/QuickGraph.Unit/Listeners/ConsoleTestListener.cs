using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Reflection;
using QuickGraph.Unit.Core;

namespace QuickGraph.Unit.Listeners
{
    public sealed class ConsoleTestListener : CounterTestListener
    {
        private bool silent = false;
        private TextWriter _out;
        private IFixture currentFixture = null;
        private TestResult currentResult = null;
        private bool useColors = true;
        private bool usePosition = true;
        private bool showProgress = true;

        public ConsoleTestListener()
            : this(Console.Out)
        { }

        public ConsoleTestListener(TextWriter writer)
        {
            if (writer==null)
                throw new ArgumentNullException("writer");
            this._out = writer;
            this.UsePosition = !System.Diagnostics.Debugger.IsAttached;
        }

        public TextWriter Out
        {
            get { return this._out; }
        }

        public bool Silent
        {
            get { return this.silent; }
            set { this.silent = value; }
        }

        public bool UseColors
        {
            get { return this.useColors; }
            set { this.useColors = value; }
        }

        public bool UsePosition
        {
            get { return this.usePosition; }
            set { this.usePosition = value; }
        }

        public bool ShowProgress
        {
            get { return this.showProgress; }
            set { this.showProgress = value; }
        }

        public override void Message(MessageImportance importance, string message)
        {
            base.Message(importance, message);
            if (silent)
                return;
            if (importance== MessageImportance.Normal || !this.UseColors)
                Console.WriteLine(message);
            else
            {
                ConsoleColor color = Console.ForegroundColor;
                try
                {
                    if (importance == MessageImportance.Low)
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                    else
                        Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine(message);
                }
                finally
                {
                    SetConsoleDefaultColor();
                }
            }
        }

        public override void Warning(string message)
        {
            base.Warning(message);
            if (silent)
                return;
            try
            {
                SetConsoleColor(TestState.Ignore);
                Console.WriteLine(message);
            }
            finally
            {
                SetConsoleDefaultColor();
            }
        }

        public override void Error(string message)
        {
            base.Error(message);
            if (silent)
                return;
            System.Runtime.CompilerServices.RuntimeHelpers.PrepareConstrainedRegions();
            try
            {
                SetConsoleColor(TestState.Failure);
                Console.WriteLine(message);
            }
            finally
            {
                SetConsoleDefaultColor();
            }
        }

        public override void Error(Exception ex)
        {
            base.Error(ex);
            if (silent)
                return;
            System.Runtime.CompilerServices.RuntimeHelpers.PrepareConstrainedRegions();
            try
            {
                Console.WriteLine(ex);
            }
            finally
            {
                SetConsoleDefaultColor();
            }
        }

        public override void BeforeBatch(TestBatch batch)
        {
            base.BeforeBatch(batch);
            if (silent)
                return;
            this.Out.WriteLine("{0}: Starting tests", DateTime.Now.ToLongTimeString());
            this.Out.Flush();
        }

        public override void AfterBatch(TestBatch batch)
        {
            base.AfterBatch(batch);
            if (silent)
                return;
            this.Out.WriteLine();
            this.Out.WriteLine("{0}: Tests finished", DateTime.Now.ToLongTimeString());
            this.Out.Flush();
        }

        public override void BeforeAssembly(TestAssembly testAssembly)
        {
            base.BeforeAssembly(testAssembly);
            if (silent)
                return;
        }

        public override void AfterAssembly(TestAssembly testAssembly)
        {
            base.AfterAssembly(testAssembly);
        }

        public override void AssemblySetUp(Result result)
        {
            base.AssemblySetUp(result);
            if (silent)
                return;
            if (result.State == TestState.Success)
                return;

            this.Out.WriteLine("AssemblySetUp FAILED!");
            this.Out.Flush();
        }

        public override void AssemblyTearDown(Result result)
        {
            base.AssemblyTearDown(result);
            if (silent)
                return;
            if (result.State == TestState.Success)
                return;

            this.Out.WriteLine("AssemblyTearDown FAILED!");
            this.Out.Flush();
        }

        public override void BeforeFixture(IFixture fixture, int testCaseCount)
        {
            base.BeforeFixture(fixture, testCaseCount);
            this.currentFixture = fixture;
        }

        public override void AfterFixture(IFixture fixture)
        {
            base.AfterFixture(fixture);
            this.currentFixture = null;
        }

        public override void FixtureSetUp(TestResult result)
        {
            base.FixtureSetUp(result);
        }

        public override void FixtureTearDown(TestResult result)
        {
            base.FixtureTearDown(result);
        }

        public override void SetUp(TestResult result)
        {
            base.SetUp(result);
            this.currentResult = result;
        }

        public override void Test(TestResult result)
        {
            base.Test(result);
            if (this.currentResult==null || this.currentResult.State== TestState.Success)
                this.currentResult = result;
        }

        public override void TearDown(TestResult result)
        {
            base.TearDown(result);
            if (this.currentResult.State== TestState.Success)
                this.currentResult = result;
        }

        public override void BeforeTestCase(ITestCase test)
        {
            base.BeforeTestCase(test);
            if (silent)
                return;
            this.WriteCounter(test);
        }


        public override void AfterTestCase(ITestCase test)
        {
            base.AfterTestCase(test);
            if (silent)
                return;
            this.WriteCounter(test);
        }

        private void WriteLine(string category, Result result, string format, params object[] args)
        {
            SetConsoleColor(result.State);
            string message = String.Format(format, args);
            Out.WriteLine("[{0}][{1}] {2}", result.State, category, message);
            this.Out.Flush();
            SetConsoleDefaultColor();
        }

        private void SetConsoleDefaultColor()
        {
            if (!this.UseColors)
                return;
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        private void SetConsoleColor(TestState state)
        {
            if (!this.UseColors)
                return;
            switch (state)
            {
                case TestState.Success:
                    Console.ForegroundColor = ConsoleColor.Gray; break;
                case TestState.Failure:
                    Console.ForegroundColor = ConsoleColor.Red; break;
                case TestState.Ignore:
                    Console.ForegroundColor = ConsoleColor.Yellow; break;
                default:
                    Console.ForegroundColor = ConsoleColor.Gray; break;
            }
        }

        private void SetCounterColor()
        {
            if (!this.UseColors)
                return;

            if (this.Counter.HasFailures)
                Console.ForegroundColor = ConsoleColor.Red;
            else
                Console.ForegroundColor = ConsoleColor.Green;
        }

        private void WriteCounter(ITestCase test)
        {
            if (silent)
                return;
            if (!this.ShowProgress)
                return;

            if (this.UsePosition)
            {
                int position = Console.CursorLeft;
                string white = new string(' ', position+1);
                Console.SetCursorPosition(0, Console.CursorTop);
                // write blanks
                Out.Write(white);
            }

            this.SetCounterColor();

            if (this.UsePosition)
                Console.SetCursorPosition(0, Console.CursorTop);
            // write test name
            string fixtureName = test.FixtureName;
            int index = fixtureName.LastIndexOf('.');
            if (index > 0)
                fixtureName = fixtureName.Substring(index + 1);
            string name = String.Format("{0}.{1}",fixtureName, test.Name);
            if (name.Length > 40)
                name = name.Substring(0, 40) + "...";
            Out.Write("{0}: {1}, {2}",
                DateTime.Now.ToLongTimeString(),
                this.Counter, 
                name);
            if (!this.UsePosition)
                Console.WriteLine();
            this.Out.Flush();
            SetConsoleDefaultColor();
        }
    }
}
