module YC.FST.GraphBasedFST

open YC.FST.TableFST
open QuickGraph
open Microsoft.FSharp.Collections
open System.Collections.Generic
open QuickGraph.Algorithms.Search
open QuickGraph.Algorithms
open QuickGraph.Collections

let setVertexRemoved (fst:#IVertexListGraph<_,_>) startV = 
        let dfs = DepthFirstSearchAlgorithm<_,_>(fst)                
        dfs.Compute(startV)
        let vertexRemoved = dfs.VertexColors |> Seq.filter(fun x -> x.Value = GraphColor.White) |> Seq.map(fun x -> x.Key)
        vertexRemoved

[<Struct>]
type EdgeLbl<'iType, 'oType> =
    val InSymb : Option<'iType>
    val OutSymb : Option<'oType>    
    new (inSymb, outSymb) = {InSymb = inSymb; OutSymb = outSymb} 

[<Class>]
type FST<'iType, 'oType when 'oType: comparison and 'iType: comparison>(initial, final, transitions) as this= 
    inherit AdjacencyGraph<int,TaggedEdge<int,EdgeLbl<'iType, 'oType>>>()
    do  
        transitions |> ResizeArray.map (fun (f,l,t) -> new TaggedEdge<_,_>(f,t,l))
        |> this.AddVerticesAndEdgeRange
        |> ignore

    let printFSTtoDOT filePrintPath =
        let strs = 
            let getVal s = 
                match s with
                | Some y -> y.ToString().Replace("\"","\\\"")
                | None -> "Eps"

            this.Edges 
            |> Seq.map (fun edge ->
                sprintf "%i -> %i [label=\"%s : %s\"]; \n" edge.Source edge.Target (getVal edge.Tag.InSymb)  (getVal edge.Tag.OutSymb))
       
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
        resFST.AddVerticesAndEdgeRange fst1.Edges |> ignore
        for e in fst2.Edges do 
            new TaggedEdge<_,_>(fst2Dict.[e.Source], fst2Dict.[e.Target], e.Tag) |> resFST.AddVerticesAndEdge |> ignore
        
        resFST.InitState <- fst1.InitState
        for v in fst2.FinalState do
            resFST.FinalState.Add(fst2Dict.[v])  
        
        for v in fst1.FinalState do
            new TaggedEdge<_,_>(v, !i, new EdgeLbl<_,_>(None, None)) |> resFST.AddVerticesAndEdge  |> ignore
        
        for v in fst2.InitState do
            new TaggedEdge<_,_>(!i, fst2Dict.[v], new EdgeLbl<_,_>(None, None)) |> resFST.AddVerticesAndEdge  |> ignore        
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
        resFST.AddVerticesAndEdgeRange fst1.Edges |> ignore
        for e in fst2.Edges do 
            new TaggedEdge<_,_>(fst2Dict.[e.Source], fst2Dict.[e.Target], e.Tag) |> resFST.AddVerticesAndEdge |> ignore
        
        resFST.InitState.Add(!i)
        resFST.FinalState.Add(!i + 1)

        for v in fst1.InitState do
            new TaggedEdge<_,_>(!i, v, new EdgeLbl<_,_>(None, None)) |> resFST.AddVerticesAndEdge  |> ignore

        for v in fst2.InitState do
            new TaggedEdge<_,_>(!i, fst2Dict.[v], new EdgeLbl<_,_>(None, None)) |> resFST.AddVerticesAndEdge  |> ignore  
                       
        for v in fst1.FinalState do
            new TaggedEdge<_,_>(v, !i + 1, new EdgeLbl<_,_>(None, None)) |> resFST.AddVerticesAndEdge  |> ignore

        for v in fst2.FinalState do
            new TaggedEdge<_,_>(fst2Dict.[v], !i + 1, new EdgeLbl<_,_>(None, None)) |> resFST.AddVerticesAndEdge  |> ignore
              
        resFST

    new () = 
        FST<_,_>(new ResizeArray<_>(),new ResizeArray<_>(),new ResizeArray<_>())

    member val InitState =  initial with get, set
    member val FinalState = final with get, set
    member this.PrintToDOT filePath = printFSTtoDOT filePath
    member this.Concat fst2 = concat this fst2 
    static member Concat(fst1, fst2) = concat fst1 fst2
    member this.Union fst2 = union this fst2
    static member Union(fst1, fst2) = union fst1 fst2 
    static member Compos(fst1:FST<_,_>, fst2:FST<_,_>) = 
        let fstDict = new Dictionary<_,_>()
        let i = ref 0
        for v1 in fst1.Vertices do
            for v2 in fst2.Vertices do
                fstDict.Add((v1, v2), !i)
                i := !i + 1
             
        let resFST =  new FST<_,_>() 
        for edge1 in fst1.Edges do
            for edge2 in fst2.Edges do 
                match edge1.Tag.OutSymb, edge2.Tag.InSymb with
                    | x,y when x = y ->
                        new TaggedEdge<_,_>(fstDict.[(edge1.Source, edge2.Source)], fstDict.[(edge1.Target, edge2.Target)], new EdgeLbl<_,_>(edge1.Tag.InSymb, edge2.Tag.OutSymb))
                        |> resFST.AddVerticesAndEdge  |> ignore
                    | _ -> ()

        for edge1 in fst1.Edges do
            if edge1.Tag.OutSymb.IsNone 
            then
                for v2 in fst2.Vertices do
                    if edge1.Tag.InSymb.IsSome
                    then   
                        new TaggedEdge<_,_>(fstDict.[(edge1.Source, v2)], fstDict.[(edge1.Target, v2)], new EdgeLbl<_,_>(edge1.Tag.InSymb, None))
                        |> resFST.AddVerticesAndEdge  |> ignore

        for edge2 in fst2.Edges do
            if edge2.Tag.InSymb.IsNone
            then 
                for v1 in fst1.Vertices do
                    if edge2.Tag.OutSymb.IsSome 
                    then 
                        new TaggedEdge<_,_>(fstDict.[(v1, edge2.Source)], fstDict.[(v1, edge2.Target)], new EdgeLbl<_,_>(None, edge2.Tag.OutSymb))
                        |> resFST.AddVerticesAndEdge  |> ignore

        for v1 in fst1.InitState do
            for v2 in fst2.InitState do
                resFST.InitState.Add(fstDict.[(v1, v2)])
                
        for v1 in fst1.FinalState do
            for v2 in fst2.FinalState do
                resFST.FinalState.Add(fstDict.[(v1, v2)])

        for v in resFST.InitState do
            new TaggedEdge<_,_>(!i, v, new EdgeLbl<_,_>(None, None)) |> resFST.AddVerticesAndEdge  |> ignore

        for v in resFST.FinalState do
            new TaggedEdge<_,_>(v, !i + 1, new EdgeLbl<_,_>(None, None)) |> resFST.AddVerticesAndEdge  |> ignore

        let vRemove1 = setVertexRemoved resFST !i

        for v in vRemove1 do
            resFST.RemoveVertex(v) |> ignore

        let FSTtmp = new FST<_,_>()
        for edge in resFST.Edges do
            new TaggedEdge<_,_>(edge.Target, edge.Source, edge.Tag) |>  FSTtmp.AddVerticesAndEdge |> ignore

        let vRemove2 = setVertexRemoved FSTtmp (!i + 1)

        for v in vRemove2 do
            resFST.RemoveVertex(v) |> ignore

        resFST.RemoveVertex(!i) |> ignore
        resFST.RemoveVertex(!i + 1) |> ignore
        resFST 

