namespace System
open System.Reflection

[<assembly: AssemblyTitleAttribute("QuickGraphFS")>]
[<assembly: AssemblyProductAttribute("YC.QuickGraph")>]
[<assembly: AssemblyDescriptionAttribute("Graph datastructures and algorithms for .NET.")>]
[<assembly: AssemblyVersionAttribute("3.7.1")>]
[<assembly: AssemblyFileVersionAttribute("3.7.1")>]
do ()

module internal AssemblyVersionInformation =
    let [<Literal>] Version = "3.7.1"
    let [<Literal>] InformationalVersion = "3.7.1"
