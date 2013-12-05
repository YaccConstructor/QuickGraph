module GraphDataContainer

open System.Collections.Generic

type GraphDataContainer = class
    val vertices_lists : ResizeArray<string list>
    val graph_info : Dictionary<string, string>
    val vertices_attrs :  Dictionary<string, (string*string) list>
    val edges_attrs :  Dictionary<string, (string*string) list>
    val general_attrs : Dictionary<string, (string*string) list>
    val assign_stmt_list : ResizeArray<string*string>

    new () = {
        vertices_lists = new ResizeArray<string list>()
        graph_info = new Dictionary<string, string>()
        vertices_attrs = new Dictionary<string, (string*string) list>()
        edges_attrs = new Dictionary<string, (string*string) list>()
        general_attrs = new Dictionary<string, (string*string) list>()
        assign_stmt_list = new ResizeArray<string*string>()
        }
end