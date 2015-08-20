using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Imaging;
using System.Reflection;
using System.IO;

namespace QuickGraph.Operations
{
    public static class OperationsResourceManager
    {
        public static Image GetImage(string imageName)
        {
            string resource = String.Format("QuickGraph.Unit.Operations.{0}.png", imageName);
            Stream stream = typeof(OperationsResourceManager).Assembly
                .GetManifestResourceStream(resource);
            if (stream == null)
                throw new Exception("Could not find resource " + resource);
            Image img = Image.FromStream(stream);
            return img;
        }

        public static Image GetLogo()
        {
            return GetImage("operations");
        }

        public static Image GetLogoBanner()
        {
            return GetImage("operations.banner");
        }

        public static void DumpResources(string path)
        {
            if (path == null)
                path = ".";

            using (Image logo = GetLogo())
            {
                logo.Save(Path.Combine(path, "operations.png"),
                    System.Drawing.Imaging.ImageFormat.Png);
            }

            using (Image logo = GetLogoBanner())
            {
                logo.Save(Path.Combine(path, "operations.banner.png"),
                    System.Drawing.Imaging.ImageFormat.Png);
            }
        }
    }
}
