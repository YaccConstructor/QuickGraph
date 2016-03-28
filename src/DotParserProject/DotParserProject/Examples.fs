module Examples

open System
open System.IO
open DotLangParser
(*open QuickGraph

let example0() =
    let input = "..\\..\\..\\test_inputs\\test1.dot"
    let graphs = DotLangParser.parse input

    for gr in graphs do
        gr.PrintAllCollectedData()
        printfn "---------------------------------------------------------"
*)
let example1() =
    let input = "..\\..\\..\\test_inputs\\test1.dot"
    let graphs = DotLangParser.parse input
    let main_graph = graphs.[0]

    match main_graph.IsStrict() with
    | true -> printfn "Input graph is strict"
    | false -> printfn "Input graph is not strict"
    
    printfn "Graph's type: %A" (main_graph.Type())
    printfn "Graph's name: %A" (main_graph.GraphName())
    printfn "Graph attributes: %A" (main_graph.GraphAttributes())
    printfn "Node attributes: %A" (main_graph.NodeAttributes())
    printfn "Edge attributes: %A" (main_graph.EdgeAttributes())
    printfn "Assignment statements: %A" (main_graph.AssignStatements())
    printfn "Vertices: %A" (main_graph.GetVerticeArray())

    (*
let example2() =
    let input = "..\\..\\..\\test_inputs\\test1.dot"
    let graphs = DotLangParser.parse input
    let main_graph = graphs.[0]

    let simpleHandler v1 v2 =
        new SUndirectedEdge<string>(v1,v2)

    let edges_array = main_graph.GetEdgeArray simpleHandler
    let my_graph = edges_array.ToAdjacencyGraph<string, SUndirectedEdge<string>>()
    for v in my_graph.Vertices do
        printf "%A " v
    printfn ""
    for e in my_graph.Edges do
        printfn "%A" e
    printfn ""

let example3() =
    let input = "..\\..\\..\\test_inputs\\test1.dot"
    let graphs = DotLangParser.parse input
    let main_graph = graphs.[0]

    let intHandler v1 v2 =
        let iv1 = Int32.Parse(v1)
        let iv2 = Int32.Parse(v2)
        new SUndirectedEdge<int>(iv1,iv2)

    let edges_array = main_graph.GetEdgeArray intHandler
    let my_graph = edges_array.ToAdjacencyGraph<int, SUndirectedEdge<int>>()
    for v in my_graph.Vertices do
        printf "%A " v
    printfn ""
    for e in my_graph.Edges do
        printfn "%A" e
    printfn ""

let example4() =
    let input = "..\\..\\..\\test_inputs\\test1.dot"
    let graphs = DotLangParser.parse input
    let main_graph = graphs.[0]

    let taggedHandler v1 v2 (tags: list<string*string>) =
        match tags.Length with
        | 0 ->
            new STaggedEdge<string, string>(v1, v2, "_no_tag_")
        | _ ->
            let _, tag = tags.[0]
            new STaggedEdge<string, string>(v1, v2, tag)

    let edges_array = main_graph.GetTaggedEdgeArray taggedHandler
    let my_graph = edges_array.ToAdjacencyGraph<string, STaggedEdge<string, string>>()
    for v in my_graph.Vertices do
        printf "%A " v
    printfn ""
    for e in my_graph.Edges do
        printfn "%A" e
    printfn ""
    *)


let example5() =
    let input = "strict graph test1_graph {
     graph [color = red]
     node [col = b, tail = bp];
     edge [ size = 100500 ] 
     style=fashionable
     6 [lable = \"v1\"]
     1 -- 1
     1
     1 -- 2 -- 3 [weight = 10]
     3
     4 -- 3 [weight = 5]
     2 -- 4 [weight = 1]
     1 -- 2
     1 -- 2
     1 -- 1
     1 -- 1
     3
     1 -- 3
     }"
    let graphs = DotLangParser.parsestring input
    let main_graph = graphs.[0]

    match main_graph.IsStrict() with
    | true -> printfn "Input graph is strict"
    | false -> printfn "Input graph is not strict"
    
    printfn "Graph's type: %A" (main_graph.Type())
    printfn "Graph's name: %A" (main_graph.GraphName())
    printfn "Graph attributes: %A" (main_graph.GraphAttributes())
    printfn "Node attributes: %A" (main_graph.NodeAttributes())
    printfn "Edge attributes: %A" (main_graph.EdgeAttributes())
    printfn "Assignment statements: %A" (main_graph.AssignStatements())
    printfn "Vertices: %A" (main_graph.GetVerticeArray())
     
       

//example0()
//example1()
//example2()
//example3()
//example4()
//example5()

let pr s = 
   let lst = VertWithAttrForStr s
   //printfn "%A" <| lst.Item "[6]"
   for i in lst do
      printfn "%A" i
  
  
pr "strict graph test1_graph {
     graph [color = red]
     node [col = b, tail = bp];
     edge [ size = 100500 ] 
     style=fashionable
     6 [lable = \"v1\"]
     1 -- 1 [weight = 3]
     1 
     1 -- 2 -- 3 [weight = 10]
     3
     4 -- 3 [weight = 5]
     2 -- 4 [weight = 1]
     1 -- 2
     2 -- 1
     1 -- 1 
     1 -- 1
     3
     1 [weight = 6]
     1 -- 3
     }"
  

//let key = Console.ReadKey(true)
