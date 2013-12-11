module DotParserProject.Utils

open Option
open DotParserProject.GraphDataContainer
open System

let log msgList = 
    msgList |> List.map (printfn "Log: %s") |> ignore
    printfn ""

let optToStr optValue =
    if (isSome optValue) then (get optValue)
    else ""

let type_key = "type"
let strict_key = "is_strict"
let name_key = "name"

let addSubgraphToArray subgr_id (gr: GraphDataContainer) (array: ResizeArray<GraphDataContainer>) =
    if (isNone subgr_id || isNone (get subgr_id)) then 
        gr.AddGeneralInfo [strict_key, ""; type_key, "subgraph"; name_key, System.Guid.NewGuid().ToString()]
    else
        gr.AddGeneralInfo [strict_key, ""; type_key, "subgraph"; name_key, get (get subgr_id)]
    array.Add gr
    gr.GraphName