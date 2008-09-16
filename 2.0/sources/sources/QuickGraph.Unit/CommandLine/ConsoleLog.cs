using System;

namespace QuickGraph.CommandLine
{
    public static class ConsoleLog
    {
        private static ConsoleColor warningColor = ConsoleColor.Yellow;
        private static ConsoleColor errorColor = ConsoleColor.Red;
        private static ConsoleColor commentColor = ConsoleColor.DarkGray;

        public static ConsoleColor WarningColor
        {
            get { return warningColor; }
            set { warningColor = value; }
        }

        public static ConsoleColor ErrorColor
        {
            get { return errorColor; }
            set { errorColor = value; }
        }

        public static ConsoleColor CommentColor
        {
            get { return commentColor; }
            set { commentColor = value; }
        }

        public static void Message(string message)
        {
            Console.WriteLine(message);
        }

        public static void Message(string format, params object[] args)
        {
            Console.WriteLine(format, args);
        }

        public static void Warning(string message)
        {
            Log(WarningColor, message);
        }

        public static void Warning(string format, params object[] args)
        {
            Log(WarningColor, format, args);
        }

        public static void Error(string message)
        {
            Log(ErrorColor, message);
        }

        public static void Error(string format, params object[] args)
        {
            Log(ErrorColor, format, args);
        }

        public static void Comment(string message)
        {
            Log(CommentColor, message);
        }

        public static void Comment(string format, params object[] args)
        {
            Log(CommentColor, format, args);
        }

        public static void Log(ConsoleColor color, string message)
        {
            ConsoleColor currentColor = Console.ForegroundColor;
            try
            {
                Console.ForegroundColor = color;
                Console.WriteLine(message);
            }
            finally
            {
                Console.ForegroundColor = currentColor;
            }
        }

        public static void Log(ConsoleColor color, string format, params object[] args)
        {
            ConsoleColor currentColor = Console.ForegroundColor;
            try
            {
                Console.ForegroundColor = color;
                Console.WriteLine(format, args);
            }
            finally
            {
                Console.ForegroundColor = currentColor;
            }
        }
    }
}
