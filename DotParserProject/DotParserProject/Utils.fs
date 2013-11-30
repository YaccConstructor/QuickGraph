module DotParserProject.Utils

open Option

let log msgList = 
    msgList |> List.map (printfn "Log: %s") |> ignore
    printfn ""

let optToStr optValue =
    if (isSome optValue) then (get optValue)
    else ""