module DotParserProject.CollectDataFuncs
open System.Collections.Generic

//let AddNode (adj_list: Dictionary<string, Dictionary<string, int>>) (node_name: string) = 
//    let contains_node = adj_list.ContainsKey node_name
//    if not contains_node then
//        adj_list.Add(node_name, new Dictionary<string, int>())
//
//let rec CreateEdges (adj_list: Dictionary<string, Dictionary<string, int>>) nodes =
//    match nodes with
//    | first::second::rest ->
//        let contains_node = adj_list.[first].ContainsKey second
//        if not contains_node then
//            adj_list.[first].Add (second, 1)
//        else
//            adj_list.[first].[second] <- adj_list.[first].[second] + 1
//        CreateEdges adj_list (second::rest)
//    | _ -> ()
//
//let AddEdges (adj_list: Dictionary<string, Dictionary<string, int>>) nodes =
////    Utils.Log ["Edges: "; nodes.ToString()]
//    for n in nodes do AddNode adj_list n
//    CreateEdges adj_list nodes

let addInfo(graph_info: Dictionary<string,string>) (info_list: list<string*string>) =
    let new_info = info_list |> List.filter (fun (key,value) -> not (graph_info.ContainsKey key))
    new_info |> List.map graph_info.Add |> ignore

let addAttribute key value (attrs:Dictionary<string, (string*string) list>) =
    let contains_node = attrs.ContainsKey key
    if not contains_node then 
        attrs.Add (key, value)
    else
        attrs.[key] <- List.concat [attrs.[key]; value]

let stmtGetData (elems_list: list<string>) 
                (attr_list: list<string*string>) 
                (vertices: ResizeArray<string list>) 
                (attrs:Dictionary<string, (string*string) list>) = 
    if not elems_list.IsEmpty then 
        vertices.Add elems_list
    if not attr_list.IsEmpty then
        addAttribute (elems_list.ToString()) attr_list attrs