namespace QuickGraph.Graphviz.Dot
{
    using System;
    using System.ComponentModel;

    public enum GraphvizImageType
    {
        [Description("Client-side imagemaps")]
        Cmap = 6,
        [Description("Figure format")]
        Fig = 0,
        [Description("Gd format")]
        Gd = 1,
        [Description("Gd2 format")]
        Gd2 = 2,
        [Description("GIF format")]
        Gif = 3,
        [Description("HP-GL/2 format")]
        Hpgl = 4,
        [Description("Server-side imagemaps")]
        Imap = 5,
        [Description("JPEG format")]
        Jpeg = 7,
        [Description("FrameMaker MIF format")]
        Mif = 8,
        [Description("MetaPost")]
        Mp = 9,
        [Description("PCL format")]
        Pcl = 10,
        [Description("PIC format")]
        Pic = 11,
        [Description("plain text format")]
        PlainText = 12,
        [Description("Portable Network Graphics format")]
        Png = 13,
        [Description("Postscript")]
        Ps = 14,
        [Description("PostScript for PDF")]
        Ps2 = 15,
        [Description("Scalable Vector Graphics")]
        Svg = 0x10,
        [Description("Scalable Vector Graphics, gzipped")]
        Svgz = 0x11,
        [Description("VRML")]
        Vrml = 0x12,
        [Description("Visual Thought format")]
        Vtx = 0x13,
        [Description("Wireless BitMap format")]
        Wbmp = 20
    }
}

