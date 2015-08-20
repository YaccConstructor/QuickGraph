using System;
using System.Collections.Generic;

namespace QuickGraph.Unit
{
    public static class TestConsole
    {
        private static string reportDirectoryName = "";

        public static string ReportDirectoryName
        {
            get { return reportDirectoryName; }
        }

        internal static void SetReportDirectoryName(string directoryName)
        {
            reportDirectoryName = directoryName;
        }

        public static void WriteImage(string url)
        {
            Console.WriteLine("[img src=\"{0}\" /]", url);
        }

        public static void WriteUri(string uriString)
        {
            WriteUri(new Uri(System.IO.Path.GetFullPath(uriString)));
        }

        public static void WriteUri(Uri uri)
        {
            Console.WriteLine(uri.AbsoluteUri);
        }

        public static void WriteBold(string message)
        {
            Console.Write("**{0}**", message);
        }

        public static void WriteBold(string format, params object[] args)
        {
            WriteBold(string.Format(format, args));
        }

        public static void WriteLineBold(string message)
        {
            WriteBold(message);
            Console.WriteLine();
        }

        public static void WriteLineBold(string format, params object[] args)
        {
            WriteLineBold(string.Format(format, args));
        }

    }
}
