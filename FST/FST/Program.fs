module YC.FST

open System.Collections.Generic
open Microsoft.FSharp.Collections
open System.IO

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
type InnerFST<'iType, 'oType when 'iType: comparison>() =
    let mutable InitState = new ResizeArray<_>()
    let mutable TableOfTransitions = new  ResizeArray<ResizeArray<_>>()
    let mutable ArrayOfFunction = new ResizeArray<'iType -> 'oType>()
    let mutable FinalState = new ResizeArray<_> ()
    let smbDict = new Dictionary<_, _>()

    let inputFSTtoInnerFST (inputFST : SimpleFST<'iType, 'oType>) =        
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
            InitState.Add stateDict.[stt]
        

        for stt in inputFST.FinalState do
            FinalState.Add stateDict.[stt]

        TableOfTransitions <- ResizeArray.init stateDict.Count (fun _ -> ResizeArray.init smbDict.Count (fun _ -> (-1, -1)))
        let i = ref 0
        for edge in inputFST.Edges do
            TableOfTransitions.[stateDict.[edge.StartV]].[smbDict.[edge.InSymb]] <- (stateDict.[edge.EndV] , !i) 
            ArrayOfFunction.[!i] <- fun _ -> edge.OutSymb
            i := !i + 1

    let printInnerFSTtoDOT filePrintPath =
        let s = "digraph G {\n"
        let strs = 
            TableOfTransitions
            |> ResizeArray.mapi(fun i x -> 
                x
                |> ResizeArray.mapi(fun j y -> sprintf "%i -> %i [label=\"%i\"]; \n" i (fst y) j))
            |> List.ofSeq
            |> ResizeArray.concat
                  
        System.IO.File.WriteAllText(filePrintPath, s + (String.concat "" strs) + "\n}")
        ()
        
    member this.Init (initState, tableOfTransitions, arrayOfFunction, finalState) = 
        InitState <- initState
        TableOfTransitions <- tableOfTransitions
        ArrayOfFunction <- arrayOfFunction
        FinalState <- finalState

    member this.LoadFromSimpleFST fst = inputFSTtoInnerFST fst
    

        