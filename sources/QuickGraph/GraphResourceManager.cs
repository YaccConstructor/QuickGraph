using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Imaging;
using System.Reflection;
using System.IO;

namespace QuickGraph
{
    public static class GraphResourceManager
    {
        public static Image GetImage(string imageName)
        {
            string resource = String.Format("QuickGraph.{0}.png", imageName);
            Stream stream = typeof(GraphResourceManager).Assembly
                .GetManifestResourceStream(resource);
            if (stream == null)
                throw new ApplicationException("Could not find resource " + resource);
            Image img = Image.FromStream(stream);
            return img;
        }

        public static Image GetLogo()
        {
            return GetImage("QuickGraph");
        }

        public static Image GetLogoBanner()
        {
            return GetImage("QuickGraph.banner");
        }
    }
}
