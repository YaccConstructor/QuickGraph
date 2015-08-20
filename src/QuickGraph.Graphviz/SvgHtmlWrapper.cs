using System;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;

namespace QuickGraph.Graphviz
{
   public static class SvgHtmlWrapper
   {
       private readonly static Regex sizeRegex = new Regex("<svg width=\"(?<Width>\\d+)px\" height=\"(?<Height>\\d+)px",
             RegexOptions.ExplicitCapture 
           | RegexOptions.Multiline 
           | RegexOptions.Compiled
           );

       /// <summary>
       /// Creates a HTML file that wraps the SVG and returns the file name
       /// </summary>
       /// <param name="svgFileName"></param>
       /// <returns></returns>
       public static string WrapSvg(string svgFileName)
       {
           using (StreamReader reader = new StreamReader(svgFileName))
           {
               Size size = ParseSize(reader.ReadToEnd());
               reader.Close();
               return DumpHtml(size, svgFileName);
           }
       }

       public static Size ParseSize(string svg)
       {
           Match m = sizeRegex.Match(svg);
           if (!m.Success)
               return new Size(400, 400);
           else
           {
               int size = int.Parse(m.Groups["Width"].Value);
               int height = int.Parse(m.Groups["Height"].Value);
               return new Size(size, height);
           }
       }

       public static string DumpHtml(Size size, string svgFileName)
       {
           string outputFile = String.Format("{0}.html",svgFileName);
           using (StreamWriter html = new StreamWriter(outputFile))
           {
               html.WriteLine("<html>");
               html.WriteLine("<body>");
               html.WriteLine("<object data=\"{0}\" type=\"image/svg+xml\" width=\"{1}\" height=\"{2}\">",
                   svgFileName,size.Width,size.Height);
               html.WriteLine("  <embed src=\"{0}\" type=\"image/svg+xml\" width=\"{1}\" height=\"{2}\">",
                   svgFileName,size.Width,size.Height);
               html.WriteLine("If you see this, you need to install a SVG viewer");
               html.WriteLine("  </embed>");
               html.WriteLine("</object>");
               html.WriteLine("</body>");
               html.WriteLine("</html>");
           }

           return outputFile;
       }
   }
}
