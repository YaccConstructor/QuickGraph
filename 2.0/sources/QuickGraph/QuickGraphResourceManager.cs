using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;

namespace QuickGraph
{
    public static class QuickGraphResourceManager
    {
        public static Image GetLogo()
        {
            return GetImage("quickgraph");
        }

        public static Image GetBanner()
        {
            return GetImage("quickgraph.banner");
        }

        private static Image GetImage(string name)
        {
            using (Stream stream = typeof(QuickGraphResourceManager).Assembly.GetManifestResourceStream(String.Format("QuickGraph.{0}.png", name)))
                return Image.FromStream(stream);
        }

        public static void DumpResources(string path)
        {
            if (String.IsNullOrEmpty(path))
                throw new ArgumentNullException("path");
            GetLogo().Save(Path.Combine(path, "quickgraph.png"), System.Drawing.Imaging.ImageFormat.Png);
            GetBanner().Save(Path.Combine(path, "quickgraph.banner.png"), System.Drawing.Imaging.ImageFormat.Png);
        }
    }
}
