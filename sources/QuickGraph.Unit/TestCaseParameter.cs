using System;
using System.Collections.Generic;

namespace QuickGraph.Unit
{
    public sealed class TestCaseParameter
    {
        private string name;
        private object value;

        public TestCaseParameter(string name, object value)
        {
            this.name = name;
            this.value = value;
        }

        public TestCaseParameter(object value)
        {
            this.name = String.Format("{0}", value);
            this.value = value;
        }

        public string Name
        {
            get { return this.name; }
        }

        public object Value
        {
            get { return this.value; }
        }

        public override string ToString()
        {
            return this.Name;
        }

        public static object[] GetValues(ICollection<TestCaseParameter> parameters)
        {
            object[] objs = new object[parameters.Count];
            int i =0;
            foreach(TestCaseParameter parameter in parameters)
                objs[i++] = parameter.Value;
            return objs;
        }
    }
}
