module YC.FSA.GraphBasedFst

open YC.FSA.FsaTable
open QuickGraph
open Microsoft.FSharp.Collections
open System.Collections.Generic
open QuickGraph.Algorithms.Search
open QuickGraph.Algorithms
open QuickGraph.Collections

type Smbl<'br> = 
    | Smbl of string * 'br
    | Eps

[<Struct>]
type EdgeLbl<'iType> =
    val Symb : Smbl<'iType>
    new symb = {Symb = symb}

[<Class>]
type FSA<'iType>(initial, final, transitions) as this= 
    inherit AdjacencyGraph<int,TaggedEdge<int,EdgeLbl<'iType>>>()
    do  
        transitions |> ResizeArray.map (fun (f,l,t) -> new TaggedEdge<_,_>(f,t,l))
        |> this.AddVerticesAndEdgeRange
        |> ignore

    let printFSAtoDOT filePrintPath printSmb =
        let strs = 
            let getVal s printSmb = 
                match s with
                | Smbl(y, br) -> (match printSmb with Some x -> x y | None -> y.ToString()).Replace("\"","\\\"")
                | Eps -> "Eps"

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
            new TaggedEdge<_,_>(v, !i, new EdgeLbl<_>(Eps)) |> resFSA.AddVerticesAndEdge  |> ignore
        
        for v in fsa2.InitState do
            new TaggedEdge<_,_>(!i, fsa2Dict.[v], new EdgeLbl<_>(Eps)) |> resFSA.AddVerticesAndEdge  |> ignore        
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
            new TaggedEdge<_,_>(!i, v, new EdgeLbl<_>(Eps)) |> resFSA.AddVerticesAndEdge  |> ignore

        for v in fsa2.InitState do
            new TaggedEdge<_,_>(!i, fst2Dict.[v], new EdgeLbl<_>(Eps)) |> resFSA.AddVerticesAndEdge  |> ignore  
                       
        for v in fsa1.FinalState do
            new TaggedEdge<_,_>(v, !i + 1, new EdgeLbl<_>(Eps)) |> resFSA.AddVerticesAndEdge  |> ignore

        for v in fsa2.FinalState do
            new TaggedEdge<_,_>(fst2Dict.[v], !i + 1, new EdgeLbl<_>(Eps)) |> resFSA.AddVerticesAndEdge  |> ignore
              
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
   