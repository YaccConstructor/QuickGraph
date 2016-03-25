namespace QuickGraph.Algorithms

open QuickGraph
open QuickGraph.Algorithms

(**
  Where do I have to add unit tests like following? I have added it to QuickGraph.Tests

    let g = new UndirectedGraph<int, IEdge<int>> ()
    g.AddVertexRange [0; 1; 2; 3] |> ignore
    g.AddEdge(new Edge<_>(0, 2))  |> ignore
    let algo = new BipartiteDimensionAlgorithm(g)
    algo.compute()
    Assert.AreEqual(algo.BipartiteDimensionValue = 2)

    g.AddVertexRange [1; 2; 3; 4; 5; 6; 7; 8; 9; 10] |> ignore
    [1,7; 1,8; 1,9; 1,10;
     2,6; 2,8; 2,9; 2,10;
     3,6; 3,7; 3,9; 3,10;
     4,6; 4,7; 4,8; 4,10;
     5,6; 5,7; 5,8; 5,9] |> List.iter (fun (u, v) -> g.AddEdge(new Edge<_>(u, v)) |> ignore)
    

**)
type private BipartiteGraph = {
    Verticies: int []
    LeftPart: int []
    RightPart: int []
    PartMap: int []
    AdjMat: bool [][]
    Edges: (int * int) []
} with member x.IsAdjacent(u, v) = x.AdjMat.[u].[v]
       member x.AdjacentVerticies v = 
           x.AdjMat.[v] 
           |> Array.mapi (fun ind value -> (ind, value)) 
           |> Array.choose(fun (ind, value) -> if value then Some(ind) else None)

type BipartiteDimensionAlgorithm(graph: UndirectedGraph<int, IEdge<int> >) as algo = 
    inherit Algorithms.AlgorithmBase<UndirectedGraph<int, IEdge<int> > >(graph)
    let mutable bdvalue = -1
    let mutable exitBecauseCanceled = false
    let mutable mapping = null
    let mutable bicliques = null
    let cancelManager = algo.Services.CancelManager
    
    let bipartiteGraph (leftPart: int []) (rightPart : int []) (edges: (int * int) []) = 
        let verts = Array.concat [leftPart; rightPart]
        let adjSize = Array.max verts + 1
        let part = Array.zeroCreate adjSize
        let adjMat = Array.init adjSize (fun _ -> Array.zeroCreate adjSize)
        let edges = edges |> Array.map (fun (u, v) -> if Array.tryFind ((=)u) leftPart |> Option.isSome then (u, v) else (v, u))
        leftPart |> Array.iter (fun v -> part.[v] <- 1)
        rightPart |> Array.iter (fun v -> part.[v] <- 2)
        edges |> Array.iter (fun (u, v) -> adjMat.[u].[v] <- true; adjMat.[v].[u] <- true)
        {
            Verticies = verts
            LeftPart = leftPart
            RightPart = rightPart
            PartMap = part
            AdjMat = adjMat
            Edges = edges
        }

    let  reduceBipartiteGraph (g: BipartiteGraph) : (BipartiteGraph * (int[])) = 
        let mergedWith = Array.init g.AdjMat.Length (fun _ -> -1) 
        for v in g.Verticies do
            if mergedWith.[v] = -1 then
                let adjV = g.AdjacentVerticies v
                for u in g.Verticies do
                    if u <> v && mergedWith.[u] = -1 && not (g.IsAdjacent(u, v)) && adjV = g.AdjacentVerticies u then
                        mergedWith.[u] <- v
        let edges = g.Edges |> Array.filter(fun (u, v) -> mergedWith.[u] = -1 && mergedWith.[v] = -1)

        let filt = Array.filter (fun v -> mergedWith.[v] = -1 && (g.AdjacentVerticies v |> Array.length > 0))
        bipartiteGraph (filt g.LeftPart) (filt g.RightPart) edges, mergedWith

    let convertUndirectedGraphToBipartite () = 
        let colors = new System.Collections.Generic.Dictionary<int, int>()
        let rec dfs v color =
            if colors.ContainsKey v then 
                if colors.[v] <> color then failwith "Graph not bipartite." else ()
            else 
                colors.Add(v, color)
                for e in graph.AdjacentEdges v do
                    dfs (e.GetOtherVertex v) (color % 2 + 1)
        for v in graph.Vertices do
            if colors.ContainsKey v then ()
            else dfs v 1
        let left = graph.Vertices |> Seq.filter (fun v -> colors.[v] = 1) |> Seq.toArray
        let right = graph.Vertices |> Seq.filter (fun v -> colors.[v] = 2) |> Seq.toArray
        let edges = graph.Edges |> Seq.toArray |> Array.map (fun e -> e.Source, e.Target)
        bipartiteGraph left right edges
    
    let bipartiteCoverAtMostK (g: BipartiteGraph) k = 
        let mapping = Array.init g.AdjMat.Length (fun _ -> Array.init k (fun _ -> false))
        let canAddVertexToClique v k = 
            g.Verticies 
            |> Array.filter (fun u -> g.PartMap.[v] <> g.PartMap.[u] && mapping.[u].[k] = true) 
            |> Array.forall (fun u -> g.AdjMat.[v].[u])
        let checkCover () = 
            g.Edges 
            |> Array.forall (fun (u, v) -> seq { 0 .. k - 1 } |> Seq.exists (fun ki -> mapping.[v].[ki] && mapping.[u].[ki]) )
        let rec travVerts vInd = 
            printfn "%d" k
            if cancelManager.IsCancelling then 
                exitBecauseCanceled <- true // Fast Exit
                true
            elif vInd >= g.Verticies.Length then
                // TRACE 
                //printfn "CHECK COVER(%A):" k
                //for i in 0 .. k - 1 do
                //    printfn "%s" (g.Verticies |> Seq.map (fun v -> ((if mapping.[v].[i] then 1 else 0) |> string) + " ") |> String.concat "")
                checkCover ()
            else 
                let v = g.Verticies.[vInd]
                let rec travBiCliques ki =
                    
                    if ki >= k then travVerts (vInd + 1)
                    else 
                        if travBiCliques (ki + 1) then true
                        else
                            if canAddVertexToClique v ki then
                                mapping.[v].[ki] <- true
                                if travBiCliques (ki + 1) then true 
                                else 
                                    mapping.[v].[ki] <- false
                                    false
                            else false
                travBiCliques 0 
        if travVerts 0 then Some(mapping) else None

    override this.InternalCompute() = 
        if graph.EdgeCount = 0 then bdvalue <- 0 
        else 
            let bipartite = convertUndirectedGraphToBipartite()
            let (g, mergedWith) = reduceBipartiteGraph bipartite
            let rec loop k = 
                if k > g.Edges.Length || cancelManager.IsCancelling then ()
                else 
                    match bipartiteCoverAtMostK g k with 
                    | Some(_) when exitBecauseCanceled -> ()
                    | Some(result) ->
                        bdvalue <- k
                        mapping <- result
                        bicliques <- Array.init k (fun i -> [0 .. g.AdjMat.Length - 1] |> List.filter (fun v -> mapping.[v].[i]) )
                    | None -> loop (k + 1)
            loop 1

    member x.BipartiteDimensionValue = if bdvalue <> -1 then bdvalue else failwith "Value not computed" 
    member x.GetNthBiclique i = bicliques.[i]

      
