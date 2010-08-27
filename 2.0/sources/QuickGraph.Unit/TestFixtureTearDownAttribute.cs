using System;

namespace QuickGraph.Unit
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class TestFixtureTearDownAttribute : Attribute { }
}
