using System;
using System.Collections;
using System.CodeDom.Compiler;
using System.IO;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using Microsoft.CSharp;
using Microsoft.VisualBasic;
using QuickGraph.Unit.Exceptions;

namespace QuickGraph.Unit
{
    public static class CompilerAssert
    {
        #region Compilers
        private static CodeDomProvider csharp;
        private static CodeDomProvider vb;
        /// <summary>
        /// Gets the C# compiler from <see cref="CSharpCodeProvider"/>.
        /// </summary>
        /// <value>
        /// C# compiler.
        /// </value>
        public static CodeDomProvider CSharpProvider
        {
            get
            {
                if (csharp==null)
                    csharp = new CSharpCodeProvider();
                return csharp;
            }
        }

        /// <summary>
        /// Gets the VB.NET compiler from <see cref="VBCodeProvider"/>.
        /// </summary>
        /// <value>
        /// VB.NET compiler.
        /// </value>
        public static CodeDomProvider VBProvider
        {
            get
            {
                if (vb==null)
                        vb = new VBCodeProvider();
                return vb;
            }
        }
        #endregion

        /// <summary>
        /// Verifies that <paramref name="source"/> compiles using the provided compiler.
        /// </summary>
        /// <param name="compiler">Compiler instance</param>
        /// <param name="source">Source code to compile</param>
        public static CompilerResults Compiles(CodeDomProvider provider, string source)
        {
            Assert.IsNotNull(provider);
            Assert.IsNotNull(source);
            CompilerParameters ps = new CompilerParameters();
            ps.GenerateInMemory = true;
            return Compiles(provider, ps, source);
        }

        /// <summary>
        /// Verifies that <paramref name="source"/> compiles using the provided compiler.
        /// </summary>
        /// <param name="compiler">Compiler instance</param>
        /// <param name="source">Source code to compile</param>
        public static CompilerResults Compiles(CodeDomProvider provider, Stream source)
        {
            Assert.IsNotNull(provider);
            Assert.IsNotNull(source);
            CompilerParameters ps = new CompilerParameters();
            return Compiles(provider, ps, source);
        }

        /// <summary>
        /// Verifies that <paramref name="source"/> compiles using the provided compiler.
        /// </summary>
        /// <param name="compiler">Compiler instance</param>
        /// <param name="references">Referenced assemblies</param>
        /// <param name="source">Source code to compile</param>
        public static CompilerResults Compiles(CodeDomProvider provider, StringCollection references, string source)
        {
            Assert.IsNotNull(provider);
            Assert.IsNotNull(references);
            Assert.IsNotNull(source);
            CompilerParameters ps = new CompilerParameters();
            foreach (string ra in references)
                ps.ReferencedAssemblies.Add(ra);

            return Compiles(provider, ps, source);
        }

        /// <summary>
        /// Verifies that <paramref name="source"/> compiles using the provided compiler.
        /// </summary>
        /// <param name="compiler">
        /// <see cref="ICodeCompiler"/> instance.</param>
        /// <param name="options">Compilation options</param>
        /// <param name="source">source to compile</param>
        public static CompilerResults Compiles(CodeDomProvider provider, CompilerParameters options, string source)
        {
            Assert.IsNotNull(provider);
            Assert.IsNotNull(options);
            Assert.IsNotNull(source);
            return Compiles(provider, options, source, false);
        }

        /// <summary>
        /// Verifies that <paramref name="source"/> compiles using the provided compiler.
        /// </summary>
        /// <param name="compiler">
        /// <see cref="ICodeCompiler"/> instance.</param>
        /// <param name="options">Compilation options</param>
        /// <param name="source">Source to compile</param>
        /// <param name="throwOnWarning">
        /// true if assertion should throw if any warning.
        /// </param>
        public static CompilerResults Compiles(CodeDomProvider provider, CompilerParameters options, string source, bool throwOnWarning)
        {
            Assert.IsNotNull(provider);
            Assert.IsNotNull(options);
            CompilerResults results = provider.CompileAssemblyFromSource(options, source);
            if (results.Errors.HasErrors)
            {
                DisplaySource(source);
                DisplayErrors(results, Console.Out);
                throw new CompilationException(provider, options, results, source);
            }
            if (throwOnWarning && results.Errors.HasWarnings)
            {
                DisplaySource(source);
                DisplayErrors(results, Console.Out);
                throw new CompilationException(provider, options, results, source);
            }
            return results;
        }

        /// <summary>
        /// Verifies that <paramref name="source"/> compiles using the provided compiler.
        /// </summary>
        /// <param name="compiler">
        /// <see cref="ICodeCompiler"/> instance.</param>
        /// <param name="options">Compilation options</param>
        /// <param name="source">Stream containing the source to compile</param>
        public static CompilerResults Compiles(CodeDomProvider provider, CompilerParameters options, Stream source)
        {
            return Compiles(provider, options, source, false);
        }

        /// <summary>
        /// Verifies that <paramref name="source"/> compiles using the provided compiler.
        /// </summary>
        /// <param name="compiler">
        /// <see cref="ICodeCompiler"/> instance.</param>
        /// <param name="options">Compilation options</param>
        /// <param name="source">Stream containing the source to compile</param>
        /// <param name="throwOnWarning">
        /// true if assertion should throw if any warning.
        /// </param>
        public static CompilerResults Compiles(CodeDomProvider provider, CompilerParameters options, Stream source, bool throwOnWarning)
        {
            using (StreamReader sr = new StreamReader(source))
            {
                return Compiles(provider, options, sr.ReadToEnd(), throwOnWarning);
            }
        }

