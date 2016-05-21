using System;
using System.IO;
using System.Windows;
using GraphX.Controls;

namespace QuickGraph.GraphXAdapter
{
    public class GraphXZoomControl : ZoomControl
    {
        public GraphXZoomControl()
        {
            var templatePath = Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + "GraphXDefaultTemplate.xaml";
            var resourceDictionary = new ResourceDictionary { Source = new Uri(templatePath) };
            Resources.MergedDictionaries.Add(resourceDictionary);
        }
    }
}
