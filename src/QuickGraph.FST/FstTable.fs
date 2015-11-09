module QuickGraph.FST.FstTable

open System.Collections.Generic
open Microsoft.FSharp.Collections
open System.IO

let maxVal = System.UInt64.MaxValue

let fstToDot strs initState finalState filePrintPath =
    let rank s l =
        "{ rank=" + s + "; " + (l |> ResizeArray.map string |> ResizeArray.toArray |> String.concat " ") + " }\n"
    let s = 
        "digraph G {\n" 
        + "rankdir = LR\n"
        + "node [shape = circle]\n"
        + (initState |> ResizeArray.map(fun x -> sprintf "%i[style=filled, fillcolor=green]\n" x) |> ResizeArray.toArray |> String.concat "")
        + (finalState |> ResizeArray.map(fun x -> sprintf "%i[shape = doublecircle, style=filled, fillcolor=red]\n" x) |> ResizeArray.toArray |> String.concat "")
        + rank "same" initState 
        + rank "min" initState 
        + rank "same" finalState 
        + rank "max" finalState 
                          
    System.IO.File.WriteAllText(filePrintPath, s + (String.concat "" strs) + "\n}")
    ()

[<Struct>]
type Edge<'iType, 'oType> = 
    val StartV : int
    val InSymb : 'iType
    val OutSymb : 'oType
    val EndV : int
    new (startV, inSymb, outSymb, endV) = {StartV = startV; InSymb = inSymb; OutSymb = outSymb; EndV = endV} 


[<Struct>]
type SimpleFST<'iType, 'oType> =
    val InitState : ResizeArray<int>
    val Edges : ResizeArray<Edge<'iType, 'oType>>
    val FinalState : ResizeArray<int>
    new (initState, edges, finalState) = {InitState = initState; Edges = edges; FinalState = finalState}    


[<Class>]
type FST<'iType, 'oType when 'iType: comparison>() as this =
    //let mutable InitState = new ResizeArray<_>()
    let mutable TableOfTransitions = new  ResizeArray<ResizeArray<_>>()
    let mutable ArrayOfFunction = new ResizeArray<'iType -> 'oType>()
    //let mutable FinalState = new ResizeArray<_> ()
    let smbDict = new Dictionary<_, _>()

    let inputFSTtoFST (inputFST : SimpleFST<'iType, 'oType>) =        
        let stateDict =                        
            let incr () =
                let key = ref -1 
                fun () ->
                    incr key
                    !key

            let sttId = incr ()
            let smbId = incr ()

            
            let stateDict = new Dictionary<_, _>()
            for edge in inputFST.Edges do
                if not <| smbDict.ContainsKey(edge.InSymb) 
                then smbDict.Add(edge.InSymb, smbId())

                if not <| stateDict.ContainsKey(edge.StartV)
                then stateDict.Add(edge.StartV, sttId())

                if not <| stateDict.ContainsKey(edge.EndV)
                then stateDict.Add(edge.EndV, sttId())
            stateDict
        
        for stt in inputFST.InitState do
            (this.InitState:ResizeArray<_>) .Add stateDict.[stt]
        

        for stt in inputFST.FinalState do
            (this.FinalState:ResizeArray<_>) .Add stateDict.[stt]

        TableOfTransitions <- ResizeArray.init stateDict.Count (fun _ -> ResizeArray.init smbDict.Count (fun _ -> (-1, -1)))
        //ArrayOfFunction <- new ResizeArray<_>(inputFST.Edges.Count)
        let i = ref 0
        for edge in inputFST.Edges do
            TableOfTransitions.[stateDict.[edge.StartV]].[smbDict.[edge.InSymb]] <- (stateDict.[edge.EndV] , !i) 
            ArrayOfFunction.Add (fun _ -> edge.OutSymb)
            i := !i + 1

    let printFSTtoDOT filePrintPath =
        let printInDict = new Dictionary<_, _>(smbDict.Count) 
        for i in smbDict do
            printInDict.Add(i.Value, i.Key)

        let strs = 
            TableOfTransitions
            |> ResizeArray.mapi(fun i x -> 
                x
                |> ResizeArray.mapi(fun j y -> if fst y <> -1 then sprintf "%i -> %i [label=\"%s (%i)\"]; \n" i (fst y) (printInDict.[j].ToString().Replace("\"","\\\"")) j else ""))
            |> List.ofSeq
            |> ResizeArray.concat
                  
        fstToDot strs this.InitState this.FinalState filePrintPath
        ()
        
    member val InitState = new ResizeArray<_>() with get, set    
    member val FinalState = new ResizeArray<_> () with get, set  
    member this.Init (initState, tableOfTransitions, arrayOfFunction, finalState) = 
        this.InitState <- initState
        TableOfTransitions <- tableOfTransitions
        ArrayOfFunction <- arrayOfFunction
        this.FinalState <- finalState

    member this.LoadFromSimpleFST fst = inputFSTtoFST fst
    member this.printFST filePath = printFSTtoDOT filePath

        