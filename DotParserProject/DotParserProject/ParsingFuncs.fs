module DotParserProject.ParsingFuncs

open System.Collections.Generic
open QuickGraph
open DotParserProject.DotParser
open System.IO

let directed = "digraph"
let undirected = "graph"
let directed_edge = "->"
let undirected_edge = "--"

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