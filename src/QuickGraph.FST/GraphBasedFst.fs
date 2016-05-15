module QuickGraph.FST.GraphBasedFst

open QuickGraph
open Microsoft.FSharp.Collections
open System.Collections.Generic
open QuickGraph.Algorithms.Search
open QuickGraph.Algorithms
open QuickGraph.Collections
open QuickGraph.FST.FstTable
open QuickGraph.FSA.GraphBasedFsa

let setVertexRemoved (fst:#IVertexListGraph<_,_>) startV =
        let dfs = DepthFirstSearchAlgorithm<_,_>(fst)
        dfs.Compute(startV)
        let vertexRemoved = new ResizeArray<_>() 
        for kvp in dfs.VertexColors  do
            if kvp.Value = GraphColor.White then vertexRemoved.Add kvp.Key
        vertexRemoved

let getReachableV (fst:#IVertexListGraph<_,_>) startV =
        let dfs = DepthFirstSearchAlgorithm<_,_>(fst)
        dfs.Compute(startV)
        let reachableV = new ResizeArray<_>() 
        for kvp in dfs.VertexColors  do
            if kvp.Value <> GraphColor.White then reachableV.Add kvp.Key
        reachableV

type EdgeFST<'a, 'b>(s,e,t)=
    inherit TaggedEdge<int, Symb<'a>*Symb<'b>>(s,e,t)

[<Class>]
type FST<'iType, 'oType>(initial, final, transitions) as this =
    inherit AdjacencyGraph<int,EdgeFST<'iType, 'oType>>()
    let cachedEdges = new ResizeArray<_>() |> ref
    do
        cachedEdges := transitions 
        this.AddVerticesAndEdgeRange !cachedEdges
        |> ignore

    let printFSTtoDOT filePrintPath printSmb =
        let strs =
            let getVal s printSmb =
                match s with
                | Smbl y -> (match printSmb with Some x -> x y | None -> y.ToString()).Replace("\"","\\\"")
                | Eps -> "Eps"

            this.Edges
            |> Seq.map (fun edge ->
                sprintf "%i -> %i [label=\"%s : %s\"]; \n" edge.Source edge.Target (getVal (fst edge.Tag) printSmb)  (getVal (snd edge.Tag) None))
       
        fstToDot strs this.InitState this.FinalState filePrintPath

    ///not path from start state to any final state 
    static let isEmptyFST (fst:FST<_,_>) =
        if fst.EdgeCount > 0 then
            let vRemove = setVertexRemoved fst fst.InitState.[0]
            let isRemove v = ResizeArray.exists ((=) v) vRemove
            ResizeArray.forall (isRemove) fst.FinalState
        else true  

    ///for FSTs, which are not empty
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
            new EdgeFST<_,_>(fst2Dict.[e.Source], fst2Dict.[e.Target], e.Tag) |> resFST.AddVerticesAndEdge |> ignore
        
        resFST.InitState <- fst1.InitState
        for v in fst2.FinalState do
            resFST.FinalState.Add(fst2Dict.[v])
        
        for v in fst1.FinalState do
            new EdgeFST<_,_>(v, !i, (Eps, Eps)) |> resFST.AddVerticesAndEdge  |> ignore
        
        for v in fst2.InitState do
            new EdgeFST<_,_>(!i, fst2Dict.[v], (Eps, Eps)) |> resFST.AddVerticesAndEdge  |> ignore
        resFST

    ///for FSTs, which are not empty
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
            new EdgeFST<_,_>(fst2Dict.[e.Source], fst2Dict.[e.Target], e.Tag) |> resFST.AddVerticesAndEdge |> ignore
        
        resFST.InitState.Add(!i)
        resFST.FinalState.Add(!i + 1)

        for v in fst1.InitState do
            new EdgeFST<_,_>(!i, v, (Eps, Eps)) |> resFST.AddVerticesAndEdge  |> ignore

        for v in fst2.InitState do
            new EdgeFST<_,_>(!i, fst2Dict.[v], (Eps, Eps)) |> resFST.AddVerticesAndEdge  |> ignore
                       
        for v in fst1.FinalState do
            new EdgeFST<_,_>(v, !i + 1, (Eps, Eps)) |> resFST.AddVerticesAndEdge  |> ignore

        for v in fst2.FinalState do
            new EdgeFST<_,_>(fst2Dict.[v], !i + 1, (Eps, Eps)) |> resFST.AddVerticesAndEdge  |> ignore
              
        resFST

    new (initial, final, transitions) =

        FST<_,_>(initial, final, transitions|> ResizeArray.map (fun (f,l,t) -> new EdgeFST<_,_>(f,t,l)))

    new () =
        FST<_,_>(new ResizeArray<_>(),new ResizeArray<_>(),new ResizeArray<EdgeFST<_,_>>())

    member val InitState =  initial with get, set
    member val FinalState = final with get, set
    member this.PrintToDOT(filePath, ?printSmb) = printFSTtoDOT filePath printSmb    
    member this.Concat fst2 = concat this fst2
    static member Concat(fst1, fst2) = concat fst1 fst2
    member this.Union fst2 = union this fst2
    static member Union(fst1, fst2) = union fst1 fst2
    member this.IsEmpty =  isEmptyFST this

    member this.CachedEdges = !cachedEdges
    member this.RecachEdges () = cachedEdges := this.Edges |> ResizeArray.ofSeq
         
    ///for FSA, which are not empty
    static member FSAtoFST(fsa:FSA<_>, transform, smblEOF) =
        let dfa = fsa.NfaToDfa()
        
        let edges = new ResizeArray<_>()
        for edge in dfa.Edges do
            new EdgeFST<_,_>(edge.Source, edge.Target, transform edge.Tag) |> edges.Add

        let vEOF = Seq.max dfa.Vertices + 1
        for v in dfa.FinalState do
            new EdgeFST<_,_>(v, vEOF, transform smblEOF) |> edges.Add

        let resFST =  new FST<_,_>(dfa.InitState, ResizeArray.singleton vEOF, edges)

        resFST     

    ///for FSTs, which are not empty
    static member oldCompose(fst1:FST<_,_>, fst2:FST<_,_>, alphabet:HashSet<_>) =
        let inline pack v1 v2 = (int64(v1) <<< 32) ||| int64(v2)
        let inline unpack v = (int(v >>> 32), int(v &&& 0xffffffffL))
        let errors = new ResizeArray<_>()
        for edge in fst1.Edges do
            if not <| alphabet.Contains(snd edge.Tag)
            then errors.Add(fst edge.Tag)
        if errors.Count > 0
        then Error (errors.ToArray())
        else
            let fstDict = new Dictionary<_,_>()
            let i = ref 0
            for v1 in fst1.Vertices do
                for v2 in fst2.Vertices do
                    fstDict.Add(pack v1 v2, !i)
                    incr i
             
            let resFST =  new FST<_,_>()
            let inline isEqual s1 s2 =               
                match s1,s2 with
                | Eps, Eps -> true
                | Smbl x, Smbl y -> x.Equals y
                | x,y -> false

            for edge1 in fst1.CachedEdges do
                for edge2 in fst2.CachedEdges do
                    if isEqual (snd edge1.Tag) (fst edge2.Tag)
                    then
                        new EdgeFST<_,_>(fstDict.[pack (edge1.Source) (edge2.Source)], fstDict.[pack (edge1.Target) (edge2.Target)], (fst edge1.Tag, snd edge2.Tag))
                        |> resFST.AddVerticesAndEdge  |> ignore 

            let isEpsilon x = match x with | Eps -> true | _ -> false

            for v1 in fst1.InitState do
                for v2 in fst2.InitState do
                    resFST.InitState.Add(fstDict.[pack v1 v2])
                    resFST.AddVertex(fstDict.[pack v1 v2]) |> ignore
                
            for v1 in fst1.FinalState do
                for v2 in fst2.FinalState do
                    resFST.FinalState.Add(fstDict.[pack v1 v2])
                    resFST.AddVertex(fstDict.[pack v1 v2]) |> ignore
            
            //resFST.PrintToDOT @"C:\yc\recursive-ascent\FST\FST\FST.Tests\DOTfst\fstt.dot"

// This code removes unreachable vertices. It's turned off for the sake of fair performance comparison.
//
//            if not(resFST.IsEmpty) then //result of composition is empty?
//                for v in resFST.InitState do
//                    new EdgeFST<_,_>(!i, v, (Eps, Eps)) |> resFST.AddVerticesAndEdge  |> ignore
//
//                for v in resFST.FinalState do
//                    new EdgeFST<_,_>(v, !i + 1, (Eps, Eps)) |> resFST.AddVerticesAndEdge  |> ignore
//
//                let reachableV1 = getReachableV resFST !i
//
//                let FSTtmp = new FST<_,_>()
//                for edge in resFST.Edges do
//                    new EdgeFST<_,_>(edge.Target, edge.Source, edge.Tag) |>  FSTtmp.AddVerticesAndEdge |> ignore
//
//                let reachableV2 = getReachableV FSTtmp (!i + 1)
//
//                let h = new HashSet<_>(reachableV1)
//                h.IntersectWith reachableV2
//                let result = new FST<_,_>(resFST.InitState, resFST.FinalState |> ResizeArray.filter (fun s -> h.Contains s), new ResizeArray<EdgeFST<_,_>>())
//                for v in h do
//                    for e in resFST.OutEdges v do
//                         if h.Contains e.Target
//                         then result.AddVerticesAndEdge e |> ignore
//                result.RemoveVertex(!i) |> ignore
//                result.RemoveVertex(!i + 1) |> ignore
//                // result.PrintToDOT @"C:\yc\temp\fstt0.dot"
//                Success result
//            else
//                let chFST1 = new ResizeArray<_>()
//                for ch in fst1.Edges do
//                    chFST1.Add (fst (ch.Tag))
//                Error (chFST1.ToArray())

            Success(resFST)
    
    static member Compose(fst1:FST<_, _>, fst2:FST<_, _>, alphabet:HashSet<_>) =
        let inline pack v1 v2 = (int64(v1) <<< 32) ||| int64(v2)
        let inline unpack v = (int(v >>> 32), int(v &&& 0xffffffffL))
        let inline isEqual s1 s2 =               
            match s1,s2 with
            | Eps, Eps -> true
            | Smbl x, Smbl y -> x.Equals y
            | x,y -> false

        let errors = new ResizeArray<_>()
        for edge in fst1.Edges do
            if not <| alphabet.Contains(snd edge.Tag)
            then errors.Add(fst edge.Tag)
        if errors.Count > 0 then
            Error (errors.ToArray())
        else
            // input FST's vertices mapping to resulting FST vertices 
            let i = ref 0
            let verticeDict = new Dictionary<int64, int>()
        
            for v1 in fst1.Vertices do
                for v2 in fst2.Vertices do
                    verticeDict.Add(pack v1 v2, !i)
                    incr i

            // initial fst
            let resFST =  new FST<_,_>()

            // main queue of algorithm
            let queue = Queue<int64>()

            // buffer helps to deal with cycles
            let buffer = Queue<int64>()

            // filling initial states
            for v1 in fst1.InitState do
                for v2 in fst2.InitState do
                    queue.Enqueue(pack v1 v2)
                    buffer.Enqueue(pack v1 v2)
                    resFST.InitState.Add(verticeDict.[pack v1 v2])
        
            // filling transitions
            while queue.Count <> 0 do
                let q = queue.Dequeue();
                let v1, v2 = unpack q

                // This adds V3*(E1+E2) complexity to the algorithm
                let fst1OutEdges = fst1.OutEdges v1
                let fst2OutEdges = fst2.OutEdges v2

                // filling final states
                if fst1.FinalState.Contains(v1) && fst2.FinalState.Contains(v2) then
                    resFST.FinalState.Add(verticeDict.[q])
                
                for e1 in fst1OutEdges do
                    for e2 in fst2OutEdges do
                        if isEqual (snd e1.Tag) (fst e2.Tag) then
                            let q' = pack e1.Target e2.Target
                            if not (buffer.Contains(q')) then
                                queue.Enqueue(q')
                                buffer.Enqueue(q')
                            new EdgeFST<_,_>(verticeDict.[q], verticeDict.[q'], (fst e1.Tag, snd e2.Tag))
                            |> resFST.AddVerticesAndEdge  |> ignore
            // resFST.PrintToDOT @"C:\yc\temp\fstt1.dot"
            Success(resFST)

and Test<'success, 'error> =
    | Success of 'success
    | Error of 'error 
