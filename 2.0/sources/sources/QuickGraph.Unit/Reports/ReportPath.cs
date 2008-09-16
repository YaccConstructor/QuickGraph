using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace QuickGraph.Unit.Reports
{
    public static class ReportPath
    {
        public static string GetName(string testAssemblyName, DateTime time)
        {
            return String.Format("{0}_{1}",
                EscapeAssemblyName(testAssemblyName),
                time.ToString("HHmmss")
                );
        }

        internal static Regex GetRegex(string testAssemblyName)
        {
            string etan = EscapeAssemblyName(testAssemblyName).ToLower();
            return new Regex(etan + @"_\d{5}_\d{6}");
        }

        public static string EscapeFileName(string fileName)
        {
            return fileName
                .Replace(' ', '_')
                .Replace('/', '_')
                .Replace(':', '_')
                .Replace('(', '_')
                .Replace(')', '_')
                .Replace('[', '_')
                .Replace(']', '_')
                .Replace('{', '_')
                .Replace('}', '_')
                ;
        }

        public static string EscapeAssemblyName(string assemblyName)
        {
            return EscapeFileName(assemblyName).Replace('.', '_');
        }
    }
}
