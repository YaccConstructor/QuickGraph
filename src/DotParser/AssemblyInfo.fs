namespace System
open System.Reflection

[<assembly: AssemblyTitleAttribute("DotParser")>]
[<assembly: AssemblyProductAttribute("QuickGraph")>]
[<assembly: AssemblyDescriptionAttribute("Graph datastructures and algorithms for .NET.")>]
[<assembly: AssemblyVersionAttribute("0.0.1")>]
[<assembly: AssemblyFileVersionAttribute("0.0.1")>]
do ()

module internal AssemblyVersionInformation =
    let [<Literal>] Version = "0.0.1"
