using System;
using QuickGraph.Unit.Exceptions;

namespace QuickGraph.Unit
{
    public static class Assume
    {
        public static void IsTrue(bool value)
        {
            if (!value)
                throw new AssumptionFailureException();
        }

        public static void IsFalse(bool value)
        {
            if (value)
                throw new AssumptionFailureException();
        }
    }
}
