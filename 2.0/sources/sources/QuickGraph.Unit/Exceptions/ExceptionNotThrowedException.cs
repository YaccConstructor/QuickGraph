using System;

namespace QuickGraph.Unit.Exceptions
{
    [Serializable]
    public sealed class ExceptionNotThrowedException : UnitException
    {
        public ExceptionNotThrowedException(Type expectedException)
            : base("Expected " + expectedException.FullName)
        { }
    }
}
