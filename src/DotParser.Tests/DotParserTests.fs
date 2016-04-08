module DotParser.Tests.Tests

open FsUnit
open NUnit.Framework
open DotParser

let isDirected (g: QuickGraph.IMutableVertexAndEdgeSet<_,_>) = g.IsDirected
let isStrict (g: QuickGraph.IMutableVertexAndEdgeSet<_,_>) = not g.AllowParallelEdges
let isEmpty (g: QuickGraph.IMutableVertexAndEdgeSet<_,_>) = g.IsVerticesEmpty


[<Test>]
let ``Empty undirected graph`` () = DotParser.parse "graph { }" |> isDirected |> should be False


[<Test>]
let ``Empty directed graph`` () = DotParser.parse "digraph { }" |> isDirected |> should be True


[<Test>]
let ``Named graph`` () = DotParser.parse "graph t { }" |> isEmpty |> should be True


[<Test>]
let ``Single node`` () =
    let g = DotParser.parse "graph { a }"

    g.VertexCount |> should equal 1
    g.EdgeCount |> should equal 0


[<Test>]
let ``Multiple nodes`` () =
    let g = DotParser.parse "graph { a b; c }"

    g.VertexCount |> should equal 3
    g.EdgeCount |> should equal 0

[<Test>]
let ``Numeral node labels`` () =
    let graph = DotParser.parse "graph { 1 2 }"

    shouldFail (fun _ -> printf "implement me")

    graph.VertexCount |> should equal 2
    graph.EdgeCount |> should equal 0
    // todo: check vertices names


[<Test>]
let ``Single edge`` () =
    let graph = DotParser.parse "graph"
    
    graph.VertexCount |> should equal 2
    graph.EdgeCount |> should equal 1
    

[<Test>]
let ``Multiple edges`` () =
    let graph = DotParser.parse "graph { a -- b c -- d -- e }"

    graph.VertexCount |> should equal 5
    graph.EdgeCount |> should equal 3


[<Test>]
let ``Multi-egde`` () =
    let graph = DotParser.parse "graph { a -- b a -- b }"
    
    graph.VertexCount |> should equal 2
    graph.EdgeCount |> should equal 2


[<Test>]
let ``Strict graph`` () =
    let graph = DotParser.parse "strict graph { a -- b a -- b }"

    graph.VertexCount |> should equal 2
    graph.EdgeCount |> should equal 1


[<Test>]
let ``Keyword labels`` () =
    let graph = DotParser.parse "graph { \"graph\" -- \"node\" }"
    // todo: check names 

    graph.VertexCount |> should equal 2
    graph.EdgeCount |> should equal 1
