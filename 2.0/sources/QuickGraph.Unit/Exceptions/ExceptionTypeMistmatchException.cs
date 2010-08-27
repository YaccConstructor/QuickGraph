using System;

namespace QuickGraph.Unit.Exceptions
{
    [Serializable]
    public sealed class ExceptionTypeMistmatchException : UnitException
    {
        public ExceptionTypeMistmatchException(Type expectedExceptionType, Exception actualException)
            : base(
                String.Format("Expected {0}, got {1}: {2}",
                    expectedExceptionType.Name, 
                    actualException.GetType().Name,
                    actualException.Message
                ),
                actualException)
        { }
    }
}
