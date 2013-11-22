module DotParserProject.ParsingFuncs

open System.Collections.Generic
open DotParserProject.DotParser

let directed = "digraph"
let undirected = "graph"
let directed_edge = "->"
let undirected_edge = "--"

let CheckEdgeOperator (graph_info: Dictionary<string, string>) =
    if graph_info.ContainsKey directed_edge && graph_info.ContainsKey undirected_edge then
        failwith "Graph can have only directed or undirected edge operators"
    let graph_type = graph_info.Item DotParser.type_key
    if graph_type.Equals undirected && graph_info.ContainsKey directed_edge then
        failwith "Undirected graph can't have directed edge operators"
    if graph_type.Equals directed && graph_info.ContainsKey undirected_edge then
        failwith "Directed graph can't have undirected edge operators"