using System;

namespace QuickGraph.Unit
{
    [AttributeUsage(AttributeTargets.Class,AllowMultiple =false, Inherited =false)]
    public sealed class CurrentFixtureAttribute : Attribute
    {}
}
