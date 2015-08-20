using System;
using System.Collections.Generic;
using System.Text;

namespace QuickGraph.Unit.Exceptions
{
    [Serializable]
    public sealed class FixtureReflectionFailedException : UnitException
    {
        public FixtureReflectionFailedException() { }
        public FixtureReflectionFailedException(string message) : base(message) { }
        public FixtureReflectionFailedException(string message, Exception inner) : base(message, inner) { }
    }
}
