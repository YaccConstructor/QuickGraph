module DotParserProject.ParsingFuncs

open System.Collections.Generic
open QuickGraph
open DotParserProject.DotParser
open System.IO

let directed = "digraph"
let undirected = "graph"
let directed_edge = "->"
let undirected_edge = "--"

let printAllCollectedData() =
    printfn "\nGeneral info:"
    for entry in graph_info do printf "%A " entry
    printfn "\n\nVertices lists:"
    vertices_lists |> ResizeArray.iter (printfn "%A")
    printfn "\nGeneral attributes:"
    for entry in general_attrs do printfn "%A: %A" entry.Key entry.Value
    printfn "\nVertices attributes:"
    for entry in vertices_attrs do printfn "%A: %A" entry.Key entry.Value
    printfn "\nEdges attributes:"
    for entry in edges_attrs do printfn "%A: %A" entry.Key entry.Value
    printfn "\nAssign statements:"
    assign_stmt_list |> ResizeArray.iter (printfn "%A")

let allCollectedDataToFile() =
    let f_vert = "..\\..\\..\\test_output\\collected_data_vert.txt"
    let f_edges = "..\\..\\..\\test_output\\collected_data_edges.txt"

    let outp = File.CreateText f_vert
    outp.WriteLine "General info:"
    for entry in graph_info do outp.Write("{0}", entry)
    outp.WriteLine "\n\nVertices lists:"
    vertices_lists |> ResizeArray.iter outp.WriteLine
    outp.WriteLine "\nGeneral attributes:"
    for entry in general_attrs do outp.WriteLine("{0} : {1}", entry.Key, entry.Value)
    outp.WriteLine "\nVertices attributes:"
    for entry in vertices_attrs do outp.WriteLine("{0} : {1}", entry.Key, entry.Value)
    outp.Close()

    let outp2 = File.CreateText f_edges
    outp2.WriteLine "Edges attributes:"
    for entry in edges_attrs do outp2.WriteLine("{0} : {1}", entry.Key, entry.Value)
    outp2.WriteLine "\nAssign statements:"
    assign_stmt_list |> ResizeArray.iter outp2.WriteLine  
    outp2.Close()

let rec createEdgesAndVertices (lst: string list) (graph: AdjacencyGraph<string, SEdge<string>>) =
    match lst with
    | first::second::rest ->
        graph.AddVertex(first) |>ignore
        graph.AddVertex(second) |>ignore
        graph.AddEdge(new SEdge<string>(first, second)) |> ignore
        createEdgesAndVertices (second::rest) graph
    | head::rest -> graph.AddVertex(head) |>ignore
    | [] -> ()

let rec createAdjacencyGraph(adjacency_list: ResizeArray<string list>) (graph: AdjacencyGraph<string, SEdge<string>>) =
    let createSpecificGraph lst = createEdgesAndVertices lst graph
    ResizeArray.map createSpecificGraph adjacency_list

let CheckEdgeOperator (graph_info: Dictionary<string, string>) =
    if graph_info.ContainsKey directed_edge && graph_info.ContainsKey undirected_edge then
        failwith "Graph can have only directed or undirected edge operators"
    let graph_type = graph_info.Item DotParser.type_key
    if graph_type.Equals undirected && graph_info.ContainsKey directed_edge then
        failwith "Undirected graph can't have directed edge operators"
    if graph_type.Equals directed && graph_info.ContainsKey undirected_edge then
        failwith "Directed graph can't have undirected edge operators"