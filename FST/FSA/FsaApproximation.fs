module YC.FSA.FsaApproximation

open QuickGraph
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

    new () =
        Appr<_>(new ResizeArray<int>(), new ResizeArray<int>(),new ResizeArray<_>())
             
    member val InitState =  initial with get, set
    member val FinalState = final with get, set   