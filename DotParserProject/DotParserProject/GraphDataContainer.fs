module DotParserProject.GraphDataContainer

open System.Collections.Generic

type GraphDataContainer = class
    val private vertices_lists : ResizeArray<string list>
    val private graph_info : Dictionary<string, string>
    val private vertices_attrs :  Dictionary<string, (string*string) list>
    val private edges_attrs :  Dictionary<string, (string*string) list>
    val private general_attrs : Dictionary<string, (string*string) list>
    val private assign_stmt_list : ResizeArray<string*string>

    new () = {
        vertices_lists = new ResizeArray<string list>()
        graph_info = new Dictionary<string, string>()
        vertices_attrs = new Dictionary<string, (string*string) list>()
        edges_attrs = new Dictionary<string, (string*string) list>()
        general_attrs = new Dictionary<string, (string*string) list>()
        assign_stmt_list = new ResizeArray<string*string>()
        }

    member x.AddGeneralInfo (info_list: list<string*string>) =
        let new_info = info_list |> List.filter (fun (key,value) -> not (x.graph_info.ContainsKey key))
        new_info |> List.map x.graph_info.Add |> ignore

    member x.AddGeneralAttrs key value =
        x.AddAttrToDict key value x.general_attrs

    member x.AddEdgeStmtData (elems_list: list<string>) (attr_list: list<string*string>) = 
        if not elems_list.IsEmpty then 
            x.vertices_lists.Add elems_list
        if not attr_list.IsEmpty then
            x.AddAttrToDict (elems_list.ToString()) attr_list x.edges_attrs
            
    member x.AddNodeStmtData (elems_list: list<string>) (attr_list: list<string*string>) = 
        if not elems_list.IsEmpty then 
            x.vertices_lists.Add elems_list
        if not attr_list.IsEmpty then
            x.AddAttrToDict (elems_list.ToString()) attr_list x.vertices_attrs
    
    member x.AddAssignStmt key value =
        x.assign_stmt_list.Add (key, value) |> ignore 

    member private x.AddAttrToDict key value (attrs_list:Dictionary<string, (string*string) list>) =
        let contains_node = attrs_list.ContainsKey key
        if not contains_node then 
            attrs_list.Add (key, value)
        else
            attrs_list.[key] <- List.concat [attrs_list.[key]; value]
end