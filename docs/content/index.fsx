(*** hide ***)
// This block of code is omitted in the generated HTML documentation. Use 
// it to define helpers that you do not want to show in the documentation.
#I "../../bin"

(**
QuickGraph
======================

<div class="row">
  <div class="span1"></div>
  <div class="span6">
    <div class="well well-small" id="nuget">
      The QuickGraph library can be <a href="https://nuget.org/packages/YC.QuickGraph">installed from NuGet</a>:
      <pre>PM> Install-Package YC.QuickGraph</pre>
    </div>
  </div>
  <div class="span1"></div>
</div>

Example
-------

This example demonstrates using a function defined in this sample library.

*)
#r "QuickGraph.dll"
open QuickGraph

printfn "hello = %i" <| Library.hello 0

(**
Some more info

Samples & documentation
-----------------------

The library is fork of original QuickGraph project and migration is not finished. So, please use [original documentation][origQGDoc].
 
Contributing and copyright
--------------------------

The project is hosted on [GitHub][gh] where you can [report issues][issues], fork 
the project and submit pull requests. If you're adding a new public API, please also 
consider adding [samples][content] that can be turned into a documentation. You might
also want to read the [library design notes][readme] to understand how it works.

The library is available under Ms-PL license, which allows modification and 
redistribution for both commercial and non-commercial purposes. For more information see the 
[License file][license] in the GitHub repository. 

  [content]: https://github.com/YaccConstructor/QuickGraph/tree/master/docs/content
  [gh]: https://github.com/YaccConstructor/QuickGraph
  [issues]: https://github.com/YaccConstructor/QuickGraph/issues
  [readme]: https://github.com/YaccConstructor/QuickGraph/blob/master/README.md
  [license]: https://github.com/YaccConstructor/QuickGraph/blob/master/LICENSE.txt
  [origQGDoc]: https://quickgraph.codeplex.com/documentation
*)