//let private biPartition (k: int) vertexCnt (leftPart: Set<int>) (edges: (int * int) []) =
//    let edgesCnt = Array.length edges
//    let check (edgeColors: int list) = 
//        seq { 0 .. k - 1 }
//        |> Seq.forall (fun color -> 
//            let bicliqueEdges = 
//                edgeColors
//                |> List.mapi (fun ind col -> (ind, col)) 
//                |> List.choose (fun (ind, col) -> if col = color then Some(edges.[ind]) else None)
//            let degree = Array.zeroCreate vertexCnt
//            bicliqueEdges
//            |> List.iter (fun (u, v) -> degree.[u] <- degree.[u] + 1; degree.[v] <- degree.[v] + 1)
//
//            let bicliqueVerticies = 
//                bicliqueEdges 
//                |> List.fold (fun s (u, v) -> s |> Set.add u |> Set.add v) Set.empty 
//            let leftCnt = 
//                bicliqueVerticies 
//                |> Set.filter (fun x -> Set.contains x leftPart) 
//                |> Set.count
//            let rightCnt = Set.count bicliqueVerticies - leftCnt
//            bicliqueVerticies
//            |> Set.forall (fun v -> degree.[v] = (if Set.contains v leftPart then rightCnt else leftCnt) )
//        )
//    let rec generate pref l =
//        if l = edgesCnt then
//            check pref
//        else    
//            let rec nth elem = 
//                if elem < k then
//                    generate (elem :: pref) (l + 1) || nth (elem + 1)
//                else false
//            nth 0
//    generate [] 0


//let biCover (initial: BipartiteGraph) = 
//    let (g, mergedWith) = reduceGraph initial
//    let cover = 
//        [1 .. g.Edges.Length] 
//        |> List.fold (fun found k -> match found with None -> biCoverK g k | x -> x ) None
//    ()

//type BipartiteAlgo<'v, 'e when 'e :> IEdge<'v> >(g: IVertexListGraph<'v,'e>) = 
//    inherit Algorithms.AlgorithmBase<IVertexListGraph<'v,'e> >(g)
//    override this.InternalCompute() = 
//        let x = 5
//        g.
//        ()
//