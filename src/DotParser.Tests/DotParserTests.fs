module DotParser.Tests.Tests

open DotParser
open DotParserProject.GraphData
open FsUnit
open NUnit.Framework
open System.Collections
open System.Linq


let isDirected (g : GraphData) = g.IsDirected
let isStrict   (g : GraphData) = g.IsStrict
let isEmpty    (g : GraphData) = g.Nodes.IsEmpty
let nodesCount (g : GraphData) = g.Nodes.Count
let edgesCount (g : GraphData) = Map.fold (fun acc _ x -> acc + List.length x) 0 g.Edges

let shouldContainNodes names (g : GraphData) =
    List.forall g.Nodes.ContainsKey names |> should be True

let map = Map.ofList
let set = Set.ofList


[<Test>]
let ``Empty undirected graph`` () = DotParser.parse "graph { }" |> isDirected |> should be False


[<Test>]
let ``Empty directed graph`` () = DotParser.parse "digraph { }" |> isDirected |> should be True


[<Test>]
let ``Named graph`` () = DotParser.parse "graph t { }" |> isEmpty |> should be True


[<Test>]
let ``Single node`` () =
    let graph = DotParser.parse "graph { a }"

    nodesCount graph |> should equal 1
    edgesCount graph |> should equal 0

    graph |> shouldContainNodes ["a"]


[<Test>]
let ``Multiple nodes`` () =
    let graph = DotParser.parse "graph { a b; c \n d \"--\" }"

    nodesCount graph |> should equal 5
    edgesCount graph |> should equal 0

    graph |> shouldContainNodes ["a"; "b"; "c"; "d"; "--"]


[<Test>]
let ``Numeral node labels`` () =
    let graph = DotParser.parse "graph { 1 2 }"

    nodesCount graph |> should equal 2
    edgesCount graph |> should equal 0

    graph |> shouldContainNodes ["1"; "2"]


[<Test>]
let ``Single edge`` () =
    let graph = DotParser.parse "graph { a -- b } "

    nodesCount graph |> should equal 2
    edgesCount graph |> should equal 1

    graph |> shouldContainNodes ["a"; "b"]


[<Test>]
let ``Multiple edges`` () =
    let graph = DotParser.parse "graph { a -- b c -- d }"

    nodesCount graph |> should equal 4
    edgesCount graph |> should equal 2



[<Test>]
let ``Multiple edges in row`` () =
    let graph = DotParser.parse "graph { a -- b c -- d -- e }"

    nodesCount graph |> should equal 5
    edgesCount graph |> should equal 3


[<Test>]
let ``Multi-egde`` () =
    let graph = DotParser.parse "graph { a -- b a -- b }"

    nodesCount graph |> should equal 2
    edgesCount graph |> should equal 2


[<Test>]
let ``Strict graph`` () =
    let graph = DotParser.parse "strict graph { a -- b a -- b }"

    isStrict graph   |> should be True
    nodesCount graph |> should equal 2
    edgesCount graph |> should equal 1


[<Test>]
let ``Keyword labels`` () =
    DotParser.parse "graph { \"graph\" \"digraph\" \"strict\" \"node\" \"edge\" \"subgraph\" }"
    |> shouldContainNodes ["graph"; "digraph"; "strict"; "node"; "edge"; "subgraph"; ]


[<Test>]
let ``Wrong edge in directed`` () =
    (fun () -> DotParser.parse "graph { a -> b }" |> ignore)
    |> should throw typeof<System.Exception>


[<Test>]
let ``Wrong edge in undirected`` () =
    (fun () -> DotParser.parse "digraph { a -- b }" |> ignore)
    |> should throw typeof<System.Exception>


[<Test>]
let ``Subgraph statement`` () =
    let graph = DotParser.parse "graph { a { b c -- d } }"

    graph |> nodesCount |> should equal 4
    graph |> edgesCount |> should equal 1


[<Test>]
let ``Subgraph on left of edge`` () =
    let graph = DotParser.parse "graph { { a b } -- c }"

    nodesCount graph |> should equal 3
    edgesCount graph |> should equal 2

    graph |> shouldContainNodes ["a"; "b"; "c"]


[<Test>]
let ``Subgraph on right of edge`` () =
    let graph = DotParser.parse "graph { a -- { b c } }"

    nodesCount graph |> should equal 3
    edgesCount graph |> should equal 2

    graph |> shouldContainNodes ["a"; "b"; "c"]


[<Test>]
let ``Subgraph on both sides of edge`` () =
    let graph = DotParser.parse "graph { { a b } -- { c d } }"

    nodesCount graph |> should equal 4
    edgesCount graph |> should equal 4

    graph |> shouldContainNodes ["a"; "b"; "c"; "d"]


[<Test>]
let ``Nested subgraphs`` () =
    let graph = DotParser.parse "graph { a -- { b -- { c d } } }"

    nodesCount graph |> should equal 4
    edgesCount graph |> should equal 5

    graph |> shouldContainNodes ["a"; "b"; "c"; "d"]


[<Test>]
let ``Empty attributes`` () =
    let graph = DotParser.parse "graph { edge [ ] graph [ ] node [] }"

    graph.GraphAttributes.IsEmpty |> should be True
    graph.NodeAttributes.IsEmpty  |> should be True
    graph.EdgeAttributes.IsEmpty  |> should be True


[<Test>]
let ``Node attibute`` () =
    let graph = DotParser.parse "graph { a node [ color = red ] b }"
    graph.Nodes.["a"] |> should equal Map.empty
    graph.Nodes.["b"] |> should equal (map ["color", "red"])


[<Test>]
let ``Edge attibutes`` () =
    let graph =
        DotParser.parse "graph { a -- b b -- c edge [ color = red, foo=bar; \"a\" = b ] b--c }"

    graph.Edges.["a", "b"] |> should equal [Map.empty]
    graph.Edges.["b", "c"]
    |> set |> should equal (set [Map.empty; map ["color", "red"; "foo", "bar"; "a", "b"]])


[<Test>]
let ``Strict graph attributes`` () =
    let graph = DotParser.parse "strict graph { a -- b a -- b edge [color=red] b -- a }"

    isStrict graph   |> should be True
    nodesCount graph |> should equal 2
    edgesCount graph |> should equal 1

    graph.Edges.["a", "b"] |> should equal [map ["color", "red"]]

[<Test>]
let ``Edge statement attributes`` () =
    let graph = DotParser.parse "graph { a -- b [color=red] }"

    graph.Edges.["a", "b"] |> should equal [map ["color", "red"]]

[<Test>]
let ``Node statement attributes`` () =
    let graph = DotParser.parse "grah { a [color=red] b c [color=blue] }"

    nodesCount graph |> should equal 3

    graph.Nodes.["a"] |> should equal [map ["color", "red"]]
    graph.Nodes.["b"] |> should equal [Map.empty]
    graph.Nodes.["c"] |> should equal [map ["color", "blue"]]