using System;

namespace QuickGraph.Unit
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple=false, Inherited=false)]
    public sealed class AssemblySetUpAttribute  : Attribute
    {}
}
