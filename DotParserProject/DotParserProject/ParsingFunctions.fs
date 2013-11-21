module ParsingFuncs
open System.Collections.Generic

let AddNode (adj_list: Dictionary<string, HashSet<string>>) (node_name: string) = 
    let contains_node = adj_list.ContainsKey node_name
    if not contains_node then
        adj_list.Add(node_name, new HashSet<string>()) |> ignore

let rec CreateEdges (adj_list: Dictionary<string, HashSet<string>>) nodes =
    match nodes with
    | first::second::rest ->
        adj_list.[first].Add second |> ignore
        CreateEdges adj_list (second::rest)
    | _ -> ()

let AddEdges (adj_list: Dictionary<string, HashSet<string>>) nodes =
    printfn "function AddEdges is called"
    printfn "Edges: "
    for n in nodes do printf "%A " n
    printfn ""
    for n in nodes do AddNode adj_list n
    CreateEdges adj_list nodes
