module YC.FST.FstApproximation

open QuickGraph
open YC.FST.GraphBasedFst
open Microsoft.FSharp.Collections

[<Struct>]
type Position<'br> =
    val start_offset: int
    val end_offset: int
    val back_ref: 'br
    new (so, eo, br) = {start_offset = so; end_offset = eo; back_ref = br}

type EdgeLblAppr<'br> = Smb of string * 'br

and [<Class>]Appr<'br>(initial, final, edges) as this =
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
            let pos = ref 0

            match str with
            | Some("") -> failwith "We don't support eps-transition!" //[|new TaggedEdge<_,_>(start, _end, new EdgeLbl<_,_>(Eps, Eps))|] 
            | None -> [|new TaggedEdge<_,_>(start, _end, new EdgeLbl<_,_>(Eps, Eps))|]
            | Some(s) ->
                let l = s.Length
                let ss = s.ToCharArray()
                Array.init l 
                    (fun i ->
                        match i with
                        | 0 when (l = 1)     -> new TaggedEdge<_,_>(start, _end, new EdgeLbl<_,_>(Smbl (ss.[i], new Position<'br>(0,1,br)), Smbl (ss.[i]))) 
                        | 0                  -> new TaggedEdge<_,_>(start, (incr counter; !counter), new EdgeLbl<_,_>(Smbl (ss.[i], new Position<_>(!pos,(incr pos; !pos),br)), Smbl (ss.[i]))) 
                        | i when (i = l - 1) -> new TaggedEdge<_,_>(!counter, _end, new EdgeLbl<_,_>(Smbl (ss.[i], new Position<_>(!pos,(incr pos; !pos),br)), Smbl (ss.[i]))) 
                        | i                  -> new TaggedEdge<_,_>(!counter, (incr counter; !counter), new EdgeLbl<_,_>(Smbl (ss.[i], new Position<_>(!pos,(incr pos; !pos),br)), Smbl (ss.[i]))) 
                    )

        let rec go (approximation:Appr<_>) =
            for edge in approximation.Edges do
                match edge.Tag with 
                | Smb (str, br) -> 
                    let x = splitEdge edge (Some str) br
                    x |> resFST.AddVerticesAndEdgeRange
                    |> ignore                                      
        go this

        let vEOF = !counter + 1
        for v in resFST.FinalState do
            new TaggedEdge<_,_>(v, vEOF, new EdgeLbl<_,_>(Smbl (char 65535,  Unchecked.defaultof<Position<'br>>), Smbl (char 65535))) |> resFST.AddVerticesAndEdge |> ignore

        resFST.FinalState <- ResizeArray.singleton vEOF
        resFST

    new () = 
        Appr<_>(new ResizeArray<_>(),new ResizeArray<_>(),new ResizeArray<_>())
             
    member val InitState =  initial with get, set
    member val FinalState = final with get, set
    member this.ToFST () = toFSTGraph()