        private static void RunResults(CompilerResults results, int expectedExitCode)
        {
            System.Reflection.MethodInfo main = results.CompiledAssembly.EntryPoint;
            Assert.IsNotNull(main,
                "Could not find entry point method");

            int exitCode = (int)main.Invoke(null, new Object[] { new string[0] });
            Assert.AreEqual(expectedExitCode, exitCode);
        }

        public static void CompilesAndRun(
            CodeDomProvider provider, 
            CompilerParameters options, 
            string source, 
            int expectedExitCode
            )
        {
            options.GenerateInMemory = true;
            options.GenerateExecutable = true;
            CompilerResults results = Compiles(provider, options, source);
            RunResults(results, expectedExitCode);
        }



        public static void CompilesAndRun(
            CodeDomProvider provider,
            CompilerParameters options,
            Stream source,
            int expectedExitCode
            )
        {
            options.GenerateInMemory = true;
            options.GenerateExecutable = true;
            CompilerResults results = Compiles(provider, options, source);
            RunResults(results, expectedExitCode);
        }

        public static void CompilesAndRun(
            CodeDomProvider provider,
            string source,
            int expectedExitCode
            )
        {
            CompilerParameters options = new CompilerParameters();
            options.GenerateInMemory = true;
            options.GenerateExecutable = true;

            CompilerResults results =  Compiles(provider, options, source);
            RunResults(results,expectedExitCode);
        }

        public static string InsertLineNumbers(string source)
        {
            Regex regex = new Regex("\n");
            LineCounter counter = new LineCounter();
            return "000 "+regex.Replace(source, new MatchEvaluator(counter.AddLineCount));
        }

        private sealed class LineCounter
        {
            public int lineCount = 0;
            public string AddLineCount(Match m)
            {
                lineCount++;
                return String.Format("{1}{0:000} ", lineCount, m);
            }
        }

        /// <summary>
        /// Verifies that <paramref name="source"/> does not compile using the provided compiler.
        /// </summary>
        /// <param name="compiler">
        /// <see cref="ICodeCompiler"/> instance.</param>
        /// <param name="source">Source to compile</param>
        public static void NotCompiles(
            CodeDomProvider provider,
            string source)
        {
            CompilerParameters options = new CompilerParameters();
            options.GenerateInMemory = false;
            NotCompiles(provider, options, source);
        }

        /// <summary>
        /// Verifies that <paramref name="source"/> does not compile using the provided compiler.
        /// </summary>
        /// <param name="compiler">
        /// <see cref="ICodeCompiler"/> instance.</param>
        /// <param name="source">Source to compile</param>
        public static void NotCompiles(
            CodeDomProvider provider,
            Stream source)
        {
            CompilerParameters options = new CompilerParameters();
            options.GenerateInMemory = false;
            NotCompiles(provider, options, source);
        }

        /// <summary>
        /// Verifies that <paramref name="source"/> does not compile using the provided compiler.
        /// </summary>
        /// <param name="compiler">
        /// <see cref="ICodeCompiler"/> instance.</param>
        /// <param name="referencedAssemblies">Collection of referenced assemblies</param>
        /// <param name="source">Source to compile</param>
        public static void NotCompiles(
            CodeDomProvider provider,
            StringCollection referencedAssemblies,
            string source)
        {
            CompilerParameters options = new CompilerParameters();
            CompilerParameters ps = new CompilerParameters();
            foreach (string ra in referencedAssemblies)
                ps.ReferencedAssemblies.Add(ra);
            NotCompiles(provider, options, source);
        }

        /// <summary>
        /// Verifies that <paramref name="source"/> does not compile using the provided compiler.
        /// </summary>
        /// <param name="compiler">
        /// <see cref="ICodeCompiler"/> instance.</param>
        /// <param name="options">Compilation options</param>
        /// <param name="source">Source to compile</param>
        public static void NotCompiles(
            CodeDomProvider provider,
            CompilerParameters options,
            string source)
        {
            if (provider == null)
                throw new ArgumentNullException("provider");
            if (options == null)
                throw new ArgumentNullException("options");
            CompilerResults results = provider.CompileAssemblyFromSource(options, source);
            if (!results.Errors.HasErrors)
                throw new CompilationException(provider, options, results, source);
        }

        /// <summary>
        /// Verifies that <paramref name="source"/> does not compile using the provided compiler.
        /// </summary>
        /// <param name="compiler">
        /// <see cref="ICodeCompiler"/> instance.</param>
        /// <param name="options">Compilation options</param>
        /// <param name="source">Source to compile</param>
        public static void NotCompiles(
            CodeDomProvider provider,
            CompilerParameters options,
            Stream source)
        {
            using (StreamReader sr = new StreamReader(source))
            {
                NotCompiles(provider, options, sr.ReadToEnd());
            }
        }

        public static void DisplaySource(string source)
        {
            Console.WriteLine(InsertLineNumbers(source));
        }

        public static void DisplayErrors(CompilerResults results, TextWriter writer)
        {
            Console.WriteLine("Errors");
            foreach (CompilerError error in results.Errors)
            {
                if (error.IsWarning)
                    continue;
                writer.WriteLine(
                    "{0} ({1},{2}): {3} {4}",
                    error.FileName,
                    error.Line,
                    error.Column,
                    error.ErrorNumber,
                    error.ErrorText);
            }

            Console.WriteLine();
            Console.WriteLine("Warnings");
            foreach (CompilerError error in results.Errors)
            {
                if (!error.IsWarning)
                    continue;
                writer.WriteLine(
                    "{0} ({1},{2}): {3} {4}",
                    error.FileName,
                    error.Line,
                    error.Column,
                    error.ErrorNumber,
                    error.ErrorText);
            }
        }
    }
}
