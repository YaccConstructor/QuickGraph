using System;
using System.IO;

namespace QuickGraph.Unit.Logging
{
    public sealed class ConsoleLoggerListener : ILoggerListener
    {
        public void Log(LogLevel level, string message)
        {
            Console.WriteLine(
                "[{0}] {1}",
                level,
                message
                );
        }
    }
}
