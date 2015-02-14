module YC.FST.GraphBasedFst

open QuickGraph
open Microsoft.FSharp.Collections
open System.Collections.Generic
open QuickGraph.Algorithms.Search
open QuickGraph.Algorithms
open QuickGraph.Collections
open YC.FST.FstTable
open YC.FSA.GraphBasedFsa
open YC.FSA.FsaApproximation

let setVertexRemoved (fst:#IVertexListGraph<_,_>) startV =
        let dfs = DepthFirstSearchAlgorithm<_,_>(fst)
        dfs.Compute(startV)
        //dfs.VisitedGraph
        let vertexRemoved = dfs.VertexColors |> Seq.filter(fun x -> x.Value = GraphColor.White) |> Seq.map(fun x -> x.Key)
        vertexRemoved

type Smbl<'a> =
    | Smbl of 'a
    | Eps

[<Struct>]
type EdgeLbl<'iType, 'oType> =
    val InSymb : Smbl<'iType>
    val OutSymb : Smbl<'oType>
    new (inSymb, outSymb) = {InSymb = inSymb; OutSymb = outSymb}

[<Class>]
type FST<'iType, 'oType>(initial, final, transitions) as this =
    inherit AdjacencyGraph<int,TaggedEdge<int,EdgeLbl<'iType, 'oType>>>()//EdgeListGraph<int,TaggedEdge<int,EdgeLbl<'iType, 'oType>>>() //AdjacencyGraph<int,TaggedEdge<int,EdgeLbl<'iType, 'oType>>>()
    do
        transitions |> ResizeArray.map (fun (f,l,t) -> new TaggedEdge<_,_>(f,t,l))
        |> this.AddVerticesAndEdgeRange
        |> ignore

    let printFSTtoDOT filePrintPath printSmb =
        let strs =
            let getVal s printSmb =
                match s with
                | Smbl y -> (match printSmb with Some x -> x y | None -> y.ToString()).Replace("\"","\\\"")
                | Eps -> "Eps"

            this.Edges
            |> Seq.map (fun edge ->
                sprintf "%i -> %i [label=\"%s : %s\"]; \n" edge.Source edge.Target (getVal edge.Tag.InSymb printSmb)  (getVal edge.Tag.OutSymb None))
       
        fstToDot strs this.InitState this.FinalState filePrintPath

    static let concat (fst1:FST<_,_>) (fst2:FST<_,_>) =
        let maxVert = Seq.max fst1.Vertices
        let fst2Dict = new Dictionary<_, _>()
        let i = ref (maxVert + 1)
        for v in fst2.Vertices do
            if not <| fst2Dict.ContainsKey(v)
            then fst2Dict.Add(v, !i)
            i := !i + 1
             
        let resFST =  new FST<_,_>()
        fst1.Edges |> resFST.AddVerticesAndEdgeRange |> ignore
        for e in fst2.Edges do
            new TaggedEdge<_,_>(fst2Dict.[e.Source], fst2Dict.[e.Target], e.Tag) |> resFST.AddVerticesAndEdge |> ignore
        
        resFST.InitState <- fst1.InitState
        for v in fst2.FinalState do
            resFST.FinalState.Add(fst2Dict.[v])
        
        for v in fst1.FinalState do
            new TaggedEdge<_,_>(v, !i, new EdgeLbl<_,_>(Eps, Eps)) |> resFST.AddVerticesAndEdge  |> ignore
        
        for v in fst2.InitState do
            new TaggedEdge<_,_>(!i, fst2Dict.[v], new EdgeLbl<_,_>(Eps, Eps)) |> resFST.AddVerticesAndEdge  |> ignore
        resFST

    static let union (fst1:FST<_,_>) (fst2:FST<_,_>) =
        let maxVert = Seq.max fst1.Vertices
        let fst2Dict = new Dictionary<_, _>()
        let i = ref (maxVert + 1)
        for v in fst2.Vertices do
            if not <| fst2Dict.ContainsKey(v)
            then fst2Dict.Add(v, !i)
            i := !i + 1
             
        let resFST =  new FST<_,_>()
        fst1.Edges |> resFST.AddVerticesAndEdgeRange |> ignore
        for e in fst2.Edges do
            new TaggedEdge<_,_>(fst2Dict.[e.Source], fst2Dict.[e.Target], e.Tag) |> resFST.AddVerticesAndEdge |> ignore
        
        resFST.InitState.Add(!i)
        resFST.FinalState.Add(!i + 1)

        for v in fst1.InitState do
            new TaggedEdge<_,_>(!i, v, new EdgeLbl<_,_>(Eps, Eps)) |> resFST.AddVerticesAndEdge  |> ignore

        for v in fst2.InitState do
            new TaggedEdge<_,_>(!i, fst2Dict.[v], new EdgeLbl<_,_>(Eps, Eps)) |> resFST.AddVerticesAndEdge  |> ignore
                       
        for v in fst1.FinalState do
            new TaggedEdge<_,_>(v, !i + 1, new EdgeLbl<_,_>(Eps, Eps)) |> resFST.AddVerticesAndEdge  |> ignore

        for v in fst2.FinalState do
            new TaggedEdge<_,_>(fst2Dict.[v], !i + 1, new EdgeLbl<_,_>(Eps, Eps)) |> resFST.AddVerticesAndEdge  |> ignore
              
        resFST

    new () =
        FST<_,_>(new ResizeArray<_>(),new ResizeArray<_>(),new ResizeArray<_>())

    member val InitState =  initial with get, set
    member val FinalState = final with get, set
    member this.PrintToDOT(filePath, ?printSmb) = printFSTtoDOT filePath printSmb    
    member this.Concat fst2 = concat this fst2
    static member Concat(fst1, fst2) = concat fst1 fst2
    member this.Union fst2 = union this fst2
    static member Union(fst1, fst2) = union fst1 fst2
    
    static member FSAtoFST(fsa:FSA<_>) = 
        let resFST =  new FST<_,_>()
        resFST.InitState <- fsa.InitState
        resFST.FinalState <- fsa.FinalState

        let getValOut s = 
            match s with
            | SmblFSA(y, br) -> Smbl y
            | _ -> Eps

        let getValIn s = 
            match s with
            | SmblFSA(y, br) -> Smbl(y, br)
            | _ -> Eps

        for edge in fsa.Edges do
            new TaggedEdge<_,_>(edge.Source, edge.Target, new EdgeLbl<_,_>(getValIn edge.Tag.Symb, getValOut edge.Tag.Symb)) |> resFST.AddVerticesAndEdge  |> ignore

        let vEOF = Seq.max fsa.Vertices + 1
        for v in resFST.FinalState do
            new TaggedEdge<_,_>(v, vEOF, new EdgeLbl<_,_>(Smbl (char 65535,  Unchecked.defaultof<Position<'br>>), Smbl (char 65535))) |> resFST.AddVerticesAndEdge |> ignore

        resFST.FinalState <- ResizeArray.singleton vEOF

        resFST     

    static member Compos(fst1:FST<_,_>, fst2:FST<_,_>, alphabet:HashSet<_>) =
        let errors = new ResizeArray<_>()
        for edge in fst1.Edges do
            if not <| alphabet.Contains(edge.Tag.OutSymb)
            then errors.Add(edge.Tag.InSymb)
        if errors.Count > 0
        then Error (errors.ToArray())
        else
            let fstDict = Array.init ((*fst1.Vertices |> Seq.max |> ((+) 1)*)20000) (fun _ -> Array.zeroCreate (70000(*fst2.Vertices |> Seq.max |> ((+)1))*)))
            let i = ref 0
            for v1 in fst1.Vertices do
                for v2 in fst2.Vertices do
                    //fstDict.Add(v1, v2), !i)
                    fstDict.[v1].[v2] <- !i
                    i := !i + 1
             
            let resFST =  new FST<_,_>()
            let inline isEqual s1 s2 =                
                match s1,s2 with
                | Eps, Eps -> true
                | Smbl x, Smbl y -> if eq.IsSome then eq.Value x y else x = y
                | x,y -> false //failwithf "Cannot be compared %A and %A" x y

            let fst1e = fst1.Edges |> Array.ofSeq
            let fst2e = fst2.Edges |> Array.ofSeq

            for edge1 in fst1e do
                for edge2 in fst2e do 
                    if isEqual edge1.Tag.OutSymb edge2.Tag.InSymb
                    then
                        new TaggedEdge<_,_>(fstDict.[edge1.Source].[edge2.Source], fstDict.[edge1.Target].[edge2.Target], new EdgeLbl<_,_>(edge1.Tag.InSymb, edge2.Tag.OutSymb))
                        |> resFST.AddVerticesAndEdge  |> ignore          

            let inline isEpsilon x = match x with | Eps -> true | _ -> false

            for edge1 in fst1e do
                if isEpsilon edge1.Tag.OutSymb
                then
                    for v2 in fst2.Vertices do
                        match edge1.Tag.InSymb with
                        | Smbl _ ->
                            new TaggedEdge<_,_>(fstDict.[edge1.Source].[v2], fstDict.[edge1.Target].[v2], new EdgeLbl<_,_>(edge1.Tag.InSymb, Eps))
                            |> resFST.AddVerticesAndEdge  |> ignore
                        | Eps -> ()

            for edge2 in fst2e do
                if isEpsilon edge2.Tag.InSymb
                then
                    for v1 in fst1.Vertices do
                        match edge2.Tag.OutSymb with
                        | Smbl _ ->
                            new TaggedEdge<_,_>(fstDict.[v1].[edge2.Source], fstDict.[v1].[edge2.Target], new EdgeLbl<_,_>(Eps, edge2.Tag.OutSymb))
                            |> resFST.AddVerticesAndEdge  |> ignore
                        | _ -> ()

            for v1 in fst1.InitState do
                for v2 in fst2.InitState do
                    resFST.InitState.Add(fstDict.[v1].[v2])
                
            for v1 in fst1.FinalState do
                for v2 in fst2.FinalState do
                    resFST.FinalState.Add(fstDict.[v1].[v2])

            for v in resFST.InitState do
                new TaggedEdge<_,_>(!i, v, new EdgeLbl<_,_>(Eps, Eps)) |> resFST.AddVerticesAndEdge  |> ignore

            for v in resFST.FinalState do
                new TaggedEdge<_,_>(v, !i + 1, new EdgeLbl<_,_>(Eps, Eps)) |> resFST.AddVerticesAndEdge  |> ignore

//            let grAfterRemove1 = setVertexRemoved resFST !i
//            let tmpGr = new FST<_,_>()
//            for v in grAfterRemove1.Vertices do
//                for edge in grAfterRemove1.OutEdges(v) do
//                    new TaggedEdge<_,_>(edge.Target, edge.Source, edge.Tag) |>  tmpGr.AddVerticesAndEdge |> ignore
//
//            let grAfterRemove2 = setVertexRemoved tmpGr (!i + 1)
//            let resFSTAfterRemove = new FST<_,_>()
//            for v in grAfterRemove2.Vertices do
//                for edge in grAfterRemove2.OutEdges(v) do
//                    new TaggedEdge<_,_>(edge.Target, edge.Source, edge.Tag) |>  resFSTAfterRemove.AddVerticesAndEdge |> ignore

//            resFSTAfterRemove.InitState <- resFST.InitState
//            resFSTAfterRemove.FinalState <- resFST.FinalState
//            resFSTAfterRemove.RemoveVertex(!i) |> ignore
//            resFSTAfterRemove.RemoveVertex(!i + 1) |> ignore
            let vRemove1 = setVertexRemoved resFST !i
            for v in vRemove1 do
                resFST.RemoveVertex(v) |> ignore
                resFST.InitState.Remove(v) |> ignore
                resFST.FinalState.Remove(v) |> ignore

            let FSTtmp = new FST<_,_>()
            for edge in resFST.Edges do
                new TaggedEdge<_,_>(edge.Target, edge.Source, edge.Tag) |>  FSTtmp.AddVerticesAndEdge |> ignore

            let vRemove2 = setVertexRemoved FSTtmp (!i + 1)

            for v in vRemove2 do
                resFST.RemoveVertex(v) |> ignore
                resFST.InitState.Remove(v) |> ignore
                resFST.FinalState.Remove(v) |> ignore

            resFST.RemoveVertex(!i) |> ignore
            resFST.RemoveVertex(!i + 1) |> ignore
            Success resFST

and Test<'success, 'error> =
    | Success of 'success
    | Error of 'error 