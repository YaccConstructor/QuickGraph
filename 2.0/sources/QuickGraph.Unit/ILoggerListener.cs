using System;

namespace QuickGraph.Unit
{
    public interface ILoggerListener
    {
        void Log(
            LogLevel level,
            string message
            );
    }
}
