module DotParserProject.Utils

open Option

let Log msg_list = 
    msg_list |> List.map (printfn "Log: %s") |> ignore
    printfn ""

let OptToStr opt_value =
    if (isSome opt_value) then (get opt_value)
    else ""