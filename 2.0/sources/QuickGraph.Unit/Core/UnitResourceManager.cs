using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Imaging;
using System.Reflection;
using System.IO;
using System.Xml;
using System.Xml.Xsl;
using System.Threading;

namespace QuickGraph.Unit.Core
{
    public static class UnitResourceManager
    {
        private static volatile object syncRoot = new object();
        private static XslCompiledTransform htmlReport = null;
        private static XslCompiledTransform htmlFixtureReport = null;
        private static XslCompiledTransform htmlSummaryReport = null;
        private static XslCompiledTransform htmlHistoryReport = null;
        private static bool initialized = false;
        private static DirectoryInfo directory;

        public static Object SyncRoot
        {
            get { return syncRoot; }
        }

        public static XslCompiledTransform HtmlReport
        {
            get
            {
                lock (syncRoot)
                {
                    if (htmlReport != null)
                        return htmlReport;

                    htmlReport = new XslCompiledTransform();
                    XsltSettings settings = new XsltSettings();
                    settings.EnableScript = true;
                    htmlReport.Load(
                        Path.Combine(directory.FullName,"unittest.xslt"),
                        settings,
                        new XmlUrlResolver()
                        );
                    return htmlReport;
                }
            }
        }

        public static XslCompiledTransform HtmlFixtureReport
        {
            get
            {
                lock (syncRoot)
                {
                    if (htmlFixtureReport != null)
                        return htmlFixtureReport;

                    htmlFixtureReport = new XslCompiledTransform();
                    XsltSettings settings = new XsltSettings();
                    settings.EnableScript = true;
                    htmlFixtureReport.Load(
                            Path.Combine(directory.FullName,"fixturetest.xslt"),
                            settings,
                            new XmlUrlResolver()
                            );
                    return htmlFixtureReport;
                }
            }
        }

        public static XslCompiledTransform HtmlSummaryReport
        {
            get
            {
                lock (syncRoot)
                {
                    if (htmlSummaryReport != null)
                        return htmlSummaryReport;

                    htmlSummaryReport = new XslCompiledTransform();
                    XsltSettings settings = new XsltSettings();
                    settings.EnableScript = true;
                    htmlSummaryReport.Load(
                        Path.Combine(directory.FullName,"unittestframes.xslt"),
                        settings,
                        new XmlUrlResolver()
                        );
                    return htmlSummaryReport;
                }
            }
        }

        public static XslCompiledTransform HtmlHistoryReport
        {
            get
            {
                lock (syncRoot)
                {
                    if (htmlHistoryReport != null)
                        return htmlHistoryReport;

                    htmlHistoryReport = new XslCompiledTransform();
                    htmlHistoryReport.Load(
                        Path.Combine(directory.FullName,"unittesthistory.xslt")
                        );
                    return htmlHistoryReport;
                }
            }
        }

        public static void DumpResources()
        {
                if (!initialized)
                {
                    DumpResources(null);
                    initialized = true;
                }
        }

        public static void DumpResources(string path)
        {
            lock(syncRoot)
            {
                if (path == null)
                    path = ".";

                directory = new DirectoryInfo(path);                
                DumpResource("quickgraph.css", path);
                DumpResource("common.xslt", path);
                DumpResource("report.js", path);
                DumpResource("unittest.xslt", path);
                DumpResource("unittesthistory.xslt", path);
                DumpResource("unittestframes.xslt", path);
                DumpResource("fixturetest.xslt", path);
                foreach (UnitImages ui in Enum.GetValues(typeof(UnitImages)))
                    DumpImage(ui.ToString(), path);
                DumpImage("unit", path);
                DumpImage("unit.banner", path);
                QuickGraphResourceManager.DumpResources(path);
            }
        }

        private static void DumpResource(string resourceName, string path)
        {
            using (StreamWriter writer = new StreamWriter(Path.Combine(path, resourceName)))
            {
                string res = "QuickGraph.Unit." + resourceName;
                Stream stream = typeof(UnitResourceManager).Assembly.GetManifestResourceStream(res);
                if (stream == null)
                    throw new ApplicationException("Could not find resource " + res);
                using (StreamReader reader = new StreamReader(stream))
                {
                    writer.Write(reader.ReadToEnd());
                }
            }
        }

        private static void DumpImage(string resourceName, string path)
        {
            using (Image image = GetImage(resourceName))
            {
                string fileName = String.Format(
                    "{0}.png", Path.Combine(path, resourceName)
                    );
                image.Save(fileName, System.Drawing.Imaging.ImageFormat.Png);
            }
        }

        public static Image GetImage(string imageName)
        {
            string resource = String.Format("QuickGraph.Unit.{0}.png", imageName);
            Stream stream = typeof(UnitResourceManager).Assembly
                .GetManifestResourceStream(resource);
            if (stream == null)
                throw new ApplicationException("Could not find resource " + resource);
            Image img = Image.FromStream(stream);
            return img;
        }

        public static Image GetLogo()
        {
            return GetImage("unit");
        }

        public static Image GetLogoBanner()
        {
            return GetImage("unit.banner");
        }
    }
}
