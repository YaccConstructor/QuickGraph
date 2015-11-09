module YC.FSA.FsaApproximation

open QuickGraph
open Microsoft.FSharp.Collections
open YC.FSA.GraphBasedFsa

[<Struct>]
type Position<'br> =
    val start_offset: int
    val end_offset: int
    val back_ref: 'br
    new (so, eo, br) = {start_offset = so; end_offset = eo; back_ref = br}

type EdgeLblAppr<'br> = string * 'br

and [<Class>]Appr<'br when 'br : equality>(initial, final, edges) as this =
    inherit AdjacencyGraph<int, TaggedEdge<int, EdgeLblAppr<'br>>>()
    do
        edges |> ResizeArray.map (fun (f,l,t) -> new TaggedEdge<_,_>(f,t,l))
        |> this.AddVerticesAndEdgeRange
        |> ignore

    new () =
        Appr<_>(new ResizeArray<int>(), new ResizeArray<int>(), new ResizeArray<_>())
             
    member val InitState =  initial with get, set
    member val FinalState = final with get, set
    member this.ApprToFSA() = 
        let resFSA = new FSA<_>()
        resFSA.InitState <- this.InitState
        resFSA.FinalState <- this.FinalState
        
        let counter = this.Vertices |> Seq.max |> ref

        let splitEdge (edg:TaggedEdge<int, EdgeLblAppr<'br>>) str br =
            let start = edg.Source
            let _end = edg.Target
            let pos = ref 0

            match str with
            | Some("") -> [|new EdgeFSA<_>(start, _end, Eps)|]
            | None -> [|new EdgeFSA<_>(start, _end, Eps)|]
            | Some(s) ->
                let l = s.Length
                let ss = s.ToCharArray()
                Array.init l
                    (fun i ->
                        match i with
                        | 0 when (l = 1)     -> new EdgeFSA<_>(start, _end, Smbl(ss.[i], new Position<'br>(0,1,br)))
                        | 0                  -> new EdgeFSA<_>(start, (incr counter; !counter), Smbl(ss.[i], new Position<_>(!pos,(incr pos; !pos),br)))
                        | i when (i = l - 1) -> new EdgeFSA<_>(!counter, _end, Smbl(ss.[i], new Position<_>(!pos,(incr pos; !pos),br)))
                        | i                  -> new EdgeFSA<_>(!counter, (incr counter; !counter), Smbl(ss.[i], new Position<_>(!pos,(incr pos; !pos),br)))
                    )

        let rec go (approximation:Appr<_>) =
            for edge in approximation.Edges do
                match edge.Tag with
                | (str, br) ->
                    let x = splitEdge edge (Some str) br
                    x |> resFSA.AddVerticesAndEdgeRange
                    |> ignore
        go this

        resFSA