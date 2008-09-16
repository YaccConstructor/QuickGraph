using System;
using System.Collections.Generic;

namespace QuickGraph.Unit
{
    public interface ILoggerService
    {
        IList<ILoggerListener> Listeners { get;}

        void Log(
            LogLevel level,
            string message);
        void Log(
            LogLevel level,
            string format,
            params object[] args
            );
        void LogMessage(
            string message
            );
        void LogMessage(
            string format,
            params object[] args
            );
        void LogWarning(
            string message
            );
        void LogWarning(
            string format,
            params object[] args
            );
        void LogError(Exception ex);
        void LogError(
            string message
            );
        void LogError(
            string format,
            params object[] args
            );
    }
}
