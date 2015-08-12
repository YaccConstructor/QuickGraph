using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuickGraph
{
    public static class TestConsole
    {
        public static bool Logging { get; set; }
        public static void WriteLine()
        {
            if (Logging)
                Console.WriteLine();
        }
        public static void WriteLine(object o)
        {
            if (Logging)
                Console.WriteLine(o);
        }
        public static void WriteLine(string text)
        {
            if (Logging)
                Console.WriteLine(text);
        }
        public static void WriteLine(string text, object arg)
        {
            if (Logging)
                Console.WriteLine(text, arg);
        }
        public static void WriteLine(string text, object arg1, object arg2)
        {
            if (Logging)
                Console.WriteLine(text, arg1, arg2);
        }
        public static void WriteLine(string text, params object[] args)
        {
            if (Logging)
                Console.WriteLine(text, args);
        }
        public static void Write(object o)
        {
            if (Logging)
                Console.Write(o);
        }
        public static void Write(string text)
        {
            if (Logging)
                Console.Write(text);
        }
        public static void Write(string text, object arg)
        {
            if (Logging)
                Console.Write(text, arg);
        }
        public static void Write(string text, params object[] args)
        {
            if (Logging)
                Console.Write(text, args);
        }
    }
}
