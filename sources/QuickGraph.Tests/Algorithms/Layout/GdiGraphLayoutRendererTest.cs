using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using MbUnit.Framework;

namespace QuickGraph.Algorithms.Layout
{
    [TypeFixture(typeof(IVertexAndEdgeListGraph<string,Edge<string>>))]
    [ProviderFactory(typeof(AdjacencyGraphFactory), typeof(IVertexAndEdgeListGraph<string, Edge<string>>))]
    public class GdiGraphLayoutRendererTest
    {
        private static int imageCount;

        [TestFixtureSetUp]
        public static void FixtureSetUp()
        {
            imageCount = 0;
        }

        [Test]
        public void Render(IVertexAndEdgeListGraph<string,Edge<string>> g)
        {
            Dictionary<string, PointF> positions = new Dictionary<string, PointF>(g.VertexCount);
            RandomLayoutAlgorithm<string, Edge<string>, IVertexAndEdgeListGraph<string, Edge<string>>> layout
                 = new RandomLayoutAlgorithm<string, Edge<string>, IVertexAndEdgeListGraph<string, Edge<string>>>(g, positions);
            layout.BoundingBox = new RectangleF(0, 0, 320, 320);
            layout.Compute();

            using (Bitmap image = new Bitmap(320, 320))
            {
                using(Graphics canvas = Graphics.FromImage(image))
                {
                    canvas.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                    canvas.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    GdiGraphLayoutRenderer<
                        string,
                        Edge<string>,
                        IVertexAndEdgeListGraph<string, Edge<string>>,
                        RandomLayoutAlgorithm<string, Edge<string>, IVertexAndEdgeListGraph<string, Edge<string>>>
                        > renderer = new GdiGraphLayoutRenderer<string, Edge<string>, IVertexAndEdgeListGraph<string, Edge<string>>, RandomLayoutAlgorithm<string, Edge<string>, IVertexAndEdgeListGraph<string, Edge<string>>>>(
                            layout,
                            canvas
                            );
                    renderer.Render();
                }

                string output = string.Format("layout_{0:0000}.png", imageCount++);
                image.Save(output);
                Console.WriteLine(new Uri(output).AbsoluteUri);
            }
        }
    }
}
