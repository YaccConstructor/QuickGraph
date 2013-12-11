module DotParserProject.GraphDataContainer

open System.Collections.Generic
open System.IO

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

    member x.GraphName = x.graph_info.Item "name"

    member private x.AddAttrToDict key value (attrs_list:Dictionary<string, (string*string) list>) =
        let contains_node = attrs_list.ContainsKey key
        if not contains_node then 
            attrs_list.Add (key, value)
        else
            attrs_list.[key] <- List.concat [attrs_list.[key]; value]

    member x.ToFiles() =
        let f_general_info = "..\\..\\..\\test_output\\collected_data_gen_info.txt"
        let f_vert = "..\\..\\..\\test_output\\collected_data_vertices.txt"
        let f_vert_attrs = "..\\..\\..\\test_output\\collected_data_vert_attrs.txt"
        let f_edges = "..\\..\\..\\test_output\\collected_data_edges.txt"
        let f_assign_list = "..\\..\\..\\test_output\\collected_data_assign_list.txt"

        let out_gen = File.CreateText f_general_info
        let out_vert = File.CreateText f_vert
        let out_vert_attrs = File.CreateText f_vert_attrs
        let out_edges = File.CreateText f_edges
        let out_assign = File.CreateText f_assign_list

        for entry in x.graph_info do out_gen.Write("{0}", entry)
        out_gen.WriteLine ""
        for entry in x.general_attrs do out_gen.WriteLine("{0} : {1}", entry.Key, entry.Value)
        x.vertices_lists |> ResizeArray.iter out_vert.WriteLine
        for entry in x.vertices_attrs do out_vert_attrs.WriteLine("{0} : {1}", entry.Key, entry.Value)
        for entry in x.edges_attrs do out_edges.WriteLine("{0} : {1}", entry.Key, entry.Value)
        x.assign_stmt_list |> ResizeArray.iter (fun (x, y) -> out_assign.WriteLine("{0} : {1}", x, y))

        out_gen.Close()
        out_vert.Close()
        out_vert_attrs.Close()
        out_edges.Close()
        out_assign.Close()

    member x.PrintAllCollectedData() =
        printfn "\nGeneral info:"
        for entry in x.graph_info do printf "%A " entry
        printfn "\n\nGeneral attributes:"
        for entry in x.general_attrs do printfn "%A: %A" entry.Key entry.Value
        printfn "\nVertices lists:"
        x.vertices_lists |> ResizeArray.iter (printfn "%A")
        printfn "\nVertices attributes:"
        for entry in x.vertices_attrs do printfn "%A: %A" entry.Key entry.Value
        printfn "\nEdges attributes:"
        for entry in x.edges_attrs do printfn "%A: %A" entry.Key entry.Value
        printfn "\nAssign statements:"
        x.assign_stmt_list |> ResizeArray.iter (printfn "%A")
end