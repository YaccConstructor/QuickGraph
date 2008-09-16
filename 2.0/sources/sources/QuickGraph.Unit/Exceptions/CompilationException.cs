using System;
using System.Runtime.Serialization;
using System.IO;
using System.CodeDom.Compiler;

namespace QuickGraph.Unit.Exceptions
{
    [Serializable]
    public class CompilationException : UnitException
    {
        private string body;
        public CompilationException(
            CodeDomProvider provider,
            CompilerParameters parameters,
            CompilerResults results,
            params String[] sources
            )
        {
            StringWriter sw = new StringWriter();
            sw.WriteLine("Compilation:  {0} errors", results.Errors.Count);
            sw.WriteLine("Compiler: {0}", provider.FileExtension);
            sw.WriteLine("CompilerParameters: {0}", parameters.ToString());
            foreach (CompilerError error in results.Errors)
            {
                sw.WriteLine(error.ToString());
            }
            sw.WriteLine("Sources:");
            foreach (string source in sources)
                sw.WriteLine(source);

            this.body = sw.ToString();
        }

        public override string Message
        {
            get
            {
                return "Compilation Error";
            }
        }

        public override string ToString()
        {
            return String.Format("{0}\n{1}",
                this.body,
                base.ToString()
            );
        }
    }
}
