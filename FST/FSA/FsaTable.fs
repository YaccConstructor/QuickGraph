module YC.FSA.FsaTable

open System.Collections.Generic
open Microsoft.FSharp.Collections
open System.IO

let maxVal = System.UInt64.MaxValue

let fsaToDot strs initState finalState filePrintPath =
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
type Edge<'br> = 
    val StartV : int
    val Symb : string * 'br
    val EndV : int
    new (startV, symb, endV) = {StartV = startV; Symb = symb; EndV = endV} 


[<Struct>]
type SimpleFSA<'br> =
    val InitState : ResizeArray<int>
    val Edges : ResizeArray<Edge<'br>>
    val FinalState : ResizeArray<int>
    new (initState, edges, finalState) = {InitState = initState; Edges = edges; FinalState = finalState}    


[<Class>]
type FSA<'iType, 'oType when 'iType: comparison>() as this =
    let mutable TableOfTransitions = new  ResizeArray<ResizeArray<_>>()
    let smbDict = new Dictionary<_, _>()

    let inputFSAtoFSA (inputFSA : SimpleFSA<'br>) =        
        let stateDict =                        
            let incr () =
                let key = ref -1 
                fun () ->
                    incr key
                    !key

            let sttId = incr ()
            let smbId = incr ()

            
            let stateDict = new Dictionary<_, _>()
            for edge in inputFSA.Edges do
                if not <| smbDict.ContainsKey(fst edge.Symb) 
                then smbDict.Add(fst edge.Symb, smbId())

                if not <| stateDict.ContainsKey(edge.StartV)
                then stateDict.Add(edge.StartV, sttId())

                if not <| stateDict.ContainsKey(edge.EndV)
                then stateDict.Add(edge.EndV, sttId())
            stateDict
        
        for stt in inputFSA.InitState do
            (this.InitState:ResizeArray<_>) .Add stateDict.[stt]
        

        for stt in inputFSA.FinalState do
            (this.FinalState:ResizeArray<_>) .Add stateDict.[stt]

        TableOfTransitions <- ResizeArray.init stateDict.Count (fun _ -> ResizeArray.init smbDict.Count (fun _ -> -1))

        for edge in inputFSA.Edges do
            TableOfTransitions.[stateDict.[edge.StartV]].[smbDict.[fst edge.Symb]] <- stateDict.[edge.EndV] 

    let printFSAtoDOT filePrintPath =
        let printInDict = new Dictionary<_, _>(smbDict.Count) 
        for i in smbDict do
            printInDict.Add(i.Value, i.Key)

        let strs = 
            TableOfTransitions
            |> ResizeArray.mapi(fun i x -> 
                x
                |> ResizeArray.mapi(fun j y -> if y <> -1 then sprintf "%i -> %i [label=\"%s\"]; \n" i y (printInDict.[j].ToString().Replace("\"","\\\""))  else ""))
            |> List.ofSeq
            |> ResizeArray.concat
                  
        fsaToDot strs this.InitState this.FinalState filePrintPath
        ()
        
    member val InitState = new ResizeArray<_>() with get, set    
    member val FinalState = new ResizeArray<_> () with get, set  
    member this.Init (initState, tableOfTransitions, arrayOfFunction, finalState) = 
        this.InitState <- initState
        TableOfTransitions <- tableOfTransitions
        this.FinalState <- finalState

    member this.LoadFromSimpleFSA fsa = inputFSAtoFSA fsa
    member this.printFSA filePath = printFSAtoDOT filePath

        
