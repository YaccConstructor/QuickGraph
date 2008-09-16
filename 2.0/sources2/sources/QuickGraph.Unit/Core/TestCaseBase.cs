using System;
using System.Collections.Generic;
using System.Reflection;
using System.IO;

namespace QuickGraph.Unit.Core
{
    public abstract class TestCaseBase : ITestCase
    {
        private string fixtureName;
        private List<TestCaseParameter> parameters = new List<TestCaseParameter>();

        public TestCaseBase(string fixtureName)
        {
            if (fixtureName == null)
                throw new ArgumentNullException("fixtureName");
            this.fixtureName = fixtureName;
        }

        public string FixtureName
        {
            get { return this.fixtureName; }
        }

        public virtual string Name
        {
            get
            {
                if (this.Parameters.Count == 0)
                {
                    return this.UndecoratedName;
                }
                else
                {
                    using (StringWriter writer = new StringWriter())
                    {
                        writer.Write(this.UndecoratedName);
                        writer.Write('(');
                        bool first = true;
                        foreach (TestCaseParameter parameter in this.Parameters)
                        {
                            if (first)
                                first = false;
                            else
                                writer.Write(", ");
                            writer.Write(parameter.Name);
                        }
                        writer.Write(')');
                        return writer.ToString();
                    }
                }
            }
        }

        public abstract string UndecoratedName
        {
            get;
        }

        public virtual string FullName
        {
            get { return String.Format("{0}.{1}", this.FixtureName, this.Name); }
        }

        public IList<TestCaseParameter> Parameters
        {
            get { return this.parameters; }
        }

        public abstract void Run(Object fixture);
    }
}
