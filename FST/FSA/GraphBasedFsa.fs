module YC.FSA.GraphBasedFsa

open QuickGraph
open Microsoft.FSharp.Collections
open System.Collections.Generic
open QuickGraph.Algorithms.Search
open QuickGraph.Algorithms
open QuickGraph.Collections
open YC.FSA.FsaApproximation

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

type SmblFSA<'br> =
    | SmblFSA of char * 'br
    | EpsFSA

[<Struct>]
type EdgeLbl<'br> =
    val Symb : SmblFSA<'br>
    new symb = {Symb = symb}

[<Class>]
type FSA<'br>(initial, final, transitions) as this =
    inherit AdjacencyGraph<int,TaggedEdge<int,EdgeLbl<'br>>>()
    do
        transitions |> ResizeArray.map (fun (f,l,t) -> new TaggedEdge<_,_>(f,t,l))
        |> this.AddVerticesAndEdgeRange
        |> ignore

    let printFSAtoDOT filePrintPath printSmb =
        let strs = 
            let getVal s printSmb = 
                match s with
                | SmblFSA(y, br) -> (match printSmb with Some x -> x y | None -> y.ToString()).Replace("\"","\\\"")
                | EpsFSA -> "Eps"

            this.Edges
            |> Seq.map (fun edge ->
                sprintf "%i -> %i [label=\"%s\"]; \n" edge.Source edge.Target (getVal edge.Tag.Symb printSmb))
        
        fsaToDot strs this.InitState this.FinalState filePrintPath

    static let concat (fsa1:FSA<_>) (fsa2:FSA<_>) =
        let maxVert = Seq.max fsa1.Vertices
        let fsa2Dict = new Dictionary<_, _>()
        let i = ref (maxVert + 1)
        for v in fsa2.Vertices do
            if not <| fsa2Dict.ContainsKey(v)
            then fsa2Dict.Add(v, !i)
            i := !i + 1
             
        let resFSA =  new FSA<_>() 
        resFSA.AddVerticesAndEdgeRange fsa1.Edges |> ignore
        for e in fsa2.Edges do 
            new TaggedEdge<_,_>(fsa2Dict.[e.Source], fsa2Dict.[e.Target], e.Tag) |> resFSA.AddVerticesAndEdge |> ignore
        
        resFSA.InitState <- fsa1.InitState
        for v in fsa2.FinalState do
            resFSA.FinalState.Add(fsa2Dict.[v])  
        
        for v in fsa1.FinalState do
            new TaggedEdge<_,_>(v, !i, new EdgeLbl<_>(EpsFSA)) |> resFSA.AddVerticesAndEdge  |> ignore
        
        for v in fsa2.InitState do
            new TaggedEdge<_,_>(!i, fsa2Dict.[v], new EdgeLbl<_>(EpsFSA)) |> resFSA.AddVerticesAndEdge  |> ignore        
        resFSA

    static let union (fsa1:FSA<_>) (fsa2:FSA<_>) =
        let maxVert = Seq.max fsa1.Vertices
        let fst2Dict = new Dictionary<_, _>()
        let i = ref (maxVert + 1)
        for v in fsa2.Vertices do
            if not <| fst2Dict.ContainsKey(v)
            then fst2Dict.Add(v, !i)
            i := !i + 1
             
        let resFSA =  new FSA<_>() 
        resFSA.AddVerticesAndEdgeRange fsa1.Edges |> ignore
        for e in fsa2.Edges do 
            new TaggedEdge<_,_>(fst2Dict.[e.Source], fst2Dict.[e.Target], e.Tag) |> resFSA.AddVerticesAndEdge |> ignore
        
        resFSA.InitState.Add(!i)
        resFSA.FinalState.Add(!i + 1)

        for v in fsa1.InitState do
            new TaggedEdge<_,_>(!i, v, new EdgeLbl<_>(EpsFSA)) |> resFSA.AddVerticesAndEdge  |> ignore

        for v in fsa2.InitState do
            new TaggedEdge<_,_>(!i, fst2Dict.[v], new EdgeLbl<_>(EpsFSA)) |> resFSA.AddVerticesAndEdge  |> ignore  
                       
        for v in fsa1.FinalState do
            new TaggedEdge<_,_>(v, !i + 1, new EdgeLbl<_>(EpsFSA)) |> resFSA.AddVerticesAndEdge  |> ignore

        for v in fsa2.FinalState do
            new TaggedEdge<_,_>(fst2Dict.[v], !i + 1, new EdgeLbl<_>(EpsFSA)) |> resFSA.AddVerticesAndEdge  |> ignore
              
        resFSA

    static let replace (fsa1:FSA<_>) (fsa2:FSA<_>) (fsa3:FSA<_>) =              
        fsa1    

    new () = 
        FSA<_>(new ResizeArray<_>(),new ResizeArray<_>(),new ResizeArray<_>())

    member val InitState =  initial with get, set
    member val FinalState = final with get, set
    member this.PrintToDOT(filePath, ?printSmb) = printFSAtoDOT filePath printSmb
    member this.Concat fsa2 = concat this fsa2
    static member Concat(fsa1, fsa2) = concat fsa1 fsa2
    member this.Union fsa2 = union this fsa2
    static member Union(fsa1, fsa2) = union fsa1 fsa2
    ///fsa1 -- original strings; fsa2 -- match strings; fsa3 -- replacement strings
    static member Replace(fsa1, fsa2, fsa3) = replace fsa1 fsa2 fsa3
    static member ApprToFSA(appr:Appr<_>) = 
        let resFSA = new FSA<_>()
        resFSA.InitState <- appr.InitState
        resFSA.FinalState <- appr.FinalState
        
        let counter = appr.Vertices |> Seq.max |> ref

        let splitEdge (edg:TaggedEdge<int, EdgeLblAppr<'br>>) str br =
            let start = edg.Source
            let _end = edg.Target
            let pos = ref 0

            match str with
            | Some("") -> failwith "We don't support eps-transition!" //[|new TaggedEdge<_,_>(start, _end, new EdgeLbl<_>(Eps))|]
            | None -> [|new TaggedEdge<_,_>(start, _end, new EdgeLbl<_>(EpsFSA))|]
            | Some(s) ->
                let l = s.Length
                let ss = s.ToCharArray()
                Array.init l
                    (fun i ->
                        match i with
                        | 0 when (l = 1)     -> new TaggedEdge<_,_>(start, _end, new EdgeLbl<_>(SmblFSA (ss.[i], new Position<'br>(0,1,br))))
                        | 0                  -> new TaggedEdge<_,_>(start, (incr counter; !counter), new EdgeLbl<_>(SmblFSA (ss.[i], new Position<_>(!pos,(incr pos; !pos),br))))
                        | i when (i = l - 1) -> new TaggedEdge<_,_>(!counter, _end, new EdgeLbl<_>(SmblFSA (ss.[i], new Position<_>(!pos,(incr pos; !pos),br))))
                        | i                  -> new TaggedEdge<_,_>(!counter, (incr counter; !counter), new EdgeLbl<_>(SmblFSA (ss.[i], new Position<_>(!pos,(incr pos; !pos),br))))
                    )

        let rec go (approximation:Appr<_>) =
            for edge in approximation.Edges do
                match edge.Tag with
                | Smb (str, br) ->
                    let x = splitEdge edge (Some str) br
                    x |> resFSA.AddVerticesAndEdgeRange
                    |> ignore
        go appr

//        let vEOF = !counter + 1
//        for v in resFSA.FinalState do
//            new TaggedEdge<_,_>(v, vEOF, new EdgeLbl<_>(SmblFSA (char 65535,  Unchecked.defaultof<Position<'br>>))) |> resFSA.AddVerticesAndEdge |> ignore
//
//        resFSA.FinalState <- ResizeArray.singleton vEOF
        resFSA
   