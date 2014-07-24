module YC.FST.FstApproximation

open QuickGraph
open YC.FST.GraphBasedFst
open Microsoft.FSharp.Collections

type EdgeLblAppr<'br when 'br:comparison> = 
    | Smb of string * 'br
    | Repl of Appr<'br> * string * string
    | Trim of Appr<'br>

and [<Class>]Appr<'br when 'br:comparison>(initial, final, edges) as this =
    inherit AdjacencyGraph<int, TaggedEdge<int, EdgeLblAppr<'br>>>()
    do 
        edges |> ResizeArray.map (fun (f,l,t) -> new TaggedEdge<_,_>(f,t,l))
        |> this.AddVerticesAndEdgeRange
        |> ignore

    let toFSTGraph () =
        let resFST = new FST<_,_>()
        resFST.InitState <- this.InitState
        resFST.FinalState <- this.FinalState
        
        let counter = this.Vertices |> Seq.max |> ref

        let splitEdge (edg:TaggedEdge<int, EdgeLblAppr<'br>>) str br =
            let start = edg.Source
            let _end = edg.Target

            match str with
            | Some("") -> [|new TaggedEdge<_,_>(start, _end, new EdgeLbl<_,_>(None, None))|]
            | None -> [|new TaggedEdge<_,_>(start, _end, new EdgeLbl<_,_>(None, None))|]
            | Some(s) ->
                let l = s.Length
                let ss = s.ToCharArray()
                Array.init l 
                    (fun i ->
                        match i with
                        | 0 when (l = 1)     -> new TaggedEdge<_,_>(start, _end, new EdgeLbl<_,_>(Some (ss.[i], br), Some (ss.[i], br))) 
                        | 0                  -> new TaggedEdge<_,_>(start, (incr counter; !counter), new EdgeLbl<_,_>(Some (ss.[i], br), Some (ss.[i], br))) 
                        | i when (i = l - 1) -> new TaggedEdge<_,_>(!counter, _end, new EdgeLbl<_,_>(Some (ss.[i], br), Some (ss.[i], br))) 
                        | i                  -> new TaggedEdge<_,_>(!counter, (incr counter; !counter), new EdgeLbl<_,_>(Some (ss.[i], br), Some (ss.[i], br))) 
                    )

        let rec go (approximation:Appr<_>) =
            for edge in approximation.Edges do
                match edge.Tag with 
                | Smb (str, br) -> 
                    splitEdge edge (Some str) br
                    |> resFST.AddVerticesAndEdgeRange
                    |> ignore
                | Repl (a,str1,str2) -> go a
                | Trim a -> go a

        go this
        resFST

    new () = 
        Appr<_>(new ResizeArray<_>(),new ResizeArray<_>(),new ResizeArray<_>())
             
    member val InitState =  initial with get, set
    member val FinalState = final with get, set
    member this.ToFST () = toFSTGraph()

